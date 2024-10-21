DELIMITER $$

CREATE TRIGGER BefInsHotelRoom BEFORE INSERT ON HotelRoom
FOR EACH ROW
BEGIN
	DECLARE sum int;
	SELECT COUNT(IdRoomBed) INTO sum
	FROM HotelRoom WHERE IdAddress = NEW.IdAddress;
	SET sum = sum + 1;
	SET NEW.Number = sum;
END $$