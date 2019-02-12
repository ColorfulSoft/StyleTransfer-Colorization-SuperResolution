//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для TensorzoomNet.

using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralEnhance
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public sealed class TensorzoomNet
    {

        public delegate void StepDone(byte percent);

        public event StepDone Step;

        private Data Data;

        public TensorzoomNet(Stream s)
        {
            this.Data = new Data(s);
        }

        public Tensor Enhance(Tensor input)
        {
            var Temp = input;
            Temp = Layers.Conv2D9x9(Temp, this.Data.Conv1_Weights, this.Data.Conv1_Biases);
            if(this.Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                Step(10);
            }
            Temp = Layers.BatchNorm(
                                    Temp,
                                    this.Data.Conv1_Shift,
                                    this.Data.Conv1_Scale,
                                    this.Data.Conv1_Mean,
                                    this.Data.Conv1_Var);
            if(this.Step != null)
            {
                Step(15);
            }
            Temp = Layers.ResidualBlock(
                                        Temp,
                                        this.Data.Res1_Conv1_Weights,
                                        this.Data.Res1_Conv2_Weights,
                                        this.Data.Res1_Conv1_Biases,
                                        this.Data.Res1_Conv2_Biases,
                                        this.Data.Res1_Conv1_Shift,
                                        this.Data.Res1_Conv2_Shift,
                                        this.Data.Res1_Conv1_Scale,
                                        this.Data.Res1_Conv2_Scale,
                                        this.Data.Res1_Conv1_Mean,
                                        this.Data.Res1_Conv2_Mean,
                                        this.Data.Res1_Conv1_Var,
                                        this.Data.Res1_Conv2_Var);
            if(this.Step != null)
            {
                Step(20);
            }
            Temp = Layers.ResidualBlock(
                                        Temp,
                                        this.Data.Res2_Conv1_Weights,
                                        this.Data.Res2_Conv2_Weights,
                                        this.Data.Res2_Conv1_Biases,
                                        this.Data.Res2_Conv2_Biases,
                                        this.Data.Res2_Conv1_Shift,
                                        this.Data.Res2_Conv2_Shift,
                                        this.Data.Res2_Conv1_Scale,
                                        this.Data.Res2_Conv2_Scale,
                                        this.Data.Res2_Conv1_Mean,
                                        this.Data.Res2_Conv2_Mean,
                                        this.Data.Res2_Conv1_Var,
                                        this.Data.Res2_Conv2_Var);
            if(this.Step != null)
            {
                Step(25);
            }
            Temp = Layers.ResidualBlock(
                                        Temp,
                                        this.Data.Res3_Conv1_Weights,
                                        this.Data.Res3_Conv2_Weights,
                                        this.Data.Res3_Conv1_Biases,
                                        this.Data.Res3_Conv2_Biases,
                                        this.Data.Res3_Conv1_Shift,
                                        this.Data.Res3_Conv2_Shift,
                                        this.Data.Res3_Conv1_Scale,
                                        this.Data.Res3_Conv2_Scale,
                                        this.Data.Res3_Conv1_Mean,
                                        this.Data.Res3_Conv2_Mean,
                                        this.Data.Res3_Conv1_Var,
                                        this.Data.Res3_Conv2_Var);
            if(this.Step != null)
            {
                Step(30);
            }
            Temp = Layers.ResidualBlock(
                                        Temp,
                                        this.Data.Res4_Conv1_Weights,
                                        this.Data.Res4_Conv2_Weights,
                                        this.Data.Res4_Conv1_Biases,
                                        this.Data.Res4_Conv2_Biases,
                                        this.Data.Res4_Conv1_Shift,
                                        this.Data.Res4_Conv2_Shift,
                                        this.Data.Res4_Conv1_Scale,
                                        this.Data.Res4_Conv2_Scale,
                                        this.Data.Res4_Conv1_Mean,
                                        this.Data.Res4_Conv2_Mean,
                                        this.Data.Res4_Conv1_Var,
                                        this.Data.Res4_Conv2_Var);
            if(this.Step != null)
            {
                Step(35);
            }
            Temp = Layers.ResidualBlock(
                                        Temp,
                                        this.Data.Res5_Conv1_Weights,
                                        this.Data.Res5_Conv2_Weights,
                                        this.Data.Res5_Conv1_Biases,
                                        this.Data.Res5_Conv2_Biases,
                                        this.Data.Res5_Conv1_Shift,
                                        this.Data.Res5_Conv2_Shift,
                                        this.Data.Res5_Conv1_Scale,
                                        this.Data.Res5_Conv2_Scale,
                                        this.Data.Res5_Conv1_Mean,
                                        this.Data.Res5_Conv2_Mean,
                                        this.Data.Res5_Conv1_Var,
                                        this.Data.Res5_Conv2_Var);
            if(this.Step != null)
            {
                Step(40);
            }
            Temp = Layers.ResidualBlock(
                                        Temp,
                                        this.Data.Res6_Conv1_Weights,
                                        this.Data.Res6_Conv2_Weights,
                                        this.Data.Res6_Conv1_Biases,
                                        this.Data.Res6_Conv2_Biases,
                                        this.Data.Res6_Conv1_Shift,
                                        this.Data.Res6_Conv2_Shift,
                                        this.Data.Res6_Conv1_Scale,
                                        this.Data.Res6_Conv2_Scale,
                                        this.Data.Res6_Conv1_Mean,
                                        this.Data.Res6_Conv2_Mean,
                                        this.Data.Res6_Conv1_Var,
                                        this.Data.Res6_Conv2_Var);
            if(this.Step != null)
            {
                Step(45);
            }
            Temp = Layers.Conv2DTranspose3x3(Temp, this.Data.Deconv1_Weights, this.Data.Deconv1_Biases);
            if(this.Step != null)
            {
                Step(50);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                Step(55);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Deconv1_Shift, this.Data.Deconv1_Scale, this.Data.Deconv1_Mean, this.Data.Deconv1_Var);
            if(this.Step != null)
            {
                Step(60);
            }
            Temp = Layers.Conv2DTranspose3x3(Temp, this.Data.Deconv2_Weights, this.Data.Deconv2_Biases);
            if(this.Step != null)
            {
                Step(65);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                Step(70);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Deconv2_Shift, this.Data.Deconv2_Scale, this.Data.Deconv2_Mean, this.Data.Deconv2_Var);
            if(this.Step != null)
            {
                Step(75);
            }
            Temp = Layers.Conv2DTranspose9x9(Temp, this.Data.Deconv3_Weights, this.Data.Deconv3_Biases);
            if(this.Step != null)
            {
                Step(80);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                Step(85);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Deconv3_Shift, this.Data.Deconv3_Scale, this.Data.Deconv3_Mean, this.Data.Deconv3_Var);
            if(this.Step != null)
            {
                Step(90);
            }
            Temp = Layers.Tanh(Temp);
            if(this.Step != null)
            {
                Step(95);
            }
            return Temp;
        }

    }

}