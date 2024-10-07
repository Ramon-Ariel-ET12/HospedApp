DELIMITER $$

CREATE PROCEDURE RegisterUser (unName VARCHAR(50), unLastName VARCHAR(50), unEmail VARCHAR(50), unPass CHAR(64))
BEGIN
    INSERT INTO User (Name, LastName, Email, Pass)
            VALUES  (unName, unLastName, unEmail, unPass);
END $$
####################################################################################

DELIMITER $$

CREATE PROCEDURE GetEmailPass (unEmail VARCHAR(50), unPass VARCHAR(50))
BEGIN
    SELECT * FROM User WHERE LOWER(Email) = LOWER(unEmail) AND Pass = SHA2(unPass, 256);
END $$