using code_review_analysis_platform.Enums;
using System.ComponentModel.DataAnnotations;
using static code_review_analysis_platform.Validators.Auth.AuthValidator;

namespace code_review_analysis_platform.Models.Auth
{
        public class LoginDetails
        {
            [EmailOrUserIdRequiredAttribute]
            public string? UserId { get; set; }
            public string? UserEmail { get; set; }

            [Required(ErrorMessage ="Password is required")]
            public required string Password { get; set; }
        }
        public class LoginResponseDTO
        {
            public required string FirstName { get; set; }
            public string? MiddleName { get; set; }
            public string? LastName { get; set; }
            public required DateTime DateOfBirth { get; set; }
            public required string UserId { get; set; }
            public string? UserEmail { get; set; }
            public required Role Role { get; set; }
            public required List<string> AccessibleRoutes { get; set; }
        }
        public class SignUpDetails
        {
            public required string FirstName { get; set; }
            public string? MiddleName { get; set; }
            public string? LastName { get; set; }
            public required DateTime DateOfBirth { get; set; }
            public required string UserId { get; set; }
            public string? UserEmail { get; set; }
            public required string Password { get; set; }
    }
}
