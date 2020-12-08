using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    public class ModelBengkel
    {
        public int bengkel_id { get; set; }
        public int account_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public object created_at { get; set; }
        public object updated_at { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string profile_picture { get; set; }
    }

    public class Bengkels
    {
        public ModelBengkel bengkel { get; set; }
    }
}
