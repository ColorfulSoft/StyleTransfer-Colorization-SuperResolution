//*************************************************************************************************
//* (C) ColorfulSoft, 2020. Все права защищены.
//*************************************************************************************************

using System;
using System.IO;

namespace NeuralColor
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
                            T.Set(x, y, z, HalfHelper.HalfToSingle(br.ReadUInt16()));
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
                Result.Set(0, 0, i, HalfHelper.HalfToSingle(br.ReadUInt16()));
            }
            return Result;
        }

        public Tensor[] LLFN_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor LLFN_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor[] LLFN_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor LLFN_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor[] LLFN_Conv3_Weights
        {
            get;
            set;
        }

        public Tensor LLFN_Conv3_Biases
        {
            get;
            set;
        }

        public Tensor[] LLFN_Conv4_Weights
        {
            get;
            set;
        }

        public Tensor LLFN_Conv4_Biases
        {
            get;
            set;
        }

        public Tensor[] LLFN_Conv5_Weights
        {
            get;
            set;
        }

        public Tensor LLFN_Conv5_Biases
        {
            get;
            set;
        }

        public Tensor[] LLFN_Conv6_Weights
        {
            get;
            set;
        }

        public Tensor LLFN_Conv6_Biases
        {
            get;
            set;
        }

        public Tensor[] MLFN_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor MLFN_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor[] MLFN_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor MLFN_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor[] GFN_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor GFN_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor[] GFN_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor GFN_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor[] GFN_Conv3_Weights
        {
            get;
            set;
        }

        public Tensor GFN_Conv3_Biases
        {
            get;
            set;
        }

        public Tensor[] GFN_Conv4_Weights
        {
            get;
            set;
        }

        public Tensor GFN_Conv4_Biases
        {
            get;
            set;
        }

        public Tensor[] GFN_Linear1_Weights
        {
            get;
            set;
        }

        public Tensor GFN_Linear1_Biases
        {
            get;
            set;
        }

        public Tensor[] GFN_Linear2_Weights
        {
            get;
            set;
        }

        public Tensor GFN_Linear2_Biases
        {
            get;
            set;
        }

        public Tensor[] GFN_Linear3_Weights
        {
            get;
            set;
        }

        public Tensor GFN_Linear3_Biases
        {
            get;
            set;
        }

        public Tensor[] CN_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor CN_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor[] CN_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor CN_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor[] CN_Conv3_Weights
        {
            get;
            set;
        }

        public Tensor CN_Conv3_Biases
        {
            get;
            set;
        }

        public Tensor[] CN_Conv4_Weights
        {
            get;
            set;
        }

        public Tensor CN_Conv4_Biases
        {
            get;
            set;
        }

        public Tensor[] CN_Conv5_Weights
        {
            get;
            set;
        }

        public Tensor CN_Conv5_Biases
        {
            get;
            set;
        }

        public Tensor[] CN_Conv6_Weights
        {
            get;
            set;
        }

        public Tensor CN_Conv6_Biases
        {
            get;
            set;
        }

        public Data(Stream s)
        {
            var br = new BinaryReader(s);
            //
            this.LLFN_Conv1_Weights = LoadWeights(br, 3, 3, 1, 64);
            this.LLFN_Conv1_Biases = LoadBiases(br, 64);
            //
            this.LLFN_Conv2_Weights = LoadWeights(br, 3, 3, 64, 128);
            this.LLFN_Conv2_Biases = LoadBiases(br, 128);
            //
            this.LLFN_Conv3_Weights = LoadWeights(br, 3, 3, 128, 128);
            this.LLFN_Conv3_Biases = LoadBiases(br, 128);
            //
            this.LLFN_Conv4_Weights = LoadWeights(br, 3, 3, 128, 256);
            this.LLFN_Conv4_Biases = LoadBiases(br, 256);
            //
            this.LLFN_Conv5_Weights = LoadWeights(br, 3, 3, 256, 256);
            this.LLFN_Conv5_Biases = LoadBiases(br, 256);
            //
            this.LLFN_Conv6_Weights = LoadWeights(br, 3, 3, 256, 512);
            this.LLFN_Conv6_Biases = LoadBiases(br, 512);
            //
            this.MLFN_Conv1_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.MLFN_Conv1_Biases = LoadBiases(br, 512);
            //
            this.MLFN_Conv2_Weights = LoadWeights(br, 3, 3, 512, 256);
            this.MLFN_Conv2_Biases = LoadBiases(br, 256);
            //
            this.GFN_Conv1_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.GFN_Conv1_Biases = LoadBiases(br, 512);
            //
            this.GFN_Conv2_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.GFN_Conv2_Biases = LoadBiases(br, 512);
            //
            this.GFN_Conv3_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.GFN_Conv3_Biases = LoadBiases(br, 512);
            //
            this.GFN_Conv4_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.GFN_Conv4_Biases = LoadBiases(br, 512);
            //
            this.GFN_Linear1_Weights = LoadWeights(br, 1, 1, 25088, 1024);
            this.GFN_Linear1_Biases = LoadBiases(br, 1024);
            //
            this.GFN_Linear2_Weights = LoadWeights(br, 1, 1, 1024, 512);
            this.GFN_Linear2_Biases = LoadBiases(br, 512);
            //
            this.GFN_Linear3_Weights = LoadWeights(br, 1, 1, 512, 256);
            this.GFN_Linear3_Biases = LoadBiases(br, 256);
            //
            this.CN_Conv1_Weights = LoadWeights(br, 3, 3, 512, 256);
            this.CN_Conv1_Biases = LoadBiases(br, 256);
            //
            this.CN_Conv2_Weights = LoadWeights(br, 3, 3, 256, 128);
            this.CN_Conv2_Biases = LoadBiases(br, 128);
            //
            this.CN_Conv3_Weights = LoadWeights(br, 3, 3, 128, 64);
            this.CN_Conv3_Biases = LoadBiases(br, 64);
            //
            this.CN_Conv4_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.CN_Conv4_Biases = LoadBiases(br, 64);
            //
            this.CN_Conv5_Weights = LoadWeights(br, 3, 3, 64, 32);
            this.CN_Conv5_Biases = LoadBiases(br, 32);
            //
            this.CN_Conv6_Weights = LoadWeights(br, 3, 3, 32, 2);
            this.CN_Conv6_Biases = LoadBiases(br, 2);
            //
            br.Close();
        }

    }

}