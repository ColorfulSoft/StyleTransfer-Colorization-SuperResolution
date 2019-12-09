//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя SubpixelConv2DLayer.

using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralEnhance
{

    public sealed class ResidualBlockData
    {

        public Tensor[] Conv1_Weights
        {

            get;

            set;

        }

        public Tensor Conv1_Mean
        {

            get;

            set;

        }

        public Tensor Conv1_Variance
        {

            get;

            set;

        }

        public Tensor Conv1_Shift
        {

            get;

            set;

        }

        public Tensor Conv1_Scale
        {

            get;

            set;

        }

        public Tensor[] Conv2_Weights
        {

            get;

            set;

        }

        public Tensor Conv2_Mean
        {

            get;

            set;

        }

        public Tensor Conv2_Variance
        {

            get;

            set;

        }

        public Tensor Conv2_Shift
        {

            get;

            set;

        }

        public Tensor Conv2_Scale
        {

            get;

            set;

        }

        ///<summary>Читает веса из потока посредством BinaryReader.</summary>
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

        private static Tensor LoadLine(int n, BinaryReader br)
        {
            var Result = new Tensor(1, 1, n);
            for(int i = 0; i < n; i++)
            {
                Result.Set(0, 0, i, HalfHelper.HalfToSingle(br.ReadUInt16()));
            }
            return Result;
        }

        public ResidualBlockData(BinaryReader br)
        {
            this.Conv1_Weights = LoadWeights(64, 64, 3, 3, br);
            this.Conv1_Shift = LoadLine(64, br);
            this.Conv1_Scale = LoadLine(64, br);
            this.Conv1_Mean = LoadLine(64, br);
            this.Conv1_Variance = LoadLine(64, br);
            this.Conv2_Weights = LoadWeights(64, 64, 3, 3, br);
            this.Conv2_Shift = LoadLine(64, br);
            this.Conv2_Scale = LoadLine(64, br);
            this.Conv2_Mean = LoadLine(64, br);
            this.Conv2_Variance = LoadLine(64, br);
        }

    }

}