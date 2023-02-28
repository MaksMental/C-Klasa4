CREATE TABLE [dbo].[Table] (
    [Id]       INT        NOT NULL,
    [login]    NCHAR (26) NULL,
    [password] NCHAR (26) NULL,
    [admin] BIT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

