CREATE TABLE [dbo].[RolePermission] (
    [RoleId]       INT NOT NULL,
    [PermissionId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC, [PermissionId] ASC),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
    CONSTRAINT [FK_RolePermission_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ([PermissionId]),
    CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId])
);



