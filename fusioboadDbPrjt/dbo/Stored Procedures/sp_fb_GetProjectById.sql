CREATE PROCEDURE [dbo].[sp_fb_Project_GetById]
    @id INT = 0
AS
BEGIN
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
    WHERE ProjectId = @id;
END
