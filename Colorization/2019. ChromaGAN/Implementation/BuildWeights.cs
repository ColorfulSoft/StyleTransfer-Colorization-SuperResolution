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
            var fs = File.Create("ChromaGAN.HModel");
            AddFileToStream(path + "0.bin", fs);
            AddFileToStream(path + "1.bin", fs);
            AddFileToStream(path + "2.bin", fs);
            AddFileToStream(path + "3.bin", fs);
            fs.Close();
        }

    }

}