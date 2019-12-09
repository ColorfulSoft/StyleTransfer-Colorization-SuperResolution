//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя ReLU.

using System;
using System.Threading.Tasks;

namespace NeuralEnhance
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         public static Tensor Elementwise(Tensor input1, Tensor input2)
         {
             var n = new Tensor(input1.Width, input1.Height, input1.Depth);
             Parallel.For(0, input1.Depth, (int d) =>
             {
                 for(int y = 0; y < input1.Height; y++)
                 {
                     for(int x = 0; x < input1.Width; x++)
                     {
                         n.Set(x, y, d, input1.Get(x, y, d) + input2.Get(x, y, d));
                     }
                 }
             });
             return n;
         }

     }

}