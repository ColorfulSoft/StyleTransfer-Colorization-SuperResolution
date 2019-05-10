//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя StyleSwapLayer.

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

        ///<summary>Реализует слой StyleSwap.</summary>
        ///<param name="Content">Карты признаков контентного изображения.</param>
        ///<param name="Style">Карты признаков стилевого изображения.</param>
        public static Tensor StyleSwap(Tensor Content, Tensor Style, int patch_size = 3, int stride = 2)
        {
            var style_kernels = extract_image_patches(Style, patch_size, stride);
            var style_kernels_norm = l2_norm(style_kernels);
            var ss_enc = Layers.Conv2D(Content, style_kernels_norm, stride);
            var ss_argmax = argmax(ss_enc);
            var encC = ss_enc.Depth;
            var ss_oh = one_hot(ss_argmax, encC);
            var ss_dec = Layers.ConvTranspose2D(ss_oh, style_kernels, stride);
            var ss_oh_sum = reduce_sum(ss_oh);
            var filter_ones = new Tensor[1]{ones(patch_size, patch_size, stride)};
            var counting = Layers.ConvTranspose2D(ss_oh_sum, filter_ones, stride);
            counting = tile(counting, Content.Depth);
            return divide(ss_dec, counting);
        }

    }

}
