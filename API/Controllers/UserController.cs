using API.Common.Logging;
using API.Constants;
using API.Controllers;
using API.DAL.DTO;
using API.Data.Interfaces;
using API.Models.Requests;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

public class UserController : BaseController
{
    private readonly IDatabaseService _db;

    private readonly IUserService _userService;
    private readonly IAppLogger<UserController> _logger;
    private readonly ISqlLogger _sqlLogger;

    public UserController( IDatabaseService db, ISqlLogger sqlLogger, IAppLogger<UserController> logger, IUserService authService)
    {
        //coomon 
        _db = db;
        _sqlLogger = sqlLogger;
        _logger = logger;

        //specific 
        _userService = authService;
    }

    /// <summary>
    /// Sign Up: Create new user and update user details
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
            var result = await _userService.UpsertUserAsync(request);
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
    /// Get User Details by id 
    /// </summary>

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return user != null ? Ok(user) : NotFound();
    }

    /// <summary>
    /// Get All User Details 
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            //_logger.LogInformation( "[GETALL-SUCCESS-01] CorrelationId: {CorrelationId} - Retrieved {UserCount} users",
            //    CorrelationId,
            //    users.Count);

            return Ok(users);
        }
        catch (Exception ex)
        {
            const string eventCode = "GETALL-ERR-01";
            await LogHelper.LogErrorAsync(
                _sqlLogger,
                eventCode,
                CorrelationId,
                "An unexpected error occurred while fetching all users.",
                ex.Message);

            return StatusCode(
                HttpStatusCodes.InternalServerError,
                new { message = "An unexpected error occurred.", correlationId = CorrelationId });
        }
    }

    /// <summary>
    /// Log in
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

        return Ok(new { message = Messages.User.s_PwdUpdateSuccess });
    }


}
