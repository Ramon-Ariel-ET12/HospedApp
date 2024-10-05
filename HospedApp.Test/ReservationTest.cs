using HospedApp.Core.Entities;

namespace HospedApp.Test;

public class ReservationTest : AdoTest
{
    [Fact]
    public void GetReservations()
    {
        var reservation = Ado.GetReservations();

        Assert.NotEmpty(reservation);
    }

    [Fact]
    public void CreateReservation()
    {
        var reservation = new Reservation()
        {
            Client = new Client(){ IdClient = 1},
            Hotel = new Hotel(){ IdHotel = 1},
            Room = new Room(){ IdRoom = 1},
            StartDate = "2025-12-1",
            EndDate = "2025-12-1",
            ClientComment = "new DateOnly(2026, 12, 1)",
        };

        Ado.CreateReservation(reservation);

        var reservations = Ado.GetReservations();
        var id = reservations.Max(x => x.IdReservation);
        Ado.CancelReservation(id);
    }
}