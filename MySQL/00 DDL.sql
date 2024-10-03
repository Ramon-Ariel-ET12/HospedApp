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
	PriceNight DECIMAL(10 ,2),
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

CREATE TABLE RoomBed(
	IdRoom INT,
	IdBed INT,
	BedQuantity TINYINT,
	CONSTRAINT PK_RoomBed PRIMARY KEY (IdRoom),
	CONSTRAINT PK_RoomBed PRIMARY KEY (IdBed),
	CONSTRAINT FK_RoomBed_Room FOREIGN KEY(IdRoom)
	REFERENCES Room (IdRoom),
	CONSTRAINT FK_RoomBed_Bed FOREIGN KEY(IdBed)
	REFERENCES Bed (IdBed)
);

CREATE TABLE Adress(
	IdAdress INT,
	IdHotel INT,
	Domicile VARCHAR(45),
	PostalCode TINYINT,
	CONSTRAINT PK_Adress PRIMARY KEY (IdAdress),
	CONSTRAINT FK_Adress_Hotel FOREIGN KEY(IdHotel)
	REFERENCES Hotel (IdHotel)
);

CREATE TABLE HotelRoom(
	IdHotel INT,
	IdRoom INT,
	Number TINYINT,
	CONSTRAINT PK_HotelRoom PRIMARY KEY (IdHotel),
	CONSTRAINT PK_HotelRoom PRIMARY KEY (IdRoom),
	CONSTRAINT FK_HotelRoom_Hotel FOREIGN KEY(IdHotel)
	REFERENCES Hotel (IdHotel)
	CONSTRAINT FK_HotelRoom_Room FOREIGN KEY(IdRoom)
	REFERENCES Room (IdRoom)
);
