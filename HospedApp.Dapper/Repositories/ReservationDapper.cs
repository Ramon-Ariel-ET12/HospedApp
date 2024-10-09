using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories;

public class ReservationDapper
{
    private readonly IDbConnection _connection;
    public ReservationDapper(IDbConnection connection) => _connection = connection;

    private readonly string _reservationQuery
        = @"SELECT r.*, c.*, h.*, a.*, ro.* FROM Reservation r
            INNER JOIN Client c ON r.IdClient = c.IdClient
            INNER JOIN Hotel h ON r.IdHotel = h.IdHotel
            INNER JOIN Address a ON a.IdAddress = r.IdAddress
            INNER JOIN Room ro ON r.IdRoom = ro.IdRoom
            WHERE r.Active = TRUE";
    private readonly string _reservationCancelledQuery
        = @"SELECT r.*, c.*, h.*, a.*, ro.* FROM Reservation r
            INNER JOIN Client c ON r.IdClient = c.IdClient
            INNER JOIN Hotel h ON r.IdHotel = h.IdHotel
            INNER JOIN Address a ON a.IdAddress = r.IdAddress
            INNER JOIN Room ro ON r.IdRoom = ro.IdRoom
            WHERE r.Active = FALSE";
    private readonly string _reservationCancel
        = @"UPDATE Reservation SET Active = FALSE WHERE IdReservation = @unIdReservation";

    public List<Reservation> GetReservations()
    {
        var reservation = _connection.Query<Reservation, Client, Hotel, Address, Room, Reservation>(_reservationQuery,
        (reservation, client, hotel, address, room) =>
        {
            reservation.Client = client;
            reservation.Hotel = hotel;
            reservation.Address = address;
            reservation.Room = room;
            return reservation;
        },
        splitOn: "IdClient, IdHotel, IdAddress, IdRoom"
        ).ToList();
        return reservation;
    }
    public List<Reservation> GetReservationsCancelled()
    {
        var reservation = _connection.Query<Reservation, Client, Hotel, Address, Room, Reservation>(_reservationCancelledQuery,
        (reservation, client, hotel, address, room) =>
        {
            reservation.Client = client;
            reservation.Hotel = hotel;
            reservation.Address = address;
            reservation.Room = room;
            return reservation;
        },
        splitOn: "IdClient, IdHotel, IdAddress, IdRoom"
        ).ToList();
        return reservation;
    }
    public void CreateReservation(Reservation reservation)
    {
        var parameters = ParametersReservation(reservation);

        try
        {
            _connection.Execute("RegisterReservation", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            if (ex.Number == 1644)
            {
                if (ex.Message.Contains("Superpuesta"))
                    throw new ConstraintException(ex.Message);
            }
        }
    }
    public void ModifyReservation(Reservation reservation)
    {
        var parameters = ParametersReservation(reservation);

        try
        {
            _connection.Execute("ModifyReservation", parameters, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException ex)
        {
            throw new ConstraintException(ex.Message);
        }
    }
    public void CancelReservation(int IdReservation)
    {
        _connection.Execute(_reservationCancel, new { unIdReservation = IdReservation });
    }

    private static DynamicParameters ParametersReservation(Reservation reservation)
    {
        var parameters = new DynamicParameters();

        if (reservation.IdReservation != 0)
            parameters.Add("@unIdReservation", reservation.IdReservation);
        parameters.Add("@unIdClient", reservation.Client!.IdClient);
        parameters.Add("@unIdHotel", reservation.Hotel!.IdHotel);
        parameters.Add("@unIdAddress", reservation.Address!.IdAddress);
        parameters.Add("@unIdRoom", reservation.Room!.IdRoom);
        parameters.Add("@unStartDate", reservation.StartDate);
        parameters.Add("@unEndDate", reservation.EndDate);
        parameters.Add("@unClientComment", reservation.ClientComment);
        return parameters;
    }
}