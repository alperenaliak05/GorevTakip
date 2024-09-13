# Görev Takip Sistemi

Görev Takip Sistemi, kullanıcıların görevlerini yönetmelerine ve takip etmelerine olanak sağlayan bir web uygulamasıdır. Bu proje, C# ve ASP.NET Core kullanılarak geliştirilmiştir ve görevlerin atamasını, ilerlemesini, ve durumlarını kolayca takip etmeyi sağlar. (deneme cengo)

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

## Örnek Sınıflar

### Controller Örneği

```csharp
using DTOs.TaskApp_WebDTO;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace TaskApp_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
```
### Model Örneği
```csharp
namespace Models
{
    public class ToDoTasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? AssignedToDepartmentId { get; set; }
        public Users? AssignedToUser { get; set; }
        public int AssignedByUserId { get; set; }
        public Users AssignedByUser { get; set; }
        public ICollection<TaskReport> TaskReports { get; set; }
        public TaskPriority Priority { get; set; }
        public string? AssignedByUserFirstName => AssignedByUser?.FirstName;
        public string? AssignedByUserLastName => AssignedByUser?.LastName;
        public ICollection<TaskProcess> TaskProcesses { get; set; } = new List<TaskProcess>();
    }

}

```
### DTO Örneği
```csharp
using Models;
using TaskStatus = Models.TaskStatus;

namespace DTOs.TaskApp_WebDTO
{
    public class TaskTrackingDTO
    {
        public int? TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedToUserName { get; set; } 
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; } 
    }
}
```
### Repository Örneği

```csharp
using Models;

namespace Repositories.IReporsitory
{
    public interface IUserBadgeRepository
    {
        Task<IEnumerable<UserBadge>> GetUserBadgesAsync(int userId);
        Task<bool> UserHasBadgeAsync(int userId, int badgeId);
        Task AddUserBadgeAsync(UserBadge userBadge);
    }
}

using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IReporsitory;

namespace TaskApp_Web.Repositories
{
    public class UserBadgeRepository : IUserBadgeRepository
    {
        private readonly TaskAppContext _context;

        public UserBadgeRepository(TaskAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserBadge>> GetUserBadgesAsync(int userId)
        {
            return await _context.UserBadges
                                 .Include(ub => ub.Badge)
                                 .Where(ub => ub.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<bool> UserHasBadgeAsync(int userId, int badgeId)
        {
            return await _context.UserBadges.AnyAsync(ub => ub.UserId == userId && ub.BadgeId == badgeId);
        }

        public async Task AddUserBadgeAsync(UserBadge userBadge)
        {
            await _context.UserBadges.AddAsync(userBadge);
            await _context.SaveChangesAsync();
        }
    }
}
```
### View Örneği
```csharp
@model IEnumerable<Models.Message>

@{
    ViewData["Title"] = "Message History";
}

<div class="container mt-5">
    <h2 class="text-center mb-4">Mesaj Geçmişi</h2>
    <div id="chatWindow" class="border rounded p-3 mb-3" style="height: 500px; overflow-y: auto; background-color: #f8f9fa;">
        <ul id="messagesList" class="list-unstyled">
            @foreach (var message in Model)
            {
                <li class="@((message.SenderId == ViewBag.SenderId) ? "sent" : "received")">
                    <strong>@((message.SenderId == ViewBag.SenderId) ? "You" : $"User {message.SenderId}"):</strong> @message.Content
                    <span class="text-muted" style="font-size: 0.8em;">@message.Timestamp.ToLocalTime().ToString("HH:mm")</span>
                </li>
            }
        </ul>
    </div>
</div>
```

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

### Ana Sayfa

Kullanıcı giriş yaptığında onu karşılayan ekrandır.

