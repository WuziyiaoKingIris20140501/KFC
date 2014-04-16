using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net.Security;
using Newtonsoft.Json.Linq;
using System.Net;

namespace HotelVp.JobConsole.Biz
{
    class PushError
    {
    }
    public class BadDeviceTokenException : Exception
    {
        public BadDeviceTokenException(string deviceToken)
            : base(string.Format("Device Token Length ({0}) Is not the required length of {1} characters!", deviceToken.Length, Notification.DEVICE_TOKEN_STRING_SIZE))
        {
            this.DeviceToken = deviceToken;
        }

        public string DeviceToken
        {
            get;
            private set;
        }
    }

    public class Notification
    {
        public string DeviceToken { get; set; }
        public NotificationPayload Payload { get; set; }

        public const int DEVICE_TOKEN_BINARY_SIZE = 32;
        public const int DEVICE_TOKEN_STRING_SIZE = 64;
        public const int MAX_PAYLOAD_SIZE = 256;

        public Notification()
        {
            DeviceToken = string.Empty;
            Payload = new NotificationPayload();
        }

        public Notification(string deviceToken)
        {
            if (!string.IsNullOrEmpty(deviceToken) && deviceToken.Length != DEVICE_TOKEN_STRING_SIZE)
                throw new BadDeviceTokenException(deviceToken);

            DeviceToken = deviceToken;
            Payload = new NotificationPayload();
        }

        public Notification(string deviceToken, NotificationPayload payload)
        {
            if (!string.IsNullOrEmpty(deviceToken) && deviceToken.Length != DEVICE_TOKEN_STRING_SIZE)
                throw new BadDeviceTokenException(deviceToken);

            DeviceToken = deviceToken;
            Payload = payload;
        }

        /// <summary>
        /// Object for storing state.  This does not affect the actual notification!
        /// </summary>
        public object Tag
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Payload.ToJson();
        }


