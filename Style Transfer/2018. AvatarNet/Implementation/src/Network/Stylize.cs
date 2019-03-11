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
                var percent = (byte)(TotalSteps / 3f);
                if(percent != LastPercent)
                {
                    OnStepDone(percent);
                    LastPercent = percent;
                }
            }
        }

        public static Tensor Stylize(Encoder Encoder,
                                     Decoder Decoder,
                                     Tensor Content,
                                     Tensor Style)
        {
            Encoder.Step += FixStep;
            Decoder.Step += FixStep;
            TotalSteps = 0f;
            LastPercent = 0;
            // Transfer style
            var C = Encoder.Encode(Content);
            var S = Encoder.EncodeStyle(Style);
            var Result = Decoder.Decode(C, S);
            // Uninstall
            Encoder.Step -= FixStep;
            Decoder.Step -= FixStep;
            return Result;
        }

    }

}