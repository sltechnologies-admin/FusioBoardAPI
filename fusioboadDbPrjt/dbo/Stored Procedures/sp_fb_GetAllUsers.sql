CREATE PROCEDURE [dbo].[sp_fb_User_GetAll]
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
        ProfilePicture
    FROM Users;
END
GO
