using System;

namespace парсер
{
    public class Правило
    {
        public string Условие { get; set; }
        public string Действие { get; set; }
        public string Параметр { get; set; }

        public override string ToString()
        {
            return $"{Условие} → {Действие} ({Параметр})";
        }
    }
}
