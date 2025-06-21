CREATE TABLE [dbo].[Roles] (
    [RoleId]      INT                NOT NULL,
    [RoleName]    VARCHAR (50)       NOT NULL,
    [Description] VARCHAR (200)      NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);

