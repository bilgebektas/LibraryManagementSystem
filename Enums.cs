namespace KutuphaneYonetim;

// Enum yerine string sabitler kullanilarak daha ilkel bir yaklasim uygulanmistir.
// Bu yaklasim eski sistemlerde veya basit senaryolarda tercih edilebilir.

// Kutuphanecilerin uzmanlik alanlari icin sabit string degerleri.
internal static class UzmanlikTuru
{
    public const string Bilim = "Bilim";
    public const string Kurgu = "Kurgu";
    public const string Tarih = "Tarih";
    public const string Teknoloji = "Teknoloji";
    
    // Tum gecerli uzmanliklari donduren yardimci metot.
    public static string[] TumDegerler() => new[] { Bilim, Kurgu, Tarih, Teknoloji };
}

// Uyelik tipleri icin sabit string degerleri.
internal static class UyelikTipi
{
    public const string Temel = "Temel";
    public const string Premium = "Premium";
    
    public static string[] TumDegerler() => new[] { Temel, Premium };
}

// Kitap turleri icin sabit string degerleri.
internal static class KitapTuru
{
    public const string Bilim = "Bilim";
    public const string Kurgu = "Kurgu";
    public const string Tarih = "Tarih";
    public const string Teknoloji = "Teknoloji";
    
    public static string[] TumDegerler() => new[] { Bilim, Kurgu, Tarih, Teknoloji };
}
