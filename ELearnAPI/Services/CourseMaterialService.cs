using ELearnAPI.EfCore;
using ELearnAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearnAPI.Services
{
    public class CourseMaterialService
    {
        private EF_DataContext _DataContext;
        public CourseMaterialService(EF_DataContext context)
        {
            _DataContext = context;
        }

        public void CreateCourseMaterial(CourseMaterial courseMaterial)
        {
            
            _DataContext.CoursesMaterial.Add(courseMaterial);
            _DataContext.SaveChanges();
        }

        public List<CourseMaterialModel> GetMaterialByCourseId(int courseId)
        {
            List<CourseMaterialModel> responseData = new List<CourseMaterialModel>();
            var courses = _DataContext.CoursesMaterial.Where(d => d.CourseId.Equals(courseId)).ToList();
            courses.ForEach(row => responseData.Add(new CourseMaterialModel()
            {
                Id = row.Id,
                Title = row.Title,
                Type = row.Type,
                Content= row.Content,
            }));
            return responseData;
        }
        public CourseMaterial GetMaterialById(int id)
        {
            CourseMaterialModel responseData = new CourseMaterialModel();
            var material = _DataContext.CoursesMaterial.Where(d => d.Id.Equals(id)).FirstOrDefault();
            try
            {
                return new CourseMaterial()
                {
                    Id = material.Id,
                    CourseId = material.CourseId,
                    Title = material.Title,
                    Type = material.Type,
                    Content = material.Content,
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public CourseMaterial UpdateMaterial(int id, CourseMaterial material)
        {
            CourseMaterial dbTable = _DataContext.CoursesMaterial.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (dbTable != null)
            {
                // update
                dbTable.Title = material.Title;
                dbTable.Type = material.Type;
                dbTable.Content = material.Content;
                _DataContext.Entry(dbTable).State = EntityState.Modified;
                _DataContext.SaveChanges();

                return new CourseMaterial()
                {
                    Id = dbTable.Id,
                    CourseId = dbTable.CourseId,
                    Title = dbTable.Title,
                    Type = dbTable.Type,
                    Content = dbTable.Content,
                };
            }
            else 
            {
                throw new Exception("material with id " + id + " was not found");
            }
        }

        public void DeleteMaterial(int id)
        {
            var material = _DataContext.CoursesMaterial.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (material != null)
            {
                _DataContext.CoursesMaterial.Remove(material);
                _DataContext.SaveChanges();
            }
        }
    }
}
