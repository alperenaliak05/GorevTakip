# GörevTakip Projesi

Bu proje, bir görev yönetim sistemi olan **GörevTakip**'i içerir. Bu sistem, kullanıcıların görevleri görüntülemesini, güncellemesini ve yönetmesini sağlar. Proje, .NET Core ve Entity Framework Core kullanılarak geliştirilmiştir.

## İçindekiler

- [Özellikler](#özellikler)
- [Kurulum](#kurulum)
- [Kullanım](#kullanım)
- [Ekran Görüntüleri](#ekran-görüntüleri)
- [Katkıda Bulunanlar](#katkıda-bulunanlar)
- [Lisans](#lisans)

## Özellikler

- Kullanıcıların görevlerini yönetmesi
- Görev durumlarını güncelleme
- Kullanıcı ve görev yönetimi
- Basit ve kullanıcı dostu arayüz


## KURULUM

1-Proje Deposunu Kopyalayın:
git clone https://github.com/alperenaliak05/GörevTakip.git

2-Gerekli Bağımlılıkları Yükleyin: Proje dizinine gidin ve gerekli NuGet paketlerini yükleyin.
cd GörevTakip
dotnet restore

3-Veritabanını Göç Ettirin:Veritabanı şemasını oluşturmak için Entity Framework Core göçlerini çalıştırın.
dotnet ef database update

4-Projeyi Çalıştırın:
dotnet run --project TaskApp_Web
Bu komut, projeyi çalıştıracak ve tarayıcınızda açılacaktır.

## Kullanım

Projeyi çalıştırdıktan sonra, web tarayıcınızda http://localhost:5000 adresine gidin. Bu adreste görev yönetimi işlemlerini gerçekleştirebilirsiniz.

## Ekran Görüntüleri
### Giriş
![Giriş](https://github.com/user-attachments/assets/cca381f9-92ac-4ca2-9596-326dbf05a55b)

### Ana Sayfa
https://github.com/user-attachments/assets/48d0d803-4e31-4be3-b040-f9b26913212d






























