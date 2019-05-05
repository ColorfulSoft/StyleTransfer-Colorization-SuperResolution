//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для Waifu2x.

using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralEnhance
{

    ///<summary>Предоставляет нейросеть Waifu2x.</summary>
    public sealed class Waifu2x
    {

        public delegate void StepDone(byte percent);

        public event StepDone Step;

        private Data Data;

        public Waifu2x(Stream s)
        {
            this.Data = new Data(s);
        }

        public Tensor Enhance(Tensor input)
        {
            var Temp = input;
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_Weights, this.Data.Conv1_Biases);
            if(this.Step != null)
            {
                Step(7);
            }
            Temp = Layers.LeakyReLU(Temp);
            if(this.Step != null)
            {
                Step(14);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_Weights, this.Data.Conv2_Biases);
            if(this.Step != null)
            {
                Step(21);
            }
            Temp = Layers.LeakyReLU(Temp);
            if(this.Step != null)
            {
                Step(28);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_Weights, this.Data.Conv3_Biases);
            if(this.Step != null)
            {
                Step(35);
            }
            Temp = Layers.LeakyReLU(Temp);
            if(this.Step != null)
            {
                Step(42);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_Weights, this.Data.Conv4_Biases);
            if(this.Step != null)
            {
                Step(49);
            }
            Temp = Layers.LeakyReLU(Temp);
            if(this.Step != null)
            {
                Step(56);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv5_Weights, this.Data.Conv5_Biases);
            if(this.Step != null)
            {
                Step(63);
            }
            Temp = Layers.LeakyReLU(Temp);
            if(this.Step != null)
            {
                Step(70);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv6_Weights, this.Data.Conv6_Biases);
            if(this.Step != null)
            {
                Step(77);
            }
            Temp = Layers.LeakyReLU(Temp);
            if(this.Step != null)
            {
                Step(84);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv7_Weights, this.Data.Conv7_Biases);
            if(this.Step != null)
            {
                Step(91);
            }
            Temp = Layers.LeakyReLU(Temp);
            if(this.Step != null)
            {
                Step(98);
            }
            return Temp;
        }

    }

}