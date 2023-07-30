using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearnAPI.EfCore
{
    [Table("enrollments")]
    public class Enrollment
    {
        [Key, Required]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public DateTimeOffset EnrolledDate { get; set; }
    }
}
