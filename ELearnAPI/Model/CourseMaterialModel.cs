using ELearnAPI.EfCore;

namespace ELearnAPI.Model
{
    public class CourseMaterialModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
