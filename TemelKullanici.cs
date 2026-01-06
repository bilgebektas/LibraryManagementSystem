namespace KutuphaneYonetim;

internal abstract class TemelKullanici : IKullanici
{
    // Ortak alanlarin kurulumunu zorunlu kilmak icin kurucu tanimlandi.
    protected TemelKullanici(string kullaniciId, string isim)
    {
        KullaniciId = kullaniciId;
        Isim = isim;
        KayitTarihi = DateTime.Now;
    }

    public string KullaniciId { get; protected set; }

    public string Isim { get; protected set; }

    public DateTime KayitTarihi { get; }

    public abstract void BilgiGoruntule();
}
