CREATE TABLE APIMatrix (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    -- Core Information
    Module NVARCHAR(100),
    Classification NVARCHAR(100),
    Feature NVARCHAR(150),
    Purpose NVARCHAR(200),
    StoredProcedure NVARCHAR(150),
    APIEndpoint NVARCHAR(200),
    AdditionalDetails NVARCHAR(300),
    UIPageUsage NVARCHAR(150),
    NavigationPath NVARCHAR(150),

    -- Tracking & Status
    ImplementationStatus VARCHAR(20) DEFAULT 'Pending',  -- Pending, In Progress, Done
    UI_API_IntegrationTestingStatus VARCHAR(20) DEFAULT 'Not Started',  -- Not Started, In Progress, Passed, Failed
    BugsCount INT DEFAULT 0,

    -- Assignment & Testing
    AssignedTo NVARCHAR(100) NULL,
    APITestedBy NVARCHAR(100) NULL,
    UITestedBy NVARCHAR(100) NULL,

    -- Risk & Release
    BlockingBugDescription NVARCHAR(300) NULL,
    Priority VARCHAR(10) DEFAULT 'Medium',  -- Low, Medium, High
    Phase VARCHAR(50) DEFAULT 'Phase 1',

    -- Timestamps
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);
