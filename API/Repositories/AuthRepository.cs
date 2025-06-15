using Microsoft.Data.SqlClient;
using API.Data.Interfaces;
using API.Models.Requests;
using API.Repositories.Interfaces;
using System.Data;

namespace API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDatabaseService _db;

        public AuthRepository(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<bool> UserExistsAsync(string email, string username)
        {
            var query = "SELECT COUNT(1) FROM Users WHERE Email = @Email OR Username = @Username";
            var parameters = new List<SqlParameter>
        {
             new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email },
             new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username }
        };

            var result = await _db.ExecuteScalarAsync(query, parameters);
            var count = Convert.ToInt32(result);

            return count > 0;
        }

        public async Task InsertUserAsync(RegisterRequest request)
        {
            var query = @"
            INSERT INTO Users (Username, Email, PasswordHash,CreatedAt, UpdatedAt, IsActive)
            VALUES (@Username, @Email, @PasswordHash, @CreatedAt, @UpdatedAt, @IsActive)";

            var now = DateTime.UtcNow;
            var parameters = new List<SqlParameter>
            {
            new SqlParameter("@Username", request.Username),
            new SqlParameter("@Email", request.Email),
            new SqlParameter("@PasswordHash", request.Password),
            new SqlParameter("@CreatedAt", now),
            new SqlParameter("@UpdatedAt", now),
            new SqlParameter("@IsActive", true)
        };

            await _db.ExecuteNonQueryAsync(query, parameters);
        }
    }

}
