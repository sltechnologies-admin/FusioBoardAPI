CREATE PROCEDURE [dbo].[sp_fb_Sprints_Update_ById]
    @id INT,
    @projectId INT = NULL,
    @name NVARCHAR(100),
    @goal NVARCHAR(500) = NULL,
    @startDate DATE,
    @endDate DATE,
    @isGlobal BIT = 0,
    @isActive BIT = 1,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate: ProjectId is required if not global
    IF (@isGlobal = 0 AND (@projectId IS NULL OR @projectId = 0))
    BEGIN
        RAISERROR('ProjectId is required for non-global sprints.', 16, 1);
        RETURN;
    END

    -- Check for duplicate name in same project/global scope (excluding current sprint)
    IF EXISTS (
        SELECT 1 FROM Sprints
        WHERE 
            Id <> @id AND
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

    -- Update sprint
    UPDATE Sprints
    SET
        ProjectId = @projectId,
        Name = @name,
        Goal = @goal,
        StartDate = @startDate,
        EndDate = @endDate,
        IsGlobal = @isGlobal,
        IsActive = @isActive,
        ModifiedBy = @userId,
        ModifiedOn = GETDATE()
    WHERE Id = @id AND IsDeleted = 0;
END;
