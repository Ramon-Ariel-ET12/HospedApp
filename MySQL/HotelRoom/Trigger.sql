DELIMITER $$

CREATE TRIGGER BefInsHotelRoom BEFORE INSERT ON HotelRoom
FOR EACH ROW
BEGIN
	DECLARE sum int;
	SELECT COUNT(IdRoom) INTO sum
	FROM HotelRoom WHERE IdHotel = NEW.IdHotel AND IdAddress = NEW.IdAddress;
	SET sum = sum + 1;
	SET NEW.Number = sum;
END $$