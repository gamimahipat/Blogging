using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingAPI.v1
{
    [Table("Users")]
    public class Users : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string MobileNo { get; set; }
        public required string PasswordHash { get; set; }
        public required string Salt { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
    }
}
