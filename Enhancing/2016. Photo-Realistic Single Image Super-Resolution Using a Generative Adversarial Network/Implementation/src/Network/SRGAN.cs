//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя BatchNormLayer.

using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralEnhance
{

    public sealed class SRGAN
    {

        private SRGAN_Data Data;

        public SRGAN(Stream s)
        {
            this.Data = new SRGAN_Data(s);
        }

        public delegate void StepDone(byte percent);

        public event StepDone Step;

        public Tensor Enhance(Tensor input)
        {
            var n = Layers.Conv2D3x3(input, Data.Conv1_Weights, Data.Conv1_Biases);
            n = Layers.ReLU(n);
            if(this.Step != null)
            {
                Step(2);
            }
            var temp = n;
            n = Layers.ResidualBlock(n, Data.Residual1);
            if(this.Step != null)
            {
                Step(7);
            }
            n = Layers.ResidualBlock(n, Data.Residual2);
            if(this.Step != null)
            {
                Step(12);
            }
            n = Layers.ResidualBlock(n, Data.Residual3);
            if(this.Step != null)
            {
                Step(17);
            }
            n = Layers.ResidualBlock(n, Data.Residual4);
            if(this.Step != null)
            {
                Step(22);
            }
            n = Layers.ResidualBlock(n, Data.Residual5);
            if(this.Step != null)
            {
                Step(27);
            }
            n = Layers.ResidualBlock(n, Data.Residual6);
            if(this.Step != null)
            {
                Step(32);
            }
            n = Layers.ResidualBlock(n, Data.Residual7);
            if(this.Step != null)
            {
                Step(37);
            }
            n = Layers.ResidualBlock(n, Data.Residual8);
            if(this.Step != null)
            {
                Step(42);
            }
            n = Layers.ResidualBlock(n, Data.Residual9);
            if(this.Step != null)
            {
                Step(47);
            }
            n = Layers.ResidualBlock(n, Data.Residual10);
            if(this.Step != null)
            {
                Step(52);
            }
            n = Layers.ResidualBlock(n, Data.Residual11);
            if(this.Step != null)
            {
                Step(57);
            }
            n = Layers.ResidualBlock(n, Data.Residual12);
            if(this.Step != null)
            {
                Step(62);
            }
            n = Layers.ResidualBlock(n, Data.Residual13);
            if(this.Step != null)
            {
                Step(67);
            }
            n = Layers.ResidualBlock(n, Data.Residual14);
            if(this.Step != null)
            {
                Step(72);
            }
            n = Layers.ResidualBlock(n, Data.Residual15);
            if(this.Step != null)
            {
                Step(77);
            }
            n = Layers.ResidualBlock(n, Data.Residual16);
            if(this.Step != null)
            {
                Step(82);
            }
            n = Layers.Conv2D3x3(n, Data.Conv2_Weights, new Tensor(1, 1, 64));
            n = Layers.BatchNorm(n, Data.Conv2_Mean, Data.Conv2_Variance, Data.Conv2_Shift, Data.Conv2_Scale);
            n = Layers.Elementwise(n, temp);
            if(this.Step != null)
            {
                Step(87);
            }
            n = Layers.Conv2D3x3(n, Data.Conv3_Weights, Data.Conv3_Biases);
            n = Layers.SubpixelConv2D(n);
            n = Layers.ReLU(n);
            if(this.Step != null)
            {
                Step(92);
            }
            n = Layers.Conv2D3x3(n, Data.Conv4_Weights, Data.Conv4_Biases);
            n = Layers.SubpixelConv2D(n);
            n = Layers.ReLU(n);
            if(this.Step != null)
            {
                Step(97);
            }
            n = Layers.Conv2D1x1(n, Data.Conv5_Weights, Data.Conv5_Biases);
            n = Layers.Tanh(n);
            if(this.Step != null)
            {
                Step(100);
            }
            return n;
        }

    }

}