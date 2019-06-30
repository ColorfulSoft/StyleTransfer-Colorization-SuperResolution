//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Структура данных кодера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Тип кодирующей сети(по её глубине).</summary>
    public enum EncoderType
    {
        ///<summary>Для Conv1_1.</summary>
        Conv1,
        ///<summary>Для Conv2_1.</summary>
        Conv2,
        ///<summary>Для Conv3_1.</summary>
        Conv3,
        ///<summary>Для Conv4_1.</summary>
        Conv4,
        ///<summary>Для Conv5_1.</summary>
        Conv5
    }

    ///<summary>Предоставляет данные кодирующей нейросети.</summary>
    public sealed class EncoderData
    {

        ///<summary>Веса свёрточного слоя Conv0.</summary>
        public Tensor[] Conv0_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv0.</summary>
        public Tensor Conv0_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv1_1.</summary>
        public Tensor[] Conv1_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv1_1.</summary>
        public Tensor Conv1_1_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv1_2.</summary>
        public Tensor[] Conv1_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv1_2.</summary>
        public Tensor Conv1_2_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv2_1.</summary>
        public Tensor[] Conv2_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv2_1.</summary>
        public Tensor Conv2_1_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv2_2.</summary>
        public Tensor[] Conv2_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv2_2.</summary>
        public Tensor Conv2_2_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv3_1.</summary>
        public Tensor[] Conv3_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv3_1.</summary>
        public Tensor Conv3_1_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv3_2.</summary>
        public Tensor[] Conv3_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv3_2.</summary>
        public Tensor Conv3_2_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv3_3.</summary>
        public Tensor[] Conv3_3_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv3_3.</summary>
        public Tensor Conv3_3_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv3_4.</summary>
        public Tensor[] Conv3_4_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv3_4.</summary>
        public Tensor Conv3_4_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv4_1.</summary>
        public Tensor[] Conv4_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv4_1.</summary>
        public Tensor Conv4_1_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv4_2.</summary>
        public Tensor[] Conv4_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv4_2.</summary>
        public Tensor Conv4_2_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv4_3.</summary>
        public Tensor[] Conv4_3_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv4_3.</summary>
        public Tensor Conv4_3_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv4_4.</summary>
        public Tensor[] Conv4_4_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv4_4.</summary>
        public Tensor Conv4_4_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv5_1.</summary>
        public Tensor[] Conv5_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя Conv5_1.</summary>
        public Tensor Conv5_1_Biases
        {
            get;
            set;
        }

        ///<summary>Считывает веса из потока посредством BinaryReader.</summary>
        ///<param name="n">Количество фильтров.</param>
        ///<param name="d">Глубина фильтров.</param>
        ///<param name="h">Высота фильтров.</param>
        ///<param name="w">Ширина фильтров.</param>
        ///<param name="br">Оболочка потока(System.IO.BinaryReader), посредством которой будет производится чтение.</param>
        private static Tensor[] LoadWeights(int n, int d, int h, int w, BinaryReader br)
        {
            var Result = new Tensor[n];
            for(int i = 0; i < n; i++)
            {
                Result[i] = new Tensor(w, h, d);
            }
            for(int y = 0; y < h; y++)
            {
                for(int x = 0; x < w; x++)
                {
                    for(int z = 0; z < d; z++)
                    {
                        for(int i = 0; i < n; i++)
                        {
                            Result[i].Set(x, y, z, HalfHelper.HalfToSingle(br.ReadUInt16()));
                        }
                    }
                }
            }
            return Result;
        }

        ///<summary>Считывает смещения из потока посредством BinaryReader.</summary>
        ///<param name="n">Количество значений.</param>
        ///<param name="br">Оболочка потока(System.IO.BinaryReader), посредством которой будет производится чтение.</param>
        private static Tensor LoadBiases(int n, BinaryReader br)
        {
            var Result = new Tensor(1, 1, n);
            for(int i = 0; i < n; i++)
            {
                Result.Set(0, 0, i, HalfHelper.HalfToSingle(br.ReadUInt16()));
            }
            return Result;
        }

        ///<summary>Инициализирует экземпляр класса EncoderData, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет производится чтение.</param>
        ///<param name="depth">Глубина сети.</param>
        public EncoderData(Stream s, EncoderType depth)
        {
            var br = new BinaryReader(s);
            if((depth == EncoderType.Conv1) || (depth == EncoderType.Conv2) || (depth == EncoderType.Conv3) || (depth == EncoderType.Conv4) || (depth == EncoderType.Conv5))
            {
                this.Conv0_Weights = LoadWeights(3, 3, 1, 1, br);
                this.Conv0_Biases = LoadBiases(3, br);
                this.Conv1_1_Weights = LoadWeights(64, 3, 3, 3, br);
                this.Conv1_1_Biases = LoadBiases(64, br);
            }
            if((depth == EncoderType.Conv2) || (depth == EncoderType.Conv3) || (depth == EncoderType.Conv4) || (depth == EncoderType.Conv5))
            {
                this.Conv1_2_Weights = LoadWeights(64, 64, 3, 3, br);
                this.Conv1_2_Biases = LoadBiases(64, br);
                this.Conv2_1_Weights = LoadWeights(128, 64, 3, 3, br);
                this.Conv2_1_Biases = LoadBiases(128, br);
            }
            if((depth == EncoderType.Conv3) || (depth == EncoderType.Conv4) || (depth == EncoderType.Conv5))
            {
                this.Conv2_2_Weights = LoadWeights(128, 128, 3, 3, br);
                this.Conv2_2_Biases = LoadBiases(128, br);
                this.Conv3_1_Weights = LoadWeights(256, 128, 3, 3, br);
                this.Conv3_1_Biases = LoadBiases(256, br);
            }
            if((depth == EncoderType.Conv4) || (depth == EncoderType.Conv5))
            {
                this.Conv3_2_Weights = LoadWeights(256, 256, 3, 3, br);
                this.Conv3_2_Biases = LoadBiases(256, br);
                this.Conv3_3_Weights = LoadWeights(256, 256, 3, 3, br);
                this.Conv3_3_Biases = LoadBiases(256, br);
                this.Conv3_4_Weights = LoadWeights(256, 256, 3, 3, br);
                this.Conv3_4_Biases = LoadBiases(256, br);
                this.Conv4_1_Weights = LoadWeights(512, 256, 3, 3, br);
                this.Conv4_1_Biases = LoadBiases(512, br);
            }
            if(depth == EncoderType.Conv5)
            {
                this.Conv4_2_Weights = LoadWeights(512, 512, 3, 3, br);
                this.Conv4_2_Biases = LoadBiases(512, br);
                this.Conv4_3_Weights = LoadWeights(512, 512, 3, 3, br);
                this.Conv4_3_Biases = LoadBiases(512, br);
                this.Conv4_4_Weights = LoadWeights(512, 512, 3, 3, br);
                this.Conv4_4_Biases = LoadBiases(512, br);
                this.Conv5_1_Weights = LoadWeights(512, 512, 3, 3, br);
                this.Conv5_1_Biases = LoadBiases(512, br);
            }
            br.Close();
        }

    }

}