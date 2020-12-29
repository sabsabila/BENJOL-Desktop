using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    public class ModelSparepart : INotifyPropertyChanged
    {
        public int num { get; set; }
        public string displayPrice { get; set; }
        public int sparepart_id { get; set; }
        public int bengkel_id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public string picture { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Spareparts
    {
        public List<ModelSparepart> spareparts { get; set; }
    }

    public class ItemSparepart
    {
        public ModelSparepart spareparts { get; set; }
    }
}
