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

```bash
git clone https://github.com/alperenaliak05/GörevTakip.git
```
2-Gerekli Bağımlılıkları Yükleyin: Proje dizinine gidin ve gerekli NuGet paketlerini yükleyin.
```bash
cd GörevTakip
dotnet restore
```
3-Veritabanını Göç Ettirin:Veritabanı şemasını oluşturmak için Entity Framework Core göçlerini çalıştırın.
```bash
dotnet ef database update
```
4-Projeyi Çalıştırın:
```bash
dotnet run --project TaskApp_Web
```
Bu komut, projeyi çalıştıracak ve tarayıcınızda açılacaktır.

## Kullanım

Projeyi çalıştırdıktan sonra, web tarayıcınızda http://localhost:5000 adresine gidin. Bu adreste görev yönetimi işlemlerini gerçekleştirebilirsiniz.

## Ekran Görüntüleri
### Giriş
![Giriş](https://github.com/user-attachments/assets/cca381f9-92ac-4ca2-9596-326dbf05a55b)

### Ana Sayfa
![ANA SAYFA](https://github.com/user-attachments/assets/48d0d803-4e31-4be3-b040-f9b26913212d)

### Tüm Kullanıcılar
![TÜM K](https://github.com/user-attachments/assets/57f6d5fa-f140-40c8-b219-35b01f308339)

### Görevlerim
![GÖREVLERİM](https://github.com/user-attachments/assets/6d9cc8fd-3ffe-4397-a9b5-30329fa92007)

### Görev Oluştur
![GÖREV OLUŞTUR](https://github.com/user-attachments/assets/1f68af99-60de-45a9-b869-48139775e96f)

### Görev Takibi
![GÖREV TAKİBİ](https://github.com/user-attachments/assets/9f190261-9de4-41aa-9248-245ca2a1c475)

### Profilim 
![PROFİL](https://github.com/user-attachments/assets/76c4c082-1b6c-4cf2-81fe-0f201a1ef5d3)










































