//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя WCTLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

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

        ///<summary>Реализует слой WCT.</summary>
        ///<param name="Content">Карты признаков контентного изображения.</param>
        ///<param name="Style">Карты признаков стилевого изображения.</param>
        ///<param name="alpha">Глубина стилизации. [0..1], по умолчанию = 0.6.</param>
        public static Tensor WCT(Tensor Content, Tensor Style, float alpha = 0.6f)
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
            var s_d = s_e.Slice1D(0, k_s).Pow(0.5f);
            var targetFeature = Tensor.MatMul(Tensor.MatMul(Tensor.MatMul(Slice2dHeight(s_v, 0, k_s), s_d.Diag()), Slice2dHeight(s_v, 0, k_s).Transpose()), whiten_cF);
            targetFeature = targetFeature.AddHeight(Style_mean);
            var Out = targetFeature.Unflat(Content.Width, Content.Height);
            var OneMinusAlpha = 1f - alpha;
            Parallel.For(0, Out.Depth, (int d) =>
            {
                for(int y = 0; y < Out.Height; y++)
                {
                    for(int x = 0; x < Out.Width; x++)
                    {
                        Out.Set(x, y, d, Out.Get(x, y, d) * alpha + Content.Get(x, y, d) * OneMinusAlpha);
                    }
                }
            });
            return Out;
        }

    }

}