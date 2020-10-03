//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019. All Rights reserved.
//*************************************************************************************************

//-> ElementwiseMulLayer implementation.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

    public static partial class Layers
    {

        ///<summary>Elementwise multiplication.</summary>
        ///<param name="input">Input tensor.</param>
        ///<param name="Scale">Multiplier.</param>
        public static Tensor ElementwiseMul(Tensor input, float Scale)
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
                        Result.Set(x, y, d, v * Scale);
                    }
                }
            });
            return Result;
        }

    }

}