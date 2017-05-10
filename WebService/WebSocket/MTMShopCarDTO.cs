using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class MTMShopCarDTO
    {
        public Buydrug[] buydrugs { get; set; }
    }

    public class Buydrug
    {
        public string _class { get; set; }
        public bool isbuy { get; set; }
        public bool ismyself { get; set; }
        public string money { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string retail_price { get; set; }
        public string specification { get; set; }
        public string upc_code { get; set; }
    }

}
