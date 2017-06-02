using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiYao.DTO
{
    class MTMHealthCollectDTO
    {
        public double eatingpreference { get; set; }
        public string smoking { get; set; }
        public string drinking { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public double waist { get; set; }
        public int heartrate { get; set; }
        public double systolicpressure { get; set; }
        public double diastolicpressure { get; set; }
        public float fastsugar { get; set; }
        public float randomsugar { get; set; }
        public float HbA1c { get; set; }
        public float cholesterol { get; set; }
        public float triglyceride { get; set; }
        public float ldl { get; set; }
        public float hdl { get; set; }

    }
}
