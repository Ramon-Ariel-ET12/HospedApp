use hospedapp;

DELIMITER $$

CREATE PROCEDURE RegisterRoomBed (unIdRoom INT, unIdBed INT, unBedQuantity TINYINT)
BEGIN
    INSERT INTO RoomBed (IdRoom, IdBed, BedQuantity)
            VALUES  (unIdRoom, unIdBed, unBedQuantity);
END $$


DELIMITER $$

CREATE PROCEDURE SearchRoomBed (Search TEXT)
BEGIN
	SELECT rb.*, h.*, r.* FROM RoomBed br
    INNER JOIN Room r ON r.IdRoom = br.IdRoom
    INNER JOIN Bed b ON b.IdBed = br.IdBed
    WHERE 
        r.Garage LIKE CONCAT('%', Search ,'%') OR
        r.PriceNight LIKE CONCAT('%', Search ,'%') OR
        b.Name LIKE CONCAT('%', Search ,'%') OR
        b.CanSleep LIKE CONCAT('%', Search ,'%');
END $$