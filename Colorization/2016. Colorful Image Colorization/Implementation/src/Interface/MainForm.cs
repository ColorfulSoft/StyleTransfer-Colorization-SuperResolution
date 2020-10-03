//*************************************************************************************************
//* (C) ColorfulSoft corp., 2019. All Rights reserved.
//*************************************************************************************************

// -> The Main Form class.

using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace NeuralColor
{

    public sealed partial class MainForm : Form
    {

        public GroupBox OriginalSettings
        {
            get;
            set;
        }

        public PictureBox Original
        {
            get;
            set;
        }

        public Button OpenOriginal
        {
            get;
            set;
        }

        public GroupBox ResultSettings
        {
            get;
            set;
        }

        public PictureBox Result
        {
            get;
            set;
        }

        public Button GenerateOrSaveResult
        {
            get;
            set;
        }

        public ProgressBar Progress
        {
            get;
            set;
        }

        public void Initialize()
        {
            // -> MainForm
            this.Text = "Colorful Image Colorization";
            this.ClientSize = new Size(582, 376);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            // -> OriginalSettings
            this.OriginalSettings = new GroupBox();
            this.OriginalSettings.Text = "Grayscale";
            this.OriginalSettings.Width = 276;
            this.OriginalSettings.Height = 321;
            this.OriginalSettings.Top = 10;
            this.OriginalSettings.Left = 10;
              // -> Original
              this.Original = new PictureBox();
              this.Original.Size = new Size(256, 256);
              this.Original.Top = 20;
              this.Original.Left = 10;
              this.Original.BackColor = Color.White;
              this.Original.BorderStyle = BorderStyle.FixedSingle;
              this.Original.SizeMode = PictureBoxSizeMode.Zoom;
              this.Original.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Original.jpg"));
              this.OriginalSettings.Controls.Add(this.Original);
              // -> OpenOriginal
              this.OpenOriginal = new Button();
              this.OpenOriginal.Top = 286;
              this.OpenOriginal.Left = 10;
              this.OpenOriginal.Size = new Size(256, 25);
              this.OpenOriginal.Text = "Open";
              this.OriginalSettings.Controls.Add(this.OpenOriginal);
            this.Controls.Add(this.OriginalSettings);
            // -> ResultSettings
            this.ResultSettings = new GroupBox();
            this.ResultSettings.Text = "Result";
            this.ResultSettings.Width = 276;
            this.ResultSettings.Height = 321;
            this.ResultSettings.Top = 10;
            this.ResultSettings.Left = 296;
              // -> Result
              this.Result = new PictureBox();
              this.Result.Size = new Size(256, 256);
              this.Result.Top = 20;
              this.Result.Left = 10;
              this.Result.BackColor = Color.White;
              this.Result.BorderStyle = BorderStyle.FixedSingle;
              this.Result.SizeMode = PictureBoxSizeMode.Zoom;
              this.ResultSettings.Controls.Add(this.Result);
              // -> GenerateOrSaveResult
              this.GenerateOrSaveResult = new Button();
              this.GenerateOrSaveResult.Top = 286;
              this.GenerateOrSaveResult.Left = 10;
              this.GenerateOrSaveResult.Size = new Size(256, 25);
              this.GenerateOrSaveResult.Text = "Colorize";
              this.ResultSettings.Controls.Add(this.GenerateOrSaveResult);
            this.Controls.Add(this.ResultSettings);
            // -> Progress
            this.Progress = new ProgressBar();
            this.Progress.Size = new Size(562, 25);
            this.Progress.Top = 341;
            this.Progress.Left = 10;
            this.Controls.Add(this.Progress);
        }

    }

}