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

        private DecoderType Depth
        {
            get;
            set;
        }

        ///<summary>Делегат для события выполнения шага через слой нейросети.</summary>
        ///<param name="percent">Процент выполнения прямого прохода через декодирующую нейросеть.</param>
        public delegate void StepDone(float percent);

        ///<summary>Инициализирует декодирующую нейросеть, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет произведено чтение параметров.</param>
        ///<param name="depth">Глубина сети.</param>
        public Decoder(Stream s, DecoderType depth)
        {
            this.Data = new DecoderData(s, depth);
            this.Depth = depth;
        }

        ///<summary>Событие совершения прямого прохода через слой нейросети. Передаёт процент выполнения прохода через всю нейросеть.</summary>
        public event StepDone Step;

        ///<summary>Выполняет прямой проход через декодирующую нейросеть.</summary>
        ///<param name="input">Входные данные.</param>
        public Tensor Decode(Tensor input)
        {
            var Temp = input;
            if(this.Depth == DecoderType.Conv5)
            {
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv5_1_Weights, this.Data.Conv5_1_Biases);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
                Temp = Layers.Upsample2D(Temp);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_4_Weights, this.Data.Conv4_4_Biases);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_3_Weights, this.Data.Conv4_3_Biases);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_2_Weights, this.Data.Conv4_2_Biases);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    this.Step(100f / 30f);
                }
            }
            if((this.Depth == DecoderType.Conv4) || (this.Depth == DecoderType.Conv5))
            {
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv4_1_Weights, this.Data.Conv4_1_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
                Temp = Layers.Upsample2D(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_4_Weights, this.Data.Conv3_4_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_3_Weights, this.Data.Conv3_3_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_2_Weights, this.Data.Conv3_2_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                    }
                }
            }
            if((this.Depth == DecoderType.Conv3) || (this.Depth == DecoderType.Conv4) || (this.Depth == DecoderType.Conv5))
            {
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv3_1_Weights, this.Data.Conv3_1_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                    }
                }
                Temp = Layers.Upsample2D(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                    }
                }
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_2_Weights, this.Data.Conv2_2_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                    }
                }
            }
            if((this.Depth == DecoderType.Conv2) || (this.Depth == DecoderType.Conv3) || (this.Depth == DecoderType.Conv4) || (this.Depth == DecoderType.Conv5))
            {
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv2_1_Weights, this.Data.Conv2_1_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                        case DecoderType.Conv2:
                        {
                            this.Step(100f / 7f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                        case DecoderType.Conv2:
                        {
                            this.Step(100f / 7f);
                            break;
                        }
                    }
                }
                Temp = Layers.Upsample2D(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                        case DecoderType.Conv2:
                        {
                            this.Step(100f / 7f);
                            break;
                        }
                    }
                }
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_2_Weights, this.Data.Conv1_2_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                        case DecoderType.Conv2:
                        {
                            this.Step(100f / 7f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                        case DecoderType.Conv2:
                        {
                            this.Step(100f / 7f);
                            break;
                        }
                    }
                }
            }
            if((this.Depth == DecoderType.Conv1) || (this.Depth == DecoderType.Conv2) || (this.Depth == DecoderType.Conv3) || (this.Depth == DecoderType.Conv4) || (this.Depth == DecoderType.Conv5))
            {
                Temp = Layers.Conv2D3x3(Temp, this.Data.Conv1_1_Weights, this.Data.Conv1_1_Biases);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                        case DecoderType.Conv2:
                        {
                            this.Step(100f / 7f);
                            break;
                        }
                        case DecoderType.Conv1:
                        {
                            this.Step(100f / 2f);
                            break;
                        }
                    }
                }
                Temp = Layers.ReLU(Temp);
                if(this.Step != null)
                {
                    switch(this.Depth)
                    {
                        case DecoderType.Conv5:
                        {
                            this.Step(100f / 30f);
                            break;
                        }
                        case DecoderType.Conv4:
                        {
                            this.Step(100f / 21f);
                            break;
                        }
                        case DecoderType.Conv3:
                        {
                            this.Step(100f / 12f);
                            break;
                        }
                        case DecoderType.Conv2:
                        {
                            this.Step(100f / 7f);
                            break;
                        }
                        case DecoderType.Conv1:
                        {
                            this.Step(100f / 2f);
                            break;
                        }
                    }
                }
            }
            return Temp;
        }

    }

}