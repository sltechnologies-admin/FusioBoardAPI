using API.Common.Logging;
using API.Controllers;
using API.DAL.DTO;
using API.Data.Interfaces;
using API.Models.Requests;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

public class AuthController : BaseController
{
    private readonly IDatabaseService _db;
    private readonly IAuthService _authService;
    private readonly IAppLogger<AuthController> _logger;
    private readonly ISqlLogger _sqlLogger;

    public AuthController(IAppLogger<AuthController> logger, IDatabaseService db, IAuthService authService,
ISqlLogger sqlLogger)
    {
        _db = db;
        _authService = authService;

        _logger = logger;
        _sqlLogger = sqlLogger;
    }

    /// <summary>
    /// SignUp User
    /// </summary>
    /// <param name="request">Login credentials.</param>
    /// <returns>JWT token on success, error message on failure.</returns>

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _authService.RegisterUserAsync(request);
            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            _logger.LogInformation("[REG-SUCCESS-01] CorrelationId: {CorrelationId} - User registered: {Email}", CorrelationId, request.Email);
            return Ok(new { message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            var eventCode = "REG-ERR-01";
            await LogHelper.LogErrorAsync(_sqlLogger, eventCode,CorrelationId,
                   "Unexpected error during registration.",
                   ex
               );
            return StatusCode(500, new { message = "An unexpected error occurred.", correlationId = CorrelationId });
        }
    }


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
            return Unauthorized(new { message = "Invalid username or password." });
        }

        var user = result.Rows[0];

        return Ok(new LoginResponse
        {
            UserId = Convert.ToInt32(user["UserId"]),
            Username = user["Username"].ToString(),
            Email = user["Email"].ToString(),
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
            return NotFound(new { message = "User not found." });

        var currentPassword = table.Rows[0]["PasswordHash"].ToString();

        // 2. Compare plain text passwords
        if (currentPassword != request.OldPassword)
            return BadRequest(new { message = "Old password is incorrect." });

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
            return StatusCode(500, new { message = "Password update failed." });

        return Ok(new { message = "Password changed successfully." });
    }


}
