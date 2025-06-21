CREATE TABLE [dbo].[Permissions] (
    [PermissionId]   INT           NOT NULL,
    [PermissionName] VARCHAR (100) NOT NULL,
    [Description]    VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([PermissionId] ASC),
    UNIQUE NONCLUSTERED ([PermissionName] ASC)
);

