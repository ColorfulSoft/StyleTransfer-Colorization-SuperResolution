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

        ///<summary>Сеть-трансформер.</summary>
        private Transformer transformer;

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
            this.SelectStyle.Enabled = true;
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
            this.SelectStyle.Enabled = false;
            this.Stylizer.Start();
        }

        ///<summary>Выполняет стилизацию. Метод выполняется в потоке Stylizer.</summary>
        private void Stylize()
        {
            var content = IOConverters.ImageToTensor(this.ContentImage.Image as Bitmap);
            var Result = transformer.Stylize(content);
            this.ResultImage.Image = IOConverters.TensorToImage(Result);
            this.Progress.Value = 0;
            this.GenerateOrSaveResult.Text = "Сохранить";
            this.GenerateOrSaveResult.Click -= this.StopProcess;
            this.GenerateOrSaveResult.Click += this.SaveResult;
            this.OpenContent.Enabled = true;
            this.SelectStyle.Enabled = true;
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
            this.Progress.Value += percent;
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
                if(this.GenerateOrSaveResult.Text == "Сохранить")
                {
                    this.GenerateOrSaveResult.Click -= SaveResult;
                    this.GenerateOrSaveResult.Click += StartProcess;
                    this.GenerateOrSaveResult.Text = "Запустить процесс";
                }
                if((this.StyleImage.Image != null) && (this.GenerateOrSaveResult.Enabled == false))
                {
                    this.GenerateOrSaveResult.Enabled = true;
                }
            }
        }

        ///<summary>Устанавливает активный стиль. Обработчик события.</summary>
        private void SelectStyleHandler(object sender, EventArgs E)
        {
            switch(this.SelectStyle.SelectedIndex)
            {
                case 0:
                {
                    this.transformer = new Transformer(Assembly.GetExecutingAssembly().GetManifestResourceStream("la_muse.model"));
                    this.transformer.Step += this.ChangeProgressValue;
                    this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("la_muse.jpg"));
                    this.StyleImage.Invalidate();
                    if(this.GenerateOrSaveResult.Text == "Сохранить")
                    {
                        this.GenerateOrSaveResult.Click -= SaveResult;
                        this.GenerateOrSaveResult.Click += StartProcess;
                        this.GenerateOrSaveResult.Text = "Запустить процесс";
                    }
                    break;
                }
                case 1:
                {
                    this.transformer = new Transformer(Assembly.GetExecutingAssembly().GetManifestResourceStream("rain_princess.model"));
                    this.transformer.Step += this.ChangeProgressValue;
                    this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("rain_princess.jpg"));
                    this.StyleImage.Invalidate();
                    if(this.GenerateOrSaveResult.Text == "Сохранить")
                    {
                        this.GenerateOrSaveResult.Click -= SaveResult;
                        this.GenerateOrSaveResult.Click += StartProcess;
                        this.GenerateOrSaveResult.Text = "Запустить процесс";
                    }
                    break;
                }
                case 2:
                {
                    this.transformer = new Transformer(Assembly.GetExecutingAssembly().GetManifestResourceStream("scream.model"));
                    this.transformer.Step += this.ChangeProgressValue;
                    this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("scream.jpg"));
                    this.StyleImage.Invalidate();
                    if(this.GenerateOrSaveResult.Text == "Сохранить")
                    {
                        this.GenerateOrSaveResult.Click -= SaveResult;
                        this.GenerateOrSaveResult.Click += StartProcess;
                        this.GenerateOrSaveResult.Text = "Запустить процесс";
                    }
                    break;
                }
                case 3:
                {
                    this.transformer = new Transformer(Assembly.GetExecutingAssembly().GetManifestResourceStream("udnie.model"));
                    this.transformer.Step += this.ChangeProgressValue;
                    this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("udnie.jpg"));
                    this.StyleImage.Invalidate();
                    if(this.GenerateOrSaveResult.Text == "Сохранить")
                    {
                        this.GenerateOrSaveResult.Click -= SaveResult;
                        this.GenerateOrSaveResult.Click += StartProcess;
                        this.GenerateOrSaveResult.Text = "Запустить процесс";
                    }
                    break;
                }
                case 4:
                {
                    this.transformer = new Transformer(Assembly.GetExecutingAssembly().GetManifestResourceStream("wave.model"));
                    this.transformer.Step += this.ChangeProgressValue;
                    this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("wave.jpg"));
                    this.StyleImage.Invalidate();
                    if(this.GenerateOrSaveResult.Text == "Сохранить")
                    {
                        this.GenerateOrSaveResult.Click -= SaveResult;
                        this.GenerateOrSaveResult.Click += StartProcess;
                        this.GenerateOrSaveResult.Text = "Запустить процесс";
                    }
                    break;
                }
                case 5:
                {
                    this.transformer = new Transformer(Assembly.GetExecutingAssembly().GetManifestResourceStream("wreck.model"));
                    this.transformer.Step += this.ChangeProgressValue;
                    this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("wreck.jpg"));
                    this.StyleImage.Invalidate();
                    if(this.GenerateOrSaveResult.Text == "Сохранить")
                    {
                        this.GenerateOrSaveResult.Click -= SaveResult;
                        this.GenerateOrSaveResult.Click += StartProcess;
                        this.GenerateOrSaveResult.Text = "Запустить процесс";
                    }
                    break;
                }
                default:
                {
                    this.transformer = new Transformer(Assembly.GetExecutingAssembly().GetManifestResourceStream("la_muse.model"));
                    this.transformer.Step += this.ChangeProgressValue;
                    this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("la_muse.jpg"));
                    this.StyleImage.Invalidate();
                    if(this.GenerateOrSaveResult.Text == "Сохранить")
                    {
                        this.GenerateOrSaveResult.Click -= SaveResult;
                        this.GenerateOrSaveResult.Click += StartProcess;
                        this.GenerateOrSaveResult.Text = "Запустить процесс";
                    }
                    break;
                }
            }
        }

        ///<summary>Инициализирует главную форму приложения и нейросети.</summary>
        public MainForm() : base()
        {
            this.InitializeComponent();
            this.Closing += this.CloseWindowHandler;
            this.transformer = new Transformer(Assembly.GetExecutingAssembly().GetManifestResourceStream("la_muse.model"));
            this.StyleImage.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("la_muse.jpg"));
            this.transformer.Step += this.ChangeProgressValue;
            this.OpenContent.Click += this.OpenContentHandler;
            this.SelectStyle.SelectedIndexChanged += this.SelectStyleHandler;
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