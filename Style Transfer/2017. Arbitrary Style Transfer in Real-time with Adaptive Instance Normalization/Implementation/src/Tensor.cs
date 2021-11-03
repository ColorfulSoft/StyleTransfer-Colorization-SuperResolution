﻿//*************************************************************************************************
//* (C) ColorfulSoft corp., 2021. All Rights reserved.
//*************************************************************************************************

using System;
using System.Runtime.InteropServices;

namespace ColorfulSoft.NeuralArt.AdaIN
{

    /// <summary>
    /// Multidimentional array of floating point data type.
    /// </summary>
    internal sealed unsafe class Tensor : IDisposable
    {

        /// <summary>
        /// Data.
        /// </summary>
        public float* Data;

        /// <summary>
        /// Should destructor free Data?
        /// </summary>
        private bool __DisposeData = true;

        /// <summary>
        /// Shape.
        /// </summary>
        public int* Shape;

        /// <summary>
        /// Number of elements.
        /// </summary>
        public int Numel;

        /// <summary>
        /// Number of dimentions.
        /// </summary>
        public int Ndim;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Tensor()
        {
        }

        /// <summary>
        /// Initializes the tensor with specified shape.
        /// </summary>
        /// <param name="shape">Shape.</param>
        public Tensor(params int[] shape)
        {
            this.Ndim = shape.Length;
            this.Numel = 1;
            this.Shape = (int*)Marshal.AllocHGlobal(sizeof(int) * this.Ndim).ToPointer();
            var Pshape = this.Shape;
            foreach(var Dim in shape)
            {
                this.Numel *= Dim;
                *Pshape++ = Dim;
            }
            this.Data = (float*)Marshal.AllocHGlobal(sizeof(float) * this.Numel).ToPointer();
        }

        /// <summary>
        /// Disposes unmanaged resources of the tensor.
        /// </summary>
        void IDisposable.Dispose()
        {
            if((this.Data != null) && this.__DisposeData)
            {
                Marshal.FreeHGlobal((IntPtr)this.Data);
                this.Data = null;
            }
            if(this.Shape != null)
            {
                Marshal.FreeHGlobal((IntPtr)this.Shape);
                this.Shape = null;
            }
        }

        /// <summary>
        /// Disposes unmanaged resources of the tensor.
        /// </summary>
        ~Tensor()
        {
            if((this.Data != null) && this.__DisposeData)
            {
                Marshal.FreeHGlobal((IntPtr)this.Data);
                this.Data = null;
            }
            if(this.Shape != null)
            {
                Marshal.FreeHGlobal((IntPtr)this.Shape);
                this.Shape = null;
            }
        }

    }

}