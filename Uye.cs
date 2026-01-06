namespace KutuphaneYonetim;

internal sealed class Uye : TemelKullanici
{
    // ISBN anahtarli sozluk uyeye ait odunc kayitlarini takip ediyor.
    private readonly Dictionary<string, (Kitap kitap, decimal ucret)> _oduncKitaplar =
        new(StringComparer.OrdinalIgnoreCase);

    public Uye(string kullaniciId, string isim, string uyelikTipi)
        : base(kullaniciId, isim)
    {
        // Uyelik tipi artik string olarak saklanıyor.
        UyelikTipi = uyelikTipi;
    }

    // String tipinde uyelik tipi bilgisi.
    public string UyelikTipi { get; }

    public decimal ToplamUcret { get; private set; }

    public IReadOnlyDictionary<string, (Kitap kitap, decimal ucret)> OduncKitaplar => _oduncKitaplar;

    public override void BilgiGoruntule()
    {
        Console.WriteLine($"Uye: {Isim} (ID: {KullaniciId})");
        Console.WriteLine($"  Uyelik Tipi: {UyelikTipi}");
        Console.WriteLine($"  Oduncte Kitap Sayisi: {_oduncKitaplar.Count}");
        Console.WriteLine($"  Toplam Ucret: {ToplamUcret:C}");
    }

    public bool OduncAl(Kitap kitap, out decimal ucret)
    {
        // Her kitap tek kopya olarak uyede bulunabilir.
        if (kitap.StokDurumu <= 0)
        {
            ucret = 0m;
            return false;
        }

        if (_oduncKitaplar.ContainsKey(kitap.ISBN))
        {
            ucret = 0m;
            return false;
        }

        ucret = kitap.UcretHesapla();
        _oduncKitaplar[kitap.ISBN] = (kitap, ucret);
        kitap.StokDurumu--;
        ToplamUcret += ucret;
        return true;
    }

    public bool IadeEt(string isbn, out decimal ucret)
    {
        // Iade edilen kitap stok miktarina geri ekleniyor.
        if (!_oduncKitaplar.TryGetValue(isbn, out var kayit))
        {
            ucret = 0m;
            return false;
        }

        kayit.kitap.StokDurumu++;
        ucret = kayit.ucret;
        _oduncKitaplar.Remove(isbn);
        // Iade sonrasi toplam ucretten dusulur, negatif deger engellenir.
        if (ToplamUcret >= ucret)
        {
            ToplamUcret -= ucret;
        }
        else
        {
            ToplamUcret = 0;
        }
        return true;
    }
}
