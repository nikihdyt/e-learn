using ELearnAPI.EfCore;
using ELearnAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearnAPI.Services
{
    public class AssignmentService
    {
        private EF_DataContext _DataContext;
        public AssignmentService(EF_DataContext context)
        {
            _DataContext = context;
        }
        public void CreateAssignment(Assignment assignment)
        {
            _DataContext.Assignments.Add(assignment);
            _DataContext.SaveChanges();
        }

        public List<AssignmentModel> GetAssignmentsByCourseId(int courseId)
        {
            List<AssignmentModel> responseData = new List<AssignmentModel>();
            var assignments = _DataContext.Assignments.Where(d => d.CourseId.Equals(courseId)).ToList();
            assignments.ForEach(row => responseData.Add(new AssignmentModel()
            {
                Id = row.Id,
                Title = row.Title,
                Description = row.Description,
                DueDate = row.DueDate,
            }));
            return responseData;
        }

        public Assignment GetAssignmentById(int id)
        {
            AssignmentModel responseData = new AssignmentModel();
            var assignment = _DataContext.Assignments.Where(d => d.Id.Equals(id)).FirstOrDefault();
            try
            {
                return new Assignment()
                {
                    Id = assignment.Id,
                    CourseId = assignment.CourseId,
                    Title = assignment.Title,
                    Description = assignment.Description,
                    DueDate = assignment.DueDate,
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Assignment UpdateAssignment(int id, Assignment assignment)
        {
            Assignment dbTable = _DataContext.Assignments.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (dbTable != null)
            {
                // update
                dbTable.Title = assignment.Title;
                dbTable.Description = assignment.Description;
                dbTable.DueDate = assignment.DueDate;
                _DataContext.Entry(dbTable).State = EntityState.Modified;
                _DataContext.SaveChanges();

                return new Assignment()
                {
                    Id = dbTable.Id,
                    CourseId = dbTable.CourseId,
                    Title = dbTable.Title,
                    Description = dbTable.Description,
                    DueDate = dbTable.DueDate,
                };
            }
            else
            {
                throw new Exception("assignment with id " + id + " was not found");
            }
        }

        public void DeleteAssignment(int id)
        {
            var assignment = _DataContext.Assignments.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (assignment != null)
            {
                _DataContext.Assignments.Remove(assignment);
                _DataContext.SaveChanges();
            }
        }
    }
}
