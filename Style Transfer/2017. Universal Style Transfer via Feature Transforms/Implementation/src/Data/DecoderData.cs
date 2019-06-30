//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Структура данных декодера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Тип декодирующей сети(по её глубине).</summary>
    public enum DecoderType
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

    ///<summary>Предоставляет данные декодера.</summary>
    public sealed class DecoderData
    {

        ///<summary>Веса свёрточного слоя 5_1.</summary>
        public Tensor[] Conv5_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 5_1.</summary>
        public Tensor Conv5_1_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 4_4.</summary>
        public Tensor[] Conv4_4_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 4_4.</summary>
        public Tensor Conv4_4_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 4_3.</summary>
        public Tensor[] Conv4_3_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 4_3.</summary>
        public Tensor Conv4_3_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 4_2.</summary>
        public Tensor[] Conv4_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 4_2.</summary>
        public Tensor Conv4_2_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 4_1.</summary>
        public Tensor[] Conv4_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 4_1.</summary>
        public Tensor Conv4_1_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 3_4.</summary>
        public Tensor[] Conv3_4_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 3_4.</summary>
        public Tensor Conv3_4_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 3_3.</summary>
        public Tensor[] Conv3_3_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 3_3.</summary>
        public Tensor Conv3_3_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 3_2.</summary>
        public Tensor[] Conv3_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 3_2.</summary>
        public Tensor Conv3_2_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 3_1.</summary>
        public Tensor[] Conv3_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 3_1.</summary>
        public Tensor Conv3_1_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 2_2.</summary>
        public Tensor[] Conv2_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 2_2.</summary>
        public Tensor Conv2_2_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 2_1.</summary>
        public Tensor[] Conv2_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 2_1.</summary>
        public Tensor Conv2_1_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 1_2.</summary>
        public Tensor[] Conv1_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 1_2.</summary>
        public Tensor Conv1_2_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя 1_1.</summary>
        public Tensor[] Conv1_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя 1_1.</summary>
        public Tensor Conv1_1_Biases
        {
            get;
            set;
        }

        ///<summary>Читает веса из потока посредством BinaryReader.</summary>
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

        ///<summary>Инициализирует экземпляр класса DecoderData, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет производится чтение.</param>
        ///<param name="depth">Глубина сети.</param>
        public DecoderData(Stream s, DecoderType depth)
        {
            var br = new BinaryReader(s);
            if(depth == DecoderType.Conv5)
            {
                this.Conv5_1_Weights = LoadWeights(512, 512, 3, 3, br);
                this.Conv5_1_Biases = LoadBiases(512, br);
                this.Conv4_4_Weights = LoadWeights(512, 512, 3, 3, br);
                this.Conv4_4_Biases = LoadBiases(512, br);
                this.Conv4_3_Weights = LoadWeights(512, 512, 3, 3, br);
                this.Conv4_3_Biases = LoadBiases(512, br);
                this.Conv4_2_Weights = LoadWeights(512, 512, 3, 3, br);
                this.Conv4_2_Biases = LoadBiases(512, br);
            }
            if((depth == DecoderType.Conv4) || (depth == DecoderType.Conv5))
            {
                this.Conv4_1_Weights = LoadWeights(256, 512, 3, 3, br);
                this.Conv4_1_Biases = LoadBiases(256, br);
                this.Conv3_4_Weights = LoadWeights(256, 256, 3, 3, br);
                this.Conv3_4_Biases = LoadBiases(256, br);
                this.Conv3_3_Weights = LoadWeights(256, 256, 3, 3, br);
                this.Conv3_3_Biases = LoadBiases(256, br);
                this.Conv3_2_Weights = LoadWeights(256, 256, 3, 3, br);
                this.Conv3_2_Biases = LoadBiases(256, br);
            }
            if((depth == DecoderType.Conv3) || (depth == DecoderType.Conv4) || (depth == DecoderType.Conv5))
            {
                this.Conv3_1_Weights = LoadWeights(128, 256, 3, 3, br);
                this.Conv3_1_Biases = LoadBiases(128, br);
                this.Conv2_2_Weights = LoadWeights(128, 128, 3, 3, br);
                this.Conv2_2_Biases = LoadBiases(128, br);
            }
            if((depth == DecoderType.Conv2) || (depth == DecoderType.Conv3) || (depth == DecoderType.Conv4) || (depth == DecoderType.Conv5))
            {
                this.Conv2_1_Weights = LoadWeights(64, 128, 3, 3, br);
                this.Conv2_1_Biases = LoadBiases(64, br);
                this.Conv1_2_Weights = LoadWeights(64, 64, 3, 3, br);
                this.Conv1_2_Biases = LoadBiases(64, br);
            }
            if((depth == DecoderType.Conv1) || (depth == DecoderType.Conv2) || (depth == DecoderType.Conv3) || (depth == DecoderType.Conv4) || (depth == DecoderType.Conv5))
            {
                this.Conv1_1_Weights = LoadWeights(3, 64, 3, 3, br);
                this.Conv1_1_Biases = LoadBiases(3, br);
            }
            br.Close();
        }

    }

}