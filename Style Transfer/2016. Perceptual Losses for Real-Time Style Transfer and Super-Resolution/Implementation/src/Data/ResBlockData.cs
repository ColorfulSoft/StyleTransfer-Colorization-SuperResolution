//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Структура данных остаточного блока.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Предоставляет данные остаточного блока.</summary>
    public sealed class ResBlockData
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

        ///<summary>Инициализирует экземпляр класса ResBlockData, считывая данные из потока.</summary>
        ///<param name="s">Поток, из которого будет производится чтение.</param>
        public ResBlockData(BinaryReader br)
        {
            this.Conv1_Weights = LoadWeights(128, 128, 3, 3, br);
            this.Conv1_Shift = LoadBiases(128, br);
            this.Conv1_Scale = LoadBiases(128, br);
            this.Conv2_Weights = LoadWeights(128, 128, 3, 3, br);
            this.Conv2_Shift = LoadBiases(128, br);
            this.Conv2_Scale = LoadBiases(128, br);
        }

    }

}