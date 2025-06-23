using API.Common.Extensions;
using API.Data.Interfaces;
using API.Features.Projects.Entities;
using API.Features.Users.Entities;
using API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDatabaseService _db;

        public ProjectRepository(IDatabaseService db)
        {
            _db = db;
        }

        //public async Task<ProjectEntity?> GetByIdAsync(int id)
        //{
        //    try
        //    {
        //        var parameters = new List<SqlParameter>
        //        {
        //    new SqlParameter("@id", SqlDbType.Int) { Value = id }
        //};

        //        var res = await _db.ExecuteReaderAsync(
        //            "sp_fb_GetProjectById",
        //            parameters,
        //            reader => new ProjectEntity {
        //                ProjectId = reader.GetInt32(reader.GetOrdinal("Id")),
        //                Name = reader.GetSafeString("ProjectName"),
        //                Description = reader.GetSafeString("Description"),
        //                StartDate = reader.GetSafeDateOnly("StartDate") ?? default,
        //                EndDate = reader.GetSafeDateOnly("EndDate") ?? default,
        //                CreatedBy = reader.GetSafeString("CreatedBy"),
        //                CreatedAt = reader.GetSafeDateTimeOffset("CreatedAt")?.UtcDateTime ?? default,
        //                UpdatedAt = reader.GetSafeDateTimeOffset("UpdatedAt")?.UtcDateTime ?? default,
        //                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
        //            },
        //            CommandType.StoredProcedure
        //        );

        //        return res.FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<ProjectEntity?> GetByIdAsync(int projectId)
        {
            var parameters = new List<SqlParameter>
            {
        new SqlParameter("@id", SqlDbType.Int) { Value = projectId }
    };

            var results = await _db.ExecuteReaderAsync(
                "sp_fb_GetProjectById",
                parameters,
                reader =>
                {
                    // 🚧 Debug: dump column names to help catch typos or mismatches
                    reader.DumpColumns();

                    // Map only when reader has rows
                    return new ProjectEntity {
                        ProjectId = reader.GetInt32("ProjectId"),
                        Name = reader.GetString("ProjectName"),
                        Description = reader.GetString("Description"),
                        //StartDate = DateOnly.FromDateTime(reader.GetDateTime("StartDate")),
                        //EndDate = DateOnly.FromDateTime(reader.GetDateTime("EndDate")),
                        //CreatedBy = reader.GetString("CreatedBy"),
                        //CreatedAt = reader.GetDateTimeOffset("CreatedAt").UtcDateTime,
                        //UpdatedAt = reader.GetDateTimeOffset("UpdatedAt").UtcDateTime,
                        IsActive = reader.GetBoolean("IsActive")
                    };
                },
                CommandType.StoredProcedure
            );

            // Return first match or null
            return results.FirstOrDefault();
        }
        public async Task<ProjectEntity?> GetByIdAsyncOld(int id)
        {
            try
            {
            var parameters = new List<SqlParameter>
            {
            new SqlParameter("@id", SqlDbType.Int) { Value = id }
            };

            var res = await _db.ExecuteReaderAsync(
             "sp_fb_GetProjectById",
             parameters,
             reader => new ProjectEntity {
                 ProjectId = reader.GetInt32("Id"),
                 Name = reader.GetString("ProjectName"),
                 Description = reader.GetSafeString("Description"),
                 StartDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate"))),
                 EndDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate"))),
                 CreatedBy = reader.GetSafeString("CreatedBy"),
                 CreatedAt = reader.GetDateTimeOffset("CreatedAt").UtcDateTime,
                 UpdatedAt = reader.GetDateTimeOffset("UpdatedAt").UtcDateTime,
                 IsActive = reader.GetBoolean("IsActive"),
             },
            CommandType.StoredProcedure
            );

            return res.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
