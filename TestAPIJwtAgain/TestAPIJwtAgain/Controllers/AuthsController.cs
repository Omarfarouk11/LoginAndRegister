using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAPIJwtAgain.Model;
using TestAPIJwtAgain.Services;

namespace TestAPIJwtAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authservice;
        public AuthsController(IAuthService authService)
        {
            _authservice = authService;

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
         if(!ModelState.IsValid) 
           {   

                return BadRequest(ModelState);
           }
         var result=await _authservice.Registerasync(model);
         if(!result.isAuthnticated)
            {
                return BadRequest(result.message);

            }
         return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Loginasync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            var result = await _authservice.LoginAsync(model);
            if (!result.isAuthnticated)
            {
                return BadRequest(result.message);

            }
            return Ok(result);
        }
        [HttpPost("AddRole")]
        public async Task<IActionResult> Loginasync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            var result = await _authservice.AddRoleAsync(model);
            if(result != string.Empty)
            {
                return BadRequest(result);
            }
          
            return Ok(model);
        }


    }
}
