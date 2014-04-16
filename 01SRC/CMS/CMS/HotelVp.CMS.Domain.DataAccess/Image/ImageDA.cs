using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

using HotelVp.Common;
using HotelVp.Common.DBUtility;
using HotelVp.Common.Utilities;
using HotelVp.Common.DataAccess;
using HotelVp.Common.Configuration;
using HotelVp.CMS.Domain.Entity;
//using HotelVp.CMS.Domain.Resource;

namespace HotelVp.CMS.Domain.DataAccess
{
    public abstract class ImageDA
    {
        public static DataTable GetImageByHotelID(ImageEntity ImageEntity)
        {
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("GetImageByHotelID");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            DataSet dsResult = cmd.ExecuteDataSet();
            return dsResult.Tables[0];
        }

        public static int InsertImage(ImageEntity ImageEntity)
        {
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("InsertImage");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID);
            cmd.SetParameterValue("@imgName", dbParm.Resolution);
            cmd.SetParameterValue("@extName", dbParm.Source);
            cmd.SetParameterValue("@imgPath", dbParm.HtpPath);
            cmd.SetParameterValue("@urlBak", dbParm.HtpPathBak);
            return cmd.ExecuteNonQuery();
        }

        public static int deleteImage(ImageEntity ImageEntity)
        {
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            DataCommand cmd = DataCommandManager.GetDataCommand("deleteImage");
            cmd.SetParameterValue("@HotelID", dbParm.HotelID); 
            return cmd.ExecuteNonQuery();
        }

        public static int CheckInsert(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("IMAGEID",OracleType.Int32),
                                    new OracleParameter("IMGTYPE",OracleType.VarChar),
                                    new OracleParameter("HTPPATH",OracleType.VarChar),
                                    new OracleParameter("SOURCE",OracleType.VarChar),
                                    new OracleParameter("ISCOVER",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    //new OracleParameter("ROOMTYPECODE",OracleType.VarChar),
                                    new OracleParameter("CREATETIME",OracleType.DateTime),
                                    new OracleParameter("SEQ",OracleType.VarChar),
                                    new OracleParameter("HTPPATHBAK",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            int TImageID = getMaxIDfromSeq("T_IMAGE_SEQ");
            parm[0].Value = TImageID;
            parm[1].Value = dbParm.ImgType;
            parm[2].Value = dbParm.HtpPath;
            parm[3].Value = dbParm.Source;
            parm[4].Value = dbParm.IsCover;
            parm[5].Value = dbParm.HotelID;
            //parm[5].Value = dbParm.RoomTypeCode;
            parm[6].Value = dbParm.CreateTime;
            parm[7].Value = dbParm.Seq;
            parm[8].Value = dbParm.HtpPathBak;
            //ImageEntity.QueryResult = HotelVp.Common.DBUtility.DbManager.Query("Image", "t_image_insert", false, parm);

            //if (ImageEntity.QueryResult.Tables.Count > 0 && ImageEntity.QueryResult.Tables[0].Rows.Count > 0)
            //{
            //    return TImageID;
            //}
            //return 0;
           int i = HotelVp.Common.DBUtility.DbManager.ExecuteSql("Image", "t_image_insert", parm);
           if (i == 1)
           {
               return TImageID;
           }
           else
           {
               return -1;
           }
        }

        public static int UpdateTRoomByHotelID(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("IMAGEID",OracleType.VarChar),
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCODE",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.Id;
            parm[1].Value = dbParm.HotelID;
            parm[2].Value = dbParm.RoomTypeCode;

            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("Image", "t_room_updateimageid_byhotelid", parm);
        }

        public static DataSet GetSupImageByHotelID(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.HotelID;

            return HotelVp.Common.DBUtility.DbManager.Query("Image", "t_image_select", true, parm);
        }

        public static int RenewSupCoverImageByID(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("ISCOVER",OracleType.VarChar),
                                    new OracleParameter("SEQ",OracleType.Int32),
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.IsCover;
            parm[1].Value = dbParm.Seq;
            parm[2].Value = dbParm.Id;

            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("Image", "t_image_updatecoverpic", parm);
        }

        public static int RenewSupImageByID(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={
                                    new OracleParameter("SEQ",OracleType.VarChar),
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.Seq;
            parm[1].Value = dbParm.Id;

            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("Image", "t_image_update", parm);
        }

        public static int DeleteSupImageByID(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={ 
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.Id;

            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("Image", "t_image_delete", parm);
        }

        public static DataSet GetSupImageByID(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={ 
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.Id;

            return HotelVp.Common.DBUtility.DbManager.Query("Image", "t_image_selectbyid", true, parm);
        }

        public static int UpdateSupImageDetailsByID(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={ 
                                    new OracleParameter("HOTELID",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.HotelID;

            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("Image", "t_image_updatedetails", parm);
        }

        public static int UpdateSupImageDetailsRepeatByID(ImageEntity ImageEntity)
        { 
            OracleParameter[] parm ={ 
                                    new OracleParameter("ISCOVER",OracleType.VarChar),
                                    new OracleParameter("IMGTYPE",OracleType.VarChar),
                                    //new OracleParameter("ROOMTYPECODE",OracleType.VarChar),
                                    new OracleParameter("ID",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.IsCover;
            parm[1].Value = dbParm.ImgType;
           // parm[2].Value = dbParm.RoomTypeCode;
            parm[2].Value = dbParm.Id;

            return HotelVp.Common.DBUtility.DbManager.ExecuteSql("Image", "t_image_updatedetails_repeat", parm);
        }

        public static DataTable SelectTRoomImageIDByHotelID(ImageEntity ImageEntity)
        {
            OracleParameter[] parm ={ 
                                    new OracleParameter("HOTELID",OracleType.VarChar),
                                    new OracleParameter("ROOMCODE",OracleType.VarChar)
                                };
            ImageDBEntity dbParm = (ImageEntity.ImageDBEntity.Count > 0) ? ImageEntity.ImageDBEntity[0] : new ImageDBEntity();
            parm[0].Value = dbParm.HotelID;
            parm[1].Value = dbParm.RoomTypeCode;

            return HotelVp.Common.DBUtility.DbManager.Query("Image", "t_room_selectimageid_byhotelid", false, parm).Tables[0];
        }

        //通过sequence查询得到下一个ID值,数据库为Oracle
        public static int getMaxIDfromSeq(string sequencename)
        {
            int seqID = 1;
            string sql = "select " + sequencename + ".nextval from dual";
            object obj = DbHelperOra.GetSingle(sql, false);
            if (obj != null)
            {
                seqID = Convert.ToInt32(obj);
            }
            return seqID;
        }
    }
}