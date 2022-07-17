using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth_service.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("name")]
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Name { get; set; }


        [Column("surname")]
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Surname { get; set; }

        public User()
        {
            Id = 1;
            Email = "gnfugur1402@hotmail.com";
            Name = "Uğur";
            Surname = "Öztürk";
        }
    }
}
