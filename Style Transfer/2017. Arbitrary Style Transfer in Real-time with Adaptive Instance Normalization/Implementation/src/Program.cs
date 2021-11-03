//*************************************************************************************************
//* (C) ColorfulSoft corp., 2021. All Rights reserved.
//*************************************************************************************************

using System;
using System.Windows.Forms;

namespace ColorfulSoft.NeuralArt.AdaIN
{

    /// <summary>
    /// Main class.
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// Entry point.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            AdaIN.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainForm());
            }
            catch
            {
            }
        }

    }

}
