
#######################################################################################
DELIMITER $$

CREATE PROCEDURE RegisterHotel (unName VARCHAR(50), unEmail VARCHAR(50), unWeb VARCHAR(50), unStar TINYINT)
BEGIN
    INSERT INTO Hotel (Name, Email, Web, Star)
        VALUES  (unName, unEmail, unWeb, unStar);
END $$
#######################################################################################
DELIMITER $$

CREATE PROCEDURE ModifyHotel (unIdHotel INT, unName VARCHAR(50), unEmail VARCHAR(50), unWeb VARCHAR(50), unStar TINYINT)
BEGIN
    UPDATE Hotel 
    SET
        Name = COALESCE(unName, Name),
        Email = COALESCE(unEmail, Email),
        Web = COALESCE(unWeb, Web),
        Star = COALESCE(unStar, Star)
    WHERE IdHotel = unIdHotel;
END $$
#######################################################################################

