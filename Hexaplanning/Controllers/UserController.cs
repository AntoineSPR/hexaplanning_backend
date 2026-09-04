using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Hexaplanning.Models;
using Hexaplanning.Services;

namespace Hexaplanning.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AuthService authService;
        private readonly UserService userService;
        private readonly SendMailService mailService;
        private readonly ILogger<UserController> logger;

        public UserController(
            AuthService authService,
            UserService userService,
            SendMailService mailService,
            ILogger<UserController> logger
        )
        {
            this.authService = authService;
            this.userService = userService;
            this.mailService = mailService;
            this.logger = logger;
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
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [EnableCors]
        [Route("update")]
        [HttpPatch]
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
            }
            catch
            {
                throw;
            }

        }

        [EnableCors]
        [Route("name")]
        [HttpPut]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await authService.UpdateName(model, HttpContext.User);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [EnableCors]
        [Route("change-password")]
        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO passwordData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await authService.ChangePassword(passwordData, HttpContext.User);

                return Ok(new { message = "Mot de passe modifi� avec succ�s" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] UserLoginDTO model)
        {
            try
            {
                if (!ModelState.IsValid) { throw new Exception("Login failed"); }
                ;

                var result = await authService.Login(model);

                return Ok(result);

            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }

        [AllowAnonymous]
        [EnableCors]
        [Route("refresh")]
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO model)
        {
            try
            {
                if (!ModelState.IsValid) { throw new Exception("Refresh failed"); }

                var result = await authService.RefreshAsync(model.RefreshToken);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Expected rejection (expired/reused/unknown refresh token) - worth a trace to
                // correlate client-reported disconnections with which case actually fired, but not
                // an application error.
                logger.LogWarning("Refresh token rejected: {Reason}", ex.Message);
                return new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            catch (Exception ex)
            {
                // Anything else here is a bug, not a normal auth outcome - it was previously
                // swallowed into the same bare 401 as the expected case above, making it
                // indistinguishable from the client and invisible in the logs.
                logger.LogError(ex, "Unexpected error while refreshing token");
                return new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
        }

        [AllowAnonymous]
        [EnableCors]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDTO model)
        {
            await authService.RevokeRefreshTokenAsync(model.RefreshToken);
            return Ok();
        }

        [Route("email/{email}")]
        [HttpGet]
        public async Task<ActionResult<UserResponseDTO?>> GetUserByEmail([FromRoute] string email)
        {
            UserResponseDTO? user = await userService.GetUserByEmail(email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [AllowAnonymous]
        [EnableCors]
        [Route("forgot-password/{email}")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromRoute] string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await mailService.SendPasswordResetEmail(email);

                return Ok(new { message = "Si votre email existe, un lien de r�initialisation a �t� envoy�." });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [AllowAnonymous]
        [EnableCors]
        [Route("reset-password")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await authService.ResetPassword(model);

                return Ok(new { message = "Mot de passe r�initialis� avec succ�s." });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        //[AllowAnonymous]
        //[EnableCors]
        //[Route("send-mail")]
        //[HttpPost]
        //public async Task<IActionResult> SendMail([FromBody] Mail mail)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        await mailService.SendEmail(mail);

        //        return Ok(new { message = "Email envoy�." });
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { message = e.Message });
        //    }
        //}
    }
}
