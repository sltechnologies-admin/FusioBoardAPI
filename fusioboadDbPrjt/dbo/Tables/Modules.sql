CREATE TABLE [dbo].[Modules] (
    [ModuleId]    INT                NOT NULL,
    [ProjectId]   INT                NOT NULL,
    [ModuleName]  VARCHAR (100)      NOT NULL,
    [Description] VARCHAR (200)      NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([ModuleId] ASC),
    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([ProjectId] ASC, [ModuleName] ASC)
);

