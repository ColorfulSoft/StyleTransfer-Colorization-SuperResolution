﻿//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Определение для слоя BatchNormLayer.

using System;
using System.Threading.Tasks;

namespace NeuralEnhance
{

    ///<summary>Предоставляет реализацию слоёв нейросети.</summary>
    public static partial class Layers
    {

        public static Tensor BatchNorm(Tensor input, Tensor shift, Tensor scale, Tensor mean, Tensor variance)
        {
            var Normalized = new Tensor(input.Width, input.Height, input.Depth);
            Parallel.For(0, input.Depth, (int d) =>
            {
                for(int y = 0; y < input.Height; y++)
                {
                    for(int x = 0; x < input.Width; x++)
                    {
                        Normalized.Set(x, y, d, (float)(((input.Get(x, y, d) - mean.W[d]) / Math.Sqrt(0.01f + variance.W[d])) * scale.W[d] + shift.W[d]));
                    }
                }
            });
            return Normalized;
        }

    }

}