        public byte[] ToBytes()
        {
            byte[] deviceToken = new byte[DeviceToken.Length / 2];
            for (int i = 0; i < deviceToken.Length; i++)
                deviceToken[i] = byte.Parse(DeviceToken.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);

            if (deviceToken.Length != DEVICE_TOKEN_BINARY_SIZE)
                throw new BadDeviceTokenException(DeviceToken);


            byte[] deviceTokenSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt16(deviceToken.Length)));

            byte[] payload = Encoding.UTF8.GetBytes(Payload.ToJson());
            if (payload.Length > MAX_PAYLOAD_SIZE)
            {
                int newSize = Payload.Alert.Body.Length - (payload.Length - MAX_PAYLOAD_SIZE);
                if (newSize > 0)
                {
                    Payload.Alert.Body = Payload.Alert.Body.Substring(0, newSize);
                    payload = Encoding.UTF8.GetBytes(Payload.ToString());
                }
                else
                {
                    do
                    {
                        Payload.Alert.Body = Payload.Alert.Body.Remove(Payload.Alert.Body.Length - 1);
                        payload = Encoding.UTF8.GetBytes(Payload.ToString());
                    }
                    while (payload.Length > MAX_PAYLOAD_SIZE && !string.IsNullOrEmpty(Payload.Alert.Body));
                }

                if (payload.Length > MAX_PAYLOAD_SIZE)
                    throw new NotificationLengthException(this);
            }
            byte[] payloadSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt16(payload.Length)));

            int bufferSize = sizeof(Byte) + deviceTokenSize.Length + deviceToken.Length + payloadSize.Length + payload.Length;
            byte[] buffer = new byte[bufferSize];

            buffer[0] = 0x00;
            Buffer.BlockCopy(deviceTokenSize, 0, buffer, sizeof(Byte), deviceTokenSize.Length);
            Buffer.BlockCopy(deviceToken, 0, buffer, sizeof(Byte) + deviceTokenSize.Length, deviceToken.Length);
            Buffer.BlockCopy(payloadSize, 0, buffer, sizeof(Byte) + deviceTokenSize.Length + deviceToken.Length, payloadSize.Length);
            Buffer.BlockCopy(payload, 0, buffer, sizeof(Byte) + deviceTokenSize.Length + deviceToken.Length + payloadSize.Length, payload.Length);

            return buffer;
        }
    }

    public class NotificationAlert
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationAlert()
        {
            Body = null;
            ActionLocalizedKey = null;
            LocalizedKey = null;
            LocalizedArgs = new List<object>();
        }

        /// <summary>
        /// Body Text of the Notification's Alert
        /// </summary>
        public string Body
        {
            get;
            set;
        }

        /// <summary>
        /// Action Button's Localized Key
        /// </summary>
        public string ActionLocalizedKey
        {
            get;
            set;
        }

        /// <summary>
        /// Localized Key
        /// </summary>
        public string LocalizedKey
        {
            get;
            set;
        }

        /// <summary>
        /// Localized Argument List
        /// </summary>
        public List<object> LocalizedArgs
        {
            get;
            set;
        }

        public void AddLocalizedArgs(params object[] values)
        {
            this.LocalizedArgs.AddRange(values);
        }

        /// <summary>
        /// Determines if the Alert is empty and should be excluded from the Notification Payload
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (!string.IsNullOrEmpty(Body)
                    || !string.IsNullOrEmpty(ActionLocalizedKey)
                    || !string.IsNullOrEmpty(LocalizedKey)
                    || (LocalizedArgs != null && LocalizedArgs.Count > 0))
                    return false;
                else
                    return true;
            }
        }
    }

    public class NotificationConnection : IDisposable
    {
        #region Constants
        private const string hostSandbox = "gateway.sandbox.push.apple.com";
        private const string hostProduction = "gateway.push.apple.com";
        #endregion

        #region Delegates and Events
        /// <summary>
        /// Handles General Exceptions
        /// </summary>
        /// <param name="sender">NotificationConnection Instance that generated the Exception</param>
        /// <param name="ex">Exception Instance</param>
        public delegate void OnError(object sender, Exception ex);
        /// <summary>
        /// Occurs when a General Error is thrown
        /// </summary>
        public event OnError Error;

        /// <summary>
        /// Handles Notification Too Long Exceptions when a Notification's payload that is being sent is too long
        /// </summary>
        /// <param name="sender">NotificationConnection Instance that generated the Exception</param>
        /// <param name="ex">NotificationTooLongException Instance</param>
        public delegate void OnNotificationTooLong(object sender, NotificationLengthException ex);
        /// <summary>
        /// Occurs when a Notification that is being sent has a payload longer than the allowable limit of 256 bytes as per Apple's specifications
        /// </summary>
        public event OnNotificationTooLong NotificationTooLong;

        /// <summary>
        /// Handles Bad Device Token Exceptions when the device token provided is not the right length
        /// </summary>
        /// <param name="sender">NotificatioConnection Instance that generated the Exception</param>
        /// <param name="ex">BadDeviceTokenException Instance</param>
        public delegate void OnBadDeviceToken(object sender, BadDeviceTokenException ex);
        /// <summary>
        /// Occurs when a Device Token that's specified is not the right length
        /// </summary>
        public event OnBadDeviceToken BadDeviceToken;

        /// <summary>
        /// Handles Successful Notification Send Events
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        /// <param name="notification">Notification object that was Sent</param>
        public delegate void OnNotificationSuccess(object sender, Notification notification);
        /// <summary>
        /// Occurs when a Notification has been successfully sent to Apple's Servers
        /// </summary>
        public event OnNotificationSuccess NotificationSuccess;

        /// <summary>
        /// Handles Failed Notification Deliveries
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        /// <param name="failed">Notification object that failed to send</param>
        public delegate void OnNotificationFailed(object sender, Notification failed);
        /// <summary>
        /// Occurs when a Notification has failed to send to Apple's Servers.  This is event is raised after the NotificationConnection has attempted to resend the notification the number of SendRetries specified.
        /// </summary>
        public event OnNotificationFailed NotificationFailed;

        /// <summary>
        /// Handles Connecting Event
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        public delegate void OnConnecting(object sender);
        /// <summary>
        /// Occurs when Connecting to Apple's servers
        /// </summary>
        public event OnConnecting Connecting;

        /// <summary>
        /// Handles Connected Event
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        public delegate void OnConnected(object sender);
        /// <summary>
        /// Occurs when successfully connected and authenticated via SSL to Apple's Servers
        /// </summary>
        public event OnConnected Connected;

        /// <summary>
        /// Handles Disconnected Event
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        public delegate void OnDisconnected(object sender);
        /// <summary>
        /// Occurs when the connection to Apple's Servers has been lost
        /// </summary>
        public event OnDisconnected Disconnected;
        #endregion

        #region Instance Variables
        private bool disposing;
        private bool closing;
        private bool accepting;
        private bool connected;
        private bool firstConnect;

        private Encoding encoding;
        private ThreadSafeQueue<Notification> notifications;
        private Thread workerThread;
        private X509Certificate certificate;
        private X509CertificateCollection certificates;
        private TcpClient apnsClient;
        private SslStream apnsStream;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Push Notification Gateway Host</param>
        /// <param name="port">Push Notification Gateway Port</param>
        /// <param name="p12File">PKCS12 .p12 or .pfx File containing Public and Private Keys</param>
        public NotificationConnection(string host, int port, string p12File)
        {
            connected = false;
            firstConnect = true;

            Host = host;
            Port = port;

            start(p12File, null);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Push Notification Gateway Host</param>
        /// <param name="port">Push Notification Gateway Port</param>
        /// <param name="p12File">PKCS12 .p12 or .pfx File containing Public and Private Keys</param>
        /// <param name="p12FilePassword">Password protecting the p12File</param>
        public NotificationConnection(string host, int port, string p12File, string p12FilePassword)
        {
            connected = false;
            firstConnect = true;

            Host = host;
            Port = port;

            start(p12File, p12FilePassword);
        }

        public NotificationConnection(string host, int port, string p12File, string p12FilePassword, bool bSleep)
        {
            connected = false;
            firstConnect = true;

            Host = host;
            Port = port;
            BSleep = bSleep;
            start(p12File, p12FilePassword, bSleep);
        }
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sandbox">Boolean flag indicating whether the default Sandbox or Production Host and Port should be used</param>
        /// <param name="p12File">PKCS12 .p12 or .pfx File containing Public and Private Keys</param>
        public NotificationConnection(bool sandbox, string p12File)
        {
            Host = sandbox ? hostSandbox : hostProduction;
            Port = 2195;

            start(p12File, null);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sandbox">Boolean flag indicating whether the default Sandbox or Production Host and Port should be used</param>
        /// <param name="p12File">PKCS12 .p12 or .pfx File containing Public and Private Keys</param>
        /// <param name="p12FilePassword">Password protecting the p12File</param>
        public NotificationConnection(bool sandbox, string p12File, string p12FilePassword)
        {
            connected = false;
            firstConnect = true;

            Host = sandbox ? hostSandbox : hostProduction;
            Port = 2195;

            start(p12File, p12FilePassword);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Unique Identifier for this Instance
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or Sets the Number of Milliseconds to wait before Reconnecting to the Apns Host if the connection was lost or failed
        /// </summary>
        public int ReconnectDelay
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets the Number of times to try resending a Notification before the NotificationFailed event is raised
        /// </summary>
        public int SendRetries
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Push Notification Gateway Host
        /// </summary>
        public string Host
        {
            get;
            private set;
        }

        public bool BSleep
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Push Notification Gateway Port
        /// </summary>
        public int Port
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of notifications currently in the queue
        /// </summary>
        public int QueuedNotificationsCount
        {
            get { return notifications.Count; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Queue's a Notification to be Sent as soon as possible in a First in First out pattern
        /// </summary>
        /// <param name="notification">Notification object to send</param>
        /// <returns>If true, the notification was queued successfully, otherwise it was not and will not be sent</returns>
        public bool QueueNotification(Notification notification)
        {
            if (!disposing && !closing && accepting)
            {
                notifications.Enqueue(notification);
                return true;
            }

            return false;
        }

        public bool QueueNotification(Notification notification, bool bSleep)
        {
            if (!disposing && !closing && accepting)
            {
                if (bSleep)
                {
                    notifications.Enqueue(notification);
                }
                else
                {
                    notifications.EnqueueLog(notification);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Closes the Apns Connection but first waits for all Queued Notifications to be sent.  This will cause QueueNotification to always return false after this method is called.
        /// </summary>
        public void Close()
        {
            accepting = false;

            //Sleep here to prevent a race condition
            // in which a notification could be queued while the worker thread
            // is sleeping after its loop, but if we set closing true within that 100 ms,
            // the queued notifications during that time would not get dequeued as the loop
            // would exit due to closing = true;
            // 250 ms should be ample time for the loop to dequeue any remaining notifications
            // after we stopped accepting above
            Thread.Sleep(250);

            closing = true;

            //Wait for buffer to be flushed out
            if (workerThread != null && workerThread.IsAlive)
                workerThread.Join();
        }

        /// <summary>
        /// Closes the Apns Connections without waiting for Queued Notifications to be sent.  This will cause QueueNotification to always return false after this method is called.
        /// </summary>
        public void Dispose()
        {

            accepting = false;

            //We don't really care about the race condition here
            // since disposing does NOT wait for all notifications to be sent

            disposing = true;

            //Wait for the worker to finish cleanly
            if (workerThread != null && workerThread.IsAlive)
                workerThread.Join();


            try { apnsStream.Close(); }
            catch { }

            try { apnsStream.Dispose(); }
            catch { }

            try { apnsClient.Client.Shutdown(SocketShutdown.Both); }
            catch { }

            try { apnsClient.Client.Close(); }
            catch { }

            try { apnsClient.Close(); }
            catch { }
        }
        #endregion

        #region Private Methods
        private void start(string p12File, string p12FilePassword)
        {
            accepting = true;
            disposing = false;
            closing = false;

            encoding = Encoding.ASCII;
            notifications = new ThreadSafeQueue<Notification>();
            Id = System.Guid.NewGuid().ToString("N");
            ReconnectDelay = 3000; //3 seconds
            SendRetries = 3;

            //Need to load the private key seperately from apple
            if (string.IsNullOrEmpty(p12FilePassword))
                certificate = new X509Certificate2(System.IO.File.ReadAllBytes(p12File));
            else
                certificate = new X509Certificate2(System.IO.File.ReadAllBytes(p12File), p12FilePassword);

            certificates = new X509CertificateCollection();
            certificates.Add(certificate);

            workerThread = new Thread(new ThreadStart(workerMethod));
            workerThread.Start();
        }

        private void start(string p12File, string p12FilePassword, bool bSleep)
        {
            accepting = true;
            disposing = false;
            closing = false;

            encoding = Encoding.ASCII;
            notifications = new ThreadSafeQueue<Notification>();
            Id = System.Guid.NewGuid().ToString("N");
            ReconnectDelay = 3000; //3 seconds
            SendRetries = 3;

            //Need to load the private key seperately from apple
            if (string.IsNullOrEmpty(p12FilePassword))
                certificate = new X509Certificate2(System.IO.File.ReadAllBytes(p12File));
            else
                certificate = new X509Certificate2(System.IO.File.ReadAllBytes(p12File), p12FilePassword);

            certificates = new X509CertificateCollection();
            certificates.Add(certificate);

            //if (!bSleep)
            //{
            //    System.Threading.Thread.Sleep(10000);
            //}
            workerThread = new Thread(new ThreadStart(workerMethod));
            workerThread.Start();
        }

        private void workerMethod()
        {
            while (!disposing && !closing)
            {
                try
                {
                    while (this.notifications.Count > 0 && !disposing)
                    {
                        Notification notification = this.notifications.Dequeue();

                        int tries = 0;
                        bool sent = false;

                        while (!sent && tries < this.SendRetries)
                        {
                            try
                            {
                                if (!disposing)
                                {
                                    while (!connected)
                                        Reconnect();

                                    try
                                    {
                                        apnsStream.Write(notification.ToBytes());
                                    }
                                    catch (BadDeviceTokenException btex)
                                    {
                                        if (this.BadDeviceToken != null)
                                            this.BadDeviceToken(this, btex);
                                    }
                                    catch (NotificationLengthException nlex)
                                    {
                                        if (this.NotificationTooLong != null)
                                            this.NotificationTooLong(this, nlex);
                                    }

                                    string txtAlert = string.Empty;

                                    if (this.NotificationSuccess != null)
                                        this.NotificationSuccess(this, notification);

                                    sent = true;
                                }
                                else
                                {
                                    this.connected = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                if (this.Error != null)
                                    this.Error(this, ex);

                                this.connected = false;
                            }

                            tries++;
                        }

                        //Didn't send in 3 tries
                        if (!sent && this.NotificationFailed != null)
                            this.NotificationFailed(this, notification);
                    }
                }
                catch (Exception ex)
                {
                    if (this.Error != null)
                        this.Error(this, ex);

                    this.connected = false;
                }

                if (!disposing)
                    Thread.Sleep(500);
            }
        }




        private bool Reconnect()
        {
            if (!firstConnect)
            {
                for (int i = 0; i < this.ReconnectDelay; i += 100)
                    System.Threading.Thread.Sleep(100);
            }
            else
            {
                firstConnect = false;
            }


            if (apnsStream != null && apnsStream.CanWrite)
            {
                try { Disconnect(); }
                catch { }
            }

            if (apnsClient != null && apnsClient.Connected)
            {
                try { CloseSslStream(); }
                catch { }
            }

            if (Connect())
            {
                this.connected = OpenSslStream();

                return this.connected;
            }

            this.connected = false;

            return this.connected;
        }

        private bool Connect()
        {
            int connectionAttempts = 0;
            while (connectionAttempts < (this.SendRetries * 2) && (apnsClient == null || !apnsClient.Connected))
            {
                if (connectionAttempts > 0)
                    Thread.Sleep(this.ReconnectDelay);

                connectionAttempts++;

                try
                {
                    if (this.Connecting != null)
                        this.Connecting(this);

                    apnsClient = new TcpClient();
                    apnsClient.Connect(this.Host, this.Port);

                }
                catch (SocketException ex)
                {
                    if (this.Error != null)
                        this.Error(this, ex);

                    return false;
                }
            }
            if (connectionAttempts >= 3)
            {
                if (this.Error != null)
                    this.Error(this, new NotificationException(3, "Too many connection attempts"));

                return false;
            }

            return true;
        }

        private bool OpenSslStream()
        {
            apnsStream = new SslStream(apnsClient.GetStream(), false, new RemoteCertificateValidationCallback(validateServerCertificate), new LocalCertificateSelectionCallback(selectLocalCertificate));

            try
            {
                apnsStream.AuthenticateAsClient(this.Host, this.certificates, System.Security.Authentication.SslProtocols.Ssl3, false);
            }
            catch (System.Security.Authentication.AuthenticationException ex)
            {
                if (this.Error != null)
                    this.Error(this, ex);

                return false;
            }

            if (!apnsStream.IsMutuallyAuthenticated)
            {
                if (this.Error != null)
                    this.Error(this, new NotificationException(4, "Ssl Stream Failed to Authenticate"));

                return false;
            }

            if (!apnsStream.CanWrite)
            {
                if (this.Error != null)
                    this.Error(this, new NotificationException(5, "Ssl Stream is not Writable"));

                return false;
            }

            if (this.Connected != null)
                this.Connected(this);

            return true;
        }

        private void EnsureDisconnected()
        {
            if (apnsStream != null)
                CloseSslStream();
            if (apnsClient != null)
                Disconnect();
        }

        private void CloseSslStream()
        {
            try
            {
                apnsStream.Close();
                apnsStream.Dispose();
                apnsStream = null;
            }
            catch (Exception ex)
            {
                if (this.Error != null)
                    this.Error(this, ex);
            }

            if (this.Disconnected != null)
                this.Disconnected(this);
        }

        private void Disconnect()
        {
            try
            {
                apnsClient.Close();
            }
            catch (Exception ex)
            {
                if (this.Error != null)
                    this.Error(this, ex);
            }
        }

        private bool validateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true; // Dont care about server's cert
        }

        private X509Certificate selectLocalCertificate(object sender, string targetHost, X509CertificateCollection localCertificates,
            X509Certificate remoteCertificate, string[] acceptableIssuers)
        {
            return certificate;
        }
        #endregion
    }
    
    public class NotificationException : Exception
    {
        public NotificationException()
            : base()
        {
        }

        public NotificationException(int code, string message)
            : base(message)
        {
            this.Code = code;
        }

        public int Code
        {
            get;
            set;
        }
    }

    public class NotificationLengthException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notification">Notification that caused the Exception</param>
        public NotificationLengthException(Notification notification)
            : base(string.Format("Notification Payload Length ({0}) Exceeds the maximum length of {1} characters", notification.Payload.ToJson().Length, Notification.MAX_PAYLOAD_SIZE))
        {
            this.Notification = notification;
        }

        /// <summary>
        /// Notification that caused the Exception
        /// </summary>
        public Notification Notification
        {
            get;
            private set;
        }

    }

    public class NotificationPayload
    {
        public NotificationAlert Alert { get; set; }

        public int? Badge { get; set; }

        public string Sound { get; set; }

        public Dictionary<string, object[]> CustomItems
        {
            get;
            private set;
        }

        public NotificationPayload()
        {
            Alert = new NotificationAlert();
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string alert)
        {
            Alert = new NotificationAlert() { Body = alert };
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string alert, int badge)
        {
            Alert = new NotificationAlert() { Body = alert };
            Badge = badge;
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string alert, int badge, string sound)
        {
            Alert = new NotificationAlert() { Body = alert };
            Badge = badge;
            Sound = sound;
            CustomItems = new Dictionary<string, object[]>();
        }

        public void AddCustom(string key, params object[] values)
        {
            if (values != null)
                this.CustomItems.Add(key, values);
        }

        public string ToJson()
        {
            JObject json = new JObject();

            JObject aps = new JObject();

            if (!this.Alert.IsEmpty)
            {
                if (!string.IsNullOrEmpty(this.Alert.Body)
                    && string.IsNullOrEmpty(this.Alert.LocalizedKey)
                    && string.IsNullOrEmpty(this.Alert.ActionLocalizedKey)
                    && (this.Alert.LocalizedArgs == null || this.Alert.LocalizedArgs.Count <= 0))
                {
                    aps["alert"] = new JValue(this.Alert.Body);
                }
                else
                {
                    JObject jsonAlert = new JObject();

                    if (!string.IsNullOrEmpty(this.Alert.LocalizedKey))
                        jsonAlert["loc-key"] = new JValue(this.Alert.LocalizedKey);

                    if (this.Alert.LocalizedArgs != null && this.Alert.LocalizedArgs.Count > 0)
                        jsonAlert["loc-args"] = new JArray(this.Alert.LocalizedArgs.ToArray());

                    if (!string.IsNullOrEmpty(this.Alert.Body))
                        jsonAlert["body"] = new JValue(this.Alert.Body);

                    if (!string.IsNullOrEmpty(this.Alert.ActionLocalizedKey))
                        jsonAlert["action-loc-key"] = new JValue(this.Alert.ActionLocalizedKey);

                    aps["alert"] = jsonAlert;
                }
            }

            if (this.Badge.HasValue)
                aps["badge"] = new JValue(this.Badge.Value);

            if (!string.IsNullOrEmpty(this.Sound))
                aps["sound"] = new JValue(this.Sound);


            json["aps"] = aps;

            foreach (string key in this.CustomItems.Keys)
            {
                if (this.CustomItems[key].Length == 1)
                    json[key] = new JValue(this.CustomItems[key][0]);
                else if (this.CustomItems[key].Length > 1)
                    json[key] = new JArray(this.CustomItems[key]);
            }

            string rawString = json.ToString(Newtonsoft.Json.Formatting.None, null);

            StringBuilder encodedString = new StringBuilder();
            foreach (char c in rawString)
            {
                if ((int)c < 32 || (int)c > 127)
                    encodedString.Append("\\u" + String.Format("{0:x4}", Convert.ToUInt32(c)));
                else
                    encodedString.Append(c);
            }
            return rawString;// encodedString.ToString();
        }

        public override string ToString()
        {
            return ToJson();
        }
    }

    public class NotificationService : IDisposable
    {
        #region Constants
        private const string hostSandbox = "gateway.sandbox.push.apple.com";
        private const string hostProduction = "gateway.push.apple.com";
        #endregion

        #region Delegates and Events
        /// <summary>
        /// Handles General Exceptions
        /// </summary>
        /// <param name="sender">NotificationConnection Instance that generated the Exception</param>
        /// <param name="ex">Exception Instance</param>
        public delegate void OnError(object sender, Exception ex);
        /// <summary>
        /// Occurs when a General Error is thrown
        /// </summary>
        public event OnError Error;

        /// <summary>
        /// Handles Notification Too Long Exceptions when a Notification's payload that is being sent is too long
        /// </summary>
        /// <param name="sender">NotificationConnection Instance that generated the Exception</param>
        /// <param name="ex">NotificationTooLongException Instance</param>
        public delegate void OnNotificationTooLong(object sender, NotificationLengthException ex);
        /// <summary>
        /// Occurs when a Notification that is being sent has a payload longer than the allowable limit of 256 bytes as per Apple's specifications
        /// </summary>
        public event OnNotificationTooLong NotificationTooLong;

        /// <summary>
        /// Handles Bad Device Token Exceptions when the device token provided is not the right length
        /// </summary>
        /// <param name="sender">NotificatioConnection Instance that generated the Exception</param>
        /// <param name="ex">BadDeviceTokenException Instance</param>
        public delegate void OnBadDeviceToken(object sender, BadDeviceTokenException ex);
        /// <summary>
        /// Occurs when a Device Token that's specified is not the right length
        /// </summary>
        public event OnBadDeviceToken BadDeviceToken;

        /// <summary>
        /// Handles Successful Notification Send Events
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        /// <param name="notification">Notification object that was Sent</param>
        public delegate void OnNotificationSuccess(object sender, Notification notification);
        /// <summary>
        /// Occurs when a Notification has been successfully sent to Apple's Servers
        /// </summary>
        public event OnNotificationSuccess NotificationSuccess;

        /// <summary>
        /// Handles Failed Notification Deliveries
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        /// <param name="failed">Notification object that failed to send</param>
        public delegate void OnNotificationFailed(object sender, Notification failed);
        /// <summary>
        /// Occurs when a Notification has failed to send to Apple's Servers.  This is event is raised after the NotificationConnection has attempted to resend the notification the number of SendRetries specified.
        /// </summary>
        public event OnNotificationFailed NotificationFailed;

        /// <summary>
        /// Handles Connecting Event
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        public delegate void OnConnecting(object sender);
        /// <summary>
        /// Occurs when Connecting to Apple's servers
        /// </summary>
        public event OnConnecting Connecting;

        /// <summary>
        /// Handles Connected Event
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        public delegate void OnConnected(object sender);
        /// <summary>
        /// Occurs when successfully connected and authenticated via SSL to Apple's Servers
        /// </summary>
        public event OnConnected Connected;

        /// <summary>
        /// Handles Disconnected Event
        /// </summary>
        /// <param name="sender">NotificationConnection Instance</param>
        public delegate void OnDisconnected(object sender);
        /// <summary>
        /// Occurs when the connection to Apple's Servers has been lost
        /// </summary>
        public event OnDisconnected Disconnected;
        #endregion

        #region Instance Variables
        private List<NotificationConnection> notificationConnections = new List<NotificationConnection>();
        private Random rand = new Random((int)DateTime.Now.Ticks);
        private int sequential = 0;
        private int reconnectDelay = 5000;
        private int sendRetries = 1;

        private bool closing = false;
        private bool disposing = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Push Notification Gateway Host</param>
        /// <param name="port">Push Notification Gateway Port</param>
        /// <param name="p12File">PKCS12 .p12 or .pfx File containing Public and Private Keys</param>
        /// <param name="connections">Number of Apns Connections to start with</param>
        public NotificationService(string host, int port, string p12File, int connections)
        {
            this.SendRetries = 1;
            closing = false;
            disposing = false;
            Host = host;
            Port = port;
            P12File = p12File;
            P12FilePassword = null;
            DistributionType = NotificationServiceDistributionType.Sequential;
            Connections = connections;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Push Notification Gateway Host</param>
        /// <param name="port">Push Notification Gateway Port</param>
        /// <param name="p12File">PKCS12 .p12 or .pfx File containing Public and Private Keys</param>
        /// <param name="p12FilePassword">Password protecting the p12File</param>
        /// <param name="connections">Number of Apns Connections to start with</param>
        public NotificationService(string host, int port, string p12File, string p12FilePassword, int connections)
        {
            this.SendRetries = 1;
            closing = false;
            disposing = false;
            Host = host;
            Port = port;
            P12File = p12File;
            P12FilePassword = p12FilePassword;
            DistributionType = NotificationServiceDistributionType.Sequential;
            Connections = connections;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sandbox">Boolean flag indicating whether the default Sandbox or Production Host and Port should be used</param>
        /// <param name="p12File">PKCS12 .p12 or .pfx File containing Public and Private Keys</param>
        /// <param name="connections">Number of Apns Connections to start with</param>
        public NotificationService(bool sandbox, string p12File, int connections)
        {
            this.SendRetries = 1;
            closing = false;
            disposing = false;
            Host = sandbox ? hostSandbox : hostProduction;
            Port = 2195;
            P12File = p12File;
            P12FilePassword = null;
            DistributionType = NotificationServiceDistributionType.Sequential;
            Connections = connections;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sandbox">Boolean flag indicating whether the default Sandbox or Production Host and Port should be used</param>
        /// <param name="p12File">PKCS12 .p12 or .pfx File containing Public and Private Keys</param>
        /// <param name="p12FilePassword">Password protecting the p12File</param>
        /// <param name="connections">Number of Apns Connections to start with</param>
        public NotificationService(bool sandbox, string p12File, string p12FilePassword, int connections)
        {
            this.SendRetries = 1;
            closing = false;
            disposing = false;
            Host = sandbox ? hostSandbox : hostProduction;
            Port = 2195;
            P12File = p12File;
            P12FilePassword = p12FilePassword;
            DistributionType = NotificationServiceDistributionType.Sequential;
            Connections = connections;
        }

        public NotificationService(bool sandbox, string p12File, string p12FilePassword, int connections, bool bSleep)
        {
            this.SendRetries = 1;
            closing = false;
            disposing = false;
            Host = sandbox ? hostSandbox : hostProduction;
            Port = 2195;
            P12File = p12File;
            P12FilePassword = p12FilePassword;
            DistributionType = NotificationServiceDistributionType.Sequential;
            BSleep = bSleep;
            Connections = connections;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Unique Identifier for this Instance
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or Sets the Number of Milliseconds to wait before Reconnecting to the Apns Host if the connection was lost or failed
        /// </summary>
        public int ReconnectDelay
        {
            get { return reconnectDelay; }
            set
            {
                reconnectDelay = value;

                foreach (NotificationConnection con in notificationConnections)
                    con.ReconnectDelay = reconnectDelay;
            }
        }

        /// <summary>
        /// Gets or Sets the Number of times to try resending a Notification before the NotificationFailed event is raised
        /// </summary>
        public int SendRetries
        {
            get { return sendRetries; }
            set
            {
                sendRetries = value;

                foreach (NotificationConnection con in notificationConnections)
                    con.SendRetries = sendRetries;
            }
        }

        /// <summary>
        /// Gets or Sets the method used to distribute Queued Notifications over all open Apns Connections
        /// </summary>
        public NotificationServiceDistributionType DistributionType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the PKCS12 .p12 or .pfx File being used
        /// </summary>
        public string P12File
        {
            get;
            private set;
        }

        private string P12FilePassword;

        /// <summary>
        /// Gets the Push Notification Gateway Host
        /// </summary>
        public string Host
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Push Notification Gateway Port
        /// </summary>
        public int Port
        {
            get;
            private set;
        }

        public bool BSleep
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or Sets the number of Apns Connections in use.  Changing this property will dynamically change the number of connections in use.  If it is decreased, connections will be Closed (waiting for the Queue to be Emptied first), or if raised, new connections will be added.
        /// </summary>
        public int Connections
        {
            get
            {
                return notificationConnections.Count;
            }
            set
            {
                //Don't want 0 connections or less
                if (value <= 0)
                    return;

                //Get the delta
                int difference = value - notificationConnections.Count;

                if (difference > 0)
                {
                    //Need to add connections
                    for (int i = 0; i < difference; i++)
                    {
                        NotificationConnection newCon = new NotificationConnection(Host, Port, P12File, P12FilePassword, BSleep);
                        newCon.SendRetries = SendRetries;
                        newCon.ReconnectDelay = ReconnectDelay;

                        newCon.Error += new NotificationConnection.OnError(newCon_Error);
                        newCon.NotificationFailed += new NotificationConnection.OnNotificationFailed(newCon_NotificationFailed);
                        newCon.NotificationTooLong += new NotificationConnection.OnNotificationTooLong(newCon_NotificationTooLong);
                        newCon.NotificationSuccess += new NotificationConnection.OnNotificationSuccess(newCon_NotificationSuccess);
                        newCon.Connecting += new NotificationConnection.OnConnecting(newCon_Connecting);
                        newCon.Connected += new NotificationConnection.OnConnected(newCon_Connected);
                        newCon.Disconnected += new NotificationConnection.OnDisconnected(newCon_Disconnected);
                        newCon.BadDeviceToken += new NotificationConnection.OnBadDeviceToken(newCon_BadDeviceToken);
                        notificationConnections.Add(newCon);
                    }

                }
                else if (difference < 0)
                {
                    //Need to remove connections
                    for (int i = 0; i < difference * -1; i++)
                    {
                        if (notificationConnections.Count > 0)
                        {
                            NotificationConnection toClose = notificationConnections[0];
                            notificationConnections.RemoveAt(0);

                            toClose.Close();
                            toClose.Dispose();
                            toClose = null;
                        }
                    }
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Queues a Notification to one of the Apns Connections using the DistributionType specified by the property.
        /// </summary>
        /// <param name="notification">Notification object to send</param>
        /// <returns>If true, the Notification has been successfully queued</returns>
        public bool QueueNotification(Notification notification)
        {
            bool queued = false;

            if (!disposing && !closing)
            {
                int tries = 0;

                while (tries < SendRetries && !queued)
                {
                    if (DistributionType == NotificationServiceDistributionType.Sequential)
                        queued = queueSequential(notification);
                    else if (DistributionType == NotificationServiceDistributionType.Random)
                        queued = queueRandom(notification);

                    tries++;
                }
            }

            return queued;
        }

        public bool QueueNotification(Notification notification, bool bSleep)
        {
            bool queued = false;

            if (!disposing && !closing)
            {
                int tries = 0;

                while (tries < SendRetries && !queued)
                {
                    if (DistributionType == NotificationServiceDistributionType.Sequential)
                        queued = queueSequential(notification, bSleep);
                    else if (DistributionType == NotificationServiceDistributionType.Random)
                        queued = queueRandom(notification);

                    tries++;
                }
            }

            return queued;
        }

        /// <summary>
        /// Closes all of the Apns Connections but first waits for all Queued Notifications on each Apns Connection to be sent.  This will cause QueueNotification to always return false after this method is called.
        /// </summary>
        public void Close()
        {
            closing = true;

            foreach (NotificationConnection con in notificationConnections)
                con.Close();
        }

        /// <summary>
        /// Closes all of the Apns Connections without waiting for Queued Notifications on each Apns Connection to be sent.  This will cause QueueNotification to always return false after this method is called.
        /// </summary>
        public void Dispose()
        {
            disposing = true;

            foreach (NotificationConnection con in notificationConnections)
                con.Dispose();
        }
        #endregion

        #region Private Methods
        void newCon_NotificationSuccess(object sender, Notification notification)
        {
            if (this.NotificationSuccess != null)
                this.NotificationSuccess(sender, notification);
        }

        void newCon_NotificationTooLong(object sender, NotificationLengthException ex)
        {
            if (this.NotificationTooLong != null)
                this.NotificationTooLong(sender, ex);
        }

        void newCon_BadDeviceToken(object sender, BadDeviceTokenException ex)
        {
            if (this.BadDeviceToken != null)
                this.BadDeviceToken(this, ex);
        }

        void newCon_NotificationFailed(object sender, Notification failed)
        {
            if (this.NotificationFailed != null)
                this.NotificationFailed(sender, failed);
        }

        void newCon_Error(object sender, Exception ex)
        {
            if (this.Error != null)
                this.Error(sender, ex);
        }

        void newCon_Disconnected(object sender)
        {
            if (this.Disconnected != null)
                this.Disconnected(sender);
        }

        void newCon_Connected(object sender)
        {
            if (this.Connected != null)
                this.Connected(sender);
        }

        void newCon_Connecting(object sender)
        {
            if (this.Connecting != null)
                this.Connecting(sender);
        }

        private bool queueSequential(Notification notification)
        {
            if (notificationConnections.Count <= sequential && sequential > 0)
                sequential = 0;

            if (notificationConnections[sequential] != null)
                return notificationConnections[sequential].QueueNotification(notification);

            return false;
        }

        private bool queueSequential(Notification notification, bool bSleep)
        {
            if (notificationConnections.Count <= sequential && sequential > 0)
                sequential = 0;

            if (notificationConnections[sequential] != null)
                return notificationConnections[sequential].QueueNotification(notification, bSleep);

            return false;
        }

        private bool queueRandom(Notification notification)
        {
            int index = rand.Next(0, notificationConnections.Count - 1);

            if (notificationConnections[index] != null)
                return notificationConnections[index].QueueNotification(notification);

            return false;
        }
        #endregion
    }


    public enum NotificationServiceDistributionType
    {
        /// <summary>
        /// Loops through all connections in sequential order to ensure completely even distribution
        /// </summary>
        Sequential,
        /// <summary>
        /// Randomly chooses a connection to use
        /// </summary>
        Random
    }


    public class ThreadSafeQueue<T>
    {
        /// <summary>
        /// Gets the Push Notification Gateway Host
        /// </summary>
        //public bool bSleep = true;
        //public bool BSleep
        //{
        //    get { return bSleep; }
        //    set { bSleep = value; }
        //}

        public ThreadSafeQueue()
        {
            queue = new Queue<T>();
            lockObj = new object();
        }

        Queue<T> queue;
        object lockObj;

        public T Dequeue()
        {
            T result = default(T);

            //if (!bSleep)
            //{
            //    System.Threading.Thread.Sleep(5000);
            //}
            
            lock (lockObj)
            {
                //System.Threading.Thread.Sleep(10000);
                result = queue.Dequeue();
            }

            return result;
        }

        public void Enqueue(T item)
        {
            lock (lockObj)
            {
                queue.Enqueue(item);
                //System.Threading.Thread.Sleep(5000);
            }
        }

        public void EnqueueLog(T item)
        {
            lock (lockObj)
            {
                queue.Enqueue(item);
                System.Threading.Thread.Sleep(1000);
            }
        }

        public int Count
        {
            get { return queue.Count; }
        }

    }
}
