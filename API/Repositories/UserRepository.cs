using Microsoft.Data.SqlClient;
using API.Data.Interfaces;
using API.Models.Requests;
using API.Repositories.Interfaces;
using System.Data;
using API.Features.Users.Entities;
using API.Common.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseService _db;

        public UserRepository(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<bool> UserExistsAsync(string email, string username)
        {
            var query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            var parameters = new List<SqlParameter>
        {
             new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username }
        };

            var result = await _db.ExecuteScalarAsync(query, parameters);
            var count = Convert.ToInt32(result);

            return count > 0;
        }

        public async Task UpsertUserAsync(RegisterRequest request)
        {
            var parameters = new List<SqlParameter>
            {
                 new SqlParameter("@Username", request.Username),
                 new SqlParameter("@Email", request.Email),
                 new SqlParameter("@PasswordHash", request.Password)
            };

            await _db.ExecuteNonQueryAsync(
                "sp_fb_UpsertUser",  // this is passed as 'query'
                parameters,
                CommandType.StoredProcedure
            );
        }

        public async Task<UserEntity?> GetByIdAsync(int id = 0)
        {
            var parameters = new List<SqlParameter>
            {
            new SqlParameter("@id", SqlDbType.Int) { Value = id }
            };


            var res = await _db.ExecuteReaderAsync(
             "sp_fb_GetUserById",
             parameters,
             reader =>   new UserEntity{
                UserId = reader.GetInt32("UserId"),
                Username = reader.GetString("Username"),
                Email = reader.GetString("Email"),
                PasswordHash = reader.GetString("PasswordHash"),
                IsActive = reader.GetBoolean("IsActive"),
               CreatedAt = reader.GetDateTimeOffset("CreatedAt").UtcDateTime,
               UpdatedAt = reader.GetDateTimeOffset("UpdatedAt").UtcDateTime
            },
            CommandType.StoredProcedure
            );

            return res.FirstOrDefault();
        }


        public async Task<IReadOnlyList<UserEntity>> GetAllAsync()
        {
            try
            {
                var res = await _db.ExecuteReaderAsync(
               "sp_fb_GetAllUsers",
               new List<SqlParameter>(),
               reader => new UserEntity {
                  UserId = reader.GetInt32("UserId"),
                  Username = reader.GetString("Username"),
                  Email = reader.GetString("Email"),
                  PasswordHash = reader.GetString("PasswordHash"),
                  IsActive = reader.GetBoolean("IsActive"),
                  CreatedAt = reader.GetDateTimeOffset("CreatedAt").UtcDateTime,
                  UpdatedAt = reader.GetDateTimeOffset("UpdatedAt").UtcDateTime
               },
              CommandType.StoredProcedure
              );

                return res.ToList();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }
    }
 }
 
