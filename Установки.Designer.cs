using System.Drawing;
using System.Windows.Forms;

namespace парсер
{
    partial class Установки
    {
        public System.ComponentModel.IContainer components = null;

        public ComboBox cmbПрофили;
        public Button btnСохранитьТекущий;
        public Button btnЗагрузить;
        public Button btnУдалить;
        public Button btnЗакрыть;
        public Label lblВыберите;
        public Label lblИнфо;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.lblВыберите = new Label();
            this.cmbПрофили = new ComboBox();
            this.btnСохранитьТекущий = new Button();
            this.btnЗагрузить = new Button();
            this.btnУдалить = new Button();
            this.btnЗакрыть = new Button();
            this.lblИнфо = new Label();

            this.SuspendLayout();

            // lblВыберите
            this.lblВыберите.Text = "Профиль настроек поиска:";
            this.lblВыберите.Location = new Point(15, 20);
            this.lblВыберите.Size = new Size(200, 23);
            this.lblВыберите.Name = "lblВыберите";

            // cmbПрофили
            this.cmbПрофили.Location = new Point(15, 45);
            this.cmbПрофили.Size = new Size(360, 24);
            this.cmbПрофили.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbПрофили.Name = "cmbПрофили";

            // btnСохранитьТекущий
            this.btnСохранитьТекущий.Text = "Сохранить текущие";
            this.btnСохранитьТекущий.Location = new Point(15, 85);
            this.btnСохранитьТекущий.Size = new Size(170, 32);
            this.btnСохранитьТекущий.Name = "btnСохранитьТекущий";

            // btnЗагрузить
            this.btnЗагрузить.Text = "Загрузить выбранный";
            this.btnЗагрузить.Location = new Point(205, 85);
            this.btnЗагрузить.Size = new Size(170, 32);
            this.btnЗагрузить.Name = "btnЗагрузить";

            // btnУдалить
            this.btnУдалить.Text = "Удалить";
            this.btnУдалить.Location = new Point(15, 125);
            this.btnУдалить.Size = new Size(170, 32);
            this.btnУдалить.Name = "btnУдалить";

            // btnЗакрыть
            this.btnЗакрыть.Text = "Закрыть";
            this.btnЗакрыть.Location = new Point(205, 125);
            this.btnЗакрыть.Size = new Size(170, 32);
            this.btnЗакрыть.Name = "btnЗакрыть";

            // lblИнфо
            this.lblИнфо.Text = "Файл профилей: profiles.json (рядом с программой)";
            this.lblИнфо.Location = new Point(15, 170);
            this.lblИнфо.Size = new Size(360, 40);
            this.lblИнфо.Name = "lblИнфо";

            // Установки
            this.ClientSize = new Size(395, 215);
            this.Controls.Add(this.lblВыберите);
            this.Controls.Add(this.cmbПрофили);
            this.Controls.Add(this.btnСохранитьТекущий);
            this.Controls.Add(this.btnЗагрузить);
            this.Controls.Add(this.btnУдалить);
            this.Controls.Add(this.btnЗакрыть);
            this.Controls.Add(this.lblИнфо);
            this.Name = "Установки";
            this.Text = "Установки — профили поиска";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.ResumeLayout(false);
        }
    }
}