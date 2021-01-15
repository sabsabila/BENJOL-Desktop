using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    class ObjectBookingDetail
    {
        public String bengkel_note { set; get; }
        public int service_cost { set; get; }
        public ObjectBookingDetail(String bengkel_note, int service_cost)
        {
            this.bengkel_note = bengkel_note;
            this.service_cost = service_cost;
        }
    }
}
