using API.Common.Extensions;
using API.Common.Models;
using API.Data.Interfaces;
using API.Features.Projects.Common;
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
        
        public async Task<Result<int>> CreateAsync(CreateProjectRequest request)
        {
            var parameters = new List<SqlParameter>
            {
        new SqlParameter("@ProjectName", request.Name),
        new SqlParameter("@Description", string.IsNullOrWhiteSpace(request.Description) ? DBNull.Value : request.Description),
        new SqlParameter("@StartDate", request.StartDate ?? (object)DBNull.Value),
        new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value),
        new SqlParameter("@CreatedBy", request.CreatedBy),

        new SqlParameter
        {
            ParameterName = "@ProjectId",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Output
        }
    };

            try
            {
                await _db.ExecuteNonQueryAsync("sp_fb_Project_Create", parameters, CommandType.StoredProcedure);

                var outputParam = parameters.First(p => p.ParameterName == "@ProjectId");
                int projectId = (outputParam.Value != DBNull.Value) ? Convert.ToInt32(outputParam.Value) : 0;

                return Result<int>.SuccessResult(projectId);
            }
            catch (SqlException ex) when (ex.Number == 50000 || ex.State == 1) // custom RAISERROR
            {
                return Result<int>.Fail("A project with this name already exists.", ex.Message);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail("An error occurred while creating the project.", ex.ToString());
            }
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

        public async Task<Result<bool>> UpdateAsync(UpdateProjectRequest request)
        {
            var parameters = new List<SqlParameter>
            {
                  new SqlParameter("@ProjectId", request.ProjectId),
                  new SqlParameter("@ProjectName", request.Name),
                  new SqlParameter("@Description", string.IsNullOrWhiteSpace(request.Description) ? DBNull.Value : request.Description),
                  new SqlParameter("@StartDate", request.StartDate ?? (object)DBNull.Value),
                  new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value),
                  new SqlParameter("@IsActive", request.IsActive ?? true),
                  new SqlParameter("@UpdatedAt", DateTimeOffset.UtcNow)
            };

            try
            {
                await _db.ExecuteNonQueryAsync("sp_fb_Project_Update", parameters, CommandType.StoredProcedure);
                return Result<bool>.SuccessResult(true);
            }
            catch (SqlException ex) when (ex.Number == 50000 || ex.State == 1)
            {
                return Result<bool>.Fail("Project not found.", ex.Message);
            }
            catch (SqlException ex) when (ex.State == 2)
            {
                return Result<bool>.Fail("A project with this name already exists.", ex.Message);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail("An error occurred while updating the project.", ex.ToString());
            }
        }

    }
}
