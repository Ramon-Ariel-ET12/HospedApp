use hospedapp;

DELIMITER $$

CREATE PROCEDURE RegisterRoom (unGarage ENUM('CON', 'SIN'), unPriceNight DECIMAL(10, 2), unDescription TEXT)
BEGIN
    INSERT INTO Room (Garage, PriceNight, Description)
            VALUES (unGarage, unPriceNight, unDescription);
END $$

DELIMITER $$

CREATE PROCEDURE ModifiRoom (unIdRoom INT, unGarage ENUM('CON', 'SIN'), unPriceNight DECIMAL(10, 2), unDescription TEXT)
BEGIN
    UPDATE Room
    SET
        Garage = COALESCE(unGarage, Garage),
        PriceNight = COALESCE(unPriceNight, PriceNight),
        Description = COALESCE(unDescription, Description)
    WHERE IdRoom = unIdRoom;
END $$

DELIMITER $$

CREATE PROCEDURE SearchRoom (Search TEXT)
BEGIN
    SELECT * FROM Room
    WHERE
        Garage LIKE CONCAT('%', Search ,'%') OR
        PriceNight LIKE CONCAT('%', Search ,'%') OR
        Description LIKE CONCAT('%', Search ,'%');
END $$