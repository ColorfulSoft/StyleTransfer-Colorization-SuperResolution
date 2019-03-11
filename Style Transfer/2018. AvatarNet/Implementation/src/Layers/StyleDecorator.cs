//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя StyleDecoratorLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        private static Tensor[] extract_image_patches(Tensor val, int patch_size = 3, int stride = 2)
        {
            var Result = new Tensor[(val.Width / stride) * (val.Height / stride)];
            int i = 0;
            for(int py = 0; py < val.Height / stride; py++)
            {
                for(int px = 0; px < val.Width / stride; px++)
                {
                    Result[i] = new Tensor(patch_size, patch_size, val.Depth);
                    var f = Result[i];
                    var y = py * stride - patch_size / 2;
                    var x = px * stride - patch_size / 2;
                    for(int fy = 0; fy < patch_size; fy++)
                    {
                        for(int fx = 0; fx < patch_size; fx++)
                        {
                            var iy = y + fy;
                            var ix = x + fx;
                            if((iy >= 0) && (iy < val.Height) && (ix >= 0) && (ix < val.Width))
                            {
                                for(int d = 0; d < val.Depth; d++)
                                {
                                    f.Set(fx, fy, d, val.Get(ix, iy, d));
                                }
                            }
                        }
                    }
                    i += 1;
                }
            }
            return Result;
        }

        private static Tensor[] l2_norm(Tensor[] val, float eps = 1e-12f)
        {
            var Result = new Tensor[val.Length];
            Parallel.For(0, val.Length, (int i) =>
            {
                var sqr_sum = 0f;
                var f = val[i];
                Result[i] = new Tensor(f.Width, f.Height, f.Depth);
                var df = Result[i];
                for(int d = 0; d < f.Depth; d++)
                {
                    for(int y = 0; y < f.Height; y++)
                    {
                        for(int x = 0; x < f.Width; x++)
                        {
                            sqr_sum += f.Get(x, y, d) * f.Get(x, y, d);
                        }
                    }
                }
                sqr_sum = (float)System.Math.Sqrt(System.Math.Max(sqr_sum, eps));
                for(int d = 0; d < df.Depth; d++)
                {
                    for(int y = 0; y < df.Height; y++)
                    {
                        for(int x = 0; x < df.Width; x++)
                        {
                            df.Set(x, y, d, f.Get(x, y, d) / sqr_sum);
                        }
                    }
                }
            });
            return Result;
        }

        private static Tensor argmax(Tensor val)
        {
            var Result = new Tensor(val.Width, val.Height, 1);
            Parallel.For(0, val.Height, (int y) =>
            {
                for(int x = 0; x < val.Width; x++)
                {
                    var max = float.MinValue;
                    var maxI = -1;
                    for(int d = 0; d < val.Depth; d++)
                    {
                        if(val.Get(x, y, d) > max)
                        {
                            max = val.Get(x, y, d);
                            maxI = d;
                        }
                    }
                    Result.Set(x, y, 0, maxI);
                }
            });
            return Result;
        }

        private static Tensor one_hot(Tensor val, int depth)
        {
            var Result = new Tensor(val.Width, val.Height, depth);
            Parallel.For(0, val.Height, (int y) =>
            {
                for(int x = 0; x < val.Width; x++)
                {
                    Result.Set(x, y, (int)val.Get(x, y, 0), 1f);
                }
            });
            return Result;
        }

        private static Tensor reduce_sum(Tensor val)
        {
            var Result = new Tensor(val.Width, val.Height, 1);
            Parallel.For(0, val.Height, (int y) =>
            {
                for(int x = 0; x < val.Width; x++)
                {
                    var sum = 0f;
                    for(int d = 0; d < val.Depth; d++)
                    {
                        sum += val.Get(x, y, d);
                    }
                    Result.Set(x, y, 0, sum);
                }
            });
            return Result;
        }

        private static Tensor ones(int width, int height, int depth)
        {
            var Result = new Tensor(width, height, depth);
            Parallel.For(0, depth, (int d) =>
            {
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        Result.Set(x, y, d, 1f);
                    }
                }
            });
            return Result;
        }

        private static Tensor tile(Tensor val, int n)
        {
            var Result = new Tensor(val.Width, val.Height, n);
            Parallel.For(0, n, (int d) =>
            {
                for(int y = 0; y < val.Height; y++)
                {
                    for(int x = 0; x < val.Width; x++)
                    {
                        Result.Set(x, y, d, val.Get(x, y, 0));
                    }
                }
            });
            return Result;
        }

        private static Tensor divide(Tensor a, Tensor b)
        {
            var Result = new Tensor(a.Width, a.Height, a.Depth);
            Parallel.For(0, a.Depth, (int d) =>
            {
                for(int y = 0; y < a.Height; y++)
                {
                    for(int x = 0; x < a.Width; x++)
                    {
                        Result.Set(x, y, d, a.Get(x, y, d) / b.Get(x, y, d));
                    }
                }
            });
            return Result;
        }

        private static Tensor StyleSwap(Tensor Content, Tensor Style, int patch_size = 3)
        {
            var style_kernels = extract_image_patches(Style, patch_size, 1);
            var style_kernels_norm = l2_norm(style_kernels);
            var ss_enc = Layers.Conv2D(Content, style_kernels_norm, 1);
            var ss_argmax = argmax(ss_enc);
            var encC = ss_enc.Depth;
            var ss_oh = one_hot(ss_argmax, encC);
            var ss_dec = Layers.ConvTranspose2D(ss_oh, style_kernels, 1);
            var ss_oh_sum = reduce_sum(ss_oh);
            var filter_ones = new Tensor[1]{ones(patch_size, patch_size, 1)};
            var counting = Layers.ConvTranspose2D(ss_oh_sum, filter_ones, 1);
            counting = tile(counting, Content.Depth);
            return divide(ss_dec, counting);
        }

        private static Tensor Slice2dHeight(Tensor T, int Start, int Count)
        {
            var Result = new Tensor(Count, T.Height, 1);
            Parallel.For(0, T.Height, (int y) =>
            {
                for(int x = Start; x < Count + Start; x++)
                {
                    Result[x - Start, y] = T[x, y];
                }
            });
            return Result;
        }

        ///<summary>Реализует слой StyleDecorator.</summary>
        ///<param name="Content">Карты признаков контентного изображения.</param>
        ///<param name="Style">Карты признаков стилевого изображения.</param>
        public static Tensor StyleDecorator(Tensor Content, Tensor Style)
        {
            var content_flat = Content.Flat();
            var style_flat = Style.Flat();
            var Content_mean = content_flat.HeightMean();
            var cF = content_flat.SubHeight(Content_mean);
            var contentCov = Tensor.MatMul(cF, cF.Transpose()).Div(Content.Height * Content.Width - 1f);
            var Style_mean = style_flat.HeightMean();
            var sF = style_flat.SubHeight(Style_mean);
            var styleCov = Tensor.MatMul(sF, sF.Transpose()).Div(Style.Height * Style.Width - 1f);
            var c_usv = LinAlg.SVD(contentCov);
            var c_u = c_usv.Item1;
            var c_e = c_usv.Item2;
            var c_v = c_usv.Item3;
            var s_usv = LinAlg.SVD(styleCov);
            var s_u = s_usv.Item1;
            var s_e = s_usv.Item2;
            var s_v = s_usv.Item3;
            var k_c = c_e.W.Length;
            for(int i = 0; i < c_e.W.Length; i++)
            {
                if(c_e.W[i] < 0.00001f)
                {
                    k_c = i - 1;
                    break;
                }
            }
            var k_s = s_e.W.Length;
            for(int i = 0; i < s_e.W.Length; i++)
            {
                if(s_e.W[i] < 0.00001f)
                {
                    k_s = i - 1;
                    break;
                }
            }
            var c_d = c_e.Slice1D(0, k_c).Pow(-0.5f);
            var step1 = Tensor.MatMul(Slice2dHeight(c_v, 0, k_c), c_d.Diag());
            var step2 = Tensor.MatMul(step1, Slice2dHeight(c_v, 0, k_c).Transpose());
            var whiten_cF = Tensor.MatMul(step2, cF);
            var whiten_content = whiten_cF.Unflat(Content.Width, Content.Height);
            var s_d = s_e.Slice1D(0, k_s).Pow(-0.5f);
            var whiten_style = Tensor.MatMul(Tensor.MatMul(Tensor.MatMul(Slice2dHeight(s_v, 0, k_s), s_d.Diag()), Slice2dHeight(s_v, 0, k_s).Transpose()), sF).Unflat(Style.Width, Style.Height);
            var s_d1 = s_e.Slice1D(0, k_s).Pow(0.5f);
            var targetFeature = Tensor.MatMul(Tensor.MatMul(Tensor.MatMul(Slice2dHeight(s_v, 0, k_s), s_d1.Diag()), Slice2dHeight(s_v, 0, k_s).Transpose()), StyleSwap(whiten_content, whiten_style).Flat());
            targetFeature = targetFeature.AddHeight(Style_mean);
            var Out = targetFeature.Unflat(Content.Width, Content.Height);
            return Out;
        }

    }

}