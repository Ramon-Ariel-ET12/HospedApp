##################################################################################################################################################
DELIMITER $$

CREATE TRIGGER BefInsReservation BEFORE INSERT ON Reservation
FOR EACH ROW
BEGIN
	IF (NOT EXISTS(SELECT * FROM HotelRoom WHERE IdAddress = NEW.IdAddress AND IdRoomBed = NEW.IdRoomBed))THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'EL cuarto no existe en el hotel';
	END IF;
	IF (NOT EXISTS(SELECT * FROM HotelRoom WHERE IdAddress = NEW.IdAddress AND IdAddress = NEW.IdAddress))THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El hotel no tiene esa dirección';
	END IF;
	IF (EXISTS(SELECT * FROM Reservation WHERE Active = TRUE AND IdAddress = NEW.IdAddress AND IdRoomBed = NEW.IdRoomBed AND StartDate <= NEW.StartDate AND EndDate >= NEW.EndDate))THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Fecha Superpuesta';
	END IF;
END $$
##################################################################################################################################################