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
        public Tensor Decode(Tensor Content, Tensor Style)
        {
            var Temp = Layers.StyleSwap(Content, Style);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv3_1_Weights, this.Data.TConv3_1_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv3_1_Shift, this.Data.TConv3_1_Scale);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv2_4_Weights, this.Data.TConv2_4_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv2_4_Shift, this.Data.TConv2_4_Scale);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv2_3_Weights, this.Data.TConv2_3_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv2_3_Shift, this.Data.TConv2_3_Scale);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv2_2_Weights, this.Data.TConv2_2_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv2_2_Shift, this.Data.TConv2_2_Scale);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv2_1_Weights, this.Data.TConv2_1_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv2_1_Shift, this.Data.TConv2_1_Scale);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv1_4_Weights, this.Data.TConv1_4_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv1_4_Shift, this.Data.TConv1_4_Scale);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.Upsample2D(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv1_3_Weights, this.Data.TConv1_3_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv1_3_Shift, this.Data.TConv1_3_Scale);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv1_2_Weights, this.Data.TConv1_2_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv1_2_Shift, this.Data.TConv1_2_Scale);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ReLU(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv1_1_Weights, this.Data.TConv1_1_Biases, 1);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            Temp = Layers.Sigmoid(Temp);
            if(this.Step != null)
            {
                this.Step(1f / 29f * 100f);
            }
            return Temp;
        }

    }

}
