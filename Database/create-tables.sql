
USE RommanelDB;
GO

IF OBJECT_ID('dbo.Cliente', 'U') IS NULL
BEGIN
    CREATE TABLE Cliente (
        IdCliente INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nome NVARCHAR(MAX) NOT NULL,
        Documento NVARCHAR(MAX) NOT NULL,
        Email NVARCHAR(MAX) NOT NULL,
        DataNascimento DATETIME2 NOT NULL,
        Telefone NVARCHAR(MAX) NOT NULL,
        IdEndereco INT NOT NULL,
        TipoPessoa NVARCHAR(MAX) NOT NULL,
        InscricaoEstadual NVARCHAR(MAX) NOT NULL,
        IsentoIE BIT NOT NULL
    );
    PRINT 'Tabela Cliente criada.'
END
ELSE
BEGIN
    PRINT 'Tabela Cliente já existe.'
END;

IF OBJECT_ID('dbo.Endereco', 'U') IS NULL
BEGIN
    CREATE TABLE Endereco (
        IdEndereco INT NOT NULL PRIMARY KEY,
        Cep NVARCHAR(MAX) NOT NULL,
        Rua NVARCHAR(MAX) NOT NULL,
        Numero NVARCHAR(MAX) NOT NULL,
        Bairro NVARCHAR(MAX) NOT NULL,
        Cidade NVARCHAR(MAX) NOT NULL,
        Estado NVARCHAR(MAX) NOT NULL,
        CONSTRAINT FK_Endereco_Cliente_IdEndereco FOREIGN KEY (IdEndereco)
            REFERENCES Cliente (IdCliente)
            ON DELETE CASCADE
    );
    PRINT 'Tabela Endereco criada.'
END
ELSE
BEGIN
    PRINT 'Tabela Endereco já existe.'
END;
