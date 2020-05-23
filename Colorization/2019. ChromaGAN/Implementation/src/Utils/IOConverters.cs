//*************************************************************************************************
//* (C) ColorfulSoft, 2020. Все права защищены.
//*************************************************************************************************

//-> Методы преобразования System.Drawing.Bitmap <=> Tensor.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace NeuralColor
{

    ///<summary>Предоставляет методы для преобразований между System.Drawing.Bitmap и классом Tensor.</summary>
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

        public static unsafe Tensor Preprocess(Bitmap x)
        {
            var tmp = new Bitmap(x, 224, 224);
            var BD = tmp.LockBits(new Rectangle(0, 0, tmp.Width, tmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var Result = new Tensor(224, 224, 3);
            for(int y = 0; y < tmp.Height; y++)
            {
                var Addr = (byte*)(BD.Scan0.ToInt32() + BD.Stride * y);
                for(int _x = 0; _x < tmp.Width; _x++)
                {
                    var B = *Addr;
                    Addr += 1;
                    var G = *Addr;
                    Addr += 1;
                    var R = *Addr;
                    Addr += 1;
                    var l = RGB2L(R, G, B) / 100f;
                    Result.Set(_x, y, 0, l);
                    Result.Set(_x, y, 1, l);
                    Result.Set(_x, y, 2, l);
                }
            }
            tmp.UnlockBits(BD);
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

        public static void rgb2lab(float r_s, float g_s, float b_s, ref float L, ref float A, ref float B)
        {
            r_s /= 255f;
            g_s /= 255f;
            b_s /= 255f;
            r_s = (r_s > 0.04045f) ? ((float)Math.Pow((r_s + 0.055f) / 1.055f, 2.4f) * 100f) : (r_s / 12.92f * 100f);
            g_s = (g_s > 0.04045f) ? ((float)Math.Pow((g_s + 0.055f) / 1.055f, 2.4f) * 100f) : (g_s / 12.92f * 100f);
            b_s = (b_s > 0.04045f) ? ((float)Math.Pow((b_s + 0.055f) / 1.055f, 2.4f) * 100f) : (b_s / 12.92f * 100f);
            var X = (r_s * 0.4124f + g_s * 0.3576f + b_s * 0.1805f) / 95.047f;
            var Y = (r_s * 0.2126f + g_s * 0.7152f + b_s * 0.0722f) / 100f;
            var Z = (r_s * 0.0193f + g_s * 0.1192f + b_s * 0.9505f) / 108.883f;
            X = (X > 0.008856f) ? ((float)Math.Pow(X, 0.3333f)) : (7.787f * X + 16f / 116f);
            Y = (Y > 0.008856f) ? ((float)Math.Pow(Y, 0.3333f)) : (7.787f * Y + 16f / 116f);
            Z = (Z > 0.008856f) ? ((float)Math.Pow(Z, 0.3333f)) : (7.787f * Z + 16f / 116f);
            L = 116f * Y - 16f;
            A = 500f * (X - Y);
            B = 200f * (Y - Z);
        }

        ///<summary>Преобразует тензор в изображение.</summary>
        ///<param name="Image">Тензор.</param>
        public static unsafe Bitmap Deprocess(Bitmap l, Tensor ab)
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
                    lab2rgb(RGB2L((byte)r, (byte)g, (byte)b), (ab.Get(_x, _y, 0) * 2f - 1f) * 150f, (ab.Get(_x, _y, 1) * 2f - 1f) * 150f, ref r, ref g, ref b);
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
                    var colorL = 0f;
                    var colorA = 0f;
                    var colorB = 0f;
                    rgb2lab(*(Addr_ab + 2), *(Addr_ab + 1), *Addr_ab, ref colorL, ref colorA, ref colorB);
                    var contentL = RGB2L(*(Addr_l + 2), *(Addr_l + 1), *Addr_l);
                    var R = 0f;
                    var G = 0f;
                    var B = 0f;
                    lab2rgb(contentL, colorA, colorB, ref R, ref G, ref B);
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