//*************************************************************************************************
//* (C) ColorfulSoft, 2020. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя JoinLayer.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Слой поэлементного умножения.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor JoinLayer(Tensor input, Tensor joint)
         {
             var Height = input.Height;
             var Width = input.Width;
             var Result = new Tensor(input.Width, input.Height, input.Depth + joint.Depth);
             Parallel.For(0, input.Depth, (int d) =>
             {
                 for(int y = 0; y < Height; y++)
                 {
                     for(int x = 0; x < Width; x++)
                     {
                         var v = input.Get(x, y, d);
                         Result.Set(x, y, d, v);
                     }
                 }
             });
             Parallel.For(0, joint.Depth, (int d) =>
             {
                 var v = joint.Get(0, 0, d);
                 for(int y = 0; y < Height; y++)
                 {
                     for(int x = 0; x < Width; x++)
                     {
                         Result.Set(x, y, d + input.Depth, v);
                     }
                 }
             });
             return Result;
         }

     }

}