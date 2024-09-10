# Görev Takip Sistemi

Görev Takip Sistemi, kullanıcıların görevlerini yönetmelerine ve takip etmelerine olanak sağlayan bir web uygulamasıdır. Bu proje, C# ve ASP.NET Core kullanılarak geliştirilmiştir ve görevlerin atamasını, ilerlemesini, ve durumlarını kolayca takip etmeyi sağlar.

## İçindekiler

- [Başlarken](#başlarken)
- [Proje Yapısı](#proje-yapısı)
- [Kurulum](#kurulum)
- [Kullanım](#kullanım)
- [Katkıda Bulunanlar](#katkıda-bulunanlar)
- [Lisans](#lisans)

## Başlarken

Bu belgede, Görev Takip Sistemi'ni nasıl kuracağınız ve kullanacağınız ile ilgili temel bilgileri bulabilirsiniz.

### Ön Koşullar

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) (veya daha yeni bir sürüm)
- [Visual Studio](https://visualstudio.microsoft.com/) (veya [VS Code](https://code.visualstudio.com/))
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) veya başka bir ilişkisel veritabanı

## Proje Yapısı

Bu proje aşağıdaki ana bileşenleri içerir:

- **Controllers**: API ve web isteklerini işleyen kontrolcüler.
- **Models**: Veritabanı tablolarını ve iş mantığını temsil eden modeller.
- **Views**: Kullanıcı arayüzü bileşenleri için Razor sayfaları.
- **DTOs**: Veri transfer nesneleri.
- **Repositories**: Veritabanı işlemlerini yöneten sınıflar.

## Kurulum

1. **Proje Dosyalarını Klonlayın**

    ```bash
    git clone https://github.com/alperenaliak05/GorevTakip.git
    cd GorevTakip
    ```

2. **Bağımlılıkları Yükleyin**

    Visual Studio veya komut satırı kullanarak bağımlılıkları yükleyin:

    ```bash
    dotnet restore
    ```

3. **Veritabanı Yapılandırması**

    Veritabanını yapılandırmak için gerekli ayarları yapın ve veritabanını oluşturun. `appsettings.json` dosyasını düzenleyerek veritabanı bağlantı bilgilerini girin.

4. **Uygulamayı Çalıştırın**

    Projeyi çalıştırmak için aşağıdaki komutu kullanın:

    ```bash
    dotnet run
    ```

## Kullanım

- **Görev Atama**: Görevleri kullanıcılara veya departmanlara atayabilirsiniz.
- **Görev İlerleme**: Görevlerin ilerleme durumlarını güncelleyebilir ve takip edebilirsiniz.
- **Görev Raporları**: Görevlerle ilgili raporlar görüntüleyebilirsiniz.

### Örnek Kullanım

- **Yeni Görev Ekleme**: [Ekran görüntüleri ve açıklamalar ekleyin]
- **Görev Durumunu Güncelleme**: [Ekran görüntüleri ve açıklamalar ekleyin]

## Katkıda Bulunanlar

- [Alperen Ali Ak](https://github.com/alperenaliak05) - Proje sahibi ve geliştirici

## Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.
