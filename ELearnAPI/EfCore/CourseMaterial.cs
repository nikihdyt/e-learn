using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearnAPI.EfCore
{
    [Table("course_materials")]
    public class CourseMaterial
    {
        [Key,Required]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
