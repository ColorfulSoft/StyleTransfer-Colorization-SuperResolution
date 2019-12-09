//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Методы преобразования System.Drawing.Bitmap <=> NeuralArt.Tensor.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace NeuralEnhance
{

    ///<summary>Предоставляет методы для преобразований между System.Drawing.Bitmap и классом Tensor.</summary>
    public static class IOConverters
    {

        ///<summary>Преобразует изображение в тензор.</summary>
        ///<param name="Image">Изображение.</param>
        public static unsafe Tensor ImageToTensor(Bitmap Image)
        {
            var Result = new Tensor(Image.Width, Image.Height, 3);
            var BD = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var w = Image.Width;
            for(int y = 0; y < Image.Height; y++)
            {
                var Addr = (byte*)(BD.Scan0.ToInt32() + BD.Stride * y);
                for(int x = 0; x < w; x++)
                {
                    Result.Set(x, y, 0, (float)(*Addr) / 127.5f - 1f);
                    Addr += 1;
                    Result.Set(x, y, 1, (float)(*Addr) / 127.5f - 1f);
                    Addr += 1;
                    Result.Set(x, y, 2, (float)(*Addr) / 127.5f - 1f);
                    Addr += 1;
                }
            }
            Image.UnlockBits(BD);
            return Result;
        }

        ///<summary>Преобразует тензор в изображение.</summary>
        ///<param name="Image">Тензор.</param>
        public static unsafe Bitmap TensorToImage(Tensor img)
        {
            var result = new Bitmap(img.Width, img.Height);
            var BD = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            for(int y = 0; y < img.Height; y++)
            {
                byte* Addr = (byte*)(BD.Scan0.ToInt32() + BD.Stride * y);
                for(int x = 0; x < img.Width; x++)
                {
                    var r = (img.Get(x, y, 2) + 1f) * 127.5f;
                    var g = (img.Get(x, y, 1) + 1f) * 127.5f;
                    var b = (img.Get(x, y, 0) + 1f) * 127.5f;
                    *Addr = (byte)b;
                    Addr += 1;
                    *Addr = (byte)g;
                    Addr += 1;
                    *Addr = (byte)r;
                    Addr += 1;
                }
            }
            result.UnlockBits(BD);
            BD = null;
            return result;
        }

    }

}