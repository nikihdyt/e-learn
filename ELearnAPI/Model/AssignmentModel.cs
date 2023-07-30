using ELearnAPI.EfCore;

namespace ELearnAPI.Model
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DueDate { get; set; }
    }
}
