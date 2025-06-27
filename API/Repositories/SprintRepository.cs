using API.Common.Extensions;
using API.Common.Models;
using API.Data.Interfaces;
using API.Features.Sprints.Common;
using API.Features.Sprints.Entities;
using API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private readonly IDatabaseService _db;

        public SprintRepository(IDatabaseService db)
        {
            _db = db;
        }
        public async Task<Result<int>> CreateAsync(SprintCreateDto dto, int userId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
            new SqlParameter("@projectId", SqlDbType.Int) { Value = dto.ProjectId },
            new SqlParameter("@name", SqlDbType.NVarChar, 100) { Value = dto.Name },
            new SqlParameter("@goal", SqlDbType.NVarChar, 500) { Value = string.IsNullOrWhiteSpace(dto.Goal) ? DBNull.Value : dto.Goal },
            new SqlParameter("@startDate", SqlDbType.Date) { Value = dto.StartDate },
            new SqlParameter("@endDate", SqlDbType.Date) { Value = dto.EndDate },
            new SqlParameter("@userId", SqlDbType.Int) { Value = userId }
        };

                var result = await _db.ExecuteScalarAsync("sp_fb_Sprints_Create", parameters, CommandType.StoredProcedure);
                int sprintId = Convert.ToInt32(result);

                return Result<int>.SuccessResult(sprintId);
            }
            catch (SqlException ex) when (ex.Number == 50000 || ex.State == 1)
            {
                var userMessage = $"A sprint with the name '{dto.Name}' already exists in this project.";
                return Result<int>.Fail(userMessage, "e_sprint_name_conflict" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail("e_sprint_create_failed", ex.Message);
            }
        }



        public async Task<Result<int>> CreateAsync_old2(SprintCreateDto dto, int userId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
            new SqlParameter("@projectId", SqlDbType.Int) { Value = dto.ProjectId },
            new SqlParameter("@name", SqlDbType.NVarChar, 100) { Value = dto.Name },
            new SqlParameter("@goal", SqlDbType.NVarChar, 500) { Value = string.IsNullOrWhiteSpace(dto.Goal) ? DBNull.Value : dto.Goal },
            new SqlParameter("@startDate", SqlDbType.Date) { Value = dto.StartDate },
            new SqlParameter("@endDate", SqlDbType.Date) { Value = dto.EndDate },
            new SqlParameter("@userId", SqlDbType.Int) { Value = userId }
            };

                var result = await _db.ExecuteScalarAsync("sp_fb_Sprints_Create", parameters, CommandType.StoredProcedure);
                int sprintId = Convert.ToInt32(result);

                return Result<int>.SuccessResult(sprintId);
            }
            catch (SqlException ex) when (ex.Number == 50000 || ex.State == 1)
            {
                return Result<int>.Fail("SqlException: e_sprint_create_conflict ", ex.Message);
            }

            catch (Exception ex)
            {
                // Optional: Add logging
                // await LogHelper.LogErrorAsync(ex, EventCodes.Sprint_Create, Messages.e_Sprint_Create, Modules.Sprint, Layers.Repository, nameof(CreateAsync), userId.ToString(), dto);
                return Result<int>.Fail("e_sprint_create_failed", ex.Message);
            }
        }


        public async Task<Result<int>> CreateAsync_old(SprintCreateDto dto, int userId)
        {
            var parameters = new List<SqlParameter>
            {
                 new SqlParameter("@projectId", dto.ProjectId),
                 new SqlParameter("@name", dto.Name),
                 new SqlParameter("@goal", string.IsNullOrWhiteSpace(dto.Goal) ? DBNull.Value : dto.Goal),
                 new SqlParameter("@startDate", dto.StartDate),
                 new SqlParameter("@endDate", dto.EndDate),
                 new SqlParameter("@userId", userId),
                //new SqlParameter
                //{
                //    ParameterName = "@Id",
                //    SqlDbType = SqlDbType.Int,
                //    Direction = ParameterDirection.Output
                //}
            };

            try
            {
                await _db.ExecuteNonQueryAsync("sp_fb_Sprints_Create", parameters, CommandType.StoredProcedure);

                //int sprintId = Convert.ToInt32(parameters.First(p => p.ParameterName == "@Id").Value);
                //return Result<int>.SuccessResult(sprintId);

                var result = await _db.ExecuteScalarAsync("sp_fb_Sprints_Create", parameters, CommandType.StoredProcedure);
                return Result<int>.SuccessResult(Convert.ToInt32(result));
            }
            catch (SqlException ex) when (ex.Number == 50000 || ex.State == 1)
            {
                return Result<int>.Fail("e_sprint_create_conflict", ex.Message);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail("e_sprint_create_failed", ex.ToString());
            }
        }
        public async Task<Result<List<SprintEntity>>> GetAllByProjectIdAsync(int projectId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                new SqlParameter("@projectId", projectId)
            };

                var result = await _db.ExecuteReaderAsync(
                    "sp_fb_Sprints_GetAll_ByProjectId",
                    parameters,
                    reader => new SprintEntity {
                        Id = reader.GetInt32("Id"),
                        ProjectId = reader.GetInt32("ProjectId"),
                        Name = reader.GetString("Name"),
                        Goal = reader.GetNullableString("Goal"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
                        IsActive = reader.GetBoolean("IsActive"),
                        CreatedBy = reader.GetNullableInt("CreatedBy"),
                        CreatedOn = reader.GetDateTime("CreatedOn"),
                        ModifiedBy = reader.GetNullableInt("ModifiedBy"),
                        ModifiedOn = reader.GetNullableDateTime("ModifiedOn")
                    },
                    CommandType.StoredProcedure
                );

                return Result<List<SprintEntity>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return Result<List<SprintEntity>>.Fail("e_sprint_fetch_all", $"Failed to fetch sprints for project {projectId}. {ex.Message}");
            }
        }

        public async Task<Result<SprintEntity?>> GetByIdAsync(int id)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                new SqlParameter("@id", id)
            };

                var result = await _db.ExecuteReaderAsync(
                    "sp_fb_Sprints_Get_ById",
                    parameters,
                    reader => new SprintEntity {
                        Id = reader.GetInt32("Id"),
                        ProjectId = reader.GetInt32("ProjectId"),
                        Name = reader.GetString("Name"),
                        Goal = reader.GetNullableString("Goal"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
                        IsActive = reader.GetBoolean("IsActive"),
                        CreatedBy = reader.GetNullableInt("CreatedBy"),
                        CreatedOn = reader.GetDateTime("CreatedOn"),
                        ModifiedBy = reader.GetNullableInt("ModifiedBy"),
                        ModifiedOn = reader.GetNullableDateTime("ModifiedOn")
                    },
                    CommandType.StoredProcedure
                );

                return Result<SprintEntity?>.SuccessResult(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Result<SprintEntity?>.Fail("e_sprint_get_by_id", $"Failed to fetch sprint {id}. {ex.Message}");
            }
        }

        public async Task<Result<int>> UpdateAsync(SprintUpdateDto dto, int userId)
        {
            var parameters = new List<SqlParameter>
            {
            new SqlParameter("@id", dto.Id),
            new SqlParameter("@projectId", dto.ProjectId),
            new SqlParameter("@name", dto.Name),
            new SqlParameter("@goal", string.IsNullOrWhiteSpace(dto.Goal) ? DBNull.Value : dto.Goal),
            new SqlParameter("@startDate", dto.StartDate),
            new SqlParameter("@endDate", dto.EndDate),
            new SqlParameter("@userId", userId)
        };

            try
            {
                await _db.ExecuteNonQueryAsync("sp_fb_Sprints_Update_ById", parameters, CommandType.StoredProcedure);
                return Result<int>.SuccessResult(dto.Id);
            }
            catch (SqlException ex) when (ex.Number == 50000 || ex.State == 1)
            {
                return Result<int>.Fail("e_sprint_update_conflict", ex.Message);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail("e_sprint_update_failed", ex.ToString());
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id, int userId)
        {
            var parameters = new List<SqlParameter>
            {
            new SqlParameter("@id", id),
            new SqlParameter("@userId", userId)
        };

            try
            {
                await _db.ExecuteNonQueryAsync("sp_fb_Sprints_Delete_ById", parameters, CommandType.StoredProcedure);
                return Result<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail("e_sprint_delete_failed", $"Failed to delete sprint {id}. {ex.Message}");
            }
        }
    }
}
