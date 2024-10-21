using System.Data;
using Dapper;
using HospedApp.Core.Entities;
using HospedApp.Core.Entities.Relations;
using MySqlConnector;

namespace HospedApp.Dapper.Repositories;

public class ReservationDapper
{
    private readonly IDbConnection _connection;
    public ReservationDapper(IDbConnection connection) => _connection = connection;

    private readonly string _reservationQuery
        = @"SELECT * FROM Reservation r
            INNER JOIN Client c ON c.IdClient = r.IdClient
            INNER JOIN Address a ON a.IdAddress = r.IdAddress
            INNER JOIN Hotel h ON h.IdHotel = a.IdHotel
            INNER JOIN RoomBed ro ON ro.IdRoomBed = r.IdRoomBed
            WHERE r.Active = TRUE";
    private readonly string _reservationCancelledQuery
        = @"SELECT * FROM Reservation r
            INNER JOIN Client c ON c.IdClient = r.IdClient
            INNER JOIN Address a ON a.IdAddress = r.IdAddress
            INNER JOIN Hotel h ON h.IdHotel = a.IdHotel
            INNER JOIN RoomBed ro ON ro.IdRoomBed = r.IdRoomBed
            WHERE r.Active = FALSE";
    private readonly string _reservationCancel
        = @"UPDATE Reservation SET Active = FALSE WHERE IdReservation = @unIdReservation";

    public async Task<List<Reservation>> GetReservations()
    {
        var reservation = (await _connection.QueryAsync<Reservation, Client, Hotel, Address, RoomBed, Reservation>(_reservationQuery,
        (reservation, client, hotel, address, roombed) =>
        {
            reservation.Client = client;
            reservation.Address = address;
            reservation.Address.Hotel = hotel;
            reservation.RoomBed = roombed;
            return reservation;
        },
        splitOn: "IdClient, IdAddress, IdHotel, IdRoomBed"
        )).ToList();
        return reservation;
    }
    public async Task<List<Reservation>> GetReservationsCancelled()
    {
        var reservation = (await _connection.QueryAsync<Reservation, Client, Address, RoomBed, Hotel, Reservation>(_reservationCancelledQuery,
        (reservation, client, address, roombed, hotel) =>
        {
            reservation.Client = client;
            reservation.Address = address;
            reservation.RoomBed = roombed;
            reservation.Address!.Hotel = hotel;
            return reservation;
        },
        splitOn: "IdClient, IdAddress, IdHotel, IdRoomBed"
        )).ToList();
        return reservation;
    }
    public async Task CreateReservation(Reservation reservation)
    {
        var parameters = ParametersReservation(reservation);

        try
        {
            await _connection.ExecuteAsync("RegisterReservation", parameters, commandType: CommandType.StoredProcedure);
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
    public async Task ModifyReservation(Reservation reservation)
    {
        var parameters = ParametersReservation(reservation);

        try
        {
            await _connection.ExecuteAsync("ModifyReservation", parameters, commandType: CommandType.StoredProcedure);
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
    public async Task CancelReservation(int IdReservation)
    {
        await _connection.ExecuteAsync(_reservationCancel, new { unIdReservation = IdReservation });
    }

    private static DynamicParameters ParametersReservation(Reservation reservation)
    {
        var parameters = new DynamicParameters();

        if (reservation.IdReservation != 0)
            parameters.Add("@unIdReservation", reservation.IdReservation);
        parameters.Add("@unIdClient", reservation.Client!.IdClient);
        parameters.Add("@unIdAddress", reservation.Address!.IdAddress);
        parameters.Add("@unIdRoomBed", reservation.RoomBed!.IdRoomBed);
        parameters.Add("@unStartDate", reservation.StartDate);
        parameters.Add("@unEndDate", reservation.EndDate);
        parameters.Add("@unClientComment", reservation.ClientComment);
        return parameters;
    }
}