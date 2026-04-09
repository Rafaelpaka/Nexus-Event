CREATE DATABASE NexusEvent;
GO

USE NexusEvent;
GO

CREATE TABLE Usuarios (
    Cpf       VARCHAR(14)  NOT NULL,
    Nome      VARCHAR(100) NOT NULL,
    Email     VARCHAR(100) NOT NULL,
    SenhaHash VARCHAR(256) NULL,
    Login     VARCHAR(50)  NULL,
    Telefone  VARCHAR(20)  NULL,
    Endereco  VARCHAR(255) NULL,
    CONSTRAINT PK_Usuarios PRIMARY KEY (Cpf)
);
GO

CREATE TABLE Eventos (
    Id              INT           NOT NULL IDENTITY(1,1),
    Nome            VARCHAR(100)  NOT NULL,
    CapacidadeTotal INT           NOT NULL,
    DataEvento      DATETIME      NOT NULL,
    PrecoPadrao     DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_Eventos PRIMARY KEY (Id)
);
GO

CREATE TABLE Cupons (
    Codigo              VARCHAR(50)   NOT NULL,
    PorcentagemDesconto DECIMAL(5,2)  NOT NULL,
    ValorMinimoRegra    DECIMAL(10,2) NOT NULL,
    LimiteUsoPorUsuario INT           NULL,
    Disponibilidade     BIT           NOT NULL DEFAULT 1,
    CONSTRAINT PK_Cupons PRIMARY KEY (Codigo)
);
GO

CREATE TABLE Reservas (
    Id             INT           NOT NULL IDENTITY(1,1),
    UsuarioCpf     VARCHAR(14)   NOT NULL,
    EventoId       INT           NOT NULL,
    CupomUtilizado VARCHAR(50)   NULL,
    ValorFinalPago DECIMAL(10,2) NOT NULL,
    CodigoReserva  VARCHAR(20)   NULL,
    CONSTRAINT PK_Reservas      PRIMARY KEY (Id),
    CONSTRAINT FK_Reservas_User FOREIGN KEY (UsuarioCpf)     REFERENCES Usuarios(Cpf),
    CONSTRAINT FK_Reservas_Even FOREIGN KEY (EventoId)       REFERENCES Eventos(Id),
    CONSTRAINT FK_Reservas_Cupo FOREIGN KEY (CupomUtilizado) REFERENCES Cupons(Codigo)
);
GO