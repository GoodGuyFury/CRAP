using BCrypt.Net;

namespace code_review_analysis_platform.Helpers
{
    public static class BCryptHelper
    {
        /// <summary>
        /// Hashes a password using BCrypt with a default work factor of 12.
        /// </summary>
        /// <param name="password">The plain-text password.</param>
        /// <returns>Hashed password.</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies a plain-text password against a stored BCrypt hash.
        /// </summary>
        /// <param name="password">The plain-text password.</param>
        /// <param name="hashedPassword">The hashed password from the database.</param>
        /// <returns>True if the password is valid, false otherwise.</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
