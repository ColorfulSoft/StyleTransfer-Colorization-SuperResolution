//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2021. All Rights reserved.
//*************************************************************************************************

using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ColorfulSoft.NeuralArt.AdaIN
{

    /// <summary>
    /// Contains various operations on tensors.
    /// </summary>
    internal static unsafe class Functional
    {

        /// <summary>
        /// AdaIN2d
        /// </summary>
        /// <param name="c">Content.</param>
        /// <param name="s">Style.</param>
        /// <returns>Tensor.</returns>
        public static Tensor AdaIN2d(Tensor c, Tensor s, float eps = 1e-5f, float alpha = 1f)
        {
            var y = new Tensor(c.Shape[0], c.Shape[1], c.Shape[2]);
            var shw = s.Shape[1] * s.Shape[2];
            var chw = c.Shape[1] * c.Shape[2];
            var ps = s.Data;
            var pc = c.Data;
            var py = y.Data;
            for(int i = 0; i < s.Shape[0]; ++i, ps += shw)
            {
                var smean = 0f;
                for(float* ps_ = ps; ps_ < (ps + shw); ++ps_)
                {
                    smean += *ps_;
                }
                smean /= shw;
                var svar = 0f;
                for(float* ps_ = ps; ps_ < (ps + shw); ++ps_)
                {
                    var d = *ps_ - smean;
                    svar += d * d;
                }
                svar = (float)Math.Sqrt(svar / shw + eps);
                var cmean = 0f;
                for(float* pc_ = pc; pc_ < (pc + chw); ++pc_)
                {
                    cmean += *pc_;
                }
                cmean /= chw;
                var cvar = 0f;
                for(float* pc_ = pc; pc_ < (pc + chw); ++pc_)
                {
                    var d = *pc_ - cmean;
                    cvar += d * d;
                }
                cvar = (float)Math.Sqrt(cvar / chw + eps);
                for(int j = 0; j < chw; ++j)
                {
                    *py++ = (*pc++ - cmean) / cvar * svar + smean;
                }
            }
            return y;
        }

        // Improved version of this: https://habr.com/ru/post/448436/
        /// <summary>
        /// Converts input tensor to matrix view.
        /// </summary>
        /// <param name="src">Input tensor.</param>
        /// <param name="srcC">Input's channels.</param>
        /// <param name="srcH">Input's height.</param>
        /// <param name="srcW">Input's width.</param>
        /// <param name="kernelY">Kernel's height.</param>
        /// <param name="kernelX">Kernel's width.</param>
        /// <param name="dilationY">Dilation by height.</param>
        /// <param name="dilationX">Dilation by width.</param>
        /// <param name="strideY">Stride by height.</param>
        /// <param name="strideX">Stride by width.</param>
        /// <param name="padY">Padding by top side.</param>
        /// <param name="padX">Padding by left side.</param>
        /// <param name="padH">Padding by bottom side.</param>
        /// <param name="padW">Padding by right side.</param>
        /// <param name="buf">Buffer.</param>
        public static void im2col(float* src,
                                  int srcC,
                                  int srcH,
                                  int srcW,
                                  int kernelY,
                                  int kernelX,
                                  int dilationY,
                                  int dilationX,
                                  int strideY,
                                  int strideX,
                                  int padY,
                                  int padX,
                                  int padH,
                                  int padW,
                                  float* buf)
        {
            int dstH = (srcH + padY + padH - (dilationY * (kernelY - 1) + 1)) / strideY + 1;
            int dstW = (srcW + padX + padW - (dilationX * (kernelX - 1) + 1)) / strideX + 1;
            for(int sc = 0; sc < srcC; ++sc)
            {
                for(int ky = 0; ky < kernelY; ky++)
                {
                    for(int kx = 0; kx < kernelX; kx++)
                    {
                        for(int dy = 0; dy < dstH; ++dy)
                        {
                            int sy = dy * strideY + ky * dilationY - padY;
                            if((sy < 0) || (sy >= srcH))
                            {
                                for(int dx = 0; dx < dstW; ++dx)
                                {
                                    *buf++ = 0;
                                }
                                continue;
                            }
                            for(int dx = 0; dx < dstW; ++dx)
                            {
                                int sx = dx * strideX + kx * dilationX - padX;
                                if((sx >= 0) && (sx < srcW))
                                {
                                    *buf++ = src[(sc * srcH + sy) * srcW + sx];
                                }
                                else
                                {
                                    *buf++ = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        // Improved version of this: https://habr.com/ru/post/448436/
        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="M">Size.</param>
        /// <param name="N">Size.</param>
        /// <param name="K">Size.</param>
        /// <param name="A">Matrix A.</param>
        /// <param name="B">Matrix B.</param>
        /// <param name="C">Result.</param>
        public static void gemm_nn(int M,
                                   int N,
                                   int K,
                                   float* A,
                                   float* B,
                                   float* C)
        {
            var Bt = (float*)Marshal.AllocHGlobal(K * N * sizeof(float)).ToPointer();
            for(int j = 0; j < N; ++j)
            {
                for(int k = 0; k < K; ++k)
                {
                    Bt[j * K + k] = B[k * N + j];
                }
            }
            Parallel.For(0, M, (int i) =>
            {
                for(int j = 0; j < N; ++j)
                {
                    var sum = 0f;
                    for(int k = 0; k < K; ++k)
                    {
                        sum += A[i * K + k] * Bt[j * K + k];
                    }
                    C[i * N + j] = sum;
                }
            });
            Marshal.FreeHGlobal((IntPtr)Bt);
        }

        // Improved version of this: https://habr.com/ru/post/448436/
        /// <summary>
        /// Conv2d.
        /// </summary>
        /// <param name="x">Input tensor.</param>
        /// <param name="weight">Weight.</param>
        /// <param name="bias">Bias.</param>
        /// <param name="padY">Padding by top side.</param>
        /// <param name="padX">Padding by left side.</param>
        /// <param name="padH">Padding by bottom side.</param>
        /// <param name="padW">Padding by right side.</param>
        /// <param name="strideY">Stride by "y".</param>
        /// <param name="strideX">Stride by "x".</param>
        /// <param name="dilationY">Stride by "y".</param>
        /// <param name="dilationX">Stride by "x".</param>
        /// <param name="group">Groups.</param>
        /// <returns>Tensor.</returns>
        public static Tensor Conv2d(Tensor x,
                                    Tensor weight,
                                    Tensor bias,
                                    int padY,
                                    int padX,
                                    int padH,
                                    int padW,
                                    int strideY,
                                    int strideX,
                                    int dilationY,
                                    int dilationX,
                                    int group)
        {
            int srcC = x.Shape[0];
            int srcH = x.Shape[1];
            int srcW = x.Shape[2];
            int kernelY = weight.Shape[2];
            int kernelX = weight.Shape[3];
            int dstC = weight.Shape[0];
            int dstH = (srcH + padY + padH - (dilationY * (kernelY - 1) + 1)) / strideY + 1;
            int dstW = (srcW + padX + padW - (dilationX * (kernelX - 1) + 1)) / strideX + 1;
            var y = new Tensor(dstC, dstH, dstW);
            var buf = (float*)Marshal.AllocHGlobal(srcC * dstH * dstW * kernelY * kernelX * sizeof(float)).ToPointer();
            // Pointers
            var pdst = y.Data;
            var pweight = weight.Data;
            var psrc = x.Data;
            int M = dstC / group;
            int N = dstH * dstW;
            int K = srcC * kernelY * kernelX / group;
            im2col(psrc, srcC, srcH, srcW, kernelY, kernelX, dilationY, dilationX, strideY, strideX, padY, padX, padH, padW, buf);
            for(int g = 0; g < group; ++g)
            {
                gemm_nn(M, N, K, pweight + M * K * g, buf + N * K * g, pdst + M * N * g);
            }
            if(bias != null)
            {
                var pbias = bias.Data;
                for(int i = 0; i < dstC; ++i)
                {
                    for(int j = 0; j < N; ++j)
                    {
                        pdst[i * N + j] += pbias[i];
                    }
                }
            }
            Marshal.FreeHGlobal((IntPtr)buf);
            return y;
        }

        /// <summary>
        /// MaxPool2d.
        /// </summary>
        /// <param name="x">Input tensor.</param>
        /// <param name="kernelH">Kernel's height.</param>
        /// <param name="kernelW">Kernel's width.</param>
        /// <param name="strideY">Stride by "y".</param>
        /// <param name="strideX">Stride by "x".</param>
        /// <param name="paddingY">Padding by "y".</param>
        /// <param name="paddingX">Padding by "x".</param>
        /// <param name="dilationY">Padding by "y".</param>
        /// <param name="dilationX">Padding by "x".</param>
        /// <returns>Tensor.</returns>
        public static Tensor MaxPool2d(Tensor x,
                                       int kernelH,
                                       int kernelW,
                                       int strideY,
                                       int strideX,
                                       int paddingY,
                                       int paddingX,
                                       int dilationY,
                                       int dilationX)
        {
            int x_width = x.Shape[2];
            int x_height = x.Shape[1];
            int x_channel = x.Shape[0];
            int y_width = (x_width + 2 * paddingX - dilationX * (kernelW - 1) - 1) / strideX + 1;
            int y_height = (x_height + 2 * paddingY - dilationY * (kernelH - 1) - 1) / strideY + 1;
            int y_channel = x_channel;
            var y = new Tensor(y_channel, y_height, y_width);
            var px = x.Data;
            var py = y.Data;
            Parallel.For(0, x_channel, (int c) =>
            {
                for(int ox = 0; ox < y_width; ++ox)
                {
                    var ix_ = ox * strideX - paddingX;
                    for(int oy = 0; oy < y_height; ++oy)
                    {
                        var iy_ = oy * strideY - paddingY;
                        var max = float.MinValue;
                        for(int fx = 0; fx < kernelW; ++fx)
                        {
                            var ix = ix_ + fx * dilationX;
                            if((ix >= x_width) || (ix < 0))
                            {
                                continue;
                            }
                            for(int fy = 0; fy < kernelH; ++fy)
                            {
                                var iy = iy_ + fy * dilationY;
                                if((iy >= x_height) || (iy < 0))
                                {
                                    continue;
                                }
                                var v = px[(c * x_height + iy) * x_width + ix];
                                if(v > max)
                                {
                                    max = v;
                                }
                            }
                        }
                        py[(c * y_height + oy) * y_width + ox] = max;
                    }
                }
            });
            return y;
        }

        /// <summary>
        /// Inplace relu.
        /// </summary>
        /// <param name="x">Input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor ReLU_(Tensor x)
        {
            for(float* px = x.Data; px < (x.Data + x.Numel); ++px)
            {
                if(*px < 0)
                {
                    *px = 0;
                }
            }
            return x;
        }

        /// <summary>
        /// Upsampling using nearest neighbor.
        /// </summary>
        /// <param name="x">Input tensor.</param>
        /// <param name="scale_y">Scale factor by height.</param>
        /// <param name="scale_x">Scale factor by width.</param>
        /// <returns>Upsampled tensor.</returns>
        public static Tensor NearestUpsample2d(Tensor x, int scale_y, int scale_x)
        {
            var y = new Tensor(x.Shape[0], x.Shape[1] * scale_y, x.Shape[2] * scale_x);
            var px = x.Data;
            var py = y.Data;
            for(int d_ = 0; d_ < x.Shape[0]; ++d_)
            {
                for(int y_ = 0; y_ < x.Shape[1]; ++y_)
                {
                    var ty = y_ * scale_y;
                    for(int x_ = 0; x_ < x.Shape[2]; ++x_)
                    {
                        var tx = x_ * scale_x;
                        var xv = px[(d_ * x.Shape[1] + y_) * x.Shape[2] + x_];
                        for(int fy = 0; fy < scale_y; ++fy)
                        {
                            var oy = ty + fy;
                            for(int fx = 0; fx < scale_x; ++fx)
                            {
                                py[(d_ * y.Shape[1] + oy) * y.Shape[2] + tx + fx] = xv;
                            }
                        }
                    }
                }
            }
            return y;
        }

    }

}
