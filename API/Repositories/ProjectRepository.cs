using API.Common.Extensions;
using API.Common.Models;
using API.Data.Interfaces;
using API.Features.Projects.Entities;
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

        public async Task<ProjectEntity?> GetByIdAsync(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id }
             };

            var results = await _db.ExecuteReaderAsync(
                "sp_fb_Project_GetById",
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
                        //StartDate = reader.IsDBNull("StartDate") ? null : DateOnly.FromDateTime(reader.GetDateTime("StartDate")),
                        //EndDate = reader.IsDBNull("EndDate") ? null : DateOnly.FromDateTime(reader.GetDateTime("EndDate")),
                        CreatedBy = reader.IsDBNull("CreatedBy") ? null : reader.GetInt32("CreatedBy").ToString(),
                        IsActive = reader.IsDBNull("IsActive") ? null : reader.GetBoolean("IsActive"),
                        CreatedAt = reader.IsDBNull("CreatedAt") ? null : reader.GetDateTimeOffset("CreatedAt").UtcDateTime,
                        UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTimeOffset("UpdatedAt").UtcDateTime
                    };
                },
                CommandType.StoredProcedure
            );

            // Return first match or null
            return results.FirstOrDefault();
        }

        public async Task<List<ProjectEntity>> GetAllAsync()
        {
            var result = await _db.ExecuteReaderAsync(
                "sp_fb_Project_GetAll",
                new List<SqlParameter>(),
                reader => new ProjectEntity {
                    ProjectId = reader.GetInt32("ProjectId"),
                    Name = reader.GetString("ProjectName"),
                    Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                    //StartDate = reader.IsDBNull("StartDate") ? null : DateOnly.FromDateTime(reader.GetDateTime("StartDate")),
                    //EndDate = reader.IsDBNull("EndDate") ? null : DateOnly.FromDateTime(reader.GetDateTime("EndDate")),
                    CreatedBy = reader.IsDBNull("CreatedBy") ? null : reader.GetInt32("CreatedBy").ToString(),
                    IsActive = reader.IsDBNull("IsActive") ? null : reader.GetBoolean("IsActive"),
                    CreatedAt = reader.IsDBNull("CreatedAt") ? null : reader.GetDateTimeOffset("CreatedAt").UtcDateTime,
                    UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTimeOffset("UpdatedAt").UtcDateTime

                },
                CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<Result<bool>> UpdateAsync(ProjectEntity entity)
        {
            const string storedProc = "sp_fb_Project_Update";

            var parameters = new List<SqlParameter>
            {
                 new("@ProjectId", entity.ProjectId),
                 new("@ProjectName", entity.Name),
                 new("@Description", (object?)entity.Description ?? DBNull.Value),
                 new("@StartDate", entity.StartDate ?? (object)DBNull.Value),
                 new("@EndDate", entity.EndDate ?? (object)DBNull.Value),
                 new("@IsActive", entity.IsActive ?? true)
            };

            try
            {
                int rowsAffected = await _db.ExecuteNonQueryAsync(storedProc, parameters, CommandType.StoredProcedure);

                if (rowsAffected == 0)
                    return Result<bool>.Fail("Project not found.");

                return Result<bool>.SuccessResult(true);
            }
            catch (SqlException ex) when (ex.Number == 50000 && ex.State == 2) // Duplicate project name
            {
                return Result<bool>.Fail("Project name already exists.");
            }
            catch (SqlException ex) when (ex.Number == 50000 && ex.State == 1) // Not found
            {
                return Result<bool>.Fail("Project not found.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail("An error occurred while updating the project.", ex.ToString());
            }
        }


        //    public async Task<Result<bool>> UpdateAsync(ProjectEntity entity)
        //    {
        //        const string query = @"
        //    UPDATE Projects
        //    SET 
        //        ProjectName = @ProjectName,
        //        Description = @Description,
        //        StartDate = @StartDate,
        //        EndDate = @EndDate,
        //        IsActive = @IsActive
        //    WHERE ProjectId = @ProjectId";

        //        var parameters = new List<SqlParameter>
        //        {
        //    new("@ProjectId", entity.ProjectId),
        //    new("@ProjectName", entity.Name),
        //    new("@Description", (object?)entity.Description ?? DBNull.Value),
        //    new SqlParameter("@StartDate", entity.StartDate ?? (object)DBNull.Value),
        //    new SqlParameter("@EndDate", entity.EndDate ??(object) DBNull.Value),
        //    new("@IsActive", entity.IsActive ?? true)
        //};

        //        try
        //        {
        //            int rowsAffected = await _db.ExecuteNonQueryAsync(query, parameters);
        //            if (rowsAffected == 0)
        //                return Result<bool>.Fail("Project not found.");

        //            return Result<bool>.SuccessResult(true);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Result<bool>.Fail("An error occurred while updating the project.", ex.ToString());
        //        }
        //    }
    }
}
