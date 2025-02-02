using code_review_analysis_platform.Responses;
using Microsoft.AspNetCore.Mvc;
using code_review_analysis_platform.Models.Auth;
using code_review_analysis_platform.Enums;
using code_review_analysis_platform.Repositories.Auth;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost("signin")]
        public async Task<ApiResponse<LoginResponseDTO>> Login([FromBody] LoginDetails LoginDetails)
        {
            if(LoginDetails.UserEmail.IsNullOrEmpty() && LoginDetails.UserId.IsNullOrEmpty())
            {
                return (ApiResponse<LoginResponseDTO>.ErrorResponse("fuck you bitch nigga"));
            }
            var response = await _authRepository.SignIn(LoginDetails);
            return (response);
        }

        [HttpPost("signup")]
        public async Task<ApiResponse<string>> SignUp([FromBody] SignUpDetails SignUpDetails)
        {
            var response = await _authRepository.CreateNewUser(SignUpDetails);

            return (response);
        }
    }
}
