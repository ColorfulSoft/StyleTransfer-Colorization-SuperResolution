//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Структура данных SANet.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Предоставляет данные SANet.</summary>
    public sealed class SANetData
    {

        ///<summary>Веса свёрточного слоя sanet4_1_f.</summary>
        public Tensor[] sanet4_1_f_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя sanet4_1_f.</summary>
        public Tensor sanet4_1_f_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя sanet4_1_g.</summary>
        public Tensor[] sanet4_1_g_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя sanet4_1_g.</summary>
        public Tensor sanet4_1_g_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя sanet4_1_h.</summary>
        public Tensor[] sanet4_1_h_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя sanet4_1_h.</summary>
        public Tensor sanet4_1_h_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя sanet4_1_out_conv.</summary>
        public Tensor[] sanet4_1_out_conv_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя sanet4_1_out_conv.</summary>
        public Tensor sanet4_1_out_conv_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя sanet5_1_f.</summary>
        public Tensor[] sanet5_1_f_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя sanet5_1_f.</summary>
        public Tensor sanet5_1_f_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя sanet5_1_g.</summary>
        public Tensor[] sanet5_1_g_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя sanet5_1_g.</summary>
        public Tensor sanet5_1_g_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя sanet5_1_h.</summary>
        public Tensor[] sanet5_1_h_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя sanet5_1_h.</summary>
        public Tensor sanet5_1_h_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя sanet5_1_out_conv.</summary>
        public Tensor[] sanet5_1_out_conv_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя sanet5_1_out_conv.</summary>
        public Tensor sanet5_1_out_conv_Biases
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя merge_conv.</summary>
        public Tensor[] merge_conv_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения свёрточного слоя merge_conv.</summary>
        public Tensor merge_conv_Biases
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
                var T = Result[i];
                for(int z = 0; z < d; z++)
                {
                    for(int y = 0; y < h; y++)
                    {
                        for(int x = 0; x < w; x++)
                        {
                            T.Set(x, y, z, HalfHelper.HalfToSingle(br.ReadUInt16()));
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

        ///<summary>Инициализирует экземпляр класса SANetData, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет производится чтение.</param>
        public SANetData(Stream s)
        {
            var br = new BinaryReader(s);
            this.sanet4_1_f_Weights = LoadWeights(512, 512, 1, 1, br);
            this.sanet4_1_f_Biases = LoadBiases(512, br);
            this.sanet4_1_g_Weights = LoadWeights(512, 512, 1, 1, br);
            this.sanet4_1_g_Biases = LoadBiases(512, br);
            this.sanet4_1_h_Weights = LoadWeights(512, 512, 1, 1, br);
            this.sanet4_1_h_Biases = LoadBiases(512, br);
            this.sanet4_1_out_conv_Weights = LoadWeights(512, 512, 1, 1, br);
            this.sanet4_1_out_conv_Biases = LoadBiases(512, br);
            this.sanet5_1_f_Weights = LoadWeights(512, 512, 1, 1, br);
            this.sanet5_1_f_Biases = LoadBiases(512, br);
            this.sanet5_1_g_Weights = LoadWeights(512, 512, 1, 1, br);
            this.sanet5_1_g_Biases = LoadBiases(512, br);
            this.sanet5_1_h_Weights = LoadWeights(512, 512, 1, 1, br);
            this.sanet5_1_h_Biases = LoadBiases(512, br);
            this.sanet5_1_out_conv_Weights = LoadWeights(512, 512, 1, 1, br);
            this.sanet5_1_out_conv_Biases = LoadBiases(512, br);
            this.merge_conv_Weights = LoadWeights(512, 512, 3, 3, br);
            this.merge_conv_Biases = LoadBiases(512, br);
            br.Close();
        }

    }

}
