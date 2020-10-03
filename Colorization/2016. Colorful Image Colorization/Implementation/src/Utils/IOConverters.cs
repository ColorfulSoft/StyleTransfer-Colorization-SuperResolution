//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019. All Rights reserved.
//*************************************************************************************************

//-> System.Drawing.Bitmap <=> Tensor conversion.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace NeuralColor
{

    ///<summary>Converters for System.Drawing.Bitmap and NeuralColor.Tensor.</summary>
    public static class IOConverters
    {

        private static float RGB2L(byte r, byte g, byte b)
        {
            float var_B = b / 255.0f;
            float var_G = g / 255.0f;
            float var_R = r / 255.0f;
            if(var_R > 0.04045f)
            {
                var_R = (float)System.Math.Pow(((var_R + 0.055f) / 1.055f), 2.4f);
            }
            else
            {
                var_R = var_R / 12.92f;
            }
            if(var_G > 0.04045f)
            {
                var_G = (float)System.Math.Pow(((var_G + 0.055f) / 1.055f), 2.4f);
            }
            else
            {
                var_G = var_G / 12.92f;
            }
            if(var_B > 0.04045f)
            {
                var_B = (float)System.Math.Pow(((var_B + 0.055f) / 1.055f), 2.4f);
            }
            else
            {
                var_B = var_B / 12.92f;
            }
            var_R *= 100f;
            var_G *= 100f;
            var_B *= 100f;
            float Y = var_R * 0.2126f + var_G * 0.7152f + var_B * 0.0722f;
            float var_Y = Y / 100f;
            if(var_Y > 0.008856f)
            {
                var_Y = (float)System.Math.Pow(var_Y, (1f / 3f));
            }
            else
            {
                var_Y = (7.787f * var_Y) + (16f / 116f);
            }
            float l_s = (116f * var_Y) - 16f;
            return l_s;
        }

        ///<summary>Converts the Image to a Tensor.</summary>
        ///<param name="Image">Input Image.</param>
        public static unsafe Tensor ImageToTensor(Bitmap Image)
        {
            Image = new Bitmap(Image, 224, 224);
            var Result = new Tensor(Image.Width, Image.Height, 1);
            var BD = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var w = Image.Width;
            for(int y = 0; y < Image.Height; y++)
            {
                var Addr = (byte*)(BD.Scan0.ToInt32() + BD.Stride * y);
                for(int x = 0; x < w; x++)
                {
                    var B = *Addr;
                    Addr += 1;
                    var G = *Addr;
                    Addr += 1;
                    var R = *Addr;
                    Addr += 1;
                    Result.Set(x, y, 0, RGB2L(R, G, B) - 50f);
                }
            }
            Image.UnlockBits(BD);
            return Result;
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

        ///<summary>Converts the Tensor to the Image.</summary>
        ///<param name="Image">Tensor.</param>
        public static unsafe Bitmap TensorToImage(Bitmap l, Tensor ab)
        {
            var tmp = new Bitmap(l, ab.Width, ab.Height);
            var BD = tmp.LockBits(new Rectangle(0, 0, tmp.Width, tmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            for(int _y = 0; _y < tmp.Height; _y++)
            {
                byte* Addr = (byte*)(BD.Scan0.ToInt32() + BD.Stride * _y);
                for(int _x = 0; _x < tmp.Width; _x++)
                {
                    float r = *(Addr + 2);
                    float g = *(Addr + 1);
                    float b = *(Addr);
                    lab2rgb(RGB2L((byte)r, (byte)g, (byte)b), ab.Get(_x, _y, 0), ab.Get(_x, _y, 1), ref r, ref g, ref b);
                    *(Addr) = (byte)(System.Math.Min(255f, System.Math.Max(0f, b)));
                    Addr += 1;
                    *(Addr) = (byte)(System.Math.Min(255f, System.Math.Max(0f, g)));
                    Addr += 1;
                    *(Addr) = (byte)(System.Math.Min(255f, System.Math.Max(0f, r)));
                    Addr += 1;
                }
            }
            tmp.UnlockBits(BD);
            tmp = new Bitmap(tmp, l.Width, l.Height);
            var BD_ab = tmp.LockBits(new Rectangle(0, 0, tmp.Width, tmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var BD_l = l.LockBits(new Rectangle(0, 0, l.Width, l.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            for(int y = 0; y < l.Height; y++)
            {
                byte* Addr_ab = (byte*)(BD_ab.Scan0.ToInt32() + BD_ab.Stride * y);
                byte* Addr_l = (byte*)(BD_l.Scan0.ToInt32() + BD_l.Stride * y);
                for(int x = 0; x < l.Width; x++)
                {
                    var Y = *(Addr_l + 2) * 0.299f + *(Addr_l + 1) * 0.587f + *Addr_l * 0.114f;
                    var U = *(Addr_ab + 2) * -0.169f + *(Addr_ab + 1) * -0.332f + *Addr_ab * 0.500f + 128.0f;
                    var V = *(Addr_ab + 2) * 0.500f + *(Addr_ab + 1) * -0.419f + *Addr_ab * -0.0813f + 128.0f;
                    var R = Y + (1.4075f * (V - 128f));
                    var G = Y - (0.3455f * (U - 128f) - (0.7169f * (V - 128f)));
                    var B = Y + (1.7790f * (U - 128f));
                    R = System.Math.Min(System.Math.Max(R, 0f), 255f);
                    G = System.Math.Min(System.Math.Max(G, 0f), 255f);
                    B = System.Math.Min(System.Math.Max(B, 0f), 255f);
                    *Addr_ab = (byte)B;
                    Addr_ab += 1;
                    *Addr_ab = (byte)G;
                    Addr_ab += 1;
                    *Addr_ab = (byte)R;
                    Addr_ab += 1;
                    Addr_l += 3;
                }
            }
            tmp.UnlockBits(BD_ab);
            l.UnlockBits(BD_l);
            return tmp;
        }

    }

}