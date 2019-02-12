//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя ResidualBlock.

using System;
using System.Threading.Tasks;

namespace NeuralEnhance
{

     ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
     public static partial class Layers
     {

         ///<summary>Слой гиперболического тангенса.</summary>
         ///<param name="input">Входные данные.</param>
         public static Tensor ResidualBlock(
                                            Tensor input,
                                            Tensor[] Filters1,
                                            Tensor[] Filters2,
                                            Tensor Biases1,
                                            Tensor Biases2,
                                            Tensor Shift1,
                                            Tensor Shift2,
                                            Tensor Scale1,
                                            Tensor Scale2,
                                            Tensor Mean1,
                                            Tensor Mean2,
                                            Tensor Var1,
                                            Tensor Var2)
         {
             var conv = Conv2D3x3(input, Filters1, Biases1);
             conv = BatchNorm(conv, Shift1, Scale1, Mean1, Var1);
             conv = ReLU(conv);
             conv = Conv2D3x3(conv, Filters2, Biases2);
             conv = BatchNorm(conv, Shift2, Scale2, Mean2, Var2);
             return ElementwiseSum(conv, input);
         }

     }

}