CREATE TABLE Users (
    UserId int IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    FirstName VARCHAR(50)  NULL,
    MiddleName VARCHAR(50) NULL,
    LastName VARCHAR(50)  NULL,
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    IsActive BIT DEFAULT 1
);


-- 2. Roles
CREATE TABLE Roles (
    RoleId int PRIMARY KEY DEFAULT NEWID(),
    RoleName VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(200),
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET()
);

-- 3. Permissions
CREATE TABLE Permissions (
    PermissionId int PRIMARY KEY ,
    PermissionName VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(200)
);


-- 4. RolePermissions (many-to-many)
CREATE TABLE RolePermission (
    RoleId Uint,
    PermissionId Uint,
    PRIMARY KEY (RoleId, PermissionId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId),
    --FOREIGN KEY (PermissionId) REFERENCES Permissions(PermissionId)
);

-- 5. Projects
CREATE TABLE Projects (
    ProjectId int IDENTITY(1,1) PRIMARY KEY,
    ProjectName VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(200),
    StartDate DATE,
    EndDate DATE,
    CreatedBy int NOT NULL ,
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    IsActive BIT DEFAULT 1
);


-- 6. UserProjectRoles
CREATE TABLE UserProjectRoles (
    UserProjectRoleId int IDENTITY(1,1) PRIMARY KEY,
    UserId int NOT NULL,
    ProjectId int NOT NULL,
    RoleId int NOT NULL,
    AssignedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UNIQUE(UserId, ProjectId, RoleId)
);


-- 7. Modules
CREATE TABLE Modules (
    ModuleId int PRIMARY KEY,
    ProjectId int NOT NULL REFERENCES Projects(ProjectId) ON DELETE CASCADE,
    ModuleName VARCHAR(100) NOT NULL,
    Description VARCHAR(200),
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UNIQUE(ProjectId, ModuleName)
);



-- 8. Releases
CREATE TABLE Releases (
    ReleaseId int PRIMARY KEY ,
    ProjectId int NOT NULL REFERENCES Projects(ProjectId) ON DELETE CASCADE,
    ReleaseName VARCHAR(100) NOT NULL,
    ReleaseDate DATE,
    Description VARCHAR(200),
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UNIQUE(ProjectId, ReleaseName)
);


-- 9. Epics
CREATE TABLE Epics (
    EpicId int PRIMARY KEY ,
    ProjectId int NOT NULL REFERENCES Projects(ProjectId) ON DELETE CASCADE,
    EpicName VARCHAR(150) NOT NULL,
    Description VARCHAR(200),
    CreatedBy int NOT NULL ,
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UNIQUE(ProjectId, EpicName)
);



-- 10. Features
CREATE TABLE Features (
    FeatureId int PRIMARY KEY ,
    EpicId int NOT NULL  ,
    FeatureName VARCHAR(150) NOT NULL,
    Description VARCHAR(200),
    CreatedBy int NOT NULL ,
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UNIQUE(EpicId, FeatureName)
);

-- 11. Stories
CREATE TABLE Stories (
    StoryId int PRIMARY KEY ,
    FeatureId int NOT NULL ,
    StoryName VARCHAR(150) NOT NULL,
    Description VARCHAR(200),
    CreatedBy int NOT NULL ,
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UNIQUE(FeatureId, StoryName)
);



-- 12. Sprints
CREATE TABLE Sprints (
    SprintId int PRIMARY KEY,
    ProjectId int NOT NULL REFERENCES Projects(ProjectId) ON DELETE CASCADE,
    SprintName VARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UNIQUE(ProjectId, SprintName)
);


-- 13. SprintStories
CREATE TABLE SprintStories (
    SprintStoryId int PRIMARY KEY,
    SprintId int NOT NULL REFERENCES Sprints(SprintId) ON DELETE CASCADE,
    StoryId int NOT NULL REFERENCES Stories(StoryId) ON DELETE CASCADE,
    UNIQUE(SprintId, StoryId)
);



-- 14. TaskStatuses
CREATE TABLE TaskStatuses (
    StatusId int PRIMARY KEY,
    StatusName VARCHAR(50) NOT NULL UNIQUE
);


-- 15. TaskPriorities
CREATE TABLE TaskPriorities (
    PriorityId int PRIMARY KEY,
    PriorityName VARCHAR(50) NOT NULL UNIQUE
);


-- 16. TaskTypes
CREATE TABLE TaskTypes (
    TypeId int PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL UNIQUE
);




-- 17. Tasks
CREATE TABLE Tasks (
    TaskId int PRIMARY KEY,
    StoryId int NOT NULL REFERENCES Stories(StoryId) ON DELETE CASCADE,
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(200),
    CreatedBy int NOT NULL  ,
    AssignedTo int  ,
    StatusId int NOT NULL REFERENCES TaskStatuses(StatusId),
    PriorityId int NOT NULL REFERENCES TaskPriorities(PriorityId),
    TypeId int NOT NULL REFERENCES TaskTypes(TypeId),
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    DueDate DATE,
    IsDeleted BIT DEFAULT 1
);


-- 18. SubTasks
CREATE TABLE SubTasks (
    SubTaskId int PRIMARY KEY,
    TaskId int NOT NULL REFERENCES Tasks(TaskId) ON DELETE CASCADE,
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(200),
    AssignedTo int,
    StatusId int NOT NULL REFERENCES TaskStatuses(StatusId),
    PriorityId int NOT NULL REFERENCES TaskPriorities(PriorityId),
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    DueDate DATE,
    IsDeleted BIT DEFAULT 1
);



-- 19. TaskProgress
CREATE TABLE TaskProgress (
    ProgressId int PRIMARY KEY,
    TaskId int NOT NULL REFERENCES Tasks(TaskId) ON DELETE CASCADE,
    UpdatedBy int NOT NULL  ,
    StatusId int NOT NULL REFERENCES TaskStatuses(StatusId),
    ProgressNote VARCHAR(200),
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET()
);



-- 20. Comments
CREATE TABLE Comments (
    CommentId int PRIMARY KEY,
    TaskId int ,
    SubTaskId int ,
    Comment  VARCHAR(200) NOT NULL,
    CreatedBy int  ,
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    UpdatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET()
);


-- 22. Notifications
CREATE TABLE Notifications (
    NotificationId int PRIMARY KEY,
    UserId int NOT NULL ,
    Notification VARCHAR(200) NOT NULL,
    IsRead BIT DEFAULT 1,
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET()
);



-- 23. AuditLogs
CREATE TABLE AuditLogs (
    AuditLogId int PRIMARY KEY,
    EntityName VARCHAR(50) NOT NULL,
    EntityId int NOT NULL,
    Action VARCHAR(50) NOT NULL,
    PerformedBy int ,
    PerformedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    Details NVARCHAR(MAX)
);


