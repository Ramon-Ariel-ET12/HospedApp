use hospedapp;

DELIMITER $$
CREATE PROCEDURE RegisterReservation (unIdClient INT, unIdHotel INT, unIdRoom INT, unStartDate DATE, unEndDate DATE, unClientComment TEXT)
BEGIN
    INSERT INTO Reservation(IdClient, IdHotel, IdRoom, StartDate, EndDate, ClientComment)
            VALUES  (unIdClient, unIdHotel, unIdRoom, unStartDate, unEndDate, unClientComment);
END $$

DELIMITER $$
CREATE PROCEDURE ModifyReservation (unIdReservation INT, unIdClient INT, unIdHotel INT, unIdRoom INT, unStartDate DATE, unEndDate DATE, unClientComment TEXT)
BEGIN
    UPDATE Reservation
    SET
        IdClient = COALESCE(unIdClient, IdClient),
        IdHotel = COALESCE(unIdHotel, IdHotel),
        IdRoom = COALESCE(unIdRoom, IdRoom),
        StarDate = COALESCE(unStarDate, StarDate),
        EndDate = COALESCE(unEndDate, EndDate),
        ClientComment = COALESCE(unClientComment, ClientComment)
    WHERE IdReservation = unIdReservation;
END $$

DELIMITER $$
CREATE PROCEDURE SearchReservation (Search TEXT)
BEGIN
    SELECT r.*, c.*, h.*, ro.* FROM Reservation r
    INNER JOIN Client c ON r.IdClient = c.IdClient
    INNER JOIN Hotel h ON r.IdHotel = h.IdHotel
    INNER JOIN Room ro ON r.IdRoom = ro.IdRoom
        WHERE
            r.StartDate LIKE CONCAT('%', Search ,'%') OR
            r.EndDate LIKE CONCAT('%', Search ,'%') OR
            r.ClientComment LIKE CONCAT('%', Search ,'%') OR
            c.Dni LIKE CONCAT('%', Search ,'%') OR
            c.Name LIKE CONCAT('%', Search ,'%') OR
            c.LastName LIKE CONCAT('%', Search ,'%') OR
            c.Sex LIKE CONCAT('%', Search ,'%') OR
            c.Phone LIKE CONCAT('%', Search ,'%') OR
            c.Email LIKE CONCAT('%', Search ,'%') OR
            c.Pass LIKE CONCAT('%', Search ,'%') OR
            h.Name LIKE CONCAT('%', Search ,'%') OR
            h.Phone LIKE CONCAT('%', Search ,'%') OR
            h.Email LIKE CONCAT('%', Search ,'%') OR
            h.Web LIKE CONCAT('%', Search ,'%') OR
            h.Star LIKE CONCAT('%', Search ,'%') OR
            ro.Garage LIKE CONCAT('%', Search ,'%') OR
            ro.PriceNight LIKE CONCAT('%', Search ,'%');
END $$