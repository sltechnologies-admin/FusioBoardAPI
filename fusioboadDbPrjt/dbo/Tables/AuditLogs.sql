CREATE TABLE [dbo].[AuditLogs] (
    [AuditLogId]  INT                NOT NULL,
    [EntityName]  VARCHAR (50)       NOT NULL,
    [EntityId]    INT                NOT NULL,
    [Action]      VARCHAR (50)       NOT NULL,
    [PerformedBy] INT                NULL,
    [PerformedAt] DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [Details]     NVARCHAR (MAX)     NULL,
    PRIMARY KEY CLUSTERED ([AuditLogId] ASC)
);

