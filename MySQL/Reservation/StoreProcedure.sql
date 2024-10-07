DELIMITER $$

CREATE PROCEDURE RegisterReservation (unIdClient INT, unIdHotel INT, unIdAddress INT, unIdRoom INT, unStartDate DATE, unEndDate DATE, unClientComment TEXT)
BEGIN
    INSERT INTO Reservation(IdClient, IdHotel, IdAddress, IdRoom, StartDate, EndDate, ClientComment)
            VALUES  (unIdClient, unIdHotel, unIdAddress, unIdRoom, unStartDate, unEndDate, unClientComment);
END $$

DELIMITER $$

CREATE PROCEDURE ModifyReservation (unIdReservation INT, unIdClient INT, unIdHotel INT, unIdAddress INT, unIdRoom INT, unStartDate DATE, unEndDate DATE, unClientComment TEXT)
BEGIN
    UPDATE Reservation
    SET
        IdClient = COALESCE(unIdClient, IdClient),
        IdHotel = COALESCE(unIdHotel, IdHotel),
        IdAddress = COALESCE(unIdAddress, IdAddress),
        IdRoom = COALESCE(unIdRoom, IdRoom),
        StartDate = COALESCE(unStartDate, StartDate),
        EndDate = COALESCE(unEndDate, EndDate),
        ClientComment = COALESCE(unClientComment, ClientComment)
    WHERE IdReservation = unIdReservation;
END $$

#####################################################################################