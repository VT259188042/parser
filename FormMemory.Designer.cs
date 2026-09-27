using System.Drawing;
using System.Windows.Forms;

namespace парсер
{
    partial class FormMemory
    {
        public System.ComponentModel.IContainer components = null;

        public Button btnСброс;

        public PictureBox[] индикаторы;
        public Button[] кнопки;
        public TextBox[] вывод;
        public TextBox основнойБуфер;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.btnСброс = new Button();
            this.SuspendLayout();

            // btnСброс
            this.btnСброс.Text = "СБРОС ПАМЯТИ";
            this.btnСброс.Location = new Point(40, 340);
            this.btnСброс.Size = new Size(150, 30);
            this.btnСброс.Name = "btnСброс";
            this.btnСброс.TabIndex = 0;
            this.btnСброс.Click += new System.EventHandler(this.btnСброс_Click);

            // FormMemory
            this.ClientSize = new Size(600, 400);
            this.Controls.Add(this.btnСброс);
            this.Name = "FormMemory";
            this.Text = "Память буфера";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
        }
    }
}