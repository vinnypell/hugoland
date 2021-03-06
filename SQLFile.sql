SELECT * FROM Classe

SELECT * FROM CompteJoueur

SELECT * FROM EffetItem

SELECT * FROM Heros

SELECT * FROM InventaireHero

SELECT * FROM Item

SELECT * FROM Monde

SELECT * FROM Monstre

SELECT * FROM ObjetMonde

--Delete all tables

DELETE FROM Classe
go
DELETE FROM CompteJoueur
go
DELETE FROM EffetItem
DELETE FROM Heros
DELETE FROM InventaireHero
DELETE FROM Item
DELETE FROM Monde
DELETE FROM Monstre
DELETE FROM ObjetMonde

USE [4DB-Equipe5-2021]
GO

DECLARE @RC int
DECLARE @pNomUtilisateur nvarchar(50)
DECLARE @pCourriel nvarchar(255)
DECLARE @pPrenom nvarchar(50)
DECLARE @pNom nvarchar(50)
DECLARE @pTypeUtilisateur int
DECLARE @pMotDePasse nvarchar(50)
DECLARE @Message nvarchar(250)

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[CreerCompteJoueur] 
   'Admin'
  ,'admin@gmail.com'
  ,'testAdmin'
  ,'testAdmin'
  ,1
  ,'1234'
  ,@Message OUTPUT
GO