using System;
using System.Windows.Forms;

namespace парсер
{
    public partial class FormРедакторПравила : Form
    {
        public Правило Rule { get; private set; }

        public FormРедакторПравила(Правило rule)
        {
            Rule = rule;
            InitializeComponent();

            // Заполняем поля значениями правила
            txtIf.Text = Rule.Условие;
            cmbDo.Items.AddRange(new string[]
            {
                "Перенос после",
                "Перенос перед",
                "Удалить",
                "Заменить",
                "Оставить строки",
                "Удалить строки",
                "Объединить через запятую"
            });
            cmbDo.SelectedItem = Rule.Действие;
            txtParam.Text = Rule.Параметр;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Rule.Условие = txtIf.Text;
            Rule.Действие = cmbDo.SelectedItem?.ToString() ?? Rule.Действие;
            Rule.Параметр = txtParam.Text;

            this.Close();
        }
    }
}
