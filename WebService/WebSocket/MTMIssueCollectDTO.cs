using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 
 * A6*
 */
namespace WebService
{
    public class MTMIssueCollectDTO
    {
        public int risklevel { get; set; }
        public Icd10 icd10 { get; set; }
        public Code_Correspondence code_correspondence { get; set; }
        public Controllable_Risk controllable_risk { get; set; }
        public Measuredata measuredata { get; set; }
        public Chart_Data chart_data { get; set; }
        public object[] nowdrugs { get; set; }
    }


    public class Rootobject
    {
        
    }

    public class Icd10
    {
        public string[] _1 { get; set; }
        public string[] _2 { get; set; }
        public string[] _6 { get; set; }
        public string[] _7 { get; set; }
        public string[] _8 { get; set; }
        public string[] _9 { get; set; }
        public string[] _10 { get; set; }
        public string[] _12 { get; set; }
        public string[] _13 { get; set; }
        public string[] _15 { get; set; }
    }

    public class Code_Correspondence
    {
        public _1 _1 { get; set; }
        public _2 _2 { get; set; }
        public _3 _3 { get; set; }
        public _4 _4 { get; set; }
        public _5 _5 { get; set; }
        public _6 _6 { get; set; }
        public _7 _7 { get; set; }
        public _8 _8 { get; set; }
        public _9 _9 { get; set; }
        public _10 _10 { get; set; }
        public _11 _11 { get; set; }
        public _12 _12 { get; set; }
        public _13 _13 { get; set; }
        public _15 _15 { get; set; }
    }

    public class _1
    {
        public string name { get; set; }
        public Subdise subdise { get; set; }
    }

    public class Subdise
    {
        public string _1 { get; set; }
    }

    public class _2
    {
        public string name { get; set; }
        public Subdise1 subdise { get; set; }
    }

    public class Subdise1
    {
        public string E78501 { get; set; }
        public string E78001 { get; set; }
        public string E7815178053 { get; set; }
        public string E78203 { get; set; }
        public string E78451 { get; set; }
    }

    public class _3
    {
        public string name { get; set; }
        public Subdise2 subdise { get; set; }
    }

    public class Subdise2
    {
        public string _3 { get; set; }
    }

    public class _4
    {
        public string name { get; set; }
        public Subdise3 subdise { get; set; }
    }

    public class Subdise3
    {
        public string I65202 { get; set; }
        public string I65201 { get; set; }
    }

    public class _5
    {
        public string name { get; set; }
        public Subdise4 subdise { get; set; }
    }

    public class Subdise4
    {
        public string _5 { get; set; }
    }

    public class _6
    {
        public string name { get; set; }
        public Subdise5 subdise { get; set; }
    }

    public class Subdise5
    {
        public string I64X04 { get; set; }
        public string I69452 { get; set; }
        public string I61902 { get; set; }
        public string I69101 { get; set; }
        public string G45901 { get; set; }
    }

    public class _7
    {
        public string name { get; set; }
        public Subdise6 subdise { get; set; }
    }

    public class Subdise6
    {
        public string I74306 { get; set; }
        public string E14503 { get; set; }
        public string I73904 { get; set; }
        public string I99X03 { get; set; }
    }

    public class _8
    {
        public string name { get; set; }
        public Subdise7 subdise { get; set; }
    }

    public class Subdise7
    {
        public string H35952 { get; set; }
        public string H35006 { get; set; }
        public string E14304 { get; set; }
        public string H35001 { get; set; }
    }

    public class _9
    {
        public string name { get; set; }
        public Subdise8 subdise { get; set; }
    }

    public class Subdise8
    {
        public string I20 { get; set; }
        public string I25551 { get; set; }
        public string I25101 { get; set; }
        public string I25652 { get; set; }
        public string E14551 { get; set; }
        public string I11901 { get; set; }
        public string I11951 { get; set; }
        public string I50 { get; set; }
        public string I11051 { get; set; }
        public string I11052 { get; set; }
    }

    public class _10
    {
        public string name { get; set; }
        public Subdise9 subdise { get; set; }
    }

    public class Subdise9
    {
        public string N03 { get; set; }
        public string N03952 { get; set; }
        public string N04903 { get; set; }
        public string E14203 { get; set; }
        public string N03903 { get; set; }
        public string I12903 { get; set; }
        public string I12001 { get; set; }
    }

    public class _11
    {
        public string name { get; set; }
        public Subdise10 subdise { get; set; }
        public Educationimg educationimg { get; set; }
    }

    public class Subdise10
    {
        public string E10E14 { get; set; }
    }

    public class Educationimg
    {
        public string[] 血糖监测 { get; set; }
        public string[] 糖尿病食物金字塔 { get; set; }
        public string[] 预防二型糖尿病 { get; set; }
        public string[] 高血压的危险因子 { get; set; }
    }

    public class _12
    {
        public string name { get; set; }
        public Subdise11 subdise { get; set; }
    }

    public class Subdise11
    {
        public string E14408 { get; set; }
        public string E14503 { get; set; }
        public string E14606 { get; set; }
        public string E14304 { get; set; }
        public string E14551 { get; set; }
        public string E14203 { get; set; }
    }

    public class _13
    {
        public string name { get; set; }
        public Subdise12 subdise { get; set; }
    }

    public class Subdise12
    {
        public string R73002 { get; set; }
    }

    public class _15
    {
        public string name { get; set; }
        public Subdise13 subdise { get; set; }
        public Educationimg1 educationimg { get; set; }
    }

    public class Subdise13
    {
        public string I10X02 { get; set; }
        public string I10X11 { get; set; }
        public string I10I15 { get; set; }
    }

    public class Educationimg1
    {
        public string[] 防止血压剧烈波动 { get; set; }
        public string[] 高血压的危险因子 { get; set; }
        public string[] 高血压患者的生活保健13 { get; set; }
        public string[] 量血压的注意事项 { get; set; }
        public string[] 预防二型糖尿病 { get; set; }
    }

    public class Controllable_Risk
    {
        public string smoking { get; set; }
        public string drinking { get; set; }
        public string fat { get; set; }
        public string eat { get; set; }
        public string usedrug { get; set; }
        public string threehight { get; set; }
    }

    public class Measuredata
    {
        public int height { get; set; }
        public int weight { get; set; }
        public string BMI { get; set; }
        public int waist { get; set; }
    }

    public class Chart_Data
    {
        public Xueya xueya { get; set; }
        public Xuetang xuetang { get; set; }
        public Xuezhi xuezhi { get; set; }
    }

    public class Xueya
    {
        public string unit { get; set; }
        public string[] x { get; set; }
        public int[] systolicpressure { get; set; }
        public int[] diastolicpressure { get; set; }
    }

    public class Xuetang
    {
        public string unit { get; set; }
        public string[] x { get; set; }
        public int[] fastBloodSugar { get; set; }
        public int[] randomBloodSugar { get; set; }
    }

    public class Xuezhi
    {
        public string unit { get; set; }
        public string[] x { get; set; }
        public int[] cholesteroly { get; set; }
        public int[] triglyceridey { get; set; }
        public int[] ldlcy { get; set; }
        public int[] hdlcy { get; set; }
    }

}
