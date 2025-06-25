CREATE PROCEDURE [dbo].[sp_fb_Sprints_Update_ById]
    @id INT,
    @projectId INT,
    @name NVARCHAR(100),
    @goal NVARCHAR(500) = NULL,
    @startDate DATE,
    @endDate DATE,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Sprints
    SET 
        ProjectId = @projectId,
        Name = @name,
        Goal = @goal,
        StartDate = @startDate,
        EndDate = @endDate,
        ModifiedBy = @userId,
        ModifiedOn = GETDATE()
    WHERE Id = @id AND IsActive = 1;

    SELECT @id AS Id;
END;
