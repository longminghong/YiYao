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
