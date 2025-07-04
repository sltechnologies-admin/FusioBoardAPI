CREATE PROCEDURE [dbo].[sp_fb_User_GetAll_Paged]
    @Page INT,
    @Size INT
AS
BEGIN
    SET NOCOUNT ON;

    -- First result: Total count
    SELECT COUNT(*) AS TotalCount FROM Users WHERE IsActive = 1;

    -- Second result: Paged data
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
    FROM Users
    WHERE IsActive = 1
    ORDER BY CreatedAt DESC
    OFFSET (@Page - 1) * @Size ROWS
    FETCH NEXT @Size ROWS ONLY;
END