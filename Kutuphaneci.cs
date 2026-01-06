namespace KutuphaneYonetim;

internal sealed class Kutuphaneci : TemelKullanici
{
    public Kutuphaneci(string kullaniciId, string isim, decimal maas, string uzmanlik)
        : base(kullaniciId, isim)
    {
        Maas = maas;
        // Uzmanlik string degeri olarak kaydediliyor.
        // Bu deger kitap turleriyle string karsilastirma ile eslestirilecek.
        Uzmanlik = uzmanlik;
    }

    public decimal Maas { get; }

    // String tipi kutuphanecinin hangi kitap turlerinden sorumlu oldugunu belirler.
    // Artik enum yerine direkt string karsilastirma yapilacak.
    public string Uzmanlik { get; }

    public override void BilgiGoruntule()
    {
        // Konsolda kutuphaneciye ait kisi ve uzmanlik bilgileri listeleniyor.
        Console.WriteLine($"Kutuphaneci: {Isim} (ID: {KullaniciId})");
        Console.WriteLine($"  Uzmanlık: {Uzmanlik}");
        Console.WriteLine($"  Maaş: {Maas:C}");
        Console.WriteLine($"  Kayıt Tarihi: {KayitTarihi:d}");
    }
}
