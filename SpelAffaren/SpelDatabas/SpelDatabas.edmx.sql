
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2014 12:03:25
-- Generated from EDMX file: C:\Repo\Grupparbete\SpelAffaren\SpelAffaren\SpelDatabas\SpelDatabas.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SpelAffarenDatabas];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_KonsolProdukt_Konsol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[KonsolProdukt] DROP CONSTRAINT [FK_KonsolProdukt_Konsol];
GO
IF OBJECT_ID(N'[dbo].[FK_KonsolProdukt_Produkt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[KonsolProdukt] DROP CONSTRAINT [FK_KonsolProdukt_Produkt];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_PersonerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ProduktGenre_Produkt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProduktGenre] DROP CONSTRAINT [FK_ProduktGenre_Produkt];
GO
IF OBJECT_ID(N'[dbo].[FK_ProduktGenre_Genre]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProduktGenre] DROP CONSTRAINT [FK_ProduktGenre_Genre];
GO
IF OBJECT_ID(N'[dbo].[FK_ProduktSpelPerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpelPerOrderSet] DROP CONSTRAINT [FK_ProduktSpelPerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_SpelPerOrderOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpelPerOrderSet] DROP CONSTRAINT [FK_SpelPerOrderOrder];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ProduktSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProduktSet];
GO
IF OBJECT_ID(N'[dbo].[KonsolSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KonsolSet];
GO
IF OBJECT_ID(N'[dbo].[GenreSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GenreSet];
GO
IF OBJECT_ID(N'[dbo].[PersonerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonerSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[SpelPerOrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpelPerOrderSet];
GO
IF OBJECT_ID(N'[dbo].[KonsolProdukt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KonsolProdukt];
GO
IF OBJECT_ID(N'[dbo].[ProduktGenre]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProduktGenre];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProduktSet'
CREATE TABLE [dbo].[ProduktSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Namn] nvarchar(max)  NOT NULL,
    [GenreId] int  NOT NULL,
    [KonsolId] int  NOT NULL,
    [Beskrivning] nvarchar(max)  NOT NULL,
    [Utgivningsår] int  NOT NULL,
    [GenreId1] int  NOT NULL
);
GO

-- Creating table 'KonsolSet'
CREATE TABLE [dbo].[KonsolSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Namn] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GenreSet'
CREATE TABLE [dbo].[GenreSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Namn] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PersonerSet'
CREATE TABLE [dbo].[PersonerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Förnamn] nvarchar(max)  NOT NULL,
    [Efternamn] nvarchar(max)  NOT NULL,
    [LogOnEmail] nvarchar(max)  NOT NULL,
    [Lösenord] nvarchar(max)  NOT NULL,
    [AdminStatus] bit  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonId] int  NOT NULL,
    [Kommentar] nvarchar(max)  NOT NULL,
    [PersonerId] int  NOT NULL
);
GO

-- Creating table 'SpelPerOrderSet'
CREATE TABLE [dbo].[SpelPerOrderSet] (
    [Antal] int  NOT NULL,
    [SpelId] int  NOT NULL,
    [OrderId] int  NOT NULL
);
GO

-- Creating table 'KonsolProdukt'
CREATE TABLE [dbo].[KonsolProdukt] (
    [Konsol_Id] int  NOT NULL,
    [Produkt_Id] int  NOT NULL
);
GO

-- Creating table 'ProduktGenre'
CREATE TABLE [dbo].[ProduktGenre] (
    [Produkt_Id] int  NOT NULL,
    [Genre_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProduktSet'
ALTER TABLE [dbo].[ProduktSet]
ADD CONSTRAINT [PK_ProduktSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'KonsolSet'
ALTER TABLE [dbo].[KonsolSet]
ADD CONSTRAINT [PK_KonsolSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GenreSet'
ALTER TABLE [dbo].[GenreSet]
ADD CONSTRAINT [PK_GenreSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonerSet'
ALTER TABLE [dbo].[PersonerSet]
ADD CONSTRAINT [PK_PersonerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SpelId], [OrderId] in table 'SpelPerOrderSet'
ALTER TABLE [dbo].[SpelPerOrderSet]
ADD CONSTRAINT [PK_SpelPerOrderSet]
    PRIMARY KEY CLUSTERED ([SpelId], [OrderId] ASC);
GO

-- Creating primary key on [Konsol_Id], [Produkt_Id] in table 'KonsolProdukt'
ALTER TABLE [dbo].[KonsolProdukt]
ADD CONSTRAINT [PK_KonsolProdukt]
    PRIMARY KEY CLUSTERED ([Konsol_Id], [Produkt_Id] ASC);
GO

-- Creating primary key on [Produkt_Id], [Genre_Id] in table 'ProduktGenre'
ALTER TABLE [dbo].[ProduktGenre]
ADD CONSTRAINT [PK_ProduktGenre]
    PRIMARY KEY CLUSTERED ([Produkt_Id], [Genre_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Konsol_Id] in table 'KonsolProdukt'
ALTER TABLE [dbo].[KonsolProdukt]
ADD CONSTRAINT [FK_KonsolProdukt_Konsol]
    FOREIGN KEY ([Konsol_Id])
    REFERENCES [dbo].[KonsolSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Produkt_Id] in table 'KonsolProdukt'
ALTER TABLE [dbo].[KonsolProdukt]
ADD CONSTRAINT [FK_KonsolProdukt_Produkt]
    FOREIGN KEY ([Produkt_Id])
    REFERENCES [dbo].[ProduktSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_KonsolProdukt_Produkt'
CREATE INDEX [IX_FK_KonsolProdukt_Produkt]
ON [dbo].[KonsolProdukt]
    ([Produkt_Id]);
GO

-- Creating foreign key on [PersonerId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_PersonerOrder]
    FOREIGN KEY ([PersonerId])
    REFERENCES [dbo].[PersonerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonerOrder'
CREATE INDEX [IX_FK_PersonerOrder]
ON [dbo].[OrderSet]
    ([PersonerId]);
GO

-- Creating foreign key on [Produkt_Id] in table 'ProduktGenre'
ALTER TABLE [dbo].[ProduktGenre]
ADD CONSTRAINT [FK_ProduktGenre_Produkt]
    FOREIGN KEY ([Produkt_Id])
    REFERENCES [dbo].[ProduktSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Genre_Id] in table 'ProduktGenre'
ALTER TABLE [dbo].[ProduktGenre]
ADD CONSTRAINT [FK_ProduktGenre_Genre]
    FOREIGN KEY ([Genre_Id])
    REFERENCES [dbo].[GenreSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProduktGenre_Genre'
CREATE INDEX [IX_FK_ProduktGenre_Genre]
ON [dbo].[ProduktGenre]
    ([Genre_Id]);
GO

-- Creating foreign key on [SpelId] in table 'SpelPerOrderSet'
ALTER TABLE [dbo].[SpelPerOrderSet]
ADD CONSTRAINT [FK_ProduktSpelPerOrder]
    FOREIGN KEY ([SpelId])
    REFERENCES [dbo].[ProduktSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [OrderId] in table 'SpelPerOrderSet'
ALTER TABLE [dbo].[SpelPerOrderSet]
ADD CONSTRAINT [FK_SpelPerOrderOrder]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpelPerOrderOrder'
CREATE INDEX [IX_FK_SpelPerOrderOrder]
ON [dbo].[SpelPerOrderSet]
    ([OrderId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------