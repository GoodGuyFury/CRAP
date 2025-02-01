using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace code_review_analysis_platform.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "NVARCHAR(50)")]
        public required string UserId { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        [Column(TypeName = "NVARCHAR(100)")]
        public required string UserEmail { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "NVARCHAR(50)")]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "NVARCHAR(50)")]
        public string? LastName { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "NVARCHAR(50)")]
        public string? MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "DATE")]
        public required DateTime DateOfBirth { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(15)")]
        public required string Phone { get; set; }
    }
    public class  Credentials
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "NVARCHAR(255)")]
        public required string Password { get; set; }

        public User? User { get; set; }
    }
    
}
