using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestWPPL.Model
{
    public class ModelBooking : INotifyPropertyChanged
    {
        public int num { get; set; }
        public int booking_id { get; set; }
        public string repairment_date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string status { get; set; }
        public string repairment_note { get; set; }
        public int user_id { get; set; }
        public string full_name { get; set; }
        public string service_name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Bookings
    {
        public List<ModelBooking> booking { get; set; }
    }
}
