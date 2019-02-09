//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя MaxPool2DLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        // В архитектуре нейросети все пулинги имеют размер фильтра 2x2. Для упрощения кода и оптимизации,
        // эти значения были интегрированы в код.

        ///<summary>Реализует слой максимального пулинга.</summary>
        ///<param name="input">Входные данные.</param>
        public static Tensor MaxPool2D(Tensor input)
        {
            var OutputWidth = input.Width / 2;
            var OutputHeight = input.Height / 2;
            var A = new Tensor(OutputWidth, OutputHeight, input.Depth);
            Parallel.For(0, input.Depth, (int d) =>
            {
                for(int ax = 0; ax < OutputWidth; ax++)
                {
                    var x = 0;
                    x += 2 * ax;
                    for (int ay = 0; ay < OutputHeight; ay++)
                    {
                        var y = 0; 
                        y += 2 * ay; 
                        float a = float.MinValue;
                        for(int fx = 0; fx < 2; fx++)
                        {
                            for(int fy = 0; fy < 2; fy++)
                            {
                                var oy = y + fy;
                                var ox = x + fx;
                                if(oy >= 0 && oy < input.Height && ox >= 0 && ox < input.Width)
                                {
                                    var v = input.Get(ox, oy, d);
                                    if (v > a)
                                    {
                                        a = v;
                                    }
                                }
                            }
                        }
                        var n = ((OutputWidth * ay) + ax) * input.Depth + d;
                        A.Set(ax, ay, d, a);
                    }
                }
            });
            return A;
        }

    }

}