using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CardReader
{
    public class IDCardReader : IDisposable
    {
        public event EventHandler CardRead;
        public IDCard CurrentCard;
        private SynchronizationContext mThreadContext;
        private bool mReading;

        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_StartFindIDCard(int iPort, byte[] pucManaInfo, int iIfOpen);
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_SelectIDCard(int iPort, byte[] pucManaMsg, int iIfOpen);
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_ReadBaseMsg(int iPort, byte[] pucCHMsg, ref UInt32 puiCHMsgLen, byte[] pucPHMsg, ref UInt32 puiPHMsgLen, int iIfOpen);
        [DllImport("WltRS.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int GetBmp(string pucPHMsg, int intf);

        public IDCardReader()
        {
            mThreadContext = SynchronizationContext.Current;
        }

        public  IDCard Read()
        {
            IDCard card = new IDCard();
            byte[] CardPUCIIN = new byte[255];
            byte[] pucManaMsg = new byte[255];
            byte[] pucCHMsg = new byte[255];
            byte[] pucPHMsg = new byte[3024];
            UInt32 puiCHMsgLen = 0;
            UInt32 puiPHMsgLen = 0;
            int st = 0;
            //读卡操作
            st = SDT_StartFindIDCard(1001, CardPUCIIN, 1);
            if (st != 0x9f) return null;
            st = SDT_SelectIDCard(1001, pucManaMsg, 1);
            if (st != 0x90) return null;
            st = SDT_ReadBaseMsg(1001, pucCHMsg, ref puiCHMsgLen, pucPHMsg, ref puiPHMsgLen, 1);
            if (st != 0x90) return null;
            int index = 0;
            card.Name = UnicodeEncoding.Unicode.GetString(pucCHMsg, 0, 30).Trim();
            index += 30;
            char sexCode = (char)pucCHMsg[30];
            card.Sex = sexCode == '1' ? "男" : (sexCode == '2' ? "女" : "其它");
            index += 2;
            card.Nationality = UnicodeEncoding.Unicode.GetString(pucCHMsg, index, 4).Trim();
            card.Nationality = nationalityDic[card.Nationality];
            index += 4;
            card.BirthDay = UnicodeEncoding.Unicode.GetString(pucCHMsg, index, 16).Trim();
            index += 16;
            card.Address = UnicodeEncoding.Unicode.GetString(pucCHMsg, index, 70).Trim();
            index += 70;
            card.IDNumber = UnicodeEncoding.Unicode.GetString(pucCHMsg, index, 36).Trim();
            index += 36;

            var wlt = card.Name + ".wlt";
            var bmp = card.Name + ".bmp";
            if(!File.Exists(bmp))
            {
                FileStream fs = File.OpenWrite(wlt);
                fs.Write(pucPHMsg, 0, (int)puiPHMsgLen);
                fs.Close();
                st = GetBmp(wlt, 2);

                if (st != 1)
                    return null;
            }

            card.HeadImage = ReadImage(bmp);
            return card;
        }

        private static BitmapSource ReadImage(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Relative);
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        public void Start()
        {
            mReading = true;
            Task.Factory.StartNew(() =>
            {
                IDCard card = null;
                while (card == null && mReading)
                {
                    card = Read();
                }
                if (card == null)
                    return;
                CurrentCard = card;
                mThreadContext.Post(delegate
                {
                    if (CardRead != null)
                    {
                        CardRead.Invoke(this, EventArgs.Empty);
                    }
                }, null);
            });
        }

        public void Dispose()
        {
            mReading = false;
        }

        static Dictionary<string, string> nationalityDic = new Dictionary<string, string>
        {
            { "01","汉"},
            { "02","蒙古"}

        };

    }
}
