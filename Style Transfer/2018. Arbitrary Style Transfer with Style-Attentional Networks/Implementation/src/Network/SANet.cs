//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Реализация кодера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Предоставляет внимательную к стилю сеть.</summary>
    public sealed class SANet
    {

        ///<summary>Данные нейросети.</summary>
        private SANetData Data
        {
            get;
            set;
        }

        ///<summary>Делегат для события выполнения шага через слой нейросети.</summary>
        public delegate void StepDone(float percent);

        ///<summary>Инициализирует кодирующую нейросеть, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет произведено чтение параметров.</param>
        public SANet(Stream s)
        {
            this.Data = new SANetData(s);
        }

        ///<summary>Событие совершения прямого прохода через слой нейросети. Передаёт процент выполнения прохода через всю нейросеть.</summary>
        public event StepDone Step;

        ///<summary>Выполняет прямой проход через кодирующую нейросеть.</summary>
        ///<param name="Content4_1">Контент со слоя ReLU4_1.</param>
        ///<param name="Style4_1">Стиль со слоя ReLU4_1.</param>
        ///<param name="Content5_1">Контент со слоя ReLU5_1.</param>
        ///<param name="Style5_1">Стиль со слоя ReLU5_1.</param>
        public Tensor Stylize(Tensor Content4_1, Tensor Style4_1, Tensor Content5_1, Tensor Style5_1)
        {
            var Fc4_1 = Layers.Norm(Content4_1);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            var Fs4_1 = Layers.Norm(Style4_1);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            
            var Fc5_1 = Layers.Norm(Content5_1);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            var Fs5_1 = Layers.Norm(Style5_1);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            
            var f4_1 = Layers.Conv2D1x1(Fc4_1, this.Data.sanet4_1_f_Weights, this.Data.sanet4_1_f_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            var g4_1 = Layers.Conv2D1x1(Fs4_1, this.Data.sanet4_1_g_Weights, this.Data.sanet4_1_g_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            var h4_1 = Layers.Conv2D1x1(Style4_1, this.Data.sanet4_1_h_Weights, this.Data.sanet4_1_h_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            
            var f5_1 = Layers.Conv2D1x1(Fc5_1, this.Data.sanet5_1_f_Weights, this.Data.sanet5_1_f_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            var g5_1 = Layers.Conv2D1x1(Fs5_1, this.Data.sanet5_1_g_Weights, this.Data.sanet5_1_g_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            var h5_1 = Layers.Conv2D1x1(Style5_1, this.Data.sanet5_1_h_Weights, this.Data.sanet5_1_h_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            
            var S4_1 = Tensor.MatMul(f4_1.Flat().Transpose(), g4_1.Flat());
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            S4_1 = Layers.Softmax(S4_1);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            var O4_1 = Layers.ElementwiseSum(Layers.Conv2D1x1(Tensor.MatMul(h4_1.Flat(), S4_1.Transpose()).Unflat(Content4_1.Width, Content4_1.Height), this.Data.sanet4_1_out_conv_Weights, this.Data.sanet4_1_out_conv_Biases), Content4_1);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            
            var S5_1 = Tensor.MatMul(f5_1.Flat().Transpose(), g5_1.Flat());
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            S5_1 = Layers.Softmax(S5_1);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            var O5_1 = Layers.ElementwiseSum(Layers.Conv2D1x1(Tensor.MatMul(h5_1.Flat(), S5_1.Transpose()).Unflat(Content5_1.Width, Content5_1.Height), this.Data.sanet5_1_out_conv_Weights, this.Data.sanet5_1_out_conv_Biases), Content5_1);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            
            var CS = Layers.Conv2D3x3(Layers.ElementwiseSum(O4_1, Layers.Upsample2D(O5_1)), this.Data.merge_conv_Weights, this.Data.merge_conv_Biases);
            if(this.Step != null)
            {
                this.Step(1f / 17f * 100f);
            }
            return CS;
        }

    }

}