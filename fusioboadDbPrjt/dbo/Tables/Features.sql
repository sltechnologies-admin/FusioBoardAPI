CREATE TABLE [dbo].[Features] (
    [FeatureId]   INT                NOT NULL,
    [EpicId]      INT                NOT NULL,
    [FeatureName] VARCHAR (150)      NOT NULL,
    [Description] VARCHAR (200)      NULL,
    [CreatedBy]   INT                NOT NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([FeatureId] ASC),
    UNIQUE NONCLUSTERED ([EpicId] ASC, [FeatureName] ASC)
);

