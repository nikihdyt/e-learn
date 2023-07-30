using ELearnAPI.EfCore;
using ELearnAPI.Model;
using ELearnAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearnAPI.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class CourseMaterialController : ControllerBase
    {
        private readonly CourseMaterialService _db;
        public CourseMaterialController(EF_DataContext eF_DataContext)
        {
            _db = new CourseMaterialService(eF_DataContext);
        }

        // Get materials by course id
        [HttpGet]
        [Route("api/materials/{courseId}")]
        public IActionResult GetMaterials(int courseId)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                IEnumerable<CourseMaterialModel> data = _db.GetMaterialByCourseId(courseId);
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

        // get material by id
        [HttpGet]
        [Route("api/material/{id}")]
        public IActionResult GetMaterial(int id)
        {
            ResponseType responseType = ResponseType.Success;
            try
            {
                CourseMaterial data = _db.GetMaterialById(id);
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

        // create a material in a specific course
        [Route("api/course_material")]
        [HttpPost]
        public IActionResult Post([FromBody] CourseMaterial courseMaterialData)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.CreateCourseMaterial(courseMaterialData);
                return Ok(ResponseHandler.GetResponseOk(responseType, courseMaterialData));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        // update material in a specific course
        [Route("api/material/{id}")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CourseMaterial materialData)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                CourseMaterial updatedMaterial = _db.UpdateMaterial(id, materialData);
                return Ok(ResponseHandler.GetResponseOk(responseType, updatedMaterial));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        // delete material in a specific course
        [Route("api/material/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.DeleteMaterial(id);
                return Ok(ResponseHandler.GetResponseOk(responseType, null));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

    }
}
