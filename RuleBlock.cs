using System;
using System.Drawing;
using System.Windows.Forms;

namespace парсер
{
    public class RuleBlock : UserControl
    {
        public Правило Rule { get; private set; }
        private FormРедакторПравила editorForm;

        private Label lblCondition;
        private Label lblAction;
        private Button btnEdit;
        private Button btnDelete;

        public Panel InPoint;
        public Panel OutPoint;

        private bool dragging = false;
        private Point dragStart;

        private FormПравка parentForm;

        public RuleBlock(Правило rule, FormПравка parent)
        {
            Rule = rule;
            parentForm = parent;
            Initialize();
        }

        private void Initialize()
        {
            this.Size = new Size(240, 120);
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.BorderStyle = BorderStyle.FixedSingle;

            var lblIf = new Label
            {
                Text = "ЕСЛИ:",
                Location = new Point(10, 10),
                ForeColor = Color.DeepSkyBlue
            };

            lblCondition = new Label
            {
                Text = Rule.Условие,
                Location = new Point(70, 10),
                ForeColor = Color.White,
                AutoSize = true
            };

            var lblThen = new Label
            {
                Text = "ТО:",
                Location = new Point(10, 35),
                ForeColor = Color.Orange
            };

            lblAction = new Label
            {
                Text = Rule.Действие,
                Location = new Point(70, 35),
                ForeColor = Color.White,
                AutoSize = true
            };

            btnEdit = new Button
            {
                Text = "Редактировать",
                Location = new Point(10, 70),
                Size = new Size(100, 30)
            };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button
            {
                Text = "Удалить",
                Location = new Point(120, 70),
                Size = new Size(80, 30)
            };
            btnDelete.Click += (s, e) =>
            {
                parentForm.УдалитьСвязиДляБлока(this);
                this.Parent.Controls.Remove(this);
            };

            InPoint = new Panel
            {
                Size = new Size(10, 10),
                BackColor = Color.DarkGray,
                Location = new Point(0, this.Height / 2 - 5),
                Cursor = Cursors.Cross
            };
            InPoint.MouseDown += (s, e) => parentForm.ЗавершитьСоединение(this);

            OutPoint = new Panel
            {
                Size = new Size(10, 10),
                BackColor = Color.DarkGray,
                Location = new Point(this.Width - 10, this.Height / 2 - 5),
                Cursor = Cursors.Cross
            };
            OutPoint.MouseDown += (s, e) => parentForm.НачатьСоединение(this);

            this.Controls.Add(lblIf);
            this.Controls.Add(lblCondition);
            this.Controls.Add(lblThen);
            this.Controls.Add(lblAction);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(InPoint);
            this.Controls.Add(OutPoint);

            this.MouseDown += Block_MouseDown;
            this.MouseMove += Block_MouseMove;
            this.MouseUp += Block_MouseUp;
        }

        private void Block_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragStart = e.Location;
        }

        private void Block_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.Left += e.X - dragStart.X;
                this.Top += e.Y - dragStart.Y;
                this.Parent.Invalidate();
            }
        }

        private void Block_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (editorForm != null && !editorForm.IsDisposed)
            {
                editorForm.Focus();
                return;
            }

            editorForm = new FormРедакторПравила(Rule);

            editorForm.FormClosed += (s, args) =>
            {
                lblCondition.Text = Rule.Условие;
                lblAction.Text = Rule.Действие;
                editorForm = null;
            };

            editorForm.Show();
        }

        public void ОтметитьПрименение(bool применено)
        {
            this.BorderStyle = применено ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
        }
    }
}
