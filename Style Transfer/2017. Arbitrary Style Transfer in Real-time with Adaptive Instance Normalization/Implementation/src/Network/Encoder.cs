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
        public delegate void StepDone(byte percent);

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
            var Temp = Layers.Conv2D(input, this.Data.Conv1_1_Weights, this.Data.Conv1_1_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv1_2_Weights, this.Data.Conv1_2_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.MaxPool2D(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv2_1_Weights, this.Data.Conv2_1_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv2_2_Weights, this.Data.Conv2_2_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.MaxPool2D(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv3_1_Weights, this.Data.Conv3_1_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv3_2_Weights, this.Data.Conv3_2_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv3_3_Weights, this.Data.Conv3_3_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv3_4_Weights, this.Data.Conv3_4_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.MaxPool2D(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Conv2D(Temp, this.Data.Conv4_1_Weights, this.Data.Conv4_1_Biases);
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            return Temp;
        }

    }

}