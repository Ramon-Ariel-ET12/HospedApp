#########################################################################################################################################################
DELIMITER $$

CREATE TRIGGER BefInsReservation BEFORE INSERT ON Reservation
FOR EACH ROW
BEGIN
	IF (NOT EXISTS(SELECT * FROM HotelRoom WHERE IdHotel = NEW.IdHotel AND IdRoom = NEW.IdRoom))THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'EL cuarto no existe en el hotel';
	END IF;
	IF (NOT EXISTS(SELECT * FROM HotelRoom WHERE IdHotel = NEW.IdHotel AND IdAddress = NEW.IdAddress))THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El hotel no tiene esa dirección';
	END IF;
	IF (EXISTS(SELECT * FROM Reservation WHERE Active = TRUE AND IdHotel = NEW.IdHotel AND IdRoom = NEW.IdRoom AND IdAddress = NEW.IdAddress AND StartDate <= NEW.StartDate AND EndDate >= NEW.EndDate))THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Fecha Superpuesta';
	END IF;
END $$
#########################################################################################################################################################
DELIMITER $$

CREATE TRIGGER BefUpdReservation BEFORE UPDATE ON Reservation
FOR EACH ROW
BEGIN
	IF (NOT EXISTS(SELECT * FROM HotelRoom WHERE IdHotel = NEW.IdHotel AND IdRoom = NEW.IdRoom))THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'EL cuarto no existe en el hotel';
	END IF;
	IF (NOT EXISTS(SELECT * FROM HotelRoom WHERE IdHotel = NEW.IdHotel AND IdAddress = NEW.IdAddress))THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El hotel no tiene esa dirección';
	END IF;
	IF (EXISTS(SELECT * FROM Reservation WHERE Active = TRUE AND IdHotel = NEW.IdHotel AND IdRoom = NEW.IdRoom AND IdAddress = NEW.IdAddress AND StartDate <= NEW.StartDate AND EndDate >= NEW.EndDate))THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Fecha Superpuesta';
	END IF;
END $$