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

         public static Tensor ResidualBlock(Tensor input, ResidualBlockData Data)
         {
             var n = Layers.Conv2D3x3(input, Data.Conv1_Weights, new Tensor(1, 1, 64));
             n = Layers.BatchNorm(n, Data.Conv1_Mean, Data.Conv1_Variance, Data.Conv1_Shift, Data.Conv1_Scale);
             n = Layers.ReLU(n);
             n = Layers.Conv2D3x3(n, Data.Conv2_Weights, new Tensor(1, 1, 64));
             n = Layers.BatchNorm(n, Data.Conv2_Mean, Data.Conv2_Variance, Data.Conv2_Shift, Data.Conv2_Scale);
             Parallel.For(0, 64, (int d) =>
             {
                 for(int y = 0; y < input.Height; y++)
                 {
                     for(int x = 0; x < input.Width; x++)
                     {
                         n.Set(x, y, d, n.Get(x, y, d) + input.Get(x, y, d));
                     }
                 }
             });
             return n;
         }

     }

}