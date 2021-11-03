//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2021. All Rights reserved.
//*************************************************************************************************

using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;

namespace ColorfulSoft.NeuralArt.AdaIN
{

    internal static unsafe class AdaIN
    {

        ///<summary>
        /// Loads data from the stream using BinaryReader.
        ///</summary>
        ///<param name="reader">Source.</param>
        ///<param name="shape">Shape of the tensor.</param>
        ///<returns>Tensor with data.</returns>
        public unsafe static Tensor LoadTensor(BinaryReader reader, params int[] shape)
        {
            var t = new Tensor(shape);
            var n = t.Numel;
            var container = t.Data;
            for(int i = 0; i < n; ++i)
            {
                container[i] = HalfHelper.HalfToSingle(reader.ReadUInt16());
            }
            return t;
        }

        ///<summary>
        /// Parameters.
        ///</summary>
        public static Dictionary<string, Tensor> Parameters
        {

            get;

            private set;

        }

        ///<summary>
        /// Progress value.
        ///</summary>
        private static float __Progress;

        ///<summary>
        /// Progress step.
        ///</summary>
        private const float Step = 100f / 27f;

        ///<summary>
        /// Progress bar changer.
        ///</summary>
        public delegate void ProgressDelegate(float Percent);

        ///<summary>
        /// Occurs when the coloring progress changes.
        ///</summary>
        public static event ProgressDelegate Progress;

        ///<summary>
        /// Converts Bitmap to Tensor.
        ///</summary>
        ///<param name="bmp">Source.</param>
        ///<returns>Tensor with pixels.</returns>
        public static Tensor Image2Tensor(Bitmap bmp)
        {
            var t = new Tensor(3, bmp.Height, bmp.Width);
            var pt = t.Data;
            for(int y = 0; y < bmp.Height; ++y)
            {
                for(int x = 0; x < bmp.Width; ++x)
                {
                    var c = bmp.GetPixel(x, y);
                    pt[y * bmp.Width + x] = c.B - 103.939f;
                    pt[(bmp.Height + y) * bmp.Width + x] = c.G - 116.779f;
                    pt[(2 * bmp.Height + y) * bmp.Width + x] = c.R - 123.68f;
                }
            }
            return t;
        }

        ///<summary>
        /// Converts Tensor to Bitmap.
        ///</summary>
        ///<param name="t">Source.</param>
        ///<returns>Bitmap with pixels from Tensor t.</returns>
        public static Bitmap Tensor2Image(Tensor t)
        {
            var bmp = new Bitmap(t.Shape[2], t.Shape[1]);
            for(int y = 0; y < t.Shape[1]; ++y)
            {
                for(int x = 0; x < t.Shape[2]; ++x)
                {
                    bmp.SetPixel(x, y, Color.FromArgb((byte)Math.Min(Math.Max(t.Data[y * t.Shape[2] + x] * 255f, 0f), 255f),
                                                      (byte)Math.Min(Math.Max(t.Data[(t.Shape[1] + y) * t.Shape[2] + x] * 255f, 0f), 255f),
                                                      (byte)Math.Min(Math.Max(t.Data[(2 * t.Shape[1] + y) * t.Shape[2] + x] * 255f, 0f), 255f)));
                }
            }
            return bmp;
        }

