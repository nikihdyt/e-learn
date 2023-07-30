using ELearnAPI.EfCore;
using ELearnAPI.Model;

namespace ELearnAPI.Services
{
    public class EnrollmentService
    {
        private EF_DataContext _DataContext;
        public EnrollmentService(EF_DataContext context)
        {
            _DataContext = context;
        }

        public void Create(Enrollment enrollment)
        {

            _DataContext.Enrollments.Add(enrollment);
            _DataContext.SaveChanges();
        }

        public List<Enrollment> GetByCourse(int courseId)
        {
            List<Enrollment> responseData = new List<Enrollment>();
            var enrollments = _DataContext.Enrollments.Where(d => d.CourseId.Equals(courseId)).ToList();
            enrollments.ForEach(row => responseData.Add(new Enrollment()
            {
                Id = row.Id,
                UserId = row.UserId,
                CourseId = courseId,
                EnrolledDate = row.EnrolledDate,
            }));
            return responseData;
        }

        public List<Enrollment> GetByUser(int userId)
        {
            List<Enrollment> responseData = new List<Enrollment>();
            var enrollments = _DataContext.Enrollments.Where(d => d.UserId.Equals(userId)).ToList();
            enrollments.ForEach(row => responseData.Add(new Enrollment()
            {
                Id = row.Id,
                UserId = row.UserId,
                CourseId = row.CourseId,
                EnrolledDate = row.EnrolledDate,
            }));
            return responseData;
        }
    }
}
