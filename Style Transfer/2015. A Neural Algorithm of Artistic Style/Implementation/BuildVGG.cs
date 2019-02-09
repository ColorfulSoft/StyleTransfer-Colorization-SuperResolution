//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.IO;

namespace BuildVGG
{

    public static class Program
    {

        public static void AddFileToStream(string fname, Stream dest)
        {
            var fs = File.OpenRead(fname);
            fs.CopyTo(dest);
            fs.Close();
        }

        public static void Main()
        {
            var output = File.Create("Net.Model");
            AddFileToStream("VGG-16\\Conv1_1_Filters", output);
            AddFileToStream("VGG-16\\Conv1_1_Biases", output);
            AddFileToStream("VGG-16\\Conv1_2_Filters", output);
            AddFileToStream("VGG-16\\Conv1_2_Biases", output);
            AddFileToStream("VGG-16\\Conv2_1_Filters", output);
            AddFileToStream("VGG-16\\Conv2_1_Biases", output);
            AddFileToStream("VGG-16\\Conv2_2_Filters", output);
            AddFileToStream("VGG-16\\Conv2_2_Biases", output);
            AddFileToStream("VGG-16\\Conv3_1_Filters", output);
            AddFileToStream("VGG-16\\Conv3_1_Biases", output);
            AddFileToStream("VGG-16\\Conv3_2_Filters", output);
            AddFileToStream("VGG-16\\Conv3_2_Biases", output);
            AddFileToStream("VGG-16\\Conv3_3_Filters", output);
            AddFileToStream("VGG-16\\Conv3_3_Biases", output);
            AddFileToStream("VGG-16\\Conv4_1_Filters", output);
            AddFileToStream("VGG-16\\Conv4_1_Biases", output);
            AddFileToStream("VGG-16\\Conv4_2_Filters", output);
            AddFileToStream("VGG-16\\Conv4_2_Biases", output);
            AddFileToStream("VGG-16\\Conv4_3_Filters", output);
            AddFileToStream("VGG-16\\Conv4_3_Biases", output);
            AddFileToStream("VGG-16\\Conv5_1_Filters", output);
            AddFileToStream("VGG-16\\Conv5_1_Biases", output);
            output.Close();
        }

    }

}