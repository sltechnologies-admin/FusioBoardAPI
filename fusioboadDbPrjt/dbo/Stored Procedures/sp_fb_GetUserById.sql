CREATE PROCEDURE dbo.sp_fb_GetUserById
    @id INT = 0
AS
BEGIN
    SELECT UserId, Username, Email, PasswordHash, IsActive, CreatedAt, UpdatedAt
    FROM Users
    WHERE UserId = @id;
END