CREATE TABLE [dbo].[UserProjectRoles] (
    [UserProjectRoleId] INT                IDENTITY (1, 1) NOT NULL,
    [UserId]            INT                NOT NULL,
    [ProjectId]         INT                NOT NULL,
    [RoleId]            INT                NOT NULL,
    [AssignedAt]        DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    PRIMARY KEY CLUSTERED ([UserProjectRoleId] ASC),
    UNIQUE NONCLUSTERED ([UserId] ASC, [ProjectId] ASC, [RoleId] ASC)
);

