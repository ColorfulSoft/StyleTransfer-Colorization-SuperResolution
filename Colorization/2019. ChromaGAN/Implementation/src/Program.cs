//*************************************************************************************************
//* (C) ColorfulSoft corp., 2020. All Rights reserved.
//*************************************************************************************************

//-> Entry point and initialization.

using System;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;

namespace NeuralColor
{

    public sealed partial class MainForm : Form
    {

        public ChromaGAN Net
        {
            get;
            set;
        }

        ///<summary>Colorization thread.</summary>
        private Thread Colorizer;

        ///<summary>Stops the colorization thread.</summary>
        ///<param name="sender">Sender(control on MainForm).</param>
        ///<param name="E">Arguments.</param>
        private void StopProcess(object sender, EventArgs E)
        {
            this.Colorizer.Abort();
            this.Colorizer = null;
            this.OpenOriginal.Enabled = true;
            this.GenerateOrSaveResult.Text = "Colorize";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.StartProcess;
            this.Progress.Value = 0;
        }

        ///<summary>Stops the colorization thread.</summary>
        ///<param name="sender">Sender(MainForm).</param>
        ///<param name="E">Arguments.</param>
        private void CloseWindowHandler(object sender, CancelEventArgs E)
        {
            if(this.Colorizer != null)
            {
                this.Colorizer.Abort();
            }
        }

        ///<summary>Starts the colorization process.</summary>
        ///<param name="sender">Sender(control on MainForm).</param>
        ///<param name="E">Arguments.</param>
        private void StartProcess(object sender, EventArgs E)
        {
            this.TotalPercent = 0f;
            this.Colorizer = new Thread(this.Colorize);
            this.GenerateOrSaveResult.Text = "Stop";
            this.GenerateOrSaveResult.Click -= this.StartProcess;
            this.GenerateOrSaveResult.Click += this.StopProcess;
            this.OpenOriginal.Enabled = false;
            this.Colorizer.Start();
        }

        ///<summary>Colorizes the image. Should be runned only in Colorizer thread.</summary>
        private void Colorize()
        {
            var Grayscale = IOConverters.Preprocess(this.Original.Image as Bitmap);
            var UV = this.Net.Colorize(Grayscale);
            this.Result.Image = IOConverters.Deprocess(this.Original.Image as Bitmap, UV);
            this.Progress.Value = 0;
            this.GenerateOrSaveResult.Text = "Save";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.SaveResult;
            this.OpenOriginal.Enabled = true;
        }

        ///<summary>Saves the result image.</summary>
        ///<param name="sender">Sender(control on MainForm).</param>
        ///<param name="E">Arguments.</param>
        private void SaveResult(object sender, EventArgs E)
        {
            var SFD = new SaveFileDialog();
            SFD.Title = "Save";
            SFD.Filter = "Images (*.bmp)|*.bmp|Images (*.emf)|*.emf|Images (*.exif)|*.exif|Images (*.gif)|*.gif|Images (*.ico)|*.ico|Images (*.jpg)|*.jpg|Images (*.png)|*.png|Images (*.tiff)|*.tiff|Images (*.wmf)|*.wmf";
            if(SFD.ShowDialog() == DialogResult.OK)
            {
                switch(SFD.FilterIndex)
                {
                    case 1:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Bmp);
                        break;
                    }
                    case 2:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Emf);
                        break;
                    }
                    case 3:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Exif);
                        break;
                    }
                    case 4:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Gif);
                        break;
                    }
                    case 5:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Icon);
                        break;
                    }
                    case 6:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Jpeg);
                        break;
                    }
                    case 7:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Png);
                        break;
                    }
                    case 8:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Tiff);
                        break;
                    }
                    case 9:
                    {
                        this.Result.Image.Save(SFD.FileName, ImageFormat.Wmf);
                        break;
                    }
                }
            }
        }

        private float TotalPercent
        {
            get;
            set;
        }

        public void FixStep(float percent)
        {
            this.TotalPercent += percent;
            this.Progress.Value = System.Math.Min((int)this.TotalPercent, 100);
        }

        ///<summary>Opens the grayscale image.</summary>
        ///<param name="sender">Sender(control on MainForm).</param>
        ///<param name="E">Arguments.</param>
        private void OpenOriginalHandler(object sender, EventArgs E)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Open";
            OFD.Filter = "Images (*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf)|*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf|All files|*.*";
            if(OFD.ShowDialog() == DialogResult.OK)
            {
                this.Original.Image = new Bitmap(OFD.FileName);
                if(GenerateOrSaveResult.Text != "Colorize")
                {
                    this.GenerateOrSaveResult.Text = "Colorize";
                    this.GenerateOrSaveResult.Click -= SaveResult;
                    this.GenerateOrSaveResult.Click += StartProcess;
                }
            }
        }

        public MainForm() : base()
        {
            this.Initialize();
            this.Closing += this.CloseWindowHandler;
            this.OpenOriginal.Click += this.OpenOriginalHandler;
            this.GenerateOrSaveResult.Click += this.StartProcess;
            this.Icon = Icon.FromHandle((new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("MainIcon.jpg"))).GetHicon());
            this.Net = new ChromaGAN(Assembly.GetExecutingAssembly().GetManifestResourceStream("ChromaGAN.HModel"));
            this.Net.Step += FixStep;
        }

    }

    public static class Program
    {

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

    }

}