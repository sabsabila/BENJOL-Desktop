using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    public class ModelUser
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
    }

    public class ItemUser
    {
        public ModelUser user { get; set; }
    }

}
