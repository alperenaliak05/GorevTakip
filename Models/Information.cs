using System.ComponentModel.DataAnnotations;
namespace Models;
public class Information
{
    public int Id { get; set; }

    [Display(Name = "Başlık")]
    [Required(ErrorMessage = "Başlık alanı zorunludur.")]
    public string Title { get; set; }

    [Display(Name = "İçerik")]
    [Required(ErrorMessage = "İçerik alanı zorunludur.")]
    public string Content { get; set; }

    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; }

    public Users CreatedByUser { get; set; }   

    public int CreatedByUserId { get; set; }
}
