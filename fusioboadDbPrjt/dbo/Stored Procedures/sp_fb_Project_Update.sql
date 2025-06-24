
/*
Error Codes (for app-side differentiation, if needed):

    State = 1 → Not Found

    State = 2 → Duplicate Project Name
*/
CREATE PROCEDURE [dbo].[sp_fb_Project_Update]
    @ProjectId INT,
    @ProjectName NVARCHAR(100),
    @Description NVARCHAR(200) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @IsActive BIT = 1,
    @UpdatedAt DATETIMEOFFSET = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectId = @ProjectId)
    BEGIN
        RAISERROR('Project not found.', 16, 1);
        RETURN;
    END

    -- Optional: enforce name uniqueness (excluding current project)
    IF EXISTS (
        SELECT 1 FROM Projects 
        WHERE ProjectName = @ProjectName AND ProjectId != @ProjectId
    )
    BEGIN
        RAISERROR('A project with this name already exists.', 16, 2);
        RETURN;
    END

    UPDATE Projects
    SET 
        ProjectName = @ProjectName,
        Description = @Description,
        StartDate = @StartDate,
        EndDate = @EndDate,
        IsActive = @IsActive,
        UpdatedAt = ISNULL(@UpdatedAt, SYSDATETIMEOFFSET())
    WHERE ProjectId = @ProjectId;
END;
GO

