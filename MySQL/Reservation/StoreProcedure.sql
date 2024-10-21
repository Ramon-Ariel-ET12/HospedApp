DELIMITER $$

CREATE PROCEDURE RegisterReservation (unIdClient INT, unIdAddress INT, unIdRoomBed INT, unStartDate DATE, unEndDate DATE, unClientComment TEXT)
BEGIN
    INSERT INTO Reservation(IdClient, IdAddress, IdRoomBed, StartDate, EndDate, ClientComment)
            VALUES  (unIdClient, unIdAddress, unIdRoomBed, unStartDate, unEndDate, unClientComment);
END $$

DELIMITER $$

CREATE PROCEDURE ModifyReservation (unIdReservation INT, unIdClient INT, unIdAddress INT, unIdRoomBed INT, unStartDate DATE, unEndDate DATE, unClientComment TEXT)
BEGIN
    UPDATE Reservation
    SET
        IdClient = COALESCE(unIdClient, IdClient),
        IdAddress = COALESCE(unIdAddress, IdAddress),
        IdRoomBed = COALESCE(unIdRoomBed, IdRoomBed),
        StartDate = COALESCE(unStartDate, StartDate),
        EndDate = COALESCE(unEndDate, EndDate),
        ClientComment = COALESCE(unClientComment, ClientComment)
    WHERE IdReservation = unIdReservation;
END $$

#####################################################################################