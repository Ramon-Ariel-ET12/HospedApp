using System.Runtime.Intrinsics.Arm;

namespace HospedApp.Test;

public class ReservationCancelledTest : AdoTest
{
    [Fact]
    public async Task GetReservationsCancelled()
    {
        var reservationCancelled = await Ado.GetReservationsCancelled();

        Assert.NotEmpty(reservationCancelled);
    }
}
