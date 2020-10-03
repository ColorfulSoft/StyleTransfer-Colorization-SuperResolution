//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019. All Rights reserved.
//*************************************************************************************************

//-> Implementation of 3-dimentional tensor.

using System;

namespace NeuralColor
{

    ///<summary>3-dimentional tensor of Single (float) data type.</summary>
    public sealed class Tensor
    {

        ///<summary>Values.</summary>
        public float[] W
        {
            get;
            set;
        }

        ///<summary>Width.</summary>
        public int Width
        {
            get;
            set;
        }

        ///<summary>Height.</summary>
        public int Height
        {
            get;
            set;
        }

        ///<summary>Depth.</summary>
        public int Depth
        {
            get;
            set;
        }

        ///<summary>Initializes the Tensor with specified size.</summary>
        ///<param name="w">Width.</param>
        ///<param name="h">Height.</param>
        ///<param name="d">Depth.</param>
        public Tensor(int w, int h, int d)
        {
            this.W = new float[w * h * d];
            this.Width = w;
            this.Height = h;
            this.Depth = d;
        }

        ///<summary>Gets the value.</summary>
        ///<param name="x">X coordinate (for Width).</param>
        ///<param name="y">Y coordinate (for Height).</param>
        ///<param name="z">Z coordinate (for Depth).</param>
        public float Get(int x, int y, int z)
        {
            return this.W[((this.Width * y) + x) * this.Depth + z];
        }

        ///<summary>Sets the value.</summary>
        ///<param name="x">X coordinate (По ширине).</param>
        ///<param name="y">Y coordinate (По высоте).</param>
        ///<param name="z">Z coordinate (По глубине).</param>
        ///<param name="v">Value.</param>
        public void Set(int x, int y, int z, float v)
        {
            this.W[((this.Width * y) + x) * this.Depth + z] = v;
        }

    }

}