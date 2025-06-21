CREATE TABLE [dbo].[Logs] (
    [LogId]         INT                IDENTITY (1, 1) NOT NULL,
    [LogLevel]      VARCHAR (20)       NULL,
    [EventCode]     VARCHAR (50)       NULL,
    [CorrelationId] VARCHAR (100)      NULL,
    [Message]       NVARCHAR (MAX)     NULL,
    [Exception]     NVARCHAR (MAX)     NULL,
    [CreatedAt]     DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([LogId] ASC)
);

