//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя ElementwiseSumLayer.

using System;
using System.Threading.Tasks;

namespace NeuralEnhance
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Слой поэлементного сложения.</summary>
         ///<param name="A">Входные данные.</param>
         ///<param name="B">Входные данные.</param>
         public static Tensor ElementwiseSum(Tensor A, Tensor B)
         {
             var Height = A.Height;
             var Width = A.Width;
             var Result = new Tensor(A.Width, A.Height, A.Depth);
             Parallel.For(0, A.Depth, (int d) =>
             {
                 for(int y = 0; y < Height; y++)
                 {
                     for(int x = 0; x < Width; x++)
                     {
                         var v1 = A.Get(x, y, d);
                         var v2 = B.Get(x, y, d);
                         Result.Set(x, y, d, v1 + v2);
                     }
                 }
             });
             return Result;
         }

     }

}