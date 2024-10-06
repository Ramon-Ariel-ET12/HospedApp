use hospedapp;

DELIMITER $$

CREATE PROCEDURE RegisterHotelRoom (unIdHotel INT, unIdAddress INT, unIdRoom INT)
BEGIN
    INSERT INTO HotelRoom (IdHotel, IdAddress, IdRoom)
            VALUES (unIdHotel, unIdAddress, unIdRoom);
END $$

