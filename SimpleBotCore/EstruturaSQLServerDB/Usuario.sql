USE [dbFIAP]

CREATE TABLE [dbo].[Usuario] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Nome]  VARCHAR (100) NULL,
    [Idade] INT NULL,
    [Cor]  VARCHAR (100) NULL,
);