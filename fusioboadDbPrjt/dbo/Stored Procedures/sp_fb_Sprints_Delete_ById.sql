CREATE PROCEDURE [dbo].[sp_fb_Sprints_Delete_ById]
    @id INT,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Sprints
    SET 
        IsActive = 0,
        ModifiedBy = @userId,
        ModifiedOn = GETDATE()
    WHERE Id = @id;
END;
