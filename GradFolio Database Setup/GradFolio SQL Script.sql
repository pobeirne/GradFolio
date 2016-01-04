
CREATE TABLE [dbo].[Profiles] (
    [Id]                UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]            NVARCHAR (128)   NOT NULL,
    [FirstName]         VARCHAR (150)    NOT NULL,
    [LastName]          VARCHAR (150)    NOT NULL,
    [Title]             VARCHAR (150)    NOT NULL,
    [Summary]           VARCHAR (600)    NOT NULL,
    [Location]          VARCHAR (150)    NOT NULL,
    [Mobile ]           NCHAR (16)       NULL,
    [Phone ]            NCHAR (16)       NULL,
    [IsAvailable]       BIT              DEFAULT ((0)) NOT NULL,
    [AvailableFromDate] DATETIME         NULL,
    [ImageUrl]          VARCHAR (MAX)    NULL,
    [PortfolioUrl]      VARCHAR (MAX)    NULL,
    [LinkedInUrl]       VARCHAR (MAX)    NULL,
    [CreateDate]        DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserProfiles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Profiles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Profiles]([Id] ASC);

CREATE TABLE [dbo].[Courses] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]     NVARCHAR (128)   NOT NULL,
    [Title]      VARCHAR (150)    NOT NULL,
    [College]    VARCHAR (150)    NOT NULL,
    [Summary]    VARCHAR (600)    NOT NULL,
    [Location]   VARCHAR (150)    NOT NULL,
    [StartDate]  DATETIME         NOT NULL,
    [EndDate]    DATETIME         NULL,
    [IsCurrent]  BIT              DEFAULT ((0)) NOT NULL,
    [CreateDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserCourses_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Courses]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Courses]([Id] ASC);

CREATE TABLE [dbo].[Experiences] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]     NVARCHAR (128)   NOT NULL,
    [Title]      VARCHAR (150)    NOT NULL,
    [Company]    VARCHAR (150)    NOT NULL,
    [Summary]    VARCHAR (600)    NOT NULL,
    [Location]   VARCHAR (150)    NOT NULL,
    [StartDate]  DATETIME         NOT NULL,
    [EndDate]    DATETIME         NULL,
    [IsCurrent]  BIT              DEFAULT ((0)) NOT NULL,
    [CreateDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserExperiences_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Experiences]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Experiences]([Id] ASC);

CREATE TABLE [dbo].[Skills] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]     NVARCHAR (128)   NOT NULL,
    [Title]      VARCHAR (150)    NOT NULL,
    [Summary]    VARCHAR (600)    NULL,
    [Level]      VARCHAR (150)    NOT NULL,
    [CreateDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserSkills_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Skills]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Skills]([Id] ASC);


CREATE TABLE [dbo].[Awards] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]     NVARCHAR (128)   NOT NULL,
    [Title]      VARCHAR (150)    NOT NULL,
    [Level]      VARCHAR (150)    NOT NULL,
    [IssuedBy]   VARCHAR (150)    NULL,
    [IssuedDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    [CreateDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserAwards_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Awards]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Awards]([Id] ASC);

CREATE TABLE [dbo].[Interests] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]     NVARCHAR (128)   NOT NULL,
    [Title]      VARCHAR (150)    NOT NULL,
    [Summary]    VARCHAR (600)    NULL,
    [CreateDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserInterests_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Interests]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Interests]([Id] ASC);

CREATE TABLE [dbo].[Projects] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]     NVARCHAR (128)   NOT NULL,
    [Title]      VARCHAR (150)    NOT NULL,
    [Summary]    VARCHAR (600)    NULL,
    [CreateDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserProjects_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Projects]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Projects]([Id] ASC);


CREATE TABLE [dbo].[CurriculumVitaes] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]      NVARCHAR (128)   NOT NULL,
    [Name]        VARCHAR (150)    NOT NULL,
    [Type]        VARCHAR (150)    NOT NULL,
    [RefNum]      BIGINT           DEFAULT (CONVERT([bigint],Crypt_Gen_Random((4)))) NOT NULL,
    [Experience1] NVARCHAR (128)   NOT NULL,
    [Experience2] NVARCHAR (128)   NOT NULL,
    [Course1]     NVARCHAR (128)   NOT NULL,
    [Course2]     NVARCHAR (128)   NOT NULL,
    [CreateDate]  DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserCurriculumVitaes_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[CurriculumVitaes]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[CurriculumVitaes]([Id] ASC);

CREATE TABLE [dbo].[Portfolios] (
    [Id]         UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserId]     NVARCHAR (128)   NOT NULL,
    [Type]       VARCHAR (150)    NOT NULL,
    [RefNum]     BIGINT           DEFAULT (CONVERT([bigint],Crypt_Gen_Random((4)))) NOT NULL,
    [CreateDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserPortfolios_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Portfolios]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Portfolios]([Id] ASC);





