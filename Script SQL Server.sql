CREATE DATABASE DBCliente;
GO

USE DBCliente;
GO

--DROP TABLE Cliente
CREATE TABLE Cliente 
(
	Id INT IDENTITY(1,1),
	Nome VARCHAR(100) NOT NULL,
	Email VARCHAR(100) PRIMARY KEY NOT NULL,
	Ativo BIT NOT NULL DEFAULT 1

	CONSTRAINT ClienteIdEmail NONCLUSTERED INDEX
    (
        Id ASC,
        Email ASC
    )
);
GO

--DROP TABLE TelefoneCliente
CREATE TABLE TelefoneCliente
(
	Id INT IDENTITY(1,1),
	IdCliente INT NOT NULL,
	IdTipoTelefone INT NOT NULL,
	Telefone VARCHAR(20) PRIMARY KEY NOT NULL,
	Ativo BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (IdCliente) REFERENCES Cliente(Id),
	FOREIGN KEY (IdTipoTelefone) REFERENCES TipoTelefone(Id)

	CONSTRAINT TelefoneIdTipo NONCLUSTERED INDEX
    (
        IdCliente ASC,
        Telefone ASC
    )
)

--DROP TABLE TipoTelefone
CREATE TABLE TipoTelefone
(
	Id INT IDENTITY(1,1),
	Tipo VARCHAR(50) PRIMARY KEY NOT NULL,
	Ativo BIT NOT NULL DEFAULT 1
)

SELECT * FROM Cliente WHERE Ativo = 1 ORDER BY Nome;
SELECT * FROM TelefoneCliente WHERE Ativo = 1;
SELECT * FROM TipoTelefone ORDER BY Tipo;