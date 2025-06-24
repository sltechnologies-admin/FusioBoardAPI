CREATE PROCEDURE [dbo].[sp_fb_User_GetPermissions]
    @UserId INT,
    @ProjectId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT 
        p.PermissionId,
        p.PermissionName,
        p.Description
    FROM UserRoles ur
    INNER JOIN RolePermission rp ON ur.RoleId = rp.RoleId
    INNER JOIN Permissions p ON rp.PermissionId = p.PermissionId
    WHERE ur.UserId = @UserId AND ur.ProjectId = @ProjectId;
END;