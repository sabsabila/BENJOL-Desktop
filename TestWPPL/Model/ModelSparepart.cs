using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    public class ModelSparepart
    {
        public int sparepart_id { get; set; }
        public int bengkel_id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public string picture { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Spareparts
    {
        public List<ModelSparepart> spareparts { get; set; }
    }

}
