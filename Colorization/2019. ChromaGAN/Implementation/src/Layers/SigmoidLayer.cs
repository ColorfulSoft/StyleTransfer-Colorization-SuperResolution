//*************************************************************************************************
//* (C) ColorfulSoft, 2020. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя SigmoidLayer.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Слой сигмоиды.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor Sigmoid(Tensor input)
         {
             var Height = input.Height;
             var Width = input.Width;
             var Result = new Tensor(input.Width, input.Height, input.Depth);
             Parallel.For(0, input.Depth, (int d) =>
             {
                 for(int y = 0; y < Height; y++)
                 {
                     for(int x = 0; x < Width; x++)
                     {
                         var v = input.Get(x, y, d);
                         Result.Set(x, y, d, 1f / (1f + (float)Math.Exp(-v)));
                     }
                 }
             });
             return Result;
         }

     }

}