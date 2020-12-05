using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestWPPL.Model
{
    public class ModelPayment : INotifyPropertyChanged
    {
        public int payment_id { get; set; }
        public int booking_id { get; set; }
        public string status { get; set; }
        public object receipt { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string repairment_date { get; set; }
        public object service_cost { get; set; }
        public string repairment_note { get; set; }
        public object bengkel_note { get; set; }
        public object first_name { get; set; }
        public object last_name { get; set; }
        public string service_name { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Payments
    {
        public List<ModelPayment> payments { get; set; }
    }

    public class ItemPayments
    {
        public ModelPayment payments { get; set; }
    }
}

