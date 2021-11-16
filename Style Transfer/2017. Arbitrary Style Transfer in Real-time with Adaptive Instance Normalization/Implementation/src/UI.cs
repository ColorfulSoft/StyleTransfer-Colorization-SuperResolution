//*************************************************************************************************
//* (C) ColorfulSoft corp., 2021. All Rights reserved.
//*************************************************************************************************

using System;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ColorfulSoft.NeuralArt.AdaIN
{

    ///<summary>MainForm</summary>
    public sealed partial class MainForm : Form
    {

        ///<summary>Content image parameters control.</summary>
        private GroupBox ContentBox;

        ///<summary>Style image parameters control.</summary>
        private GroupBox StyleBox;

        ///<summary>Output image parameters control.</summary>
        private GroupBox ResultBox;

        ///<summary>Content image.</summary>
        private PictureBox ContentImage;

        ///<summary>Style image.</summary>
        private PictureBox StyleImage;

        ///<summary>Output image.</summary>
        private PictureBox ResultImage;

        ///<summary>Open content button.</summary>
        private Button OpenContent;

        ///<summary>Open style button.</summary>
        private Button OpenStyle;

        ///<summary>Button to start stylization thread or save output image.</summary>
        private Button GenerateOrSaveResult;

        ///<summary>Progress bar.</summary>
        private ProgressBar Progress;

        ///<summary>Is stylized image done?</summary>
        private bool Stylized;

        ///<summary>Stylization thread.</summary>
        private Thread Stylizer;

        ///<summary>Stops the stylization process. Event handler.</summary>
        ///<param name="sender">Sender.</param>
        ///<param name="E">Args.</param>
        private void StopProcess(object sender, EventArgs E)
        {
            this.Stylizer.Abort();
            this.Stylizer = null;
            this.OpenContent.Enabled = true;
            this.OpenStyle.Enabled = true;
            this.GenerateOrSaveResult.Text = "Generate";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.StartProcess;
            this.Progress.Value = 0;
        }

        ///<summary>Starts the stylization process. Event handler.</summary>
        ///<param name="sender">Sender.</param>
        ///<param name="E">Args.</param>
        private void StartProcess(object sender, EventArgs E)
        {
            this.Stylizer = new Thread(() =>
            {
                this.ResultImage.Image = AdaIN.Stylize(this.ContentImage.Image as Bitmap, this.StyleImage.Image as Bitmap);
                this.Stylized = true;
                this.Progress.Value = 0;
                this.GenerateOrSaveResult.Text = "Save";
                this.GenerateOrSaveResult.Click -= this.StopProcess;
                this.GenerateOrSaveResult.Click += this.SaveResult;
                this.OpenContent.Enabled = true;
                this.OpenStyle.Enabled = true;
            });
            this.GenerateOrSaveResult.Text = "Stop";
            this.GenerateOrSaveResult.Click -= this.StartProcess;
            this.GenerateOrSaveResult.Click += this.StopProcess;
            this.OpenContent.Enabled = false;
            this.OpenStyle.Enabled = false;
            this.Stylized = false;
            this.Stylizer.Start();
        }

        ///<summary>Saves the result image. Event handler.</summary>
        ///<param name="sender">Sender.</param>
        ///<param name="E">Args.</param>
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
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Bmp);
                        break;
                    }
                    case 2:
                    {
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Emf);
                        break;
                    }
                    case 3:
                    {
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Exif);
                        break;
                    }
                    case 4:
                    {
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Gif);
                        break;
                    }
                    case 5:
                    {
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Icon);
                        break;
                    }
                    case 6:
                    {
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Jpeg);
                        break;
                    }
                    case 7:
                    {
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Png);
                        break;
                    }
                    case 8:
                    {
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Tiff);
                        break;
                    }
                    case 9:
                    {
                        this.ResultImage.Image.Save(SFD.FileName, ImageFormat.Wmf);
                        break;
                    }
                }
            }
        }

        ///<summary>Initializes the main form.</summary>
        public MainForm() : base()
        {
            //-> MainForm
            this.ClientSize = new Size(868, 376);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Arbitrary Style Transfer in Real-time with Adaptive Instance Normalization";
            this.Icon = Icon.FromHandle((new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("MainIcon.jpg"))).GetHicon());
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Closing += delegate
            {
                if(this.Stylizer != null)
                {
                    this.Stylizer.Abort();
                }
            };
            //-> ContentBox
            this.ContentBox = new GroupBox();
            this.ContentBox.Size = new Size(276, 321);
            this.ContentBox.Text = "Content";
            this.ContentBox.Top = 10;
            this.ContentBox.Left = 10;
              //-> ContentImage
              this.ContentImage = new PictureBox();
              this.ContentImage.Size = new Size(256, 256);
              this.ContentImage.Top = 20;
              this.ContentImage.Left = 10;
              this.ContentImage.BackColor = Color.White;
              this.ContentImage.BorderStyle = BorderStyle.FixedSingle;
              this.ContentImage.SizeMode = PictureBoxSizeMode.Zoom;
              this.ContentImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Content.jpg"));
              this.ContentBox.Controls.Add(this.ContentImage);
              //-> OpenContent
              this.OpenContent = new Button();
              this.OpenContent.Top = 286;
              this.OpenContent.Left = 10;
              this.OpenContent.Size = new Size(256, 25);
              this.OpenContent.Text = "Open";
              this.OpenContent.Click += delegate
              {
                  var OFD = new OpenFileDialog();
                  OFD.Title = "Open content image";
                  OFD.Filter = "Images (*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf)|*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf|All files|*.*";
                  if(OFD.ShowDialog() == DialogResult.OK)
                  {
                      this.ContentImage.Image = new Bitmap(OFD.FileName);
                      if(this.Stylized)
                      {
                          this.Stylized = false;
                          this.GenerateOrSaveResult.Text = "Generate";
                          this.GenerateOrSaveResult.Click -= SaveResult;
                          this.GenerateOrSaveResult.Click += StartProcess;
                      }
                      if((this.StyleImage.Image != null) && (this.GenerateOrSaveResult.Enabled == false))
                      {
                          this.GenerateOrSaveResult.Enabled = true;
                      }
                  }
              };
              this.ContentBox.Controls.Add(this.OpenContent);
            this.Controls.Add(this.ContentBox);
            //-> StyleBox
            this.StyleBox = new GroupBox();
            this.StyleBox.Size = new Size(276, 321);
            this.StyleBox.Text = "Style";
            this.StyleBox.Top = 10;
            this.StyleBox.Left = 296;
              //-> StyleImage
              this.StyleImage = new PictureBox();
              this.StyleImage.Size = new Size(256, 256);
              this.StyleImage.Top = 20;
              this.StyleImage.Left = 10;
              this.StyleImage.BackColor = Color.White;
              this.StyleImage.BorderStyle = BorderStyle.FixedSingle;
              this.StyleImage.SizeMode = PictureBoxSizeMode.Zoom;
              this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Style.jpg"));
              this.StyleBox.Controls.Add(this.StyleImage);
              //-> OpenStyle
              this.OpenStyle = new Button();
              this.OpenStyle.Top = 286;
              this.OpenStyle.Left = 10;
              this.OpenStyle.Size = new Size(256, 25);
              this.OpenStyle.Text = "Open";
              this.OpenStyle.Click += delegate
              {
                  var OFD = new OpenFileDialog();
                  OFD.Title = "Open style image";
                  OFD.Filter = "Images (*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf)|*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf|All files|*.*";
                  if(OFD.ShowDialog() == DialogResult.OK)
                  {
                      this.StyleImage.Image = new Bitmap(OFD.FileName);
                      if(this.Stylized)
                      {
                          this.Stylized = false;
                          this.GenerateOrSaveResult.Text = "Generate";
                          this.GenerateOrSaveResult.Click -= SaveResult;
                          this.GenerateOrSaveResult.Click += StartProcess;
                      }
                      if((this.ContentImage.Image != null) && (this.GenerateOrSaveResult.Enabled == false))
                      {
                          this.GenerateOrSaveResult.Enabled = true;
                      }
                  }
              };
              this.StyleBox.Controls.Add(this.OpenStyle);
            this.Controls.Add(this.StyleBox);
            //-> ResultBox
            this.ResultBox = new GroupBox();
            this.ResultBox.Size = new Size(276, 321);
            this.ResultBox.Text = "Result";
            this.ResultBox.Top = 10;
            this.ResultBox.Left = 582;
              //-> ResultImage
              this.ResultImage = new PictureBox();
              this.ResultImage.Size = new Size(256, 256);
              this.ResultImage.Top = 20;
              this.ResultImage.Left = 10;
              this.ResultImage.BackColor = Color.White;
              this.ResultImage.BorderStyle = BorderStyle.FixedSingle;
              this.ResultImage.SizeMode = PictureBoxSizeMode.Zoom;
              this.ResultBox.Controls.Add(this.ResultImage);
              //-> GenerateOrSaveResult
              this.GenerateOrSaveResult = new Button();
              this.GenerateOrSaveResult.Top = 286;
              this.GenerateOrSaveResult.Left = 10;
              this.GenerateOrSaveResult.Size = new Size(256, 25);
              this.GenerateOrSaveResult.Text = "Generate";
              this.GenerateOrSaveResult.Click += this.StartProcess;
              this.ResultBox.Controls.Add(this.GenerateOrSaveResult);
            this.Controls.Add(this.ResultBox);
            //-> Progress
            this.Progress = new ProgressBar();
            this.Progress.Size = new Size(848, 25);
            this.Progress.Top = 341;
            this.Progress.Left = 10;
            this.Controls.Add(this.Progress);
            //
            AdaIN.Progress += (float Percent) =>
            {
                this.Progress.Value = (int)Percent;
            };
            this.Stylized = false;
        }

    }

}
