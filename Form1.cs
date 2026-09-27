using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using System.Text.RegularExpressions;








namespace парсер
{
    public partial class Form1 : Form
    {
        public List<string> блоки = new List<string>();
        // Form1.cs — в начале класса

        public const string Разделитель = "════════════════════════════════════";
        public Form1()
        {
            InitializeComponent();
            ПрименитьСтиль();
            ПривязатьОбработчики();
        }
        private void ПоказатьСправку(string текст)
        {
            lblСправка.Text = текст;
        }

        private void ОчиститьСправку(object sender, EventArgs e)
        {
            lblСправка.Text = "";
        }

        public void ПривязатьОбработчики()
        {
            сохранитьВОдинФайл.Click += СохранитьВсеВОдинФайл_Click;
            сохранитьВНумерованные.Click += СохранитьВсеВНумерованные_Click;

            кнопкаНастройки.Click += кнопкаНастройки_Click;
            читатБуфер.Click += читатБуфер_Click;
            очиститьБуфер.Click += очиститьБуфер_Click;
            ВыделитБлоки.Click += ВыделитБлоки_Click;
            просмотрБлоков.Click += просмотрБлоков_Click;
            сохраанитьФайлы.Click += сохраанитьФайлы_Click;
            памятьБуфера.Click += памятьБуфера_Click;
            ВыборПутиСохранения.Click += ВыборПутиСохранения_Click;
        }
        public List<string> НайтиБлокиУниверсально(string текст, string начало, string конец)
        {
            var блоки = new List<string>();

            // Если пользователь ввёл регулярку (например, содержит спецсимволы)
            bool isRegex = Regex.IsMatch(начало + конец, @"[\\\*\+\?

\[\]

\(\)\|]");

            if (isRegex)
            {
                // Режим регулярных выражений
                var matches = Regex.Matches(текст, $"{начало}(.*?){конец}", RegexOptions.Singleline);
                foreach (Match m in matches)
                {
                    блоки.Add(m.Value.Trim());
                }
                return блоки;
            }

            // Режим текстового поиска
            int pos = 0;
            while (true)
            {
                int start = текст.IndexOf(начало, pos, StringComparison.OrdinalIgnoreCase);
                if (start == -1) break;

                int end = текст.IndexOf(конец, start + начало.Length, StringComparison.OrdinalIgnoreCase);
                if (end == -1) break;

                string блок = текст.Substring(start, end - start);
                блоки.Add(блок.Trim());
                pos = end + конец.Length;
            }

            // Если ничего не найдено — пробуем баланс скобок
            if (блоки.Count == 0 && начало.Contains("{") && конец.Contains("}"))
            {
                блоки = НайтиБлокиПоБалансу(текст);
            }

            return блоки;
        }
        public List<string> НайтиБлокиПоБалансу(string текст)
        {
            var блоки = new List<string>();
            int depth = 0;
            int start = -1;

            for (int i = 0; i < текст.Length; i++)
            {
                char c = текст[i];

                if (c == '{')
                {
                    if (depth == 0) start = i;
                    depth++;
                }
                else if (c == '}')
                {
                    depth--;
                    if (depth == 0 && start != -1)
                    {
                        string блок = текст.Substring(start, i - start + 1);
                        блоки.Add(блок.Trim());
                        start = -1;
                    }
                }
            }

            return блоки;
        }
        private void ПодсветитьСовпадения(RichTextBox box, string начало, string конец)
        {
            box.SelectAll();
            box.SelectionColor = Color.FromArgb(220, 220, 220);

            int pos = 0;
            while (true)
            {
                int start = box.Text.IndexOf(начало, pos, StringComparison.OrdinalIgnoreCase);
                if (start == -1) break;

                int end = box.Text.IndexOf(конец, start + начало.Length, StringComparison.OrdinalIgnoreCase);
                if (end == -1) break;

                box.Select(start, end - start + конец.Length);
                box.SelectionColor = Color.LimeGreen;
                pos = end + конец.Length;
            }
        }

