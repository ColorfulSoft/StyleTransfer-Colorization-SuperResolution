//*************************************************************************************************
//* (C) ColorfulSoft, 2019. Все права защищены.
//*************************************************************************************************

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace NeuralArt
{

    ///<summary>Главная форма приложения.</summary>
    public sealed partial class MainForm : Form
    {

        ///<summary>Поле настроек контента.</summary>
        public GroupBox ContentSettings
        {
            get;
            set;
        }

        ///<summary>Поле настроек результата.</summary>
        public GroupBox ResultSettings
        {
            get;
            set;
        }

        ///<summary>Поле настроек стиля.</summary>
        public GroupBox StyleSettings
        {
            get;
            set;
        }

        ///<summary>Контентное изображение.</summary>
        public PictureBox ContentImageBox
        {
            get;
            set;
        }

        ///<summary>Стилизованное изображение.</summary>
        public PictureBox ResultImageBox
        {
            get;
            set;
        }

        ///<summary>Стилевое изображение.</summary>
        public PictureBox StyleImageBox
        {
            get;
            set;
        }

        ///<summary>Кнопка открытия контентного изображения.</summary>
        public Button OpenContent
        {
            get;
            set;
        }

        ///<summary>Кнопка сохранения стилизованного изображения.</summary>
        public Button SaveResult
        {
            get;
            set;
        }

        ///<summary>Кнопка открытия стилевого изображения.</summary>
        public Button OpenStyle
        {
            get;
            set;
        }

        ///<summary>Кнопка запуска процесса.</summary>
        public Button StartProcess
        {
            get;
            set;
        }

        ///<summary>Вывод параметров итерации.</summary>
        public Label Iteration
        {
            get;
            set;
        }

        ///<summary>Кнопка остановки процесса.</summary>
        public Button StopProcess
        {
            get;
            set;
        }

        ///<summary>Инициализирует форму и элементы управления формы.</summary>
        public void Initialize()
        {
            //-> MainForm
            this.Text = "C# implementation of Gatys's neural style algorithm.";
            this.Icon = Icon.FromHandle((new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("MainIcon.jpg"))).GetHicon());
            this.ClientSize = new Size(868, 376);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //-> ContentSettings
            this.ContentSettings = new GroupBox();
            this.ContentSettings.Text = "Контент";
            this.ContentSettings.Size = new Size(276, 321);
            this.ContentSettings.Top = 10;
            this.ContentSettings.Left = 10;
            //--------------------------------------------------
              //-> ContentImageBox
              this.ContentImageBox = new PictureBox();
              this.ContentImageBox.Size = new Size(256, 256);
              this.ContentImageBox.SizeMode = PictureBoxSizeMode.Zoom;
              this.ContentImageBox.Left = 10;
              this.ContentImageBox.Top = 20;
              this.ContentImageBox.BorderStyle = BorderStyle.FixedSingle;
              this.ContentSettings.Controls.Add(this.ContentImageBox);
              //-> OpenContent
              this.OpenContent = new Button();
              this.OpenContent.Text = "Открыть контентное изображение";
              this.OpenContent.Size = new Size(256, 25);
              this.OpenContent.Left = 10;
              this.OpenContent.Top = 286;
              this.ContentSettings.Controls.Add(this.OpenContent);
            //--------------------------------------------------
            this.Controls.Add(this.ContentSettings);
            //-> ResultSettings
            this.ResultSettings = new GroupBox();
            this.ResultSettings.Text = "Результат";
            this.ResultSettings.Size = new Size(276, 321);
            this.ResultSettings.Top = 10;
            this.ResultSettings.Left = 296;
            //--------------------------------------------------
              //-> ResultImageBox
              this.ResultImageBox = new PictureBox();
              this.ResultImageBox.Size = new Size(256, 256);
              this.ResultImageBox.SizeMode = PictureBoxSizeMode.Zoom;
              this.ResultImageBox.Left = 10;
              this.ResultImageBox.Top = 20;
              this.ResultImageBox.BorderStyle = BorderStyle.FixedSingle;
              this.ResultSettings.Controls.Add(this.ResultImageBox);
              //-> SaveResult
              this.SaveResult = new Button();
              this.SaveResult.Text = "Сохранить результат";
              this.SaveResult.Size = new Size(256, 25);
              this.SaveResult.Left = 10;
              this.SaveResult.Top = 286;
              this.ResultSettings.Controls.Add(this.SaveResult);
            //--------------------------------------------------
            this.Controls.Add(this.ResultSettings);
            //-> StyleSettings
            this.StyleSettings = new GroupBox();
            this.StyleSettings.Text = "Стиль";
            this.StyleSettings.Size = new Size(276, 321);
            this.StyleSettings.Top = 10;
            this.StyleSettings.Left = 582;
            //--------------------------------------------------
              //-> StyleImageBox
              this.StyleImageBox = new PictureBox();
              this.StyleImageBox.Size = new Size(256, 256);
              this.StyleImageBox.SizeMode = PictureBoxSizeMode.Zoom;
              this.StyleImageBox.Left = 10;
              this.StyleImageBox.Top = 20;
              this.StyleImageBox.BorderStyle = BorderStyle.FixedSingle;
              this.StyleSettings.Controls.Add(this.StyleImageBox);
              //-> OpenStyle
              this.OpenStyle = new Button();
              this.OpenStyle.Text = "Открыть стилевое изображение";
              this.OpenStyle.Size = new Size(256, 25);
              this.OpenStyle.Left = 10;
              this.OpenStyle.Top = 286;
              this.StyleSettings.Controls.Add(this.OpenStyle);
            //--------------------------------------------------
            this.Controls.Add(this.StyleSettings);
            //-> StartProcess
            this.StartProcess = new Button();
            this.StartProcess.Text = "Запустить итеративный процесс";
            this.StartProcess.Size = new Size(276, 25);
            this.StartProcess.Left = 10;
            this.StartProcess.Top = 341;
            this.Controls.Add(this.StartProcess);
            //-> Iteration
            this.Iteration = new Label();
            this.Iteration.Text = "Итерация: 0; Время: 0";
            this.Iteration.TextAlign = ContentAlignment.MiddleCenter;
            this.Iteration.Size = new Size(276, 25);
            this.Iteration.Left = 296;
            this.Iteration.Top = 341;
            this.Controls.Add(this.Iteration);
            //-> StopProcess
            this.StopProcess = new Button();
            this.StopProcess.Text = "Остановить итеративный процесс";
            this.StopProcess.Enabled = false;
            this.StopProcess.Size = new Size(276, 25);
            this.StopProcess.Left = 582;
            this.StopProcess.Top = 341;
            this.Controls.Add(this.StopProcess);
        }

    }

}