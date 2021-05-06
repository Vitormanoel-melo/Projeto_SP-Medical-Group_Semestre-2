--DQL
USE SPMed_Group;
GO

-- Mostrou a quantidade de usuários após realizar a importação do banco de dados
SELECT COUNT(idUsuario) AS QtdUsuarios FROM usuarios;
GO


-- O médico poderá ver os agendamentos (consultas) associados a ele
SELECT nome AS NomeMedico, dataConsulta, CONVERT(VARCHAR(5), horaConsulta) AS HorarioConsulta, descricao FROM medicos
INNER JOIN consultas
ON medicos.idMedico = consultas.idMedico
WHERE medicos.idMedico = 1;
GO


-- O paciente poderá visualizar suas próprias consultas;
SELECT nome AS NomePaciente, dataConsulta, CONVERT(VARCHAR(5), horaConsulta) AS HorarioConsulta, descricao FROM pacientes
INNER JOIN consultas
ON pacientes.idPaciente = consultas.idPaciente
WHERE pacientes.idPaciente = 4;
GO


-- Converteu a data de nascimento do usuário para o formato (mm-dd-yyyy) na exibição
SELECT nome ,CONVERT (VARCHAR(10), dataNascimento, 101) AS DataNascimento FROM usuarios
INNER JOIN pacientes
ON usuarios.idUsuario = pacientes.idUsuario
GO


-- Criou uma função para retornar a quantidade de médicos de uma determinada especialidade
CREATE FUNCTION QtdMedicos(@idEspecialidade INT)
RETURNS INT
AS
BEGIN
	DECLARE @especialidade VARCHAR(100)
		   ,@quantidade INT

	SELECT @quantidade = COUNT(nome) FROM medicos
	WHERE medicos.idEspecialidade = @idEspecialidade

	RETURN @quantidade
END
GO

SELECT dbo.QtdMedicos(16) AS numero_medicos;
GO


-- Criou uma função para que retorne a idade do usuário a partir de uma determinada stored procedure
CREATE PROCEDURE idade
AS
	SELECT nome, DATEPART(YEAR, DATEADD(YEAR, -1, GETDATE())) - DATEPART(YEAR, dataNascimento) AS Idade FROM pacientes

EXEC idade;