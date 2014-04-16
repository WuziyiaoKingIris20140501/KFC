using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using HotelVp.CMS.Domain.DataAccess;
using HotelVp.Common;
using HotelVp.Common.Logger;
using HotelVp.Common.DBUtility;
using HotelVp.CMS.Domain.Entity;

namespace HotelVp.CMS.Domain.Process
{
    public class WebAutoCompleteBP
    {
        static string _nameSpaceClass = "HotelVp.CMS.Domain.Process.WebAutoCompleteBP  Method: ";
        
        public DataSet GetWebCompleteList(string TypeString, string ValueString,string cityName)
        {
            try
            {
                return WebAutoCompleteDA.GetWebCompleteList(TypeString, ValueString,cityName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}