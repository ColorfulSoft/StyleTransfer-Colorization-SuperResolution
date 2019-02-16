using System;
using System.IO;

namespace BuildWeights
{

    public static class Program
    {

        public static void AddFileToStream(Stream fs, string fname)
        {
            var f = File.OpenRead(fname);
            f.CopyTo(fs);
            f.Close();
        }

        public static void Main()
        {
            Directory.CreateDirectory("Encoders\\");
            Directory.CreateDirectory("Decoders\\");
            var output = File.Create("Encoders\\vgg_normalised_conv1_1.model");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv1_1\\Conv0");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv1_1\\Conv1_1");
            output.Close();
            output = File.Create("Encoders\\vgg_normalised_conv2_1.model");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv2_1\\Conv0");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv2_1\\Conv1_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv2_1\\Conv1_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv2_1\\Conv2_1");
            output.Close();
            output = File.Create("Encoders\\vgg_normalised_conv3_1.model");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv3_1\\Conv0");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv3_1\\Conv1_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv3_1\\Conv1_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv3_1\\Conv2_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv3_1\\Conv2_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv3_1\\Conv3_1");
            output.Close();
            output = File.Create("Encoders\\vgg_normalised_conv4_1.model");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv0");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv1_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv1_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv2_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv2_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv3_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv3_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv3_3");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv3_4");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv4_1\\Conv4_1");
            output.Close();
            output = File.Create("Encoders\\vgg_normalised_conv5_1.model");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv0");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv1_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv1_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv2_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv2_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv3_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv3_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv3_3");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv3_4");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv4_1");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv4_2");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv4_3");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv4_4");
            AddFileToStream(output, "Weights\\Encoders\\vgg_normalised_conv5_1\\Conv5_1");
            output.Close();
            output = File.Create("Decoders\\feature_invertor_conv1_1.model");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv1_1\\Conv1_1");
            output.Close();
            output = File.Create("Decoders\\feature_invertor_conv2_1.model");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv2_1\\Conv2_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv2_1\\Conv1_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv2_1\\Conv1_1");
            output.Close();
            output = File.Create("Decoders\\feature_invertor_conv3_1.model");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv3_1\\Conv3_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv3_1\\Conv2_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv3_1\\Conv2_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv3_1\\Conv1_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv3_1\\Conv1_1");
            output.Close();
            output = File.Create("Decoders\\feature_invertor_conv4_1.model");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv4_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv3_4");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv3_3");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv3_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv3_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv2_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv2_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv1_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv4_1\\Conv1_1");
            output.Close();
            output = File.Create("Decoders\\feature_invertor_conv5_1.model");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv5_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv4_4");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv4_3");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv4_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv4_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv3_4");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv3_3");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv3_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv3_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv2_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv2_1");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv1_2");
            AddFileToStream(output, "Weights\\Decoders\\feature_invertor_conv5_1\\Conv1_1");
            output.Close();
        }

    }

}