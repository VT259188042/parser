using System.Drawing;
using System.Windows.Forms;

namespace парсер
{
    partial class Form1
    {
        private Label lblСправка;

        public TextBox txtВходнойТекст;
        public TextBox txtНачалоПоиска;
        public TextBox txtНаследникИмени;
        public TextBox txtОкончаниеПоиска;
        public TextBox txtПуть;
        public Button сохранитьВОдинФайл;
        public Button сохранитьВНумерованные;
        public Button читатБуфер;
        public Button очиститьБуфер;
        public Button ВыделитБлоки;
        public Button просмотрБлоков;
        public Button сохраанитьФайлы;
        public Button памятьБуфера;
        public Button ВыборПутиСохранения;

        public Label lblНачало;
        public Label lblИмя;
        public Label lblКонец;
        public Label lblПуть;
        public Button кнопкаНастройки;
        public ToolTip toolTip1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        public void InitializeComponent()
        {
            // сохранитьВОдинФайл
            this.сохранитьВОдинФайл = new Button();
            this.сохранитьВОдинФайл.Location = new Point(612, 524);
            this.сохранитьВОдинФайл.Name = "сохранитьВОдинФайл";
            this.сохранитьВОдинФайл.Size = new Size(140, 35);
            this.сохранитьВОдинФайл.TabIndex = 29;
            this.сохранитьВОдинФайл.Text = "Все в один файл";
            this.Controls.Add(this.сохранитьВОдинФайл);

            // сохранитьВНумерованные
            this.сохранитьВНумерованные = new Button();
            this.сохранитьВНумерованные.Location = new Point(762, 524);
            this.сохранитьВНумерованные.Name = "сохранитьВНумерованные";
            this.сохранитьВНумерованные.Size = new Size(180, 35);
            this.сохранитьВНумерованные.TabIndex = 30;
            this.сохранитьВНумерованные.Text = "Все в файлы 001,002,...";
            this.Controls.Add(this.сохранитьВНумерованные);

            this.components = new System.ComponentModel.Container();
            this.lblСправка = new System.Windows.Forms.Label();
            this.кнопкаНастройки = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtВходнойТекст = new System.Windows.Forms.TextBox();
            this.txtНачалоПоиска = new System.Windows.Forms.TextBox();
            this.txtНаследникИмени = new System.Windows.Forms.TextBox();
            this.txtОкончаниеПоиска = new System.Windows.Forms.TextBox();
            this.txtПуть = new System.Windows.Forms.TextBox();
            this.lblНачало = new System.Windows.Forms.Label();
            this.lblИмя = new System.Windows.Forms.Label();
            this.lblКонец = new System.Windows.Forms.Label();
            this.lblПуть = new System.Windows.Forms.Label();
            this.читатБуфер = new System.Windows.Forms.Button();
            this.очиститьБуфер = new System.Windows.Forms.Button();
            this.ВыделитБлоки = new System.Windows.Forms.Button();
            this.просмотрБлоков = new System.Windows.Forms.Button();
            this.сохраанитьФайлы = new System.Windows.Forms.Button();
            this.памятьБуфера = new System.Windows.Forms.Button();
            this.ВыборПутиСохранения = new System.Windows.Forms.Button();
            this.форма_правка = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblСправка
            // 
            this.lblСправка.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblСправка.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.lblСправка.Location = new System.Drawing.Point(12, 559);
            this.lblСправка.Name = "lblСправка";
            this.lblСправка.Size = new System.Drawing.Size(860, 20);
            this.lblСправка.TabIndex = 0;
            // 
            // кнопкаНастройки
            // 
            this.кнопкаНастройки.Location = new System.Drawing.Point(462, 479);
            this.кнопкаНастройки.Name = "кнопкаНастройки";
            this.кнопкаНастройки.Size = new System.Drawing.Size(140, 35);
            this.кнопкаНастройки.TabIndex = 16;
            this.кнопкаНастройки.Text = "Настройки";
            // 
            // txtВходнойТекст
            // 
            this.txtВходнойТекст.Location = new System.Drawing.Point(15, 15);
            this.txtВходнойТекст.Multiline = true;
            this.txtВходнойТекст.Name = "txtВходнойТекст";
            this.txtВходнойТекст.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtВходнойТекст.Size = new System.Drawing.Size(860, 300);
            this.txtВходнойТекст.TabIndex = 0;
            // 
            // txtНачалоПоиска
            // 
            this.txtНачалоПоиска.Location = new System.Drawing.Point(147, 334);
            this.txtНачалоПоиска.Name = "txtНачалоПоиска";
            this.txtНачалоПоиска.Size = new System.Drawing.Size(720, 20);
            this.txtНачалоПоиска.TabIndex = 2;
            // 
            // txtНаследникИмени
            // 
            this.txtНаследникИмени.Location = new System.Drawing.Point(147, 366);
            this.txtНаследникИмени.Name = "txtНаследникИмени";
            this.txtНаследникИмени.Size = new System.Drawing.Size(720, 20);
            this.txtНаследникИмени.TabIndex = 4;
            // 
            // txtОкончаниеПоиска
            // 
            this.txtОкончаниеПоиска.Location = new System.Drawing.Point(147, 398);
            this.txtОкончаниеПоиска.Name = "txtОкончаниеПоиска";
            this.txtОкончаниеПоиска.Size = new System.Drawing.Size(720, 20);
            this.txtОкончаниеПоиска.TabIndex = 6;
            // 
            // txtПуть
            // 
            this.txtПуть.Location = new System.Drawing.Point(147, 438);
            this.txtПуть.Name = "txtПуть";
            this.txtПуть.Size = new System.Drawing.Size(635, 20);
            this.txtПуть.TabIndex = 8;
            // 
            // lblНачало
            // 
            this.lblНачало.Location = new System.Drawing.Point(12, 336);
            this.lblНачало.Name = "lblНачало";
            this.lblНачало.Size = new System.Drawing.Size(130, 23);
            this.lblНачало.TabIndex = 17;
            this.lblНачало.Text = "Начало поиска:";
            // 
            // lblИмя
            // 
            this.lblИмя.Location = new System.Drawing.Point(12, 368);
            this.lblИмя.Name = "lblИмя";
            this.lblИмя.Size = new System.Drawing.Size(130, 23);
            this.lblИмя.TabIndex = 18;
            this.lblИмя.Text = "Наследник имени:";
            // 
            // lblКонец
            // 
            this.lblКонец.Location = new System.Drawing.Point(12, 400);
            this.lblКонец.Name = "lblКонец";
            this.lblКонец.Size = new System.Drawing.Size(130, 23);
            this.lblКонец.TabIndex = 19;
            this.lblКонец.Text = "Окончание поиска:";
            // 
            // lblПуть
            // 
            this.lblПуть.Location = new System.Drawing.Point(12, 440);
            this.lblПуть.Name = "lblПуть";
            this.lblПуть.Size = new System.Drawing.Size(130, 23);
            this.lblПуть.TabIndex = 20;
            this.lblПуть.Text = "Путь сохранения:";
            // 
            // читатБуфер
            // 
            this.читатБуфер.Location = new System.Drawing.Point(12, 479);
            this.читатБуфер.Name = "читатБуфер";
            this.читатБуфер.Size = new System.Drawing.Size(140, 35);
            this.читатБуфер.TabIndex = 21;
            this.читатБуфер.Text = "Читать буфер";
            // 
            // очиститьБуфер
            // 
            this.очиститьБуфер.Location = new System.Drawing.Point(162, 479);
            this.очиститьБуфер.Name = "очиститьБуфер";
            this.очиститьБуфер.Size = new System.Drawing.Size(140, 35);
            this.очиститьБуфер.TabIndex = 22;
            this.очиститьБуфер.Text = "Очистить";
            // 
            // ВыделитБлоки
            // 
            this.ВыделитБлоки.Location = new System.Drawing.Point(312, 479);
            this.ВыделитБлоки.Name = "ВыделитБлоки";
            this.ВыделитБлоки.Size = new System.Drawing.Size(140, 35);
            this.ВыделитБлоки.TabIndex = 23;
            this.ВыделитБлоки.Text = "Выделить блоки";
            // 
            // просмотрБлоков
            // 
            this.просмотрБлоков.Location = new System.Drawing.Point(12, 524);
            this.просмотрБлоков.Name = "просмотрБлоков";
            this.просмотрБлоков.Size = new System.Drawing.Size(140, 35);
            this.просмотрБлоков.TabIndex = 24;
            this.просмотрБлоков.Text = "Просмотр";
            // 
            // сохраанитьФайлы
            // 
            this.сохраанитьФайлы.Location = new System.Drawing.Point(162, 524);
            this.сохраанитьФайлы.Name = "сохраанитьФайлы";
            this.сохраанитьФайлы.Size = new System.Drawing.Size(140, 35);
            this.сохраанитьФайлы.TabIndex = 25;
            this.сохраанитьФайлы.Text = "Сохранить";
            // 
            // памятьБуфера
            // 
            this.памятьБуфера.Location = new System.Drawing.Point(312, 524);
            this.памятьБуфера.Name = "памятьБуфера";
            this.памятьБуфера.Size = new System.Drawing.Size(140, 35);
            this.памятьБуфера.TabIndex = 26;
            this.памятьБуфера.Text = "Память";
            this.памятьБуфера.Click += new System.EventHandler(this.памятьБуфера_Click_1);
            // 
            // ВыборПутиСохранения
            // 
            this.ВыборПутиСохранения.Location = new System.Drawing.Point(792, 437);
            this.ВыборПутиСохранения.Name = "ВыборПутиСохранения";
            this.ВыборПутиСохранения.Size = new System.Drawing.Size(75, 23);
            this.ВыборПутиСохранения.TabIndex = 9;
            this.ВыборПутиСохранения.Text = "Обзор";
            // 
            // форма_правка
            // 
            this.форма_правка.Location = new System.Drawing.Point(462, 524);
            this.форма_правка.Name = "форма_правка";
            this.форма_правка.Size = new System.Drawing.Size(140, 35);
            this.форма_правка.TabIndex = 27;
            this.форма_правка.Text = "Правка";
            this.форма_правка.Click += new System.EventHandler(this.форма_правка_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(985, 590);
            this.Controls.Add(this.форма_правка);
            this.Controls.Add(this.lblСправка);
            this.Controls.Add(this.кнопкаНастройки);
            this.Controls.Add(this.txtВходнойТекст);
            this.Controls.Add(this.lblНачало);
            this.Controls.Add(this.txtНачалоПоиска);
            this.Controls.Add(this.lblИмя);
            this.Controls.Add(this.txtНаследникИмени);
            this.Controls.Add(this.lblКонец);
            this.Controls.Add(this.txtОкончаниеПоиска);
            this.Controls.Add(this.lblПуть);
            this.Controls.Add(this.txtПуть);
            this.Controls.Add(this.ВыборПутиСохранения);
            this.Controls.Add(this.читатБуфер);
            this.Controls.Add(this.очиститьБуфер);
            this.Controls.Add(this.ВыделитБлоки);
            this.Controls.Add(this.просмотрБлоков);
            this.Controls.Add(this.сохраанитьФайлы);
            this.Controls.Add(this.памятьБуфера);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FORM PARSER";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.ComponentModel.IContainer components;
        public Button форма_правка;
    }
}