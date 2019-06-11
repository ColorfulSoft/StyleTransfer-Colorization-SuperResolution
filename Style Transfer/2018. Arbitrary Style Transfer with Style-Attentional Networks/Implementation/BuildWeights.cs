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
            var output = File.Create("vgg_normalised.model");
            AddFileToStream(output, "Weights\\vgg_normalised\\0_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\0_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\2_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\2_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\5_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\5_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\9_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\9_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\12_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\12_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\16_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\16_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\19_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\19_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\22_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\22_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\25_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\25_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\29_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\29_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\32_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\32_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\35_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\35_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\38_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\38_bias");
            AddFileToStream(output, "Weights\\vgg_normalised\\42_weight");
            AddFileToStream(output, "Weights\\vgg_normalised\\42_bias");
            output.Close();
            output = File.Create("SANet.model");
            AddFileToStream(output, "Weights\\SANet\\sanet4_1_f_weight");
            AddFileToStream(output, "Weights\\SANet\\sanet4_1_f_bias");
            AddFileToStream(output, "Weights\\SANet\\sanet4_1_g_weight");
            AddFileToStream(output, "Weights\\SANet\\sanet4_1_g_bias");
            AddFileToStream(output, "Weights\\SANet\\sanet4_1_h_weight");
            AddFileToStream(output, "Weights\\SANet\\sanet4_1_h_bias");
            AddFileToStream(output, "Weights\\SANet\\sanet4_1_out_conv_weight");
            AddFileToStream(output, "Weights\\SANet\\sanet4_1_out_conv_bias");
            AddFileToStream(output, "Weights\\SANet\\sanet5_1_f_weight");
            AddFileToStream(output, "Weights\\SANet\\sanet5_1_f_bias");
            AddFileToStream(output, "Weights\\SANet\\sanet5_1_g_weight");
            AddFileToStream(output, "Weights\\SANet\\sanet5_1_g_bias");
            AddFileToStream(output, "Weights\\SANet\\sanet5_1_h_weight");
            AddFileToStream(output, "Weights\\SANet\\sanet5_1_h_bias");
            AddFileToStream(output, "Weights\\SANet\\sanet5_1_out_conv_weight");
            AddFileToStream(output, "Weights\\SANet\\sanet5_1_out_conv_bias");
            AddFileToStream(output, "Weights\\SANet\\merge_conv_weight");
            AddFileToStream(output, "Weights\\SANet\\merge_conv_bias");
            output.Close();
            output = File.Create("decoder.model");
            AddFileToStream(output, "Weights\\decoder\\1_weight");
            AddFileToStream(output, "Weights\\decoder\\1_bias");
            AddFileToStream(output, "Weights\\decoder\\5_weight");
            AddFileToStream(output, "Weights\\decoder\\5_bias");
            AddFileToStream(output, "Weights\\decoder\\8_weight");
            AddFileToStream(output, "Weights\\decoder\\8_bias");
            AddFileToStream(output, "Weights\\decoder\\11_weight");
            AddFileToStream(output, "Weights\\decoder\\11_bias");
            AddFileToStream(output, "Weights\\decoder\\14_weight");
            AddFileToStream(output, "Weights\\decoder\\14_bias");
            AddFileToStream(output, "Weights\\decoder\\18_weight");
            AddFileToStream(output, "Weights\\decoder\\18_bias");
            AddFileToStream(output, "Weights\\decoder\\21_weight");
            AddFileToStream(output, "Weights\\decoder\\21_bias");
            AddFileToStream(output, "Weights\\decoder\\25_weight");
            AddFileToStream(output, "Weights\\decoder\\25_bias");
            AddFileToStream(output, "Weights\\decoder\\28_weight");
            AddFileToStream(output, "Weights\\decoder\\28_bias");
            output.Close();
        }

    }

}