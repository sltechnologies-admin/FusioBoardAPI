using API.Common.Extensions;
using API.Data.Interfaces;
using API.Features.Users.Common;
using API.Features.Users.Entities;
using API.Models.Requests;
using API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

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
                "sp_fb_User_Upsert",  // this is passed as 'query'
                parameters,
                CommandType.StoredProcedure
            );
        }

        public async Task<UserDto?> GetByIdAsync(int id = 0)
        {
            var parameters = new List<SqlParameter>
            {
            new SqlParameter("@id", SqlDbType.Int) { Value = id }
            };

            var res = await _db.ExecuteReaderAsync(
             "sp_fb_User_GetById",
             parameters,
            // reader =>   new UserEntity{
            reader => new UserDto {
                UserId = reader.GetInt32("UserId"),
                Username = reader.GetString("Username"),
                Email = reader.GetString("Email"),
                IsActive = reader.GetBoolean("IsActive"),
                TotalCount = reader.GetInt32("TotalCount"), // ✅ Add this only if SP returns it
                CreatedAt = reader.GetDateTimeOffset("CreatedAt").UtcDateTime,
                UpdatedAt = reader.GetDateTimeOffset("UpdatedAt").UtcDateTime 
            },
            CommandType.StoredProcedure
            );

            return res.FirstOrDefault();
        }


        public async Task<IReadOnlyList<UserDto>> GetAllAsync()
        {
            try
            {
                var res = await _db.ExecuteReaderAsync(
               "sp_fb_User_GetAll",
               new List<SqlParameter>(),
               reader => new UserDto {
                  UserId = reader.GetInt32("UserId"),
                  Username = reader.GetString("Username"),
                  Email = reader.GetString("Email"),
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

        public async Task<(List<UserEntity> list, int TotalCount)> GetAllAsync(int page, int size)
        {
            var users = new List<UserEntity>();
            int totalCount = 0;

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Page", page),
                new SqlParameter("@Size", size)
            };

            await _db.ExecuteReaderMultiAsync(
                "sp_fb_User_GetAll_Paged",
                parameters,
                async reader =>
                {
                    // First result set: Users
                    while (await reader.ReadAsync())
                    {
                        users.Add(new UserEntity {
                            UserId = reader.GetInt32("UserId"),
                            Username = reader.GetString("Username"),
                            Email = reader.GetString("Email"),
                            PasswordHash = reader.GetString("PasswordHash"),
                            IsActive = reader.GetBoolean("IsActive"),
                            CreatedAt = reader.GetDateTimeOffset("CreatedAt").UtcDateTime,
                            UpdatedAt = reader.GetDateTimeOffset("UpdatedAt").UtcDateTime
                        });
                    }

                    // Second result set: TotalCount
                    if (await reader.NextResultAsync() && await reader.ReadAsync())
                    {
                        totalCount = reader.GetInt32("TotalCount");
                    }
                },
                CommandType.StoredProcedure
            );

            return (users, totalCount);
        }

        /*
         | Feature / Layer         | **DTO** (`UserRoleDto`)                            | **Entity** (`UserRoleEntity`)                     | **ViewModel** (`UserRoleViewModel`)               |
        | ----------------------- | -------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
        | **Purpose**             | Transfer data across layers or over network        | Represent table schema, used in domain/data layer | Shape data for the frontend or UI screens         |
        | **Used In**             | Service ↔ Controller ↔ API Response ↔ Repositories | Domain Layer, Repositories, EF Core Models        | Controllers, Razor Views, React/Angular Bindings  |
        | **Bound to DB?**        | ❌ No                                               | ✅ Yes                                             | ❌ No                                              |
        | **Serialization Safe?** | ✅ Yes                                              | ⚠️ Risky (can expose internal structure)          | ✅ Yes                                             |
        | **Contains Logic?**     | ❌ Never                                            | ✅ Sometimes (business logic/behavior)             | ❌ Never                                           |
        | **Optimized for?**      | Network I/O, storage response                      | Persistence, transactions                         | UI/UX (input forms, dropdowns, etc.)              |
        | **Example Use Case**    | Return role list to frontend                       | Add/edit roles in database                        | Bind to a form that assigns a user multiple roles |

        Clarification:
                    Yes, this repository is DB-bound, because:
                    It interacts directly with SQL tables and stored procedures.
                    It uses ADO.NET and SqlDataReader to map results.
                    However, that does not mean you must use entity models (UserEntity, RoleEntity, etc.) everywhere in the repository
        🧠 Guiding Principle
                   Use Entity Models when persisting domain state; use DTOs when querying flat/read-optimized data.
          */
        public async Task<List<UserRoleDto>> GetUserRolesAsync(int id)
        {
            var parameters = new List<SqlParameter>
            {
                 new SqlParameter("@id", id)
            };

            var roles = await _db.ExecuteReaderAsync(
                "sp_fb_User_GetRoles",
                parameters,
                reader => new UserRoleDto {
                    RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                    RoleName = reader.GetString(reader.GetOrdinal("RoleName"))
                },
                CommandType.StoredProcedure);

            return roles;
        }



    }
}
 
