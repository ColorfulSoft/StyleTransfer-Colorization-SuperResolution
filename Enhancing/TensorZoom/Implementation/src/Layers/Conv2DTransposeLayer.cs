//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя Conv2DTransposeLayer.

using System;
using System.Threading.Tasks;

namespace NeuralEnhance
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        ///<summary>Слой обратной свёртки.</summary>
        ///<param name="input">Входные данные.</param>
        public static Tensor Conv2DTranspose3x3(Tensor input, Tensor[] Filters, Tensor Biases)
        {
            var OutputWidth = input.Width * 2;
            var OutputHeight = input.Height * 2;
            var OutputDepth = Filters[0].Depth;
            var tempOutput = new Tensor(OutputWidth, OutputHeight, OutputDepth);
            System.Threading.Tasks.Parallel.For(0, input.Depth, (int d) =>
            {
                var f = Filters[d];
                var x = -1;
                var y = -1;
                for(var ay = 0; ay < input.Height; y += 2, ay++)
                {
                    x = -1;
                    for(var ax = 0; ax < input.Width; x += 2, ax++)
                    {
                        var chain_grad = input.Get(ax, ay, d);
                        for(int fy = 0; fy < 3; fy++)
                        {
                            var oy = y + fy;
                            for(int fx = 0; fx < 3; fx++)
                            {
                                var ox = x + fx;
                                if((oy >= 0) && (oy < OutputHeight) && (ox >= 0) && (ox < OutputWidth))
                                {
                                    for(int fd = 0; fd < f.Depth; fd++)
                                    {
                                        var ix1 = ((OutputWidth * oy) + ox) * OutputDepth + fd;
                                        var ix2 = ((3 * fy) + fx) * f.Depth + fd;
                                        tempOutput.W[ix1] += f.W[ix2] * chain_grad;
                                    }
                                }
                            }
                        }
                    }
                }
            });
            Parallel.For(0, OutputDepth, (int d) =>
            {
                for(int y = 0; y < OutputHeight; y++)
                {
                    for(int x = 0; x < OutputWidth; x++)
                    {
                        tempOutput.Set(x, y, d, Biases.W[d] + tempOutput.Get(x, y, d));
                    }
                }
            });
            return tempOutput;
        }

        ///<summary>Слой обратной свёртки.</summary>
        ///<param name="input">Входные данные.</param>
        public static Tensor Conv2DTranspose9x9(Tensor input, Tensor[] Filters, Tensor Biases)
        {
            var OutputWidth = input.Width;
            var OutputHeight = input.Height;
            var OutputDepth = Filters[0].Depth;
            var tempOutput = new Tensor(OutputWidth, OutputHeight, OutputDepth);
            System.Threading.Tasks.Parallel.For(0, input.Depth, (int d) =>
            {
                var f = Filters[d];
                var x = -4;
                var y = -4;
                for(var ay = 0; ay < input.Height; y += 1, ay++)
                {
                    x = -4;
                    for(var ax = 0; ax < input.Width; x += 1, ax++)
                    {
                        var chain_grad = input.Get(ax, ay, d);
                        for(int fy = 0; fy < 9; fy++)
                        {
                            var oy = y + fy;
                            for(int fx = 0; fx < 9; fx++)
                            {
                                var ox = x + fx;
                                if((oy >= 0) && (oy < OutputHeight) && (ox >= 0) && (ox < OutputWidth))
                                {
                                    for(int fd = 0; fd < f.Depth; fd++)
                                    {
                                        var ix1 = ((OutputWidth * oy) + ox) * OutputDepth + fd;
                                        var ix2 = ((9 * fy) + fx) * f.Depth + fd;
                                        tempOutput.W[ix1] += f.W[ix2] * chain_grad;
                                    }
                                }
                            }
                        }
                    }
                }
            });
            Parallel.For(0, OutputDepth, (int d) =>
            {
                for(int y = 0; y < OutputHeight; y++)
                {
                    for(int x = 0; x < OutputWidth; x++)
                    {
                        tempOutput.Set(x, y, d, Biases.W[d] + tempOutput.Get(x, y, d));
                    }
                }
            });
            return tempOutput;
        }

    }

}