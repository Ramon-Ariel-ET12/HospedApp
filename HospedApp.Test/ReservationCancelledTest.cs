namespace HospedApp.Test;

public class ReservationCancelledTest : AdoTest
{
    [Fact]
    public async Task CreateReservationCancelled()
    {
        var reservation = await Ado.GetReservations();

        var cancell = reservation.Max(x => x.IdReservation);

        await Ado.CancelReservation(cancell);
    }
    [Fact]
    public async Task GetReservationsCancelled()
    {
        var reservationCancelled = await Ado.GetReservationsCancelled();

        Assert.NotEmpty(reservationCancelled);
    }
}
