//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Реализация сети-трансформера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Реализует сеть-трансформер.</summary>
    public sealed class Transformer
    {

        ///<summary>Данные нейросети.</summary>
        private TransformerData Data
        {
            get;
            set;
        }

        ///<summary>Делегат для события выполнения шага через слой нейросети.</summary>
        ///<param name="percent">Процент выполнения прямого прохода через декодирующую нейросеть.</param>
        public delegate void StepDone(byte percent);

        ///<summary>Инициализирует сеть-трансформер, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет произведено чтение параметров.</param>
        public Transformer(Stream s)
        {
            this.Data = new TransformerData(s);
        }

        ///<summary>Событие совершения прямого прохода через слой нейросети. Передаёт процент выполнения прохода через всю нейросеть.</summary>
        public event StepDone Step;

        ///<summary>Выполняет прямой проход через сеть-трансформер.</summary>
        ///<param name="input">Входные данные.</param>
        public Tensor Stylize(Tensor input)
        {
            var Temp = Layers.Conv2D(input, this.Data.Conv1_Weights, 1);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.Conv1_Shift, this.Data.Conv1_Scale);
            Temp = Layers.ReLU(Temp);
            Temp = Layers.Conv2D(Temp, this.Data.Conv2_Weights, 2);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.Conv2_Shift, this.Data.Conv2_Scale);
            Temp = Layers.ReLU(Temp);
            Temp = Layers.Conv2D(Temp, this.Data.Conv3_Weights, 2);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.Conv3_Shift, this.Data.Conv3_Scale);
            Temp = Layers.ReLU(Temp);
            Temp = Layers.ResBlock(Temp, this.Data.ResBlock1);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.ResBlock(Temp, this.Data.ResBlock2);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.ResBlock(Temp, this.Data.ResBlock3);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.ResBlock(Temp, this.Data.ResBlock4);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.ResBlock(Temp, this.Data.ResBlock5);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv1_Weights, 2);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv1_Shift, this.Data.TConv1_Scale);
            Temp = Layers.ReLU(Temp);
            Temp = Layers.ConvTranspose2D(Temp, this.Data.TConv2_Weights, 2);
            if(Step != null)
            {
                Step(10);
            }
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv2_Shift, this.Data.TConv2_Scale);
            Temp = Layers.ReLU(Temp);
            Temp = Layers.Conv2D(Temp, this.Data.TConv3_Weights, 1);
            Temp = Layers.InstanceNorm2D(Temp, this.Data.TConv3_Shift, this.Data.TConv3_Scale);
            Temp = Layers.Tanh(Temp);
            return Temp;
        }

    }

}