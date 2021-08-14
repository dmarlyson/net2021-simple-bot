USE [dbFIAP]

CREATE TABLE [dbo].[Pergunta] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Usuario]  VARCHAR (100) NULL,
    [Pergunta] VARCHAR (MAX) NULL
);