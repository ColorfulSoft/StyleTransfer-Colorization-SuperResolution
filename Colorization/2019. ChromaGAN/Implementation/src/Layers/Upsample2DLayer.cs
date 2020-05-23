//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя Upsample2DLayer.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         // В архитектуре нейросети все слои апсемплинга имеют коэффициент увеличения, равный 2. Для
         // упрощения и оптимизации, метод реализует ресайз с фиксированым коэффициентом 2.

         ///<summary>Слой апсемплинга. Используется метод ближайшего соседа.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor Upsample2D(Tensor input)
         {
             var Result = new Tensor(input.Width * 2, input.Height * 2, input.Depth);
             for (int d = 0; d < input.Depth; d++)
             {
                 for (var ax = 0; ax < input.Width; ax++)
                 {
                     var x = 2 * ax;
                     for (var ay = 0; ay < input.Height; ay++)
                     {
                         var y = 2 * ay; 
                         float a = input.Get(ax, ay, d);
                         for (byte fx = 0; fx < 2; fx++)
                         {
                             for (byte fy = 0; fy < 2; fy++)
                             {
                                 var oy = y + fy;
                                 var ox = x + fx;
                                 if (oy >= 0 && oy < Result.Height && ox >= 0 && ox < Result.Width)
                                 {
                                    Result.Set(ox, oy, d, a);
                                 }
                             }
                         }
                     }
                 }
             }
             return Result;
         }

    }

}