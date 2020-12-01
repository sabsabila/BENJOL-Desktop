using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    public class ModelBooking
    {
        public int booking_id { get; set; }
        public string repairment_date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string repairment_note { get; set; }
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string service_name { get; set; }
        public string pickup_location { get; set; }
        public string dropoff_location { get; set; }
    }

    public class Bookings
    {
        public List<ModelBooking> booking { get; set; }
    }
}
