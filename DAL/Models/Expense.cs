using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public double Payment { get; set; } 
        public string? Comment { get; set; }
        [Required]
        public Account?  Account { get; set; }
        [Required]
        public Category? Category { get; set; }

        public bool? IsDeleted { get; set; }

    }
}
