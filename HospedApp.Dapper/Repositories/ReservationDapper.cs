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
        = @"SELECT r.*, c.*, h.*, ro.* FROM Reservation r
            INNER JOIN Client c ON r.IdClient = c.IdClient
            INNER JOIN Hotel h ON r.IdHotel = h.IdHotel
            INNER JOIN Room ro ON r.IdRoom = ro.IdRoom";
    private readonly string _reservationCancel
        = @"UPDATE Reservation SET Active = FALSE WHERE IdReservation = @unIdReservation";

    public List<Reservation> GetReservations()
    {
        var reservation = _connection.Query<Reservation, Client, Hotel, Room, Reservation>(_reservationQuery, 
        (reservation, client, hotel, room) =>
        {
            reservation.Client = client;
            reservation.Hotel = hotel;
            reservation.Room = room;
            return reservation;
        },
        splitOn: "IdClient, IdHotel, IdRoom"
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
    public void CancelReservation(int IdReservation)
    {
        _connection.Execute(_reservationCancel, new { unIdReservation = IdReservation});
    }

    private static DynamicParameters ParametersReservation(Reservation reservation)
    {
        var parameters = new DynamicParameters();

        if (reservation.IdReservation == 0)
            parameters.Add("@unIdReservation", reservation.IdReservation);
        parameters.Add("@unIdClient", reservation.Client!.IdClient);
        parameters.Add("@unIdHotel", reservation.Hotel!.IdHotel);
        parameters.Add("@unIdRoom", reservation.Room!.IdRoom);
        parameters.Add("@unStartDate", reservation.StartDate);
        parameters.Add("@unEndDate", reservation.EndDate);
        parameters.Add("@unClientComment", reservation.ClientComment);
        return parameters;
    }
}