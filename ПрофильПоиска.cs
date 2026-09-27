using System.Collections.Generic;

namespace парсер
{
    public class ПрофильПоиска
    {
        public string Имя { get; set; }            // как отображается в списке
        public string НачалоПоиска { get; set; }   // "using"
        public string НаследникИмени { get; set; } // "class"
        public string ОкончаниеПоиска { get; set; }// "}"
        public string ПутьСохранения { get; set; } // C:\...
    }

    public class СписокПрофилей
    {
        public List<ПрофильПоиска> Профили { get; set; } = new List<ПрофильПоиска>();
    }
}