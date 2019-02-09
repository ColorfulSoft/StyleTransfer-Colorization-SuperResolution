//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace NeuralArt
{

    ///<summary>Предоставляет математические операции над матрицами.</summary>
    public static class Math
    {

        ///<summary>Разворачивает трёхмерный массив в двумерный.</summary>
        ///<param name="T">Трёхмерный входной тензор.</param>
        public static Tensor Flat(Tensor T)
        {
            var PNum = Environment.ProcessorCount;
            var Result = new Tensor(T.Width * T.Height, T.Depth, 1, true);
            var TaskPart = T.Depth / PNum;
            var width = T.Width;
            var height = T.Height;
            Parallel.For(0, PNum, (int p) =>
            {
                for(int d = p * TaskPart; d < (p + 1) * TaskPart; d++)
                {
                    int i = 0;
                    for(int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            Result.SetW(i, d, 0, T.GetW(x, y, d));
                            i += 1;
                        }
                    }
                }
            });
            for(int d = PNum * TaskPart; d < T.Depth; d++)
            {
                int i = 0;
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        Result.SetW(i, d, 0, T.GetW(x, y, d));
                        i += 1;
                    }
                }
            }
            return Result;
        }

        ///<summary>Транспонирует тензор в его двумерном представлении.</summary>
        ///<param name="T">Входной тензор в двумерном представлении.</param>
        public static Tensor Transpose2D(Tensor T)
        {
            var PNum = Environment.ProcessorCount;
            var Result = new Tensor(T.Height, T.Width, 1, true);
            var TaskPart = T.Height / PNum;
            var width = T.Width;
            Parallel.For(0, PNum, (int p) =>
            {
                for(int y = TaskPart * p; y < TaskPart * (p + 1); y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        Result.SetW(y, x, 0, T.GetW(x, y, 0));
                    }
                }
            });
            for(int y = PNum * TaskPart; y < T.Height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Result.SetW(y, x, 0, T.GetW(x, y, 0));
                }
            }
            return Result;
        }

        ///<summary>Выполняет умножение двух тензоров в двумерном представлении.</summary>
        ///<param name="A">Первая матрица - множитель.</param>
        ///<param name="B">Вторая матрица - множитель.</param>
        public static Tensor MatMul2D(Tensor A, Tensor B)
        {
            var PNum = Environment.ProcessorCount;
            var Result = new Tensor(A.Width, B.Height, 1, true);
            var TaskPart = A.Width / PNum;
            Parallel.For(0, PNum, (int p) =>
            {
                for(int i = TaskPart * p; i < TaskPart * (p + 1); i++)
                {
                    for(int j = 0; j < B.Height; j++)
                    {
                        for(int k = 0; k < B.Width; k++)
                        {
                            Result.AddW(i, j, 0, A.GetW(i, k, 0) * B.GetW(k, j, 0));
                        }
                    }
                }
            });
            for(int i = TaskPart * PNum; i < A.Width; i++)
            {
                for(int j = 0; j < B.Height; j++)
                {
                    for(int k = 0; k < B.Width; k++)
                    {
                        Result.AddW(i, j, 0, A.GetW(i, k, 0) * B.GetW(k, j, 0));
                    }
                }
            }
            return Result;
        }

        ///<summary>Вычисляет матрицу Грамма (Граммиан) для тензора.</summary>
        ///<param name="F">Входной тензор.</param>
        public static Tensor Gram_Matrix(Tensor F)
        {
            var Ft = Flat(F);
            return MatMul2D(Transpose2D(Ft), Ft);
        }

    }

    ///<summary>Предоставляет методы для преобразований между System.Drawing.Bitmap и классом Tensor.</summary>
    public static class IOConverters
    {

        ///<summary>Преобразует изображение в тензор.</summary>
        ///<param name="Image">Изображение.</param>
        public static unsafe Tensor ImageToTensor(Bitmap Image)
        {
            var Result = new Tensor(Image.Width, Image.Height, 3, true);
            var BD = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var w = Image.Width;
            Parallel.For(0, Image.Height, (int y) =>
            {
                var Addr = (byte*)(BD.Scan0.ToInt32() + BD.Stride * y);
                for(int x = 0; x < w; x++)
                {
                    Result.SetW(x, y, 0, (float)(*Addr) - 103.939f);
                    Addr += 1;
                    Result.SetW(x, y, 1, (float)(*Addr) - 116.779f);
                    Addr += 1;
                    Result.SetW(x, y, 2, (float)(*Addr) - 123.68f);
                    Addr+=1;
                }
            });
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
                    var r = img.GetW(x, y, 2) + 123.68f;
                    if(r < 0.0f)
                    {
                        r = 0.0f;
                    }
                    if(r > 255.0f)
                    {
                        r = 255.0f;
                    }
                    var g = img.GetW(x, y, 1) + 116.779f;
                    if(g < 0.0f)
                    {
                        g = 0.0f;
                    }
                    if(g > 255.0f)
                    {
                        g = 255.0f;
                    }
                    var b = img.GetW(x, y, 0) + 103.939f;
                    if(b < 0.0f)
                    {
                        b = 0.0f;
                    }
                    if(b > 255.0f)
                    {
                        b = 255.0f;
                    }
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