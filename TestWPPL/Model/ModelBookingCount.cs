namespace TestWPPL.Model
{
    public class ModelBookingCount
    {
        public int booking_count { get; set; }
    }

    public class BookingCount
    {
        public ModelBookingCount count { get; set; }
    }
}
