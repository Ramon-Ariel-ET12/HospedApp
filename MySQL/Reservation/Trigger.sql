use hospedapp;

DELIMITER $$

CREATE TRIGGER BefInsReservation BEFORE INSERT ON Reservation
FOR EACH ROW
BEGIN
	IF (NOT EXISTS(SELECT * FROM HotelRoom WHERE IdHotel = NEW.IdHotel AND IdRoom = NEW.IdRoom))THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'EL cuarto no existe en el hotel';
	END IF;
	IF (EXISTS(SELECT * FROM Reservation WHERE IdHotel = NEW.IdHotel AND IdRoom = NEW.IdRoom AND StartDate <= NEW.StartDate AND EndDate >= NEW.EndDate))THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Fecha Superpuesta';
	END IF;
END $$