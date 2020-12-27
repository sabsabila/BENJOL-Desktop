using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    class ObjectPayment
    {
        public String bengkel_note { set; get; }
        public int service_cost { set; get; }
        public int stock { set; get; }

        public ObjectPayment(String bengkel_note, int service_cost)
        {
            this.bengkel_note = bengkel_note;
            this.service_cost = service_cost;
        }
    }
}
