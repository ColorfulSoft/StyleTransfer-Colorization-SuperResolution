//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя SubpixelConv2DLayer.

using System;
using System.Threading.Tasks;

namespace NeuralEnhance
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         public static Tensor SubpixelConv2D(Tensor input)
         {
             var Result = new Tensor(input.Width * 2, input.Height * 2, input.Depth / 4);
             for(int y = 0; y < Result.Height; y += 2)
             {
                 for(int x = 0; x < Result.Width; x += 2)
                 {
                     for(int d = 0; d < input.Depth / 4; d++)
                     {
                         Result.Set(x, y, d, input.Get(x / 2, y / 2, d));
                     }
                     for(int d = input.Depth / 4; d < input.Depth / 4 * 2; d++)
                     {
                         Result.Set(x + 1, y, d - Result.Depth, input.Get(x / 2, y / 2, d));
                     }
                     for(int d = input.Depth / 4 * 2; d < input.Depth / 4 * 3; d++)
                     {
                         Result.Set(x, y + 1, d - Result.Depth * 2, input.Get(x / 2, y / 2, d));
                     }
                     for(int d = input.Depth / 4 * 3; d < input.Depth; d++)
                     {
                         Result.Set(x + 1, y + 1, d - Result.Depth * 3, input.Get(x / 2, y / 2, d));
                     }
                 }
             }
             return Result;
         }

     }

}