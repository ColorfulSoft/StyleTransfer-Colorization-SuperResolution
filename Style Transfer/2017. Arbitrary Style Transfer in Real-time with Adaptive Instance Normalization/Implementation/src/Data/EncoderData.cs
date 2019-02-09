//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Структура данных кодера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Предоставляет данные кодирующей нейросети.</summary>
    public sealed class EncoderData
    {

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
                            Result[i].Set(x, y, z, br.ReadSingle());
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
                Result.Set(0, 0, i, br.ReadSingle());
            }
            return Result;
        }

        ///<summary>Инициализирует экземпляр класса EncoderData, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет производится чтение.</param>
        public EncoderData(Stream s)
        {
            var br = new BinaryReader(s);
            this.Conv1_1_Weights = LoadWeights(64, 3, 3, 3, br);
            this.Conv1_1_Biases = LoadBiases(64, br);
            this.Conv1_2_Weights = LoadWeights(64, 64, 3, 3, br);
            this.Conv1_2_Biases = LoadBiases(64, br);
            this.Conv2_1_Weights = LoadWeights(128, 64, 3, 3, br);
            this.Conv2_1_Biases = LoadBiases(128, br);
            this.Conv2_2_Weights = LoadWeights(128, 128, 3, 3, br);
            this.Conv2_2_Biases = LoadBiases(128, br);
            this.Conv3_1_Weights = LoadWeights(256, 128, 3, 3, br);
            this.Conv3_1_Biases = LoadBiases(256, br);
            this.Conv3_2_Weights = LoadWeights(256, 256, 3, 3, br);
            this.Conv3_2_Biases = LoadBiases(256, br);
            this.Conv3_3_Weights = LoadWeights(256, 256, 3, 3, br);
            this.Conv3_3_Biases = LoadBiases(256, br);
            this.Conv3_4_Weights = LoadWeights(256, 256, 3, 3, br);
            this.Conv3_4_Biases = LoadBiases(256, br);
            this.Conv4_1_Weights = LoadWeights(512, 256, 3, 3, br);
            this.Conv4_1_Biases = LoadBiases(512, br);
            br.Close();
        }

    }

}