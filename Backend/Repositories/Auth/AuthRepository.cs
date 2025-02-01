using code_review_analysis_platform.Data;
using code_review_analysis_platform.Data.Entities;
using code_review_analysis_platform.Models.Auth;
using code_review_analysis_platform.Responses;
using Microsoft.EntityFrameworkCore;

namespace code_review_analysis_platform.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<ApiResponse<string>> CreateNewUser(SignUpDetails SignUpDetails);
    }
    public class AuthRepository: IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<string>> CreateNewUser(SignUpDetails NewUser)
        {
            bool emailExists = await _context.Users.AnyAsync(u => u.UserEmail == NewUser.UserEmail);
            if (emailExists)
            {
                return ApiResponse<string>.ErrorResponse("Email already exists.");
            }

            // Check if the user ID already exists in the database
            bool userIdExists = await _context.Users.AnyAsync(u => u.UserId == NewUser.UserId);
            if (userIdExists)
            {
                return ApiResponse<string>.ErrorResponse("User ID already exists.");
            }

            User NewUserDb = new User
            {
                UserEmail = NewUser.UserEmail,
                UserId = NewUser.UserId,
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                MiddleName = NewUser.MiddleName,
                DateOfBirth = NewUser.DateOfBirth,
                Phone = NewUser.Phone
            };
            _context.Users.Add(NewUserDb);
            await _context.SaveChangesAsync();
            return ApiResponse<string>.SuccessResponse(string.Empty,"User Created Successfully");
        }
    }
}
