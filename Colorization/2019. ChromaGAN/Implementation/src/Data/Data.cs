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

        public Tensor[] VGG_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv3_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv3_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv4_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv4_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv5_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv5_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv6_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv6_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv7_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv7_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv8_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv8_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv9_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv9_Biases
        {
            get;
            set;
        }

        public Tensor[] VGG_Conv10_Weights
        {
            get;
            set;
        }

        public Tensor VGG_Conv10_Biases
        {
            get;
            set;
        }

        public Tensor[] global_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor global_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor global_BN1_Mean
        {
            get;
            set;
        }

        public Tensor global_BN1_Variance
        {
            get;
            set;
        }

        public Tensor global_BN1_Beta
        {
            get;
            set;
        }

        public Tensor global_BN1_Gamma
        {
            get;
            set;
        }

        public Tensor[] global_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor global_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor global_BN2_Mean
        {
            get;
            set;
        }

        public Tensor global_BN2_Variance
        {
            get;
            set;
        }

        public Tensor global_BN2_Beta
        {
            get;
            set;
        }

        public Tensor global_BN2_Gamma
        {
            get;
            set;
        }

        public Tensor[] global_Conv3_Weights
        {
            get;
            set;
        }

        public Tensor global_Conv3_Biases
        {
            get;
            set;
        }

        public Tensor global_BN3_Mean
        {
            get;
            set;
        }

        public Tensor global_BN3_Variance
        {
            get;
            set;
        }

        public Tensor global_BN3_Beta
        {
            get;
            set;
        }

        public Tensor global_BN3_Gamma
        {
            get;
            set;
        }

        public Tensor[] global_Conv4_Weights
        {
            get;
            set;
        }

        public Tensor global_Conv4_Biases
        {
            get;
            set;
        }

        public Tensor global_BN4_Mean
        {
            get;
            set;
        }

        public Tensor global_BN4_Variance
        {
            get;
            set;
        }

        public Tensor global_BN4_Beta
        {
            get;
            set;
        }

        public Tensor global_BN4_Gamma
        {
            get;
            set;
        }

        public Tensor[] global2_Dense1_Weights
        {
            get;
            set;
        }

        public Tensor global2_Dense1_Biases
        {
            get;
            set;
        }

        public Tensor[] global2_Dense2_Weights
        {
            get;
            set;
        }

        public Tensor global2_Dense2_Biases
        {
            get;
            set;
        }

        public Tensor[] global2_Dense3_Weights
        {
            get;
            set;
        }

        public Tensor global2_Dense3_Biases
        {
            get;
            set;
        }

        public Tensor[] midlevel_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor midlevel_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor midlevel_BN1_Mean
        {
            get;
            set;
        }

        public Tensor midlevel_BN1_Variance
        {
            get;
            set;
        }

        public Tensor midlevel_BN1_Beta
        {
            get;
            set;
        }

        public Tensor midlevel_BN1_Gamma
        {
            get;
            set;
        }

        public Tensor[] midlevel_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor midlevel_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor midlevel_BN2_Mean
        {
            get;
            set;
        }

        public Tensor midlevel_BN2_Variance
        {
            get;
            set;
        }

        public Tensor midlevel_BN2_Beta
        {
            get;
            set;
        }

        public Tensor midlevel_BN2_Gamma
        {
            get;
            set;
        }

        public Tensor[] output_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor output_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor[] output_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor output_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor[] output_Conv3_Weights
        {
            get;
            set;
        }

        public Tensor output_Conv3_Biases
        {
            get;
            set;
        }

        public Tensor[] output_Conv4_Weights
        {
            get;
            set;
        }

        public Tensor output_Conv4_Biases
        {
            get;
            set;
        }

        public Tensor[] output_Conv5_Weights
        {
            get;
            set;
        }

        public Tensor output_Conv5_Biases
        {
            get;
            set;
        }

        public Tensor[] output_Conv6_Weights
        {
            get;
            set;
        }

        public Tensor output_Conv6_Biases
        {
            get;
            set;
        }

        public Data(Stream s)
        {
            var br = new BinaryReader(s);
            //
            this.VGG_Conv1_Weights = LoadWeights(br, 3, 3, 3, 64);
            this.VGG_Conv1_Biases = LoadBiases(br, 64);
            //
            this.VGG_Conv2_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.VGG_Conv2_Biases = LoadBiases(br, 64);
            //
            this.VGG_Conv3_Weights = LoadWeights(br, 3, 3, 64, 128);
            this.VGG_Conv3_Biases = LoadBiases(br, 128);
            //
            this.VGG_Conv4_Weights = LoadWeights(br, 3, 3, 128, 128);
            this.VGG_Conv4_Biases = LoadBiases(br, 128);
            //
            this.VGG_Conv5_Weights = LoadWeights(br, 3, 3, 128, 256);
            this.VGG_Conv5_Biases = LoadBiases(br, 256);
            //
            this.VGG_Conv6_Weights = LoadWeights(br, 3, 3, 256, 256);
            this.VGG_Conv6_Biases = LoadBiases(br, 256);
            //
            this.VGG_Conv7_Weights = LoadWeights(br, 3, 3, 256, 256);
            this.VGG_Conv7_Biases = LoadBiases(br, 256);
            //
            this.VGG_Conv8_Weights = LoadWeights(br, 3, 3, 256, 512);
            this.VGG_Conv8_Biases = LoadBiases(br, 512);
            //
            this.VGG_Conv9_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.VGG_Conv9_Biases = LoadBiases(br, 512);
            //
            this.VGG_Conv10_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.VGG_Conv10_Biases = LoadBiases(br, 512);
            //
            this.global_Conv1_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.global_Conv1_Biases = LoadBiases(br, 512);
            this.global_BN1_Mean = LoadBiases(br, 512);
            this.global_BN1_Variance = LoadBiases(br, 512);
            this.global_BN1_Beta = LoadBiases(br, 512);
            this.global_BN1_Gamma = LoadBiases(br, 512);
            //
            this.global_Conv2_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.global_Conv2_Biases = LoadBiases(br, 512);
            this.global_BN2_Mean = LoadBiases(br, 512);
            this.global_BN2_Variance = LoadBiases(br, 512);
            this.global_BN2_Beta = LoadBiases(br, 512);
            this.global_BN2_Gamma = LoadBiases(br, 512);
            //
            this.global_Conv3_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.global_Conv3_Biases = LoadBiases(br, 512);
            this.global_BN3_Mean = LoadBiases(br, 512);
            this.global_BN3_Variance = LoadBiases(br, 512);
            this.global_BN3_Beta = LoadBiases(br, 512);
            this.global_BN3_Gamma = LoadBiases(br, 512);
            //
            this.global_Conv4_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.global_Conv4_Biases = LoadBiases(br, 512);
            this.global_BN4_Mean = LoadBiases(br, 512);
            this.global_BN4_Variance = LoadBiases(br, 512);
            this.global_BN4_Beta = LoadBiases(br, 512);
            this.global_BN4_Gamma = LoadBiases(br, 512);
            //
            this.global2_Dense1_Weights = LoadWeights(br, 1, 1, 25088, 1024);
            this.global2_Dense1_Biases = LoadBiases(br, 1024);
            //
            this.global2_Dense2_Weights = LoadWeights(br, 1, 1, 1024, 512);
            this.global2_Dense2_Biases = LoadBiases(br, 512);
            //
            this.global2_Dense3_Weights = LoadWeights(br, 1, 1, 512, 256);
            this.global2_Dense3_Biases = LoadBiases(br, 256);
            //
            this.midlevel_Conv1_Weights = LoadWeights(br, 3, 3, 512, 512);
            this.midlevel_Conv1_Biases = LoadBiases(br, 512);
            this.midlevel_BN1_Mean = LoadBiases(br, 512);
            this.midlevel_BN1_Variance = LoadBiases(br, 512);
            this.midlevel_BN1_Beta = LoadBiases(br, 512);
            this.midlevel_BN1_Gamma = LoadBiases(br, 512);
            //
            this.midlevel_Conv2_Weights = LoadWeights(br, 3, 3, 512, 256);
            this.midlevel_Conv2_Biases = LoadBiases(br, 256);
            this.midlevel_BN2_Mean = LoadBiases(br, 256);
            this.midlevel_BN2_Variance = LoadBiases(br, 256);
            this.midlevel_BN2_Beta = LoadBiases(br, 256);
            this.midlevel_BN2_Gamma = LoadBiases(br, 256);
            //
            this.output_Conv1_Weights = LoadWeights(br, 1, 1, 512, 256);
            this.output_Conv1_Biases = LoadBiases(br, 256);
            //
            this.output_Conv2_Weights = LoadWeights(br, 3, 3, 256, 128);
            this.output_Conv2_Biases = LoadBiases(br, 128);
            //
            this.output_Conv3_Weights = LoadWeights(br, 3, 3, 128, 64);
            this.output_Conv3_Biases = LoadBiases(br, 64);
            //
            this.output_Conv4_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.output_Conv4_Biases = LoadBiases(br, 64);
            //
            this.output_Conv5_Weights = LoadWeights(br, 3, 3, 64, 32);
            this.output_Conv5_Biases = LoadBiases(br, 32);
            //
            this.output_Conv6_Weights = LoadWeights(br, 3, 3, 32, 2);
            this.output_Conv6_Biases = LoadBiases(br, 2);
            //
            br.Close();
        }

    }

}