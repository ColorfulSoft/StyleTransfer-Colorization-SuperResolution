//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Функции потерь(вычисления градиентов).</summary>
    public static class Losses
    {

        ///<summary>Функция взвешенной ошибки стиля изображения. L(style) = W(G(F(X)) - G(F(S))).</summary>
        ///<param name="X">Карта признаков на выходе со слоя. Градиенты будут записаны в DW тензора.</param>
        ///<param name="S">Матрица Грама для выходной карты признаков со слоя.</param>
        ///<param name="k">Вес ошибки.</param>
        public static float StyleLoss(Tensor X, Tensor S, float k)
        {
            var _Outp = Math.Gram_Matrix(X);
            var _Dest = S;
            double Temp = 0.0;
            var D = new Tensor(_Outp.Width, _Outp.Height, 1, false);
            for(int i = 0; i < _Outp.W.Length; i++)
            {
                var Delta = (_Outp.W[i] - _Dest.W[i]) * k;
                Temp += Delta * Delta;
                D.W[i] = Delta;
            }
            var M = X.Width * X.Height;
            var N = X.Depth;
            var Result = (float)((double)Temp / ((long)4 * M * M * N * N));
            var Ft = Math.Transpose2D(Math.Flat(X));
            var Grad = Math.MatMul2D(D, Ft);
            Parallel.For(0, X.Depth, (int _d) =>
            {
                int i = 0;
                for(int y = 0; y < X.Height; y++)
                {
                    for(int x = 0; x < X.Width; x++)
                    {
                        X.AddDW(x, y, _d, (float)((double)Grad.GetW(_d, i, 0) / ((long)M * M * N * N)));
                        i += 1;
                    }
                }
            });
            return Result;
        }

        ///<summary>Функция взвешенной ошибки контента. L(content) = W(X - C)</summary>
        ///<param name="X">Карта признаков на выходе со слоя. Градиенты будут записаны в DW тензора.</param>
        ///<param name="C">Требуемая карта признаков для изображения.</param>
        ///<param name="k">Вес ошибки.</param>
        public static float ContentLoss(Tensor X, Tensor C, float k)
        {
            float Result = 0.0f;
            var M = X.Width * X.Height;
            var N = X.Depth;
            Parallel.For(0, X.Depth, (int d) =>
            {
                var Sum = 0.0f;
                for(int y = 0; y < X.Height; y++)
                {
                    for(int x = 0; x < X.Width; x++)
                    {
                        var Delta = X.GetW(x, y, d) - C.GetW(x, y, d);
                        X.AddDW(x, y, d, (float)((double)Delta / ((long)M * M * N * N) * k));
                        Sum += Delta * Delta;
                    }
                }
                lock((object)Result)
                {
                    Result += (float)((double)Sum / ((long)4 * M * M * N * N) * k);
                }
            });
            return Result;
        }

    }

}