![Ana Sayfa](https://github.com/user-attachments/assets/e037f5ee-63b6-4394-be83-85814cb92b4f)

### Tüm Kullanıcılar

Şirket içinde çalışan tüm kullanıcıların müsaitlik durumlarını görüntüleyebilir, tüm kullanıcıların profil bilgilerine ulaşabilirsiniz.

![Tüm Kullanıcılar](https://github.com/user-attachments/assets/7ae6bf19-2019-48e7-802d-c65bab6e9f39)

### Chat

Şirket içi anlık mesajlaşma uygulamasıdır.

![Chat](https://github.com/user-attachments/assets/a355cf5f-2f61-4720-aa7e-31a6b1c6f128)

### Görevlerim

Görevlerinizi görebilir detaylarına erişebilir, detay ekranından süreç ekleyebilirsiniz. Tamamlandığında veya reddedildiğinde bunu bildirmeniz için gerekli butonlarla dönüt sağlayabilirsiniz.

![Görevlerim](https://github.com/user-attachments/assets/5b051b60-205c-4403-aee2-9dbf20ef8c8a)

### Görevlendir

Görevleri kullanıcılara veya departmanlara atayabilirsiniz.

![Görevlendir](https://github.com/user-attachments/assets/b4b77985-30b4-4380-875c-2dfcd625e2b6)

### Görev Takip

Atadığınız görevlerin atanan kullanıcı tarafından süreçlerini görüntüleyebilir, görev durumunu görüntüleyebilirsiniz.

![Görev Takip](https://github.com/user-attachments/assets/a7f53170-a474-4188-820f-dc1fa5209e30)

### Görev İlerleme

Görevlerin ilerleme durumlarını güncelleyebilir ve takip edebilirsiniz.

![Görev İlerleme](https://github.com/user-attachments/assets/4f6aa6f6-cf30-48af-a9d4-b4ef79e9634d)

### Görev Raporları

Görevlerle ilgili raporlar görüntüleyebilirsiniz.

![Görev Raporları](https://github.com/user-attachments/assets/ec470475-a30a-49a8-aed4-fd8e825e4ba6)

### Profilim

Profilim butonuna tıkladığınızda listelenen üç farklı sayfa vardır:
- **Profil Bilgilerim**: Kullanıcı profil bilgilerini, kazandığı rozetleri bu ekranda görüntüleyebilir. O anki durumunu güncelleyebilir (Meşgul, Müsait, İzinli, Toplantıda).
  
  ![Profil Bilgilerim](https://github.com/user-attachments/assets/06728bf8-4c18-444a-99a7-7468184b0828)

- **Yapılacaklar Listem**: Kullanıcı için uygulama içindeki mini bir uygulamadır. Kendine o gün yapacağı her şeyi oluşturabileceği bir alan içerir. Listede yaptığı her şeye tik atabilir ve madde silebilir.
  
  ![Yapılacaklar Listem](https://github.com/user-attachments/assets/b4f9d3f1-97ff-4fb8-af6c-ef1644b08c1e)

- **Şirket Bilgilendirmeleri**: İnsan Kaynakları tarafından atanan, şirket içinde herkesi ilgilendiren olayları tüm kullanıcılara duyuran bilgilendirme servisidir.
  
  ![Şirket Bilgilendirmeleri](https://github.com/user-attachments/assets/ac6dfc69-42d9-403b-9076-c0ca31e6deb3)

### Koyu Tema

Göz yoruculuğunu önlemek için konulmuş bir siyah tema çalışmasıdır. Profilim butonu yanındaki Ay emojisine basıldığında aktif olur.

![Koyu Tema](https://github.com/user-attachments/assets/d9e988a0-a16d-4927-90ca-c2e5c8c2f7b1)

### Örnek Kullanım

- **Yeni Görev Ekleme**: [[Kullanıcı bu ekranda bir kişi veya departman bazlı görev atayabilir, atadığı görevin önceliğini ve bitirilmesi gereken tarihi seçebilir. (Görsel görünüm için tıklayın.)](https://github.com/user-attachments/assets/b45821a4-276e-4a42-bba5-5a569594dbc6)]

- **Görev Durumunu Güncelleme**: [[Görevlerim kısmında bulunan görevin yanında gelen bu butonlar sayesinde görev atanan kullanıcı görevin detayını görebilir, tamamladığında veya reddettiğinde bunu butonlar aracılığıyla bildirir. (Örnek görünüm için tıklayın.)](https://github.com/user-attachments/assets/c668f95f-c7d0-4d83-b35b-cad6493a75f4)]

## Katkıda Bulunanlar

- [Alperen Ali Ak](https://github.com/alperenaliak05) - Proje sahibi ve geliştirici

## Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.
