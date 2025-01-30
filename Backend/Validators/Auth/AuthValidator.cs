using code_review_analysis_platform.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace code_review_analysis_platform.Validators.Auth
{
    public class AuthValidator
    {
        public class EmailOrUserIdRequiredAttribute : ValidationAttribute
        {
            public override bool IsValid(object? value)
            {
               var Models = value as Login;
                if (Models == null) {
                    return false;
                }
                return !string.IsNullOrEmpty(Models.UserEmail) || !string.IsNullOrEmpty(Models.UserId);
            }
        }
    }
}
