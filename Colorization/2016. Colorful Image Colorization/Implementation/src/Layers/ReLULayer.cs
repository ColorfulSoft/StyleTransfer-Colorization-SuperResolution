//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019. All Rights reserved.
//*************************************************************************************************

//-> ReLULayer implementation.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

    public static partial class Layers
    {

        ///<summary>ReLU function.</summary>
        ///<param name="input">Input tensor.</param>
        public static Tensor ReLU(Tensor input)
        {
            var Height = input.Height;
            var Width = input.Width;
            var Result = new Tensor(input.Width, input.Height, input.Depth);
            Parallel.For(0, input.Depth, (int d) =>
            {
                for(int y = 0; y < Height; y++)
                {
                    for(int x = 0; x < Width; x++)
                    {
                        var v = input.Get(x, y, d);
                        Result.Set(x, y, d, (v > 0.0f) ? (v) : (0.0f));
                    }
                }
            });
            return Result;
        }

    }

}