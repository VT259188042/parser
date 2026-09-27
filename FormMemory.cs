using System;
using System.Drawing;
using System.Windows.Forms;

namespace парсер
{
    public partial class FormMemory : Form
    {
        public string[] память = new string[10];

        // Конструктор без параметров — нужен дизайнеру WinForms
        public FormMemory()
        {
            InitializeComponent();
            ПрименитьСтиль();
            СоздатьСлоты();
        }

        // Основной конструктор
        public FormMemory(TextBox основнойБуфер) : this()
        {
            this.основнойБуфер = основнойБуфер;
        }

        // ================================================================
        //  СТИЛИЗАЦИЯ
        // ================================================================
        public void ПрименитьСтиль()
        {
            Color bgForm    = Color.FromArgb(30, 30, 30);
            Color bgInput   = Color.FromArgb(45, 45, 48);
            Color bgBtn     = Color.FromArgb(50, 50, 55);
            Color bgBtnHov  = Color.FromArgb(75, 75, 80);
            Color bgBtnDown = Color.FromArgb(90, 90, 95);
            Color fgLight   = Color.FromArgb(220, 220, 220);
            Color borderClr = Color.FromArgb(60, 60, 60);

            this.BackColor = bgForm;
            this.ForeColor = fgLight;

            btnСброс.BackColor = bgBtn;
            btnСброс.ForeColor = fgLight;
            btnСброс.FlatStyle = FlatStyle.Flat;
            btnСброс.FlatAppearance.BorderSize = 1;
            btnСброс.FlatAppearance.BorderColor = borderClr;
            btnСброс.FlatAppearance.MouseOverBackColor = bgBtnHov;
            btnСброс.FlatAppearance.MouseDownBackColor = bgBtnDown;
            btnСброс.Font = new Font("Segoe UI", 9F);
        }

        // ================================================================
        //  СОЗДАНИЕ 10 СЛОТОВ (вручную, без дизайнера)
        // ================================================================
        public void СоздатьСлоты()
        {
            индикаторы = new PictureBox[10];
            кнопки     = new Button[10];
            вывод      = new TextBox[10];

            Color bgInput  = Color.FromArgb(45, 45, 48);
            Color bgBtn    = Color.FromArgb(50, 50, 55);
            Color bgBtnHov = Color.FromArgb(75, 75, 80);
            Color fgLight  = Color.FromArgb(220, 220, 220);
            Color borderClr= Color.FromArgb(60, 60, 60);

            for (int i = 0; i < 10; i++)
            {
                int y = 20 + i * 30;

                индикаторы[i] = new PictureBox
                {
                    Location = new Point(10, y),
                    Size = new Size(20, 20),
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Gray   // вместо Properties.Resources.пусто
                };

                кнопки[i] = new Button
                {
                    Text = $"Сектор {i + 1}",
                    Location = new Point(40, y - 2),
                    Size = new Size(100, 24),
                    Tag = i,
                    BackColor = bgBtn,
                    ForeColor = fgLight,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F)
                };
                кнопки[i].FlatAppearance.BorderSize = 1;
                кнопки[i].FlatAppearance.BorderColor = borderClr;
                кнопки[i].FlatAppearance.MouseOverBackColor = bgBtnHov;
                кнопки[i].Click += Слот_Click;
                кнопки[i].MouseDown += Слот_ПравыйКлик;

                вывод[i] = new TextBox
                {
                    Location = new Point(150, y - 2),
                    Size = new Size(420, 24),
                    ReadOnly = true,
                    BackColor = bgInput,
                    ForeColor = fgLight,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Consolas", 9F)
                };

                this.Controls.Add(индикаторы[i]);
                this.Controls.Add(кнопки[i]);
                this.Controls.Add(вывод[i]);
            }
        }

        // ================================================================
        //  ЛОГИКА СЛОТОВ
        // ================================================================
        public void Слот_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int номер = Convert.ToInt32(btn.Tag);

            if (string.IsNullOrWhiteSpace(память[номер]))
                СохранитьВСлот(номер);
            else
                ЗагрузитьИзСлота(номер);
        }

        public void Слот_ПравыйКлик(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button btn = sender as Button;
                int номер = Convert.ToInt32(btn.Tag);
                ОчиститьСлот(номер);
            }
        }

        public void СохранитьВСлот(int номер)
        {
            if (основнойБуфер == null) return;
            память[номер] = основнойБуфер.Text;
            Обновить();
        }

        public void ЗагрузитьИзСлота(int номер)
        {
            if (основнойБуфер == null) return;
            основнойБуфер.Text = память[номер];
        }

        public void ОчиститьСлот(int номер)
        {
            память[номер] = "";
            Обновить();
        }

        public void btnСброс_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < память.Length; i++)
                память[i] = "";
            Обновить();
        }

        public void Обновить()
        {
            for (int i = 0; i < 10; i++)
            {
                if (string.IsNullOrWhiteSpace(память[i]))
                {
                    индикаторы[i].BackColor = Color.Gray;
                    вывод[i].Text = "";
                }
                else
                {
                    индикаторы[i].BackColor = Color.LimeGreen;
                    вывод[i].Text = память[i].Length > 40
                        ? память[i].Substring(0, 40) + "..."
                        : память[i];
                }
            }
        }
    }
}