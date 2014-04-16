using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace HotelVp.EBOK.Domain.API
{
    public class Setting
    {
        public static string Host
        {
            get
            {

                return ConfigurationManager.AppSettings["Host"];
            }
        }

        public static string SearchHost
        {
            get
            {

                return ConfigurationManager.AppSettings["SearchHost"];
            }
        }

        public static string ClientCodes  //客户端代码 如：HotelVP  
        {
            get
            {
                return ConfigurationManager.AppSettings["ClientCodes"];

            }
        }

        public static string ClientCode  //客户端代码 如：HotelVP  
        {
            get
            {
                return ConfigurationManager.AppSettings["ClientCode"];

            }
        }

        public static string ApiVersion  //客户端代码 如：HotelVP  
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiVersion"];

            }
        }

        public static string SignKey  //客户端代码 如：HotelVP  
        {
            get
            {
                return ConfigurationManager.AppSettings["SignKey"];

            }
        }


        public static string DefaultClientCode  //客户端代码 如：HotelVP  
        {
            get
            {
                return "HOTELVP";

            }
        }

        public static string UseCode
        {
            get
            {
                return "WAP";
            }
        }

        public static string UseCodeVersion
        {
            get
            {
                return "3.0";
            }
        }

        /// <summary>
        /// 0表示正常   1表示永远昨夜  2表示永远今夜
        /// </summary>
        public static string AppType
        {
            get
            {
                return ConfigurationManager.AppSettings["AppType"];
                //return 0;

            }
        }
    }
}
