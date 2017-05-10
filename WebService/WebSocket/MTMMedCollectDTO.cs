using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class MTMMedCollectDTO
    {
        public string operatetype { get; set; }
        public string errormsg { get; set; }
        public string complete { get; set; }
        public string isallergy { get; set; }
        
        public Allergicdrug[] allergicdrug { get; set; }
        public string hypotensor { get; set; }
        public string systolicpressure { get; set; }
        public string diastolicpressure { get; set; }
        public Drug[] drugs { get; set; }
    }



    public class Drug
    {
        public string name { get; set; }
        public string[] drugstype { get; set; }
        public string regular { get; set; }
        public string whether { get; set; }
        public string behavior { get; set; }
        public Useddosage useddosage { get; set; }
        public Drugtime drugtime { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }

    public class Useddosage
    {
        public string day { get; set; }
        public string time { get; set; }
        public string dosage { get; set; }
    }

    public class Drugtime
    {
        public string time1 { get; set; }
        public string time2 { get; set; }
        public string time3 { get; set; }
        public string time4 { get; set; }
        public string time5 { get; set; }
    }


   
    public class Allergicdrug
    {
        public string drugcode { get; set; }
        public string drugname { get; set; }
    }


}
