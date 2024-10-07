using HospedApp.Core.Entities;

namespace HospedApp.Test;

public class ReservationTest : AdoTest
{
    [Fact]
    public async Task GetReservations()
    {
        var reservation = await Ado.GetReservations();

        Assert.NotEmpty(reservation);
    }

    [Fact]
    public async Task CreateReservation()
    {
        var reservation = new Reservation()
        {
            Client = new Client() { IdClient = 1 },
            Hotel = new Hotel() { IdHotel = 1 },
            Address = new Address() { IdAddress = 1 },
            Room = new Room() { IdRoom = 1 },
            StartDate = new(3000, 12, 1),
            EndDate = new(3000, 12, 1),
            ClientComment = "new DateOnly(2026, 12, 1)",
        };

        await Ado.CreateReservation(reservation);

        var reservations = await Ado.GetReservations();
        var id = reservations.Max(x => x.IdReservation);
        await Ado.CancelReservation(id);
    }
}