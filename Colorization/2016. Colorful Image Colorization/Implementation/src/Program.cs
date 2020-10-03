//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019. All Rights reserved.
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

        public ColorfulColorization Net
        {
            get;
            set;
        }

        ///<summary>ColorizationThread.</summary>
        private Thread Enhancer;

        ///<summary>Stops the colorization process.</summary>
        ///<param name="sender">Sender(control on MainForm).</param>
        ///<param name="E">Arguments.</param>
        private void StopProcess(object sender, EventArgs E)
        {
            this.Enhancer.Abort();
            this.Enhancer = null;
            this.OpenOriginal.Enabled = true;
            this.GenerateOrSaveResult.Text = "Colorize";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.StartProcess;
            this.Progress.Value = 0;
        }

        ///<summary>Stops the colorization process.</summary>
        ///<param name="sender">Sender(MainForm).</param>
        ///<param name="E">Arguments.</param>
        private void CloseWindowHandler(object sender, CancelEventArgs E)
        {
            if(this.Enhancer != null)
            {
                this.Enhancer.Abort();
            }
        }

        ///<summary>Starts the colorization process.</summary>
        ///<param name="sender">Sender(control on MainForm).</param>
        ///<param name="E">Arguments.</param>
        private void StartProcess(object sender, EventArgs E)
        {
            this.TotalPercent = 0f;
            this.Enhancer = new Thread(this.Enhance);
            this.GenerateOrSaveResult.Text = "Stop";
            this.GenerateOrSaveResult.Click -= this.StartProcess;
            this.GenerateOrSaveResult.Click += this.StopProcess;
            this.OpenOriginal.Enabled = false;
            this.Enhancer.Start();
        }

        ///<summary>Colorizes the image. Should be runned only in Enhancer thread.</summary>
        private void Enhance()
        {
            var original = this.Original.Image as Bitmap;
            var ab = this.Net.Colorize(IOConverters.ImageToTensor(original));
            this.Result.Image = IOConverters.TensorToImage(original, ab);
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
            this.Net = new ColorfulColorization(Assembly.GetExecutingAssembly().GetManifestResourceStream("ColorfulImageColorization.model"));
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