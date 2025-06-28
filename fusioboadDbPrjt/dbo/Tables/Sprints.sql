CREATE TABLE [dbo].[Sprints] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [ProjectId] INT NULL,  -- Nullable to support global sprints
    [Name] NVARCHAR(100) NOT NULL,
    [Goal] NVARCHAR(500) NULL,
    [StartDate] DATE NOT NULL,
    [EndDate] DATE NOT NULL,
    [IsGlobal] BIT NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,  -- <-- Soft delete flag
    [CreatedBy] INT,
    [CreatedOn] DATETIME DEFAULT GETDATE(),
    [ModifiedBy] INT,
    [ModifiedOn] DATETIME
);
