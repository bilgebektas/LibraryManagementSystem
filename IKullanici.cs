namespace KutuphaneYonetim;

internal interface IKullanici
{
    // Tum kullanici turleri icin ortak kimlik ve ad alanlari.
    string KullaniciId { get; }

    string Isim { get; }
}
