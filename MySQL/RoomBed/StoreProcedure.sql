DELIMITER $$

CREATE PROCEDURE RegisterRoomBed (unIdRoom INT, unIdBed INT, unBedQuantity TINYINT)
BEGIN
    INSERT INTO RoomBed (IdRoom, IdBed, BedQuantity)
            VALUES  (unIdRoom, unIdBed, unBedQuantity);
END $$



DELIMITER $$

CREATE PROCEDURE ModifyRoomBed (unIdRoomBed INT, unIdRoom INT, unIdBed INT, unBedQuantity TINYINT)
BEGIN
    INSERT INTO RoomBed (IdRoomBed, IdRoom, IdBed, BedQuantity)
            VALUES  (unIdRoomBed, unIdRoom, unIdBed, unBedQuantity);
END $$