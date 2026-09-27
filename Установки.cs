using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace парсер
{
    public partial class Установки : Form
    {
        // Ссылка на Form1 — чтобы читать/писать его поля
        public readonly Form1 _главная;

        // Путь к файлу профилей
        public readonly string _файлПрофилей;

        // Список профилей в памяти
        public List<ПрофильПоиска> _профили = new List<ПрофильПоиска>();

        // Конструктор без параметров — для дизайнера
        public Установки()
        {
            InitializeComponent();
            ПрименитьСтиль();
            _файлПрофилей = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "profiles.json");
            ЗагрузитьИзФайла();
            ОбновитьСписок();
        }

        // Основной конструктор
        public Установки(Form1 главная) : this()
        {
            _главная = главная;
        }

        // ================================================================
        //  СТИЛИЗАЦИЯ
        // ================================================================
        public void ПрименитьСтиль()
        {
            Color bgForm   = Color.FromArgb(30, 30, 30);
            Color bgInput  = Color.FromArgb(45, 45, 48);
            Color bgBtn    = Color.FromArgb(50, 50, 55);
            Color bgBtnHov = Color.FromArgb(75, 75, 80);
            Color fgLight  = Color.FromArgb(220, 220, 220);
            Color fgDim    = Color.FromArgb(170, 170, 170);
            Color borderClr= Color.FromArgb(60, 60, 60);

            this.BackColor = bgForm;
            this.ForeColor = fgLight;

            lblВыберите.ForeColor = fgDim;
            lblИнфо.ForeColor = fgDim;

            cmbПрофили.BackColor = bgInput;
            cmbПрофили.ForeColor = fgLight;
            cmbПрофили.FlatStyle = FlatStyle.Flat;
            cmbПрофили.Font = new Font("Segoe UI", 9F);

            Button[] кнопки = { btnСохранитьТекущий, btnЗагрузить, btnУдалить, btnЗакрыть };
            foreach (var b in кнопки)
            {
                b.BackColor = bgBtn;
                b.ForeColor = fgLight;
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 1;
                b.FlatAppearance.BorderColor = borderClr;
                b.FlatAppearance.MouseOverBackColor = bgBtnHov;
                b.Font = new Font("Segoe UI", 9F);
            }

            // Подписки
            btnСохранитьТекущий.Click += btnСохранитьТекущий_Click;
            btnЗагрузить.Click += btnЗагрузить_Click;
            btnУдалить.Click += btnУдалить_Click;
            btnЗакрыть.Click += (s, e) => this.Close();
        }

        // ================================================================
        //  ФАЙЛ
        // ================================================================
        public void ЗагрузитьИзФайла()
        {
            try
            {
                if (File.Exists(_файлПрофилей))
                {
                    string json = File.ReadAllText(_файлПрофилей);
                    var данные = JsonSerializer.Deserialize<СписокПрофилей>(json);
                    _профили = данные?.Профили ?? new List<ПрофильПоиска>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось прочитать файл профилей:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _профили = new List<ПрофильПоиска>();
            }
        }

        public void СохранитьВФайл()
        {
            try
            {
                var данные = new СписокПрофилей { Профили = _профили };
                var опции = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(данные, опции);
                File.WriteAllText(_файлПрофилей, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить файл профилей:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================
        //  СПИСОК
        // ================================================================
        public void ОбновитьСписок()
        {
            cmbПрофили.Items.Clear();
            foreach (var p in _профили)
                cmbПрофили.Items.Add(p.Имя);

            if (cmbПрофили.Items.Count > 0)
                cmbПрофили.SelectedIndex = 0;
        }

        // ================================================================
        //  СОХРАНИТЬ ТЕКУЩИЕ
        // ================================================================
        public void btnСохранитьТекущий_Click(object sender, EventArgs e)
        {
            if (_главная == null)
            {
                MessageBox.Show("Нет ссылки на главную форму.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Спрашиваем имя профиля
            string имя = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите имя профиля:", "Сохранение настроек", "Профиль " + (_профили.Count + 1));

            if (string.IsNullOrWhiteSpace(имя)) return;

            // Если профиль с таким именем уже есть — перезаписываем
            var существующий = _профили.Find(p => p.Имя == имя);
            if (существующий != null)
            {
                существующий.НачалоПоиска    = _главная.txtНачалоПоиска.Text;
                существующий.НаследникИмени  = _главная.txtНаследникИмени.Text;
                существующий.ОкончаниеПоиска = _главная.txtОкончаниеПоиска.Text;
                существующий.ПутьСохранения  = _главная.txtПуть.Text;
            }
            else
            {
                _профили.Add(new ПрофильПоиска
                {
                    Имя = имя,
                    НачалоПоиска    = _главная.txtНачалоПоиска.Text,
                    НаследникИмени  = _главная.txtНаследникИмени.Text,
                    ОкончаниеПоиска = _главная.txtОкончаниеПоиска.Text,
                    ПутьСохранения  = _главная.txtПуть.Text
                });
            }

            СохранитьВФайл();
            ОбновитьСписок();

            // Выделить только что сохранённый профиль
            int idx = _профили.FindIndex(p => p.Имя == имя);
            if (idx >= 0) cmbПрофили.SelectedIndex = idx;
        }

        // ================================================================
        //  ЗАГРУЗИТЬ ВЫБРАННЫЙ
        // ================================================================
        public void btnЗагрузить_Click(object sender, EventArgs e)
        {
            if (_главная == null || cmbПрофили.SelectedIndex < 0) return;

            var p = _профили[cmbПрофили.SelectedIndex];

            _главная.txtНачалоПоиска.Text     = p.НачалоПоиска ?? "";
            _главная.txtНаследникИмени.Text   = p.НаследникИмени ?? "";
            _главная.txtОкончаниеПоиска.Text  = p.ОкончаниеПоиска ?? "";
            _главная.txtПуть.Text             = p.ПутьСохранения ?? "";

            MessageBox.Show($"Профиль «{p.Имя}» загружен в главную форму.",
                "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================================================================
        //  УДАЛИТЬ
        // ================================================================
        public void btnУдалить_Click(object sender, EventArgs e)
        {
            if (cmbПрофили.SelectedIndex < 0) return;

            var p = _профили[cmbПрофили.SelectedIndex];

            var ответ = MessageBox.Show(
                $"Удалить профиль «{p.Имя}»?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ответ != DialogResult.Yes) return;

            _профили.RemoveAt(cmbПрофили.SelectedIndex);
            СохранитьВФайл();
            ОбновитьСписок();
        }
    }
}