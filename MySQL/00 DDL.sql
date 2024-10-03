drop database if exists HospedApp;
create database HospedApp;
use HospedApp;
SELECT 'Creando Tablas' AS 'Estado';

CREATE TABLE Reservation(
	IdReservation INT,
	IdClient INT,
	IdHotel INT,
	IdRoom INT,
	StartDate DATE,
	EndDate DATE,
	ClientComment Varchar(45),
	Active DEFAULT TRUE,
	CONSTRAINT PK_Reservation PRIMARY KEY (IdReservation),
	CONSTRAINT FK_Reservation_Client FOREIGN KEY(IdClient)
	REFERENCES Client (IdClient),
	CONSTRAINT FK_Reservation_Hotel FOREIGN KEY(IdHotel)
	REFERENCES Hotel (IdHotel),
	CONSTRAINT FK_Reservation_Room FOREIGN KEY(IdRoom)
	REFERENCES Room (IdRoom)
);

CREATE TABLE Client(
	IdClient INT,
	Dni INT,
	Name Varchar(45),
	LastName Varchar(45),
	Phone Varchar(45),
	Email Varchar(45),
	Pass CHAR,
	CONSTRAINT PK_Client PRIMARY KEY (IdClient)
);

CREATE TABLE User(
	IdUser INT,
	Name Varchar(45),
	LastName Varchar(45),
	Email Varchar(45),
	Pass CHAR,
	CONSTRAINT PK_User PRIMARY KEY (IdUser)
);

CREATE TABLE Bed(
	IdBed INT,
	Name VARCHAR(45),
	CanSleep TINYINT,
	CONSTRAINT PK_Bed PRIMARY KEY (IdBed)
);

CREATE TABLE Room(
	IdRoom INT,
	Garage ENUM,
	PriceNight DECIMAL(10_2),
	Description VARCHAR(45),
	CONSTRAINT PK_Room PRIMARY KEY (IdRoom)
);

CREATE TABLE Hotel(
	IdHotel INT,
	Name VARCHAR(45),
	Phone VARCHAR(45),
	Email VARCHAR(45),
	Web VARCHAR(45),
	Star TINYINT,
	CONSTRAINT PK_Hotel PRIMARY KEY (IdHotel)
);

CREATE TABLE ();




create table Tarjeta(
	idTarjeta INT not null auto_increment,
	Dni INT UNSIGNED,
	Saldo DECIMAL(7,2),
	CONSTRAINT PK_Tarjeta PRIMARY KEY (idTarjeta)
);




create table JuegaFichin(
	idJuegaFichin INT not null auto_increment,
	Dni INT UNSIGNED,
	idTarjeta INT not null,
	idFichin INT not null,
	FechayHora DATETIME,
	Gasto DECIMAL(7,2),
	CONSTRAINT PK_JuegaFichin PRIMARY KEY (idJuegaFichin),
	CONSTRAINT FK_JuegaFichin_Cliente FOREIGN KEY (Dni)
	REFERENCES Cliente (Dni),
	CONSTRAINT FK_JuegaFichin_Tarjeta FOREIGN KEY (idTarjeta)
	REFERENCES Tarjeta (idTarjeta),
	CONSTRAINT FK_JuegaFichin_Fichin FOREIGN KEY (idFichin)
	REFERENCES Fichin (idFichin)
);
