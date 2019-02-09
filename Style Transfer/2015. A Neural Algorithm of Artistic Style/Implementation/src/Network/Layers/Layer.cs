//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;

namespace NeuralArt
{

    ///<summary>Интерфейс слоя нейросети.</summary>
    public interface Layer
    {

        ///<summary>Входные данные слоя.</summary>
        Tensor Input
        {
            get;
            set;
        }

        ///<summary>Выходные данные.</summary>
        Tensor Output
        {
            get;
            set;
        }

        ///<summary>Ширина входного тензора.</summary>
        int InputWidth
        {
            get;
            set;
        }

        ///<summary>Высота входного тензора.</summary>
        int InputHeight
        {
            get;
            set;
        }

        ///<summary>Глубина входного тензора.</summary>
        int InputDepth
        {
            get;
            set;
        }

        ///<summary>Ширина выходного тензора.</summary>
        int OutputWidth
        {
            get;
            set;
        }

        ///<summary>Высота выходного тензора.</summary>
        int OutputHeight
        {
            get;
            set;
        }

        ///<summary>Глубина выходного тензора.</summary>
        int OutputDepth
        {
            get;
            set;
        }

        ///<summary>Метод прямого распространения через слой.</summary>
        ///<param name="input">Входные данные.</param>
        Tensor Forward(Tensor input);

        ///<summary>Метод обратного распространения (градиента) через слой.</summary>
        void Backward();

    }

}