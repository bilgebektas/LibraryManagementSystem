using KutuphaneYonetim;

internal class Program
{
    private static void Main()
    {
        // Kutuphane nesnesi olusturuluyor, sistem buradan basliyor.
        var kutuphane = new Kutuphane();
        // Cikis isareti, dongu kontrol etmek icin kullaniliyor.
        var cikis = false;

        // Ana menu dongusunde kullanici islemleri soruluyor ve istenilen secim yapiliyor.
        while (!cikis)
        {
            // Kullaniciya menu gosteriliyor.
            MenuyuGoster();
            Console.Write("Seciminiz: ");
            // Kullanicinin secimi okunuyor.
            var secim = Console.ReadLine();
            Console.WriteLine();

            // Kullanicinin secimi degerlendiriliyor ve uygun isleme yonlendiriliyor.
            switch (secim)
            {
                case "1":
                    // Yeni bir kitap eklemek icin ilgili metot cagriliyor.
                    KitapEkle(kutuphane);
                    break;
                case "2":
                    // Sisteme yeni bir uye kaydedilecek.
                    UyeEkle(kutuphane);
                    break;
                case "3":
                    // Kutuphaneci personeli sisteme ekleniyor.
                    KutuphaneciEkle(kutuphane);
                    break;
                case "4":
                    // Uye bir kitap odunc almak istiyor.
                    KitapOduncAl(kutuphane);
                    break;
                case "5":
                    // Uye oduncta aldigi kitabi iade etmek istiyor.
                    KitapIadeEt(kutuphane);
                    break;
                case "6":
                    // Kutuphanede mevcut tum kitaplar listeleniyor.
                    kutuphane.TumKitaplariGoruntule();
                    break;
                case "7":
                    // Sisteme kayitli tum uyeler gosteriliyor.
                    kutuphane.TumUyeleriGoruntule();
                    break;
                case "8":
                    // Tum kutuphaneci personeli gosteriliyor.
                    kutuphane.TumKutuphanecileriGoruntule();
                    break;
                case "0":
                    // Kullanici cikis yapmak istiyor, dongu sonlandirilacak.
                    cikis = true;
                    break;
                default:
                    // Gecersiz secim yapilmissa kullanici uyariliyor.
                    Console.WriteLine("Gecersiz secim, lutfen tekrar deneyin.");
                    break;
            }

            if (!cikis)
            {
                // Kullaniciya bir sonraki adim oncesi zaman taniniyor.
                Console.WriteLine();
                Console.WriteLine("Devam etmek icin Enter tusuna basin...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

    private static void MenuyuGoster()
    {
        // Tum menuyu konsolda tek seferde gostermek icin kullaniliyor.
        // Kullanici hangi islemleri yapabilecegini bu menu sayesinde ogreniyor.
        Console.WriteLine("--- Kutuphane Yonetim Sistemi ---");
        Console.WriteLine("1. Kitap ekle");
        Console.WriteLine("2. Uye ekle");
        Console.WriteLine("3. Kutuphaneci ekle");
        Console.WriteLine("4. Kitap odunc al");
        Console.WriteLine("5. Kitap iade et");
        Console.WriteLine("6. Tum kitaplari goruntule");
        Console.WriteLine("7. Tum uyeleri goruntule");
        Console.WriteLine("8. Tum kutuphanecileri goruntule");
        Console.WriteLine("0. Cikis");
    }

    private static void KitapEkle(Kutuphane kutuphane)
    {
        // Sisteme yeni bir kitap eklemek icin gerekli bilgiler sorgulanıyor.
        // Kitap adi kitabi tanımlamaya yardımcı olan ilk bilgi.
        Console.Write("Kitap adi: ");
        var kitapAdi = Console.ReadLine() ?? string.Empty;

        // Yazarın ismi kitabın kaynağını belirtir.
        Console.Write("Yazar adi: ");
        var yazarAdi = Console.ReadLine() ?? string.Empty;

        // ISBN kitabın benzersiz kimliği, kopya kontrolu icin kritik.
        Console.Write("ISBN: ");
        var isbn = Console.ReadLine() ?? string.Empty;

        // Kitap turu (Bilim, Kurgu, Tarih, Teknoloji) onemli cunku kutuphaneci atamasında kullanılır.
        var kitapTuru = StringDegeriniSec("Kitap turu", KitapTuru.TumDegerler());
        if (string.IsNullOrEmpty(kitapTuru))
        {
            Console.WriteLine("Kitap turu secilemedi, islem iptal edildi.");
            return;
        }

        // Kitaptan kac kopya var, bu bilgi odunc alabilirligini belirler.
        Console.Write("Stok durumu (tam sayi): ");
        if (!int.TryParse(Console.ReadLine(), out var stok) || stok < 0)
        {
            Console.WriteLine("Gecerli bir stok sayisi girilmedi.");
            return;
        }

        var kitap = new Kitap(kitapAdi, yazarAdi, isbn, kitapTuru, stok);
        // Tum bilgileri iceren yeni kitap nesnesi olusturuluyor.
        if (kutuphane.KitapEkle(kitap))
        {
            Console.WriteLine("Kitap basariyla eklendi.");
            // Kitap türüne uygun bir kütüphaneci atanıp atanmadığı kontrol ediliyor.
            if (kitap.AtanmisKutuphaneci is null)
            {
                Console.WriteLine("Bu tura uygun atanmis bir kutuphaneci bulunamadi.");
            }
            else
            {
                // Başarılı bir atama durumunda atanan kütüphaneci bilgisi gösteriliyor.
                Console.WriteLine($"Atanan kutuphaneci: {kitap.AtanmisKutuphaneci.Isim} ({kitap.AtanmisKutuphaneci.Uzmanlik})");
            }
        }
        else
        {
            // Ayni ISBN'li kitap varsa tekrar eklenmez, veri tutarliligi saglanir.
            Console.WriteLine("Bu ISBN ile kitap zaten mevcut.");
        }
    }

    private static void UyeEkle(Kutuphane kutuphane)
    {
        // Yeni bir kullanıcı sisteme üye olarak kaydediliyor.
        // Her üye benzersiz bir ID ile ayrılır, bu sistemde temel tanıcı.
        Console.Write("Uye ID: ");
        var uyeId = Console.ReadLine() ?? string.Empty;

        // Üyenin adı kişisel tanıtma için kullanılıyor.
        Console.Write("Isim: ");
        var isim = Console.ReadLine() ?? string.Empty;

        // Üyelik tipi (Temel/Premium) üyenin hakları belirler.
        var uyelikTipi = StringDegeriniSec("Uyelik tipi", UyelikTipi.TumDegerler());
        if (string.IsNullOrEmpty(uyelikTipi))
        {
            Console.WriteLine("Uyelik tipi secilemedi, islem iptal edildi.");
            return;
        }

        var uye = new Uye(uyeId, isim, uyelikTipi);
        // Üyenin tüm bilgileriyle yeni nesnesi oluşturuluyor.
        if (kutuphane.UyeOl(uye))
        {
            Console.WriteLine("Uye basariyla eklendi.");
        }
        else
        {
            // Aynı ID'li üye varsa tekrar eklenmez.
            Console.WriteLine("Bu ID ile uye zaten mevcut.");
        }
    }

    private static void KutuphaneciEkle(Kutuphane kutuphane)
    {
        // Kütüphaneci personeli sisteme ekleniyor.
        // Her kütüphanecinin benzersiz ID'si vardır.
        Console.Write("Kutuphaneci ID: ");
        var kutuphaneciId = Console.ReadLine() ?? string.Empty;

        // Kütüphanecinin adı tanıtma için gereklidir.
        Console.Write("Isim: ");
        var isim = Console.ReadLine() ?? string.Empty;

        // Maaş bilgisi personel kayıtları için önemli.
        Console.Write("Maas (ondalik sayi): ");
        if (!decimal.TryParse(Console.ReadLine(), out var maas) || maas < 0)
        {
            Console.WriteLine("Gecerli bir maas girilmedi.");
            return;
        }

        // Uzmanlık türü çok önemli çünkü ilgili kitap türlerine otomatik atanır.
        var uzmanlikTuru = StringDegeriniSec("Uzmanlik turu", UzmanlikTuru.TumDegerler());
        if (string.IsNullOrEmpty(uzmanlikTuru))
        {
            Console.WriteLine("Uzmanlik turu secilemedi, islem iptal edildi.");
            return;
        }

        // Kütüphaneci nesnesi tüm verilerle oluşturuluyor.
        var kutuphaneci = new Kutuphaneci(kutuphaneciId, isim, maas, uzmanlikTuru);
        // Kütüphaneci kütüphaneye ekleniyor ve otomatik kitap atanması başlatılır.
        if (kutuphane.KutuphaneciEkle(kutuphaneci))
        {
            Console.WriteLine("Kutuphaneci basariyla eklendi.");
        }
        else
        {
            // Aynı ID'li kütüphaneci varsa tekrar eklenmez.
            Console.WriteLine("Bu ID ile kutuphaneci zaten mevcut.");
        }
    }

    private static void KitapOduncAl(Kutuphane kutuphane)
    {
        // Üyenin kitap ödünç alma talebi işleniyor.
        // Hangi üyenin talep ettiğini belirlemek için ID gerekli.
        Console.Write("Uye ID: ");
        var uyeId = Console.ReadLine() ?? string.Empty;

        // Hangi kitabı ödünç almak istediğini belirtmek için ISBN lazım.
        Console.Write("Kitap ISBN: ");
        var isbn = Console.ReadLine() ?? string.Empty;

        var (basarili, mesaj, ucret, kutuphaneci) = kutuphane.KitapOduncAl(uyeId, isbn);
        // Ödünç alma işlemi gerçekleştiriliyor, başarı ve detay döner.
        Console.WriteLine(mesaj);
        // Eğer kitap başarıyla ödünç alındıysa ilgili kütüphaneci gösteriliyor.
        if (kutuphaneci is not null)
        {
            Console.WriteLine($"Atanmis kutuphaneci: {kutuphaneci.Isim} ({kutuphaneci.Uzmanlik})");
        }
        else
        {
            // Uygun kütüphaneci atanmadığı durumda kullanıcı bilgilendirilir.
            Console.WriteLine("Bu tura uygun atanmis bir kutuphaneci bulunamadi.");
        }

        // Ödünç alınan kitaptan düşülecek ücret gösteriliyor.
        if (basarili)
        {
            Console.WriteLine($"Alinan kitap ucreti: {ucret:C}");
        }
    }

    private static void KitapIadeEt(Kutuphane kutuphane)
    {
        // Üyenin kitap iade işlemi gerçekleştiriliyor.
        // İade işleminde de üyeyi belirlemek için ID lazım.
        Console.Write("Uye ID: ");
        var uyeId = Console.ReadLine() ?? string.Empty;

        // Hangi kitabı iade ettiğini belirtmek için ISBN gerekli.
        Console.Write("Kitap ISBN: ");
        var isbn = Console.ReadLine() ?? string.Empty;

        var (basarili, mesaj, ucret) = kutuphane.KitapIadeEt(uyeId, isbn);
        // İade işlemi gerçekleştiriliyor, sistem hangi tutar elde ettiğini döner.
        Console.WriteLine(mesaj);
        // İade edilen kitaptan elde edilen ücret gösteriliyor.
        if (basarili)
        {
            Console.WriteLine($"Kitap ucreti: {ucret:C}");
            // İadenin ardından üyenin güncel toplam ücretine bakılıyor.
            var uye = kutuphane.UyeBul(uyeId);
            if (uye is not null)
            {
                Console.WriteLine($"Guncel toplam ucret: {uye.ToplamUcret:C}");
            }
        }
    }

    private static string StringDegeriniSec(string baslik, string[] secenekler)
    {
        // Enum yerine string array kullanarak daha ilkel bir secim mekanizmasi.
        // Kullaniciya secenekler gosteriliyor.
        Console.WriteLine($"{baslik} secenekleri:");

        // Tum secenekler numarali olarak listeleniyor.
        for (int i = 0; i < secenekler.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {secenekler[i]}");
        }

        // Kullanici bir secim yapiyor (sadece numara).
        Console.Write($"{baslik} seciminiz (numara): ");
        var girdi = Console.ReadLine();
        Console.WriteLine();

        // Sayisal giris dogrulanıyor ve indekse cevriliyor.
        if (int.TryParse(girdi, out var sayi) && sayi >= 1 && sayi <= secenekler.Length)
        {
            // Secilen string deger donduruluyor.
            return secenekler[sayi - 1];
        }

        // Gecersiz giris durumunda bos string doner.
        return string.Empty;
    }
}
