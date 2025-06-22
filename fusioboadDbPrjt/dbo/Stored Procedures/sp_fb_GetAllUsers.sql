CREATE PROCEDURE dbo.sp_fb_GetAllUsers
AS
BEGIN
    SELECT UserId, Username, Email, PasswordHash, IsActive, CreatedAt, UpdatedAt
    FROM Users;
END