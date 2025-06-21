CREATE TABLE [dbo].[Epics] (
    [EpicId]      INT                NOT NULL,
    [ProjectId]   INT                NOT NULL,
    [EpicName]    VARCHAR (150)      NOT NULL,
    [Description] VARCHAR (200)      NULL,
    [CreatedBy]   INT                NOT NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([EpicId] ASC),
    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([ProjectId] ASC, [EpicName] ASC)
);

