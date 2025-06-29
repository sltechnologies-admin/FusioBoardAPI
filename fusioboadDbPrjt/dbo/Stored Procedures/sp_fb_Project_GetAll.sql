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
        IsActive,
        TotalCount = COUNT(*) OVER()
    FROM Projects
    ORDER BY CreatedAt DESC;
END;