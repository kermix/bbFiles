
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/26/2017 21:32:56
-- Generated from EDMX file: C:\Users\kermax\Source\Repos\bbFiles\bbFiles\bbFiles\Entities\dbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bbFiles];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_Orders_dbo_Acceptors_AcceptorId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_dbo_Orders_dbo_Acceptors_AcceptorId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Donates_dbo_Donors_Donor_PESEL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Donates] DROP CONSTRAINT [FK_dbo_Donates_dbo_Donors_Donor_PESEL];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Donates_dbo_Orders_OrderId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Donates] DROP CONSTRAINT [FK_dbo_Donates_dbo_Orders_OrderId];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAcceptor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acceptors] DROP CONSTRAINT [FK_UserAcceptor];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Acceptors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Acceptors];
GO
IF OBJECT_ID(N'[dbo].[Donates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Donates];
GO
IF OBJECT_ID(N'[dbo].[Donors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Donors];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Acceptors'
CREATE TABLE [dbo].[Acceptors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Address_Street] nvarchar(max)  NULL,
    [Address_City] nvarchar(max)  NULL,
    [Address_PostalCode] nvarchar(max)  NULL,
    [Contact_Phone] nvarchar(max)  NULL,
    [Contact_Email] nvarchar(max)  NULL,
    [User_Id] int  NULL
);
GO

-- Creating table 'Donates'
CREATE TABLE [dbo].[Donates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Amount] int  NOT NULL,
    [Avaliable] bit  NOT NULL,
    [DonateDate] datetime  NOT NULL,
    [OrderId] int  NULL,
    [Donor_PESEL] nvarchar(11)  NOT NULL
);
GO

-- Creating table 'Donors'
CREATE TABLE [dbo].[Donors] (
    [PESEL] nvarchar(11)  NOT NULL,
    [Firstname] nvarchar(max)  NULL,
    [Surname] nvarchar(max)  NULL,
    [Address_Street] nvarchar(max)  NULL,
    [Address_City] nvarchar(max)  NULL,
    [Address_PostalCode] nvarchar(max)  NULL,
    [Contact_Phone] nvarchar(max)  NULL,
    [Contact_Email] nvarchar(max)  NULL,
    [Blood_Type] tinyint  NOT NULL,
    [Blood_RhMarker] bit  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [Blood_Type] tinyint  NOT NULL,
    [Blood_RhMarker] bit  NOT NULL,
    [Amount] int  NOT NULL,
    [Send] bit  NOT NULL,
    [AcceptorId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NULL,
    [RegisteredDate] datetime  NOT NULL,
    [LastLoginDate] datetime  NULL,
    [PasswordChaged] bit  NOT NULL,
    [Role] tinyint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Acceptors'
ALTER TABLE [dbo].[Acceptors]
ADD CONSTRAINT [PK_Acceptors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Donates'
ALTER TABLE [dbo].[Donates]
ADD CONSTRAINT [PK_Donates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PESEL] in table 'Donors'
ALTER TABLE [dbo].[Donors]
ADD CONSTRAINT [PK_Donors]
    PRIMARY KEY CLUSTERED ([PESEL] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AcceptorId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_dbo_Orders_dbo_Acceptors_AcceptorId]
    FOREIGN KEY ([AcceptorId])
    REFERENCES [dbo].[Acceptors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Orders_dbo_Acceptors_AcceptorId'
CREATE INDEX [IX_FK_dbo_Orders_dbo_Acceptors_AcceptorId]
ON [dbo].[Orders]
    ([AcceptorId]);
GO

-- Creating foreign key on [Donor_PESEL] in table 'Donates'
ALTER TABLE [dbo].[Donates]
ADD CONSTRAINT [FK_dbo_Donates_dbo_Donors_Donor_PESEL]
    FOREIGN KEY ([Donor_PESEL])
    REFERENCES [dbo].[Donors]
        ([PESEL])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Donates_dbo_Donors_Donor_PESEL'
CREATE INDEX [IX_FK_dbo_Donates_dbo_Donors_Donor_PESEL]
ON [dbo].[Donates]
    ([Donor_PESEL]);
GO

-- Creating foreign key on [OrderId] in table 'Donates'
ALTER TABLE [dbo].[Donates]
ADD CONSTRAINT [FK_dbo_Donates_dbo_Orders_OrderId]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Donates_dbo_Orders_OrderId'
CREATE INDEX [IX_FK_dbo_Donates_dbo_Orders_OrderId]
ON [dbo].[Donates]
    ([OrderId]);
GO

-- Creating foreign key on [User_Id] in table 'Acceptors'
ALTER TABLE [dbo].[Acceptors]
ADD CONSTRAINT [FK_UserAcceptor]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAcceptor'
CREATE INDEX [IX_FK_UserAcceptor]
ON [dbo].[Acceptors]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------