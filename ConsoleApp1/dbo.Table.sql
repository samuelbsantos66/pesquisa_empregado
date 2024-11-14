CREATE TABLE [dbo].[Table]
(
	[matricula] INT NOT NULL PRIMARY KEY IDENTITY, 
    [cpf] NCHAR(11) NULL, 
    [nome] NVARCHAR(MAX) NULL, 
    [endereco] NVARCHAR(MAX) NULL
)
