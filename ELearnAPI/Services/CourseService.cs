using ELearnAPI.EfCore;
using ELearnAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearnAPI.Services
{
    public class CourseService
    {
        private EF_DataContext _context;
        public CourseService(EF_DataContext context)
        {
            _context = context;
        }
        public List<CourseModel> GetCourses()
        {
            List<CourseModel> responseData = new List<CourseModel>();
            var courses = _context.Courses.ToList();
            courses.ForEach(row => responseData.Add(new CourseModel()
            {
                Id = row.Id,
                Title = row.Title,
                Description = row.Description,
                Prerequisites = row.Prerequisites,
            }));
            return responseData;
        }

        public CourseModel GetCourseById(int id)
        {
            CourseModel responseData = new CourseModel();
            var course = _context.Courses.Where(d => d.Id.Equals(id)).FirstOrDefault();
            try
            {
                return new CourseModel()
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Prerequisites = course.Prerequisites,
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void UpsertCourse(Course course)
        {
            if (course.Id > 0)
            {
                Course dbTable = _context.Courses.Find(course.Id);
                // dbTable = _context.Courses.Where(d => d.Id.Equals(course.Id)).FirstOrDefault();
                if (dbTable != null)
                {
                    // update
                    dbTable.Title = course.Title;
                    dbTable.Description = course.Description;
                    dbTable.Prerequisites = course.Prerequisites;
                    _context.Entry(dbTable).State = EntityState.Modified;
                }
            }
            else
            {
                // insert
                _context.Courses.Add(course);
            }
            _context.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            var course = _context.Courses.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }
    }
}
