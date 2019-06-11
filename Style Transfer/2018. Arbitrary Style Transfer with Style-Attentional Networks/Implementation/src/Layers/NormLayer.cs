//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя NormLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        ///<summary>Реализует слой нормализации каналов тензора.</summary>
        ///<param name="input">Входные данные.</param>
        ///<param name="epsilon">Системный параметр, защищающий от деления на 0. По умолчанию = 0.00001.</param>
        public static Tensor Norm(Tensor input, float epsilon = 1e-5f)
        {
            var Mean = new float[input.Depth];
            var Variance = new float[input.Depth];
            for(int d = 0; d < input.Depth; d++)
            {
                double Temp = 0.0;
                for(int y = 0; y < input.Height; y++)
                {
                    for(int x = 0; x < input.Width; x++)
                    {
                        Temp += input.Get(x, y, d);
                    }
                }
                Temp /= input.Height * input.Width;
                Mean[d] = (float)Temp;
                double Temp2 = 0.0;
                for(int y = 0; y < input.Height; y++)
                {
                    for(int x = 0; x < input.Width; x++)
                    {
                        var _x = input.Get(x, y, d) - Temp;
                        Temp2 += _x * _x;
                    }
                }
                Temp2 /= input.Height * input.Width;
                Variance[d] = (float)Temp2;
            }
            var Normalized = new Tensor(input.Width, input.Height, input.Depth);
            Parallel.For(0, input.Depth, (int d) =>
            {
                for(int y = 0; y < input.Height; y++)
                {
                    for(int x = 0; x < input.Width; x++)
                    {
                        Normalized.Set(x, y, d, (float)((input.Get(x, y, d) - Mean[d]) / Math.Sqrt(epsilon + Variance[d])));
                    }
                }
            });
            return Normalized;
        }

    }

}