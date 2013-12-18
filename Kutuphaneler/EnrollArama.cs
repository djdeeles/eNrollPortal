namespace EnrollPortal.Kutuphaneler
{
    public class EnrollArama
    {
        public static string QueryStringeCevir(string ArananKelime)
        {
            ArananKelime = ArananKelime.Replace(" ", "-");
            ArananKelime = ArananKelime.Replace("ı", "i_");
            ArananKelime = ArananKelime.Replace("I", "_i");
            ArananKelime = ArananKelime.Replace("ğ", "g_");
            ArananKelime = ArananKelime.Replace("Ğ", "_g");
            ArananKelime = ArananKelime.Replace("ü", "u_");
            ArananKelime = ArananKelime.Replace("Ü", "_u");
            ArananKelime = ArananKelime.Replace("ş", "s_");
            ArananKelime = ArananKelime.Replace("Ş", "_s");
            ArananKelime = ArananKelime.Replace("ö", "o_");
            ArananKelime = ArananKelime.Replace("Ö", "_o");
            ArananKelime = ArananKelime.Replace("ç", "c_");
            ArananKelime = ArananKelime.Replace("Ç", "_c");
            ArananKelime = ArananKelime.Replace("?", "");
            ArananKelime = ArananKelime.Replace("<", "");
            ArananKelime = ArananKelime.Replace(">", "");
            ArananKelime = ArananKelime.Replace(";", "");
            ArananKelime = ArananKelime.Replace(":", "");
            ArananKelime = ArananKelime.Replace("~", "");
            ArananKelime = ArananKelime.Replace(",", "");
            ArananKelime = ArananKelime.Replace("`", "");
            ArananKelime = ArananKelime.Replace("'", "");
            ArananKelime = ArananKelime.Replace("!", "");
            ArananKelime = ArananKelime.Replace("+", "");
            ArananKelime = ArananKelime.Replace("/", "");
            ArananKelime = ArananKelime.Replace(@"\", "");
            ArananKelime = ArananKelime.Replace("%", "");
            ArananKelime = ArananKelime.Replace("^", "");
            ArananKelime = ArananKelime.Replace("\"", "-");
            ArananKelime = ArananKelime.Replace("’", "-");
            ArananKelime = ArananKelime.ToLower();
            return ArananKelime;
        }

        public static string HtmlDegistir(string Kelime)
        {
            Kelime = Kelime.Replace("&uuml;", "u");
            Kelime = Kelime.Replace("&Uuml;", "u");
            Kelime = Kelime.Replace("&ccedil;", "c");
            Kelime = Kelime.Replace("&Ccedil;", "c");
            Kelime = Kelime.Replace("&ouml;", "o");
            Kelime = Kelime.Replace("&Ouml;", "o");
            return Kelime;
        }

        public static string QueryStringeTersCevir(string ArananKelime)
        {
            ArananKelime = ArananKelime.ToLower();
            ArananKelime = ArananKelime.Replace("u_", "&uuml;");
            ArananKelime = ArananKelime.Replace("_u", "&Uuml;");
            ArananKelime = ArananKelime.Replace("o_", "&ouml;");
            ArananKelime = ArananKelime.Replace("_o", "&Ouml;");
            ArananKelime = ArananKelime.Replace("c_", "&Ccedil;");
            ArananKelime = ArananKelime.Replace("_c", "&Ccedil;");
            ArananKelime = ArananKelime.Replace("s_", "ş");
            ArananKelime = ArananKelime.Replace("_s", "Ş");
            ArananKelime = ArananKelime.Replace("i_", "ı");
            ArananKelime = ArananKelime.Replace("_i", "I");
            ArananKelime = ArananKelime.Replace("g_", "ğ");
            ArananKelime = ArananKelime.Replace("_g", "Ğ");
            ArananKelime = ArananKelime.Replace("-", " ");
            ArananKelime = ArananKelime.ToLower();
            return ArananKelime;
        }
    }

    public class Arama
    {
        public string Ara { get; set; }
    }
}