//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Реализация декодера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Реализует декодирующую сеть.</summary>
    public sealed class Decoder
    {

        ///<summary>Данные нейросети.</summary>
        private DecoderData Data
        {
            get;
            set;
        }

        ///<summary>Делегат для события выполнения шага через слой нейросети.</summary>
        ///<param name="percent">Процент выполнения прямого прохода через декодирующую нейросеть.</param>
        public delegate void StepDone(byte percent);

        ///<summary>Инициализирует декодирующую нейросеть, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет произведено чтение параметров.</param>
        public Decoder(Stream s)
        {
            this.Data = new DecoderData(s);
        }

        ///<summary>Событие совершения прямого прохода через слой нейросети. Передаёт процент выполнения прохода через всю нейросеть.</summary>
        public event StepDone Step;

        ///<summary>Выполняет прямой проход через декодирующую нейросеть.</summary>
        ///<param name="input">Входные данные.</param>
        public Tensor Decode(Tensor input)
        {
            var Temp = Layers.Conv2D(input, this.Data.Conv4_1_Weights, this.Data.Conv4_1_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.Upsample2D(Temp);
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
            Temp = Layers.Upsample2D(Temp);
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
            Temp = Layers.Upsample2D(Temp);
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
            Temp = Layers.Conv2D(Temp, this.Data.Conv1_1_Weights, this.Data.Conv1_1_Biases);
            if(Step != null)
            {
                Step(5);
            }
            Temp = Layers.ReLU(Temp);
            return Temp;
        }

    }

}