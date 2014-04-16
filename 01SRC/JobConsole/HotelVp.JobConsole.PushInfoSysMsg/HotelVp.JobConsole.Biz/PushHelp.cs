using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;
using System.IO;
using System.Data;
using System.Configuration;

using HotelVp.JobConsole.DataAccess;

namespace HotelVp.JobConsole.Biz
{
    public abstract class PushHelp
    {
        public static string logFile = "log.txt";
        public static string logFile1 = "queue.txt";

        public static void PusMsgHelping(DataSet dsPusMsgList, string taskID, string strContent)
        {
            int iMaxRows = int.Parse(ConfigurationManager.AppSettings["MaxRows"].ToString());
            bool bSleep = (dsPusMsgList.Tables[0].Rows.Count > iMaxRows) ? true : false;
            //Variables you may need to edit:
            //---------------------------------

            //True if you are using sandbox certificate, or false if using production
            bool sandbox = ("0".Equals(ConfigurationManager.AppSettings["Sandbox"].ToString())) ? true : false;

            //Put your device token in here
            //  string testDeviceToken = "bc9eb0ca6fedb7967ab7563a36fc197b90df3c99aae38db335cb152b7377fdb9";

            //Put your PKCS12 .p12 or .pfx filename here.
            // Assumes it is in the same directory as your app
            string p12File = GetAppValue("p12File");// "aps_production_identity.p12";

            //This is the password that you protected your p12File 
            //  If you did not use a password, set it as null or an empty string
            string p12FilePassword = GetAppValue("p12FilePassword");//"123456";

            //Number of notifications to send
            //int count = 1000;

            //Number of milliseconds to wait in between sending notifications in the loop
            // This is just to demonstrate that the APNS connection stays alive between messages
            

            //string numberFileName = GetAppValue("tokenFile");

            string content = strContent;// GetAppValue("content");

            //bool isTest = GetAppValue("isTest") == "true" ? true : false;

            //Actual Code starts below:
            //--------------------------------

            string p12Filename = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, p12File);

            //string numFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, numberFileName);

            //string[] numberList = ReadFile(numFile).Contains(',') ? ReadFile(numFile).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : new string[] { };

            //NotificationService service = new NotificationService(sandbox, p12Filename, p12FilePassword, 1, bSleep);
            NotificationService service = new NotificationService(sandbox, p12Filename, p12FilePassword, 1);

            service.SendRetries = 1; //5 retries before generating notificationfailed event
            service.ReconnectDelay = 5000; //5 seconds

            service.Error += new NotificationService.OnError(service_Error);
            service.NotificationTooLong += new NotificationService.OnNotificationTooLong(service_NotificationTooLong);

            service.BadDeviceToken += new NotificationService.OnBadDeviceToken(service_BadDeviceToken);
            service.NotificationFailed += new NotificationService.OnNotificationFailed(service_NotificationFailed);
            service.NotificationSuccess += new NotificationService.OnNotificationSuccess(service_NotificationSuccess);
            service.Connecting += new NotificationService.OnConnecting(service_Connecting);
            service.Connected += new NotificationService.OnConnected(service_Connected);
            service.Disconnected += new NotificationService.OnDisconnected(service_Disconnected);

            //The notifications will be sent like this:
            //		Testing: 1...
            //		Testing: 2...
            //		Testing: 3...
            // etc...
            //for (int i = 0; i < numberList.Length; i++)
            int sleepBetweenNotifications = (bSleep) ? Convert.ToInt32(GetAppValue("sleepBetweenNotificationsshot")) : Convert.ToInt32(GetAppValue("sleepBetweenNotifications")); //100;

