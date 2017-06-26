using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ScanDrug : UserControl
    {
        const int Scan = 1;
        const int Push = 2;
        string drugId;
        Storyboard scan2;
        Storyboard scan1;
        Storyboard scan3;
        Storyboard scan4;
        Storyboard scan5;
        int state = Scan;
        string readingImageString;
        List<DrugInfo> drugInfo;
        DrugInfo selectedDrug;
        void InitializingDrugInfo()
        {
            drugInfo = new List<DrugInfo>
            {
                
                new DrugInfo
                {
                    DrugID = "6901028062008",
                    DrugName = "小苏",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法小苏用法",
                                DrugUnwell = "小苏不良小苏不良反应小苏不良反应小苏不良小苏不良反应小苏不良反应小苏不良小苏不良反应小苏不良反应小苏不良小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应小苏不良反应",
                                DrugNotice = "小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项小苏注意事项",
                                DrugStoreAdv = "小苏存储事项孩子接触不到的地方小苏存储事项孩子接触不到的地方小苏存储事项孩子接触不到的地方小苏存储事项孩子接触不到的地方小苏存储事项孩子接触不到的地方小苏存储事项孩子接触不到的地方小苏存储事项孩子接触不到的地方。",
                     },
                },

                new DrugInfo
                {
                    DrugID = "6921168509256",
                    DrugName = "测试矿泉水",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "矿泉水用法",
                                DrugUnwell = "矿泉水不良反应",
                                DrugNotice = "矿泉水注意事项",
                                DrugStoreAdv = "矿泉水存储事项孩子接触不到的地方。",
                     },
                },

                new DrugInfo
                {
                    DrugID = "6958703500010",
                    DrugName = "苯磺酸氨氯地平片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	常见的不良反应有水肿、面部潮红、心悸、嗜睡、头晕、腹痛；\n水肿常常发生于脚踝，如果您不能耐受请就诊咨询，医生会给您调整治疗方案。",
                                DrugNotice = "-	一些药物可能会影响该药的效果，如果您正在服用其它药物，请告知您的医生；\n服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },

                new DrugInfo
                {
                    DrugID = "6958703500096",
                    DrugName = "阿托伐他汀钙片",
                    DrugImageSource = "5.高血脂的健康饮食习惯.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	常见不良反应有腹泻、关节痛、肌痛、尿道感染、四肢疼痛，同时该药可能会引起转氨酶升高，用药前建议您抽血检测，用药时如果出现相关症状建议您就诊咨询；\n-   如果出现肌痛、肌无力等肌肉症状，建议您就诊咨询，医生会抽血检查肌酸激酶进行评估，必要时（若超过正常上限10倍）会停药更换其它药物。",
                                DrugNotice = "-	健康的生活方式是药物治疗的基础，在用药期间请注意保持健康的饮食习惯（详细请点击下方相关阅读）；\n-   治疗期间，需定期抽血测血脂水平评估治疗效果； \n-   在服药期间请不要过量饮酒，也不要大量食用或饮用葡萄柚（西柚），因为它们会影响药效；\n-   该药有较明显的药物间相互作用，服用新的药物（包括非处方药和中草药）前请咨询医生或药师。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。\n[相关阅读]：高血脂患者的健康饮食习惯“5.高血脂的健康饮食习惯”",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6950641900181",
                    DrugName = "硫酸氢氯吡格雷片",
                    DrugImageSource = "1出血的表现 副本.JPG",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "请于饭前用适量水送服，请注意整片吞下，不要掰开、研碎或咀嚼服用。",
                                DrugUnwell = "胃肠道不适包括恶心、呕吐、胃肠道和腹部疼痛；\n该药会使出血的风险增加，因此你需要留意自己是否有出血的表现。如果出现的话，请及时告诉您的医生，他们会帮助您进行分析判断，并决定是否要调整治疗方案。",
                                DrugNotice = "如果您近期要进行拔牙或手术，请告知医生正在服用该药；\n服药时止血时间可能比平长。",
                                DrugStoreAdv = "请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜； ",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6924147659034",
                    DrugName = "阿司匹林肠溶片",
                    DrugImageSource = "1出血的表现 副本.JPG",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于饭前用适量水送服，注意整片吞下，不要掰开、研碎或咀嚼服用。",
                                DrugUnwell = "-	胃肠道不适包括恶心、呕吐、胃肠道和腹部疼痛；-   该药会使出血的风险增加，因此您需要留意自己是否有出血的表现（如鼻出血、眼底出血等，详情请点击下方相关阅读）。如果有，请及时告诉您的医生，他们会帮助您进行分析判断，并决定是否要调整治疗方案。 ",
                                DrugNotice = "-	如果您近期要进行拔牙或手术，请告知医生正在服用该药；-   服药时止血时间可能比平时长。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜； -   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6958703500157",
                    DrugName = "甲磺酸多沙唑嗪缓释片",
                    DrugImageSource = "2.体位性低血压的预防和应对.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请在睡前用适量水送服，注意整片吞下，不要掰开、研碎或咀嚼服用；\n-   如果您在大便中发现药片类似物，请不要担心，药物成分已被吸收，那只是不被吸收的药壳",
                                DrugUnwell = "-	常见不良反应包括眩晕、乏力、腹痛、恶心、背痛、体位性低血压等，如果频繁出现或不能耐受，请就医咨询；如何预防体位性低血压，请点击下方相关阅读。",
                                DrugNotice = "-	服药期间如果您出现头晕、乏力，这会导致反应能力下降，请不要驾车或操作机械。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6921361255288",
                    DrugName = "钙尔奇D",
                    DrugImageSource = "3.维生素D是补钙的好帮手.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请用适量水送服，如果药片太大难以吞咽，可酌情掰开服用；\n-   尽管服用时间无特殊要求，通常建议晚饭后服用。",
                                DrugUnwell = "-	可能发生的不良反应包括打饱嗝、腹胀、腹痛、便秘、腹泻、恶心和呕吐等胃肠不适。",
                                DrugNotice = "-	为避免抑制钙的吸收，服药前后请不要过量饮用含咖啡因的饮料、浓茶和酒，不要大量吸烟，以及不要大量进食富含纤维素的食物；\n-   该药同时含有钙和维生素D，维生素D有助于帮助人体吸收钙，详细请点击下方相关阅读；\n-   该药与其他药物同时服用可能会发生相互作用，服用前请咨询医师或药师。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6958703500010",
                    DrugName = "苯磺酸氨氯地平片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	常见的不良反应有水肿、面部潮红、心悸、嗜睡、头晕、腹痛；\n-   水肿常常发生于脚踝，如果您不能耐受请就诊咨询，医生会给您调整治疗方案。",
                                DrugNotice = "-	一些药物可能会影响该药的效果，如果您正在服用其它药物，请告知您的医生；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6951283550307",
                    DrugName = "氯沙坦片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	可能的不良反应包括头晕、头痛、高血钾等；",
                                DrugNotice = "-	请保持健康的饮食习惯，并进行适当的锻炼；\n-   用药前和用药期间需抽血检查血钾和肾功能，从而帮助调整剂量和药物选择；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读；\n-   如果您同时在服用保钾利尿药（如：螺内酯，氨苯蝶啶，阿米洛利）、补钾剂，或食用含钾的盐代用品，请告知医生，因为这些也可使血钾升高，医生可能不会建议您食用含钾盐代用品，若必须服用药物，您的抽血监测频率可能要更高，必要时可能会调整治疗方案； ",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6951283550000",
                    DrugName = "氯沙坦钾片",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	可能的不良反应包括头晕、头痛、高血钾等；",
                                DrugNotice = "-	请保持健康的饮食习惯，并进行适当的锻炼；\n-   用药前和用药期间需抽血检查血钾和肾功能，从而帮助调整剂量和药物选择；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读；\n-   如果您同时在服用保钾利尿药（如：螺内酯，氨苯蝶啶，阿米洛利）、补钾剂，或食用含钾的盐代用品，请告知医生，因为这些也可使血钾升高，医生可能不会建议您食用含钾盐代用品，若必须服用药物，您的抽血监测频率可能要更高，必要时可能会调整治疗方案； ",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6924147651014",
                    DrugName = "阿卡波糖片",
                    DrugImageSource = "6.低血糖反应及处理ver.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请餐前即刻用适量水吞服，或与前几口主食一起咀嚼服用；\n-   如果漏服药物，下一次用药维持原剂量，注意不要加倍。",
                                DrugUnwell = "-	常见不良反应包括胃肠胀气和肠鸣音，偶有腹泻和腹胀；\n-   低血糖是多数降糖药物常见的不良反应，尤其是两种或以上降糖药物联用时更容易出现。低血糖的识别和处理请点击下方相关阅读。",
                                DrugNotice = "-	为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   为确保用药安全，用药期间请您定期就诊，医生会抽血检查肾功能、血常规。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6902182392109",
                    DrugName = "盐酸二甲双胍片",
                    DrugImageSource = "7.低血糖反应及处理ver.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请在餐时用适量水送服；\n-   如果漏服药物，下一次用药维持原剂量不要加倍。",
                                DrugUnwell = "-	常见不良反应有恶心、呕吐、腹泻、消化不良、乏力及头痛；\n-   低血糖是多数降糖药物常见的不良反应，尤其是两种或以上降糖药物联用时更容易出现。低血糖的识别和处理请点击下方相关阅读。",
                                DrugNotice = "-	服药期间请不要饮酒；\n-   为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   如果您需要造影检查，请告知医生正在服用本药品，可能需要停药后再做检查；\n-   为确保用药安全，用药期间请您定期就诊，医生会抽血检查肾功能、血常规。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6902182393106",
                    DrugName = "盐酸二甲双胍片",
                    DrugImageSource = "7.低血糖反应及处理ver.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请在餐时用适量水送服；\n-   如果漏服药物，下一次用药维持原剂量不要加倍。",
                                DrugUnwell = "-	常见不良反应有恶心、呕吐、腹泻、消化不良、乏力及头痛；\n-   低血糖是多数降糖药物常见的不良反应，尤其是两种或以上降糖药物联用时更容易出现。低血糖的识别和处理请点击下方相关阅读。",
                                DrugNotice = "-	服药期间请不要饮酒；\n-   为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   如果您需要造影检查，请告知医生正在服用本药品，可能需要停药后再做检查；\n-   为确保用药安全，用药期间请您定期就诊，医生会抽血检查肾功能、血常规。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6923878310320",
                    DrugName = "琥珀酸美托洛尔缓释片",
                    DrugImageSource = "8.如何在家测量心率.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于早晨用半杯液体送服，白开水、牛奶等均可；\n-   需要服用半片时，可将药片掰开，但注意不要咀嚼或压碎。",
                                DrugUnwell = "-	可能的不良反应有疲乏、头晕、肢体冷感、心动过缓等；",
                                DrugNotice = "-	服药期间请注意每天监测心率（详细请点击下方链接），如果出现小于55次/分或心律不齐，请及时就诊，医生会根据您的情况调整剂量或药物；\n-   不要突然自行停药，要在医生指导下缓慢、逐渐减少药物剂量停药；\n-   如果您同时患有糖尿病，则更需要规律监测血糖。因为这类药物可以降低心率，从而掩盖低血糖的心率加快的症状。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6951283550390",
                    DrugName = "辛伐他汀片",
                    DrugImageSource = "5.高血脂的健康饮食习惯.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于临睡前用适量水送服。",
                                DrugUnwell = "-	常见不良反应包括便秘、恶心、腹痛、头痛、上呼吸道感染等；\n-   如果出现肌痛、肌无力等肌肉症状，建议您就诊咨询，医生会抽血检查肌酸激酶进行评估，必要时会停药更换其它药物。",
                                DrugNotice = "-	健康的生活方式是药物治疗的基础，在用药期间请注意保持健康的饮食习惯（详细请点击下方相关阅读）；\n-   在服药期间请不要过量饮酒，也不要大量食用或饮用葡萄柚（西柚），因为它们会影响药效；\n-   治疗期间，需定期抽血测血脂水平评估治疗效果； \n-   治疗前、调整剂量或与其它药物合用时，医生可能会抽血评估转氨酶和肌酸激酶用以确保用药安全；\n-   该药有较明显的药物间相互作用，服用新的药物（包括非处方药和中草药）前请咨询医生或药师。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜； \n-   如果室内温度过高，则可以放在空调房间或冰箱冷藏室（与放鸡蛋的同层）；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6951283550420",
                    DrugName = "辛伐他汀片",
                    DrugImageSource = "5.高血脂的健康饮食习惯.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于临睡前用适量水送服。",
                                DrugUnwell = "-	常见不良反应包括便秘、恶心、腹痛、头痛、上呼吸道感染等；\n-   如果出现肌痛、肌无力等肌肉症状，建议您就诊咨询，医生会抽血检查肌酸激酶进行评估，必要时会停药更换其它药物。",
                                DrugNotice = "-	健康的生活方式是药物治疗的基础，在用药期间请注意保持健康的饮食习惯（详细请点击下方相关阅读）；\n-   在服药期间请不要过量饮酒，也不要大量食用或饮用葡萄柚（西柚），因为它们会影响药效；\n-   治疗期间，需定期抽血测血脂水平评估治疗效果； \n-   治疗前、调整剂量或与其它药物合用时，医生可能会抽血评估转氨酶和肌酸激酶用以确保用药安全；\n-   该药有较明显的药物间相互作用，服用新的药物（包括非处方药和中草药）前请咨询医生或药师。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜； \n-   如果室内温度过高，则可以放在空调房间或冰箱冷藏室（与放鸡蛋的同层）；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6923878310122",
                    DrugName = "非洛地平缓释片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于早晨用适量水送服，注意整片吞下，不要掰开、研碎或咀嚼服用。",
                                DrugUnwell = "-	可能的不良反应包括头痛、面部潮红、心慌、下肢水肿等；\n-   水肿常常发生于脚踝，如果您不能耐受，请就诊咨询，医生会给您调整治疗方案；",
                                DrugNotice = "-	服药期间请不要吃葡萄柚（也称为西柚），因为它可能会影响药效；\n-   一些药物可能会影响该药的效果，如果您正在服用其它药物，请告知您的医生；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6923878310139",
                    DrugName = "非洛地平缓释片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于早晨用适量水送服，注意整片吞下，不要掰开、研碎或咀嚼服用。",
                                DrugUnwell = "-	可能的不良反应包括头痛、面部潮红、心慌、下肢水肿等；\n-   水肿常常发生于脚踝，如果您不能耐受，请就诊咨询，医生会给您调整治疗方案；",
                                DrugNotice = "-	服药期间请不要吃葡萄柚（也称为西柚），因为它可能会影响药效；\n-   一些药物可能会影响该药的效果，如果您正在服用其它药物，请告知您的医生；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6902182330101",
                    DrugName = "福辛普利钠片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请用适量水送服，用药时间不受食物影响。",
                                DrugUnwell = "-	可能的不良反应包括干咳、头晕、头痛、高血钾等，如果您的咳嗽无法忍受，请告诉医生以便更换其他降压药物。",
                                DrugNotice = "-	用药期间如果您出现头晕、乏力，这会导致反应能力下降，请不要驾车或操作机械。\n-   用药前和用药期间需抽血检查血钾和肾功能，从而帮助调整剂量和药物选择；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读；\n-   一些药物可能会影响该药的效果，如果您正在服用其它药物，请告知您的医生（NSAIDs）；\n-   如果您同时在服用保钾利尿药（如：螺内酯，氨苯蝶啶，阿米洛利）、补钾剂，或食用含钾的盐代用品，请告知医生，因为这些也可使血钾升高，医生可能不会建议您食用含钾的盐代用品，若必须服用药物，您的抽血监测频率可能要更高，必要时可能会调整治疗方案。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   如果室内温度过高，则可以放在空调房间或冰箱冷藏室（与放鸡蛋的同层）；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6931837200094",
                    DrugName = "富马酸比索洛尔片",
                    DrugImageSource = "8.如何在家测量心率.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于早晨用适量水送服。",
                                DrugUnwell = "-	可能的不良反应有疲乏、头晕、肢体冷感、心动过缓等。",
                                DrugNotice = "-	服药期间请注意每天监测心率（详细请点击下方相关阅读），如果出现小于55次/分或心律不齐，请及时就诊，医生会根据您的情况调整剂量或药物；\n-   不要突然自行停药，要在医生指导下缓慢、逐渐减少药物剂量停药；\n-   如果您同时患有糖尿病，则更需要规律监测血糖。因为这类药物可以降低心率，从而掩盖低血糖的心率加快的症状。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   如果室内温度过高，则可以放在空调房间或冰箱冷藏室（与放鸡蛋的同层）；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6939901400067",
                    DrugName = "替米沙坦片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	可能的不良反应包括头晕、头痛、高血钾等。",
                                DrugNotice = "-	请保持健康的饮食习惯，并进行适当的锻炼；\n-   用药前和用药期间需抽血检查血钾和肾功能，从而帮助调整剂量和药物选择；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读；\n-   如果您同时在服用保钾利尿药（如：螺内酯，氨苯蝶啶，阿米洛利）、补钾剂，或食用含钾的盐代用品，请告知医生，因为这些也可使血钾升高，医生可能不会建议您食用含钾盐代用品，若必须服用药物，您的抽血监测频率可能要更高，必要时可能会调整治疗方案。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6950641900143",
                    DrugName = "厄贝沙坦片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	可能的不良反应包括头晕、头痛、高血钾等。",
                                DrugNotice = "-	请保持健康的饮食习惯，并进行适当的锻炼；\n-   用药前和用药期间需抽血检查血钾和肾功能，从而帮助调整剂量和药物选择；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读；\n-   如果您同时在服用保钾利尿药（如：螺内酯，氨苯蝶啶，阿米洛利）、补钾剂，或食用含钾的盐代用品，请告知医生，因为这些也可使血钾升高，医生可能不会建议您食用含钾盐的代用品，若必须服用药物，您的抽血监测频率可能要更高，必要时可能会调整治疗方案。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6958703500515",
                    DrugName = "氨氯地平阿托伐他汀钙片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	可能的不良反应包括头痛、鼻咽炎、恶心、呕吐、肌痛、关节痛、下肢水肿等；\n-   水肿常常发生于脚踝，如果您不能耐受，请就诊咨询，医生会给您调整治疗方案；\n-   如果出现肌痛、肌无力等肌肉症状，建议您就诊咨询，医生会抽血检查肌酸激酶进行评估，必要时（若超过正常上限10倍）会停药更换其它药物。",
                                DrugNotice = "-	请保持健康的饮食习惯，并进行适当的锻炼；\n-   服药期间需要定期监测血压及血脂控制情况，医生会根据情况调整药物或剂量； \n-   在服药期间请不要过量饮酒，也不要大量食用或饮用葡萄柚（西柚），因为它们会影响药效；\n-   该药可能会引起转氨酶升高，用药前建议您抽血检测，用药时如果出现相关症状建议您就诊咨询。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6950641900129",
                    DrugName = "厄贝沙坦氢氯噻嗪片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	常见不良反应包括眩晕、疲劳、恶心、呕吐、肾功能异常等。",
                                DrugNotice = "-	服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读；\n-   服药期间需要定期监测电解质及肾功能，从而帮助调整剂量和药物选择。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6910853819589",
                    DrugName = "瑞舒伐他汀钙片",
                    DrugImageSource = "5.高血脂的健康饮食习惯.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	常见不良反应有腹痛、恶心、肌痛、头痛、虚弱；\n-   如果出现肌痛、肌无力等肌肉症状，建议您就诊咨询，医生会抽血检查肌酸激酶进行评估，必要时（若超过正常上限5倍）会停药更换其它药物。",
                                DrugNotice = "-	健康的生活方式是药物治疗的基础，在用药期间请注意保持健康的饮食习惯（详细请点击下方相关阅读）；\n-   治疗期间，需定期抽血测血脂水平评估治疗效果； \n-   在服药期间请不要过量饮酒，因为酒精会影响药效；\n-   治疗前、调整剂量或与其它药物合用时，医生可能会抽血评估转氨酶和肌酸激酶用以确保用药安全；\n-   该药有较明显的药物间相互作用，服用新的药物（包括非处方药和中草药）前请咨询医生或药师。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6924147654015",
                    DrugName = "硝苯地平控释片",
                    DrugImageSource = "4.家庭自测血压注意事项.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请每天固定同一时间（如早晨）用适量水吞服，不要掰开、研碎或咀嚼服用，服药时间不受食物影响；\n-   如果您在大便中发现药片类似物，请不要担心，药物成分已被吸收，那只是不被吸收的药壳。",
                                DrugUnwell = "-	  可能的不良反应有：头痛、面部潮红、心慌、下肢水肿等；\n-     水肿常发生于脚踝，如果您不能耐受，请就诊咨询，医生会给您调整治疗方案；",
                                DrugNotice = "-	服药期间请不要吃葡萄柚（也称为西柚），因为它可能会影响药效；\n-   一些药物可能会影响该药的效果，如果您正在服用其它药物，请告知您的医生；\n-   服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6935014100078",
                    DrugName = "格列喹酮片",
                    DrugImageSource = "7.低血糖反应及处理ver.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于饭前半小时用适量水送服，为避免发生低血糖，服药后请不要忘记吃饭；\n-   如果漏服药物，下一次用药维持原剂量，注意不要加倍。",
                                DrugUnwell = "-	极少数人有皮肤过敏反应、胃肠道反应、轻度低血糖反应及血液系统方面改变的报道； \n-   低血糖是多数降糖药物常见的不良反应，尤其是两种或以上降糖药物联用时更容易出现。低血糖的识别和处理请点击下方相关阅读。",
                                DrugNotice = "-	请注意控制饮食，并适当进行锻炼；\n-   为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   服药期间请不要饮酒；\n-   如果您对磺酰脲或磺胺类药物过敏，请告知医生或药师，可能会为您调整药物。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "",
                    DrugName = "磷酸西格列汀片",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	 请固定在每天的同一时间服用，服药时间不受食物影响。",
                                DrugUnwell = "-	可能发生的不良反应包括上呼吸道感染、鼻咽炎、头痛。",
                                DrugNotice = "-	请注意控制饮食，并适当进行锻炼；\n-   为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   为确保用药安全，用药期间请您定期就诊，医生会抽血检查肾功能。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
                new DrugInfo
                {
                    DrugID = "6900233882296",
                    DrugName = "盐酸吡格列酮片",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	 请固定在每天的同一时间服用，服药时间不受食物影响。",
                                DrugUnwell = "-	可能发生的不良反应包括肌痛、头痛、咽炎、鼻窦炎、上呼吸道感染。",
                                DrugNotice = "-	请注意控制饮食，并适当进行锻炼；\n-   为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   为确保用药安全，用药期间请您定期就诊，医生会抽血检查肝功能； \n-   该药可引起或加重一些糖尿病患者的心衰，如果您伴有心衰或用药期间出现体重快速增加、呼吸困难或水肿症状，请您及时就诊咨询。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },new DrugInfo
                {
                    DrugID = "69002338829999",
                    DrugName = "盐酸吡格列酮片",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	 请固定在每天的同一时间服用，服药时间不受食物影响。",
                                DrugUnwell = "-	可能发生的不良反应包括肌痛、头痛、咽炎、鼻窦炎、上呼吸道感染。",
                                DrugNotice = "-	请注意控制饮食，并适当进行锻炼；\n-   为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   为确保用药安全，用药期间请您定期就诊，医生会抽血检查肝功能； \n-   该药可引起或加重一些糖尿病患者的心衰，如果您伴有心衰或用药期间出现体重快速增加、呼吸困难或水肿症状，请您及时就诊咨询。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "",
                    DrugName = "格列齐特缓释片",
                    DrugImageSource = "7.低血糖反应及处理ver.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	 请于早餐时用适量水吞服，不要掰开、研碎或咀嚼服用；\n-   如果漏服药物，下一次用药维持原剂量，注意不要加倍。",
                                DrugUnwell = "-	可能发生的不良反应包括低血糖、腹痛、腹泻、恶心、呕吐、便秘；\n-   低血糖是多数降糖药物常见的不良反应，尤其是两种或以上降糖药物联用时更容易出现。低血糖的识别和处理请点击下方相关阅读。",
                                DrugNotice = "-	请注意控制饮食，并适当进行锻炼；\n-   为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   如果您对磺酰脲或磺胺类药物过敏，请告知医生或药师，可能会为您调整药物；\n-   为确保用药安全，轻中度肾功能不全者需定期就诊，医生会抽血检查肾功能。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6947544900059",
                    DrugName = "盐酸二甲双胍缓释胶囊",
                    DrugImageSource = "7.低血糖反应及处理ver.jpg",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请餐时或餐后用适量水吞服，不要打开、研碎或咀嚼服用；\n-   如果漏服药物，下一次用药维持原剂量不要加倍。",
                                DrugUnwell = "-	常见不良反应有恶心、呕吐、腹泻、消化不良、乏力及头痛；\n-   低血糖是多数降糖药物常见的不良反应，尤其是两种或以上降糖药物联用时更容易出现。低血糖的识别和处理请点击下方相关阅读。",
                                DrugNotice = "-	服药期间请不要饮酒；\n-   为评估降糖效果，您需定期监测血糖和/或糖化血红蛋白，必要时可能会调整剂量和药物品种；\n-   如果您需要造影检查，请告知医生正在服用本药品，可能需要停药后再做检查；\n-   为确保用药安全，用药期间请您定期就诊，医生会抽血检查肾功能、血常规。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },

                },
            };
        }

        public ScanDrug()
        {
            drugId = "";
            
            InitializingDrugInfo();
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                Window.GetWindow(this).TextInput += ScanDrug_TextInput;

                FindStoryboard();
            };
            this.Unloaded += (s, e) =>
            {

            };

            scrollviewer.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            zhuyiScrollviewer.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            storeScrollviewer.ManipulationBoundaryFeedback += (s, e) =>
            {
                e.Handled = true;
            };
            
            DoWork();
        }


        private void FindStoryboard()
        {
            scan1 = FindResource("Scan1") as Storyboard;
            scan2 = FindResource("Scan2") as Storyboard;
            scan3 = FindResource("Scan3") as Storyboard;

            scan5 = FindResource("Scan5") as Storyboard;

            scan1.Duration = TimeSpan.Parse("0:0:2.4");

            scan2.BeginTime = TimeSpan.FromSeconds(-3);
            scan2.Duration = TimeSpan.FromSeconds(7);

            scan3.BeginTime = TimeSpan.FromSeconds(-7);
            scan3.Duration = TimeSpan.FromSeconds(9);

            scan5.BeginTime = TimeSpan.FromSeconds(-14);
            scan5.Duration = TimeSpan.FromSeconds(20);

            scan1.Completed += (s, e) =>
            {
                scan2.Begin();
            };
            scan2.Completed += (s, e) =>
            {
                scan2.Begin();
            };
            scan3.Completed += (s, e) =>
            {
                scan5.Begin();
            };
            scan5.Completed += (s, e) =>
            {
                arrow.Visibility = Visibility.Visible;
                reading.Visibility = Visibility.Visible;
            };

            scan1.Begin();
            //scan2.Begin();
            //scan3.Begin();
            // scan5.Begin();
            // scan4.Begin();
        }

        private void ScanDrug_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "\r")
            {
                DoWork();
            }
            else
            {
                drugId += e.Text;
            }
        }


        private void DoWork()
        {
            if (state == Scan)
            {
                selectedDrug = null;

                var drug = drugInfo.FirstOrDefault(d => d.DrugID == drugId);
                
                if (drug != null)
                {
                    try
                    {
                        selectedDrug = (DrugInfo)drug;

                        textBlock1.Text = selectedDrug.DrugProperties.DrugUnwell;
                        textBlock1_Copy.Text = "";
                        
                        textBlock7.Text = selectedDrug.DrugProperties.DrugUseWay;

                        textBlock5.Text = selectedDrug.DrugProperties.DrugNotice;
                        textBlock1_Copy2.Text = "";

                        textBlock4.Text = selectedDrug.DrugProperties.DrugStoreAdv;
                        
                        String uriString;

                        uriString = "pack://application:,,,/Images/ScanDrug/";
                        uriString += selectedDrug.DrugID;
                        uriString += ".png";

                        if (!System.IO.File.Exists(uriString))
                        {
                            uriString = "pack://application:,,,/Images/ScanDrug/default_med.png";
                        }

                        BitmapImage bitmap = new BitmapImage();

                        bitmap.BeginInit();

                        bitmap.UriSource = new Uri(uriString, UriKind.Absolute);
                        bitmap.EndInit();

                        yao.Source = bitmap;


                        scan2.Stop();
                        scan3.Begin();
                        state++;
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show("ScanDrug.xaml DoWork Error "+ e);
                    }
                }
            }
            drugId = string.Empty;
        }

        //protected override void OnTouchDown(TouchEventArgs e)
        //{
        //    base.OnTouchDown(e);
        //    PointerDown();
        //}
        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            PointerDown();
            Type touchObject = e.OriginalSource.GetType();
            if (touchObject.Name == "TextBlock")
            {
                //if (e.TouchDevice.Target)
                //{

                //}

            }
            else if (touchObject.Name == "Button")
            {
                if (e.TouchDevice.Capture(this))
                {
                    e.Handled = true;
                }
            }
            
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            PointerDown();
        }

        private void PointerDown()
        {
            Debug.WriteLine("PointDown");
            //if (state == Scan)
            //{
            //    scan2.Stop();
            //    scan3.Begin();
            //    state++;
            //}
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard show = new Storyboard();

            DoubleAnimation d1 = new DoubleAnimation(1, TimeSpan.FromMilliseconds(300));
            show.Children.Add(d1);
            Storyboard.SetTarget(d1, pop);
            Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));

            ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
            ObjectKeyFrame k1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(0),
                Value = Visibility.Visible
            };
            d2.KeyFrames.Add(k1);
            show.Children.Add(d2);
            Storyboard.SetTarget(d2, pop);
            Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
            show.Begin();
            try
            {
                BitmapImage bitmap = new BitmapImage();

                bitmap.BeginInit();

                readingImageString = "pack://application:,,,/Images/ScanDrug/";
                readingImageString += selectedDrug.DrugImageSource;

                bitmap.UriSource = new Uri(readingImageString, UriKind.Absolute);

                bitmap.EndInit();
                readingImage.Source = bitmap;
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard show = new Storyboard();

            DoubleAnimation d1 = new DoubleAnimation(0, TimeSpan.FromMilliseconds(300));
            show.Children.Add(d1);
            Storyboard.SetTarget(d1, pop);
            Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));

            ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
            ObjectKeyFrame k1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(300),
                Value = Visibility.Collapsed
            };
            d2.KeyFrames.Add(k1);
            show.Children.Add(d2);
            Storyboard.SetTarget(d2, pop);
            Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
            show.Begin();
        }
    }



    class DrugInfo
    {
        public string DrugID { get; set; }
        public string DrugName { get; set; }
        public string DrugImageSource { get; set; }
        public DrugProperty DrugProperties { get; set; }

    }
    class DrugProperty
    {
        public string DrugUseWay { get; set; }
        public string DrugUnwell { get; set; }
        public string DrugNotice { get; set; }
        public string DrugStoreAdv { get; set; }
    }

}
