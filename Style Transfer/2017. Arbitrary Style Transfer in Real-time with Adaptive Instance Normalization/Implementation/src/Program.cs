//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Точка входа приложения.

using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;

namespace NeuralArt
{

    ///<summary>Главная форма приложения.</summary>
    public sealed partial class MainForm : Form
    {

        ///<summary>Кодирующая нейросеть.</summary>
        private Encoder encoder;

        ///<summary>Декодирующая нейросеть.</summary>
        private Decoder decoder;

        ///<summary>Значение, указывающее, было ли текущее изображение отстилизовано.</summary>
        private bool Stylized;

        ///<summary>Поток стилизации.</summary>
        private Thread Stylizer;

        ///<summary>Останавливает процесс стилизации. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на объёкт(кнопку на форме), вызвавший метод.</param>
        ///<param name="E">Аргументы.</param>
        private void StopProcess(object sender, EventArgs E)
        {
            this.Stylizer.Abort();
            this.Stylizer = null;
            this.OpenContent.Enabled = true;
            this.OpenStyle.Enabled = true;
            this.GenerateOrSaveResult.Text = "Запустить процесс";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.StartProcess;
            this.TotalProgress = 0f;
            this.Progress.Value = 0;
        }

        ///<summary>Останавливает поток стилизации, если он был запущен. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на форму, вызвавшую метод.</param>
        ///<param name="E">Аргументы.</param>
        private void CloseWindowHandler(object sender, CancelEventArgs E)
        {
            if(this.Stylizer != null)
            {
                this.Stylizer.Abort();
            }
        }

        ///<summary>Запускает процесс стилизации. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на объёкт(кнопку на форме), вызвавший метод.</param>
        ///<param name="E">Аргументы.</param>
        private void StartProcess(object sender, EventArgs E)
        {
            this.Stylizer = new Thread(this.Stylize);
            this.GenerateOrSaveResult.Text = "Остановить процесс";
            this.GenerateOrSaveResult.Click -= this.StartProcess;
            this.GenerateOrSaveResult.Click += this.StopProcess;
            this.OpenContent.Enabled = false;
            this.OpenStyle.Enabled = false;
            this.Stylized = false;
            this.Stylizer.Start();
        }

        ///<summary>Выполняет стилизацию. Метод выполняется в потоке Stylizer.</summary>
        private void Stylize()
        {
            var content = IOConverters.ImageToTensor(this.ContentImage.Image as Bitmap);
            var style = IOConverters.ImageToTensor(this.StyleImage.Image as Bitmap);
            var content_features = encoder.Encode(content);
            var style_features = encoder.Encode(style);
            var Total = Layers.AdaIN(content_features, style_features);
            var Decoded = decoder.Decode(Total);
            this.ResultImage.Image = IOConverters.TensorToImage(Decoded);
            this.Stylized = true;
            this.Progress.Value = 0;
            this.TotalProgress = 0f;
            this.GenerateOrSaveResult.Text = "Сохранить";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.SaveResult;
            this.OpenContent.Enabled = true;
            this.OpenStyle.Enabled = true;
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

        ///<summary>Общий прогресс выполнения.</summary>
        private float TotalProgress;

        ///<summary>Изменяет значение общего прогресса выполнения, прибавляя к нему заданный процент. Это - обработчик события.</summary>
        ///<param name="percent">Процент.</param>
        private void ChangeProgressValue(byte percent)
        {
            this.TotalProgress += percent / 300f;
            this.Progress.Value = (int)(this.TotalProgress * 100f);
        }

        ///<summary>Открывает диалоговое окно выбора контентного изображения. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на объёкт(кнопку на форме), вызвавший метод.</param>
        ///<param name="E">Аргументы.</param>
        private void OpenContentHandler(object sender, EventArgs E)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Открыть контентное изображение";
            OFD.Filter = "Изображения (*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf)|*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf|Все файлы|*.*";
            if(OFD.ShowDialog() == DialogResult.OK)
            {
                this.ContentImage.Image = new Bitmap(OFD.FileName);
                if(this.Stylized)
                {
                    this.Stylized = false;
                    this.GenerateOrSaveResult.Text = "Запустить процесс";
                    this.GenerateOrSaveResult.Click -= SaveResult;
                    this.GenerateOrSaveResult.Click += StartProcess;
                }
                if((this.StyleImage.Image != null) && (this.GenerateOrSaveResult.Enabled == false))
                {
                    this.GenerateOrSaveResult.Enabled = true;
                }
            }
        }

        ///<summary>Открывает диалоговое окно выбора стилевого изображения. Это - обработчик события.</summary>
        ///<param name="sender">Ссылка на объёкт(кнопку на форме), вызвавший метод.</param>
        ///<param name="E">Аргументы.</param>
        private void OpenStyleHandler(object sender, EventArgs E)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Открыть стилевое изображение";
            OFD.Filter = "Изображения (*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf)|*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf|Все файлы|*.*";
            if(OFD.ShowDialog() == DialogResult.OK)
            {
                this.StyleImage.Image = new Bitmap(OFD.FileName);
                if(this.Stylized)
                {
                    this.Stylized = false;
                    this.GenerateOrSaveResult.Text = "Запустить процесс";
                    this.GenerateOrSaveResult.Click -= SaveResult;
                    this.GenerateOrSaveResult.Click += StartProcess;
                }
                if((this.ContentImage.Image != null) && (this.GenerateOrSaveResult.Enabled == false))
                {
                    this.GenerateOrSaveResult.Enabled = true;
                }
            }
        }

        ///<summary>Инициализирует главную форму приложения и нейросети.</summary>
        public MainForm() : base()
        {
            this.InitializeComponent();
            this.Closing += this.CloseWindowHandler;
            this.TotalProgress = 0f;
            this.decoder = new Decoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("Decoder.HModel"));
            this.encoder = new Encoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("Encoder.HModel"));
            this.decoder.Step += this.ChangeProgressValue;
            this.encoder.Step += this.ChangeProgressValue;
            this.Stylized = false;
            this.OpenContent.Click += this.OpenContentHandler;
            this.OpenStyle.Click += this.OpenStyleHandler;
            this.GenerateOrSaveResult.Click += this.StartProcess;
        }

    }

    ///<summary>Главный класс приложения.</summary>
    public static class Program
    {

        [STAThread]
        ///<summary>Точка входа приложения.</summary>
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

    }

}