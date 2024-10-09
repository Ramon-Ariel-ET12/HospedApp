DELIMITER $$

CREATE PROCEDURE RegisterRoom (unGarage ENUM('CON', 'SIN'), unPriceNight DECIMAL(10, 2), unDescription TEXT)
BEGIN
    INSERT INTO Room (Garage, PriceNight, Description)
            VALUES (unGarage, unPriceNight, unDescription);
END $$

DELIMITER $$

CREATE PROCEDURE ModifyRoom (unIdRoom INT, unGarage ENUM('CON', 'SIN'), unPriceNight DECIMAL(10, 2), unDescription TEXT)
BEGIN
    UPDATE Room
    SET
        Garage = COALESCE(unGarage, Garage),
        PriceNight = COALESCE(unPriceNight, PriceNight),
        Description = COALESCE(unDescription, Description)
    WHERE IdRoom = unIdRoom;
END $$

