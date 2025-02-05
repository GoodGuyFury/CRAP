using code_review_analysis_platform.Responses;
using Microsoft.AspNetCore.Mvc;
using code_review_analysis_platform.Models.Auth;
using code_review_analysis_platform.Repositories.Auth;

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

        [HttpPost("signin")]
        public async Task<ApiResponse<LoginResponseDTO>> Login([FromBody] LoginDetailsDTO LoginDetails)
        {

            var response = await _authRepository.SignIn(LoginDetails);
            return (response);
        }

        [HttpPost("signup")]
        public async Task<ApiResponse<string>> SignUp([FromBody] SignUpDetailsDTO SignUpDetails)
        {
            var response = await _authRepository.CreateNewUser(SignUpDetails);

            return (response);
        }
    }
}
