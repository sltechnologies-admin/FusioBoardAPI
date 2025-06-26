CREATE PROCEDURE [dbo].[sp_fb_InsertLog]
    @LogLevel VARCHAR(20),
    @EventCode VARCHAR(50),
    @CorrelationId VARCHAR(100),
    @UserMessage NVARCHAR(MAX),
    @TechnicalDetails NVARCHAR(MAX),
    @Module VARCHAR(100),
    @Layer VARCHAR(50),
    @Method VARCHAR(100),
    @RequestedBy VARCHAR(100),
    @Source VARCHAR(100),
    @UserId INT = NULL,
    @InputParameters NVARCHAR(MAX) = NULL,
    @RequestUrl NVARCHAR(500) = NULL,
    @HttpMethod VARCHAR(10) = NULL,
    @ClientIP VARCHAR(100) = NULL
AS
BEGIN
    INSERT INTO Logs (
        LogLevel, EventCode, CorrelationId,
        UserMessage, TechnicalDetails,
        Module, Layer, Method,
        RequestedBy, Source, UserId,
        InputParameters, RequestUrl, HttpMethod, ClientIP
    )
    VALUES (
        @LogLevel, @EventCode, @CorrelationId,
        @UserMessage, @TechnicalDetails,
        @Module, @Layer, @Method,
        @RequestedBy, @Source, @UserId,
        @InputParameters, @RequestUrl, @HttpMethod, @ClientIP
    );
END;
