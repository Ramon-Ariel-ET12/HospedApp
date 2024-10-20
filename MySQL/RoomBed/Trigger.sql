DELIMITER $$

CREATE TRIGGER BefInsRoomBed BEFORE INSERT ON RoomBed
FOR EACH ROW
BEGIN
    DECLARE sum int;
    SELECT COUNT(IdRoom) INTO sum
	FROM RoomBed;
	SET sum = sum + 1;
	SET NEW.IdRoomBed = sum;
END $$