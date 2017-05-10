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
        public int height { get; set; }
        public int weight { get; set; }
        public int waist { get; set; }
        public int heartrate { get; set; }
        public int systolicpressure { get; set; }
        public int diastolicpressure { get; set; }
        public float fastsugar { get; set; }
        public float randomsugar { get; set; }
        public float HbA1c { get; set; }
        public float cholesterol { get; set; }
        public float triglyceride { get; set; }
        public float ldl { get; set; }
        public float hdl { get; set; }

    }
}
