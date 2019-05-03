//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя TanhLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Слой гиперболического тангенса.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor Tanh(Tensor input)
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
                         Result.Set(x, y, d, (float)Math.Tanh(v));
                     }
                 }
             });
             return Result;
         }

     }

}