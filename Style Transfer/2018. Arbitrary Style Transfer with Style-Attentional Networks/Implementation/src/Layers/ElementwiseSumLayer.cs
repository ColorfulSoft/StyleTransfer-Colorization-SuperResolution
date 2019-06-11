//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя ElementwiseSumLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        ///<summary>Слой поэлементной суммы.</summary>
        ///<param name="A">Входные данные.</param>
        ///<param name="B">Входные данные.</param>
        public static Tensor ElementwiseSum(Tensor A, Tensor B)
        {
            var Height = Math.Min(A.Height, B.Height);
            var Width = Math.Min(A.Width, B.Width);
            var Result = new Tensor(Width, Height, A.Depth);
            Parallel.For(0, A.Depth, (int d) =>
            {
                for(int y = 0; y < Height; y++)
                {
                    for(int x = 0; x < Width; x++)
                    {
                        var v = A.Get(x, y, d) + B.Get(x, y, d);
                        Result.Set(x, y, d, v);
                    }
                }
            });
            return Result;
        }

    }

}