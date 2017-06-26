using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class MTMHealthCollectDTO
    {
        public int eatingpreference { get; set; }
        public string smoking { get; set; }
        public string drinking { get; set; }
        //public double height { get; set; }
        //public double weight { get; set; }
        //public double waist { get; set; }
        //public int heartrate { get; set; }
        //public double systolicpressure { get; set; }
        //public double diastolicpressure { get; set; }
        //public float fastsugar { get; set; }
        //public float randomsugar { get; set; }
        //public float HbA1c { get; set; }
        //public float cholesterol { get; set; }
        //public float triglyceride { get; set; }
        //public float ldl { get; set; }
        //public float hdl { get; set; }

        public float height { get; set; }
        public float weight { get; set; }
        public float waist { get; set; }
        public float heartrate { get; set; }
        public float systolicpressure { get; set; }
        public float diastolicpressure { get; set; }
        public float fastsugar { get; set; }
        public float randomsugar { get; set; }
        public float HbA1c { get; set; }
        public float cholesterol { get; set; }
        public float triglyceride { get; set; }
        public float ldl { get; set; }
        public float hdl { get; set; }

        public float agobloodfat_tcmaxvalue { get; set; }
        public float agobloodfat_ldlmaxvalue { get; set; }
        public float agobloodfat_hdlminvalue { get; set; }
        public float agobloodfat_tgmaxvalue { get; set; }
    }

     
}
