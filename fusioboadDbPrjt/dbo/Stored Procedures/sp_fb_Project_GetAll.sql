 CREATE PROCEDURE [dbo].[sp_fb_Project_GetAll]
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