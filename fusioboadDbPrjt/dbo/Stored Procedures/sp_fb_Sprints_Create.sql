CREATE PROCEDURE [dbo].[sp_fb_Sprints_Create]
    @projectId INT = NULL,       -- Optional project
    @name NVARCHAR(100),
    @goal NVARCHAR(500) = NULL,
    @startDate DATE,
    @endDate DATE,
    @isGlobal BIT = 0,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate: if not global, projectId must be provided and not zero
    IF (@isGlobal = 0 AND (@projectId IS NULL OR @projectId = 0))
    BEGIN
        RAISERROR('ProjectId is required for non-global sprints.', 16, 1);
        RETURN;
    END

    -- Validate: no duplicate sprint name in same scope
    IF EXISTS (
        SELECT 1 FROM Sprints 
        WHERE 
            Name = @name AND 
            (
                (@isGlobal = 1 AND IsGlobal = 1 AND ProjectId IS NULL)
                OR
                (@isGlobal = 0 AND ProjectId = @projectId)
            )
            AND IsDeleted = 0
    )
    BEGIN
        RAISERROR('A sprint with the same name already exists in this scope.', 16, 1);
        RETURN;
    END

    -- Insert sprint
    INSERT INTO Sprints (
        ProjectId,
        Name,
        Goal,
        StartDate,
        EndDate,
        IsActive,
        IsGlobal,
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
        @isGlobal,
        @userId,
        GETDATE()
    );

    SELECT SCOPE_IDENTITY() AS Id;
END;
