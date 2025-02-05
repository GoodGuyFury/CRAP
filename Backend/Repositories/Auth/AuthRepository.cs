using code_review_analysis_platform.Data;
using code_review_analysis_platform.Data.Entities;
using code_review_analysis_platform.Helpers;
using code_review_analysis_platform.Models.Auth;
using code_review_analysis_platform.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace code_review_analysis_platform.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<ApiResponse<string>> CreateNewUser(SignUpDetailsDTO SignUpDetails);
        Task<ApiResponse<LoginResponseDTO>> SignIn(LoginDetailsDTO user);
    }
    public class AuthRepository: IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<string>> CreateNewUser(SignUpDetailsDTO NewUser)
        {
            try
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

                User newUserDb = new User
                {
                    UserEmail = NewUser.UserEmail,
                    UserId = NewUser.UserId,
                    FirstName = NewUser.FirstName,
                    LastName = NewUser.LastName,
                    MiddleName = NewUser.MiddleName,
                    DateOfBirth = NewUser.DateOfBirth,
                    Phone = NewUser.Phone
                };
                Credentials newUserCred = new Credentials
                {
                    UserId = NewUser.UserId,
                    Password = BCryptHelper.HashPassword(NewUser.Password),
                    User = newUserDb
                };

                _context.Users.Add(newUserDb);
                _context.Credentials.Add(newUserCred);
                await _context.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse(string.Empty, "User Created Successfully");
            } catch (Exception ex)
            {
                return ApiResponse<string>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<LoginResponseDTO>> SignIn(LoginDetailsDTO user)
        {
            if (string.IsNullOrEmpty(user.UserEmail) && string.IsNullOrEmpty(user.UserId))
            {
                return ApiResponse<LoginResponseDTO>.ErrorResponse("You have to provide either Email or UserId");
            }

            var dbUser = !string.IsNullOrEmpty(user.UserId)
                ? await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId)
                : await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);

            if (dbUser == null)
            {
                return ApiResponse<LoginResponseDTO>.ErrorResponse("User not found");
            }

            var dbUserCred = await _context.Credentials.FirstOrDefaultAsync(u => u.UserId == dbUser.UserId);
            if (dbUserCred == null)
            {
                return ApiResponse<LoginResponseDTO>.ErrorResponse("Invalid credentials");
            }

            if (!BCryptHelper.VerifyPassword(user.Password, dbUserCred.Password))
            {
                return ApiResponse<LoginResponseDTO>.ErrorResponse("Invalid Password");
            }

            var dbUserDetails = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dbUserCred.UserId);
            if (dbUserDetails == null)
            {
                return ApiResponse<LoginResponseDTO>.ErrorResponse("User details not found");
            }

            LoginResponseDTO returnUser = new LoginResponseDTO
            {
                UserEmail = dbUserDetails.UserEmail,
                UserId = dbUserDetails.UserId,
                AccessibleRoutes = new List<string>(),
                DateOfBirth = dbUserDetails.DateOfBirth,
                FirstName = dbUserDetails.FirstName,
                LastName = dbUserDetails.LastName,
                MiddleName = dbUserDetails.MiddleName,
                Role = Enums.Role.Admin
            };

            return ApiResponse<LoginResponseDTO>.SuccessResponse(returnUser);
        }

    }
}
