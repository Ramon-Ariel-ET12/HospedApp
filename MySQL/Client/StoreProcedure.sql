use hospedapp;

#######################################################################################
DELIMITER $$

CREATE PROCEDURE RegisterClient (unDni INT, unName Varchar(50), unLastName Varchar(50), unPhone Varchar(14), unEmail Varchar(50), unPass CHAR(64))
BEGIN
    INSERT INTO Client (Dni, Name, LastName, Phone, Email, Pass)
            VALUES  (unDni, unName, unLastName, unPhone, unEmail, unPass);
END $$
#######################################################################################
DELIMITER $$

CREATE PROCEDURE ModifiClient (unIdClient INT, unDni INT, unName Varchar(50), unLastName Varchar(50), unPhone Varchar(14), unEmail Varchar(50), unPass CHAR(64))
BEGIN
    UPDATE Client
    SET
        Dni = COALESCE(unDni, Dni),
        Name = COALESCE(unName, Name),
        Email = COALESCE(unEmail, Email),
        Pass = COALESCE(unPass, Pass)
    WHERE IdClient = unIdClient;
END $$
#######################################################################################
DELIMITER $$

CREATE PROCEDURE SearchClient (Search VARCHAR(255))
BEGIN
    SELECT * FROM Client
    WHERE
        Dni LIKE CONCAT('%', Search ,'%')
        OR Name LIKE CONCAT('%', Search ,'%')
        OR LastName LIKE CONCAT('%', Search ,'%')
        OR Email LIKE CONCAT('%', Search ,'%')
        OR Pass LIKE CONCAT('%', Search ,'%');
END $$