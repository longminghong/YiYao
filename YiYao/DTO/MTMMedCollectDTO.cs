using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiYao.DTO
{
    class MTMMedCollectDTO
    {
    }


    public class Rootobject
    {
        public string isallergy { get; set; }
        public Allergicdrug[] allergicdrug { get; set; }
        public string hypotensor { get; set; }
        public int systolicpressure { get; set; }
        public int diastolicpressure { get; set; }
        public Drug[] drugs { get; set; }
    }

    public class Allergicdrug
    {
        public string drugcode { get; set; }
        public string drugname { get; set; }
    }

    public class Drug
    {
        public string name { get; set; }
        public string drug_code { get; set; }
        public string commonname { get; set; }
        public string[] drugstype { get; set; }
        public string regular { get; set; }
        public string whether { get; set; }
        public string behavior { get; set; }
        public int day { get; set; }
        public int time { get; set; }
        public int dosage { get; set; }
        public string time1 { get; set; }
        public string time2 { get; set; }
        public string time3 { get; set; }
        public string time4 { get; set; }
        public string time5 { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }

}