            string strResult = string.Empty;
            for (int i = 0; i < dsPusMsgList.Tables[0].Rows.Count ; i++)
            {
                try
                {
                    if (!PushMsgDA.CheckPushPlanActionHistory(taskID, dsPusMsgList.Tables[0].Rows[i]["DEVICETOKEN"].ToString().Trim()))
                    {
                        continue;
                    }

                    //Create a new notification to send
                    Notification alertNotification = new Notification(dsPusMsgList.Tables[0].Rows[i]["DEVICETOKEN"].ToString().Trim());

                    alertNotification.Payload.Alert.Body = string.Format(content);
                    alertNotification.Payload.Sound = "default";
                    alertNotification.Payload.Badge = 1;

                    //Queue the notification to be sent
                    if (service.QueueNotification(alertNotification, bSleep))
                    {
                        strResult = "发送成功";
                        Console.WriteLine("Notification Queued!");
                        writeFile(logFile1, content + "Notification Queued!" + dsPusMsgList.Tables[0].Rows[i]["DEVICETOKEN"].ToString().Trim());
                    }
                    else
                    {
                        strResult = "发送失败";
                        Console.WriteLine("Notification Failed to be Queued!");
                        writeFile(logFile1, content + "Notification Failed to be Queued!" + dsPusMsgList.Tables[0].Rows[i]["DEVICETOKEN"].ToString().Trim());
                    }

                    PushMsgDA.InsertPushPlanActionHistory(taskID, dsPusMsgList.Tables[0].Rows[i]["TELPHONE"].ToString().Trim(), dsPusMsgList.Tables[0].Rows[i]["DEVICETOKEN"].ToString().Trim(), strResult);

                    //Sleep in between each message
                    //if (i < numberList.Length)
                    //{
                    Console.WriteLine("Sleeping " + sleepBetweenNotifications + " milliseconds before next Notification...");
                    System.Threading.Thread.Sleep(sleepBetweenNotifications);
                    //}
                }
                catch (Exception ex)
                {
                    //  Log King
                    CommonDA.InsertEventHistoryError(taskID, "Que Push发送Telphone： " + dsPusMsgList.Tables[0].Rows[i]["TELPHONE"].ToString().Trim() + "Que Push发送Devicetoken： " + dsPusMsgList.Tables[0].Rows[i]["DEVICETOKEN"].ToString().Trim() + "Que Push发送异常： " + ex.Message);
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }

            Console.WriteLine("Cleaning Up...");
            writeFile(logFile, "Cleaning Up...");
            //First, close the service.  
            //This ensures any queued notifications get sent befor the connections are closed
            service.Close();

            //Clean up
            service.Dispose();

            Console.WriteLine("Done!");
            writeFile(logFile, "Done");
            //Console.WriteLine("Press enter to exit...");
            //writeFile(logFile, "Press enter to exit...");
            //Console.ReadLine();
        }


        static string ReadFile(string fileName)
        {
            string res = string.Empty;
            if (File.Exists(fileName))
            {

                using (StreamReader sr = File.OpenText(fileName))
                {
                    res = sr.ReadToEnd();
                }
            }

            else
            {
                Console.WriteLine("File not found");
            }
            return res;
        }

        static void writeFile(string fileName, string message)
        {
            if ("0".Equals(ConfigurationManager.AppSettings["writeLog"].ToString()))
            {
                return;
            }

            if (!File.Exists(fileName))
            {
                FileStream newFile = File.Create(fileName);
                newFile.Close();
            }

            StreamWriter txtWriter = null;
            try
            {
                txtWriter = File.AppendText(fileName);
                txtWriter.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                txtWriter.WriteLine(", " + message);

            }
            catch (Exception ex)
            {
                txtWriter.WriteLine(ex.ToString());
            }
            finally
            {
                if (txtWriter != null)
                {
                    txtWriter.Flush();
                    txtWriter.Close();
                }
            }
        }


        public static string GetAppValue(string key)
        {
            return ConfigurationManager.AppSettings[key] == null ? string.Empty : ConfigurationManager.AppSettings[key];
        }


        static void service_BadDeviceToken(object sender, BadDeviceTokenException ex)
        {
            Console.WriteLine("Bad Device Token: {0}", ex.Message);
            writeFile(logFile, "Bad Device Token: " + ex.Message);
        }

        static void service_Disconnected(object sender)
        {
            Console.WriteLine("Disconnected...");
            // writeFile(logFile, "Disconnected... ");
        }

        static void service_Connected(object sender)
        {
            Console.WriteLine("Connected...");
            // writeFile(logFile, "Connected... ");
        }

        static void service_Connecting(object sender)
        {
            Console.WriteLine("Connecting...");
            //  writeFile(logFile, "Connecting... ");
        }

        static void service_NotificationTooLong(object sender, NotificationLengthException ex)
        {
            Console.WriteLine(string.Format("Notification Too Long: {0}", ex.Notification.ToString()));
            writeFile(logFile, string.Format("Notification Too Long: {0}", ex.Notification.ToString()));
        }

        static void service_NotificationSuccess(object sender, Notification notification)
        {
            Console.WriteLine(string.Format("Notification Success: {0}{1}", notification.ToString(), notification.DeviceToken));
            writeFile(logFile, string.Format("Notification Success: {0}{1}", notification.ToString(), notification.DeviceToken));
        }

        static void service_NotificationFailed(object sender, Notification notification)
        {
            Console.WriteLine(string.Format("Notification Failed: {0}{1}", notification.ToString(), notification.DeviceToken));
            writeFile(logFile, string.Format("Notification Failed: {0}{1}", notification.ToString(), notification.DeviceToken));
        }

        static void service_Error(object sender, Exception ex)
        {
            Console.WriteLine(string.Format("Error: {0}", ex.Message));
            writeFile(logFile, string.Format("Error: {0}", ex.Message));
        }
    }
}
