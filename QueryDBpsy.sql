CREATE DATABASE PsicologiaEscolarDB;

GO

USE PsicologiaEscolarDB;


CREATE TABLE USUARIOS (
IdUsuario INT IDENTITY (1,1) primary key,
Nombre NVARCHAR (100) NOT NULL,
Apellido NVARCHAR (100) NOT NULL,
Email NVARCHAR (255) NOT NULL,
Rol NVARCHAR (50) NOT NULL, -- Docente, Administrativo, Psicologo
PasswordHash NVARCHAR(255) NOT NULL -- Para almacenar contraseña encriptada
);
GO

CREATE TABLE Tickets (
    TicketId INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL, -- Usuario que creó el ticket (docente)
    Estado NVARCHAR(50) NOT NULL DEFAULT 'Pendiente', -- Ejemplo: Pendiente, Aceptado, Realizado
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    Descripcion NVARCHAR(MAX) NOT NULL,
    Respuesta NVARCHAR(MAX), -- Respuesta del psicólogo
    FechaRespuesta DATETIME,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIOS(IdUsuario)
);
GO

INSERT INTO USUARIOS (Nombre, Apellido, Email, Rol, PasswordHash)
VALUES 
('Wendy', 'Villazon', 'wendy@psytrack.com', 'Administrativo', 'pass123'),
('Giusepth', 'Rivera', 'giusepth@psytrack.com', 'Psicologo', 'pass123');
GO

create proc sp_ValidarUsuario
@Email NVARCHAR(255),
@PasswordHash NVARCHAR(255)
AS
BEGIN 
SELECT IdUsuario, Nombre, Rol
FROM USUARIOS
WHERE Email = @Email AND PasswordHash = @PasswordHash;
END

SELECT IdUsuario, Nombre, Rol
FROM USUARIOS
WHERE Email = 'giusepth@psytrack.com' AND PasswordHash = 'pass123';



create proc sp_CrearTicket
@IdUsuario INT,
@Descripcion NVARCHAR(MAX)
AS
BEGIN

	BEGIN TRY
		INSERT INTO Tickets (IdUsuario, Descripcion)
		VALUES (@IdUsuario, @Descripcion);
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END

go 

CREATE PROC sp_ObtenerTicketsPorUsuario
@IdUsuario INT
AS
BEGIN
   SET NOCOUNT ON;
    SELECT *
    FROM Tickets
    WHERE IdUsuario = @IdUsuario
    ORDER BY FechaCreacion DESC;
END

go 

CREATE PROCEDURE sp_ObtenerTodosTickets
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Tickets
    ORDER BY FechaCreacion DESC;
END

go

CREATE PROCEDURE sp_ActualizarTicket
    @TicketId INT,
    @Estado NVARCHAR(50),
    @Respuesta NVARCHAR(MAX)
AS
BEGIN
    UPDATE Tickets
    SET Estado = @Estado,
        Respuesta = @Respuesta,
        FechaRespuesta = GETDATE()
    WHERE TicketId = @TicketId;
END

go 

CREATE PROCEDURE sp_EliminarTicket
    @TicketId INT
AS
BEGIN
    BEGIN TRY
        DELETE FROM Tickets
        WHERE TicketId = @TicketId;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

select * from Tickets