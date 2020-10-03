//*************************************************************************************************
//* (C) ColorfulSoft corp., 2020. All Rights reserved.
//*************************************************************************************************

//-> System.Drawing.Bitmap <=> Tensor converters.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace NeuralColor
{

    ///<summary>Implements the conversions for System.Drawing.Bitmap and Tensor.</summary>
    public static class IOConverters
    {

        public static unsafe Tuple<Tensor, Tensor> Preprocess(Bitmap x)
        {
            var tmp1 = new Bitmap(x, (x.Width / 8) * 8, (x.Height / 8) * 8);
            var BD1 = tmp1.LockBits(new Rectangle(0, 0, tmp1.Width, tmp1.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var tmp2 = new Bitmap(x, 224, 224);
            var BD2 = tmp2.LockBits(new Rectangle(0, 0, tmp2.Width, tmp2.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var Result1 = new Tensor(tmp1.Width, tmp1.Height, 1);
            var Result2 = new Tensor(224, 224, 1);
            for(int y = 0; y < tmp1.Height; y++)
            {
                var Addr = (byte*)(BD1.Scan0.ToInt32() + BD1.Stride * y);
                for(int _x = 0; _x < tmp1.Width; _x++)
                {
                    var B = *Addr / 255f;
                    Addr += 1;
                    var G = *Addr / 255f;
                    Addr += 1;
                    var R = *Addr / 255f;
                    Addr += 1;
                    var Y = 0.299f * R + 0.587f * G + 0.114f * B;
                    Result1.Set(_x, y, 0, Y - 0.44505388568813414f);
                }
            }
            tmp1.UnlockBits(BD1);
            for(int y = 0; y < tmp2.Height; y++)
            {
                var Addr = (byte*)(BD2.Scan0.ToInt32() + BD2.Stride * y);
                for(int _x = 0; _x < tmp2.Width; _x++)
                {
                    var B = *Addr / 255f;
                    Addr += 1;
                    var G = *Addr / 255f;
                    Addr += 1;
                    var R = *Addr / 255f;
                    Addr += 1;
                    var Y = 0.299f * R + 0.587f * G + 0.114f * B;
                    Result2.Set(_x, y, 0, Y - 0.44505388568813414f);
                }
            }
            tmp2.UnlockBits(BD2);
            return new Tuple<Tensor, Tensor>(Result1, Result2);
        }

        //http://www.easyrgb.com/index.php?X=MATH&H=01#text1
        private static void lab2rgb(float l_s, float a_s, float b_s, ref float R, ref float G, ref float B)
        {
            float var_Y = (l_s + 16f) / 116f;
            float var_X = a_s / 500f + var_Y;
            float var_Z = var_Y - b_s / 200f;
            if(System.Math.Pow(var_Y, 3f) > 0.008856)
            {
                var_Y = (float)System.Math.Pow(var_Y, 3f);
            }
            else
            {
                var_Y = (var_Y - 16f / 116f) / 7.787f;
            }
            if(System.Math.Pow(var_X, 3f) > 0.008856)
            {
                var_X = (float)System.Math.Pow(var_X, 3f);
            }
            else
            {
                var_X = (var_X - 16f / 116f) / 7.787f;
            }
            if(System.Math.Pow(var_Z, 3f) > 0.008856)
            {
                var_Z = (float)System.Math.Pow(var_Z, 3f);
            }
            else
            {
                var_Z = (var_Z - 16f / 116f) / 7.787f;
            }
            float X = 95.047f * var_X;    //ref_X =  95.047     Observer= 2°, Illuminant= D65
            float Y = 100.000f * var_Y;   //ref_Y = 100.000
            float Z = 108.883f * var_Z;   //ref_Z = 108.883
            var_X = X / 100f;             //X from 0 to  95.047      (Observer = 2°, Illuminant = D65)
            var_Y = Y / 100f;             //Y from 0 to 100.000
            var_Z = Z / 100f;             //Z from 0 to 108.883
            float var_R = var_X * 3.2406f + var_Y * -1.5372f + var_Z * -0.4986f;
            float var_G = var_X * -0.9689f + var_Y * 1.8758f + var_Z * 0.0415f;
            float var_B = var_X * 0.0557f + var_Y * -0.2040f + var_Z * 1.0570f;
            if(var_R > 0.0031308f)
            {
                var_R = 1.055f * (float)System.Math.Pow(var_R, (1f / 2.4f)) - 0.055f;
            }
            else
            {
                var_R = 12.92f * var_R;
            }
            if(var_G > 0.0031308f)
            {
                var_G = 1.055f * (float)System.Math.Pow(var_G, (1f / 2.4f)) - 0.055f;
            }
            else
            {
                var_G = 12.92f * var_G;
            }
            if(var_B > 0.0031308f)
            {
                var_B = 1.055f * (float)System.Math.Pow(var_B, (1f / 2.4f)) - 0.055f;
            }
            else
            {
                var_B = 12.92f * var_B;
            }
            R = var_R * 255f;
            G = var_G * 255f;
            B = var_B * 255f;
        }

        public static unsafe Bitmap Deprocess(Tensor Y, Tensor UV)
        {
            var tmp = new Bitmap(Math.Min(Y.Width, UV.Width), Math.Min(Y.Height, UV.Height));
            var BD = tmp.LockBits(new Rectangle(0, 0, tmp.Width, tmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            for(int _y = 0; _y < tmp.Height; _y++)
            {
                byte* Addr = (byte*)(BD.Scan0.ToInt32() + BD.Stride * _y);
                for(int _x = 0; _x < tmp.Width; _x++)
                {
                    float R = 0;
                    float G = 0;
                    float B = 0;
                    lab2rgb((Y.Get(_x, _y, 0) + 0.44505388568813414f) * 100f, (UV.Get(_x, _y, 0) * 2f - 1f) * 100f, (UV.Get(_x, _y, 1) * 2f - 1f) * 100f, ref R, ref G, ref B);
                    R = System.Math.Min(System.Math.Max(R, 0f), 255f);
                    G = System.Math.Min(System.Math.Max(G, 0f), 255f);
                    B = System.Math.Min(System.Math.Max(B, 0f), 255f);
                    *Addr = (byte)B;
                    Addr += 1;
                    *Addr = (byte)G;
                    Addr += 1;
                    *Addr = (byte)R;
                    Addr += 1;
                }
            }
            tmp.UnlockBits(BD);
            return tmp;
        }

    }

}