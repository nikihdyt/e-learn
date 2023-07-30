using ELearnAPI.EfCore;
using ELearnAPI.Model;
using ELearnAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELearnAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _db;
        public UserController(EF_DataContext eF_DataContext, JwtUtils jwtUtils)
        {
            _db = new UserService(eF_DataContext, jwtUtils);
        }

        // register
        [Route("api/register")]
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _db.Register(user);
                return Ok(ResponseHandler.GetResponseOk(responseType, "Registered succesfully"));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        [Route("api/login")]
        [HttpPost]
        public IActionResult Login([FromBody] AuthenticateRequest request)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                AuthenticateResponse res = _db.Authenticate(request);
                return Ok(ResponseHandler.GetResponseOk(responseType, res));
            }
            catch (Exception e)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }


    }
}
