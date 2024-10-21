DELIMITER $$

CREATE PROCEDURE RegisterRoomBed (unIdRoom INT, unIdBed INT, unBedQuantity TINYINT)
BEGIN
    INSERT INTO RoomBed (IdRoom, IdBed, BedQuantity)
            VALUES  (unIdRoom, unIdBed, unBedQuantity);
END $$

DELIMITER $$

CREATE PROCEDURE ModifyRoomBed (unIdRoomBed INT, unIdRoom INT, unIdBed INT, unBedQuantity TINYINT)
BEGIN
    UPDATE RoomBed
    SET
        IdRoom = COALESCE(unIdRoom, IdRoom),
        IdBed = COALESCE(unIdBed, IdBed),
        BedQuantity = COALESCE(unBedQuantity, BedQuantity)
    WHERE IdRoomBed = unIdRoomBed;
END $$