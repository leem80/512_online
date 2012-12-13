using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;

namespace PROnline.Src
{
    public class SMS
    {

        [DllImport("sms.dll", EntryPoint = "Sms_Connection")]
        public static extern uint Sms_Connection(string CopyRight, uint Com_Port, uint Com_BaudRate, out string Mobile_Type, out string CopyRightToCOM);

        [DllImport("sms.dll", EntryPoint = "Sms_Disconnection")]
        public static extern uint Sms_Disconnection();

        [DllImport("sms.dll", EntryPoint = "Sms_Send")]
        public static extern uint Sms_Send(string Sms_TelNum, string Sms_Text);

        [DllImport("sms.dll", EntryPoint = "Sms_Receive")]
        public static extern uint Sms_Receive(string Sms_Type, out string Sms_Text);

        [DllImport("sms.dll", EntryPoint = "Sms_Delete")]
        public static extern uint Sms_Delete(string Sms_Index);

        [DllImport("sms.dll", EntryPoint = "Sms_AutoFlag")]
        public static extern uint Sms_AutoFlag();

        [DllImport("sms.dll", EntryPoint = "Sms_NewFlag")]
        public static extern uint Sms_NewFlag();

        public static int Connect()
        {
            String TypeStr = "";
            String CopyRightToCOM = "";
            String CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";
            uint comNum = (uint)MvcApplication.SMSCom;
            uint status = Sms_Connection(CopyRightStr, comNum, 9600, out TypeStr, out CopyRightToCOM);
            return (int)status;
        }

        public static string SendMessage(string sPhoneNum, string sContent)
        {
            try
            {
                if (MvcApplication.SMSStatus == 1)
                {
                    if(Sms_Send(sPhoneNum, sContent)==1){
                        return "发送成功";
                    }
                    else{
                        return "发送失败";
                    }
                }
                else
                {
                    return "短信猫未连接";
                }
            }
            catch
            {
                return "发送失败";
            }
        }
    }
}