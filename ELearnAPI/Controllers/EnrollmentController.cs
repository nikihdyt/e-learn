using ELearnAPI.EfCore;
using ELearnAPI.Model;
using ELearnAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELearnAPI.Controllers
{
    [Route("api/enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentService _db;
        public EnrollmentController(EF_DataContext eF_DataContext)
        {
            _db = new EnrollmentService(eF_DataContext);
        }

        // create an enrollment
        [HttpPost]
        public IActionResult Post([FromBody] Enrollment enrollmentData)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.Create(enrollmentData);
                return Ok(ResponseHandler.GetResponseOk(responseType, enrollmentData));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        // get enrollment by course
        [HttpGet("course/{courseId}")]
        public IActionResult GetByCourse(int courseId)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                IEnumerable<Enrollment> data = _db.GetByCourse(courseId);
                if (!data.Any())
                {
                    responseType = ResponseType.Success;
                }
                return Ok(ResponseHandler.GetResponseOk(responseType, data));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetResponseBadRequest(e));
            }
        }

        // get enrollment by student
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                IEnumerable<Enrollment> data = _db.GetByUser(userId);
                if (!data.Any())
                {
                    responseType = ResponseType.Success;
                }
                return Ok(ResponseHandler.GetResponseOk(responseType, data));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetResponseBadRequest(e));
            }
        }

    }
}
