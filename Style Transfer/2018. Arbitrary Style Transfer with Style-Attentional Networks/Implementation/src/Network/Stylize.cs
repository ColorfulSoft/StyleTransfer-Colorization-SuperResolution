//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Реализация кодера.

using System;
using System.IO;

namespace NeuralArt
{

    public static class StyleTransfer
    {

        public delegate void ProgressHandler(byte Percent);

        public static event ProgressHandler OnStepDone;

        private static float TotalSteps
        {
            get;
            set;
        }

        private static byte LastPercent
        {
            get;
            set;
        }

        private static void FixStep(float val)
        {
            TotalSteps += val;
            if(OnStepDone != null)
            {
                var percent = (byte)(TotalSteps / 4f);
                if(percent != LastPercent)
                {
                    OnStepDone(percent);
                    LastPercent = percent;
                }
            }
        }

        public static Tensor Stylize(Encoder Encoder,
                                     Decoder Decoder,
                                     SANet SANet,
                                     Tensor Content,
                                     Tensor Style)
        {
            Encoder.Step += FixStep;
            Decoder.Step += FixStep;
            SANet.Step += FixStep;
            TotalSteps = 0f;
            LastPercent = 0;
            var C = Encoder.Encode(Content);
            var S = Encoder.Encode(Style);
            var CS = SANet.Stylize(C.Item1, S.Item1, C.Item2, S.Item2);
            var Result = Decoder.Decode(CS);
            // Uninstall
            Encoder.Step -= FixStep;
            Decoder.Step -= FixStep;
            SANet.Step -= FixStep;
            return Result;
        }

    }

}