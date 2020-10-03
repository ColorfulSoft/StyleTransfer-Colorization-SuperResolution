//*************************************************************************************************
//* (C) ColorfulSoft corp., 2020. All Rights reserved
//*************************************************************************************************

//-> FusionLayer implementation.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

    public static partial class Layers
    {

        ///<summary>Concatenation layer.</summary>
        ///<param name="input">Input tensor.</param>
        ///<param name="joint">Join tensor.</param>
        public static Tensor Fusion(Tensor input, Tensor joint)
        {
            var Height = input.Height;
            var Width = input.Width;
            var Result = new Tensor(input.Width, input.Height, input.Depth + joint.Depth);
            Parallel.For(0, input.Depth, (int d) =>
            {
                for(int y = 0; y < Height; y++)
                {
                    for(int x = 0; x < Width; x++)
                    {
                        var v = input.Get(x, y, d);
                        Result.Set(x, y, d, v);
                    }
                }
            });
            Parallel.For(0, joint.Depth, (int d) =>
            {
                var v = joint.Get(0, 0, d);
                for(int y = 0; y < Height; y++)
                {
                    for(int x = 0; x < Width; x++)
                    {
                        Result.Set(x, y, d + input.Depth, v);
                    }
                }
            });
            return Result;
        }

    }

}