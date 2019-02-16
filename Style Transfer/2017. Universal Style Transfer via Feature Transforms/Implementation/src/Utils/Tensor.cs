//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Описание типа трёхмерного массива (тензора) и некоторых методов для работы с ним.

using System;
using System.Threading.Tasks;

namespace NeuralArt
{

    ///<summary>Представляет тензор (трёхмерный массив) чисел типа Single (float).</summary>
    public sealed class Tensor
    {

        ///<summary>Значения.</summary>
        public float[] W
        {
            get;
            set;
        }

        ///<summary>Ширина.</summary>
        public int Width
        {
            get;
            set;
        }

        ///<summary>Высота.</summary>
        public int Height
        {
            get;
            set;
        }

        ///<summary>Глубина.</summary>
        public int Depth
        {
            get;
            set;
        }

        ///<summary>Инициализирует тензор (трёхмерный массив) с заданными размерами.</summary>
        ///<param name="w">Ширина тензора.</param>
        ///<param name="h">Высота тензора.</param>
        ///<param name="d">Глубина тензора.</param>
        public Tensor(int w, int h, int d)
        {
            this.W = new float[w * h * d];
            this.Width = w;
            this.Height = h;
            this.Depth = d;
        }

        ///<summary>Получает значение с заданными координатами.</summary>
        ///<param name="x">Координата X (По ширине).</param>
        ///<param name="y">Координата Y (По высоте).</param>
        ///<param name="z">Координата Z (По глубине).</param>
        public float Get(int x, int y, int z)
        {
            return this.W[((this.Width * y) + x) * this.Depth + z];
        }

        ///<summary>Устанавливает значение с заданными координатами.</summary>
        ///<param name="x">Координата X (По ширине).</param>
        ///<param name="y">Координата Y (По высоте).</param>
        ///<param name="z">Координата Z (По глубине).</param>
        ///<param name="v">Значение.</param>
        public void Set(int x, int y, int z, float v)
        {
            this.W[((this.Width * y) + x) * this.Depth + z] = v;
        }

        public float this[int x, int y = 0, int d = 0]
        {
            get
            {
                return this.Get(x, y, d);
            }
            set
            {
                this.Set(x, y, d, value);
            }
        }

        public Tensor Flat()
        {
            var Result = new Tensor(this.Width * this.Height, this.Depth, 1);
            Parallel.For(0, this.Depth, (int d) =>
            {
                int i = 0;
                for(int y = 0; y < this.Height; y++)
                {
                    for(int x = 0; x < this.Width; x++)
                    {
                        Result.Set(i, d, 0, this.Get(x, y, d));
                        i += 1;
                    }
                }
            });
            return Result;
        }

        public Tensor Transpose()
        {
            var Result = new Tensor(this.Height, this.Width, this.Depth);
            for(int d = 0; d < Result.Depth; d++)
            {
                Parallel.For(0, this.Height, (int y) =>
                {
                    for(int x = 0; x < this.Width; x++)
                    {
                        Result.Set(y, x, d, this.Get(x, y, d));
                    }
                });
            }
            return Result;
        }

        public Tensor Div(float v)
        {
            var Result = new Tensor(this.Width, this.Height, this.Depth);
            Parallel.For(0, this.Depth, (int d) =>
            {
                for(int y = 0; y < this.Height; y++)
                {
                    for(int x = 0; x < this.Width; x++)
                    {
                        Result.Set(x, y, d, this.Get(x, y, d) / v);
                    }
                }
            });
            return Result;
        }

        public Tensor Sqrt()
        {
            var Result = new Tensor(this.Width, this.Height, this.Depth);
            Parallel.For(0, this.Depth, (int d) =>
            {
                for(int y = 0; y < this.Height; y++)
                {
                    for(int x = 0; x < this.Width; x++)
                    {
                        Result.Set(x, y, d, (float)System.Math.Sqrt(this.Get(x, y, d)));
                    }
                }
            });
            return Result;
        }

        public Tensor Pow(float v)
        {
            var Result = new Tensor(this.Width, this.Height, this.Depth);
            Parallel.For(0, this.Depth, (int d) =>
            {
                for(int y = 0; y < this.Height; y++)
                {
                    for(int x = 0; x < this.Width; x++)
                    {
                        Result.Set(x, y, d, (float)System.Math.Pow(this.Get(x, y, d), v));
                    }
                }
            });
            return Result;
        }

        public static Tensor MatMul(Tensor A, Tensor B)
        {
            var Result = new Tensor(B.Width, A.Height, A.Depth);
            for(int n = 0; n < A.Depth; n++)
            {
                Parallel.For(0, A.Height, (int i) =>
                {
                    for (int j = 0; j < B.Width; j++)
                    {
                        for (int k = 0; k < B.Height; k++)
                        {
                            Result[j, i, n] += A[k, i, n] * B[j, k, n];
                        }
                    }
                });
            }
            return Result;
        }

        public Tensor HeightMean()
        {
            var Result = new Tensor(this.Height, 1, 1);
            var N = this.Width;
            Parallel.For(0, this.Height, (int y) =>
            {
                var Sum = 0.0;
                for(int x = 0; x < this.Width; x++)
                {
                    Sum += this.Get(x, y, 0);
                }
                Result[y] = (float)(Sum / N);
            });
            return Result;
        }

        public Tensor Slice1D(int start, int count)
        {
            var Result = new Tensor(count, 1, 1);
            for(int i = start; i < start + count; i++)
            {
                Result[i - start] = this[i];
            }
            return Result;
        }

        public Tensor Diag()
        {
            var Result = new Tensor(this.Width, this.Width, 1);
            for(int i = 0; i < this.Width; i++)
            {
                Result.Set(i, i, 0, this[i]);
            }
            return Result;
        }

        public Tensor SubHeight(Tensor Sub)
        {
            var Result = new Tensor(this.Width, this.Height, 1);
            Parallel.For(0, this.Height, (int y) =>
            {
                var V = Sub[y];
                for(int x = 0; x < this.Width; x++)
                {
                    Result[x, y] = this[x, y] - V;
                }
            });
            return Result;
        }

        public Tensor AddHeight(Tensor Add)
        {
            var Result = new Tensor(this.Width, this.Height, 1);
            Parallel.For(0, this.Height, (int y) =>
            {
                var V = Add[y];
                for(int x = 0; x < this.Width; x++)
                {
                    Result[x, y] = this[x, y] + V;
                }
            });
            return Result;
        }

        public Tensor Unflat(int Width, int Height)
        {
            var Result = new Tensor(Width, Height, this.Height);
            Parallel.For(0, this.Height, (int d) =>
            {
                int i = 0;
                for(int y = 0; y < Height; y++)
                {
                    for(int x = 0; x < Width; x++)
                    {
                        Result.Set(x, y, d, this[i, d]);
                        i += 1;
                    }
                }
            });
            return Result;
        }

    }

}