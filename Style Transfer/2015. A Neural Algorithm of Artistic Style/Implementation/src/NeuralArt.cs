//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace NeuralArt
{

    ///<summary>Главная форма приложения.</summary>
    public sealed partial class MainForm : Form
    {

        ///<summary>Поток стилизации.</summary>
        public Thread StylizationThread
        {
            get;
            set;
        }

        ///<summary>Выполняет фиксацию итерации. Обработчик события.</summary>
        ///<param name="iter">Итерация.</param>
        ///<param name="loss">Ошибка.</param>
        public void FixIteration(int iter, float loss)
        {
            this.Iteration.Text = "Iteration: " + (iter + 1).ToString() + "; Time: " + (DateTime.Now - Program.DT).TotalSeconds.ToString();
            this.ResultImageBox.Image = IOConverters.TensorToImage(Program.X);
            Program.DT = DateTime.Now;
        }

        ///<summary>Открывает файловый диалог и устанавливает выбранное контентное изображение. Обработчик события.</summary>
        public void OpenContentHandler(object sender, EventArgs E)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Открыть контентное изображение";
            OFD.Filter = "Изображения (*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf)|*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf|Все файлы|*.*";
            if(OFD.ShowDialog() == DialogResult.OK)
            {
                this.ContentImageBox.Image = new Bitmap(OFD.FileName);
                Program.Content = IOConverters.ImageToTensor(this.ContentImageBox.Image as Bitmap);
                Program.X = Program.Content.Clone();
                this.ResultImageBox.Image = IOConverters.TensorToImage(Program.X);
            }
        }

        ///<summary>Открывает файловый диалог и сохраняет результат стилизации в указанном месте. Обработчик события.</summary>
        public void SaveResultHandler(object sender, EventArgs E)
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
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Bmp);
                        break;
                    }
                    case 2:
                    {
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Emf);
                        break;
                    }
                    case 3:
                    {
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Exif);
                        break;
                    }
                    case 4:
                    {
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Gif);
                        break;
                    }
                    case 5:
                    {
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Icon);
                        break;
                    }
                    case 6:
                    {
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Jpeg);
                        break;
                    }
                    case 7:
                    {
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Png);
                        break;
                    }
                    case 8:
                    {
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Tiff);
                        break;
                    }
                    case 9:
                    {
                        this.ResultImageBox.Image.Save(SFD.FileName, ImageFormat.Wmf);
                        break;
                    }
                }
            }
        }

        ///<summary>Открывает файловый диалог и устанавливает выбранное стилевое изображение. Обработчик события.</summary>
        public void OpenStyleHandler(object sender, EventArgs E)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Открыть стилевое изображение";
            OFD.Filter = "Изображения (*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf)|*.bmp; *.emf; *.exif; *.gif; *.ico; *.jpg; *.png; *.tiff; *.wmf|Все файлы|*.*";
            if(OFD.ShowDialog() == DialogResult.OK)
            {
                this.StyleImageBox.Image = new Bitmap(OFD.FileName);
                Program.Style = IOConverters.ImageToTensor(new Bitmap(this.StyleImageBox.Image, this.ContentImageBox.Image.Size));
                Program.X = Program.Content.Clone();
                this.ResultImageBox.Image = IOConverters.TensorToImage(Program.X);
            }
        }

        ///<summary>Выполняет стилизацию.</summary>
        public void Stylize()
        {
            Program.Net.FixStyle(Program.Style);
            Program.Net.FixContent(Program.Content);
            Program.DT = DateTime.Now;
            Program.Net.StartIterativeProcess(Program.X);
        }

        ///<summary>Инициализирует поток для стилизации и запускает его. Обработчик события.</summary>
        public void StartProcessHandler(object sender, EventArgs E)
        {
            this.OpenContent.Enabled = false;
            this.OpenStyle.Enabled = false;
            this.StartProcess.Enabled = false;
            this.StopProcess.Enabled = true;
            this.StylizationThread = new Thread(this.Stylize);
            this.StylizationThread.Start();
        }

        ///<summary>Останавливает процесс стилизации. Обработчик события.</summary>
        public void StopProcessHandler(object sender, EventArgs E)
        {
            this.OpenContent.Enabled = true;
            this.OpenStyle.Enabled = true;
            this.StartProcess.Enabled = true;
            this.StopProcess.Enabled = false;
            this.StylizationThread.Abort();
            this.StylizationThread = null;
            this.Iteration.Text = "Iteration: 0; Time: 0";
        }

        ///<summary>Обеспечивает правильное завершение работы приложения. Обработчик события.</summary>
        public void ClosingHandler(object sender, EventArgs E)
        {
            if(this.StylizationThread != null)
            {
                this.StylizationThread.Abort();
            }
        }

        ///<summary>Инициализирует главную форму приложения. Конструктор.</summary>
        public MainForm() : base()
        {
            this.Initialize();
            this.ContentImageBox.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Content.jpg"));
            this.StyleImageBox.Image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Style.jpg"));
            Program.Net = new VGG16(Assembly.GetExecutingAssembly().GetManifestResourceStream("Net.Model"));
            Program.Net.OnIterationDone += this.FixIteration;
            Program.Style = IOConverters.ImageToTensor(new Bitmap(StyleImageBox.Image, ContentImageBox.Image.Size));
            Program.Content = IOConverters.ImageToTensor(ContentImageBox.Image as Bitmap);
            Program.X = Program.Content.Clone();
            this.ResultImageBox.Image = IOConverters.TensorToImage(Program.X);
            this.StartProcess.Click += this.StartProcessHandler;
            this.OpenContent.Click += this.OpenContentHandler;
            this.OpenStyle.Click += this.OpenStyleHandler;
            this.StopProcess.Click += this.StopProcessHandler;
            this.Closing += this.ClosingHandler;
            this.SaveResult.Click += this.SaveResultHandler;
        }

    }

    ///<summary>Главные параметры и методы приложения. Главный класс приложения.</summary>
    public static class Program
    {

        ///<summary>Контентное изображение.</summary>
        public static Tensor Content
        {
            get;
            set;
        }

        ///<summary>Стилевое изображение.</summary>
        public static Tensor Style
        {
            get;
            set;
        }

        ///<summary>Холст.</summary>
        public static Tensor X
        {
            get;
            set;
        }

        ///<summary>Loss - net. Стилизационная нейросеть.</summary>
        public static VGG16 Net
        {
            get;
            set;
        }

        ///<summary>Время начала итерации.</summary>
        public static DateTime DT
        {
            get;
            set;
        }

        [STAThread]
        ///<summary>Точка входа.</summary>
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

    }

}