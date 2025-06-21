CREATE TABLE [dbo].[RolePermission] (
    [RoleId]       INT NOT NULL,
    [PermissionId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC, [PermissionId] ASC),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId])
);

