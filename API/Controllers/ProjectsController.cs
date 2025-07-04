using API.Common.Logging;
using API.Constants;
using API.DAL.DTO;
using API.Data.Interfaces;
using API.Features.Projects.Common;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IDatabaseService _db;

        private readonly IProjectService _service;
        private readonly IAppLogger<ProjectsController> _logger;
        private readonly ISqlLogger _sqlLogger;

        public ProjectsController(IDatabaseService db, ISqlLogger sqlLogger, IAppLogger<ProjectsController> logger, IProjectService service)
        {
            //coomon 
            _db = db;
            _sqlLogger = sqlLogger;
            _logger = logger;

            //specific 
            _service = service;
        }

        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            const string eventCode = "PRJ-CRT-01";
            const string userMessage =   Messages.Project.e_ProjectCreationFailed;

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new { message = "Project name is required." });

            if (request.CreatedBy <= 0)
                return BadRequest(new { message = "Valid CreatedBy user ID is required." });

            try
            {
                var result = await _service.CreateAsync(request);

                if (!result.IsSuccess)
                {
                    await LogHelper.LogErrorAsync(_sqlLogger, eventCode, CorrelationId, result.UserErrorMessage ?? userMessage, result.TechnicalErrorDetails);
                    return BadRequest(new { message = result.UserErrorMessage });
                }

                _logger.LogInformation("[{EventCode}] CorrelationId: {CorrelationId} - Project created: {ProjectName}",
                    eventCode, CorrelationId, request.Name);

                return Ok(new { message = Messages.Project.s_ProjectCreated, projectId = result.Data });
            }
            catch (Exception ex)
            {
                await LogHelper.LogErrorAsync(_sqlLogger, eventCode, CorrelationId, userMessage, ex.ToString());
                return StatusCode(HttpStatusCodes.InternalServerError, new { message = userMessage, correlationId = CorrelationId });
            }
        }

        /// <summary>
        /// Update an existing project
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
        {
            const string eventCode = "PRJ-ERR-04";// EventCodes.Project.UpdateError;
            const string userMessage = Messages.Project.e_ProjectUpdateFailed;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _service.UpdateAsync(request);

                if (!result.IsSuccess)
                {
                    return await HandleFailureAsync(
                        eventCode,
                        result.UserErrorMessage,
                        result.TechnicalErrorDetails
                    );
                }
                return Ok(new { message = Messages.Project.s_ProjectUpdatedSuccessfully });
            }
            catch (Exception ex)
            {
                return await HandleFailureAsync(
                    eventCode,
                    userMessage,
                    ex.ToString()
                );
            }
        }

        /// <summary>
        /// Assign/edit role for user in project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/users/assign-role")]
        public async Task<IActionResult> AssignRoleToUserForGivenProject(int projectId, [FromBody] AssignRoleRequest request)
        {
            if (request == null || request.UserId <= 0 || request.RoleId <= 0)
                return BadRequest("Invalid user or role information.");

            try
            {
                // Check if the role is already assigned
                string checkQuery = @"
            SELECT COUNT(*) 
            FROM UserRoles 
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
                UPDATE UserRoles
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
            INSERT INTO UserRoles (UserId, ProjectId, RoleId, AssignedAt)
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
                return StatusCode(HttpStatusCodes.InternalServerError, new { error = "Database error", detail = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCodes.InternalServerError, new { error = "Unexpected error", detail = ex.Message });
            }
        }

        /// <summary>
        /// View roles in project (project-user-role view)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>

        [HttpGet("{projectId}/users/roles")]
        public async Task<IActionResult> GetAssignedUsersAndRoles(int projectId)
        {
            try
            {
                string query = @"
            SELECT upr.UserId, u.UserName, upr.RoleId, r.RoleName, upr.AssignedAt
            FROM UserRoles upr
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
                return StatusCode(HttpStatusCodes.InternalServerError, new { error = "Database error", detail = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCodes.InternalServerError, new { error = "Unexpected error", detail = ex.Message });
            }
        }

        /// <summary>
        /// Get project by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (!result.IsSuccess)
                    return NotFound(new { message = result.UserErrorMessage });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                const string eventCode = "GETPROJ-ERR-02";

                await LogHelper.LogErrorAsync(
                    _sqlLogger,
                    eventCode,
                    CorrelationId,
                    Messages.Project.e_UnexpectedErrorFetchingProjectById,
                    ex.Message);

                return StatusCode(HttpStatusCodes.InternalServerError,
                    new { message = "An unexpected error occurred.", correlationId = CorrelationId });
            }
        }

        /// <summary>
        /// Fetch all projects
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProjects()
        {
            const string eventCode = EventCodes.Project.FetchAllError;
            const string userMessage = Messages.Project.e_UnexpectedErrorFetchingProjects;

            try
            {
                var result = await _service.GetAllAsync();

                if (!result.IsSuccess)
                {
                    return await HandleFailureAsync(
                        eventCode,
                        result.UserErrorMessage,               // user-facing message
                        result.TechnicalErrorDetails        // technical details
                    );
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return await HandleFailureAsync(
                    eventCode,
                    userMessage,
                    ex.ToString(),                // include stack trace in log
                    logToConsole: true            // log optionally to console if needed
                );
            }
        }
    }
}
