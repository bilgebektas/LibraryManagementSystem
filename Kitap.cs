namespace KutuphaneYonetim;

internal sealed class Kitap
{
    public Kitap(string kitapAdi, string yazarAdi, string isbn, string tur, int stokDurumu)
    {
        KitapAdi = kitapAdi;
        YazarAdi = yazarAdi;
        ISBN = isbn;
        // Tur string degeri olarak saklanıyor, bu sayede kutuphaneci ataması string karsilastirma ile yapilacak.
        Tur = tur;
        StokDurumu = stokDurumu;
    }

    public string KitapAdi { get; }

    public string YazarAdi { get; }

    public string ISBN { get; }

    // String tipi kullanılarak kitabın kategorisi belirleniyor.
    // Bu string degeri kutuphaneci atamasinda ve ucret hesaplamasinda kullanilir.
    public string Tur { get; }

    public int StokDurumu { get; set; }

    public Kutuphaneci? AtanmisKutuphaneci { get; private set; }

    public decimal UcretHesapla()
    {
        // Kitap ücreti tür bazında sabit değerler ile belirleniyor.
        // String karsilastirma ile her tur icin farkli ucret donduruluyor.
        return Tur switch
        {
            KitapTuru.Bilim => 3m,
            KitapTuru.Kurgu => 2m,
            KitapTuru.Tarih => 2m,
            KitapTuru.Teknoloji => 4m,
            _ => 0m
        };
    }

    public void AtanmisKutuphaneciAyarla(Kutuphaneci? kutuphaneci)
    {
        // Her kitap turune uygun kutuphaneci atanabiliyor.
        AtanmisKutuphaneci = kutuphaneci;
    }

    public void KitapBilgiGoruntule()
    {
        // Konsolda kitap ozet bilgisi ve atanmis kutuphaneci gosteriliyor.
        Console.WriteLine($"Kitap: {KitapAdi} - {YazarAdi} (ISBN: {ISBN})");
        Console.WriteLine($"  Tur: {Tur}");
        Console.WriteLine($"  Stok: {StokDurumu}");
        Console.WriteLine($"  Atanmis Kutuphaneci: {AtanmisKutuphaneci?.Isim ?? "Atanmadi"}");
    }
}
