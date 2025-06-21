CREATE TABLE [dbo].[Releases] (
    [ReleaseId]   INT                NOT NULL,
    [ProjectId]   INT                NOT NULL,
    [ReleaseName] VARCHAR (100)      NOT NULL,
    [ReleaseDate] DATE               NULL,
    [Description] VARCHAR (200)      NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([ReleaseId] ASC),
    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([ProjectId] ASC, [ReleaseName] ASC)
);

