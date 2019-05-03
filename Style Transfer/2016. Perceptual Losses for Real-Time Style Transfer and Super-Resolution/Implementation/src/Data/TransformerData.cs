//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Структура данных сети-трансформера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Предоставляет данные сети-трансформера.</summary>
    public sealed class TransformerData
    {

        ///<summary>Веса свёрточного слоя Conv1.</summary>
        public Tensor[] Conv1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения нормализующего слоя InstanceNorm1.</summary>
        public Tensor Conv1_Shift
        {
            get;
            set;
        }

        ///<summary>Множители нормализующего слоя InstanceNorm1.</summary>
        public Tensor Conv1_Scale
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv2.</summary>
        public Tensor[] Conv2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения нормализующего слоя InstanceNorm2.</summary>
        public Tensor Conv2_Shift
        {
            get;
            set;
        }

        ///<summary>Множители нормализующего слоя InstanceNorm2.</summary>
        public Tensor Conv2_Scale
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя Conv3.</summary>
        public Tensor[] Conv3_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения нормализующего слоя InstanceNorm3.</summary>
        public Tensor Conv3_Shift
        {
            get;
            set;
        }

        ///<summary>Множители нормализующего слоя InstanceNorm3.</summary>
        public Tensor Conv3_Scale
        {
            get;
            set;
        }

        ///<summary>Данные первого остаточного блока.</summary>
        public ResBlockData ResBlock1
        {
            get;
            set;
        }

        ///<summary>Данные второго остаточного блока.</summary>
        public ResBlockData ResBlock2
        {
            get;
            set;
        }

        ///<summary>Данные третьего остаточного блока.</summary>
        public ResBlockData ResBlock3
        {
            get;
            set;
        }

        ///<summary>Данные четвёртого остаточного блока.</summary>
        public ResBlockData ResBlock4
        {
            get;
            set;
        }

        ///<summary>Данные пятого остаточного блока.</summary>
        public ResBlockData ResBlock5
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя TConv1.</summary>
        public Tensor[] TConv1_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения нормализующего слоя InstanceNorm4.</summary>
        public Tensor TConv1_Shift
        {
            get;
            set;
        }

        ///<summary>Множители нормализующего слоя InstanceNorm4.</summary>
        public Tensor TConv1_Scale
        {
            get;
            set;
        }

        ///<summary>Веса транспонированного свёрточного слоя TConv2.</summary>
        public Tensor[] TConv2_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения нормализующего слоя InstanceNorm5.</summary>
        public Tensor TConv2_Shift
        {
            get;
            set;
        }

        ///<summary>Множители нормализующего слоя InstanceNorm5.</summary>
        public Tensor TConv2_Scale
        {
            get;
            set;
        }

        ///<summary>Веса свёрточного слоя TConv3.</summary>
        public Tensor[] TConv3_Weights
        {
            get;
            set;
        }

        ///<summary>Смещения нормализующего слоя InstanceNorm6.</summary>
        public Tensor TConv3_Shift
        {
            get;
            set;
        }

        ///<summary>Множители нормализующего слоя InstanceNorm6.</summary>
        public Tensor TConv3_Scale
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
            for(int x = 0; x < w; x++)
            {
                for(int y = 0; y < h; y++)
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

        ///<summary>Инициализирует экземпляр класса TransformerData, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет производится чтение.</param>
        public TransformerData(Stream s)
        {
            var br = new BinaryReader(s);
            this.Conv1_Weights = LoadWeights(32, 3, 9, 9, br);
            this.Conv1_Shift = LoadBiases(32, br);
            this.Conv1_Scale = LoadBiases(32, br);
            this.Conv2_Weights = LoadWeights(64, 32, 3, 3, br);
            this.Conv2_Shift = LoadBiases(64, br);
            this.Conv2_Scale = LoadBiases(64, br);
            this.Conv3_Weights = LoadWeights(128, 64, 3, 3, br);
            this.Conv3_Shift = LoadBiases(128, br);
            this.Conv3_Scale = LoadBiases(128, br);
            this.ResBlock1 = new ResBlockData(br);
            this.ResBlock2 = new ResBlockData(br);
            this.ResBlock3 = new ResBlockData(br);
            this.ResBlock4 = new ResBlockData(br);
            this.ResBlock5 = new ResBlockData(br);
            this.TConv1_Weights = LoadWeights(128, 64, 3, 3, br);
            this.TConv1_Shift = LoadBiases(64, br);
            this.TConv1_Scale = LoadBiases(64, br);
            this.TConv2_Weights = LoadWeights(64, 32, 3, 3, br);
            this.TConv2_Shift = LoadBiases(32, br);
            this.TConv2_Scale = LoadBiases(32, br);
            this.TConv3_Weights = LoadWeights(3, 32, 9, 9, br);
            this.TConv3_Shift = LoadBiases(3, br);
            this.TConv3_Scale = LoadBiases(3, br);
            br.Close();
        }

    }

}