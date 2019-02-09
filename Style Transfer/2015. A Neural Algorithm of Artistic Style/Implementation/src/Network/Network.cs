//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Loss - network. Нейросеть VGG - 16.</summary>
    public sealed class VGG16
    {

        ///<summary>Слои нейросети.</summary>
        private Layer[] Layers
        {
            get;
            set;
        }

        ///<summary>Матрица Грамма для стилевой карты признаков из первого блока нейросети.</summary>
        private Tensor Style1
        {
            get;
            set;
        }

        ///<summary>Матрица Грамма для стилевой карты признаков из второго блока нейросети.</summary>
        private Tensor Style2
        {
            get;
            set;
        }

        ///<summary>Матрица Грамма для стилевой карты признаков из третьего блока нейросети.</summary>
        private Tensor Style3
        {
            get;
            set;
        }

        ///<summary>Матрица Грамма для стилевой карты признаков из четвёртого блока нейросети.</summary>
        private Tensor Style4
        {
            get;
            set;
        }

        ///<summary>Матрица Грамма для стилевой карты признаков из пятого блока нейросети.</summary>
        private Tensor Style5
        {
            get;
            set;
        }

        ///<summary>Карта признаков контентного изображения.</summary>
        private Tensor Content
        {
            get;
            set;
        }

        ///<summary>Читает фильтры свёрточного слоя из потока через BinaryReader.</summary>
        ///<param name="n">Количество фильтров.</param>
        ///<param name="d">Глубина фильтров.</param>
        ///<param name="br">Оболочка для потока, из которого читаются данные.</param>
        private static Tensor[] LoadFilters(int n, int d, BinaryReader br)
        {
            var Result = new Tensor[n];
            for(int i = 0; i < n; i++)
            {
                Result[i] = new Tensor(3, 3, d, false);
                var f = Result[i];
                for(int z = 0; z < d; z++)
                {
                    for(byte y = 0; y < 3; y++)
                    {
                        for(byte x = 0; x < 3; x++)
                        {
                            f.SetW(x, y, z, br.ReadSingle());
                        }
                    }
                }
            }
            return Result;
        }

        ///<summary>Читает смещения свёрточного слоя из потока через BinaryReader.</summary>
        ///<param name="n">Количество значений.</param>
        ///<param name="br">Оболочка для потока, из которого читаются данные.</param>
        private static Tensor LoadBiases(int n, BinaryReader br)
        {
            var Result = new Tensor(1, 1, n, false);
            var f = Result.W;
            for(int i = 0; i < n; i++)
            {
                f[i] = br.ReadSingle();
            }
            return Result;
        }

        ///<summary>Делегат для события завершения итерации.</summary>
        public delegate void IterationHandler(int iter, float loss);

        ///<summary>Событие завершения итерации.</summary>
        public event IterationHandler OnIterationDone;

        ///<summary>Инициализирует Loss - нейросеть VGG19, считывая данные свёрточных слоёв из потока s.</summary>
        ///<param name="s">Поток, из которого будут считаны данные свёрточных слоёв (фильтры и смещения).</param>
        public VGG16(Stream s)
        {
            var br = new BinaryReader(s);
            this.Layers = new Layer[26];
            // block 1
            this.Layers[0] = new ConvLayer(LoadFilters(64, 3, br), LoadBiases(64, br));
            this.Layers[1] = new ReluLayer();
            this.Layers[2] = new ConvLayer(LoadFilters(64, 64, br), LoadBiases(64, br));
            this.Layers[3] = new ReluLayer();
            this.Layers[4] = new MaxPoolLayer();
            // block 2
            this.Layers[5] = new ConvLayer(LoadFilters(128, 64, br), LoadBiases(128, br));
            this.Layers[6] = new ReluLayer();
            this.Layers[7] = new ConvLayer(LoadFilters(128, 128, br), LoadBiases(128, br));
            this.Layers[8] = new ReluLayer();
            this.Layers[9] = new MaxPoolLayer();
            // block 3
            this.Layers[10] = new ConvLayer(LoadFilters(256, 128, br), LoadBiases(256, br));
            this.Layers[11] = new ReluLayer();
            this.Layers[12] = new ConvLayer(LoadFilters(256, 256, br), LoadBiases(256, br));
            this.Layers[13] = new ReluLayer();
            this.Layers[14] = new ConvLayer(LoadFilters(256, 256, br), LoadBiases(256, br));
            this.Layers[15] = new ReluLayer();
            this.Layers[16] = new MaxPoolLayer();
            // block 4
            this.Layers[17] = new ConvLayer(LoadFilters(512, 256, br), LoadBiases(512, br));
            this.Layers[18] = new ReluLayer();
            this.Layers[19] = new ConvLayer(LoadFilters(512, 512, br), LoadBiases(512, br));
            this.Layers[20] = new ReluLayer();
            this.Layers[21] = new ConvLayer(LoadFilters(512, 512, br), LoadBiases(512, br));
            this.Layers[22] = new ReluLayer();
            this.Layers[23] = new MaxPoolLayer();
            // block 5
            this.Layers[24] = new ConvLayer(LoadFilters(512, 512, br), LoadBiases(512, br));
            this.Layers[25] = new ReluLayer();
        }

        ///<summary>Прямое распространение через нейросеть.</summary>
        ///<param name="input">Входные данные.</param>
        public void Forward(Tensor input)
        {
            var act = this.Layers[0].Forward(input);
            for(byte i = 1; i < 26; i++)
            {
                act = this.Layers[i].Forward(act);
            }
        }

        // Стилевые слои: ReLU1_1, ReLU2_1, ReLU3_1, ReLU4_1, ReLU5_1.

        ///<summary>Фиксирует матрицы Грамма для стилевых признаков.</summary>
        ///<param name="S">Стилевое изображение.</param>
        public void FixStyle(Tensor S)
        {
            this.Forward(S);
            this.Style1 = Math.Gram_Matrix(this.Layers[1].Output);
            this.Style2 = Math.Gram_Matrix(this.Layers[6].Output);
            this.Style3 = Math.Gram_Matrix(this.Layers[11].Output);
            this.Style4 = Math.Gram_Matrix(this.Layers[18].Output);
            this.Style5 = Math.Gram_Matrix(this.Layers[25].Output);
        }

        // Контентный слой: ReLU4_2.

        ///<summary>Фиксирует контентные признаки.</summary>
        ///<param name="C">Контентное изображение.</param>
        public void FixContent(Tensor C)
        {
            this.Forward(C);
            this.Content = this.Layers[20].Output.Clone();
        }

        ///<summary>Обратное распространение ошибки(градиента) и её попутное вычисление.</summary>
        public float Backward()
        {
            this.Layers[25].Output.DW = new float[this.Layers[25].Output.W.Length];
            var Result = Losses.StyleLoss(this.Layers[25].Output, this.Style5, 200f);
            this.Layers[25].Backward();
            this.Layers[24].Backward();
            this.Layers[23].Backward();
            this.Layers[22].Backward();
            this.Layers[21].Backward();
            Result += Losses.ContentLoss(this.Layers[20].Output, this.Content, 1f);
            this.Layers[20].Backward();
            this.Layers[19].Backward();
            Result += Losses.StyleLoss(this.Layers[18].Output, this.Style4, 200f);
            this.Layers[18].Backward();
            this.Layers[17].Backward();
            this.Layers[16].Backward();
            this.Layers[15].Backward();
            this.Layers[14].Backward();
            this.Layers[13].Backward();
            this.Layers[12].Backward();
            Result += Losses.StyleLoss(this.Layers[11].Output, this.Style3, 200f);
            this.Layers[11].Backward();
            this.Layers[10].Backward();
            this.Layers[9].Backward();
            this.Layers[8].Backward();
            this.Layers[7].Backward();
            Result += Losses.StyleLoss(this.Layers[6].Output, this.Style2, 200f);
            this.Layers[6].Backward();
            this.Layers[5].Backward();
            this.Layers[4].Backward();
            this.Layers[3].Backward();
            this.Layers[2].Backward();
            Result += Losses.StyleLoss(this.Layers[1].Output, this.Style1, 200f);
            this.Layers[1].Backward();
            this.Layers[0].Backward();
            return Result;
        }

        ///<summary>Запускает итеративный процесс. Количество итераций: 100.</summary>
        ///<param name="X">Начальное изображение (холст).</param>
        public void StartIterativeProcess(Tensor X)
        {
            for(int i = 0; i < 100; i++)
            {
                this.Forward(X);
                var l = this.Backward();
                Adam.Train(X);
                if(OnIterationDone != null)
                {
                    OnIterationDone(i, l);
                }
            }
        }

    }

}
