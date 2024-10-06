using System.Runtime.Intrinsics.Arm;

namespace HospedApp.Test;

public class ReservationCancelledTest : AdoTest
{
    [Fact]
    public void GetReservationsCancelled()
    {
        var reservationCancelled = Ado.GetReservationsCancelled();

        Assert.NotEmpty(reservationCancelled);
    }
}
