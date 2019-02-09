//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralArt
{

    // В архитектуре используемой нейросети (VGG-16) все пулинги имеют окна размером 2x2. Для оптимизации,
    // эти значения интегрированы в код.

    ///<summary>Слой максимального пулинга/подвыборки (Maximum Pooling).</summary>
    public sealed class MaxPoolLayer : Layer
    {

        ///<summary>Ширина входного тензора.</summary>
        public int InputWidth
        {
            get;
            set;
        }

        ///<summary>Высота входного тензора.</summary>
        public int InputHeight
        {
            get;
            set;
        }

        ///<summary>Глубина входного тензора.</summary>
        public int InputDepth
        {
            get;
            set;
        }

        ///<summary>Ширина выходного тензора.</summary>
        public int OutputWidth
        {
            get;
            set;
        }

        ///<summary>Высота выходного тензора.</summary>
        public int OutputHeight
        {
            get;
            set;
        }

        ///<summary>Глубина выходного тензора.</summary>
        public int OutputDepth
        {
            get;
            set;
        }

        ///<summary>Входные данные.</summary>
        public Tensor Input
        {
            get;
            set;
        }

        ///<summary>Выходные данные.</summary>
        public Tensor Output
        {
            get;
            set;
        }

        ///<summary>Индексы максимальных элементов по оси X.</summary>
        public int[] SwitchX
        {
            get;
            set;
        }

        ///<summary>Индексы максимальных элементов по оси Y.</summary>
        public int[] SwitchY
        {
            get;
            set;
        }

        ///<summary>Инициализирует слой максимального пулинга/подвыборки (Maximum Pooling).</summary>
        public MaxPoolLayer()
        {
            this.InputWidth = 0;
            this.InputHeight = 0;
            this.InputDepth = 0;
            this.OutputWidth = 0;
            this.OutputHeight = 0;
            this.OutputDepth = 0;
        }

        ///<summary>Прямое распространение через слой максимального пулинга/подвыборки (Maximum Pooling).</summary>
        ///<param name="input">Входные данные.</param>
        public Tensor Forward(Tensor input)
        {
            this.Input = input;
            this.InputWidth = input.Width;
            this.InputHeight = input.Height;
            this.InputDepth = input.Depth;
            this.OutputWidth = input.Width / 2;
            this.OutputHeight = input.Height / 2;
            this.OutputDepth = input.Depth;
            this.SwitchX = new int[this.OutputWidth * this.OutputHeight * this.OutputDepth];
            this.SwitchY = new int[this.OutputWidth * this.OutputHeight * this.OutputDepth];
            var A = new Tensor(this.OutputWidth, this.OutputHeight, this.OutputDepth, true);
            Parallel.For(0, this.OutputDepth, (int d) =>
            {
                for(int ax = 0; ax < this.OutputWidth; ax++)
                {
                    var x = 2 * ax;
                    for(int ay = 0; ay < this.OutputHeight;  ay++)
                    {
                        var y = 2 * ay; 
                        float a = float.MinValue;
                        var winx = -1;
                        var winy = -1;
                        for(byte fx = 0; fx < 2; fx++)
                        {
                            for(byte fy = 0; fy < 2; fy++)
                            {
                                var oy = y + fy;
                                var ox = x + fx;
                                if((oy >= 0) && (oy < input.Height) && (ox >= 0) && (ox < input.Width))
                                {
                                    var v = input.GetW(ox, oy, d);
                                    if (v > a)
                                    {
                                        a = v;
                                        winx = ox;
                                        winy = oy;
                                    }
                                }
                            }
                        }
                        int n = ((this.OutputWidth * ay) + ax) * this.OutputDepth + d;
                        this.SwitchX[n] = winx;
                        this.SwitchY[n] = winy;
                        A.SetW(ax, ay, d, a);
                    }
                }
            });
            this.Output = A;
            return A;
        }

        ///<summary>Обратное распространение (градиента) через слой максимального пулинга/подвыборки (Maximum Pooling).</summary>
        public void Backward()
        {
            var V = this.Input;
            V.DW = new float[V.W.Length];
            var A = this.Output;
            Parallel.For(0, this.OutputDepth, (int d) =>
            {
                for(int ax = 0; ax < this.OutputWidth; ax++)
                {
                    for(int ay = 0; ay < this.OutputHeight; ay++)
                    {
                        float a = A.GetDW(ax, ay, d);
                        int n = ((this.OutputWidth * ay) + ax) * this.OutputDepth + d;
                        V.AddDW(this.SwitchX[n], this.SwitchY[n], d, a);
                    }
                }
            });
        }

    }

}