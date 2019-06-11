//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя Softmax.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        ///<summary>Слой softmax.</summary>
        ///<param name="input">Входные данные.</param>
        public static Tensor Softmax(Tensor input)
        {
            var Result = new Tensor(input.Width, input.Height, input.Depth);
            Parallel.For(0, input.Depth, (int d) =>
            {
                for(int y = 0; y < input.Height; y++)
                {
                    float amax = float.MinValue;
                    for(int x = 0; x < input.Width; x++)
                    {
                        var v = input.Get(x, y, d);
                        if(amax < v)
                        {
                            amax = v;
                        }
                    }
                    double sum = 0.0;
                    for(int x = 0; x < input.Width; x++)
                    {
                        var v = input.Get(x, y, d);
                        sum += System.Math.Exp((double)v - amax);
                    }
                    for(int x = 0; x < input.Width; x++)
                    {
                        var v = input.Get(x, y, d);
                        Result.Set(x, y, d, (float)(System.Math.Exp((double)v - amax) / sum));
                    }
                }
            });
            return Result;
        }

    }

}
