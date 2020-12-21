using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestWPPL.Model
{
    public class ModelService : INotifyPropertyChanged
    {
        public int num { get; set; }
        public int service_id { get; set; }
        public int bengkel_id { get; set; }
        public string service_name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
               
    }

    public class Services
    {
        public List<ModelService> services { get; set; }
    }
}
