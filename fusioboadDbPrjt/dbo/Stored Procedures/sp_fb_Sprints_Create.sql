CREATE PROCEDURE [dbo].[sp_fb_Sprints_Create]
    @projectId INT,
    @name NVARCHAR(100),
    @goal NVARCHAR(500) = NULL,
    @startDate DATE,
    @endDate DATE,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Sprints (
        ProjectId,
        Name,
        Goal,
        StartDate,
        EndDate,
        IsActive,
        CreatedBy,
        CreatedOn
    )
    VALUES (
        @projectId,
        @name,
        @goal,
        @startDate,
        @endDate,
        1,
        @userId,
        GETDATE()
    );

    SELECT SCOPE_IDENTITY() AS Id;
END;
