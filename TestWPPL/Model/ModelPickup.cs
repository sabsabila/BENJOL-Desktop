using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    public class ModelPickup : INotifyPropertyChanged
    {
        public int num { get; set; }
        public string buttonAction { get; set; }
        public int booking_id { get; set; }
        public string repairment_date { get; set; }
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string pickup_location { get; set; }
        public string dropoff_location { get; set; }
        public string status { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Pickups
    {
        public List<ModelPickup> pickups { get; set; }
    }
}
