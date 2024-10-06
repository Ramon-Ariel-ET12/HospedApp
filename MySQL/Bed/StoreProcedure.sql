use hospedapp;

DELIMITER $$

CREATE PROCEDURE RegisterBed (unName VARCHAR(50), unCanSleep TINYINT)
BEGIN
    INSERT INTO Bed (Name, CanSleep)
            VALUES  (unName, unCanSleep);
END $$

DELIMITER $$

CREATE PROCEDURE ModifyBed (unIdBed INT, unName VARCHAR(50), unCanSleep TINYINT)
BEGIN
    UPDATE Bed
    SET 
        Name = COALESCE(unName, Name),
        CanSleep = COALESCE(unCanSleep, CanSleep)
    WHERE IdBed = unIdBed;
END $$

