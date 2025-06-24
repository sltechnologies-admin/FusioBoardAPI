CREATE PROCEDURE [dbo].[sp_fb_UserRole_Assign]
    @UserId INT,
    @ProjectId INT,
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 
        FROM UserRoles 
        WHERE UserId = @UserId AND ProjectId = @ProjectId AND RoleId = @RoleId
    )
    BEGIN
        RAISERROR('Role already assigned to the user for this project.', 16, 1);
        RETURN;
    END

    INSERT INTO UserRoles (UserId, ProjectId, RoleId, AssignedAt)
    VALUES (@UserId, @ProjectId, @RoleId, SYSDATETIMEOFFSET());
END;