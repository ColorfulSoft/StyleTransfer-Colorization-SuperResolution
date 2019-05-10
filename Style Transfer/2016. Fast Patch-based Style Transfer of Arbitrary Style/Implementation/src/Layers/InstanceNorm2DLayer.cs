//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя InstanceNormLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        ///<summary>Реализует слой нормализации карт признаков.</summary>
        ///<param name="Input">Карты признаков контентного изображения.</param>
        ///<param name="Shift">Смещения.</param>
        ///<param name="Scale">Множители.</param>
        public static Tensor InstanceNorm2D(Tensor Input, Tensor Shift, Tensor Scale)
        {
            var Mean = new float[Input.Depth];
            var Variance = new float[Input.Depth];
            for(int d = 0; d < Input.Depth; d++)
            {
                double Temp = 0.0;
                for(int y = 0; y < Input.Height; y++)
                {
                    for(int x = 0; x < Input.Width; x++)
                    {
                        Temp += Input.Get(x, y, d);
                    }
                }
                Temp /= Input.Height * Input.Width;
                Mean[d] = (float)Temp;
                double Temp2 = 0.0;
                for(int y = 0; y < Input.Height; y++)
                {
                    for(int x = 0; x < Input.Width; x++)
                    {
                        var _x = Input.Get(x, y, d) - Temp;
                        Temp2 += _x * _x;
                    }
                }
                Temp2 /= Input.Height * Input.Width;
                Variance[d] = (float)Temp2;
            }
            var Normalized = new Tensor(Input.Width, Input.Height, Input.Depth);
            Parallel.For(0, Input.Depth, (int d) =>
            {
                for(int y = 0; y < Input.Height; y++)
                {
                    for(int x = 0; x < Input.Width; x++)
                    {
                        Normalized.Set(x, y, d, (float)(((Input.Get(x, y, d) - Mean[d]) / Math.Sqrt(0.001f + Variance[d])) * Scale.W[d] + Shift.W[d]));
                    }
                }
            });
            return Normalized;
        }

    }

}