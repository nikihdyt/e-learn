using ELearnAPI.EfCore;
using ELearnAPI.Model;
using ELearnAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELearnAPI.Controllers
{
    [Route("api/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly AssignmentService _db;
        public AssignmentController(EF_DataContext eF_DataContext)
        {
            _db = new AssignmentService(eF_DataContext);
        }

        // create assignment on specific course
        [HttpPost]
        public IActionResult Post([FromBody] Assignment assignmentData)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.CreateAssignment(assignmentData);
                return Ok(ResponseHandler.GetResponseOk(responseType, assignmentData));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }


        // get assginments of a course
        [HttpGet]
        [Route("course/{courseId}")]
        public IActionResult GetAssignments(int courseId)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                IEnumerable<AssignmentModel> data = _db.GetAssignmentsByCourseId(courseId);
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

        // get assignment by id
        [HttpGet("{id}")]
        public IActionResult GetAssignment(int id)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                Assignment data = _db.GetAssignmentById(id);
                if (data == null)
                {
                    responseType = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetResponseOk(responseType, data));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetResponseBadRequest(e));
            }
        }

        // TODO: get assignment of a specific user
        // optional param: duedate > now

        // update assignment
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Assignment assignmentData)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                Assignment updatedAssignment = _db.UpdateAssignment(id, assignmentData);
                return Ok(ResponseHandler.GetResponseOk(responseType, updatedAssignment));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        // delete assignment
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.DeleteAssignment(id);
                return Ok(ResponseHandler.GetResponseOk(responseType, null));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }
    }
}
