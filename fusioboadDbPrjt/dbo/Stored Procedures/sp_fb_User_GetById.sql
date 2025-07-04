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
        (SELECT COUNT(*) FROM Users WHERE UserId = @id) AS TotalCount
    FROM Users
    WHERE UserId = @id;
END