using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


/// <summary>
///Oder 的摘要说明
/// </summary>
public class Oder
{
	public Oder()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public string getStatusByPayAndBookStatus(string bookstatus, string paystatus, string bookdate)
    {
        string nowDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-1));
        bookdate = string.Format("{0:yyyy-MM-dd}", bookdate);

        if (bookstatus == "5" && paystatus == "1" && nowDate.CompareTo(bookdate) > 0)
        {
            return "已完成";
        }
        if (bookstatus == "5" && paystatus == "1")
        {
            return "可入住";
        }
        if ((bookstatus == "0" || bookstatus == "1" || bookstatus == "5") && paystatus != "1")
        {
            return "等待支付";
        }
        if (bookstatus == "3")
        {
            return "已取消";
        }
        return "已取消";


    }

    public string getBookStatus(string bookstatus)
    {
        string strRtn = string.Empty;
        switch (bookstatus)
        {
            case "0":
                strRtn = "新建";
                break;
            case "1":
                strRtn = "新建成功";
                break;
            case "2":
                strRtn = "新建失败";
                break;
            case "3":
                strRtn = "超时";
                break;
            case "4":
                strRtn = "订单取消";
                break;
            case "5":
                strRtn = "成功";
                break;
            case "6":
                strRtn = "已完成";
                break;
            default:
                strRtn = "未知状态";
                break;       
        }
        return strRtn; 
    }

    public string getPayStatus(string paystatus)
    {
        string strRtn = string.Empty;
        switch (paystatus)
        {
            case "0":
                strRtn = "未支付";
                break;
            case "1":
                strRtn = "支付成功";
                break;
            case "2":
                strRtn = "等待支付";
                break;
            case "3":
                strRtn = "支付中";
                break;
            case "4":
                strRtn = "支付失败";
                break;
            case "5":
                strRtn = "支付确认中";
                break;
            case "6":
                strRtn = "异常取消";
                break;
            default:
                strRtn = "未支付";
                break;
        }
        return strRtn; 
    
    }

    public DataTable getBookDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn BookStatus = new DataColumn("BOOK_STATUS");
        DataColumn BookStatusText = new DataColumn("BOOK_STATUS_TEXT");
        dt.Columns.Add(BookStatus);
        dt.Columns.Add(BookStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["BOOK_STATUS"] = "";
        dr0["BOOK_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);

        for (int i = 0; i < 6; i++)
        {
            DataRow dr = dt.NewRow();
            dr["BOOK_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["BOOK_STATUS_TEXT"] = "新建";
                    break;
                case "1":
                    dr["BOOK_STATUS_TEXT"] = "新建成功";
                    break;
                case "2":
                    dr["BOOK_STATUS_TEXT"] = "新建失败";
                    break;
                case "3":
                    dr["BOOK_STATUS_TEXT"] = "超时";
                    break;
                case "4":
                    dr["BOOK_STATUS_TEXT"] = "订单取消";
                    break;
                case "5":
                    dr["BOOK_STATUS_TEXT"] = "成功";
                    break;
                case "6":
                    dr["BOOK_STATUS_TEXT"] = "已完成";
                    break;               
                default:
                    dr["BOOK_STATUS_TEXT"] = "未知状态";
                    break;
            }                     
            dt.Rows.Add(dr);
        }

       

        return dt;    
    }

    public DataTable getPayDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn PayStatus = new DataColumn("PAY_STATUS");
        DataColumn PayStatusText = new DataColumn("PAY_STATUS_TEXT");

        dt.Columns.Add(PayStatus);
        dt.Columns.Add(PayStatusText);

        DataRow dr0 = dt.NewRow();
        dr0["PAY_STATUS"] = "";
        dr0["PAY_STATUS_TEXT"] = "不限制";
        dt.Rows.Add(dr0);
        for (int i = 0; i < 6; i++)
        {
            DataRow dr = dt.NewRow();
            dr["PAY_STATUS"] = i.ToString();
            switch (i.ToString())
            {
                case "0":
                    dr["PAY_STATUS_TEXT"] = "未支付";
                    break;
                case "1":
                    dr["PAY_STATUS_TEXT"] = "支付成功";
                    break;
                case "2":
                    dr["PAY_STATUS_TEXT"] = "等待支付";
                    break;
                case "3":
                    dr["PAY_STATUS_TEXT"] = "支付中";
                    break;
                case "4":
                    dr["PAY_STATUS_TEXT"] = "支付失败";
                    break;
                case "5":
                    dr["PAY_STATUS_TEXT"] = "支付确认中";
                    break;
                case "6":
                    dr["PAY_STATUS_TEXT"] = "异常取消";
                    break;
                default:
                    dr["PAY_STATUS_TEXT"] = "未支付";
                    break;
            }
            dt.Rows.Add(dr);
        }

       
        return dt;    

    }
}