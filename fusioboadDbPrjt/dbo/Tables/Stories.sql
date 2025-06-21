CREATE TABLE [dbo].[Stories] (
    [StoryId]     INT                NOT NULL,
    [FeatureId]   INT                NOT NULL,
    [StoryName]   VARCHAR (150)      NOT NULL,
    [Description] VARCHAR (200)      NULL,
    [CreatedBy]   INT                NOT NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([StoryId] ASC),
    UNIQUE NONCLUSTERED ([FeatureId] ASC, [StoryName] ASC)
);

