DELIMITER $$

CREATE PROCEDURE RegisterHotelRoom (unIdAddress INT, unIdRoomBed INT)
BEGIN
    INSERT INTO HotelRoom (IdAddress, IdRoomBed)
            VALUES (unIdAddress, unIdRoomBed);
END $$

