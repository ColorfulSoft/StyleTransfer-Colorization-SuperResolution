//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя SubpixelConv2DLayer.

using System;
using System.IO;
using System.Threading.Tasks;

namespace NeuralEnhance
{

    public sealed class SRGAN_Data
    {

        public Tensor[] Conv1_Weights
        {
            get;
            set;
        }

        public Tensor Conv1_Biases
        {
            get;
            set;
        }

        public ResidualBlockData Residual1
        {
            get;
            set;
        }

        public ResidualBlockData Residual2
        {
            get;
            set;
        }

        public ResidualBlockData Residual3
        {
            get;
            set;
        }

        public ResidualBlockData Residual4
        {
            get;
            set;
        }

        public ResidualBlockData Residual5
        {
            get;
            set;
        }

        public ResidualBlockData Residual6
        {
            get;
            set;
        }

        public ResidualBlockData Residual7
        {
            get;
            set;
        }

        public ResidualBlockData Residual8
        {
            get;
            set;
        }

        public ResidualBlockData Residual9
        {
            get;
            set;
        }

        public ResidualBlockData Residual10
        {
            get;
            set;
        }

        public ResidualBlockData Residual11
        {
            get;
            set;
        }

        public ResidualBlockData Residual12
        {
            get;
            set;
        }

        public ResidualBlockData Residual13
        {
            get;
            set;
        }

        public ResidualBlockData Residual14
        {
            get;
            set;
        }

        public ResidualBlockData Residual15
        {
            get;
            set;
        }

        public ResidualBlockData Residual16
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

        public Tensor[] Conv3_Weights
        {
            get;
            set;
        }

        public Tensor Conv3_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv4_Weights
        {
            get;
            set;
        }

        public Tensor Conv4_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv5_Weights
        {
            get;
            set;
        }

        public Tensor Conv5_Biases
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

        public SRGAN_Data(Stream s)
        {
            var br = new BinaryReader(s);
            this.Conv1_Weights = LoadWeights(64, 3, 3, 3, br);
            this.Conv1_Biases = LoadLine(64, br);
            this.Residual1 = new ResidualBlockData(br);
            this.Residual2 = new ResidualBlockData(br);
            this.Residual3 = new ResidualBlockData(br);
            this.Residual4 = new ResidualBlockData(br);
            this.Residual5 = new ResidualBlockData(br);
            this.Residual6 = new ResidualBlockData(br);
            this.Residual7 = new ResidualBlockData(br);
            this.Residual8 = new ResidualBlockData(br);
            this.Residual9 = new ResidualBlockData(br);
            this.Residual10 = new ResidualBlockData(br);
            this.Residual11 = new ResidualBlockData(br);
            this.Residual12 = new ResidualBlockData(br);
            this.Residual13 = new ResidualBlockData(br);
            this.Residual14 = new ResidualBlockData(br);
            this.Residual15 = new ResidualBlockData(br);
            this.Residual16 = new ResidualBlockData(br);
            this.Conv2_Weights = LoadWeights(64, 64, 3, 3, br);
            this.Conv2_Shift = LoadLine(64, br);
            this.Conv2_Scale = LoadLine(64, br);
            this.Conv2_Mean = LoadLine(64, br);
            this.Conv2_Variance = LoadLine(64, br);
            this.Conv3_Weights = LoadWeights(256, 64, 3, 3, br);
            this.Conv3_Biases = LoadLine(256, br);
            this.Conv4_Weights = LoadWeights(256, 64, 3, 3, br);
            this.Conv4_Biases = LoadLine(256, br);
            this.Conv5_Weights = LoadWeights(3, 64, 1, 1, br);
            this.Conv5_Biases = LoadLine(3, br);
            br.Close();
        }

    }

}