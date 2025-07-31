namespace Rento.Application.Reservations.Common
{
    public record ReservationDateRangeResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
