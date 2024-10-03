drop database if exists HospedApp;

create database HospedApp;

use HospedApp;

CREATE TABLE Client (
    IdClient INT,
    Dni INT,
    Name Varchar(30),
    LastName Varchar(30),
    Phone Varchar(14),
    Email Varchar(30),
    Pass CHAR,
    CONSTRAINT PK_Client PRIMARY KEY (IdClient)
);

CREATE TABLE User (
    IdUser INT,
    Name Varchar(30),
    LastName Varchar(30),
    Email Varchar(30),
    Pass CHAR,
    CONSTRAINT PK_User PRIMARY KEY (IdUser)
);

CREATE TABLE Bed (
    IdBed INT,
    Name VARCHAR(30),
    CanSleep TINYINT,
    CONSTRAINT PK_Bed PRIMARY KEY (IdBed)
);

CREATE TABLE Room (
    IdRoom INT,
    Garage ENUM('M', 'F'),
    PriceNight DECIMAL(10, 2),
    Description TEXT,
    CONSTRAINT PK_Room PRIMARY KEY (IdRoom)
);

CREATE TABLE Hotel (
    IdHotel INT,
    Name VARCHAR(30),
    Phone VARCHAR(14),
    Email VARCHAR(30),
    Web VARCHAR(50),
    Star TINYINT,
    CONSTRAINT PK_Hotel PRIMARY KEY (IdHotel)
);

CREATE TABLE RoomBed (
    IdRoom INT,
    IdBed INT,
    BedQuantity TINYINT,
    CONSTRAINT PK_RoomBed PRIMARY KEY (IdRoom, IdBed),
    CONSTRAINT FK_RoomBed_Room FOREIGN KEY (IdRoom) REFERENCES Room (IdRoom),
    CONSTRAINT FK_RoomBed_Bed FOREIGN KEY (IdBed) REFERENCES Bed (IdBed)
);

CREATE TABLE Address (
    IdAddress INT,
    IdHotel INT,
    Domicile VARCHAR(30),
    PostalCode TINYINT,
    CONSTRAINT PK_Address PRIMARY KEY (IdAddress),
    CONSTRAINT FK_Address_Hotel FOREIGN KEY (IdHotel) REFERENCES Hotel (IdHotel)
);

CREATE TABLE HotelRoom (
    IdHotel INT,
    IdRoom INT,
    Number TINYINT,
    CONSTRAINT FK_HotelRoom_Hotel FOREIGN KEY (IdHotel) REFERENCES Hotel (IdHotel),
    CONSTRAINT FK_HotelRoom_Room FOREIGN KEY (IdRoom) REFERENCES Room (IdRoom)
);

CREATE TABLE Reservation (
    IdReservation INT,
    IdClient INT,
    IdHotel INT,
    IdRoom INT,
    StartDate DATE,
    EndDate DATE,
    ClientComment TEXT,
    Active BOOL DEFAULT TRUE,
    CONSTRAINT PK_Reservation PRIMARY KEY (IdReservation),
    CONSTRAINT FK_Reservation_Client FOREIGN KEY (IdClient) REFERENCES Client (IdClient),
    CONSTRAINT FK_Reservation_Hotel FOREIGN KEY (IdHotel) REFERENCES Hotel (IdHotel),
    CONSTRAINT FK_Reservation_Room FOREIGN KEY (IdRoom) REFERENCES Room (IdRoom)
);