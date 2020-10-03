//***********************************************
//* (C) ColorfulSoft corp., 2019. All Rights reserved.
//***********************************************

//-> Upsample2DLayer implementation.

using System;
using System.Threading.Tasks;

namespace NeuralColor
{

    public static partial class Layers
    {

        // In the current neural network only 2x upsampling is used so only it was implemented

        ///<summary>Nearest neighbor upsample.</summary>
        ///<param name="input">Input tensor.</param>
        public static Tensor Upsample2D(Tensor input)
        {
            var Result = new Tensor(input.Width * 2, input.Height * 2, input.Depth);
            for (int d = 0; d < input.Depth; d++)
            {
                for (var ax = 0; ax < input.Width; ax++)
                {
                    var x = 2 * ax;
                    for (var ay = 0; ay < input.Height; ay++)
                    {
                        var y = 2 * ay; 
                        float a = input.Get(ax, ay, d);
                        for (byte fx = 0; fx < 2; fx++)
                        {
                            for (byte fy = 0; fy < 2; fy++)
                            {
                                var oy = y + fy;
                                var ox = x + fx;
                                if (oy >= 0 && oy < Result.Height && ox >= 0 && ox < Result.Width)
                                {
                                   Result.Set(ox, oy, d, a);
                                }
                            }
                        }
                    }
                }
            }
            return Result;
        }

    }

}