using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * A3页面*
 */
namespace WebService
{
    public class MTMCustInfo
    {
        public string operatetype { get; set; }
        public string errormsg { get; set; }

        public string name { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string phone { get; set; }
        public string pattern { get; set; }
        public string phonenumber { get; set; }

        public string phonezone { get; set; }
        public string extension { get; set; }

        public string cardno { get; set; }
        public string carddate { get; set; }
        public string ssn { get; set; }
        public string email { get; set; }
        public string detailaddress { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
    }


    public class subInfo
    {
        public string operatetype { get; set; }
        public string errormsg { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string pattern { get; set; }
        public string phone { get; set; }
        public string cardno { get; set; }
        public string carddate { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string detailaddress { get; set; }
        public string ssn { get; set; }
        public string email { get; set; }
    }

}
