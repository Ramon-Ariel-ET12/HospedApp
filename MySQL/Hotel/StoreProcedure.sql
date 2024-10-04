-- Active: 1726541820216@@127.0.0.1@3306@hospedapp
USE HospedApp;

#######################################################################################
DELIMITER $$

CREATE PROCEDURE RegisterHotel (unName VARCHAR(50), unPhone VARCHAR(14), unEmail VARCHAR(50), unWeb VARCHAR(50), unStar TINYINT)
BEGIN
    INSERT INTO Hotel (Name, Phone, Email, Web, Star)
        VALUES  (unName, unPhone, unEmail, unWeb, unStar);
END $$
#######################################################################################
DELIMITER $$

CREATE PROCEDURE ModifyHotel (unIdHotel INT, unName VARCHAR(50), unPhone VARCHAR(14), unEmail VARCHAR(50), unWeb VARCHAR(50), unStar TINYINT)
BEGIN
    UPDATE Hotel 
    SET
        Name = COALESCE(unName, Name),
        Phone = COALESCE(unPhone, Phone),
        Email = COALESCE(unEmail, Email),
        Web = COALESCE(unWeb, Web),
        Star = COALESCE(unStar, Star)
    WHERE IdHotel = unIdHotel;
END $$
#######################################################################################

DELIMITER $$

CREATE PROCEDURE SearchHotel (Search varchar(255))
BEGIN
	SELECT * FROM hotel
    WHERE 
        Name LIKE CONCAT('%', Search ,'%') 
        OR Phone LIKE CONCAT('%', Search ,'%')
        OR Email LIKE CONCAT('%', Search ,'%')
        OR Web LIKE CONCAT('%', Search ,'%')
        OR Star LIKE CONCAT('%', Search ,'%');
END $$
#######################################################################################