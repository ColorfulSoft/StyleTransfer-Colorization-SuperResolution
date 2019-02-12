//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя Conv2DTransposeLayer.

using System;
using System.Threading.Tasks;

namespace NeuralEnhance
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Слой обратной свёртки.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor Conv2DTranspose3x3(Tensor input, Tensor[] Weights, Tensor Biases)
         {
             var OutputWidth = input.Width * 2;
             var OutputHeight = input.Height * 2;
             var OutputDepth = Weights[0].Depth;
             var tempOutput = new Tensor(OutputWidth, OutputHeight, OutputDepth);
             for(int y = 0; y < input.Height; y++)
             {
                 for(int x = 0; x < input.Width; x++)
                 {
                     for(int d = 0; d < Weights.Length; d++)
                     {
                         var Filter = Weights[d];
                         Parallel.For(0, Filter.Depth, (int fd) =>
                         {
                             for(byte fy = 0; fy < 3; fy++)
                             {
                                 for(byte fx = 0; fx < 3; fx++)
                                 {
                                     var ox = x * 2 + fx;
                                     var oy = y * 2 + fy;
                                     if((ox < OutputWidth) && (oy < OutputHeight))
                                     {
                                         tempOutput.W[((OutputWidth * oy) + ox) * OutputDepth + fd] = input.W[((input.Width * y) + x) * input.Depth + d] * Filter.W[((3 * fy) + fx) * Filter.Depth + fd] + tempOutput.W[((OutputWidth * oy) + ox) * OutputDepth + fd];
                                     }
                                 }
                             }
                         });
                     }
                 }
             }
             Parallel.For(0, OutputDepth, (int d) =>
             {
                 for(int y = 0; y < OutputHeight; y++)
                 {
                     for(int x = 0; x < OutputWidth; x++)
                     {
                         tempOutput.Set(x, y, d, Biases.W[d] + tempOutput.Get(x, y, d));
                     }
                 }
             });
             return tempOutput;
         }

         ///<summary>Слой обратной свёртки.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor Conv2DTranspose9x9(Tensor input, Tensor[] Weights, Tensor Biases)
         {
             var OutputWidth = input.Width;
             var OutputHeight = input.Height;
             var OutputDepth = Weights[0].Depth;
             var tempOutput = new Tensor(OutputWidth, OutputHeight, OutputDepth);
             for(int y = 0; y < input.Height; y++)
             {
                 for(int x = 0; x < input.Width; x++)
                 {
                     for(int d = 0; d < Weights.Length; d++)
                     {
                         var Filter = Weights[d];
                         //Parallel.For(0, Filter.Depth, (int fd) =>
                         for(int fd = 0; fd < Filter.Depth; fd++)
                         {
                             for(byte fy = 0; fy < 9; fy++)
                             {
                                 for(byte fx = 0; fx < 9; fx++)
                                 {
                                     var ox = x + fx;
                                     var oy = y + fy;
                                     if((ox < OutputWidth) && (oy < OutputHeight))
                                     {
                                         tempOutput.W[((OutputWidth * oy) + ox) * OutputDepth + fd] = input.W[((input.Width * y) + x) * input.Depth + d] * Filter.W[((9 * fy) + fx) * Filter.Depth + fd] + tempOutput.W[((OutputWidth * oy) + ox) * OutputDepth + fd];
                                     }
                                 }
                             }
                         }//);
                     }
                 }
             }
             Parallel.For(0, OutputDepth, (int d) =>
             {
                 for(int y = 0; y < OutputHeight; y++)
                 {
                     for(int x = 0; x < OutputWidth; x++)
                     {
                         tempOutput.Set(x, y, d, Biases.W[d] + tempOutput.Get(x, y, d));
                     }
                 }
             });
             return tempOutput;
         }

    }

}