use hospedapp;

#######################################################################################
DELIMITER $$

CREATE PROCEDURE RegisterClient (unDni INT, unName Varchar(50), unLastName Varchar(50), unSex ENUM('M', 'F'), unPhone Varchar(14), unEmail Varchar(50), unPass CHAR(64))
BEGIN
    INSERT INTO Client (Dni, Name, LastName, Sex, Phone, Email, Pass)
            VALUES  (unDni, unName, unLastName, unSex, unPhone, unEmail, unPass);
END $$
#######################################################################################
DELIMITER $$

CREATE PROCEDURE ModifiClient (unIdClient INT, unDni INT, unName Varchar(50), unLastName Varchar(50), unSex ENUM('M', 'F'), unPhone Varchar(14), unEmail Varchar(50), unPass CHAR(64))
BEGIN
    UPDATE Client
    SET
        Dni = COALESCE(unDni, Dni),
        Name = COALESCE(unName, Name),
        LastName = COALESCE(unLastName, LastName),
        Sex = COALESCE(unSex, Sex),
        Phone = COALESCE(unPhone, Phone),
        Email = COALESCE(unEmail, Email),
        Pass = COALESCE(unPass, Pass)
    WHERE IdClient = unIdClient;
END $$
#######################################################################################
