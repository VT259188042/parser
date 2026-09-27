using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace парсер
{
    public partial class FormПравка : Form
    {
        private TextBox основнойБуфер;

        private Panel canvas;
        private Button btnAdd;
        private Button btnRun;
        private RichTextBox txtРезультат;

        private List<RuleBlock> blocks = new List<RuleBlock>();
        private List<Connection> connections = new List<Connection>();

        private RuleBlock dragFromBlock = null;
        private Point dragLineStart;
        private Point dragLineEnd;
        private bool isDraggingLine = false;

        public FormПравка(TextBox входнойБуфер)
        {
            основнойБуфер = входнойБуфер;
            InitializeComponent();      // твой дизайнерный метод
            ПостроитьИнтерфейс();       // наша ручная разметка
        }

        private void ПостроитьИнтерфейс()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);

            canvas = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(800, 650),
                BackColor = Color.FromArgb(40, 40, 40)
            };
            canvas.Paint += Canvas_Paint;
            canvas.MouseMove += canvas_MouseMove;

            btnAdd = new Button
            {
                Text = "Добавить правило",
                Location = new Point(830, 20),
                Size = new Size(240, 40)
            };
            btnAdd.Click += BtnAdd_Click;

            btnRun = new Button
            {
                Text = "Запустить цепочку",
                Location = new Point(830, 70),
                Size = new Size(240, 40)
            };
            btnRun.Click += BtnRun_Click;

            txtРезультат = new RichTextBox
            {
                Location = new Point(830, 130),
                Size = new Size(240, 530),
                ReadOnly = true,
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White
            };

            this.Controls.Add(canvas);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnRun);
            this.Controls.Add(txtРезультат);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var pr = new Правило
            {
                Условие = "",
                Действие = "Выберете действие",
                Параметр = ""
            };

            var block = new RuleBlock(pr, this)
            {
                Location = new Point(50 + blocks.Count * 20, 50 + blocks.Count * 20)
            };

            blocks.Add(block);
            canvas.Controls.Add(block);

            if (blocks.Count > 1)
                connections.Add(new Connection(blocks[blocks.Count - 2], block));

            canvas.Invalidate();
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            string текст = основнойБуфер.Text;

            foreach (var block in blocks)
            {
                string старый = текст;
                текст = ПрименитьПравило(текст, block.Rule);

                bool применено = !string.Equals(старый, текст);
                block.ОтметитьПрименение(применено);
            }

            txtРезультат.Text = текст;
        }

        private string ПрименитьПравило(string текст, Правило pr)
        {
            if (string.IsNullOrWhiteSpace(pr.Условие))
                return текст;

            switch (pr.Действие)
            {
                case "Перенос после":
                    return текст.Replace(pr.Условие, pr.Условие + "\r\n" + pr.Параметр);

                case "Перенос перед":
                    return текст.Replace(pr.Условие, "\r\n" + pr.Параметр + pr.Условие);

                case "Удалить":
                    return текст.Replace(pr.Условие, "");

                case "Заменить":
                    return текст.Replace(pr.Условие, pr.Параметр);

                case "Оставить строки":
                    return string.Join("\r\n",
                        текст.Split('\n').Where(x => x.Contains(pr.Условие)));

                case "Удалить строки":
                    return string.Join("\r\n",
                        текст.Split('\n').Where(x => !x.Contains(pr.Условие)));

                case "Объединить через запятую":
                    var matches = текст.Split('\n')
                                       .Where(x => x.Contains(pr.Условие))
                                       .Select(x => x.Trim());
                    return string.Join(", ", matches);
            }

            return текст;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (var c in connections)
                c.Draw(e.Graphics);

            if (isDraggingLine)
            {
                using (var pen = new Pen(Color.Yellow, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawLine(pen, dragLineStart, dragLineEnd);
                }
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingLine)
            {
                dragLineEnd = e.Location;
                canvas.Invalidate();
            }
        }

        public void НачатьСоединение(RuleBlock from)
        {
            dragFromBlock = from;
            isDraggingLine = true;

            dragLineStart = new Point(
                from.Left + from.Width,
                from.Top + from.Height / 2
            );
        }

        public void ЗавершитьСоединение(RuleBlock to)
        {
            if (!isDraggingLine || dragFromBlock == null || dragFromBlock == to)
                return;

            connections.Add(new Connection(dragFromBlock, to));

            isDraggingLine = false;
            dragFromBlock = null;

            canvas.Invalidate();
        }

        public void УдалитьСвязиДляБлока(RuleBlock block)
        {
            connections.RemoveAll(c => c.From == block || c.To == block);
            canvas.Invalidate();
        }
    }
}
