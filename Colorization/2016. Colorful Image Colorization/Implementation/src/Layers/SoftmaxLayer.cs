//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя SoftmaxLayer.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Слой softmax.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor Softmax(Tensor input)
         {
             var Height = input.Height;
             var Width = input.Width;
             var Result = new Tensor(input.Width, input.Height, input.Depth);
             Parallel.For(0, input.Height, (int y) =>
             {
                 for(int x = 0; x < Width; x++)
                 {
                     float amax = float.MinValue;
                     for(int d = 0; d < input.Depth; d++)
                     {
                         var v = input.Get(x, y, d);
                         if(amax < v)
                         {
                             amax = v;
                         }
                     }
                     double sum = 0.0;
                     for(int d = 0; d < input.Depth; d++)
                     {
                         var v = input.Get(x, y, d);
                         sum += System.Math.Exp((double)v - amax);
                     }
                     for(int d = 0; d < input.Depth; d++)
                     {
                         var v = input.Get(x, y, d);
                         Result.Set(x, y, d, (float)(System.Math.Exp((double)v - amax) / sum));
                     }
                 }
             });
             return Result;
         }

     }

}