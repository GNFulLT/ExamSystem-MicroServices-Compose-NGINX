using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth_service.Models
{
    [Table("sessions")]
    public class Session
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("session_key")]
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string SessionKey {  get; set; }

        [Column("user_id")]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public User User { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Column("expire_date")]
        public DateTime ExpireDate { get; set; }

        public Session()
        {
            SessionKey = "merabalar";
            UserID = 1;
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
            ExpireDate = DateTime.UtcNow.AddDays(1);
        }
    }
}
