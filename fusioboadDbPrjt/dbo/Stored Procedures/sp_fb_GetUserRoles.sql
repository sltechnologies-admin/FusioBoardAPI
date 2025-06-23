CREATE PROCEDURE [dbo].[sp_fb_GetUserRoles]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ur.UserProjectRoleId,
        ur.UserId,
        ur.ProjectId,
        ur.RoleId,
        r.RoleName,
        r.Description AS RoleDescription,
        ur.AssignedAt
    FROM UserRoles ur
    INNER JOIN Roles r ON ur.RoleId = r.RoleId
    WHERE ur.UserId = @UserId;
END;