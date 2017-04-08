using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NOTest
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        #region API声明
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_StartFindIDCard (int iPort,  byte[] pucManaInfo,int iIfOpen);
        [DllImport("sdtapi.dll",CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_SelectIDCard (int iPort , byte[] pucManaMsg,int iIfOpen);
        [DllImport("sdtapi.dll",CallingConvention = CallingConvention.StdCall)]
        static extern int SDT_ReadBaseMsg (int iPort, byte[] pucCHMsg, ref UInt32 puiCHMsgLen, byte[] pucPHMsg,ref UInt32 puiPHMsgLen,int iIfOpen);
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            //变量声明
            byte[] CardPUCIIN = new byte[255];
            byte[] pucManaMsg = new byte[255];
            byte[] pucCHMsg = new byte[255];
            byte[] pucPHMsg = new byte[3024];
            UInt32 puiCHMsgLen=0;
            UInt32 puiPHMsgLen=0;
            int st =0;
            //读卡操作
            st= SDT_StartFindIDCard(1001, CardPUCIIN, 1);
            if (st != 0x9f) return;
            st = SDT_SelectIDCard(1001,  pucManaMsg, 1);
            if (st != 0x90) return;
            st = SDT_ReadBaseMsg(1001,   pucCHMsg, ref puiCHMsgLen,   pucPHMsg, ref puiPHMsgLen, 1);
            if (st != 0x90) return;
            //显示结果
            textBox1.Text = System.Text.ASCIIEncoding.Unicode.GetString(pucCHMsg);

        }
    }
}