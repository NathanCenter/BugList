CREATE TABLE [dbo].[UserProfile] (
    [Id]             INT          NOT NULL IDENTITY,
    [Name]           VARCHAR (80) NOT NULL,
    [UserType]       VARCHAR (80) NOT NULL,
    [FirebaseUserId] VARCHAR (80) NOT NULL,
    [Email]          VARCHAR (80) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

