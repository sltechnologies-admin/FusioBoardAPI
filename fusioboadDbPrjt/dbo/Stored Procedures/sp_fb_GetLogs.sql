CREATE PROCEDURE [dbo].[sp_fb_GetLogs]
    @PageNumber INT = 1,
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        LogId,
        LogLevel,
        EventCode,
        CorrelationId,
        UserMessage,
        TechnicalDetails,
        Module,
        Layer,
        Method,
        RequestedBy,
        Source,
        UserId,
        InputParameters,
        RequestUrl,
        HttpMethod,
        ClientIP,
        CreatedAt
    FROM Logs
    ORDER BY CreatedAt DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
