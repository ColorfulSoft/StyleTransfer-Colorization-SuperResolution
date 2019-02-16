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

        ///<summary>Кодирующая нейросеть ReLU1_1.</summary>
        private Encoder encoder1;

        ///<summary>Декодирующая нейросеть ReLU1_1.</summary>
        private Decoder decoder1;

        ///<summary>Кодирующая нейросеть ReLU2_1.</summary>
        private Encoder encoder2;

        ///<summary>Декодирующая нейросеть ReLU2_1.</summary>
        private Decoder decoder2;

        ///<summary>Кодирующая нейросеть ReLU3_1.</summary>
        private Encoder encoder3;

        ///<summary>Декодирующая нейросеть ReLU3_1.</summary>
        private Decoder decoder3;

        ///<summary>Кодирующая нейросеть ReLU4_1.</summary>
        private Encoder encoder4;

        ///<summary>Декодирующая нейросеть ReLU4_1.</summary>
        private Decoder decoder4;

        ///<summary>Кодирующая нейросеть ReLU5_1.</summary>
        private Encoder encoder5;

        ///<summary>Декодирующая нейросеть ReLU5_1.</summary>
        private Decoder decoder5;

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
            var Result = StyleTransfer.Stylize(this.encoder1,
                                                 this.decoder1,
                                                 this.encoder2,
                                                 this.decoder2,
                                                 this.encoder3,
                                                 this.decoder3,
                                                 this.encoder4,
                                                 this.decoder4,
                                                 this.encoder5,
                                                 this.decoder5,
                                                 content,
                                                 style);
            this.ResultImage.Image = IOConverters.TensorToImage(Result);
            this.Stylized = true;
            this.Progress.Value = 0;
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

        ///<summary>Изменяет значение общего прогресса выполнения, прибавляя к нему заданный процент. Это - обработчик события.</summary>
        ///<param name="percent">Процент.</param>
        private void ChangeProgressValue(byte percent)
        {
            this.Progress.Value = percent;
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
            // Encoders
            this.encoder1 = new Encoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("vgg_normalised_conv1_1.model"), EncoderType.Conv1);
            this.encoder2 = new Encoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("vgg_normalised_conv2_1.model"), EncoderType.Conv2);
            this.encoder3 = new Encoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("vgg_normalised_conv3_1.model"), EncoderType.Conv3);
            this.encoder4 = new Encoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("vgg_normalised_conv4_1.model"), EncoderType.Conv4);
            this.encoder5 = new Encoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("vgg_normalised_conv5_1.model"), EncoderType.Conv5);
            // Decoders
            this.decoder1 = new Decoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("feature_invertor_conv1_1.model"), DecoderType.Conv1);
            this.decoder2 = new Decoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("feature_invertor_conv2_1.model"), DecoderType.Conv2);
            this.decoder3 = new Decoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("feature_invertor_conv3_1.model"), DecoderType.Conv3);
            this.decoder4 = new Decoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("feature_invertor_conv4_1.model"), DecoderType.Conv4);
            this.decoder5 = new Decoder(Assembly.GetExecutingAssembly().GetManifestResourceStream("feature_invertor_conv5_1.model"), DecoderType.Conv5);
            StyleTransfer.OnStepDone += this.ChangeProgressValue;
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