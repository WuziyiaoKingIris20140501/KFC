using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;

namespace HotelVp.Common.DBUtility
{
    public abstract class XmlSqlAnalyze
    {
        public static string GotSqlTextFromXml(string SqlName, string SqlId)
        {
            string sqlString = String.Empty;

            string path = System.AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["SqlPath"].ToString() + SqlName + ConfigurationManager.AppSettings["SqlType"].ToString();
            XmlDocument doc = new XmlDocument();//创建一个新的xmldocumt 对象
            doc.Load(path);//加载xml文件
            string SqlTagName = ConfigurationManager.AppSettings["SqlTagName"].ToString();
            string SqlAttributes = ConfigurationManager.AppSettings["SqlAttributes"].ToString();
            XmlNodeList list = doc.DocumentElement.GetElementsByTagName(SqlTagName);
            foreach (XmlNode node in list)
            {
                if (node.Attributes[SqlAttributes].Value.Equals(SqlId))
                {
                    sqlString = node.InnerText;
                    break;
                }
            }
            return sqlString;
        }

        //public static object GotSqlParams(string SqlName, string SqlId, string[] cmdParms, DataBaseType dbType)
        //{
        //    if (dbType.Equals(DataBaseType.Oracle))
        //    {
        //        Hashtable paramsXml = GotSqlParamsFromXml(SqlName, SqlId);
        //        OracleParameter[] cmdParmOra = {};

        //        for(int i=0;i<cmdParms.Length-1;i++)
        //        {
        //            cmdParmOra[i] = new OracleParameter();

        //            cmdParmOra[i].ParameterName = "";
        //            cmdParmOra[i].OracleType = OracleType;
        //            cmdParmOra[i].Value = cmdParms[i];
        //        }

        //        return cmdParmOra;
        //    }

        //    SqlParameter[] cmdParmSql = { };
        //    return cmdParmSql;
        //}

        //private static Hashtable GotSqlParamsFromXml(string SqlName, string SqlId)
        //{

        //    Hashtable paramsList = new Hashtable();

        //    return paramsList;
        //}           
    }
}