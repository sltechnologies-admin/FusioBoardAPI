CREATE TABLE [dbo].[Logs] (
    [LogId]            INT                IDENTITY (1, 1) NOT NULL,
    [LogLevel]         VARCHAR (50)       NULL,
    [EventCode]        VARCHAR (50)       NULL,
    [CorrelationId]    VARCHAR (100)      NULL,
    [UserMessage]      NVARCHAR (MAX)     NULL,
    [TechnicalDetails] NVARCHAR (MAX)     NULL,
    [Module]           VARCHAR (100)      NULL,
    [Layer]            VARCHAR (50)       NULL,
    [Method]           VARCHAR (100)      NULL,
    [RequestedBy]      VARCHAR (100)      NULL,
    [Source]           VARCHAR (100)      NULL,
    [UserId]           INT                NULL,
    [InputParameters]  NVARCHAR (MAX)     NULL,
    [RequestUrl]       NVARCHAR (500)     NULL,
    [HttpMethod]       VARCHAR (10)       NULL,
    [ClientIP]         VARCHAR (100)      NULL,
    [CreatedAt]        DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([LogId] ASC)
);




