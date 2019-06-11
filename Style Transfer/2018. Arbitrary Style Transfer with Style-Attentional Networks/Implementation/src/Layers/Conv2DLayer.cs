//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя Conv2DLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    // В архитектуре сети все свёртки имеют фильтры размером 1x1 либо 3x3 и шаг = 1. Для упрощения кода и оптимизации,
    // эти значения были интегрированы в код.

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        ///<summary>Реализует слой двумерной свёртки с фильтром 3x3.</summary>
        ///<param name="input">Входной тензор.</param>
        ///<param name="Filters">Фильтры.</param>
        ///<param name="Biases">Смещения.</param>
        public static Tensor Conv2D3x3(Tensor input, Tensor[] Filters, Tensor Biases)
        {
            var OutputDepth = Filters.Length;
            var Result = new Tensor(input.Width, input.Height, OutputDepth);
            Parallel.For(0, OutputDepth, (int d) =>
            {
                var f = Filters[d];
                for (int ay = 0; ay < input.Height; ay++)
                {
                    var y = ay - 1;
                    for (int ax = 0; ax < input.Width; ax++)
                    {
                        var x = ax - 1;
                        var a = 0.0f;
                        for (byte fy = 0; fy < 3; fy++)
                        {
                            var oy = y + fy;
                            for (byte fx = 0; fx < 3; fx++)
                            {
                                var ox = x + fx;
                                if ((oy >= 0) && (oy < input.Height) && (ox >= 0) && (ox < input.Width))
                                {
                                    var fi = ((3 * fy) + fx) * f.Depth;
                                    var ti = ((input.Width * oy) + ox) * input.Depth;
                                    for (var fd = 0; fd < f.Depth; fd++)
                                    {
                                        a += f.W[fi + fd] * input.W[ti + fd];
                                    }
                                }
                            }
                        }
                        a += Biases.W[d];
                        Result.Set(ax, ay, d, a);
                    }
                }
            });
            return Result;
        }

        ///<summary>Реализует слой двумерной свёртки с фильтром 1x1.</summary>
        ///<param name="input">Входной тензор.</param>
        ///<param name="Filters">Фильтры.</param>
        ///<param name="Biases">Смещения.</param>
        public static Tensor Conv2D1x1(Tensor input, Tensor[] Filters, Tensor Biases)
        {
            var OutputDepth = Filters.Length;
            var Result = new Tensor(input.Width, input.Height, OutputDepth);
            Parallel.For(0, OutputDepth, (int d) =>
            {
                var f = Filters[d];
                for (int ay = 0; ay < input.Height; ay++)
                {
                    var y = ay;
                    for (int ax = 0; ax < input.Width; ax++)
                    {
                        var x = ax;
                        var a = 0.0f;
                        for (byte fy = 0; fy < 1; fy++)
                        {
                            var oy = y + fy;
                            for (byte fx = 0; fx < 1; fx++)
                            {
                                var ox = x + fx;
                                if ((oy >= 0) && (oy < input.Height) && (ox >= 0) && (ox < input.Width))
                                {
                                    var fi = ((1 * fy) + fx) * f.Depth;
                                    var ti = ((input.Width * oy) + ox) * input.Depth;
                                    for (var fd = 0; fd < f.Depth; fd++)
                                    {
                                        a += f.W[fi + fd] * input.W[ti + fd];
                                    }
                                }
                            }
                        }
                        a += Biases.W[d];
                        Result.Set(ax, ay, d, a);
                    }
                }
            });
            return Result;
        }

    }

}