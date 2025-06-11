using API.DAL;
using API.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class ProjectsController : ControllerBase
    {
        private readonly DbHelper _db;

        public ProjectsController(DbHelper db)
        {
            _db = db;
        }


 
        [HttpPost("api/projects")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ProjectName))
                return BadRequest("Project name is required.");

            if (request.CreatedBy <= 0)
                return BadRequest("Valid CreatedBy user ID is required.");

            const string insertQuery = @"
        INSERT INTO Projects 
        (ProjectName, Description, StartDate, EndDate, CreatedBy, CreatedAt, UpdatedAt, IsActive)
        OUTPUT INSERTED.ProjectId
        VALUES 
        (@ProjectName, @Description, @StartDate, @EndDate, @CreatedBy, SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 1);";

            var parameters = new List<SqlParameter>
            {
        new SqlParameter("@ProjectName", request.ProjectName),
        new SqlParameter("@Description", string.IsNullOrEmpty(request.Description) ? DBNull.Value : request.Description),
        new SqlParameter("@StartDate", request.StartDate ?? (object)DBNull.Value),
        new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value),
        new SqlParameter("@CreatedBy", request.CreatedBy)
    };

            try
            {
                var projectId = (int)await _db.ExecuteScalarAsync(insertQuery, parameters);

                return Ok(new {
                    message = "Project created successfully.",
                    projectId
                });
            }
            catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation
            {
                return Conflict(new { error = "A project with this name already exists." });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Database error", detail = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Unexpected error", detail = ex.Message });
            }
        }



        /// <summary>
        /// Assign/Edit  role to a user in a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("api/projects/{projectId}/users/assign-role")]
        public async Task<IActionResult> AssignRoleToUser(int projectId, [FromBody] AssignRoleRequest request)
        {
            if (request == null || request.UserId <= 0 || request.RoleId <= 0)
                return BadRequest("Invalid user or role information.");

            try
            {
                // Check if the role is already assigned
                string checkQuery = @"
            SELECT COUNT(*) 
            FROM UserProjectRoles 
            WHERE ProjectId = @ProjectId AND UserId = @UserId AND RoleId = @RoleId";

                int checkCount = (int)await _db.ExecuteScalarAsync(checkQuery, new List<SqlParameter>
                {
            new SqlParameter("@ProjectId", projectId),
            new SqlParameter("@UserId", request.UserId),
            new SqlParameter("@RoleId", request.RoleId)
        });

                if (checkCount > 0)
                {
                    // If role already assigned, update AssignedAt timestamp
                    string updateQuery = @"
                UPDATE UserProjectRoles
                SET RoleId = @RoleId, AssignedAt = @AssignedAt
                WHERE ProjectId = @ProjectId AND UserId = @UserId";

                    await _db.ExecuteNonQueryAsync(updateQuery, new List<SqlParameter>
                    {
                new SqlParameter("@RoleId", request.RoleId),
                new SqlParameter("@AssignedAt", DateTime.UtcNow),
                new SqlParameter("@ProjectId", projectId),
                new SqlParameter("@UserId", request.UserId)
            });

                    return Ok(new { message = "User role updated in project." });
                }

                // Insert new role assignment
                string insertQuery = @"
            INSERT INTO UserProjectRoles (UserId, ProjectId, RoleId, AssignedAt)
            VALUES (@UserId, @ProjectId, @RoleId, @AssignedAt)";

                await _db.ExecuteNonQueryAsync(insertQuery, new List<SqlParameter>
                {
            new SqlParameter("@UserId", request.UserId),
            new SqlParameter("@ProjectId", projectId),
            new SqlParameter("@RoleId", request.RoleId),
            new SqlParameter("@AssignedAt", DateTime.UtcNow)
        });

                return Ok(new { message = "User role assigned successfully." });
            }
            catch (SqlException ex)
            {
                // Log if needed
                return StatusCode(500, new { error = "Database error", detail = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Unexpected error", detail = ex.Message });
            }
        }


        [HttpGet("api/projects/{projectId}/users/roles")]
        public async Task<IActionResult> GetAssignedUsersAndRoles(int projectId)
        {
            try
            {
                string query = @"
            SELECT upr.UserId, u.UserName, upr.RoleId, r.RoleName, upr.AssignedAt
            FROM UserProjectRoles upr
            INNER JOIN Users u ON upr.UserId = u.UserId
            INNER JOIN Roles r ON upr.RoleId = r.RoleId
            WHERE upr.ProjectId = @ProjectId
            ORDER BY upr.AssignedAt DESC";

                var parameters = new List<SqlParameter>
                {
            new SqlParameter("@ProjectId", projectId)
        };

                var result = await _db.ExecuteReaderAsync(query, parameters, reader => new
                {
                    UserId = reader.GetInt32(0),
                    UserName = reader.GetString(1),
                    RoleId = reader.GetInt32(2),
                    RoleName = reader.GetString(3),
                    AssignedAt = reader.GetDateTimeOffset(4)
                });

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Database error", detail = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Unexpected error", detail = ex.Message });
            }
        }


    }
}
