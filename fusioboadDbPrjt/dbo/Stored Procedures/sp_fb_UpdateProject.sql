
/*
Error Codes (for app-side differentiation, if needed):

    State = 1 → Not Found

    State = 2 → Duplicate Project Name
*/

CREATE PROCEDURE [dbo].[sp_fb_UpdateProject]
    @ProjectId INT,
    @ProjectName NVARCHAR(100),
    @Description NVARCHAR(200) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if project exists
    IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectId = @ProjectId)
    BEGIN
        RAISERROR ('Project not found.', 16, 1);
        RETURN;
    END

    -- Check if the new project name is already used by another project
    IF EXISTS (
        SELECT 1 FROM Projects 
        WHERE ProjectName = @ProjectName AND ProjectId != @ProjectId
    )
    BEGIN
        RAISERROR ('Project name already exists.', 16, 2);
        RETURN;
    END

    -- Perform the update
    UPDATE Projects
    SET
        ProjectName = @ProjectName,
        Description = @Description,
        StartDate = @StartDate,
        EndDate = @EndDate,
        IsActive = @IsActive,
        UpdatedAt = SYSDATETIMEOFFSET()
    WHERE ProjectId = @ProjectId;
END;
GO

