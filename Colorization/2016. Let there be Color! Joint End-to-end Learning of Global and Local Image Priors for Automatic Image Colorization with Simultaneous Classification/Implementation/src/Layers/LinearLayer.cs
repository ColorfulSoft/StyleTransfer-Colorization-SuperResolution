//*************************************************************************************************
//* (C) ColorfulSoft, 2020. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя LinearLayer.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Слой поэлементного умножения.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor LinearLayer(Tensor input, Tensor[] weights, Tensor biases)
         {
             var Height = input.Height;
             var Width = input.Width;
             var Result = new Tensor(1, 1, weights.Length);
             Parallel.For(0, weights.Length, (int d) =>
             {
                 var f = weights[d];
                 var a = 0f;
                 var i = 0;
                 for(int id = 0; id < input.Depth; id++)
                 {
                     for(int y = 0; y < Height; y++)
                     {
                         for(int x = 0; x < Width; x++)
                         {
                             a += input.Get(x, y, id) * f.W[i];
                             i += 1;
                         }
                     }
                 }
                 Result.W[d] = a + biases.W[d];
             });
             return Result;
         }

     }

}