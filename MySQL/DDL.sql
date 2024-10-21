DROP DATABASE IF EXISTS HospedApp;

CREATE DATABASE HospedApp;

USE HospedApp;

CREATE TABLE Client (
    IdClient INT PRIMARY KEY AUTO_INCREMENT,
    Dni INT UNIQUE NOT NULL,
    Name Varchar(50) NOT NULL,
    LastName Varchar(50) NOT NULL,
    Sex ENUM('M', 'F') NOT NULL,
    Email Varchar(50) UNIQUE NOT NULL,
    Pass CHAR(64) NOT NULL
);

CREATE TABLE User (
    IdUser INT PRIMARY KEY AUTO_INCREMENT,
    Name Varchar(50) NOT NULL,
    LastName Varchar(50) NOT NULL,
    Email Varchar(50) UNIQUE NOT NULL,
    Pass CHAR(64) NOT NULL
);

CREATE TABLE Bed (
    IdBed INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(50) UNIQUE NOT NULL,
    CanSleep TINYINT NOT NULL
);

CREATE TABLE Room (
    IdRoom INT PRIMARY KEY AUTO_INCREMENT,
    Garage ENUM('CON', 'SIN') NOT NULL,
    PriceNight DECIMAL(10, 2) NOT NULL,
    Description TEXT NOT NULL
);

CREATE TABLE Hotel (
    IdHotel INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(50) UNIQUE NOT NULL,
    Web VARCHAR(50) NOT NULL,
    Star TINYINT NOT NULL
);

CREATE TABLE RoomBed (
    IdRoomBed INT,
    IdRoom INT,
    IdBed INT,
    BedQuantity TINYINT NOT NULL,
    CONSTRAINT PK_RoomBed PRIMARY KEY (IdRoomBed, IdRoom, IdBed),
    CONSTRAINT FK_RoomBed_Room FOREIGN KEY (IdRoom) REFERENCES Room (IdRoom),
    CONSTRAINT FK_RoomBed_Bed FOREIGN KEY (IdBed) REFERENCES Bed (IdBed)
);

CREATE TABLE Address (
    IdAddress INT PRIMARY KEY AUTO_INCREMENT,
    IdHotel INT NOT NULL,
    Domicile VARCHAR(50) UNIQUE NOT NULL,
    PostalCode INT NOT NULL,
    CONSTRAINT FK_Address_Hotel FOREIGN KEY (IdHotel) REFERENCES Hotel (IdHotel)
);

CREATE TABLE HotelRoom (
    IdAddress INT NOT NULL,
    IdRoomBed INT NOT NULL,
    Number TINYINT,
    CONSTRAINT FK_HotelRoom_Address FOREIGN KEY (IdAddress) REFERENCES Address (IdAddress),
    CONSTRAINT FK_HotelRoom_RoomBed FOREIGN KEY (IdRoomBed) REFERENCES RoomBed (IdRoomBed)
);

CREATE TABLE Reservation (
    IdReservation INT PRIMARY KEY AUTO_INCREMENT,
    IdClient INT NOT NULL,
    IdAddress INT NOT NULL,
    IdRoomBed INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    ClientComment TEXT,
    Active BOOL DEFAULT TRUE,
    CONSTRAINT FK_Reservation_Client FOREIGN KEY (IdClient) REFERENCES Client (IdClient),
    CONSTRAINT FK_Reservation_Address FOREIGN KEY (IdAddress) REFERENCES Address (IdAddress),
    CONSTRAINT FK_Reservation_RoomBed FOREIGN KEY (IdRoomBed) REFERENCES RoomBed (IdRoomBed)
);