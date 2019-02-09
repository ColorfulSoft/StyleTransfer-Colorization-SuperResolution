//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.Threading;
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

        ///<summary>Градиенты.</summary>
        public float[] DW
        {
            get;
            set;
        }

        ///<summary>Значение, указывающее, участвует ли данный тензор в обратном распространении ошибки.</summary>
        public bool Trainable
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
        ///<param name="delta">Значение, указывающее, будет ли тензор участвовать в обратном распространении ошибки. true - будет, false - не будет.</param>
        public Tensor(int w, int h, int d, bool delta)
        {
            this.W = new float[w * h * d];
            if(delta == true)
            {
                this.DW = new float[w * h * d];
            }
            this.Width = w;
            this.Height = h;
            this.Depth = d;
            this.Trainable = delta;
        }

        ///<summary>Получает значение с заданными координатами.</summary>
        ///<param name="x">Координата X (По ширине).</param>
        ///<param name="y">Координата Y (По высоте).</param>
        ///<param name="z">Координата Z (По глубине).</param>
        public float GetW(int x, int y, int z)
        {
            return this.W[((this.Width * y) + x) * this.Depth + z];
        }

        ///<summary>Получает градиент с заданными координатами.</summary>
        ///<param name="x">Координата X (По ширине).</param>
        ///<param name="y">Координата Y (По высоте).</param>
        ///<param name="z">Координата Z (По глубине).</param>
        public float GetDW(int x, int y, int z)
        {
            return this.DW[((this.Width * y) + x) * this.Depth + z];
        }

        ///<summary>Устанавливает значение с заданными координатами.</summary>
        ///<param name="x">Координата X (По ширине).</param>
        ///<param name="y">Координата Y (По высоте).</param>
        ///<param name="z">Координата Z (По глубине).</param>
        ///<param name="v">Значение.</param>
        public void SetW(int x, int y, int z, float v)
        {
            this.W[((this.Width * y) + x) * this.Depth + z] = v;
        }

        ///<summary>Устанавливает градиент с заданными координатами.</summary>
        ///<param name="x">Координата X (По ширине).</param>
        ///<param name="y">Координата Y (По высоте).</param>
        ///<param name="z">Координата Z (По глубине).</param>
        ///<param name="v">Значение.</param>
        public void SetDW(int x, int y, int z, float v)
        {
            this.DW[((this.Width * y) + x) * this.Depth + z] = v;
        }

        ///<summary>Прибавляет значение к элементу тензора с заданными координатами.</summary>
        ///<param name="x">Координата X (По ширине).</param>
        ///<param name="y">Координата Y (По высоте).</param>
        ///<param name="z">Координата Z (По глубине).</param>
        ///<param name="v">Значение.</param>
        public void AddW(int x, int y, int z, float v)
        {
            this.W[((this.Width * y) + x) * this.Depth + z] += v;
        }

        ///<summary>Прибавляет значение к градиенту тензора с заданными координатами.</summary>
        ///<param name="x">Координата X (По ширине).</param>
        ///<param name="y">Координата Y (По высоте).</param>
        ///<param name="z">Координата Z (По глубине).</param>
        ///<param name="v">Значение.</param>
        public void AddDW(int x, int y, int z, float v)
        {
            this.DW[((this.Width * y) + x) * this.Depth + z] += v;
        }

        ///<summary>Возвращает копию текущего тензора.</summary>
        public Tensor Clone()
        {
            var PNum = Environment.ProcessorCount;
            var Result = new Tensor(this.Width, this.Height, this.Depth, true);
            var TaskPart = this.W.Length / PNum;
            Parallel.For(0, PNum, (int p) =>
            {
                for(int i = p * TaskPart; i < (p + 1) * TaskPart; i++)
                {
                    Result.W[i] = this.W[i];
                }
            });
            for(int i = PNum * TaskPart; i < Result.W.Length; i++)
            {
                Result.W[i] = this.W[i];
            }
            return Result;
        }

    }

}