//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя ConvTranspose2DLayer.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        ///<summary>Реализует слой двумерной обратной свёртки.</summary>
        ///<param name="input">Входной тензор.</param>
        ///<param name="Filters">Фильтры.</param>
        ///<param name="Biases">Смещения.</param>
        public static Tensor ConvTranspose2D(Tensor input, Tensor[] Filters, Tensor Biases, int stride = 2)
        {
            var OutputWidth = input.Width * stride;
            var OutputHeight = input.Height * stride;
            var OutputDepth = Filters[0].Depth;
            var tempOutput = new Tensor(OutputWidth, OutputHeight, OutputDepth);
            System.Threading.Tasks.Parallel.For(0, input.Depth, (int d) =>
            {
                var f = Filters[d];
                var x = -f.Width / 2;
                var y = -f.Height / 2;
                for(var ay = 0; ay < input.Height; y += stride, ay++)
                {
                    x = -f.Width / 2;
                    for(var ax = 0; ax < input.Width; x += stride, ax++)
                    {
                        var chain_grad = input.Get(ax, ay, d);
                        for(int fy = 0; fy < f.Height; fy++)
                        {
                            var oy = y + fy;
                            for(int fx = 0; fx < f.Width; fx++)
                            {
                                var ox = x + fx;
                                if((oy >= 0) && (oy < OutputHeight) && (ox >= 0) && (ox < OutputWidth))
                                {
                                    for(int fd = 0; fd < f.Depth; fd++)
                                    {
                                        var ix1 = ((OutputWidth * oy) + ox) * OutputDepth + fd;
                                        var ix2 = ((f.Width * fy) + fx) * f.Depth + fd;
                                        tempOutput.W[ix1] += f.W[ix2] * chain_grad;
                                    }
                                }
                            }
                        }
                    }
                }
            });
            Parallel.For(0, tempOutput.Depth, (int d) =>
            {
                for(int y = 0; y < tempOutput.Height; y++)
                {
                    for(int x = 0; x < tempOutput.Width; x++)
                    {
                        tempOutput.Set(x, y, d, tempOutput.Get(x, y, d) + Biases.W[d]);
                    }
                }
            });
            return tempOutput;
        }

    }

}