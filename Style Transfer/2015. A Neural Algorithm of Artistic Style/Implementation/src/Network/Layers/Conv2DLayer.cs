//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralArt
{

    // В архитектуре используемой нейросети (VGG-16) все свёртки имеют фильтры размером 3x3. Для оптимизации,
    // эти значения интегрированы в код.

    ///<summary>Свёрточный слой.</summary>
    public sealed class ConvLayer : Layer
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

        ///<summary>Фильтры.</summary>
        public Tensor[] Filters
        {
            get;
            set;
        }

        ///<summary>Смещения.</summary>
        public Tensor Biases
        {
            get;
            set;
        }

        ///<summary>Инициализирует свёрточный слой с заданными фильтрами и смещениями.</summary>
        ///<param name="filters">Фильтры.</param>
        ///<param name="biases">Смещения.</param>
        public ConvLayer(Tensor[] filters, Tensor biases)
        {
            this.InputWidth = 0;
            this.InputHeight = 0;
            this.InputDepth = 0;
            this.OutputWidth = 0;
            this.OutputHeight = 0;
            this.OutputDepth = filters.Length;
            this.Filters = filters;
            this.Biases = biases;
        }

        ///<summary>Прямое распространение через свёрточный слой.</summary>
        ///<param name="input">Входные данные.</param>
        public Tensor Forward(Tensor input)
        {
            this.InputWidth = input.Width;
            this.InputHeight = input.Height;
            this.InputDepth = input.Depth;
            this.OutputWidth = input.Width;
            this.OutputHeight = input.Height;
            this.Input = input;
            var Result = new Tensor(this.OutputWidth, this.OutputHeight, this.OutputDepth, true);
            Parallel.For(0, this.OutputDepth, (int d) =>
            {
                for(int ay = 0; ay < this.OutputHeight; ay++)
                {
                    var y = ay - 1;
                    var f = this.Filters[d];
                    for(var ax = 0; ax < this.OutputWidth; ax++)
                    {
                        var x = ax - 1;
                        var a = 0.0;
                        for(byte fy = 0; fy < 3; fy++)
                        {
                            var oy = y + fy;
                            for(byte fx = 0; fx < 3; fx++)
                            {
                                var ox = x + fx;
                                if((oy >= 0) && (oy < this.InputHeight) && (ox >= 0) && (ox < this.InputWidth))
                                {
                                    for(var fd = 0; fd < f.Depth; fd++)
                                    {
                                        a += f.W[((3 * fy) + fx) * f.Depth + fd] * input.W[((this.InputWidth * oy) + ox) * input.Depth + fd];
                                    }
                                }
                            }
                        }
                        a += this.Biases.W[d];
                        Result.SetW(ax, ay, d, (float)a);
                    }
                }
            });
            this.Output = Result;
            return Result;
        }

        ///<summary>Обратное распространение (градиента) через свёрточный слой.</summary>
        public void Backward()
        {
            var V = this.Input;
            V.DW = new float[V.W.Length];
            var inputWidth = V.Width;
            var inputHeight = V.Height;
            System.Threading.Tasks.Parallel.For(0, this.OutputDepth, (int d) =>
            {
                var f = this.Filters[d];
                var x = -1;
                var y = -1;
                for(var ay = 0; ay < this.OutputHeight; y += 1, ay++)
                {
                    x = -1;
                    for(var ax = 0; ax < this.OutputWidth; x += 1, ax++)
                    {
                        var chain_grad = this.Output.GetDW(ax, ay, d);
                        for(byte fy = 0; fy < 3; fy++)
                        {
                            var oy = y + fy;
                            for(byte fx = 0; fx < 3; fx++)
                            {
                                var ox = x + fx;
                                if((oy >= 0) && (oy < inputHeight) && (ox >= 0) && (ox < inputWidth))
                                {
                                    for(var fd = 0; fd < f.Depth; fd++)
                                    {
                                        var ix1 = ((inputWidth * oy) + ox) * V.Depth + fd;
                                        var ix2 = ((3 * fy) + fx) * f.Depth + fd;
                                        V.DW[ix1] += f.W[ix2] * chain_grad;
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

    }

}