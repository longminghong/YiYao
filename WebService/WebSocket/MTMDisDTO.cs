using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
     public class MTMDisDTO
    {
        public string operatetype { get; set; }
        public string errormsg { get; set; }
        public string complete { get; set; }
        public Diseasedata diseasedata { get; set; }
    }

  

    public class Diseasedata
    {
        public object[] disease1 { get; set; }
        public object[] disease2 { get; set; }
        public object[] disease3 { get; set; }
        public object[] disease4 { get; set; }
        public object[] disease6 { get; set; }
        public object[] disease7 { get; set; }
        public object[] disease8 { get; set; }
        public object[] disease9 { get; set; }
        public object[] disease10 { get; set; }
        public object[] disease11 { get; set; }
        public object[] disease12 { get; set; }
        public object[] disease13 { get; set; }
        public object[] disease15 { get; set; }
    }

}
