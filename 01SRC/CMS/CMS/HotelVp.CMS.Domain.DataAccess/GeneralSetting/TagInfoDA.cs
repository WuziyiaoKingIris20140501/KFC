using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity.GeneralSetting;

namespace HotelVp.CMS.Domain.DataAccess.GeneralSetting
{
    public abstract class TagInfoDA
    {
        public static TagInfoEntity TagInfoSearch(TagInfoEntity tagInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TAGNAME",OracleType.VarChar)
                                };
            if (string.IsNullOrEmpty(tagInfoEntity.CityID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = tagInfoEntity.CityID;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.TagName))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = tagInfoEntity.TagName;
            }
            string SqlString = XmlSqlAnalyze.GotSqlTextFromXml("TagInfo", "t_tag_info_search");
            DataSet dsResult = DbManager.Query(SqlString, parm, (tagInfoEntity.PageCurrent - 1) * tagInfoEntity.PageSize, tagInfoEntity.PageSize, true);

            tagInfoEntity.QueryResult = dsResult;
            return tagInfoEntity;
        }

        public static TagInfoEntity TagInfoSearchCount(TagInfoEntity tagInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("TAGNAME",OracleType.VarChar)
                                };
            if (string.IsNullOrEmpty(tagInfoEntity.CityID))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = tagInfoEntity.CityID;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.TagName))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = tagInfoEntity.TagName;
            }

            DataSet dsResult = DbManager.Query("TagInfo", "t_tag_info_search_count", true, parm);
            tagInfoEntity.QueryResult = dsResult;
            return tagInfoEntity;
        }


        public static int TagInfoInsert(TagInfoEntity tagInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TAGNAME",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar),
                                    new OracleParameter("TYPEID",OracleType.VarChar),
                                    new OracleParameter("STATUS",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("CITYNAME",OracleType.VarChar),
                                    new OracleParameter("PINYINLONG",OracleType.VarChar),
                                    new OracleParameter("PINYINSHORT",OracleType.VarChar)
                                };
            if (string.IsNullOrEmpty(tagInfoEntity.TagName))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = tagInfoEntity.TagName;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.Longitude))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = tagInfoEntity.Longitude;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.Latitude))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = tagInfoEntity.Latitude;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.TypeID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = tagInfoEntity.TypeID;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.Status))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = tagInfoEntity.Status;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.CityID))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = tagInfoEntity.CityID;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.CityName))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = tagInfoEntity.CityName;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.PinyinLong))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = tagInfoEntity.PinyinLong;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.PinyinShort))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = tagInfoEntity.PinyinShort;
            }

            int i = DbManager.ExecuteSql("TagInfo", "t_tag_info_insert", parm);
            return i;
        }



        public static int TagInfoUpdate(TagInfoEntity tagInfoEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("TAGNAME",OracleType.VarChar),
                                    new OracleParameter("LONGITUDE",OracleType.VarChar),
                                    new OracleParameter("LATITUDE",OracleType.VarChar),
                                    new OracleParameter("TYPEID",OracleType.VarChar),
                                    new OracleParameter("STATUS",OracleType.VarChar),
                                    new OracleParameter("CITYID",OracleType.VarChar),
                                    new OracleParameter("CITYNAME",OracleType.VarChar),
                                    new OracleParameter("PINYINLONG",OracleType.VarChar),
                                    new OracleParameter("PINYINSHORT",OracleType.VarChar),
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            if (string.IsNullOrEmpty(tagInfoEntity.TagName))
            {
                parm[0].Value = DBNull.Value;
            }
            else
            {
                parm[0].Value = tagInfoEntity.TagName;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.Longitude))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = tagInfoEntity.Longitude;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.Latitude))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = tagInfoEntity.Latitude;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.TypeID))
            {
                parm[3].Value = DBNull.Value;
            }
            else
            {
                parm[3].Value = tagInfoEntity.TypeID;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.Status))
            {
                parm[4].Value = DBNull.Value;
            }
            else
            {
                parm[4].Value = tagInfoEntity.Status;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.CityID))
            {
                parm[5].Value = DBNull.Value;
            }
            else
            {
                parm[5].Value = tagInfoEntity.CityID;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.CityName))
            {
                parm[6].Value = DBNull.Value;
            }
            else
            {
                parm[6].Value = tagInfoEntity.CityName;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.PinyinLong))
            {
                parm[7].Value = DBNull.Value;
            }
            else
            {
                parm[7].Value = tagInfoEntity.PinyinLong;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.PinyinShort))
            {
                parm[8].Value = DBNull.Value;
            }
            else
            {
                parm[8].Value = tagInfoEntity.PinyinShort;
            }
            if (string.IsNullOrEmpty(tagInfoEntity.Id))
            {
                parm[9].Value = DBNull.Value;
            }
            else
            {
                parm[9].Value = tagInfoEntity.Id;
            }
            int i = DbManager.ExecuteSql("TagInfo", "t_tag_info_update", parm);
            return i;
        }
    }
}
