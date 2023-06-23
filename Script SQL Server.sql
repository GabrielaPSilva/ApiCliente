CREATE DATABASE DBCliente;
GO

USE DBCliente;
GO

--DROP TABLE Cliente
CREATE TABLE Cliente 
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nome VARCHAR(100) NOT NULL,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Ativo BIT NOT NULL DEFAULT 1
);
GO

CREATE NONCLUSTERED INDEX ClienteIdEmail
ON Cliente
    (
        Id ASC,
        Email ASC
    );
GO

--DROP TABLE TipoTelefone
CREATE TABLE TipoTelefone
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Tipo VARCHAR(50) UNIQUE NOT NULL
);

--DROP TABLE TelefoneCliente
CREATE TABLE TelefoneCliente
(
	Id INT IDENTITY(1,1),
	IdCliente INT NOT NULL,
	IdTipoTelefone INT NOT NULL,
	Telefone VARCHAR(20) PRIMARY KEY NOT NULL,
	Ativo BIT NOT NULL DEFAULT 1,
	CONSTRAINT FK_Cliente_TelefoneCliente FOREIGN KEY (IdCliente) REFERENCES Cliente(Id),
	CONSTRAINT FK_TelefoneCliente_TipoTelefone FOREIGN KEY (IdTipoTelefone) REFERENCES TipoTelefone(Id)
);
GO

CREATE NONCLUSTERED INDEX TelefoneIdTipo 
ON TelefoneCliente
    (
        IdCliente ASC,
        Telefone ASC
    );
GO

SELECT * FROM Cliente WHERE Ativo = 1 ORDER BY Nome;
SELECT * FROM TelefoneCliente WHERE Ativo = 1;
SELECT * FROM TipoTelefone ORDER BY Tipo;