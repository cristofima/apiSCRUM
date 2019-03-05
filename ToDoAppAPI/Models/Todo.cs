using System.ComponentModel.DataAnnotations;

namespace ToDoAppAPI.Models
{
    public partial class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}