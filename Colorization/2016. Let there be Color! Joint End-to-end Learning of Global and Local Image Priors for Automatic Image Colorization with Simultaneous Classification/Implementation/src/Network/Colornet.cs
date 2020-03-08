//*************************************************************************************************
//* (C) ColorfulSoft, 2020. Все права защищены.
//*************************************************************************************************

//-> Определение для Colornet.

using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralColor
{

    ///<summary>Предоставляет реализацию окрашивающей нейросети.</summary>
    public sealed class Colornet
    {

        public delegate void StepDone(float percent);

        public event StepDone Step;

        public Data Data;

        public Colornet(Stream s)
        {
            this.Data = new Data(s);
        }

        public Tensor Colorize(Tensor Input1, Tensor Input2)
        {
            // First line
            var Temp = Input1;
            //**********
            //* Conv1
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.LLFN_Conv1_Weights, this.Data.LLFN_Conv1_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv2
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.LLFN_Conv2_Weights, this.Data.LLFN_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv3
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.LLFN_Conv3_Weights, this.Data.LLFN_Conv3_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv4
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.LLFN_Conv4_Weights, this.Data.LLFN_Conv4_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv5
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.LLFN_Conv5_Weights, this.Data.LLFN_Conv5_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv6
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.LLFN_Conv6_Weights, this.Data.LLFN_Conv6_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            // Second line
            var Temp2 = Input2;
            //**********
            //* Conv1
            //**********
            Temp2 = Layers.Conv2D(Temp2, this.Data.LLFN_Conv1_Weights, this.Data.LLFN_Conv1_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv2
            //**********
            Temp2 = Layers.Conv2D(Temp2, this.Data.LLFN_Conv2_Weights, this.Data.LLFN_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv3
            //**********
            Temp2 = Layers.Conv2D(Temp2, this.Data.LLFN_Conv3_Weights, this.Data.LLFN_Conv3_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv4
            //**********
            Temp2 = Layers.Conv2D(Temp2, this.Data.LLFN_Conv4_Weights, this.Data.LLFN_Conv4_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv5
            //**********
            Temp2 = Layers.Conv2D(Temp2, this.Data.LLFN_Conv5_Weights, this.Data.LLFN_Conv5_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            //**********
            //* Conv6
            //**********
            Temp2 = Layers.Conv2D(Temp2, this.Data.LLFN_Conv6_Weights, this.Data.LLFN_Conv6_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            // Mid feature level
            //**********
            //* Conv1
            //**********
            Temp = Layers.Conv2D(Temp, this.Data.MLFN_Conv1_Weights, this.Data.MLFN_Conv1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.MLFN_Conv2_Weights, this.Data.MLFN_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            // Global feature level
            Temp2 = Layers.Conv2D(Temp2, this.Data.GFN_Conv1_Weights, this.Data.GFN_Conv1_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.Conv2D(Temp2, this.Data.GFN_Conv2_Weights, this.Data.GFN_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.Conv2D(Temp2, this.Data.GFN_Conv3_Weights, this.Data.GFN_Conv3_Biases, 2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.Conv2D(Temp2, this.Data.GFN_Conv4_Weights, this.Data.GFN_Conv4_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.LinearLayer(Temp2, this.Data.GFN_Linear1_Weights, this.Data.GFN_Linear1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.LinearLayer(Temp2, this.Data.GFN_Linear2_Weights, this.Data.GFN_Linear2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.LinearLayer(Temp2, this.Data.GFN_Linear3_Weights, this.Data.GFN_Linear3_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp2 = Layers.ReLU(Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            // Fusion
            Temp = Layers.JoinLayer(Temp, Temp2);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            // Colorization
            Temp = Layers.Conv2D(Temp, this.Data.CN_Conv1_Weights, this.Data.CN_Conv1_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.CN_Conv2_Weights, this.Data.CN_Conv2_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.CN_Conv3_Weights, this.Data.CN_Conv3_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.CN_Conv4_Weights, this.Data.CN_Conv4_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.CN_Conv5_Weights, this.Data.CN_Conv5_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Conv2D(Temp, this.Data.CN_Conv6_Weights, this.Data.CN_Conv6_Biases);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Sigmoid(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(100f / 58f);
            }
            return Temp;
        }

    }

}