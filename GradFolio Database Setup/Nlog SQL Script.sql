CREATE TABLE [dbo].[NLog_Error] (
    [Id]         INT            IDENTITY (1001, 1) NOT NULL,
    [time_stamp] DATETIME       CONSTRAINT [DF_NLogError_time_stamp] DEFAULT (getdate()) NOT NULL,
    [host]       NVARCHAR (MAX) NOT NULL,
    [type]       NVARCHAR (MAX) NOT NULL,
    [source]     NVARCHAR (MAX) NOT NULL,
    [message]    NVARCHAR (MAX) NOT NULL,
    [level]      NVARCHAR (MAX) NOT NULL,
    [logger]     NVARCHAR (MAX) NOT NULL,
    [stacktrace] NVARCHAR (MAX) NOT NULL,
    [allxml]     NTEXT          NOT NULL,
    CONSTRAINT [PK_NLogError] PRIMARY KEY CLUSTERED ([Id] ASC)
);

