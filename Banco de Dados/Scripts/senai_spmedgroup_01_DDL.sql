--DDL

CREATE DATABASE SPMed_Group;
GO

USE SPMed_Group;
GO

CREATE TABLE tiposUsuarios
(
	idTipoUsuario	INT PRIMARY KEY IDENTITY
	,nomeTipo		VARCHAR(100) NOT NULL
);
GO


CREATE TABLE usuarios
(
	idUsuario		INT PRIMARY KEY IDENTITY
	,idTipoUsuario	INT FOREIGN KEY REFERENCES tiposUsuarios(idTipoUsuario)
	,email			VARCHAR(255) UNIQUE NOT NULL
	,senha			VARCHAR(255) NOT NULL
);
GO


CREATE TABLE pacientes
(
	idPaciente			INT PRIMARY KEY IDENTITY
	,idUsuario			INT FOREIGN KEY REFERENCES usuarios(idUsuario)
	,nome				VARCHAR(255) NOT NULL
	,rg					CHAR(9) UNIQUE NOT NULL
	,cpf				CHAR(11) UNIQUE NOT NULL
	,telefone			VARCHAR(100)
	,dataNascimento		DATE NOT NULL
	,endereco			VARCHAR(200)
);
GO


CREATE TABLE clinicas
(
	idClinica			INT PRIMARY KEY IDENTITY
	,nomeClinica		VARCHAR(200) NOT NULL
	,dataAbertura		VARCHAR(200) NOT NULL
	,horarioAbertura	TIME NOT NULL
	,horarioFechamento	TIME
	,cnpj				CHAR(14) NOT NULL
	,razaoSocial		VARCHAR(255)
	,endereco			VARCHAR(255) UNIQUE NOT NULL
);
GO


CREATE TABLE especialidades
(
	idEspecialidade	INT PRIMARY KEY IDENTITY
	,descricao		VARCHAR(200) UNIQUE NOT NULL
);
GO


CREATE TABLE medicos
(
	idMedico			INT PRIMARY KEY IDENTITY
	,idEspecialidade	INT FOREIGN KEY REFERENCES especialidades(idEspecialidade)
	,idUsuario			INT FOREIGN KEY REFERENCES usuarios(idUsuario)
	,idClinica			INT FOREIGN KEY REFERENCES clinicas(idClinica)
	,nome				VARCHAR(255) NOT NULL
	,crm				CHAR(7) NOT NULL
);
GO

CREATE TABLE situacoes
(
	idSituacao	INT PRIMARY KEY IDENTITY
	,descricao	VARCHAR(100)
);

CREATE TABLE consultas
(
	idConsulta		INT PRIMARY KEY IDENTITY
	,idMedico		INT FOREIGN KEY REFERENCES medicos(idMedico)
	,idPaciente		INT FOREIGN KEY REFERENCES pacientes(idPaciente)
	,idSituacao		INT FOREIGN KEY REFERENCES situacoes(idSituacao)
	,dataConsulta	DATE NOT NULL
	,horaConsulta	TIME NOT NULL
	,descricao		VARCHAR(255) DEFAULT('consulta')
);