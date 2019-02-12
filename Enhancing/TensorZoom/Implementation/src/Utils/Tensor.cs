//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

//-> Описание типа трёхмерного массива (тензора) и некоторых методов для работы с ним.

using System;

namespace NeuralEnhance
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

    }

}