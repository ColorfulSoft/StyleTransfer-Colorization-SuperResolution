//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;

namespace NeuralArt
{

    ///<summary>Оптимизатор Adam.</summary>
    public static class Adam
    {

        public static float c
        {
            get;
            set;
        }

        public static float eps
        {
            get;
            set;
        }

        public static float beta1
        {
            get;
            set;
        }

        public static float beta2
        {
            get;
            set;
        }

        private static float accBeta1
        {
            get;
            set;
        }

        private static float accBeta2
        {
            get;
            set;
        }

        private static float oneMinusBeta1
        {
            get;
            set;
        }

        private static float oneMinusBeta2
        {
            get;
            set;
        }

        private static float[] accumulatedFirstMoment
        {
            get;
            set;
        }

        private static float[] accumulatedSecondMoment
        {
            get;
            set;
        }

        ///<summary>Конструктор Adam. Инициализирует все поля.</summary>
        static Adam()
        {
            c = -10f;
            eps = 1e-8f;
            beta1 = 0.9f;
            beta2 = 0.999f;
            accBeta1 = beta1;
            accBeta2 = beta2;
            oneMinusBeta1 = 1 - beta1;
            oneMinusBeta2 = 1 - beta2;
        }

        ///<summary>Выполняет обновление параметров по их градиентам.</summary>
        public static void Train(Tensor Value)
        {
            var oneMinusAccBeta1 = 1 - accBeta1;
            var oneMinusAccBeta2 = 1 - accBeta2;
            if(accumulatedFirstMoment == null)
            {
                accumulatedFirstMoment = new float[Value.DW.Length];
            }
            if(accumulatedSecondMoment == null)
            {
                accumulatedSecondMoment = new float[Value.DW.Length];
            }
            var gradient = Value.DW;
            var firstMoment = accumulatedFirstMoment;
            var secondMoment = accumulatedSecondMoment;
            for(int i = 0; i < Value.W.Length; i++)
            {
                var newFirstMoment = beta1 * firstMoment[i] + oneMinusBeta1 * gradient[i];
                var newSecondMoment = beta2 * secondMoment[i] + oneMinusBeta2 * gradient[i] * gradient[i];
                var biasCorrectedFirstMoment = newFirstMoment / oneMinusAccBeta1;
                var biasCorrectedSecondMoment = newSecondMoment / oneMinusAccBeta2;
                accumulatedFirstMoment[i] = newFirstMoment;
                accumulatedSecondMoment[i] = newSecondMoment;
                var newValue = c * (biasCorrectedFirstMoment / (eps + System.Math.Sqrt(biasCorrectedSecondMoment))) + Value.W[i];
                Value.W[i] = (float)newValue;
            }
            accBeta1 = accBeta1 * beta1;
            accBeta2 = accBeta2 * beta2;
        }
    }

}