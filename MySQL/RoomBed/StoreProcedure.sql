DELIMITER $$

CREATE PROCEDURE RegisterRoomBed (unIdRoom INT, unIdBed INT, unBedQuantity TINYINT)
BEGIN
    INSERT INTO RoomBed (IdRoom, IdBed, BedQuantity)
            VALUES  (unIdRoom, unIdBed, unBedQuantity);
END $$


