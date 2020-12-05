using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    class ObjectBengkel
    {
        public String name { set; get; }
        public String phone { set; get; }
        public String email { set; get; }
        public String address { set; get; }

        public ObjectBengkel(String name, String phone, String email, String address)
        {
            this.name = name;
            this.phone = phone;
            this.email = email;
            this.address = address;
        }
    }
}
