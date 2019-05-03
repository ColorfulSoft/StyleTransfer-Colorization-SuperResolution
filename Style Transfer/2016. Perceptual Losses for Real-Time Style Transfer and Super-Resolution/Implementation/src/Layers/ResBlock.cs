//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Определение для слоя ResBlock.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Остаточный блок.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor ResBlock(Tensor input, ResBlockData Data)
         {
             var y = Conv2D(input, Data.Conv1_Weights, 1);
             y = InstanceNorm2D(y, Data.Conv1_Shift, Data.Conv1_Scale);
             y = ReLU(y);
             y = Conv2D(y, Data.Conv2_Weights, 1);
             y = InstanceNorm2D(y, Data.Conv2_Shift, Data.Conv2_Scale);
             return ElementwiseSum(input, y);
         }

     }

}