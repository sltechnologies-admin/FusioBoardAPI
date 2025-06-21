CREATE TABLE [dbo].[Projects] (
    [ProjectId]   INT                IDENTITY (1, 1) NOT NULL,
    [ProjectName] VARCHAR (100)      NOT NULL,
    [Description] VARCHAR (200)      NULL,
    [StartDate]   DATE               NULL,
    [EndDate]     DATE               NULL,
    [CreatedBy]   INT                NOT NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [IsActive]    BIT                DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([ProjectId] ASC),
    UNIQUE NONCLUSTERED ([ProjectName] ASC)
);

