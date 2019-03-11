//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Реализация кодера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Предоставляет кодирующую нейросеть.</summary>
    public sealed class Encoder
    {

        ///<summary>Данные нейросети.</summary>
        private EncoderData Data
        {
            get;
            set;
        }

        ///<summary>Делегат для события выполнения шага через слой нейросети.</summary>
        public delegate void StepDone(float percent);

        ///<summary>Инициализирует кодирующую нейросеть, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет произведено чтение параметров.</param>
        public Encoder(Stream s)
        {
            this.Data = new EncoderData(s);
        }

        ///<summary>Событие совершения прямого прохода через слой нейросети. Передаёт процент выполнения прохода через всю нейросеть.</summary>
        public event StepDone Step;

        ///<summary>Выполняет прямой проход через кодирующую нейросеть.</summary>
        ///<param name="input">Входные данные.</param>
        public Tensor Encode(Tensor input)
        {
            var Temp = input;
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_1_Weights, this.Data.Conv1_1_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_2_Weights, this.Data.Conv1_2_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.AvgPool2D(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_1_Weights, this.Data.Conv2_1_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_2_Weights, this.Data.Conv2_2_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.AvgPool2D(Temp);
            if(this.Step != null)
            {
                //this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_1_Weights, this.Data.Conv3_1_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_2_Weights, this.Data.Conv3_2_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_3_Weights, this.Data.Conv3_3_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_4_Weights, this.Data.Conv3_4_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.AvgPool2D(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_1_Weights, this.Data.Conv4_1_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            return Temp;
        }

        ///<summary>Выполняет прямой проход через кодирующую нейросеть.</summary>
        ///<param name="input">Входные данные.</param>
        public Tensor[] EncodeStyle(Tensor input)
        {
            var Temp = input;
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_1_Weights, this.Data.Conv1_1_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            var Conv1 = Temp;
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_2_Weights, this.Data.Conv1_2_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.AvgPool2D(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_1_Weights, this.Data.Conv2_1_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            var Conv2 = Temp;
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_2_Weights, this.Data.Conv2_2_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.AvgPool2D(Temp);
            if(this.Step != null)
            {
                //this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_1_Weights, this.Data.Conv3_1_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            var Conv3 = Temp;
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_2_Weights, this.Data.Conv3_2_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_3_Weights, this.Data.Conv3_3_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_4_Weights, this.Data.Conv3_4_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.AvgPool2D(Temp);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_1_Weights, this.Data.Conv4_1_Biases);
            if(this.Step != null)
            {
                this.Step(5f);
            }
            Temp = Layers.ReLU(Temp);
            var Conv4 = Temp;
            if(this.Step != null)
            {
                this.Step(5f);
            }
            return new Tensor[4]{Conv1, Conv2, Conv3, Conv4};
        }

    }

}