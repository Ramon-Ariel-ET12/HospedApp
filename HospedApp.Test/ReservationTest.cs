using HospedApp.Core.Entities;
using HospedApp.Core.Entities.Relations;

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
            Address = new Address() { IdAddress = 1 },
            RoomBed = new RoomBed() { IdRoomBed = 1 },
            StartDate = new(4000, 12, 1),
            EndDate = new(4000, 12, 20),
            ClientComment = "new DateOnly(4000, 12, 1)",
        };

        await Ado.CreateReservation(reservation);

        var reservations = await Ado.GetReservations();
        var id = reservations.Max(x => x.IdReservation);
        await Ado.CancelReservation(id);
    }
}