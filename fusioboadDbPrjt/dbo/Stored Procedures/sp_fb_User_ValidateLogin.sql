CREATE PROCEDURE [dbo].[sp_fb_User_ValidateLogin]
    @Username NVARCHAR(100),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SELECT 
        UserId, 
        Username, 
        Email,
        IsEmailVerified,
        IsActive
    FROM Users
    WHERE Username = @Username
      AND PasswordHash = @PasswordHash
      AND IsActive = 1;
END