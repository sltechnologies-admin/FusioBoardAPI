using API.DAL;
using API.DAL.DTO;
using API.Models;

//using Microsoft.AspNetCore.Identity.Data;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly DbHelper _db;

    public AuthController(DbHelper db)
    {
        _db = db;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {

       
        // 1. Check for existing user
        var emailCheckQuery = "SELECT COUNT(1) FROM Users WHERE Email = @Email OR Username = @Username";
        var count = (int)await _db.ExecuteScalarAsync(emailCheckQuery, new List<SqlParameter>
        {
            new SqlParameter("@Email", request.Email),
            new SqlParameter("@Username", request.Username)
        });

        if (count > 0)
            return BadRequest(new { message = "Email or Username already exists." });

        // 2. Insert new user
        var insertQuery = @"
            INSERT INTO Users (
                 Username, Email, PasswordHash,
                FirstName, MiddleName, LastName,
                CreatedAt, UpdatedAt, IsActive
            )
            VALUES (
                 @Username, @Email, @PasswordHash,
                @FirstName, @MiddleName, @LastName,
                @CreatedAt, @UpdatedAt, @IsActive
            )";

        //var userId = Guid.NewGuid();

       //Random random = new Random();
       //int userId = random.Next(1, 101); // 1 to 100 inclusive
         var now = DateTime.UtcNow;

            var parameters = new List<SqlParameter>
              {
    // new SqlParameter("@UserId", userId), // Uncomment if needed
                 new SqlParameter("@Username", request.Username),
                 new SqlParameter("@Email", request.Email),
                 new SqlParameter("@PasswordHash", request.Password),
                 new SqlParameter("@FirstName", request.FirstName),
                 new SqlParameter("@MiddleName", string.IsNullOrEmpty(request.MiddleName)
                     ? DBNull.Value
                     : request.MiddleName),
                 new SqlParameter("@LastName", request.LastName),
                 new SqlParameter("@CreatedAt", now),
                 new SqlParameter("@UpdatedAt", now),
                 new SqlParameter("@IsActive", true)
                };

            await _db.ExecuteNonQueryAsync(insertQuery, parameters);

        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        return Ok(new { message = "User registered successfully."});
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

        return Ok(new LoginResponse {
            UserId = Convert.ToInt32(user["UserId"]) ,
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
