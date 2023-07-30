using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearnAPI.EfCore
{
    [Table("users")]
    public class User
    {
        [Key, Required]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
