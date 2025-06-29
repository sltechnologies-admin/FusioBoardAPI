CREATE PROCEDURE [dbo].[sp_fb_User_GetById]
    @id INT = 0
AS
BEGIN
    SELECT 
        UserId, 
        Username, 
        Email, 
        PasswordHash, 
        IsActive, 
        CreatedAt, 
        UpdatedAt,
        IsEmailVerified,
        ProfilePicture,
        TotalCount = COUNT(*) OVER()
    FROM Users
    WHERE UserId = @id;
END