        public void кнопкаНастройки_Click(object sender, EventArgs e)
        {
            var f = new Установки(this);
            f.ShowDialog(this); // модально, чтобы при закрытии сразу видеть результат
        }
        // ================================================================
        //  СТИЛИЗАЦИЯ (вынесено из InitializeComponent)
        // ================================================================
        public void ПрименитьСтиль()
        {
            txtНачалоПоиска.MouseEnter += (s, e) => ПоказатьСправку("Текст, с которого начинается блок.");
            txtНачалоПоиска.MouseLeave += ОчиститьСправку;

            txtНаследникИмени.MouseEnter += (s, e) => ПоказатьСправку("Ключевое слово, после которого идёт имя класса.");
            txtНаследникИмени.MouseLeave += ОчиститьСправку;

            txtОкончаниеПоиска.MouseEnter += (s, e) => ПоказатьСправку("Текст, которым заканчивается блок.");
            txtОкончаниеПоиска.MouseLeave += ОчиститьСправку;

            txtПуть.MouseEnter += (s, e) => ПоказатьСправку("Папка, куда сохраняются файлы.");
            txtПуть.MouseLeave += ОчиститьСправку;

            txtВходнойТекст.MouseEnter += (s, e) => ПоказатьСправку("Исходный текст для парсинга.");
            txtВходнойТекст.MouseLeave += ОчиститьСправку;
            lblНачало.MouseEnter += (s, e) => ПоказатьСправку("Начало блока — маркер, с которого начинается класс.");
            lblНачало.MouseLeave += ОчиститьСправку;

            lblИмя.MouseEnter += (s, e) => ПоказатьСправку("Слово после которого идёт имя класса.");
            lblИмя.MouseLeave += ОчиститьСправку;

            lblКонец.MouseEnter += (s, e) => ПоказатьСправку("Окончание блока — маркер закрытия класса.");
            lblКонец.MouseLeave += ОчиститьСправку;

            lblПуть.MouseEnter += (s, e) => ПоказатьСправку("Папка для сохранения .cs файлов.");
            lblПуть.MouseLeave += ОчиститьСправку;

            Color bgForm     = Color.FromArgb(30, 30, 30);
            Color bgInput    = Color.FromArgb(45, 45, 48);
            Color bgInputHov = Color.FromArgb(55, 55, 58);
            Color bgBtn      = Color.FromArgb(50, 50, 55);
            Color bgBtnHov   = Color.FromArgb(75, 75, 80);
            Color bgBtnDown  = Color.FromArgb(90, 90, 95);
            Color fgLight    = Color.FromArgb(220, 220, 220);
            Color fgDim      = Color.FromArgb(170, 170, 170);
            Color fgAccent   = Color.FromArgb(86, 156, 214);
            Color borderClr  = Color.FromArgb(60, 60, 60);

            this.BackColor = bgForm;
            this.ForeColor = fgLight;
            Button[] buttons = {
    читатБуфер, очиститьБуфер, ВыделитБлоки,
    просмотрБлоков, сохраанитьФайлы, памятьБуфера,
    ВыборПутиСохранения, кнопкаНастройки
};
            // TextBox
            TextBox[] textBoxes = {
                txtВходнойТекст, txtНачалоПоиска, txtНаследникИмени,
                txtОкончаниеПоиска, txtПуть
            };
            foreach (var tb in textBoxes)
            {
                tb.BackColor = bgInput;
                tb.ForeColor = fgLight;
                tb.BorderStyle = BorderStyle.FixedSingle;
                tb.Font = new Font("Consolas", 9F);

                Color orig = tb.BackColor;
                tb.MouseEnter += (s, e) => ((TextBox)s).BackColor = bgInputHov;
                tb.MouseLeave += (s, e) => ((TextBox)s).BackColor = orig;
            }

            // Labels
            Label[] labels = { lblНачало, lblИмя, lblКонец, lblПуть };
            foreach (var lbl in labels)
            {
                lbl.ForeColor = fgDim;
                lbl.BackColor = bgForm;
                lbl.Cursor = Cursors.Hand;
                lbl.MouseEnter += (s, e) => ((Label)s).ForeColor = fgAccent;
                lbl.MouseLeave += (s, e) => ((Label)s).ForeColor = fgDim;
            }

            // Кнопки
            //Button[] buttons = {
            //    читатБуфер, очиститьБуфер, ВыделитБлоки,
            //    просмотрБлоков, сохраанитьФайлы, памятьБуфера,
            //    ВыборПутиСохранения
            //};
            foreach (var btn in buttons)
            {
                btn.BackColor = bgBtn;
                btn.ForeColor = fgLight;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = borderClr;
                btn.FlatAppearance.MouseOverBackColor = bgBtnHov;
                btn.FlatAppearance.MouseDownBackColor = bgBtnDown;
                btn.Font = new Font("Segoe UI", 9F);
            }
            foreach (var btn in buttons)
            {
                btn.MouseEnter += (s, e) => ПоказатьСправку(btn.Text);
                btn.MouseLeave += ОчиститьСправку;
            }

            // Тултипы
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 400;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(lblНачало,
                "Введите текст-маркер, с которого начинается блок.\n" +
                "Пример: «public class» или «[Serializable]»");
            toolTip1.SetToolTip(txtНачалоПоиска,
                "Текст, с которого начинается блок.");
            toolTip1.SetToolTip(lblИмя,
                "Ключевое слово, после которого идёт имя для файла.\n" +
                "По умолчанию: «class».");
            toolTip1.SetToolTip(txtНаследникИмени,
                "Ключевое слово для извлечения имени файла.");
            toolTip1.SetToolTip(lblКонец,
                "Введите текст-маркер, которым заканчивается блок.\n" +
                "Пример: «}» или «// END»");
            toolTip1.SetToolTip(txtОкончаниеПоиска,
                "Текст, которым заканчивается блок.");
            toolTip1.SetToolTip(lblПуть,
                "Папка, в которую будут сохранены .cs файлы.");
            toolTip1.SetToolTip(txtПуть,
                "Путь к папке для сохранения файлов.");
            toolTip1.SetToolTip(txtВходнойТекст,
                "Сюда вставляется исходный текст для парсинга.");
            toolTip1.SetToolTip(ВыборПутиСохранения,
                "Открывает диалог выбора папки для сохранения файлов.");
            toolTip1.SetToolTip(читатБуфер,
                "Читает текст из буфера обмена и вставляет его в поле ввода сверху.");
            toolTip1.SetToolTip(очиститьБуфер,
                "Очищает поле ввода и буфер обмена.");
            toolTip1.SetToolTip(ВыделитБлоки,
                "Ищет блоки в тексте по маркерам «Начало» и «Окончание».");
            toolTip1.SetToolTip(просмотрБлоков,
                "Открывает окно со списком всех найденных блоков.");
            toolTip1.SetToolTip(сохраанитьФайлы,
                "Сохраняет каждый найденный блок в отдельный .cs файл.");
            toolTip1.SetToolTip(памятьБуфера,
                "Открывает форму памяти буфера.");
        }

