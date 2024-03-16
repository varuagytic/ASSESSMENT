using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Forename { get; set; }

        [StringLength(100)]
        [Required]
        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public User()
        {
            Id = 0;
            Forename = "";
            Surname = "";
            Email = "";
            IsActive = true;
            DateOfBirth = DateTime.UtcNow;
        }
    }
}