//*************************************************************************************************
//* (C) ColorfulSoft corp., 2020. All Rights reserved.
//*************************************************************************************************

//-> LinearLayer implementation.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

    public static partial class Layers
    {

        ///<summary>Fully connected layer forward pass.</summary>
        ///<param name="input">Input tensor.</param>
        ///<param name="weights">Weights.</param>
        ///<param name="biases">Biases.</param>
        public static Tensor LinearLayer(Tensor input, Tensor[] weights, Tensor biases)
        {
            var Height = input.Height;
            var Width = input.Width;
            var Result = new Tensor(1, 1, weights.Length);
            Parallel.For(0, weights.Length, (int d) =>
            {
                var f = weights[d];
                var a = 0f;
                var i = 0;
                for(int id = 0; id < input.Depth; id++)
                {
                    for(int y = 0; y < Height; y++)
                    {
                        for(int x = 0; x < Width; x++)
                        {
                            a += input.Get(x, y, id) * f.W[i];
                            i += 1;
                        }
                    }
                }
                Result.W[d] = a + biases.W[d];
            });
            return Result;
        }

    }

}