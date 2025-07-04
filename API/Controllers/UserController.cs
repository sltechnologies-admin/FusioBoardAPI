using API.Common.Extensions;
using API.Common.Logging;
using API.Common.Models;
using API.Constants;
using API.Controllers;
using API.DAL.DTO;
using API.Data.Interfaces;
using API.Features.Logs.Common;
using API.Models.Requests;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing;

public class UserController : BaseController
{
    private readonly IDatabaseService _db;

    private readonly IUserService _service;
    private readonly IAppLogger<UserController> _logger;
    private readonly ISqlLogger _sqlLogger;

    public UserController( IDatabaseService db, ISqlLogger sqlLogger, IAppLogger<UserController> logger, IUserService service)
    {
        //coomon 
        _db = db;
        _sqlLogger = sqlLogger;
        _logger = logger;

        //specific 
        _service = service;
    }

    /// <summary>
    /// Create or update user
    /// </summary>
    /// <param name="request">RegisterRequest</param>
    /// <returns>JWT token on success, error message on failure.</returns>

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.UpsertUserAsync(request);
            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            _logger.LogInformation("[REG-SUCCESS-01] CorrelationId: {CorrelationId} - User registered: {Email}", CorrelationId, request.Email);
            return Ok(new { message = Messages.User.s_UserRegSuccess });
        }
        catch (Exception ex)
        {
            var eventCode = "REG-ERR-01";
            await LogHelper.LogErrorAsync(_sqlLogger, eventCode,CorrelationId,Messages.User.e_UnexpectedRegistrationError,ex.Message);
            return StatusCode(HttpStatusCodes.InternalServerError, new { message = "An unexpected error occurred.", correlationId = CorrelationId });
        }
    }

    /// <summary>
    /// Get user by ID : : GOLD example
    /// </summary>

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        const string eventCode = "GET-ROLES-ERR-01-GETById";
        const string userMessage = Messages.User.e_UnexpectedErrorFetchingUserRoles;
        try
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(new { message = result.UserErrorMessage });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return await HandleFailureAsync(eventCode, userMessage, ExceptionHelper.GetDetailedError(ex), isException: true);
        }
    }

    /// <summary>
    /// Fetch all roles assigned to a user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/roles")]
    public async Task<IActionResult> GetUserRoles(int id)
    {
        try
        {
            var result = await _service.GetUserRolesAsync(id);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.UserErrorMessage });

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            const string eventCode = "GETROLES-ERR-01";
            await LogHelper.LogErrorAsync(
                _sqlLogger,
                eventCode,
                CorrelationId,
                Messages.User.e_UnexpectedErrorFetchingUserRoles,
                ex.Message);

            return StatusCode(HttpStatusCodes.InternalServerError,
                new { message = "An unexpected error occurred.", correlationId = CorrelationId });
        }
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("all-old")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllUsersAsync();

        //if (!result.Success)
        //    return BadRequest(new { message = result.ErrorMessage });

        return Ok(result);        
    }

    /// <summary>
    /// Get all users : GOLD example
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers(int page = 1, int size = 10)
    {
        const string eventCode = "GET-Users-ERR-Controller-GETById";
        string userMessage = "An unexpected error occurred while fetching users.";
        try
        {
            var result = await _service.GetAllUsersAsync(page, size);

            if (!result.IsSuccess)
            {
                return BadRequest(new {
                    message = result.UserErrorMessage,
                    correlationId = CorrelationId,
                    eventCode
                });
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return await HandleFailureAsync(eventCode, userMessage, ExceptionHelper.GetDetailedError(ex), isException: true);
        }
    }



    /// <summary>
    /// User login
    /// </summary>
    /// <param name="request">LoginRequest</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var query = @"
        SELECT UserId, Username, Email
        FROM Users
        WHERE Username = @Username AND PasswordHash = @PasswordHash AND IsActive = 1";

        var parameters = new List<SqlParameter>
        {
        new SqlParameter("@Username", request.Username),
        new SqlParameter("@PasswordHash", request.Password) // assuming plain password stored (not recommended)
    };

        var result = await _db.ExecuteQueryAsync(query, parameters);

        if (result.Rows.Count == 0)
        {
            return Unauthorized(new { message = Messages.User.i_InvalidUserNameOrPwd });
        }

        var user = result.Rows[0];

        return Ok(new LoginResponse
        {
            UserId = Convert.ToInt32(user["UserId"]),
            Username = user["Username"]?.ToString() ?? string.Empty,
            Email = user["Email"]?.ToString() ?? string.Empty,
            Message = "Login successful"
        });
    }

    /// <summary>
    /// 	Change user password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        // 1. Fetch current password from database
        var query = @"
        SELECT PasswordHash 
        FROM Users 
        WHERE Username = @Username AND IsActive = 1";

        var table = await _db.ExecuteQueryAsync(query, new List<SqlParameter>
        {
        new SqlParameter("@Username", request.Username)
    });

        if (table.Rows.Count == 0)
            return NotFound(new { message =Messages.User.i_UserNotFound });

        var currentPassword = table.Rows[0]["PasswordHash"].ToString();

        // 2. Compare plain text passwords
        if (currentPassword != request.OldPassword)
            return BadRequest(new { message = Messages.User.i_OldPwdIncorrect });

        // 3. Update password to new one
        var updateQuery = @"
        UPDATE Users
        SET PasswordHash = @NewPassword,
            UpdatedAt = @UpdatedAt
        WHERE Username = @Username";

        var rowsAffected = await _db.ExecuteNonQueryAsync(updateQuery, new List<SqlParameter>
        {
        new SqlParameter("@NewPassword", request.NewPassword),
        new SqlParameter("@UpdatedAt", DateTime.UtcNow),
        new SqlParameter("@Username", request.Username)
    });

        if (rowsAffected == 0)
            return StatusCode(HttpStatusCodes.InternalServerError, new { message = Messages.User.e_PwdUpdateFailed });

        return Ok(new { message = Messages.User.s_PasswordChanged });
    }


}
