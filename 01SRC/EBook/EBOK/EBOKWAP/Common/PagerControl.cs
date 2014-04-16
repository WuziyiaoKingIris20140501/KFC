using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Xml;
using HotelVp.Common.DBUtility;
using HotelVp.Common.DataAccess;
using System.Text.RegularExpressions;

public static class PagerControl
{
    public static string Pagers(int cur, string attr, double rowcount, int pageSize, int split = 2)
    {
        //<li class="prev disabled"><a href="#">← Previous</a></li>
        //<li class="active"><a href="#">1</a></li>
        //<li><a href="#">2</a></li>
        //<li><a href="#">3</a></li>
        //<li><a href="#">4</a></li>
        //<li><a href="#">5</a></li>
        //<li class="next"><a href="#">Next → </a></li>

        int total = GetPageNum(rowcount, pageSize);
        if (total < 1)
            return "";

        var pageBuilder = new StringBuilder();
        pageBuilder.Append("<div style=\"width:100%;text-align:right;\">");

        int startRow = 0;
        int endRow = 0;

        if (total == 1)
        {
            startRow = 1;
            endRow = int.Parse(rowcount.ToString());
        }
        else
        {
            startRow = 1 + (cur - 1) * 10;
            endRow = startRow + pageSize - 1;
            endRow = (endRow > rowcount) ? int.Parse(rowcount.ToString()) : endRow;
        }


        pageBuilder.Append("<ul class=\"pagination\" style=\"text-align:right\">");
        pageBuilder.Append("<li class=\"disabled\"><a href=\"#\"> Showing " + startRow + " to " + endRow + " of " + rowcount + " entries </a></li>");

        //var str = new StringBuilder();
        if (cur > 1)
        {
            //str.Append("<a href=\"" + string.Format(attr, cur - 1) + "\" class=\"pg-prev\" >上一页</a>");
            pageBuilder.Append("<li><a href=\"#\" onclick=\"" + string.Format(attr, cur - 1) + "\">← Previous </a></li>");
        }
        else
        {
            pageBuilder.Append("<li class=\"disabled\"><a href=\"#\"> Previous </a></li>");
            //str.Append("<span class=\"pg-prev\">上一页</span>");
        }
        var start = cur - split;
        var end = cur + split;
        if (start - 1 < 0)
        {
            end = end - (start - 1);
            start = 1;
        }
        if (end > total)
            end = total;
        for (int i = 1; i <= total; i++)  // 循环页数
        {
            //if (i == start - 1 && start - 1 != 1)
            //    str.Append("<span>...</span>");
            //if (i == end + 1 && total != end + 1)
            //    str.Append("<span>...</span>");
            if (i == cur)
                pageBuilder.Append("<li class=\"active\" styel=\"disabled:disabled\"><a href=\"#\" onclick=\"" + string.Format(attr, i) + "\" >" + i + "</a></li>");
            //str.Append("<span class=\"current\">" + cur + "</span>");
            else if ((i >= start && i <= end))// || i == 1 || i == total
                pageBuilder.Append("<li><a href=\"#\" onclick=\"" + string.Format(attr, i) + "\" >" + i + "</a></li>");
            //str.Append("<a href=\"" + string.Format(attr, i) + "\" >" + i + "</a>");
        }
        if (cur < total)
        {
            pageBuilder.Append("<li><a href=\"#\" onclick=\"" + string.Format(attr, cur - 1) + "\"> Next →</a></li>");
            //str.Append("<a href=\"" + string.Format(attr, cur + 1) + "\" class=\"pg pg_next\" >下一页</a>");
        }
        else
        {
            pageBuilder.Append("<li class=\"disabled\"><a href=\"#\"> Next </a></li>");
            //str.Append("<span class=\"pg pg_next\">下一页</span>");
        }

        pageBuilder.Append("</ul></div>");
        return pageBuilder.ToString();
    }

    public static int GetPageNum(double total, int pagesize)
    {
        var m = total % pagesize;
        if (m > 0)
            m = 1;
        return (int)((total / pagesize) + m);
    }
}