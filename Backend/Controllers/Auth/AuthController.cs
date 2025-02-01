using code_review_analysis_platform.Responses;
using Microsoft.AspNetCore.Mvc;
using code_review_analysis_platform.Models.Auth;
using code_review_analysis_platform.Enums;
using code_review_analysis_platform.Repositories.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace code_review_analysis_platform.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository) {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public ApiResponse<LoginResponseDTO> Login([FromBody] LoginDetails LoginDetails)
        {
            var UserData = new LoginResponseDTO {
                FirstName = "",
                LastName = "",
                DateOfBirth = DateTime.Now,
                Role = Role.Admin,
                UserId = "dasikhcjn",
                AccessibleRoutes = new() { "hi", "hoo", "hhh" },

            };
            return (ApiResponse<LoginResponseDTO>.SuccessResponse(UserData, "sexy"));
        }
        [HttpPost("signup")]
        public async Task<ApiResponse<string>> SignUp([FromBody] SignUpDetails SignUpDetails)
        {
            var respose = await _authRepository.CreateNewUser(SignUpDetails);

            return (respose);
        }
    }
}
