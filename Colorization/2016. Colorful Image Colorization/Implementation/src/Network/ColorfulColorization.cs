//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для ColorfulColorization.

using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralColor
{

    ///<summary>Предоставляет реализацию окрашивающей нейросети.</summary>
    public sealed class ColorfulColorization
    {

        public delegate void StepDone(float percent);

        public event StepDone Step;

        public Data Data;

        public ColorfulColorization(Stream s)
        {
            this.Data = new Data(s);
        }

        public Tensor Colorize(Tensor Input)
        {
            var Temp = Input;
            //**********
            //* Conv1
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.BW_Conv1_1_Weights, this.Data.BW_Conv1_1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv1_2_Weights, this.Data.Conv1_2_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Conv1_2_Mean, this.Data.Conv1_2_Variance, this.Data.Conv1_2_Scale);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Conv2
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.Conv2_1_Weights, this.Data.Conv2_1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv2_2_Weights, this.Data.Conv2_2_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Conv2_2_Mean, this.Data.Conv2_2_Variance, this.Data.Conv2_2_Scale);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Conv3
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.Conv3_1_Weights, this.Data.Conv3_1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv3_2_Weights, this.Data.Conv3_2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv3_3_Weights, this.Data.Conv3_3_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Conv3_3_Mean, this.Data.Conv3_3_Variance, this.Data.Conv3_3_Scale);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Conv4
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.Conv4_1_Weights, this.Data.Conv4_1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv4_2_Weights, this.Data.Conv4_2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv4_3_Weights, this.Data.Conv4_3_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Conv4_3_Mean, this.Data.Conv4_3_Variance, this.Data.Conv4_3_Scale);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Conv5
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.Conv5_1_Weights, this.Data.Conv5_1_Biases, 1, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv5_2_Weights, this.Data.Conv5_2_Biases, 1, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv5_3_Weights, this.Data.Conv5_3_Biases, 1, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Conv5_3_Mean, this.Data.Conv5_3_Variance, this.Data.Conv5_3_Scale);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Conv6
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.Conv6_1_Weights, this.Data.Conv6_1_Biases, 1, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv6_2_Weights, this.Data.Conv6_2_Biases, 1, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv6_3_Weights, this.Data.Conv6_3_Biases, 1, 2);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Conv6_3_Mean, this.Data.Conv6_3_Variance, this.Data.Conv6_3_Scale);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Conv7
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.Conv7_1_Weights, this.Data.Conv7_1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv7_2_Weights, this.Data.Conv7_2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv7_3_Weights, this.Data.Conv7_3_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.BatchNorm(Temp, this.Data.Conv7_3_Mean, this.Data.Conv7_3_Variance, this.Data.Conv7_3_Scale);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Conv8
            //**********
            Temp = Layers.ConvTranspose2D(Temp, this.Data.Conv8_1_Weights, this.Data.Conv8_1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv8_2_Weights, this.Data.Conv8_2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv8_3_Weights, this.Data.Conv8_3_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Softmax
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.Conv8_313_Weights, this.Data.Conv8_313_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.ElementwiseMul(Temp, 6f);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            Temp = Layers.Softmax(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            //**********
            //* Decoding
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.Class8_ab_Weights, this.Data.Class8_ab_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 55f);
            }
            return Temp;
        }

    }

}