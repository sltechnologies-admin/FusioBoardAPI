 CREATE PROCEDURE [dbo].[sp_fb_GetAllProjects]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ProjectId,
        ProjectName,
        Description,
        StartDate,
        EndDate,
        CreatedBy,
        CreatedAt,
        UpdatedAt,
        IsActive
    FROM Projects
    ORDER BY CreatedAt DESC;
END;