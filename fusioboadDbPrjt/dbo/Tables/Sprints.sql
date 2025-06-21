CREATE TABLE [dbo].[Sprints] (
    [SprintId]   INT                NOT NULL,
    [ProjectId]  INT                NOT NULL,
    [SprintName] VARCHAR (100)      NOT NULL,
    [StartDate]  DATE               NOT NULL,
    [EndDate]    DATE               NOT NULL,
    [CreatedAt]  DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [UpdatedAt]  DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([SprintId] ASC),
    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([ProjectId] ASC, [SprintName] ASC)
);

