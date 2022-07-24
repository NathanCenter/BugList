CREATE TABLE [dbo].[Bug] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (80) NOT NULL,
    [Line]        VARCHAR (80) NOT NULL,
    [Solved]      VARCHAR (80) NOT NULL,
    [projectId]   INT          NULL,
    [BugTypeId]   INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

