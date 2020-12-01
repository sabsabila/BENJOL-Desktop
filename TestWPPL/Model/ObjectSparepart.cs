using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPPL.Model
{
    class ObjectSparepart
    {
        public String name { set; get; }
        public int price { set; get;}
        public int stock { set; get; }

        public ObjectSparepart(String name, int price, int stock)
        {
            this.name = name;
            this.price = price;
            this.stock = stock;
        }
    }
}
