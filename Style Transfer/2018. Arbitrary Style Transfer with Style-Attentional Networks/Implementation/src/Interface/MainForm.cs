//***********************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//***********************************************

//-> Главная форма приложения.

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace NeuralArt
{

    ///<summary>Главная форма приложения.</summary>
    public sealed partial class MainForm : Form
    {

        ///<summary>Панель параметров контентного изображения.</summary>
        private GroupBox ContentBox;

        ///<summary>Панель параметров стилевого изображения.</summary>
        private GroupBox StyleBox;

        ///<summary>Панель параметров результатирующего изображения.</summary>
        private GroupBox ResultBox;

        ///<summary>Контентное изображение.</summary>
        private PictureBox ContentImage;

        ///<summary>Стилевое изображение.</summary>
        private PictureBox StyleImage;

        ///<summary>Результатирующее изображение.</summary>
        private PictureBox ResultImage;

        ///<summary>Кнопка открытия диалогового окна выбора контентного изображения.</summary>
        private Button OpenContent;

        ///<summary>Кнопка открытия диалогового окна выбора стилевого изображения.</summary>
        private Button OpenStyle;

        ///<summary>Кнопка запуска/остановки процесса стилизации, либо - открытия диалогового окна сохранения результатирующего изображения.</summary>
        private Button GenerateOrSaveResult;

        ///<summary>Строка прогресса выполнения.</summary>
        private ProgressBar Progress;

        ///<summary>Инициализирует элементы управления формы и саму форму.</summary>
        private void InitializeComponent()
        {
            //-> MainForm
            this.ClientSize = new Size(868, 376);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Arbitrary Style Transfer with Style-Attentional Networks";
            this.Icon = Icon.FromHandle((new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("MainIcon.jpg"))).GetHicon());
            this.MaximizeBox = false;
            //-> ContentBox
            this.ContentBox = new GroupBox();
            this.ContentBox.Size = new Size(276, 321);
            this.ContentBox.Text = "Контент";
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
              this.OpenContent.Text = "Открыть изображение";
              this.ContentBox.Controls.Add(this.OpenContent);
            this.Controls.Add(this.ContentBox);
            //-> StyleBox
            this.StyleBox = new GroupBox();
            this.StyleBox.Size = new Size(276, 321);
            this.StyleBox.Text = "Стиль";
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
              this.OpenStyle.Text = "Открыть изображение";
              this.StyleBox.Controls.Add(this.OpenStyle);
            this.Controls.Add(this.StyleBox);
            //-> ResultBox
            this.ResultBox = new GroupBox();
            this.ResultBox.Size = new Size(276, 321);
            this.ResultBox.Text = "Результат";
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
              this.GenerateOrSaveResult.Text = "Запустить процесс";
              this.ResultBox.Controls.Add(this.GenerateOrSaveResult);
            this.Controls.Add(this.ResultBox);
            //-> Progress
            this.Progress = new ProgressBar();
            this.Progress.Size = new Size(848, 25);
            this.Progress.Top = 341;
            this.Progress.Left = 10;
            this.Controls.Add(this.Progress);
        }

    }

}