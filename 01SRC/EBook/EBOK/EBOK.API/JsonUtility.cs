using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace HotelVp.EBOK.Domain.API
{
    public class JsonUtility
    {

        public static T Deserialize<T>(string json)
        {

            T result = default(T);
            using (var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                result = (T)serializer.ReadObject(inputStream);
                return result;
            }
        }

        public static string Serialize(object obj)
        {


            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer s = new DataContractJsonSerializer(obj.GetType());
                s.WriteObject(ms, obj);
                byte[] json = ms.ToArray();
                return Encoding.UTF8.GetString(json, 0, json.Length);
            }

        }


    }
}
