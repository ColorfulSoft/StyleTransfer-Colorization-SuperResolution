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
        public Tuple<Tensor, Tensor> Encode(Tensor input)
        {
            var Temp = input;
            Temp = Layers.Conv2D1x1(input, this.Data.Conv0_Weights, this.Data.Conv0_Biases);
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_1_Weights, this.Data.Conv1_1_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_2_Weights, this.Data.Conv1_2_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.MaxPool2D(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_1_Weights, this.Data.Conv2_1_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_2_Weights, this.Data.Conv2_2_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.MaxPool2D(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_1_Weights, this.Data.Conv3_1_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_2_Weights, this.Data.Conv3_2_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_3_Weights, this.Data.Conv3_3_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_4_Weights, this.Data.Conv3_4_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.MaxPool2D(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_1_Weights, this.Data.Conv4_1_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            var Conv4_1 = Temp;
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_2_Weights, this.Data.Conv4_2_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_3_Weights, this.Data.Conv4_3_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_4_Weights, this.Data.Conv4_4_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.MaxPool2D(Temp);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv5_1_Weights, this.Data.Conv5_1_Biases);
            if(Step != null)
            {
                Step(100f / 30f);
            }
            Temp = Layers.ReLU(Temp);
            var Conv5_1 = Temp;
            if(Step != null)
            {
                Step(100f / 30f);
            }
            return new Tuple<Tensor, Tensor>(Conv4_1, Conv5_1);
        }

    }

}