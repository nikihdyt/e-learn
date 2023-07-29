using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearnAPI.EfCore
{
    [Table("courses")]
    public class Course
    {
        [Key, Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Prerequisites { get; set; }
    }
}
