//*************************************************************************************************
//* (C) ColorfulSoft, 2020. Все права защищены.
//*************************************************************************************************

//-> Определение метода инициализации приложения.

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

        public Colornet Net
        {
            get;
            set;
        }

        ///<summary>Поток выполнения.</summary>
        private Thread Enhancer;

        ///<summary>Останавливает процесс обработки. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на объёкт(кнопку на форме), вызвавший метод.</param>
        ///<param name="E">Аргументы.</param>
        private void StopProcess(object sender, EventArgs E)
        {
            this.Enhancer.Abort();
            this.Enhancer = null;
            this.OpenOriginal.Enabled = true;
            this.GenerateOrSaveResult.Text = "Окрасить";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.StartProcess;
            this.Progress.Value = 0;
        }

        ///<summary>Останавливает поток обработки, если он был запущен. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на форму, вызвавшую метод.</param>
        ///<param name="E">Аргументы.</param>
        private void CloseWindowHandler(object sender, CancelEventArgs E)
        {
            if(this.Enhancer != null)
            {
                this.Enhancer.Abort();
            }
        }

        ///<summary>Запускает процесс обработки. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на объёкт(кнопку на форме), вызвавший метод.</param>
        ///<param name="E">Аргументы.</param>
        private void StartProcess(object sender, EventArgs E)
        {
            this.TotalPercent = 0f;
            this.Enhancer = new Thread(this.Enhance);
            this.GenerateOrSaveResult.Text = "Остановить процесс";
            this.GenerateOrSaveResult.Click -= this.StartProcess;
            this.GenerateOrSaveResult.Click += this.StopProcess;
            this.OpenOriginal.Enabled = false;
            this.Enhancer.Start();
        }

        ///<summary>Выполняет улучшение изображения. Метод выполняется в потоке Enhancer.</summary>
        private void Enhance()
        {
            var Grayscale = IOConverters.Preprocess(this.Original.Image as Bitmap);
            var UV = this.Net.Colorize(Grayscale.Item1, Grayscale.Item2);
            this.Result.Image = IOConverters.Deprocess(Grayscale.Item1, UV);
            this.Progress.Value = 0;
            this.GenerateOrSaveResult.Text = "Сохранить";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.SaveResult;
            this.OpenOriginal.Enabled = true;
        }

        ///<summary>Открывает диалоговое окно сохранения результата. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на объёкт(кнопку на форме), вызвавший метод.</param>
        ///<param name="E">Аргументы.</param>
        private void SaveResult(object sender, EventArgs E)
        {
            var SFD = new SaveFileDialog();
            SFD.Title = "Сохранить результат";
            SFD.Filter = "Изображения (*.bmp)|*.bmp|Изображения (*.emf)|*.emf|Изображения (*.exif)|*.exif|Изображения (*.gif)|*.gif|Изображения (*.ico)|*.ico|Изображения (*.jpg)|*.jpg|Изображения (*.png)|*.png|Изображения (*.tiff)|*.tiff|Изображения (*.wmf)|*.wmf";
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

        ///<summary>Открывает диалоговое окно выбора контентного изображения. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на объёкт(кнопку на форме), вызвавший метод.</param>
        ///<param name="E">Аргументы.</param>
        private void OpenOriginalHandler(object sender, EventArgs E)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Открыть изображение";
            OFD.Filter = "Изображения (*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf)|*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf|Все файлы|*.*";
            if(OFD.ShowDialog() == DialogResult.OK)
            {
                this.Original.Image = new Bitmap(OFD.FileName);
                if(GenerateOrSaveResult.Text != "Окрасить")
                {
                    this.GenerateOrSaveResult.Text = "Окрасить";
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
            this.Net = new Colornet(Assembly.GetExecutingAssembly().GetManifestResourceStream("Colornet.HModel"));
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