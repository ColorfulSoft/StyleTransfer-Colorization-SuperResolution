//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019. All Rights reserved.
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

        public Tensor[] BW_Conv1_1_Weights
        {
            get;
            set;
        }

        public Tensor BW_Conv1_1_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv1_2_Weights
        {
            get;
            set;
        }

        public Tensor Conv1_2_Biases
        {
            get;
            set;
        }

        public Tensor Conv1_2_Mean
        {
            get;
            set;
        }

        public Tensor Conv1_2_Variance
        {
            get;
            set;
        }

        public float Conv1_2_Scale
        {
            get;
            set;
        }

        public Tensor[] Conv2_1_Weights
        {
            get;
            set;
        }

        public Tensor Conv2_1_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv2_2_Weights
        {
            get;
            set;
        }

        public Tensor Conv2_2_Biases
        {
            get;
            set;
        }

        public Tensor Conv2_2_Mean
        {
            get;
            set;
        }

        public Tensor Conv2_2_Variance
        {
            get;
            set;
        }

        public float Conv2_2_Scale
        {
            get;
            set;
        }

        public Tensor[] Conv3_1_Weights
        {
            get;
            set;
        }

        public Tensor Conv3_1_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv3_2_Weights
        {
            get;
            set;
        }

        public Tensor Conv3_2_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv3_3_Weights
        {
            get;
            set;
        }

        public Tensor Conv3_3_Biases
        {
            get;
            set;
        }

        public Tensor Conv3_3_Mean
        {
            get;
            set;
        }

        public Tensor Conv3_3_Variance
        {
            get;
            set;
        }

        public float Conv3_3_Scale
        {
            get;
            set;
        }

        public Tensor[] Conv4_1_Weights
        {
            get;
            set;
        }

        public Tensor Conv4_1_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv4_2_Weights
        {
            get;
            set;
        }

        public Tensor Conv4_2_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv4_3_Weights
        {
            get;
            set;
        }

        public Tensor Conv4_3_Biases
        {
            get;
            set;
        }

        public Tensor Conv4_3_Mean
        {
            get;
            set;
        }

        public Tensor Conv4_3_Variance
        {
            get;
            set;
        }

        public float Conv4_3_Scale
        {
            get;
            set;
        }

        public Tensor[] Conv5_1_Weights
        {
            get;
            set;
        }

        public Tensor Conv5_1_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv5_2_Weights
        {
            get;
            set;
        }

        public Tensor Conv5_2_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv5_3_Weights
        {
            get;
            set;
        }

        public Tensor Conv5_3_Biases
        {
            get;
            set;
        }

        public Tensor Conv5_3_Mean
        {
            get;
            set;
        }

        public Tensor Conv5_3_Variance
        {
            get;
            set;
        }

        public float Conv5_3_Scale
        {
            get;
            set;
        }

        public Tensor[] Conv6_1_Weights
        {
            get;
            set;
        }

        public Tensor Conv6_1_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv6_2_Weights
        {
            get;
            set;
        }

        public Tensor Conv6_2_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv6_3_Weights
        {
            get;
            set;
        }

        public Tensor Conv6_3_Biases
        {
            get;
            set;
        }

        public Tensor Conv6_3_Mean
        {
            get;
            set;
        }

        public Tensor Conv6_3_Variance
        {
            get;
            set;
        }

        public float Conv6_3_Scale
        {
            get;
            set;
        }

        public Tensor[] Conv7_1_Weights
        {
            get;
            set;
        }

        public Tensor Conv7_1_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv7_2_Weights
        {
            get;
            set;
        }

        public Tensor Conv7_2_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv7_3_Weights
        {
            get;
            set;
        }

        public Tensor Conv7_3_Biases
        {
            get;
            set;
        }

        public Tensor Conv7_3_Mean
        {
            get;
            set;
        }

        public Tensor Conv7_3_Variance
        {
            get;
            set;
        }

        public float Conv7_3_Scale
        {
            get;
            set;
        }

        public Tensor[] Conv8_1_Weights
        {
            get;
            set;
        }

        public Tensor Conv8_1_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv8_2_Weights
        {
            get;
            set;
        }

        public Tensor Conv8_2_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv8_3_Weights
        {
            get;
            set;
        }

        public Tensor Conv8_3_Biases
        {
            get;
            set;
        }

        public Tensor[] Conv8_313_Weights
        {
            get;
            set;
        }

        public Tensor Conv8_313_Biases
        {
            get;
            set;
        }

        public Tensor[] Class8_ab_Weights
        {
            get;
            set;
        }

        public Tensor Class8_ab_Biases
        {
            get;
            set;
        }

        public Data(Stream s)
        {
            var br = new BinaryReader(s);
            //
            this.BW_Conv1_1_Weights = LoadWeights(br, 3, 3, 1, 64);
            this.BW_Conv1_1_Biases = LoadBiases(br, 64);
            //
            this.Conv1_2_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Conv1_2_Biases = LoadBiases(br, 64);
            //
            this.Conv1_2_Mean = LoadBiases(br, 64);
            this.Conv1_2_Variance = LoadBiases(br, 64);
            this.Conv1_2_Scale = br.ReadSingle();
            //
            this.Conv2_1_Weights = LoadWeights(br, 3, 3, 64, 128);
            this.Conv2_1_Biases = LoadBiases(br, 128);
            //
            this.Conv2_2_Weights = LoadWeights(br, 3, 3, 128, 128);
            this.Conv2_2_Biases = LoadBiases(br, 128);
            //
            this.Conv2_2_Mean = LoadBiases(br, 128);
            this.Conv2_2_Variance = LoadBiases(br, 128);
            this.Conv2_2_Scale = br.ReadSingle();
            //
            this.Conv3_1_Weights = LoadWeights(br, 3, 3, 128, 256);
            this.Conv3_1_Biases = LoadBiases(br, 256);
            //
            this.Conv3_2_Weights = LoadWeights(br, 3, 3, 256, 256);
            this.Conv3_2_Biases = LoadBiases(br, 256);
            //
            this.Conv3_3_Weights = LoadWeights(br, 3, 3, 256, 256);
            this.Conv3_3_Biases = LoadBiases(br, 256);
            //
            this.Conv3_3_Mean = LoadBiases(br, 256);
            this.Conv3_3_Variance = LoadBiases(br, 256);
            this.Conv3_3_Scale = br.ReadSingle();
            //
            this.Conv4_1_Weights = LoadWeights(br, 3, 3, 256, 512);
            this.Conv4_1_Biases = LoadBiases(br, 512);
            //
            this.Conv4_2_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv4_2_Biases = LoadBiases(br, 512);
            //
            this.Conv4_3_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv4_3_Biases = LoadBiases(br, 512);
            //
            this.Conv4_3_Mean = LoadBiases(br, 512);
            this.Conv4_3_Variance = LoadBiases(br, 512);
            this.Conv4_3_Scale = br.ReadSingle();
            //
            this.Conv5_1_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv5_1_Biases = LoadBiases(br, 512);
            //
            this.Conv5_2_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv5_2_Biases = LoadBiases(br, 512);
            //
            this.Conv5_3_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv5_3_Biases = LoadBiases(br, 512);
            //
            this.Conv5_3_Mean = LoadBiases(br, 512);
            this.Conv5_3_Variance = LoadBiases(br, 512);
            this.Conv5_3_Scale = br.ReadSingle();
            //
            this.Conv6_1_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv6_1_Biases = LoadBiases(br, 512);
            //
            this.Conv6_2_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv6_2_Biases = LoadBiases(br, 512);
            //
            this.Conv6_3_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv6_3_Biases = LoadBiases(br, 512);
            //
            this.Conv6_3_Mean = LoadBiases(br, 512);
            this.Conv6_3_Variance = LoadBiases(br, 512);
            this.Conv6_3_Scale = br.ReadSingle();
            //
            this.Conv7_1_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv7_1_Biases = LoadBiases(br, 512);
            //
            this.Conv7_2_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv7_2_Biases = LoadBiases(br, 512);
            //
            this.Conv7_3_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.Conv7_3_Biases = LoadBiases(br, 512);
            //
            this.Conv7_3_Mean = LoadBiases(br, 512);
            this.Conv7_3_Variance = LoadBiases(br, 512);
            this.Conv7_3_Scale = br.ReadSingle();
            //
            this.Conv8_1_Weights = LoadWeights(br, 4, 4, 256, 512);
            this.Conv8_1_Biases = LoadBiases(br, 256);
            //
            this.Conv8_2_Weights = LoadWeights(br, 3, 3, 256, 256);
            this.Conv8_2_Biases = LoadBiases(br, 256);
            //
            this.Conv8_3_Weights = LoadWeights(br, 3, 3, 256, 256);
            this.Conv8_3_Biases = LoadBiases(br, 256);
            //
            this.Conv8_313_Weights = LoadWeights(br, 1, 1, 256, 313);
            this.Conv8_313_Biases = LoadBiases(br, 313);
            //
            this.Class8_ab_Weights = LoadWeights(br, 1, 1, 313, 2);
            this.Class8_ab_Biases = LoadBiases(br, 2);
            //
            br.Close();
        }

    }

}