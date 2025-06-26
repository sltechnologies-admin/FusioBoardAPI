CREATE PROCEDURE [dbo].[sp_fb_GetLogs]
    @PageNumber INT = 1,
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        LogId,
        CorrelationId,
        ErrorCode,
        Message,
        StackTrace,
        Source,
        CreatedOn
    FROM Logs
    ORDER BY CreatedOn DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
