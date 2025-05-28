using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Başlık 50 karakterden fazla olamaz!")]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Date { get; set; }
    }
}
