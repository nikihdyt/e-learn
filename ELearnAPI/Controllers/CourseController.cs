using ELearnAPI.EfCore;
using ELearnAPI.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ELearnAPI.Controllers
{
    
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DbHelper _db;
        public CourseController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelper(eF_DataContext);
        }

        // GET: api/<CourseController>
        [Route("api/courses")]
        [HttpGet]
        public IActionResult Get()
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                IEnumerable<CourseModel> data = _db.GetCourses();
                if (!data.Any())
                {
                    responseType = ResponseType.Success;
                }
                return Ok(ResponseHandler.GetResponseOk(responseType, data));
            }
            catch (Exception e)
            {
                responseType = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        [Route("api/course/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                CourseModel data = _db.GetCourseById(id);
                if (data == null)
                {
                    responseType = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetResponseOk(responseType, data));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        // POST api/<CourseController>
        [Route("api/course")]
        [HttpPost]
        public IActionResult Post([FromBody] Course courseData)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.UpsertCourse(courseData);
                return Ok(ResponseHandler.GetResponseOk(responseType, courseData));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        // PUT api/<CourseController>/5
        [Route("api/course/update/{id}")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course courseData)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.UpsertCourse(courseData);
                return Ok(ResponseHandler.GetResponseOk(responseType, courseData));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        // DELETE api/<CourseController>/5
        [Route("api/course/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.DeleteCourse(id);
                return Ok(ResponseHandler.GetResponseOk(responseType, null));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }
    }
}
