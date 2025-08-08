using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Procrastinator.Models;
using Procrastinator.Services;

namespace Procrastinator.Controllers
{
        [Route("[controller]")]
        [Authorize]
        [ApiController]
        public class UserController : ControllerBase
        {

            private readonly AuthService authService;
            private readonly UserService userService;

            public UserController(
                AuthService authService,
                UserService userService
            )
            {
                this.authService = authService;
                this.userService = userService;
            }

            [AllowAnonymous]
            [EnableCors]
            [Route("register")]
            [HttpPost]
            public async Task<IActionResult> Register([FromBody] UserCreateDTO model)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        throw new Exception("Validation issue");
                    }

                    var result = await authService.Register(model);

                    return Ok(result);
                } catch(Exception e)
                {
                return BadRequest(e.Message);
                }
            }


            [EnableCors]
            [Route("update")]
            [HttpPatch]
            [AllowAnonymous]

            public async Task<IActionResult> Update([FromBody] UserCreateDTO model)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var result = await authService.Update(model, HttpContext.User);

                    return Ok(result);
                } catch
                {
                    throw;
                }
                
            }


            [AllowAnonymous]
            [Route("login")]
            [HttpPost]
            public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] UserLoginDTO model)
            {
                try
                {
                    if (!ModelState.IsValid) { throw new Exception("Login failed"); };

                    var result = await authService.Login(model, HttpContext.Response);
    
                    return Ok(result);
                    
                } catch
                {
                    throw;
                }
            }

            [Route("email/{email}")]
            [HttpGet]
            public async Task<ActionResult<UserResponseDTO?>> GetUserByEmail([FromRoute] string email)
            {
                UserResponseDTO? user = await userService.GetUserByEmail(email);
                if (user == null) return NotFound();
                return Ok(user);
            }
        }
}
