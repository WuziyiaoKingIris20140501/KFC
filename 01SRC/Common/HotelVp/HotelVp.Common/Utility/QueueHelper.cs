using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Configuration;

using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System.Collections;


namespace HotelVp.Common.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class QueueHelper
    {
        private static string queueUrl = ConfigurationManager.AppSettings["MyQueueUrl"];
        private static string queueNm = ConfigurationManager.AppSettings["MyQueueNm"];

        public static void SendMessage(string Body)
        {
            try
            {
                //Create the Connection Factory
                IConnectionFactory factory = new ConnectionFactory(queueUrl);
                using (IConnection connection = factory.CreateConnection())
                {
                    //Create the Session  
                    using (ISession session = connection.CreateSession())
                    {
                        IMessageProducer prod = session.CreateProducer(
                            new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(queueNm));
                        ITextMessage msg = prod.CreateTextMessage();
                        msg.Text = Body;
                        msg.NMSDeliveryMode = MsgDeliveryMode.Persistent;
                        prod.Send(msg);
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public static ArrayList ReceiveMessage()
        {
            ArrayList alText = new ArrayList();
            try
            {
                //Create the Connection Factory  
                IConnectionFactory factory = new ConnectionFactory(queueUrl);
                using (IConnection connection = factory.CreateConnection())
                {
                    //Create the Session  
                    connection.Start();
                    using (ISession session = connection.CreateSession())
                    {
                        IDestination destination = session.GetQueue(queueNm);

                        using (IMessageConsumer consumer = session.CreateConsumer(destination))
                        {
                            IMessage advisory;
                            while ((advisory = consumer.Receive(TimeSpan.FromMilliseconds(2000))) != null)
                            {
                                ActiveMQTextMessage amqMsg = (ActiveMQTextMessage)advisory;
                                alText.Add(amqMsg.Text);
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return alText;
        }
    }
}