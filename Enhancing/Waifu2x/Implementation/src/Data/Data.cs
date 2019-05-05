//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.IO;

namespace NeuralEnhance
{

    public sealed class Data
    {

        private static Tensor[] LoadWeights(BinaryReader br, int w, int h, int d, int n)
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

        private static Tensor LoadBiases(BinaryReader br, int n)
        {
            var Result = new Tensor(1, 1, n);
            for(int i = 0; i < n; i++)
            {
                Result.Set(0, 0, i, br.ReadSingle());
            }
            return Result;
        }

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

        public Tensor[] Conv2_Weights
        {
            get;
            set;
        }

        public Tensor Conv2_Biases
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

        public Tensor[] Conv6_Weights
        {
            get;
            set;
        }

        public Tensor Conv6_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv7_Weights
        {
            get;
            set;
        }

        public Tensor Conv7_Biases
        {
            get;
            set;
        }

        public Data(Stream s)
        {
            var br = new BinaryReader(s);
            this.Conv1_Weights = LoadWeights(br, 3, 3, 3, 32);
            this.Conv1_Biases = LoadBiases(br, 32);
            this.Conv2_Weights = LoadWeights(br, 3, 3, 32, 32);
            this.Conv2_Biases = LoadBiases(br, 32);
            this.Conv3_Weights = LoadWeights(br, 3, 3, 32, 64);
            this.Conv3_Biases = LoadBiases(br, 64);
            this.Conv4_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Conv4_Biases = LoadBiases(br, 64);
            this.Conv5_Weights = LoadWeights(br, 3, 3, 64, 128);
            this.Conv5_Biases = LoadBiases(br, 128);
            this.Conv6_Weights = LoadWeights(br, 3, 3, 128, 128);
            this.Conv6_Biases = LoadBiases(br, 128);
            this.Conv7_Weights = LoadWeights(br, 3, 3, 128, 3);
            this.Conv7_Biases = LoadBiases(br, 3);
            br.Close();
        }

    }

}