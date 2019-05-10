//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Структура данных декодера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Предоставляет данные декодера.</summary>
    public sealed class DecoderData
    {

        ///<summary>Веса транспонированного свёрточного слоя 3_1.</summary>
        public Tensor[] TConv3_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 3_1.</summary>
        public Tensor TConv3_1_Biases
        {
            get;
            set;
        }

        ///<summary>Смещения номализующего слоя 3_1.</summary>
        public Tensor TConv3_1_Shift
        {
            get;
            set;
        }

        ///<summary>Множители номализующего слоя 3_1.</summary>
        public Tensor TConv3_1_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя 2_4.</summary>
        public Tensor[] TConv2_4_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 2_4.</summary>
        public Tensor TConv2_4_Biases
        {
            get;
            set;
        }

        ///<summary>Смещения номализующего слоя 2_4.</summary>
        public Tensor TConv2_4_Shift
        {
            get;
            set;
        }

        ///<summary>Множители номализующего слоя 2_4.</summary>
        public Tensor TConv2_4_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя 2_3.</summary>
        public Tensor[] TConv2_3_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 2_3.</summary>
        public Tensor TConv2_3_Biases
        {
            get;
            set;
        }

        ///<summary>Смещения номализующего слоя 2_3.</summary>
        public Tensor TConv2_3_Shift
        {
            get;
            set;
        }

        ///<summary>Множители номализующего слоя 2_3.</summary>
        public Tensor TConv2_3_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя 2_2.</summary>
        public Tensor[] TConv2_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 2_2.</summary>
        public Tensor TConv2_2_Biases
        {
            get;
            set;
        }

        ///<summary>Смещения номализующего слоя 2_2.</summary>
        public Tensor TConv2_2_Shift
        {
            get;
            set;
        }

        ///<summary>Множители номализующего слоя 2_2.</summary>
        public Tensor TConv2_2_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя 2_1.</summary>
        public Tensor[] TConv2_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 2_1.</summary>
        public Tensor TConv2_1_Biases
        {
            get;
            set;
        }

        ///<summary>Смещения номализующего слоя 2_1.</summary>
        public Tensor TConv2_1_Shift
        {
            get;
            set;
        }

        ///<summary>Множители номализующего слоя 2_1.</summary>
        public Tensor TConv2_1_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя 1_4.</summary>
        public Tensor[] TConv1_4_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 1_4.</summary>
        public Tensor TConv1_4_Biases
        {
            get;
            set;
        }

        ///<summary>Смещения номализующего слоя 1_4.</summary>
        public Tensor TConv1_4_Shift
        {
            get;
            set;
        }

        ///<summary>Множители номализующего слоя 1_4.</summary>
        public Tensor TConv1_4_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя 1_3.</summary>
        public Tensor[] TConv1_3_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 1_3.</summary>
        public Tensor TConv1_3_Biases
        {
            get;
            set;
        }

        ///<summary>Смещения номализующего слоя 1_3.</summary>
        public Tensor TConv1_3_Shift
        {
            get;
            set;
        }

        ///<summary>Множители номализующего слоя 1_3.</summary>
        public Tensor TConv1_3_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя 1_2.</summary>
        public Tensor[] TConv1_2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 1_2.</summary>
        public Tensor TConv1_2_Biases
        {
            get;
            set;
        }

        ///<summary>Смещения номализующего слоя 1_2.</summary>
        public Tensor TConv1_2_Shift
        {
            get;
            set;
        }

        ///<summary>Множители номализующего слоя 1_2.</summary>
        public Tensor TConv1_2_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя 1_1.</summary>
        public Tensor[] TConv1_1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения транспонированного свёрточного слоя 1_1.</summary>
        public Tensor TConv1_1_Biases
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
                            T.Set(x, y, z, br.ReadSingle());
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

        ///<summary>Инициализирует экземпляр класса DecoderData, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет производится чтение.</param>
        public DecoderData(Stream s)
        {
            var br = new BinaryReader(s);
            this.TConv3_1_Weights = LoadWeights(256, 128, 3, 3, br);
            this.TConv3_1_Biases = LoadBiases(128, br);
            this.TConv3_1_Scale = LoadBiases(128, br);
            this.TConv3_1_Shift = LoadBiases(128, br);
            this.TConv2_4_Weights = LoadWeights(128, 128, 3, 3, br);
            this.TConv2_4_Biases = LoadBiases(128, br);
            this.TConv2_4_Scale = LoadBiases(128, br);
            this.TConv2_4_Shift = LoadBiases(128, br);
            this.TConv2_3_Weights = LoadWeights(128, 128, 3, 3, br);
            this.TConv2_3_Biases = LoadBiases(128, br);
            this.TConv2_3_Scale = LoadBiases(128, br);
            this.TConv2_3_Shift = LoadBiases(128, br);
            this.TConv2_2_Weights = LoadWeights(128, 128, 3, 3, br);
            this.TConv2_2_Biases = LoadBiases(128, br);
            this.TConv2_2_Scale = LoadBiases(128, br);
            this.TConv2_2_Shift = LoadBiases(128, br);
            this.TConv2_1_Weights = LoadWeights(128, 64, 3, 3, br);
            this.TConv2_1_Biases = LoadBiases(64, br);
            this.TConv2_1_Scale = LoadBiases(64, br);
            this.TConv2_1_Shift = LoadBiases(64, br);
            this.TConv1_4_Weights = LoadWeights(64, 64, 3, 3, br);
            this.TConv1_4_Biases = LoadBiases(64, br);
            this.TConv1_4_Scale = LoadBiases(64, br);
            this.TConv1_4_Shift = LoadBiases(64, br);
            this.TConv1_3_Weights = LoadWeights(64, 64, 3, 3, br);
            this.TConv1_3_Biases = LoadBiases(64, br);
            this.TConv1_3_Scale = LoadBiases(64, br);
            this.TConv1_3_Shift = LoadBiases(64, br);
            this.TConv1_2_Weights = LoadWeights(64, 64, 3, 3, br);
            this.TConv1_2_Biases = LoadBiases(64, br);
            this.TConv1_2_Scale = LoadBiases(64, br);
            this.TConv1_2_Shift = LoadBiases(64, br);
            this.TConv1_1_Weights = LoadWeights(64, 3, 3, 3, br);
            this.TConv1_1_Biases = LoadBiases(3, br);
            br.Close();
        }

    }

}