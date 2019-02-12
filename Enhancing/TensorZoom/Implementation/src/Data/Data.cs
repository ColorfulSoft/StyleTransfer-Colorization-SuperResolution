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

        public Tensor Conv1_Mean
        {
            get;
            set;
        }

        public Tensor Conv1_Var
        {
            get;
            set;
        }

        public Tensor[] Res1_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor Res1_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor Res1_Conv1_Shift
        {
            get;
            set;
        }

        public Tensor Res1_Conv1_Scale
        {
            get;
            set;
        }

        public Tensor Res1_Conv1_Mean
        {
            get;
            set;
        }

        public Tensor Res1_Conv1_Var
        {
            get;
            set;
        }

        public Tensor[] Res1_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor Res1_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor Res1_Conv2_Shift
        {
            get;
            set;
        }

        public Tensor Res1_Conv2_Scale
        {
            get;
            set;
        }

        public Tensor Res1_Conv2_Mean
        {
            get;
            set;
        }

        public Tensor Res1_Conv2_Var
        {
            get;
            set;
        }

        public Tensor[] Res2_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor Res2_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor Res2_Conv1_Shift
        {
            get;
            set;
        }

        public Tensor Res2_Conv1_Scale
        {
            get;
            set;
        }

        public Tensor Res2_Conv1_Mean
        {
            get;
            set;
        }

        public Tensor Res2_Conv1_Var
        {
            get;
            set;
        }

        public Tensor[] Res2_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor Res2_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor Res2_Conv2_Shift
        {
            get;
            set;
        }

        public Tensor Res2_Conv2_Scale
        {
            get;
            set;
        }

        public Tensor Res2_Conv2_Mean
        {
            get;
            set;
        }

        public Tensor Res2_Conv2_Var
        {
            get;
            set;
        }

        public Tensor[] Res3_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor Res3_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor Res3_Conv1_Shift
        {
            get;
            set;
        }

        public Tensor Res3_Conv1_Scale
        {
            get;
            set;
        }

        public Tensor Res3_Conv1_Mean
        {
            get;
            set;
        }

        public Tensor Res3_Conv1_Var
        {
            get;
            set;
        }

        public Tensor[] Res3_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor Res3_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor Res3_Conv2_Shift
        {
            get;
            set;
        }

        public Tensor Res3_Conv2_Scale
        {
            get;
            set;
        }

        public Tensor Res3_Conv2_Mean
        {
            get;
            set;
        }

        public Tensor Res3_Conv2_Var
        {
            get;
            set;
        }

        public Tensor[] Res4_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor Res4_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor Res4_Conv1_Shift
        {
            get;
            set;
        }

        public Tensor Res4_Conv1_Scale
        {
            get;
            set;
        }

        public Tensor Res4_Conv1_Mean
        {
            get;
            set;
        }

        public Tensor Res4_Conv1_Var
        {
            get;
            set;
        }

        public Tensor[] Res4_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor Res4_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor Res4_Conv2_Shift
        {
            get;
            set;
        }

        public Tensor Res4_Conv2_Scale
        {
            get;
            set;
        }

        public Tensor Res4_Conv2_Mean
        {
            get;
            set;
        }

        public Tensor Res4_Conv2_Var
        {
            get;
            set;
        }

        public Tensor[] Res5_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor Res5_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor Res5_Conv1_Shift
        {
            get;
            set;
        }

        public Tensor Res5_Conv1_Scale
        {
            get;
            set;
        }

        public Tensor Res5_Conv1_Mean
        {
            get;
            set;
        }

        public Tensor Res5_Conv1_Var
        {
            get;
            set;
        }

        public Tensor[] Res5_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor Res5_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor Res5_Conv2_Shift
        {
            get;
            set;
        }

        public Tensor Res5_Conv2_Scale
        {
            get;
            set;
        }

        public Tensor Res5_Conv2_Mean
        {
            get;
            set;
        }

        public Tensor Res5_Conv2_Var
        {
            get;
            set;
        }

        public Tensor[] Res6_Conv1_Weights
        {
            get;
            set;
        }

        public Tensor Res6_Conv1_Biases
        {
            get;
            set;
        }

        public Tensor Res6_Conv1_Shift
        {
            get;
            set;
        }

        public Tensor Res6_Conv1_Scale
        {
            get;
            set;
        }

        public Tensor Res6_Conv1_Mean
        {
            get;
            set;
        }

        public Tensor Res6_Conv1_Var
        {
            get;
            set;
        }

        public Tensor[] Res6_Conv2_Weights
        {
            get;
            set;
        }

        public Tensor Res6_Conv2_Biases
        {
            get;
            set;
        }

        public Tensor Res6_Conv2_Shift
        {
            get;
            set;
        }

        public Tensor Res6_Conv2_Scale
        {
            get;
            set;
        }

        public Tensor Res6_Conv2_Mean
        {
            get;
            set;
        }

        public Tensor Res6_Conv2_Var
        {
            get;
            set;
        }

        public Tensor[] Deconv1_Weights
        {
            get;
            set;
        }

        public Tensor Deconv1_Biases
        {
            get;
            set;
        }

        public Tensor Deconv1_Shift
        {
            get;
            set;
        }

        public Tensor Deconv1_Scale
        {
            get;
            set;
        }

        public Tensor Deconv1_Mean
        {
            get;
            set;
        }

        public Tensor Deconv1_Var
        {
            get;
            set;
        }

        public Tensor[] Deconv2_Weights
        {
            get;
            set;
        }

        public Tensor Deconv2_Biases
        {
            get;
            set;
        }

        public Tensor Deconv2_Shift
        {
            get;
            set;
        }

        public Tensor Deconv2_Scale
        {
            get;
            set;
        }

        public Tensor Deconv2_Mean
        {
            get;
            set;
        }

        public Tensor Deconv2_Var
        {
            get;
            set;
        }

        public Tensor[] Deconv3_Weights
        {
            get;
            set;
        }

        public Tensor Deconv3_Biases
        {
            get;
            set;
        }

        public Tensor Deconv3_Shift
        {
            get;
            set;
        }

        public Tensor Deconv3_Scale
        {
            get;
            set;
        }

        public Tensor Deconv3_Mean
        {
            get;
            set;
        }

        public Tensor Deconv3_Var
        {
            get;
            set;
        }

        public Data(Stream s)
        {
            var br = new BinaryReader(s);
            this.Conv1_Weights = LoadWeights(br, 9, 9, 3, 64);
            this.Conv1_Biases = LoadBiases(br, 64);
            this.Conv1_Shift = LoadBiases(br, 64);
            this.Conv1_Scale = LoadBiases(br, 64);
            this.Conv1_Mean = LoadBiases(br, 64);
            this.Conv1_Var = LoadBiases(br, 64);
            //
            this.Res1_Conv1_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res1_Conv1_Biases = LoadBiases(br, 64);
            this.Res1_Conv1_Shift = LoadBiases(br, 64);
            this.Res1_Conv1_Scale = LoadBiases(br, 64);
            this.Res1_Conv1_Mean = LoadBiases(br, 64);
            this.Res1_Conv1_Var = LoadBiases(br, 64);
            this.Res1_Conv2_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res1_Conv2_Biases = LoadBiases(br, 64);
            this.Res1_Conv2_Shift = LoadBiases(br, 64);
            this.Res1_Conv2_Scale = LoadBiases(br, 64);
            this.Res1_Conv2_Mean = LoadBiases(br, 64);
            this.Res1_Conv2_Var = LoadBiases(br, 64);
            //
            this.Res2_Conv1_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res2_Conv1_Biases = LoadBiases(br, 64);
            this.Res2_Conv1_Shift = LoadBiases(br, 64);
            this.Res2_Conv1_Scale = LoadBiases(br, 64);
            this.Res2_Conv1_Mean = LoadBiases(br, 64);
            this.Res2_Conv1_Var = LoadBiases(br, 64);
            this.Res2_Conv2_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res2_Conv2_Biases = LoadBiases(br, 64);
            this.Res2_Conv2_Shift = LoadBiases(br, 64);
            this.Res2_Conv2_Scale = LoadBiases(br, 64);
            this.Res2_Conv2_Mean = LoadBiases(br, 64);
            this.Res2_Conv2_Var = LoadBiases(br, 64);
            //
            this.Res3_Conv1_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res3_Conv1_Biases = LoadBiases(br, 64);
            this.Res3_Conv1_Shift = LoadBiases(br, 64);
            this.Res3_Conv1_Scale = LoadBiases(br, 64);
            this.Res3_Conv1_Mean = LoadBiases(br, 64);
            this.Res3_Conv1_Var = LoadBiases(br, 64);
            this.Res3_Conv2_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res3_Conv2_Biases = LoadBiases(br, 64);
            this.Res3_Conv2_Shift = LoadBiases(br, 64);
            this.Res3_Conv2_Scale = LoadBiases(br, 64);
            this.Res3_Conv2_Mean = LoadBiases(br, 64);
            this.Res3_Conv2_Var = LoadBiases(br, 64);
            //
            this.Res4_Conv1_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res4_Conv1_Biases = LoadBiases(br, 64);
            this.Res4_Conv1_Shift = LoadBiases(br, 64);
            this.Res4_Conv1_Scale = LoadBiases(br, 64);
            this.Res4_Conv1_Mean = LoadBiases(br, 64);
            this.Res4_Conv1_Var = LoadBiases(br, 64);
            this.Res4_Conv2_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res4_Conv2_Biases = LoadBiases(br, 64);
            this.Res4_Conv2_Shift = LoadBiases(br, 64);
            this.Res4_Conv2_Scale = LoadBiases(br, 64);
            this.Res4_Conv2_Mean = LoadBiases(br, 64);
            this.Res4_Conv2_Var = LoadBiases(br, 64);
            //
            this.Res5_Conv1_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res5_Conv1_Biases = LoadBiases(br, 64);
            this.Res5_Conv1_Shift = LoadBiases(br, 64);
            this.Res5_Conv1_Scale = LoadBiases(br, 64);
            this.Res5_Conv1_Mean = LoadBiases(br, 64);
            this.Res5_Conv1_Var = LoadBiases(br, 64);
            this.Res5_Conv2_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res5_Conv2_Biases = LoadBiases(br, 64);
            this.Res5_Conv2_Shift = LoadBiases(br, 64);
            this.Res5_Conv2_Scale = LoadBiases(br, 64);
            this.Res5_Conv2_Mean = LoadBiases(br, 64);
            this.Res5_Conv2_Var = LoadBiases(br, 64);
            //
            this.Res6_Conv1_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res6_Conv1_Biases = LoadBiases(br, 64);
            this.Res6_Conv1_Shift = LoadBiases(br, 64);
            this.Res6_Conv1_Scale = LoadBiases(br, 64);
            this.Res6_Conv1_Mean = LoadBiases(br, 64);
            this.Res6_Conv1_Var = LoadBiases(br, 64);
            this.Res6_Conv2_Weights = LoadWeights(br, 3, 3, 64, 64);
            this.Res6_Conv2_Biases = LoadBiases(br, 64);
            this.Res6_Conv2_Shift = LoadBiases(br, 64);
            this.Res6_Conv2_Scale = LoadBiases(br, 64);
            this.Res6_Conv2_Mean = LoadBiases(br, 64);
            this.Res6_Conv2_Var = LoadBiases(br, 64);
            //
            this.Deconv1_Weights = LoadWeights(br, 3, 3, 32, 64);
            this.Deconv1_Biases = LoadBiases(br, 32);
            this.Deconv1_Shift = LoadBiases(br, 32);
            this.Deconv1_Scale = LoadBiases(br, 32);
            this.Deconv1_Mean = LoadBiases(br, 32);
            this.Deconv1_Var = LoadBiases(br, 32);
            //
            this.Deconv2_Weights = LoadWeights(br, 3, 3, 16, 32);
            this.Deconv2_Biases = LoadBiases(br, 16);
            this.Deconv2_Shift = LoadBiases(br, 16);
            this.Deconv2_Scale = LoadBiases(br, 16);
            this.Deconv2_Mean = LoadBiases(br, 16);
            this.Deconv2_Var = LoadBiases(br, 16);
            //
            this.Deconv3_Weights = LoadWeights(br, 9, 9, 3, 16);
            this.Deconv3_Biases = LoadBiases(br, 3);
            this.Deconv3_Shift = LoadBiases(br, 3);
            this.Deconv3_Scale = LoadBiases(br, 3);
            this.Deconv3_Mean = LoadBiases(br, 3);
            this.Deconv3_Var = LoadBiases(br, 3);
            br.Close();
        }

    }

}