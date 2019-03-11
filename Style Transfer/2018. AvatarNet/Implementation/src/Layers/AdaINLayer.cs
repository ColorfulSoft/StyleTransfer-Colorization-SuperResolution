//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя AdaINLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        ///<summary>Реализует слой адаптивной нормализации карт признаков.</summary>
        ///<param name="Content">Карты признаков контентного изображения.</param>
        ///<param name="Style">Карты признаков стилевого изображения.</param>
        ///<param name="epsilon">Системный параметр, защищающий от деления на 0. По умолчанию = 0.00001.</param>
        public static Tensor AdaIN(Tensor Content, Tensor Style, float epsilon = 1e-5f)
        {
            var ContentMean = new float[Content.Depth];
            var ContentVariance = new float[Content.Depth];
            for(int d = 0; d < Content.Depth; d++)
            {
                double Temp = 0.0;
                for(int y = 0; y < Content.Height; y++)
                {
                    for(int x = 0; x < Content.Width; x++)
                    {
                        Temp += Content.Get(x, y, d);
                    }
                }
                Temp /= Content.Height * Content.Width;
                ContentMean[d] = (float)Temp;
                double Temp2 = 0.0;
                for(int y = 0; y < Content.Height; y++)
                {
                    for(int x = 0; x < Content.Width; x++)
                    {
                        var _x = Content.Get(x, y, d) - Temp;
                        Temp2 += _x * _x;
                    }
                }
                Temp2 /= Content.Height * Content.Width;
                ContentVariance[d] = (float)Temp2;
            }
            var StyleMean = new float[Style.Depth];
            var StyleVariance = new float[Style.Depth];
            for(int d = 0; d < Style.Depth; d++)
            {
                double Temp = 0.0;
                for(int y = 0; y < Style.Height; y++)
                {
                    for(int x = 0; x < Style.Width; x++)
                    {
                        Temp += Style.Get(x, y, d);
                    }
                }
                Temp /= Style.Height * Style.Width;
                StyleMean[d] = (float)Temp;
                double Temp2 = 0.0;
                for(int y = 0; y < Style.Height; y++)
                {
                    for(int x = 0; x < Style.Width; x++)
                    {
                        var _x = Style.Get(x, y, d) - Temp;
                        Temp2 += _x * _x;
                    }
                }
                Temp2 /= Style.Height * Style.Width;
                StyleVariance[d] = (float)Temp2;
            }
            var Normalized = new Tensor(Content.Width, Content.Height, Content.Depth);
            Parallel.For(0, Content.Depth, (int d) =>
            {
                for(int y = 0; y < Content.Height; y++)
                {
                    for(int x = 0; x < Content.Width; x++)
                    {
                        Normalized.Set(x, y, d, (float)(((Content.Get(x, y, d) - ContentMean[d]) / Math.Sqrt(epsilon + ContentVariance[d])) * Math.Sqrt(StyleVariance[d]) + StyleMean[d]));
                    }
                }
            });
            return Normalized;
        }

    }

}