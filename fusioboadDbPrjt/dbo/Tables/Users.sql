CREATE TABLE [dbo].[Users] (
    [UserId]          INT                IDENTITY (1, 1) NOT NULL,
    [Username]        VARCHAR (50)       NOT NULL,
    [Email]           VARCHAR (100)      NOT NULL,
    [PasswordHash]    VARCHAR (255)      NOT NULL,
    [CreatedAt]       DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [UpdatedAt]       DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NULL,
    [IsActive]        BIT                DEFAULT ((1)) NULL,
    [IsEmailVerified] BIT                CONSTRAINT [DF_Users_IsEmailVerified] DEFAULT ((0)) NULL,
    [ProfilePicture]  NVARCHAR (255)     NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);





