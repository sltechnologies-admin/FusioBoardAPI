CREATE PROCEDURE [dbo].[sp_fb_Sprints_Get_ById]
    @id INT
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
    WHERE Id = @id AND IsActive = 1;
END;
