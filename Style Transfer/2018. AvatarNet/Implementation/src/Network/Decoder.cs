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
        public delegate void StepDone(float percent);

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
        public Tensor Decode(Tensor Content, Tensor[] Styles)
        {
            var Temp = Layers.StyleDecorator(Content, Styles[3]);
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_1_Weights, this.Data.Conv4_1_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_4_Weights, this.Data.Conv3_4_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_3_Weights, this.Data.Conv3_3_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_2_Weights, this.Data.Conv3_2_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.AdaIN(Temp, Styles[2]);
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_1_Weights, this.Data.Conv3_1_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_2_Weights, this.Data.Conv2_2_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.AdaIN(Temp, Styles[1]);
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_1_Weights, this.Data.Conv2_1_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_2_Weights, this.Data.Conv1_2_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.AdaIN(Temp, Styles[0]);
            Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_1_Weights, this.Data.Conv1_1_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            Temp = Layers.Conv2D7x7(Temp, this.Data.Conv_Out_Weights, this.Data.Conv_Out_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 22f * 100f);
            }
            return Temp;
        }

    }

}