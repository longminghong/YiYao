using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class MTMRecMedDTO
    {
        public Westdrugs westdrugs { get; set; }
        public Chineseherb[] chineseherb { get; set; }
        public Healthproduct[] healthproduct { get; set; }
        public string westpage { get; set; }
        public string chinesepage { get; set; }
        public string healthpage { get; set; }
        public string operatetype { get; set; }
    }

    public class Westdrugs
    {
        public Firstdrug[] firstdrugs { get; set; }
        public Seconddrug[] seconddrugs { get; set; }
    }

    public class Firstdrug
    {
        public string bigkind { get; set; }
        public string common_name { get; set; }
        public string drug_code { get; set; }
        public string[] indication { get; set; }
        public string littlekind { get; set; }
        public string name { get; set; }
        public string order { get; set; }
        public string producer { get; set; }
        public string retail_price { get; set; }
        public string specification { get; set; }
        public string twonumber { get; set; }
        public string upc_code { get; set; }
        public string[] usage { get; set; }
    }

    public class Seconddrug
    {
        public string bigkind { get; set; }
        public string common_name { get; set; }
        public string drug_code { get; set; }
        public string[] indication { get; set; }
        public string littlekind { get; set; }
        public string name { get; set; }
        public string order { get; set; }
        public string producer { get; set; }
        public string retail_price { get; set; }
        public string specification { get; set; }
        public string upc_code { get; set; }
        public string[] usage { get; set; }
        public string prevkind { get; set; }
        public string group { get; set; }
    }

    public class Chineseherb
    {
        public string name { get; set; }
        public string producer { get; set; }
        public string bar_code { get; set; }
        public string specification { get; set; }
        public string chemical_name { get; set; }
        public string upc_code { get; set; }
        public string categories { get; set; }
        public string dosage { get; set; }
        public string approval_number { get; set; }
        public string retail_price { get; set; }
        public string mtm_code { get; set; }
        public string[] usage { get; set; }
        public string[] indication { get; set; }
    }

    public class Healthproduct
    {
        public string formula { get; set; }
        public string dose { get; set; }
        public string remarks { get; set; }
        public object reason { get; set; }
        public bool whetherRepeat { get; set; }
        public string type { get; set; }
    }



}
