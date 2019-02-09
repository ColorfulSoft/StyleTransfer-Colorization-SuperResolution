//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя Conv2DLayer.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

     // В архитектуре сети все свёртки имеют фильтры размером 3x3. Для упрощения кода и оптимизации,
     // эти значения были интегрированы в код.

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Реализует слой двумерной свёртки.</summary>
         ///<param name="input">Входной тензор.</param>
         ///<param name="Filters">Фильтры.</param>
         ///<param name="Biases">Смещения.</param>
         public static Tensor Conv2D(Tensor input, Tensor[] Filters, Tensor Biases)
         {
             var OutputDepth = Filters.Length;
             var Result = new Tensor(input.Width, input.Height, OutputDepth);
             Parallel.For(0, OutputDepth, (int d) =>
             {
                 for (int ay = 0; ay < input.Height; ay++)
                 {
                     var y = ay - 1;
                     var f = Filters[d];
                     for (var ax = 0; ax < input.Width; ax++)
                     {
                         var x = ax - 1;
                         var a = 0.0;
                         for (byte fy = 0; fy < 3; fy++)
                         {
                             var oy = y + fy;
                             for (var fx = 0; fx < 3; fx++)
                             {
                                 var ox = x + fx;
                                 if ((oy >= 0) && (oy < input.Height) && (ox >= 0) && (ox < input.Width))
                                 {
                                     for (var fd = 0; fd < f.Depth; fd++)
                                     {
                                         a += f.W[((3 * fy) + fx) * f.Depth + fd] * input.W[((input.Width * oy) + ox) * input.Depth + fd];
                                     }
                                 }
                             }
                         }
                         a += Biases.W[d];
                         Result.Set(ax, ay, d, (float)a);
                     }
                 }
             });
             return Result;
         }

     }

}