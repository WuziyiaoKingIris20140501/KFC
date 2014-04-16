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
using HotelVp.CMS.Domain.Entity;


namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class FAQDA
    {
        public static FAQEntity BindFAQList(FAQEntity faqEntity)
        {
            faqEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("FAQInfo", "t_lm_qa_select", false);          
            return faqEntity;
        }
        
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="menuEntity"></param>
        /// <returns></returns>
        public static int Insert(FAQEntity faqEntity)
        {      

            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("QUSETION_HEAD",OracleType.VarChar),
                                    new OracleParameter("ANSWER_BODY",OracleType.VarChar),
                                    new OracleParameter("SEQ",OracleType.Int32) 
                              
                                                                 
                                };
            FAQDBEntity dbParm = (faqEntity.FAQDBEntity.Count > 0) ? faqEntity.FAQDBEntity[0] : new FAQDBEntity();

            parm[0].Value = dbParm.ID;
            if (String.IsNullOrEmpty(dbParm.QUSETION_HEAD))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.QUSETION_HEAD;
            }

            if (String.IsNullOrEmpty(dbParm.ANSWER_BODY))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.ANSWER_BODY;
            }
            parm[3].Value = dbParm.SEQ;

            int result = HotelVp.Common.DBUtility.DbManager.ExecuteSql("FAQInfo", "t_lm_qa_insert", parm);
            return result;

        }

        //修改菜单是否显示
        public static int UpdateFaqByID(FAQEntity faqEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Int32),
                                    new OracleParameter("QUSETION_HEAD",OracleType.VarChar),
                                    new OracleParameter("ANSWER_BODY",OracleType.VarChar)
                                };

            FAQDBEntity dbParm = (faqEntity.FAQDBEntity.Count > 0) ? faqEntity.FAQDBEntity[0] : new FAQDBEntity();
           
            parm[0].Value = dbParm.ID;
            if (String.IsNullOrEmpty(dbParm.QUSETION_HEAD))
            {
                parm[1].Value = DBNull.Value;
            }
            else
            {
                parm[1].Value = dbParm.QUSETION_HEAD;
            }

            if (String.IsNullOrEmpty(dbParm.ANSWER_BODY))
            {
                parm[2].Value = DBNull.Value;
            }
            else
            {
                parm[2].Value = dbParm.ANSWER_BODY;
            }          
           
            int result = HotelVp.Common.DBUtility.DbManager.ExecuteSql("FAQInfo", "t_lm_qa_update", parm);
            return result;

        }

        //删除
        public static int DeleteFaqByID(FAQEntity faqEntity)
        {
             OracleParameter[] parm ={
                                    new OracleParameter("ID",OracleType.Int32)                                                                   
                                };

            FAQDBEntity dbParm = (faqEntity.FAQDBEntity.Count > 0) ? faqEntity.FAQDBEntity[0] : new FAQDBEntity();
            parm[0].Value = dbParm.ID;
            int result = HotelVp.Common.DBUtility.DbManager.ExecuteSql("FAQInfo", "t_lm_qa_delete", parm);
            return result;           
        }
    }

    
}
