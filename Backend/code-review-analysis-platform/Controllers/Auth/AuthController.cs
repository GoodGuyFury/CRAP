using code_review_analysis_platform.Responses;
using Microsoft.AspNetCore.Mvc;
using code_review_analysis_platform.Models.Auth;
using code_review_analysis_platform.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace code_review_analysis_platform.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<AuthController>
        [HttpPost("login")]
        public ApiResponse<LoginResponseDTO> Get()
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

        //// GET api/<AuthController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<AuthController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<AuthController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AuthController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
