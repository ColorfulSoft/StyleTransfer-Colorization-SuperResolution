//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Реализация кодера.

using System;
using System.IO;

namespace NeuralArt
{

    ///<summary>Метод передачи стиля.</summary>
    public enum TransferMethod
    {
        AdaIN,
        WCT
    }

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
                var percent = (byte)(TotalSteps / 15f);
                if(percent != LastPercent)
                {
                    OnStepDone(percent);
                    LastPercent = percent;
                }
            }
        }

        public static Tensor Stylize(Encoder Encoder1,
                                     Decoder Decoder1,
                                     Encoder Encoder2,
                                     Decoder Decoder2,
                                     Encoder Encoder3,
                                     Decoder Decoder3,
                                     Encoder Encoder4,
                                     Decoder Decoder4,
                                     Encoder Encoder5,
                                     Decoder Decoder5,
                                     Tensor Content,
                                     Tensor Style,
                                     TransferMethod Method = TransferMethod.WCT,
                                     float alpha = 0.6f)
        {
            Encoder1.Step += FixStep;
            Decoder1.Step += FixStep;
            Encoder2.Step += FixStep;
            Decoder2.Step += FixStep;
            Encoder3.Step += FixStep;
            Decoder3.Step += FixStep;
            Encoder4.Step += FixStep;
            Decoder4.Step += FixStep;
            Encoder5.Step += FixStep;
            Decoder5.Step += FixStep;
            TotalSteps = 0f;
            LastPercent = 0;
            if(Method == TransferMethod.WCT)
            {
                // WCT 5
                var Conv5 = Decoder5.Decode(Layers.WCT(Encoder5.Encode(Content), Encoder5.Encode(Style), alpha));
                // WCT 4
                var Conv4 = Decoder4.Decode(Layers.WCT(Encoder4.Encode(Conv5), Encoder4.Encode(Style), alpha));
                // WCT 3
                var Conv3 = Decoder3.Decode(Layers.WCT(Encoder3.Encode(Conv4), Encoder3.Encode(Style), alpha));
                // WCT 2
                var Conv2 = Decoder2.Decode(Layers.WCT(Encoder2.Encode(Conv3), Encoder2.Encode(Style), alpha));
                // WCT 1
                var Conv1 = Decoder1.Decode(Layers.WCT(Encoder1.Encode(Conv2), Encoder1.Encode(Style), alpha));
                // Uninstall
                Encoder1.Step -= FixStep;
                Decoder1.Step -= FixStep;
                Encoder2.Step -= FixStep;
                Decoder2.Step -= FixStep;
                Encoder3.Step -= FixStep;
                Decoder3.Step -= FixStep;
                Encoder4.Step -= FixStep;
                Decoder4.Step -= FixStep;
                Encoder5.Step -= FixStep;
                Decoder5.Step -= FixStep;
                return Conv1;
            }
            else
            {
                // AdaIN 5
                var Conv5 = Decoder5.Decode(Layers.AdaIN(Encoder5.Encode(Content), Encoder5.Encode(Style), alpha));
                // AdaIN 4
                var Conv4 = Decoder4.Decode(Layers.AdaIN(Encoder4.Encode(Conv5), Encoder4.Encode(Style), alpha));
                // AdaIN 3
                var Conv3 = Decoder3.Decode(Layers.AdaIN(Encoder3.Encode(Conv4), Encoder3.Encode(Style), alpha));
                // AdaIN 2
                var Conv2 = Decoder2.Decode(Layers.AdaIN(Encoder2.Encode(Conv3), Encoder2.Encode(Style), alpha));
                // AdaIN 1
                var Conv1 = Decoder1.Decode(Layers.AdaIN(Encoder1.Encode(Conv2), Encoder1.Encode(Style), alpha));
                // Uninstall
                Encoder1.Step -= FixStep;
                Decoder1.Step -= FixStep;
                Encoder2.Step -= FixStep;
                Decoder2.Step -= FixStep;
                Encoder3.Step -= FixStep;
                Decoder3.Step -= FixStep;
                Encoder4.Step -= FixStep;
                Decoder4.Step -= FixStep;
                Encoder5.Step -= FixStep;
                Decoder5.Step -= FixStep;
                return Conv1;
            }
        }

    }

}