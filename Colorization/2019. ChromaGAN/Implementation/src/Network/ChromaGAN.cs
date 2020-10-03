//*************************************************************************************************
//* (C) ColorfulSoft corp., 2020. All Rights reserved.
//*************************************************************************************************

//-> ChromaGAN implementation.

using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralColor
{

    ///<summary>Implements the ChromaGAN neural network.</summary>
    public sealed class ChromaGAN
    {

        public delegate void StepDone(float percent);

        public event StepDone Step;

        public Data Data;

        public ChromaGAN(Stream s)
        {
            this.Data = new Data(s);
        }

        public Tensor Colorize(Tensor Input)
        {
            // Encoding
            var Temp = Input;
            //**********
            //* Conv1
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv1_Weights, this.Data.VGG_Conv1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv2
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv2_Weights, this.Data.VGG_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            // Pool1
            //**********
            Temp = Layers.MaxPool2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv3
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv3_Weights, this.Data.VGG_Conv3_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv4
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv4_Weights, this.Data.VGG_Conv4_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            // Pool2
            //**********
            Temp = Layers.MaxPool2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv5
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv5_Weights, this.Data.VGG_Conv5_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv6
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv6_Weights, this.Data.VGG_Conv6_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv7
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv7_Weights, this.Data.VGG_Conv7_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            // Pool3
            //**********
            Temp = Layers.MaxPool2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv8
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv8_Weights, this.Data.VGG_Conv8_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv9
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv9_Weights, this.Data.VGG_Conv9_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv10
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.VGG_Conv10_Weights, this.Data.VGG_Conv10_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            var Temp2 = Temp;
            // global_features
            //**********
            //* Conv1
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.global_Conv1_Weights, this.Data.global_Conv1_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.global_BN1_Mean, this.Data.global_BN1_Variance, this.Data.global_BN1_Beta, this.Data.global_BN1_Gamma);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv2
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.global_Conv2_Weights, this.Data.global_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.global_BN2_Mean, this.Data.global_BN2_Variance, this.Data.global_BN2_Beta, this.Data.global_BN2_Gamma);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv3
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.global_Conv3_Weights, this.Data.global_Conv3_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.global_BN3_Mean, this.Data.global_BN3_Variance, this.Data.global_BN3_Beta, this.Data.global_BN3_Gamma);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv4
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.global_Conv4_Weights, this.Data.global_Conv4_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.global_BN4_Mean, this.Data.global_BN4_Variance, this.Data.global_BN4_Beta, this.Data.global_BN4_Gamma);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            // global_features2
            //**********
            //* Dense1
            //**********
            Temp = Layers.Dense(Temp, this.Data.global2_Dense1_Weights, this.Data.global2_Dense1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Dense2
            //**********
            Temp = Layers.Dense(Temp, this.Data.global2_Dense2_Weights, this.Data.global2_Dense2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Dense3
            //**********
            Temp = Layers.Dense(Temp, this.Data.global2_Dense3_Weights, this.Data.global2_Dense3_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            // midlevel_features
            //**********
            //* Conv1
            //**********
            Temp2 = Layers.Conv2D(Temp2, this.Data.midlevel_Conv1_Weights, this.Data.midlevel_Conv1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp2 = Layers.BatchNorm(Temp2, this.Data.midlevel_BN1_Mean, this.Data.midlevel_BN1_Variance, this.Data.midlevel_BN1_Beta, this.Data.midlevel_BN1_Gamma);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            //**********
            //* Conv2
            //**********
            Temp2 = Layers.Conv2D(Temp2, this.Data.midlevel_Conv2_Weights, this.Data.midlevel_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp2 = Layers.BatchNorm(Temp2, this.Data.midlevel_BN2_Mean, this.Data.midlevel_BN2_Variance, this.Data.midlevel_BN2_Beta, this.Data.midlevel_BN2_Gamma);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Fusion(Temp2, Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            // Colorization
            //**********
            //* Conv1
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.output_Conv1_Weights, this.Data.output_Conv1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.output_Conv2_Weights, this.Data.output_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.output_Conv3_Weights, this.Data.output_Conv3_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.output_Conv4_Weights, this.Data.output_Conv4_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.output_Conv5_Weights, this.Data.output_Conv5_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.output_Conv6_Weights, this.Data.output_Conv6_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Sigmoid(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 60f);
            }
            return Temp;
        }

    }

}