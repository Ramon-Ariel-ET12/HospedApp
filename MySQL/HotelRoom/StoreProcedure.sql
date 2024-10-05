use hospedapp;

DELIMITER $$

CREATE PROCEDURE RegisterHotelRoom (unIdHotel INT, unIdRoom INT)
BEGIN
    INSERT INTO HotelRoom (IdHotel, IdRoom)
            VALUES (unIdHotel, unIdRoom);
END $$

DELIMITER $$

CREATE PROCEDURE SearchHotelRoom (Search TEXT)
BEGIN
	SELECT hr.*, h.*, r.* FROM hotelRoom hr
    INNER JOIN Hotel h ON h.IdHotel = hr.IdHotel
    INNER JOIN Room r ON r.IdRoom = hr.IdRoom
    WHERE 
        h.Name LIKE CONCAT('%', Search ,'%') OR
        h.Phone LIKE CONCAT('%', Search ,'%') OR
        h.Email LIKE CONCAT('%', Search ,'%') OR
        h.Web LIKE CONCAT('%', Search ,'%') OR
        h.Star LIKE CONCAT('%', Search ,'%') OR
        r.Garage LIKE CONCAT('%', Search ,'%') OR
        r.PriceNight LIKE CONCAT('%', Search ,'%');
END $$