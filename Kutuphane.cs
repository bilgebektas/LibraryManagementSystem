namespace KutuphaneYonetim;

internal sealed class Kutuphane
{
    // Sistem verilerini tutulabilir kildigi icin listeler readonly olarak tutuluyor.
    private readonly List<Kitap> _kitaplar = new();
    private readonly List<Uye> _uyeler = new();
    private readonly List<Kutuphaneci> _kutuphaneciler = new();

    public IReadOnlyList<Kitap> Kitaplar => _kitaplar;

    public IReadOnlyList<Uye> Uyeler => _uyeler;

    public IReadOnlyList<Kutuphaneci> Kutuphaneciler => _kutuphaneciler;

    public bool KitapEkle(Kitap kitap)
    {
        // Aynı ISBN'li kitap eklenmesini engelliyoruz.
        if (_kitaplar.Any(k => string.Equals(k.ISBN, kitap.ISBN, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        _kitaplar.Add(kitap);
        KutuphaneciyiKitabaAta(kitap);
        return true;
    }

    public bool UyeOl(Uye uye)
    {
        // Uye kayitlarında benzersiz ID kontrolu yapiliyor.
        if (_uyeler.Any(u => string.Equals(u.KullaniciId, uye.KullaniciId, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        _uyeler.Add(uye);
        return true;
    }

    public bool KutuphaneciEkle(Kutuphaneci kutuphaneci)
    {
        // Her kutuphaneci icin tekil bir ID bulunmasi saglaniyor.
        if (_kutuphaneciler.Any(k => string.Equals(k.KullaniciId, kutuphaneci.KullaniciId, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        _kutuphaneciler.Add(kutuphaneci);
        KitaplariYenidenAta(kutuphaneci.Uzmanlik);
        return true;
    }

    public void TumKitaplariGoruntule()
    {
        // Liste bos ise hizlica kullanici bilgilendiriliyor.
        if (_kitaplar.Count == 0)
        {
            Console.WriteLine("Kutuphanede kitap bulunmuyor.");
            return;
        }

        foreach (var kitap in _kitaplar)
        {
            kitap.KitapBilgiGoruntule();
            Console.WriteLine();
        }
    }

    public void TumUyeleriGoruntule()
    {
        if (_uyeler.Count == 0)
        {
            Console.WriteLine("Kayitli uye bulunmuyor.");
            return;
        }

        foreach (var uye in _uyeler)
        {
            uye.BilgiGoruntule();
            Console.WriteLine();
        }
    }

    public void TumKutuphanecileriGoruntule()
    {
        if (_kutuphaneciler.Count == 0)
        {
            Console.WriteLine("Kayitli kutuphaneci bulunmuyor.");
            return;
        }

        foreach (var kutuphaneci in _kutuphaneciler)
        {
            kutuphaneci.BilgiGoruntule();
            Console.WriteLine();
        }
    }

    public (bool basarili, string mesaj, decimal ucret, Kutuphaneci? kutuphaneci) KitapOduncAl(
        string uyeId,
        string isbn)
    {
        // Odunc alma adimi hem uye hem kitap dogrulamasini yapiyor.
        var uye = UyeBul(uyeId);
        if (uye is null)
        {
            return (false, "Uye bulunamadi.", 0m, null);
        }

        var kitap = KitapBul(isbn);
        if (kitap is null)
        {
            return (false, "Kitap bulunamadi.", 0m, null);
        }

        if (kitap.StokDurumu <= 0)
        {
            return (false, "Kitabin stokta adedi bulunmuyor.", 0m, kitap.AtanmisKutuphaneci);
        }

        if (!uye.OduncAl(kitap, out var ucret))
        {
            return (false, "Kitap odunc alma islemi basarisiz.", 0m, kitap.AtanmisKutuphaneci);
        }

        return (true, "Kitap odunc alma islemi basarili.", ucret, kitap.AtanmisKutuphaneci);
    }

    public (bool basarili, string mesaj, decimal ucret) KitapIadeEt(string uyeId, string isbn)
    {
        var uye = UyeBul(uyeId);
        if (uye is null)
        {
            return (false, "Uye bulunamadi.", 0m);
        }

        if (!uye.IadeEt(isbn, out var ucret))
        {
            return (false, "Iade edilecek kitap bulunamadi.", 0m);
        }

        return (true, "Iade islemi tamamlandi.", ucret);
    }

    public Kitap? KitapBul(string isbn)
    {
        return _kitaplar.FirstOrDefault(k => string.Equals(k.ISBN, isbn, StringComparison.OrdinalIgnoreCase));
    }

    public Uye? UyeBul(string uyeId)
    {
        return _uyeler.FirstOrDefault(u => string.Equals(u.KullaniciId, uyeId, StringComparison.OrdinalIgnoreCase));
    }

    private void KutuphaneciyiKitabaAta(Kitap kitap)
    {
        // Kitap türüne göre uygun uzmanlığa sahip kütüphaneciyi bulup atama yapıyoruz.
        // String karsilastirma ile eslesme kontrol ediliyor (case-insensitive).
        var kutuphaneci = _kutuphaneciler.FirstOrDefault(k => 
            string.Equals(k.Uzmanlik, kitap.Tur, StringComparison.OrdinalIgnoreCase));
        kitap.AtanmisKutuphaneciAyarla(kutuphaneci);
    }

    private void KitaplariYenidenAta(string uzmanlik)
    {
        // Yeni bir kütüphaneci eklendiğinde onun uzmanlık alanına uygun tüm kitaplar yeniden atanıyor.
        foreach (var kitap in _kitaplar.Where(k => 
            string.Equals(k.Tur, uzmanlik, StringComparison.OrdinalIgnoreCase)))
        {
            KutuphaneciyiKitabaAta(kitap);
        }
    }
}
