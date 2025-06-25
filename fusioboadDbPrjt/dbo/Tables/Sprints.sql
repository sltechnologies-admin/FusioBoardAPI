CREATE TABLE [dbo].[Sprints] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [ProjectId] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Goal] NVARCHAR(500) NULL,
    [StartDate] DATE NOT NULL,
    [EndDate] DATE NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedBy] INT,
    [CreatedOn] DATETIME DEFAULT GETDATE(),
    [ModifiedBy] INT,
    [ModifiedOn] DATETIME
);
