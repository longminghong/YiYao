using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class MTMMedPlanDTO
    {
        public Plandrug[] plandrugs { get; set; }
    }


    /*
     *  
     *  "upc_code": "67889",
      "category": "buydrugs",
      "day": "1",
      "time": "2",
      "dosage": "3",
      "start": "2017-04-05",
      "end": "2017-04-14",
      "date": "2017-04-07",
      "name": "G汤臣倍健维生素B族片（73616）",
      "common_name": "",
      "specification": "550mg*100片"
      *
     */
    public class Plandrug
    {
        public string category { get; set; }
        public string upc_code { get; set; }
        public string common_name { get; set; }
        public string name { get; set; }
        public string specification { get; set; }
        public string day { get; set; }
        public string time { get; set; }
        public string dosage { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string date { get; set; }
    }

}
