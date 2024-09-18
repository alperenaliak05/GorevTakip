using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçersiz Email Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Departman seçimi gereklidir.")]
        public int DepartmentId { get; set; }
        public SelectList? Departments { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçimi gereklidir.")]
        public string Gender { get; set; }

        [Phone(ErrorMessage = "Geçersiz Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Çalışma saatleri gereklidir.")]
        public string WorkingHours { get; set; }
    }
}
