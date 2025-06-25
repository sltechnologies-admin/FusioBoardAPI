CREATE PROCEDURE [dbo].[sp_fb_Sprints_GetAll_ByProjectId]
    @projectId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        ProjectId,
        Name,
        Goal,
        StartDate,
        EndDate,
        IsActive,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn
    FROM Sprints
    WHERE ProjectId = @projectId AND IsActive = 1
    ORDER BY StartDate DESC;
END;
