//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя Conv2DLayer.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        private static Tensor[] DilateFilters(Tensor[] T, int dilation = 1)
        {
            if(dilation == 1)
            {
                return T;
            }
            else
            {
                var Result = new Tensor[T.Length];
                Parallel.For(0, T.Length, (int i) =>
                {
                    Result[i] = new Tensor(T[i].Width * dilation + dilation - 1, T[i].Height * dilation + dilation - 1, T[i].Depth);
                    var F = Result[i];
                    var f = T[i];
                    for(int y = 0; y < f.Height; y++)
                    {
                        for(int x = 0; x < f.Width; x++)
                        {
                            for(int d = 0; d < f.Depth; d++)
                            {
                                F.Set(x * dilation + dilation - 1, y * dilation + dilation - 1, d, f.Get(x, y, d));
                            }
                        }
                    }
                });
                return Result;
            }
        }

        ///<summary>Реализует слой двумерной свёртки.</summary>
        ///<param name="input">Входной тензор.</param>
        ///<param name="Filters">Фильтры.</param>
        ///<param name="Biases">Смещения.</param>
        ///<param name="stride">Шаг.</param>
        ///<param name="dilation">Разрежение.</param>
        public static Tensor Conv2D(Tensor input, Tensor[] Filters, Tensor Biases, int stride = 1, int dilation = 1)
        {
            Filters = DilateFilters(Filters, dilation);
            var OutputDepth = Filters.Length;
            var Result = new Tensor(input.Width / stride, input.Height / stride, OutputDepth);
            Parallel.For(0, OutputDepth, (int d) =>
            {
                var f = Filters[d];
                for (int ay = 0; ay < Result.Height; ay++)
                {
                    var y = ay * stride - f.Height / 2;
                    for (int ax = 0; ax < Result.Width; ax++)
                    {
                        var x = ax * stride - f.Width / 2;
                        var a = 0.0f;
                        for (int fy = 0; fy < f.Height; fy++)
                        {
                            var oy = y + fy;
                            for (int fx = 0; fx < f.Width; fx++)
                            {
                                var ox = x + fx;
                                if ((oy >= 0) && (oy < input.Height) && (ox >= 0) && (ox < input.Width))
                                {
                                    var fi = ((f.Width * fy) + fx) * f.Depth;
                                    var ti = ((input.Width * oy) + ox) * input.Depth;
                                    for (var fd = 0; fd < f.Depth; fd++)
                                    {
                                        a += f.W[fi + fd] * input.W[ti + fd];
                                    }
                                }
                            }
                        }
                        Result.Set(ax, ay, d, a + Biases.W[d]);
                    }
                }
            });
            return Result;
        }

    }

}