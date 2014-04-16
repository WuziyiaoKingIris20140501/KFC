using System;  
using System.Collections.Generic;  
using System.Text;  
using System.Web;  
using System.Web.SessionState;
using HotelVp.Common.DBUtility;

namespace HotelVp.Common.Logger
{
    /// <summary>
    /// 提供相同对象包含属性值得合并方法
    /// </summary>
    public class LogMessage : IRequiresSessionState  
    {
        public LogMessage() { }
        //public LogMessage(string computerName, string ipAddress, string event_Id, string conTent, string userID, string UserName)
        //{
        //    this.computername = computerName;
        //    this.ipaddress = ipAddress;
        //    this.event_id = event_Id;
        //    this.content = conTent;
        //    this.userid = userID;
        //    this.username = UserName;
        //}
        private string userid;
        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string computername;
        public string Computername
        {
            get { return computername; }
            set { computername = value; }
        }

        private string ipaddress;
        public string IpAddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }

        private string event_id;
        public string Event_id
        {
            get { return event_id; }
            set { event_id = value; }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private MessageType msgtype;

        public MessageType MsgType
        {
            get { return msgtype; }
            set { msgtype = value; }
        }
    }
}