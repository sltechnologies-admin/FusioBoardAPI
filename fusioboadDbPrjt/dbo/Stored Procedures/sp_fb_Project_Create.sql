CREATE PROCEDURE [dbo].[sp_fb_Project_Create]
    @ProjectName NVARCHAR(100),
    @Description NVARCHAR(200) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @CreatedBy INT,
    @ProjectId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if a project with the same name already exists
    IF EXISTS (SELECT 1 FROM Projects WHERE ProjectName = @ProjectName)
    BEGIN
        -- Duplicate name - use state 1 to represent unique constraint violation
        RAISERROR('A project with this name already exists.', 16, 1);
        RETURN;
    END

    -- Insert the new project
    INSERT INTO Projects (
        ProjectName,
        Description,
        StartDate,
        EndDate,
        CreatedBy,
        CreatedAt,
        UpdatedAt,
        IsActive
    )
    VALUES (
        @ProjectName,
        @Description,
        @StartDate,
        @EndDate,
        @CreatedBy,
        SYSDATETIMEOFFSET(),
        SYSDATETIMEOFFSET(),
        1
    );

    -- Return the inserted ProjectId
    SET @ProjectId = SCOPE_IDENTITY();
END;
GO
