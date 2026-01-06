# Kütüphane Yönetim Sistemi 

Bu çalışma, C# programlama dili kullanılarak geliştirilmiş bir konsol uygulamasıdır. Projenin temel amacı, kütüphane operasyonlarını Nesne Tabanlı Programlama (OOP) prensiplerine uygun bir mimariyle simüle etmektir.

## Uygulanan Teknik Gereksinimler

Proje geliştirilirken akademik kriterlerde belirtilen şu yapılar kullanılmıştır:

- Arayüzler (Interfaces): IKullanici arayüzü üzerinden kullanıcı kimlik ve isim bilgileri standartlaştırılmıştır.
- Soyut Sınıflar (Abstract Classes): TemelKullanici sınıfı ile ortak kullanıcı özellikleri soyutlanmıştır.
- Kalıtım (Inheritance): Kütüphaneci ve Üye sınıfları, TemelKullanici sınıfından türetilmiştir.
- Çok Biçimlilik (Polymorphism): BilgiGoruntule metodu her alt sınıfta özelleştirilerek (method overriding) kullanılmıştır.
- Kapsülleme (Encapsulation): Erişim belirleyiciler aracılığıyla veri güvenliği sağlanmıştır.
- Veri Yapıları: Ödünç alma işlemlerinde hızlı erişim için Dictionary koleksiyon yapısı tercih edilmiştir.

## Fonksiyonel Özellikler

- Sisteme yeni kitap, üye ve kütüphaneci kaydı yapılabilmektedir.
- Kitapların türüne göre ilgili uzmanlık alanındaki kütüphanecilerle eşleştirilmesi sağlanmaktadır.
- Kitap ödünç alma ve iade süreçleri yönetilmektedir.
- İade işlemlerinde kitap türüne göre (Bilim, Kurgu, Tarih, Teknoloji) dinamik ücret hesaplaması yapılmaktadır.
- Tüm veri listeleri konsol üzerinden görüntülenebilmektedir.

## Çalıştırma Talimatları

1. Projeyi yerel dizine klonlayın.
2. Visual Studio veya ilgili IDE üzerinden projeyi derleyin.
3. Console ekranı üzerinden Switch-Case yapısıyla kurgulanmış menüyü kullanarak işlemleri gerçekleştirin.