        /// <summary>
        /// VGG19 encoder.
        /// </summary>
        /// <param name="x">Input image.</param>
        /// <returns>Tensor with features.</returns>
        public static Tensor Encode(Tensor x)
        {
            var y = Functional.ReLU_(Functional.Conv2d(x, Parameters["encoder.0.weight"], Parameters["encoder.0.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["encoder.1.weight"], Parameters["encoder.1.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.MaxPool2d(y, 2, 2, 2, 2, 0, 0, 1, 1);
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["encoder.2.weight"], Parameters["encoder.2.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["encoder.3.weight"], Parameters["encoder.3.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.MaxPool2d(y, 2, 2, 2, 2, 0, 0, 1, 1);
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["encoder.4.weight"], Parameters["encoder.4.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["encoder.5.weight"], Parameters["encoder.5.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["encoder.6.weight"], Parameters["encoder.6.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["encoder.7.weight"], Parameters["encoder.7.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.MaxPool2d(y, 2, 2, 2, 2, 0, 0, 1, 1);
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["encoder.8.weight"], Parameters["encoder.8.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            return y;
        }

        /// <summary>
        /// Decodes vgg feature maps to image.
        /// </summary>
        /// <param name="x">Tensor with features.</param>
        /// <returns>Tensor with pixels.</returns>
        public static Tensor Decode(Tensor x)
        {
            var y = Functional.ReLU_(Functional.Conv2d(x, Parameters["decoder.0.weight"], Parameters["decoder.0.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.NearestUpsample2d(y, 2, 2);
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["decoder.1.weight"], Parameters["decoder.1.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["decoder.2.weight"], Parameters["decoder.2.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["decoder.3.weight"], Parameters["decoder.3.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["decoder.4.weight"], Parameters["decoder.4.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.NearestUpsample2d(y, 2, 2);
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["decoder.5.weight"], Parameters["decoder.5.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["decoder.6.weight"], Parameters["decoder.6.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.NearestUpsample2d(y, 2, 2);
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["decoder.7.weight"], Parameters["decoder.7.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            y = Functional.ReLU_(Functional.Conv2d(y, Parameters["decoder.8.weight"], Parameters["decoder.8.bias"], 1, 1, 1, 1, 1, 1, 1, 1, 1));
            __Progress += Step;
            if(Progress != null)
            {
                Progress(Math.Min(Math.Max(__Progress, 0), 100));
            }
            return y;
        }

        /// <summary>
        /// Performs artistic image stylization.
        /// </summary>
        /// <param name="content">Content image.</param>
        /// <param name="style">Style image.</param>
        /// <returns>Stylized image.</returns>
        public static Bitmap Stylize(Bitmap content, Bitmap style)
        {
            __Progress = 0f;
            var c = Encode(Image2Tensor(content));
            var s = Encode(Image2Tensor(style));
            return Tensor2Image(Decode(Functional.AdaIN2d(c, s)));
        }

        public static void Initialize()
        {
            var br = new BinaryReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Encoder.hmodel"));
            Parameters = new Dictionary<string, Tensor>();
            Parameters.Add("encoder.0.weight", LoadTensor(br, 64, 3, 3, 3));
            Parameters.Add("encoder.0.bias", LoadTensor(br, 64));
            Parameters.Add("encoder.1.weight", LoadTensor(br, 64, 64, 3, 3));
            Parameters.Add("encoder.1.bias", LoadTensor(br, 64));
            Parameters.Add("encoder.2.weight", LoadTensor(br, 128, 64, 3, 3));
            Parameters.Add("encoder.2.bias", LoadTensor(br, 128));
            Parameters.Add("encoder.3.weight", LoadTensor(br, 128, 128, 3, 3));
            Parameters.Add("encoder.3.bias", LoadTensor(br, 128));
            Parameters.Add("encoder.4.weight", LoadTensor(br, 256, 128, 3, 3));
            Parameters.Add("encoder.4.bias", LoadTensor(br, 256));
            Parameters.Add("encoder.5.weight", LoadTensor(br, 256, 256, 3, 3));
            Parameters.Add("encoder.5.bias", LoadTensor(br, 256));
            Parameters.Add("encoder.6.weight", LoadTensor(br, 256, 256, 3, 3));
            Parameters.Add("encoder.6.bias", LoadTensor(br, 256));
            Parameters.Add("encoder.7.weight", LoadTensor(br, 256, 256, 3, 3));
            Parameters.Add("encoder.7.bias", LoadTensor(br, 256));
            Parameters.Add("encoder.8.weight", LoadTensor(br, 512, 256, 3, 3));
            Parameters.Add("encoder.8.bias", LoadTensor(br, 512));
            br.Close();
            br = new BinaryReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Decoder.hmodel"));
            Parameters.Add("decoder.0.weight", LoadTensor(br, 256, 512, 3, 3));
            Parameters.Add("decoder.0.bias", LoadTensor(br, 256));
            Parameters.Add("decoder.1.weight", LoadTensor(br, 256, 256, 3, 3));
            Parameters.Add("decoder.1.bias", LoadTensor(br, 256));
            Parameters.Add("decoder.2.weight", LoadTensor(br, 256, 256, 3, 3));
            Parameters.Add("decoder.2.bias", LoadTensor(br, 256));
            Parameters.Add("decoder.3.weight", LoadTensor(br, 256, 256, 3, 3));
            Parameters.Add("decoder.3.bias", LoadTensor(br, 256));
            Parameters.Add("decoder.4.weight", LoadTensor(br, 128, 256, 3, 3));
            Parameters.Add("decoder.4.bias", LoadTensor(br, 128));
            Parameters.Add("decoder.5.weight", LoadTensor(br, 128, 128, 3, 3));
            Parameters.Add("decoder.5.bias", LoadTensor(br, 128));
            Parameters.Add("decoder.6.weight", LoadTensor(br, 64, 128, 3, 3));
            Parameters.Add("decoder.6.bias", LoadTensor(br, 64));
            Parameters.Add("decoder.7.weight", LoadTensor(br, 64, 64, 3, 3));
            Parameters.Add("decoder.7.bias", LoadTensor(br, 64));
            Parameters.Add("decoder.8.weight", LoadTensor(br, 3, 64, 3, 3));
            Parameters.Add("decoder.8.bias", LoadTensor(br, 3));
            br.Close();
        }

    }

}