        // ================================================================
        //  ИЗВЛЕЧЕНИЕ ИМЕНИ КЛАССА
        // ================================================================
        public string ИзвлечьИмяКласса(string блок, string ключ)
        {
            if (string.IsNullOrWhiteSpace(ключ))
                ключ = "class";

            int pos = блок.IndexOf(ключ, StringComparison.Ordinal);
            if (pos == -1) return "БезИмени";

            pos += ключ.Length;
            while (pos < блок.Length && char.IsWhiteSpace(блок[pos]))
                pos++;

            string имя = "";
            while (pos < блок.Length)
            {
                char c = блок[pos];
                if (char.IsWhiteSpace(c) || c == '{' || c == ';')
                    break;
                имя += c;
                pos++;
            }
            return имя.Trim();
        }

        public string SanitizeFileName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Unknown";
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name.Trim('.', ' ', '\t');
        }

        // ================================================================
        //  ОБРАБОТЧИКИ
        // ================================================================
        public void читатБуфер_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText())
                    txtВходнойТекст.Text = Clipboard.GetText();
                else
                    MessageBox.Show("Буфер обмена не содержит текста.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения буфера: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void очиститьБуфер_Click(object sender, EventArgs e)
        {
            txtВходнойТекст.Clear();
            try { Clipboard.Clear(); } catch { }
        }
        // Form1.cs
        // Form1.cs

        private void СохранитьВсеВНумерованные_Click(object sender, EventArgs e)
        {
            if (блоки.Count == 0)
            {
                MessageBox.Show("Нет блоков. Сначала нажмите «Выделить блоки».",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Спросим папку
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку для сохранения нумерованных файлов";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string папка = dlg.SelectedPath;

                try
                {
                    // Спросим префикс и количество знаков
                    string префикс = Microsoft.VisualBasic.Interaction.InputBox(
                        "Префикс имени файла (например, «блок_»):",
                        "Параметры сохранения", "блок_");

                    if (префикс == null) return;   // отмена

                    string знаковСтр = Microsoft.VisualBasic.Interaction.InputBox(
                        "Сколько цифр в номере? (например, 3 → 001)",
                        "Параметры сохранения", "3");

                    if (!int.TryParse(знаковСтр, out int знаков) || знаков < 1)
                        знаков = 3;

                    int сохранено = 0;
                    int ошибок = 0;

                    for (int i = 0; i < блоки.Count; i++)
                    {
                        try
                        {
                            string номер = (i + 1).ToString(new string('0', знаков));
                            string имяФайла = $"{префикс}{номер}.txt";
                            string полныйПуть = Path.Combine(папка, имяФайла);

                            File.WriteAllText(полныйПуть, блоки[i],
                                              System.Text.Encoding.UTF8);
                            сохранено++;
                        }
                        catch (Exception ex)
                        {
                            ошибок++;
                            System.Diagnostics.Debug.WriteLine(
                                $"Ошибка сохранения блока {i + 1}: {ex.Message}");
                        }
                    }

                    MessageBox.Show(
                        $"Сохранено: {сохранено}\nОшибок: {ошибок}\nПапка: {папка}",
                        "Готово", MessageBoxButtons.OK,
                        ошибок > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка:\n{ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void СохранитьВсеВОдинФайл_Click(object sender, EventArgs e)
        {
            if (блоки.Count == 0)
            {
                MessageBox.Show("Нет блоков. Сначала нажмите «Выделить блоки».",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Сохранить все блоки в один файл";
                dlg.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                dlg.FileName = "блоки.txt";

                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine($"# Всего блоков: {блоки.Count}");
                    sb.AppendLine($"# Разделитель: {Разделитель}");
                    sb.AppendLine();

                    for (int i = 0; i < блоки.Count; i++)
                    {
                        sb.AppendLine($"### БЛОК {i + 1} ###");
                        sb.AppendLine(блоки[i]);
                        sb.AppendLine();
                        sb.AppendLine(Разделитель);
                        sb.AppendLine();
                    }

                    File.WriteAllText(dlg.FileName, sb.ToString(),
                                      System.Text.Encoding.UTF8);

                    MessageBox.Show($"Сохранено блоков: {блоки.Count}\n" +
                                    $"Файл: {dlg.FileName}",
                        "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения:\n{ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void ВыделитБлоки_Click(object sender, EventArgs e)
        {
            блоки.Clear();

            string текст = txtВходнойТекст.Text;
            string начало = txtНачалоПоиска.Text;
            string конец = txtОкончаниеПоиска.Text;

            if (string.IsNullOrWhiteSpace(текст))
            {
                MessageBox.Show("Нет текста для парсинга.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(начало) || string.IsNullOrWhiteSpace(конец))
            {
                MessageBox.Show("Укажите начало и конец блока.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Универсальный поиск
            блоки = НайтиБлокиУниверсально(текст, начало, конец);

            MessageBox.Show($"Найдено блоков: {блоки.Count}",
                "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обрезает текст так, чтобы он заканчивался на последней сбалансированной }.
        /// Учитывает строки, символьные литералы и комментарии.
        /// </summary>
        public string ОбрезатьПоБалансу(string текст)
        {
            int depth = 0;
            int последняяЗакрывающая = -1;
            bool inString = false, inChar = false, inLine = false, inBlock = false;

            for (int i = 0; i < текст.Length; i++)
            {
                char c = текст[i];
                char next = (i + 1 < текст.Length) ? текст[i + 1] : '\0';

                if (inLine) { if (c == '\n') inLine = false; continue; }
                if (inBlock) { if (c == '*' && next == '/') { inBlock = false; i++; } continue; }
                if (inString) { if (c == '\\') { i++; continue; } if (c == '"') inString = false; continue; }
                if (inChar) { if (c == '\\') { i++; continue; } if (c == '\'') inChar = false; continue; }

                if (c == '/' && next == '/') { inLine = true; i++; continue; }
                if (c == '/' && next == '*') { inBlock = true; i++; continue; }
                if (c == '"') { inString = true; continue; }
                if (c == '\'') { inChar = true; continue; }

                if (c == '{') depth++;
                else if (c == '}')
                {
                    depth--;
                    if (depth == 0) последняяЗакрывающая = i;
                }
            }

            if (последняяЗакрывающая == -1) return текст; // нет классов — вернём как есть
            return текст.Substring(0, последняяЗакрывающая + 1);
        }        /// <summary>
                 /// Рекурсивно ищет все классы в диапазоне [start, end) текста.
                 /// prefix — цепочка имён родительских классов для отображения.
                 /// </summary>
        public void НайтиВсеКлассы(string текст, int start, int end, string prefix)
        {
            int pos = start;

            while (pos < end)
            {
                // Ищем начало объявления класса
                int classPos = НайтиОбъявлениеКласса(текст, pos, end);
                if (classPos == -1) break;

                // Находим { после объявления
                int openBrace = текст.IndexOf('{', classPos);
                if (openBrace == -1 || openBrace >= end) break;

                // Границы тела класса
                int closeBrace = НайтиКонецБлока(текст, openBrace);
                if (closeBrace == -1) break;

                // Извлекаем имя класса
                string имя = ИзвлечьИмяКласса(текст.Substring(classPos, openBrace - classPos), "class");
                string полноеИмя = string.IsNullOrEmpty(prefix) ? имя : prefix + "." + имя;

                // Добавляем блок в список с заголовком
                string блок = текст.Substring(classPos, closeBrace - classPos + 1);
                блоки.Add($"// ===== {полноеИмя} =====\r\n{блок}");

                // Рекурсивно ищем вложенные классы внутри тела
                НайтиВсеКлассы(текст, openBrace + 1, closeBrace, полноеИмя);

                // Продолжаем поиск ПОСЛЕ закрывающей скобки
                pos = closeBrace + 1;
            }
        }
        /// <summary>
        /// Ищет ближайшее объявление класса в диапазоне [start, end).
        /// Учитывает модификаторы: public, public, internal, protected,
        /// static, sealed, abstract, partial, а также наследование через ':'.
        /// </summary>
        public int НайтиОбъявлениеКласса(string текст, int start, int end)
        {
            int pos = start;

            while (pos < end)
            {
                int idx = текст.IndexOf("class", pos, StringComparison.Ordinal);
                if (idx == -1 || idx >= end) return -1;

                // Слева от "class" должен быть пробел или модификатор
                // (чтобы не поймать, например, "Myclass" или "класс" в строке)
                if (idx > 0 && (char.IsLetterOrDigit(текст[idx - 1]) || текст[idx - 1] == '_'))
                {
                    pos = idx + 5;
                    continue;
                }

                // Справа от "class" должен быть пробел
                int after = idx + 5;
                if (after >= end || !char.IsWhiteSpace(текст[after]))
                {
                    pos = idx + 5;
                    continue;
                }

                // Проверяем, что мы не внутри строки или комментария
                if (ВнутриСтрокиИлиКомментария(текст, idx))
                {
                    pos = idx + 5;
                    continue;
                }

                return idx;
            }

            return -1;
        }
        /// <summary>
        /// Возвращает true, если позиция pos находится внутри строки, символа
        /// или комментария. Нужно, чтобы не ловить "class" в тексте.
        /// </summary>
        public bool ВнутриСтрокиИлиКомментария(string текст, int pos)
        {
            bool inString = false, inChar = false, inLine = false, inBlock = false;

            for (int i = 0; i < pos && i < текст.Length; i++)
            {
                char c = текст[i];
                char next = (i + 1 < текст.Length) ? текст[i + 1] : '\0';

                if (inLine) { if (c == '\n') inLine = false; continue; }
                if (inBlock) { if (c == '*' && next == '/') { inBlock = false; i++; } continue; }
                if (inString) { if (c == '\\') { i++; continue; } if (c == '"') inString = false; continue; }
                if (inChar) { if (c == '\\') { i++; continue; } if (c == '\'') inChar = false; continue; }

                if (c == '/' && next == '/') { inLine = true; i++; continue; }
                if (c == '/' && next == '*') { inBlock = true; i++; continue; }
                if (c == '"') inString = true;
                if (c == '\'') inChar = true;
            }

            return inString || inChar || inLine || inBlock;
        }
        /// <summary>
        /// Находит позицию закрывающей } для блока, начинающегося с openBrace.
        /// Учитывает строки, символьные литералы, // и /* */ комментарии,
        /// а также вложенные { }.
        /// </summary>
        public int НайтиКонецБлока(string текст, int openBrace)
        {
            int depth = 0;
            int i = openBrace;
            bool inString = false;
            bool inChar = false;
            bool inLineComment = false;
            bool inBlockComment = false;

            while (i < текст.Length)
            {
                char c = текст[i];
                char next = (i + 1 < текст.Length) ? текст[i + 1] : '\0';

                // Комментарии и строки обрабатываем первыми
                if (inLineComment)
                {
                    if (c == '\n') inLineComment = false;
                    i++;
                    continue;
                }
                if (inBlockComment)
                {
                    if (c == '*' && next == '/') { inBlockComment = false; i += 2; continue; }
                    i++;
                    continue;
                }
                if (inString)
                {
                    if (c == '\\' && next != '\0') { i += 2; continue; } // escape
                    if (c == '"') inString = false;
                    i++;
                    continue;
                }
                if (inChar)
                {
                    if (c == '\\' && next != '\0') { i += 2; continue; }
                    if (c == '\'') inChar = false;
                    i++;
                    continue;
                }

                // Не в строке/комментарии — проверяем начало
                if (c == '/' && next == '/') { inLineComment = true; i += 2; continue; }
                if (c == '/' && next == '*') { inBlockComment = true; i += 2; continue; }
                if (c == '"') { inString = true; i++; continue; }
                if (c == '\'') { inChar = true; i++; continue; }

                if (c == '{') depth++;
                else if (c == '}')
                {
                    depth--;
                    if (depth == 0) return i; // нашли закрывающую скобку класса
                }

                i++;
            }

            return -1; // не нашли — незакрытый блок
        }

        public void просмотрБлоков_Click(object sender, EventArgs e)
        {
            if (блоки.Count == 0)
            {
                MessageBox.Show("Нет блоков для просмотра.",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var previewForm = new Form())
            {
                previewForm.Text = "Предпросмотр блоков";
                previewForm.Size = new Size(800, 500);
                previewForm.StartPosition = FormStartPosition.CenterParent;
                previewForm.BackColor = Color.FromArgb(30, 30, 30);

                var listBox = new ListBox
                {
                    Dock = DockStyle.Left,
                    Width = 220,
                    SelectionMode = SelectionMode.One,
                    BackColor = Color.FromArgb(45, 45, 48),
                    ForeColor = Color.FromArgb(220, 220, 220),
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Consolas", 9F)
                };

                var textBox = new RichTextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    BackColor = Color.FromArgb(45, 45, 48),
                    ForeColor = Color.FromArgb(220, 220, 220),
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Consolas", 10F),
                    DetectUrls = false
                };
                listBox.SelectedIndexChanged += (s, args) =>
                {
                    if (listBox.SelectedIndex >= 0 && listBox.SelectedIndex < блоки.Count)
                    {
                        textBox.Text = блоки[listBox.SelectedIndex];
                        Подсветить(textBox);
                    }
                };

                for (int i = 0; i < блоки.Count; i++)
                {
                    string display = блоки[i].Length > 50
                        ? блоки[i].Substring(0, 50) + "..."
                        : блоки[i];
                    listBox.Items.Add($"[{i}] {display}");
                }

                listBox.SelectedIndex = 0;
                textBox.Text = блоки[0];

                listBox.SelectedIndexChanged += (s, args) =>
                {
                    if (listBox.SelectedIndex >= 0 && listBox.SelectedIndex < блоки.Count)
                        textBox.Text = блоки[listBox.SelectedIndex];
                };
                textBox.Text = блоки[0];
                Подсветить(textBox);

                previewForm.Controls.Add(textBox);
                previewForm.Controls.Add(listBox);
                previewForm.ShowDialog();
            }

        }
        private void Подсветить(RichTextBox box)
        {
            int selStart = box.SelectionStart;
            int selLength = box.SelectionLength;

            box.SelectAll();
            box.SelectionColor = Color.FromArgb(220, 220, 220);

            // Ключевые слова
            string[] keywords = {
        "class", "public", "private", "protected", "internal",
        "namespace", "using", "return", "void", "int", "string",
        "new", "static", "partial", "abstract", "sealed"
    };

            foreach (var word in keywords)
                Раскрасить(box, word, Color.DeepSkyBlue);

            // Комментарии //
            Раскрасить(box, "//", Color.Green);

            // Комментарии /* */
            Раскрасить(box, "/*", Color.Green);
            Раскрасить(box, "*/", Color.Green);

            // Строки "..."
            РаскраситьРегекс(box, "\".*?\"", Color.Khaki);

            // Символьные литералы 'a'
            РаскраситьРегекс(box, "\'.*?\'", Color.Khaki);

            // Скобки
            Раскрасить(box, "{", Color.Orange);
            Раскрасить(box, "}", Color.Orange);

            box.SelectionStart = selStart;
            box.SelectionLength = selLength;
        }
        private void Раскрасить(RichTextBox box, string слово, Color цвет)
        {
            int pos = 0;
            while ((pos = box.Text.IndexOf(слово, pos)) != -1)
            {
                box.Select(pos, слово.Length);
                box.SelectionColor = цвет;
                pos += слово.Length;
            }
        }

        private void РаскраситьРегекс(RichTextBox box, string pattern, Color цвет)
        {
            var matches = System.Text.RegularExpressions.Regex.Matches(box.Text, pattern);
            foreach (System.Text.RegularExpressions.Match m in matches)
            {
                box.Select(m.Index, m.Length);
                box.SelectionColor = цвет;
            }
        }

        public void сохраанитьФайлы_Click(object sender, EventArgs e)
        {
            string путь = txtПуть.Text.Trim();

            if (string.IsNullOrEmpty(путь) || !Directory.Exists(путь))
            {
                MessageBox.Show("Укажите существующий путь для сохранения.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (блоки.Count == 0)
            {
                MessageBox.Show("Нет блоков для сохранения.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ключ = txtНаследникИмени.Text;
            int saved = 0, errors = 0;

            foreach (var блок in блоки)
            {
                try
                {
                    string имя = ИзвлечьИмяКласса(блок, ключ);
                    string safeName = SanitizeFileName(имя) + ".cs";
                    string fullPath = Path.Combine(путь, safeName);

                    File.WriteAllText(fullPath, блок);
                    saved++;
                }
                catch (Exception ex)
                {
                    errors++;
                    System.Diagnostics.Debug.WriteLine($"Ошибка сохранения блока: {ex.Message}");
                }
            }

            string msg = $"Сохранено: {saved}. Ошибок: {errors}.";
            MessageBox.Show(msg, "Результат", MessageBoxButtons.OK,
                errors > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        public void памятьБуфера_Click(object sender, EventArgs e)
        {
            try
            {
                var fm = new FormMemory(txtВходнойТекст);
                fm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось открыть форму памяти:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ВыборПутиСохранения_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку для сохранения файлов";
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtПуть.Text = dlg.SelectedPath;
            }
        }

        private void форма_правка_Click(object sender, EventArgs e)
        { // Открываем визуальный конструктор правил
          // Передаём ссылку на основной текстовый буфер (txtВходнойТекст)
            var f = new FormПравка(txtВходнойТекст);

            // Показываем как обычное окно (не модальное)
            f.Show();
        }

        private void памятьБуфера_Click_1(object sender, EventArgs e)
        {
        }
    }

}
