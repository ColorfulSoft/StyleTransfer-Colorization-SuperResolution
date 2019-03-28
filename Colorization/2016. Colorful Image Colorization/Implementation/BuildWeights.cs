using System;
using System.IO;

namespace BuildWeights
{

    public static class Program
    {

        private static void AddFileToStream(string fname, Stream s)
        {
            var f = File.OpenRead(fname);
            f.CopyTo(s);
            f.Close();
        }

        public static void Main()
        {
            var path = "Weights\\";
            var fs = File.Create("ColorfulImageColorization.model");
            AddFileToStream(path + "bw_conv1_1_0", fs);
            AddFileToStream(path + "bw_conv1_1_1", fs);
            AddFileToStream(path + "conv1_2_0", fs);
            AddFileToStream(path + "conv1_2_1", fs);
            AddFileToStream(path + "conv1_2norm_0", fs);
            AddFileToStream(path + "conv1_2norm_1", fs);
            AddFileToStream(path + "conv1_2norm_2", fs);
            AddFileToStream(path + "conv2_1_0", fs);
            AddFileToStream(path + "conv2_1_1", fs);
            AddFileToStream(path + "conv2_2_0", fs);
            AddFileToStream(path + "conv2_2_1", fs);
            AddFileToStream(path + "conv2_2norm_0", fs);
            AddFileToStream(path + "conv2_2norm_1", fs);
            AddFileToStream(path + "conv2_2norm_2", fs);
            AddFileToStream(path + "conv3_1_0", fs);
            AddFileToStream(path + "conv3_1_1", fs);
            AddFileToStream(path + "conv3_2_0", fs);
            AddFileToStream(path + "conv3_2_1", fs);
            AddFileToStream(path + "conv3_3_0", fs);
            AddFileToStream(path + "conv3_3_1", fs);
            AddFileToStream(path + "conv3_3norm_0", fs);
            AddFileToStream(path + "conv3_3norm_1", fs);
            AddFileToStream(path + "conv3_3norm_2", fs);
            AddFileToStream(path + "conv4_1_0", fs);
            AddFileToStream(path + "conv4_1_1", fs);
            AddFileToStream(path + "conv4_2_0", fs);
            AddFileToStream(path + "conv4_2_1", fs);
            AddFileToStream(path + "conv4_3_0", fs);
            AddFileToStream(path + "conv4_3_1", fs);
            AddFileToStream(path + "conv4_3norm_0", fs);
            AddFileToStream(path + "conv4_3norm_1", fs);
            AddFileToStream(path + "conv4_3norm_2", fs);
            AddFileToStream(path + "conv5_1_0", fs);
            AddFileToStream(path + "conv5_1_1", fs);
            AddFileToStream(path + "conv5_2_0", fs);
            AddFileToStream(path + "conv5_2_1", fs);
            AddFileToStream(path + "conv5_3_0", fs);
            AddFileToStream(path + "conv5_3_1", fs);
            AddFileToStream(path + "conv5_3norm_0", fs);
            AddFileToStream(path + "conv5_3norm_1", fs);
            AddFileToStream(path + "conv5_3norm_2", fs);
            AddFileToStream(path + "conv6_1_0", fs);
            AddFileToStream(path + "conv6_1_1", fs);
            AddFileToStream(path + "conv6_2_0", fs);
            AddFileToStream(path + "conv6_2_1", fs);
            AddFileToStream(path + "conv6_3_0", fs);
            AddFileToStream(path + "conv6_3_1", fs);
            AddFileToStream(path + "conv6_3norm_0", fs);
            AddFileToStream(path + "conv6_3norm_1", fs);
            AddFileToStream(path + "conv6_3norm_2", fs);
            AddFileToStream(path + "conv7_1_0", fs);
            AddFileToStream(path + "conv7_1_1", fs);
            AddFileToStream(path + "conv7_2_0", fs);
            AddFileToStream(path + "conv7_2_1", fs);
            AddFileToStream(path + "conv7_3_0", fs);
            AddFileToStream(path + "conv7_3_1", fs);
            AddFileToStream(path + "conv7_3norm_0", fs);
            AddFileToStream(path + "conv7_3norm_1", fs);
            AddFileToStream(path + "conv7_3norm_2", fs);
            AddFileToStream(path + "conv8_1_0", fs);
            AddFileToStream(path + "conv8_1_1", fs);
            AddFileToStream(path + "conv8_2_0", fs);
            AddFileToStream(path + "conv8_2_1", fs);
            AddFileToStream(path + "conv8_3_0", fs);
            AddFileToStream(path + "conv8_3_1", fs);
            AddFileToStream(path + "conv8_313_0", fs);
            AddFileToStream(path + "conv8_313_1", fs);
            AddFileToStream(path + "pts_in_hull", fs);
            AddFileToStream(path + "class8_ab_1", fs);
            fs.Close();
        }

    }

}