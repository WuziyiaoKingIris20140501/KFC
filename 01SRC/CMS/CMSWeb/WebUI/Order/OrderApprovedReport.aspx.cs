using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HotelVp.CMS.Domain.Entity;
using HotelVp.CMS.Domain.Process;

public partial class WebUI_Order_OrderApprovedReport : System.Web.UI.Page
{
    public string FirstOrders = string.Empty;//初审完成订单号
    //public string FirstNSOrders = string.Empty;//初审NS订单号
    public List<string> FirstNSOrdersList = new List<string>();//初审NS订单号
    public string RecheckOrders = string.Empty;//复审完成订单号
    OrderInfoEntity _orderInfoEntity = new OrderInfoEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!System.IO.Directory.Exists("C:\\ConsultLog"))
        {
            System.IO.Directory.CreateDirectory("C:\\ConsultLog");
        }
        if (!System.IO.File.Exists("C:\\ConsultLog\\订单审核报表-初审总酒店数量-Log.txt"))
        {
            System.IO.File.Create("C:\\ConsultLog\\订单审核报表-初审总酒店数量-Log.txt").Close();
            System.IO.File.Create("C:\\ConsultLog\\订单审核报表-初审未完成酒店数量-Log.txt").Close();

            System.IO.File.Create("C:\\ConsultLog\\订单审核报表-初审总订单数量-Log.txt").Close();
            System.IO.File.Create("C:\\ConsultLog\\订单审核报表-初审未完成订单数量-Log.txt").Close();
        }
    }

    #region    时间分组
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //离店时间
        string startDate = this.StartDate.Value;
        string endDate = this.EndDate.Value;
        //城市
        string cityID = hidSelectCity.Value == "" ? "" : hidSelectCity.Value.Substring((hidSelectCity.Value.IndexOf('[') + 1), (hidSelectCity.Value.IndexOf(']') - 1));
        //审核人员
        string sales = hidSelectBussiness.Value == "" ? "" : hidSelectBussiness.Value.Substring((hidSelectBussiness.Value.IndexOf('[') + 1), (hidSelectBussiness.Value.IndexOf(']') - 1));

        DataTable time = GetDate(startDate, endDate);

        DataTable dtResult = GetOrderList(startDate, endDate, cityID);

        DataTable hisTable = GetApprovedOrderList(startDate, DateTime.Parse(endDate).AddMonths(3).ToShortDateString(), sales);

        DataTable hisTableByCheck = GetApprovedOrderListByCheck(startDate, DateTime.Parse(endDate).AddMonths(3).ToString(), sales);

        AssemblyText(time, dtResult, hisTable, hisTableByCheck, sales);
    }

    public void AssemblyText(DataTable time, DataTable orderTable, DataTable hisTable, DataTable hisTableByCheck, string sales)
    {
        #region
        List<string> listHotel = new List<string>();//总酒店数（可入住单的酒店总数）
        int Orders = orderTable.Rows.Count;//订单总数（可入住单的酒店总数）
        int OrderNums = 0; //间夜总数（可入住单的酒店总数）
        List<string> ApprovNoHotel = new List<string>();//审核未完成酒店数量（未初审+初审NS未进行复审的酒店数量）
        int ApprovNoOrders = 0;//审核未完成订单数量（未初审+初审NS未进行复审的订单数量）
        int ApprovNoOrderNums = 0;//审核未完成间夜数量（未初审+初审NS未进行复审的间夜数量[房间数量*1]）


        int firstApprovHotel = 0;  //初审 –初审 总酒店数量（=酒店总数）
        int firstApprovOrders = 0; //初审总订单数量
        int firstApprovOrderNums = 0;//间夜数量
        List<string> firstApprovNoHotel = new List<string>(); //初审未完成酒店数量
        int firstApprovNoOrders = 0;//初审未完成订单数量
        int firstApprovNoOrderNums = 0;//初审未完成间夜数量

        List<string> recheckApproveListHotel = new List<string>();//复审 – 复审总酒店数量（初审结果为NS）
        int recheckApprovOrders = 0; //复审总订单数量（初审结果为NS）
        int recheckApprovOrderNums = 0; //复审总间夜数量（初审结果为NS）
        List<string> recheckApprovNoHotel = new List<string>();  //复审未完成酒店数量
        int recheckApprovNoOrders = 0; //复审未完成订单数量
        int recheckApprovNoOrderNums = 0; //复审未完成间夜数量

        //离店单
        DataRow[] rowOutCheckOrders = orderTable.Select("BOOK_STATUS_OTHER='8'");
        int outCheckOrders = rowOutCheckOrders.Length; //离店总订单数
        int outCheckOrderNums = 0; //离店总间夜数
        for (int i = 0; i < rowOutCheckOrders.Length; i++)
        {
            outCheckOrderNums += int.Parse(rowOutCheckOrders[i]["book_room_num"].ToString()) * 1;
        }

        int NSCountOrders = 0;//NS总订单数（初审为NS+复审NS的总订单数量）
        int NSCountOrderNums = 0;//NS总间夜数（初审为NS+复审NS的总间夜数量）
        double NSPickRate = 0;//NS拾回率（复审离店间夜数量/复审总间夜数量）
        double NSRate = 0;//NS率（复审NS间夜数量/总间夜数量）
        #endregion

        #region
        bool flag = false;
        double Num1 = 0;//复审离店间夜数量
        double Num2 = 0;//复审总间夜数量        
        DataTable dtNewTable = new DataTable();
        dtNewTable.Columns.Add("DATE");//日期
        dtNewTable.Columns.Add("ORDERNUM");//订单数
        dtNewTable.Columns.Add("FIRSTTRIAL"); //初审订单
        dtNewTable.Columns.Add("FIRSTNS");//初审NS订单
        dtNewTable.Columns.Add("FIRSTRATE");//初审完成率
        dtNewTable.Columns.Add("RECHECKORDERNUM");//复审订单数
        dtNewTable.Columns.Add("RECHECKTRIAL");//复审完成订单
        dtNewTable.Columns.Add("RECHECKNS");//复审NS
        dtNewTable.Columns.Add("RECHECKRATE");//复审完成率
        dtNewTable.Columns.Add("NSPICKRATE");//NS拾回率
        #endregion

        #region
        for (int i = 0; i < time.Rows.Count; i++)
        {
            DataRow row = dtNewTable.NewRow();
            row["DATE"] = time.Rows[i]["time"].ToString();//日期
            DataRow[] orderRows = orderTable.Select("out_date='" + time.Rows[i]["time"].ToString() + "'");//根据日期区分订单
            row["ORDERNUM"] = orderRows.Length;//订单数
            if (orderRows.Length > 0)
            {
                flag = false;
                for (int j = 0; j < orderRows.Length; j++)//循环当天订单  
                {
                    if (!listHotel.Contains(orderRows[j]["hotel_id"].ToString()))//获取总的酒店数
                    {
                        listHotel.Add(orderRows[j]["hotel_id"].ToString());
                    }
                    //间夜总数 
                    OrderNums += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1;


                    DataRow[] hisRow = hisTable.Select("EVENT_FG_ID='" + orderRows[j]["fog_order_num"].ToString() + "'");
                    var hisRowLength = hisRow.Length > 1 ? 1 : hisRow.Length;
                    row["FIRSTTRIAL"] = int.Parse(row["FIRSTTRIAL"].ToString() == "" ? "0" : row["FIRSTTRIAL"].ToString()) + hisRowLength; //初审订单  

                    DataRow[] NSrows = hisTable.Select("OD_STATUS='No-Show' and EVENT_FG_ID='" + orderRows[j]["fog_order_num"].ToString() + "'"); //初审NS订单
                    var NSrowsLength = NSrows.Length > 1 ? 1 : NSrows.Length;
                    row["FIRSTNS"] = int.Parse(row["FIRSTNS"].ToString() == "" ? "0" : row["FIRSTNS"].ToString()) + NSrowsLength;

                    //审核未完成酒店 各种信息 （见附件）
                    if (hisRow.Length == 0 || (hisRow.Length > 0 && hisRow[0]["OD_STATUS"].ToString() == "No-Show" && hisTableByCheck.Select("EVENT_FG_ID='" + orderRows[j]["fog_order_num"].ToString() + "'").Length == 0))
                    {
                        if (!ApprovNoHotel.Contains(orderRows[j]["hotel_id"].ToString()))
                        {
                            ApprovNoHotel.Add(orderRows[j]["hotel_id"].ToString());//审核未完成酒店数量
                        }
                        ApprovNoOrders += 1;//审核未完成订单数量

                        ApprovNoOrderNums += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1; //审核未完成间夜数量
                    }

                    //初审未完成酒店 各种信息
                    if (hisRow.Length == 0)
                    {
                        if (!firstApprovNoHotel.Contains(orderRows[j]["hotel_id"].ToString()))
                        {
                            firstApprovNoHotel.Add(orderRows[j]["hotel_id"].ToString());//初审未完成酒店数量
                        }
                        firstApprovNoOrders += 1;//初审未完成订单数量

                        firstApprovNoOrderNums += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1;//初审未完成间夜数量
                    }

                    if (NSrows.Length > 0)
                    {
                        if (!recheckApproveListHotel.Contains(orderRows[j]["hotel_id"].ToString()))
                        {
                            recheckApproveListHotel.Add(orderRows[j]["hotel_id"].ToString());//复审总酒店数量（初审结果为NS）
                        }
                        recheckApprovOrders += 1; //复审总订单数量（初审结果为NS）

                        recheckApprovOrderNums += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1;  //复审总间夜数量（初审结果为NS）
                        Num2 += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1;

                        #region
                        row["RECHECKORDERNUM"] = int.Parse(row["RECHECKORDERNUM"].ToString() == "" ? "0" : row["RECHECKORDERNUM"].ToString()) + NSrowsLength; //复审订单数

                        DataRow[] hisCheckRow = hisTableByCheck.Select("EVENT_FG_ID='" + orderRows[j]["fog_order_num"].ToString() + "'");//复审 已完成 订单
                        var hisCheckRowLength = hisCheckRow.Length > 1 ? 1 : hisCheckRow.Length;
                        row["RECHECKTRIAL"] = int.Parse(row["RECHECKTRIAL"].ToString() == "" ? "0" : row["RECHECKTRIAL"].ToString()) + hisCheckRowLength;
                        if (hisCheckRow.Length == 0)
                        {
                            if (!recheckApprovNoHotel.Contains(orderRows[j]["hotel_id"].ToString()))
                            {
                                recheckApprovNoHotel.Add(orderRows[j]["hotel_id"].ToString());//复审未完成酒店数量
                            }
                            recheckApprovNoOrders += 1; //复审未完成订单数量.

                            recheckApprovNoOrderNums += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1; //复审未完成间夜数量
                        }

                        DataRow[] NSCheckrows = hisTableByCheck.Select("OD_STATUS='No-Show' and EVENT_FG_ID='" + orderRows[j]["fog_order_num"].ToString() + "'");//复审NS订单
                        var NSCheckrowsLength = NSCheckrows.Length > 1 ? 1 : NSCheckrows.Length;
                        row["RECHECKNS"] = int.Parse(row["RECHECKNS"].ToString() == "" ? "0" : row["RECHECKNS"].ToString()) + NSCheckrowsLength;
                        if (NSCheckrows.Length > 0)
                        {
                            NSCountOrders += 1;//NS总订单数（初审为NS+复审NS的总订单数量）

                            NSCountOrderNums += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1;//NS总间夜数（初审为NS+复审NS的总间夜数量）
                        }

                        DataRow[] outCheckrows = hisTableByCheck.Select("OD_STATUS='离店' and EVENT_FG_ID='" + orderRows[j]["fog_order_num"].ToString() + "'");//复审离店订单
                        if (outCheckrows.Length > 0)
                        {
                            NSPickRate += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1;
                            Num1 += int.Parse(orderRows[j]["book_room_num"].ToString()) * 1;
                        }
                        flag = true;
                        #endregion
                    }
                    if (!flag)
                    {
                        row["RECHECKORDERNUM"] = int.Parse(row["RECHECKORDERNUM"].ToString() == "" ? "0" : row["RECHECKORDERNUM"].ToString()) + 0;
                        row["RECHECKTRIAL"] = int.Parse(row["RECHECKTRIAL"].ToString() == "" ? "0" : row["RECHECKTRIAL"].ToString()) + 0;
                        row["RECHECKNS"] = int.Parse(row["RECHECKNS"].ToString() == "" ? "0" : row["RECHECKNS"].ToString()) + 0;
                    }
                }
                #region
                //初审完成率
                if (row["ORDERNUM"].ToString() != "" && row["ORDERNUM"].ToString() != "0")
                {
                    row["FIRSTRATE"] = (double.Parse(row["FIRSTTRIAL"].ToString() == "" ? "0" : row["FIRSTTRIAL"].ToString()) / double.Parse(row["ORDERNUM"].ToString() == "" ? "0" : row["ORDERNUM"].ToString())) * 100;
                    row["FIRSTRATE"] = double.Parse(row["FIRSTRATE"].ToString()).ToString("0.00") + "%";
                }
                else
                {
                    row["FIRSTRATE"] = "0.00" + "%";
                }

                //复审完成率
                if (row["RECHECKORDERNUM"].ToString() != "" && row["RECHECKORDERNUM"].ToString() != "0")
                {
                    row["RECHECKRATE"] = (double.Parse(row["RECHECKTRIAL"].ToString() == "" ? "0" : row["RECHECKTRIAL"].ToString()) / double.Parse(row["RECHECKORDERNUM"].ToString() == "" ? "0" : row["RECHECKORDERNUM"].ToString())) * 100;
                    row["RECHECKRATE"] = double.Parse(row["RECHECKRATE"].ToString()).ToString("0.00") + "%";
                }
                else
                {
                    row["RECHECKRATE"] = "0.00" + "%";
                }
                #endregion
            }
            else
            {
                #region
                row["FIRSTTRIAL"] = "0"; //初审订单
                row["FIRSTNS"] = "0";//NS订单
                row["FIRSTRATE"] = "0.00";//初审完成率
                row["RECHECKORDERNUM"] = "0"; //复审订单数
                row["RECHECKTRIAL"] = "0";//复审完成订单
                row["RECHECKNS"] = "0";//复审NS
                row["RECHECKRATE"] = "0.00"; //复审完成率
                row["NSPICKRATE"] = "0.00";//NS拾回率
                #endregion
            }
            if (Num2 != 0)
                row["NSPICKRATE"] = ((Num1 / Num2) * 100).ToString("0.00") + "%";
            else
                row["NSPICKRATE"] = "0.00" + "%";

            dtNewTable.Rows.Add(row);
        }
        #endregion

        firstApprovHotel = listHotel.Count;//初审 总酒店数量
        firstApprovOrders = Orders;//初审总订单数量
        firstApprovOrderNums = OrderNums;//间夜数量

        NSPickRate = NSPickRate / recheckApprovOrderNums;//NS拾回率（复审离店间夜数量/复审总间夜数量）
        NSPickRate = double.Parse(NSPickRate.ToString("0.00"));
        NSRate = NSCountOrderNums / OrderNums;//NS率（复审NS间夜数量/总间夜数量）
        NSRate = double.Parse(NSRate.ToString("0.00"));

        gridViewCSReviewUserList.DataSource = dtNewTable;
        gridViewCSReviewUserList.DataKeyNames = new string[] { };//主键
        gridViewCSReviewUserList.DataBind();

        #region
        this.divHotelSys.InnerHtml = "";
        this.divHotelFirts.InnerHtml = "";
        this.divHotelCheck.InnerHtml = "";
        this.divHotelNS.InnerHtml = "";

        this.divHotelSys.InnerHtml += "<table style=\"width:100%\" cellpadding=\"0\" cellspacing=\"0\">";

        this.divHotelSys.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        this.divHotelSys.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">总酒店数:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + listHotel.Count.ToString() + "</td>";
        this.divHotelSys.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">订单总数:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + Orders.ToString() + "</td>";
        this.divHotelSys.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">间夜总数:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + OrderNums.ToString() + "</td>";
        this.divHotelSys.InnerHtml += "</tr>";

        this.divHotelSys.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        this.divHotelSys.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">审核未完成酒店数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + ApprovNoHotel.Count.ToString() + "</td>";
        this.divHotelSys.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">审核未完成订单数量:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + ApprovNoOrders.ToString() + "</td>";
        this.divHotelSys.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">审核未完成间夜数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + ApprovNoOrderNums.ToString() + "</td>";
        this.divHotelSys.InnerHtml += "</tr>";
        this.divHotelSys.InnerHtml += "</table>";

        this.divHotelFirts.InnerHtml += "<table style=\"width:100%\" cellpadding=\"0\" cellspacing=\"0\">";
        this.divHotelFirts.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        this.divHotelFirts.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">初审-总酒店数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + firstApprovHotel.ToString() + "</td>";
        this.divHotelFirts.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">初审总订单数量:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + firstApprovOrders.ToString() + "</td>";
        this.divHotelFirts.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">初审间夜数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + firstApprovOrderNums.ToString() + "</td>";
        this.divHotelFirts.InnerHtml += "</tr>";

        this.divHotelFirts.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        this.divHotelFirts.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">初审未完成酒店数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + firstApprovNoHotel.Count.ToString() + "</td>";
        this.divHotelFirts.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">初审未完成订单数量:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + firstApprovNoOrders.ToString() + "</td>";
        this.divHotelFirts.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">初审未完成间夜数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + firstApprovNoOrderNums.ToString() + "</td>";
        this.divHotelFirts.InnerHtml += "</tr>";
        this.divHotelFirts.InnerHtml += "</table>";

        this.divHotelCheck.InnerHtml += "<table style=\"width:100%\" cellpadding=\"0\" cellspacing=\"0\">";
        this.divHotelCheck.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        this.divHotelCheck.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">复审-总酒店数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + recheckApproveListHotel.Count.ToString() + "</td>";
        this.divHotelCheck.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">复审总订单数量:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + recheckApprovOrders.ToString() + "</td>";
        this.divHotelCheck.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">复审总间夜数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + recheckApprovOrderNums.ToString() + "</td>";
        this.divHotelCheck.InnerHtml += "</tr>";

        this.divHotelCheck.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        this.divHotelCheck.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">复审未完成酒店数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + recheckApprovNoHotel.Count.ToString() + "</td>";
        this.divHotelCheck.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">复审未完成订单数量:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + recheckApprovNoOrders.ToString() + "</td>";
        this.divHotelCheck.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">复审未完成间夜数量:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + recheckApprovNoOrderNums.ToString() + "</td>";
        this.divHotelCheck.InnerHtml += "</tr>";
        this.divHotelCheck.InnerHtml += "</table>";

        this.divHotelNS.InnerHtml += "<table style=\"width:100%\" cellpadding=\"0\" cellspacing=\"0\">";
        this.divHotelNS.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        this.divHotelNS.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">离店总订单数:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + outCheckOrders.ToString() + "</td>";
        this.divHotelNS.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">离店总间夜数:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + outCheckOrderNums.ToString() + "</td>";
        this.divHotelNS.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">NS总订单数:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + NSCountOrders.ToString() + "</td>";
        this.divHotelNS.InnerHtml += "</tr>";

        this.divHotelNS.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        this.divHotelNS.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">NS总间夜数:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + NSCountOrderNums.ToString() + "</td>";
        this.divHotelNS.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">NS拾回率:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + NSPickRate.ToString() + "%" + "</td>";
        this.divHotelNS.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">NS率:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + NSRate.ToString() + "%" + "</td>";
        this.divHotelNS.InnerHtml += "</tr>";

        this.divHotelNS.InnerHtml += "</table>";

        this.divHotelNS.Style.Add("display", "block");
        #endregion
    }

    //根据离店时间  订单城市  获取订单列表 
    public DataTable GetOrderList(string startDate, string endDate, string cityID)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        orderinfoEntity.CITY_ID = cityID;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.OrderApprovedReport(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    //根据审核人员 获取初审 订单列表
    public DataTable GetApprovedOrderList(string startDate, string endDate, string sales)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.CONTACT_NAME = sales;
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetApprovedOrderList(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    //根据审核人员 获取复审 订单列表
    public DataTable GetApprovedOrderListByCheck(string startDate, string endDate, string sales)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.CONTACT_NAME = sales;
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetApprovedOrderListByCheck(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    /// <summary>
    /// 自动拼取时间段  --  业务逻辑 拼装
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    private DataTable GetDate(string startDate, string endDate)
    {
        DataTable TimeList = new DataTable();
        TimeList.Columns.Add(new DataColumn("time"));
        TimeList.Columns.Add(new DataColumn("timeMD"));
        TimeSpan ts = DateTime.Parse(endDate) - DateTime.Parse(startDate);
        int days = ts.Days;
        for (int i = 0; i <= days; i++)
        {
            DataRow drRow = TimeList.NewRow();
            drRow["time"] = DateTime.Parse(startDate).AddDays(i).ToShortDateString();
            drRow["timeMD"] = DateTime.Parse(startDate).AddDays(i).Month.ToString() + "-" + DateTime.Parse(startDate).AddDays(i).Day.ToString();

            TimeList.Rows.Add(drRow);
        }
        return TimeList;
    }
    #endregion

    #region   人员分组
    protected void btnApprovSearch_Click(object sender, EventArgs e)
    {
        //离店时间
        string startDate = this.StartDatePanel.Value;
        string endDate = this.EndDatePanel.Value;
        //城市
        string cityID = hidSelectCity.Value == "" ? "" : hidSelectCity.Value.Substring((hidSelectCity.Value.IndexOf('[') + 1), (hidSelectCity.Value.IndexOf(']') - 1));

        DataTable sales = GetSales(startDate, endDate);

        //DataTable dtResult = GetOrderListNum(startDate, endDate);

        //DataTable hisTable = GetApprovedOrderList(startDate, endDate);//初审订单列表

        //DataTable hisTableByCheck = GetApprovedOrderListByCheck(startDate, endDate);//复审订单列表

        //AssemblyApproveUserText2(sales, dtResult, hisTable, hisTableByCheck);
        AssemblyApproveUserText(sales);
    }

    public void AssemblyApproveUserText1(DataTable Sales, DataTable orderTable, DataTable hisTable, DataTable hisTableByCheck)
    {
        string AllOrderNo = "";
        #region
        List<string> listHotel = new List<string>();//总酒店数（可入住单的酒店总数）
        List<string> firstApprovNoHotel = new List<string>(); //初审 酒店数量
        List<string> recheckApproveListHotel = new List<string>();//复审 – 复审总酒店数量（初审结果为NS）
        int OrderNums = 0;//间夜总数（可入住单的间夜总数）
        int recheckApprovOrders = 0; //复审总订单数量（初审结果为NS）
        int Orders = orderTable.Rows.Count;//订单总数（可入住单的订单总数）       
        #endregion

        #region
        DataTable dtNewTable = new DataTable();
        dtNewTable.Columns.Add("FIRSTSALES");//人员
        dtNewTable.Columns.Add("FIRSTHOTELS");//初审酒店数
        dtNewTable.Columns.Add("FIRSTTORDERS"); //初审订单数
        dtNewTable.Columns.Add("FIRSTNSRATE");//初审NS率
        dtNewTable.Columns.Add("FIRSTWORKRATE");//初审工作量 占比
        dtNewTable.Columns.Add("RECHECKSALES");//人员
        dtNewTable.Columns.Add("RECHECKHOTELS");//复审酒店数
        dtNewTable.Columns.Add("RECHECKTORDERS"); //复审订单数
        dtNewTable.Columns.Add("RECHECKNSRATE");//复审NS率
        dtNewTable.Columns.Add("RECHECKWORKRATE");//复审工作量 占比
        #endregion

        for (int i = 0; i < orderTable.Rows.Count; i++)
        {
            string fogID = orderTable.Rows[i]["fog_order_num"].ToString();
            OrderNums += int.Parse(orderTable.Rows[i]["book_room_num"].ToString()) * 1;//间夜总数

            if (!listHotel.Contains(orderTable.Rows[i]["hotel_id"].ToString()))//获取总的酒店数
            {
                listHotel.Add(orderTable.Rows[i]["hotel_id"].ToString());
            }

            DataRow[] fristRow = hisTable.Select("EVENT_FG_ID='" + fogID + "'");
            if (fristRow.Length > 0)
            {
                DataRow row = dtNewTable.NewRow();

                var hisRowLength = fristRow.Length > 1 ? 1 : fristRow.Length;
                row["FIRSTTORDERS"] = int.Parse(row["FIRSTTORDERS"].ToString() == "" ? "0" : row["FIRSTTORDERS"].ToString()) + hisRowLength; //初审订单数

                DataRow[] NSrows = hisTable.Select("OD_STATUS='No-Show' and EVENT_FG_ID='" + fogID + "'"); //初审NS订单
                if (NSrows.Length > 0)//初审NS  计算间夜数 
                {
                    var roomNums = int.Parse(orderTable.Rows[i]["book_room_num"].ToString()) * 1;
                    row["FIRSTNSRATE"] = int.Parse(row["FIRSTNSRATE"].ToString() == "" ? "0" : row["FIRSTNSRATE"].ToString()) + roomNums; // 初审NS率 --- 初审间夜数

                    DataRow[] checkRow = hisTableByCheck.Select("EVENT_FG_ID='" + fogID + "'");
                    if (checkRow.Length > 0)
                    {
                        if (!recheckApproveListHotel.Contains(orderTable.Rows[i]["hotel_id"].ToString()))
                        {
                            recheckApproveListHotel.Add(orderTable.Rows[i]["hotel_id"].ToString());//复审总酒店数量（初审结果为NS）
                        }
                        var chkRowLength = checkRow.Length > 1 ? 1 : checkRow.Length;
                        row["RECHECKTORDERS"] = int.Parse(row["RECHECKTORDERS"].ToString() == "" ? "0" : row["RECHECKTORDERS"].ToString()) + chkRowLength; //复审订单数
                        DataRow[] chkNSrows = hisTableByCheck.Select("OD_STATUS='No-Show' and EVENT_FG_ID='" + fogID + "'"); //复审NS订单
                        if (chkNSrows.Length > 0)//复审NS  计算间夜数 
                        {
                            var chkRoomNums = int.Parse(orderTable.Rows[i]["book_room_num"].ToString()) * 1;
                            row["RECHECKNSRATE"] = int.Parse(row["RECHECKNSRATE"].ToString() == "" ? "0" : row["RECHECKNSRATE"].ToString()) + chkRoomNums; // 复审NS率 --- 复审间夜数
                        }
                    }
                }
                dtNewTable.Rows.Add(row);
            }
            else
            {
                AllOrderNo += fogID + ",";
            }
        }

        //mainPanelDiv.InnerHtml = AllOrderNo;

    }

    public void AssemblyApproveUserText2(DataTable Sales, DataTable orderTable, DataTable hisTable, DataTable hisTableByCheck)
    {
        #region
        List<string> listHotel = new List<string>();//总酒店数（可入住单的酒店总数）
        List<string> firstApprovNoHotel = new List<string>(); //初审 酒店数量
        List<string> recheckApproveListHotel = new List<string>();//复审 – 复审总酒店数量（初审结果为NS）
        int OrderNums = 0;//间夜总数（可入住单的间夜总数）
        int recheckApprovOrders = 0; //复审总订单数量（初审结果为NS）
        int Orders = orderTable.Rows.Count;//订单总数（可入住单的订单总数）       
        #endregion

        #region
        DataTable dtNewTable = new DataTable();
        dtNewTable.Columns.Add("FIRSTSALES");//人员
        dtNewTable.Columns.Add("FIRSTHOTELS");//初审酒店数
        dtNewTable.Columns.Add("FIRSTTORDERS"); //初审订单数
        dtNewTable.Columns.Add("FIRSTNSRATE");//初审NS率
        dtNewTable.Columns.Add("FIRSTWORKRATE");//初审工作量 占比
        dtNewTable.Columns.Add("RECHECKSALES");//人员
        dtNewTable.Columns.Add("RECHECKHOTELS");//复审酒店数
        dtNewTable.Columns.Add("RECHECKTORDERS"); //复审订单数
        dtNewTable.Columns.Add("RECHECKNSRATE");//复审NS率
        dtNewTable.Columns.Add("RECHECKWORKRATE");//复审工作量 占比
        #endregion

        for (int i = 0; i < Sales.Rows.Count; i++)//根据人员分组
        {
            firstApprovNoHotel = new List<string>(); //初审 酒店数量
            recheckApproveListHotel = new List<string>();//复审 – 复审总酒店数量（初审结果为NS）

            DataRow row = dtNewTable.NewRow();
            row["FIRSTSALES"] = Sales.Rows[i]["EVENT_USER"].ToString();
            row["RECHECKSALES"] = Sales.Rows[i]["EVENT_USER"].ToString();

            //遍历订单列表   根据订单号和审核人  判断是否有初审 
            for (int j = 0; j < orderTable.Rows.Count; j++)
            {
                OrderNums += int.Parse(orderTable.Rows[j]["book_room_num"].ToString()) * 1;//间夜总数

                #region  初审计算
                if (!listHotel.Contains(orderTable.Rows[j]["hotel_id"].ToString()))//获取总的酒店数
                {
                    listHotel.Add(orderTable.Rows[j]["hotel_id"].ToString());
                }

                DataRow[] fristRow = hisTable.Select("EVENT_FG_ID='" + orderTable.Rows[j]["fog_order_num"].ToString() + "' and EVENT_USER='" + Sales.Rows[i]["EVENT_USER"].ToString() + "'");
                if (fristRow.Length > 0)
                {
                    if (!firstApprovNoHotel.Contains(orderTable.Rows[j]["hotel_id"].ToString()))//初审酒店数
                    {
                        firstApprovNoHotel.Add(orderTable.Rows[j]["hotel_id"].ToString());
                    }
                    var hisRowLength = fristRow.Length > 1 ? 1 : fristRow.Length;
                    row["FIRSTTORDERS"] = int.Parse(row["FIRSTTORDERS"].ToString() == "" ? "0" : row["FIRSTTORDERS"].ToString()) + hisRowLength; //初审订单数

                    DataRow[] NSrows = hisTable.Select("OD_STATUS='No-Show' and EVENT_FG_ID='" + orderTable.Rows[j]["fog_order_num"].ToString() + "' and EVENT_USER='" + Sales.Rows[i]["EVENT_USER"].ToString() + "'"); //初审NS订单
                    if (NSrows.Length > 0)//初审NS  计算间夜数 
                    {
                        var roomNums = int.Parse(orderTable.Rows[j]["book_room_num"].ToString()) * 1;
                        row["FIRSTNSRATE"] = int.Parse(row["FIRSTNSRATE"].ToString() == "" ? "0" : row["FIRSTNSRATE"].ToString()) + roomNums; // 初审NS率 --- 初审间夜数
                        #region   复审计算   初审为NS，才进行复审的计算

                        recheckApprovOrders += 1;//复审总订单数量（初审结果为NS）

                        DataRow[] checkRow = hisTableByCheck.Select("EVENT_FG_ID='" + orderTable.Rows[j]["fog_order_num"].ToString() + "' and EVENT_USER='" + Sales.Rows[i]["EVENT_USER"].ToString() + "'");
                        if (checkRow.Length > 0)
                        {
                            if (!recheckApproveListHotel.Contains(orderTable.Rows[j]["hotel_id"].ToString()))
                            {
                                recheckApproveListHotel.Add(orderTable.Rows[j]["hotel_id"].ToString());//复审总酒店数量（初审结果为NS）
                            }
                            var chkRowLength = checkRow.Length > 1 ? 1 : checkRow.Length;
                            row["RECHECKTORDERS"] = int.Parse(row["RECHECKTORDERS"].ToString() == "" ? "0" : row["RECHECKTORDERS"].ToString()) + chkRowLength; //复审订单数
                            DataRow[] chkNSrows = hisTableByCheck.Select("OD_STATUS='No-Show' and EVENT_FG_ID='" + orderTable.Rows[j]["fog_order_num"].ToString() + "' and EVENT_USER='" + Sales.Rows[i]["EVENT_USER"].ToString() + "'"); //复审NS订单
                            if (chkNSrows.Length > 0)//复审NS  计算间夜数 
                            {
                                var chkRoomNums = int.Parse(orderTable.Rows[j]["book_room_num"].ToString()) * 1;
                                row["RECHECKNSRATE"] = int.Parse(row["RECHECKNSRATE"].ToString() == "" ? "0" : row["RECHECKNSRATE"].ToString()) + chkRoomNums; // 复审NS率 --- 复审间夜数
                            }
                            else
                            {
                                row["RECHECKNSRATE"] = int.Parse(row["RECHECKNSRATE"].ToString() == "" ? "0" : row["RECHECKNSRATE"].ToString()) + 0;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        row["FIRSTNSRATE"] = int.Parse(row["FIRSTNSRATE"].ToString() == "" ? "0" : row["FIRSTNSRATE"].ToString()) + 0;
                        row["RECHECKTORDERS"] = int.Parse(row["RECHECKTORDERS"].ToString() == "" ? "0" : row["RECHECKTORDERS"].ToString()) + 0;
                        row["RECHECKNSRATE"] = int.Parse(row["RECHECKNSRATE"].ToString() == "" ? "0" : row["RECHECKNSRATE"].ToString()) + 0;
                    }
                }
                else
                {
                    row["FIRSTTORDERS"] = int.Parse(row["FIRSTTORDERS"].ToString() == "" ? "0" : row["FIRSTTORDERS"].ToString()) + 0;
                    row["FIRSTNSRATE"] = int.Parse(row["FIRSTNSRATE"].ToString() == "" ? "0" : row["FIRSTNSRATE"].ToString()) + 0;
                    row["RECHECKTORDERS"] = int.Parse(row["RECHECKTORDERS"].ToString() == "" ? "0" : row["RECHECKTORDERS"].ToString()) + 0;
                    row["RECHECKNSRATE"] = int.Parse(row["RECHECKNSRATE"].ToString() == "" ? "0" : row["RECHECKNSRATE"].ToString()) + 0;
                }
                #endregion
            }
            row["FIRSTHOTELS"] = firstApprovNoHotel.Count.ToString();
            row["RECHECKHOTELS"] = recheckApproveListHotel.Count.ToString();
            dtNewTable.Rows.Add(row);
        }

        //this.mainPanelDiv3.InnerHtml = "";
        //this.mainPanelDiv3.InnerHtml += "<table style=\"width:100%\" cellpadding=\"0\" cellspacing=\"0\">";

        //this.mainPanelDiv3.InnerHtml += "<tr style=\"line-height:35px;vertical-align:bottom;\">";
        //this.mainPanelDiv3.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">总酒店数:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + listHotel.Count.ToString() + "</td>";
        //this.mainPanelDiv3.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;padding-right:15px;\">订单总数:</td><td style=\"border-bottom:1px solid black;text-align:left;\">" + Orders.ToString() + "</td>";
        //this.mainPanelDiv3.InnerHtml += "<td style=\"border-bottom:1px solid black;text-align:right;width:20%;padding-right:15px;\">间夜总数:</td><td style=\"border-bottom:1px solid black;text-align:left;width:13%;\">" + OrderNums.ToString() + "</td>";
        //this.mainPanelDiv3.InnerHtml += "</tr>";
        //this.mainPanelDiv3.InnerHtml = "</table>";

        //this.mainPanelDiv3.Style.Add("display", "block");

        for (int i = 0; i < dtNewTable.Rows.Count; i++)
        {
            //dtNewTable.Rows[i]["FIRSTNSRATE"] = ((double.Parse(dtNewTable.Rows[i]["FIRSTNSRATE"].ToString()) / double.Parse(OrderNums.ToString())) * 100).ToString("0.00") + "%";//初审NS率
            //dtNewTable.Rows[i]["FIRSTWORKRATE"] = ((double.Parse(dtNewTable.Rows[i]["FIRSTTORDERS"].ToString()) / double.Parse(Orders.ToString())) * 100).ToString("0.00") + "%";//初审工作量 占比            

            //dtNewTable.Rows[i]["RECHECKNSRATE"] = ((double.Parse(dtNewTable.Rows[i]["RECHECKNSRATE"].ToString()) / double.Parse(OrderNums.ToString())) * 100).ToString("0.00") + "%";//复审NS率
            //dtNewTable.Rows[i]["RECHECKWORKRATE"] = ((double.Parse(dtNewTable.Rows[i]["RECHECKTORDERS"].ToString()) / double.Parse(recheckApprovOrders.ToString())) * 100).ToString("0.00") + "%";//复审工作量 占比 

            dtNewTable.Rows[i]["FIRSTNSRATE"] = double.Parse(dtNewTable.Rows[i]["FIRSTNSRATE"].ToString()) / double.Parse(OrderNums.ToString());//初审NS率
            dtNewTable.Rows[i]["FIRSTNSRATE"] = (double.Parse(dtNewTable.Rows[i]["FIRSTNSRATE"].ToString()) * 100).ToString("0.00") + "%";
            dtNewTable.Rows[i]["FIRSTWORKRATE"] = double.Parse(dtNewTable.Rows[i]["FIRSTTORDERS"].ToString()) / double.Parse(Orders.ToString());//初审工作量 占比            
            dtNewTable.Rows[i]["FIRSTWORKRATE"] = (double.Parse(dtNewTable.Rows[i]["FIRSTWORKRATE"].ToString()) * 100).ToString("0.00") + "%";

            dtNewTable.Rows[i]["RECHECKNSRATE"] = double.Parse(dtNewTable.Rows[i]["RECHECKNSRATE"].ToString()) / double.Parse(OrderNums.ToString());//复审NS率
            dtNewTable.Rows[i]["RECHECKNSRATE"] = (double.Parse(dtNewTable.Rows[i]["RECHECKNSRATE"].ToString()) * 100).ToString("0.00") + "%";
            dtNewTable.Rows[i]["RECHECKWORKRATE"] = double.Parse(dtNewTable.Rows[i]["RECHECKTORDERS"].ToString()) / double.Parse(recheckApprovOrders.ToString());//复审工作量 占比 
            dtNewTable.Rows[i]["RECHECKWORKRATE"] = (double.Parse(dtNewTable.Rows[i]["RECHECKWORKRATE"].ToString()) * 100).ToString("0.00") + "%";
        }

        gridView1.DataSource = dtNewTable;
        gridView1.DataKeyNames = new string[] { };//主键
        gridView1.DataBind();
    }

    public void AssemblyApproveUserText3(DataTable Sales, DataTable orderTable, DataTable hisTable, DataTable hisTableByCheck)
    {
        string fogOrderNum = "";
        #region
        List<string> listHotel = new List<string>();//总酒店数（可入住单的酒店总数）
        //List<string> firstApprovNoHotel = new List<string>(); //初审 酒店数量
        //List<string> recheckApproveListHotel = new List<string>();//复审 – 复审总酒店数量（初审结果为NS）
        int firstApprovNoHotel = 0;//初审 总酒店数量
        int recheckApproveListHotel = 0;//复审 – 复审总酒店数量（初审结果为NS）
        int OrderNums = 0;//间夜总数（可入住单的间夜总数）
        int recheckApprovOrders = 0; //复审总订单数量（初审结果为NS）
        int Orders = orderTable.Rows.Count;//订单总数（可入住单的订单总数）       
        #endregion

        #region
        DataTable dtNewTable = new DataTable();
        dtNewTable.Columns.Add("FIRSTSALES");//人员
        dtNewTable.Columns.Add("FIRSTHOTELS");//初审酒店数
        dtNewTable.Columns.Add("FIRSTTORDERS"); //初审订单数
        dtNewTable.Columns.Add("FIRSTNSRATE");//初审NS率
        dtNewTable.Columns.Add("FIRSTWORKRATE");//初审工作量 占比
        dtNewTable.Columns.Add("RECHECKSALES");//人员
        dtNewTable.Columns.Add("RECHECKHOTELS");//复审酒店数
        dtNewTable.Columns.Add("RECHECKTORDERS"); //复审订单数
        dtNewTable.Columns.Add("RECHECKNSRATE");//复审NS率
        dtNewTable.Columns.Add("RECHECKWORKRATE");//复审工作量 占比
        #endregion

        #region
        for (int i = 0; i < Sales.Rows.Count; i++)
        {
            fogOrderNum = "";
            DataRow row = dtNewTable.NewRow();
            row["FIRSTSALES"] = Sales.Rows[i]["EVENT_USER"].ToString();
            row["RECHECKSALES"] = Sales.Rows[i]["EVENT_USER"].ToString();

            DataRow[] rowHisTable = hisTable.Select("EVENT_USER='" + Sales.Rows[i]["EVENT_USER"].ToString() + "'");//初审酒店列表
            DataRow[] rowHisCheckTable = hisTableByCheck.Select("EVENT_USER='" + Sales.Rows[i]["EVENT_USER"].ToString() + "'");//复审酒店列表

            if (rowHisTable.Length > 0)
            {
                for (int j = 0; j < rowHisTable.Length; j++)//初审订单号
                {
                    fogOrderNum += rowHisTable[j]["EVENT_FG_ID"].ToString() + ",";
                    if ((j != 0 && j % 50 == 0) || j == rowHisTable.Length - 1)
                    {
                        int num = GetHisTableOrder(fogOrderNum).Rows.Count;//初审 酒店数量
                        firstApprovNoHotel += num;
                        row["FIRSTHOTELS"] = int.Parse(row["FIRSTHOTELS"].ToString() == "" ? "0" : row["FIRSTHOTELS"].ToString()) + num;
                        row["FIRSTTORDERS"] = int.Parse(row["FIRSTTORDERS"].ToString() == "" ? "0" : row["FIRSTTORDERS"].ToString()) + rowHisTable.Length;
                        DataTable NSTable = OrderApprovedByOrderDetails(fogOrderNum);//计算间夜数
                        for (int l = 0; l < fogOrderNum.Split(',').Length; l++)
                        {
                            if (fogOrderNum.Split(',')[l].ToString() != "")
                            {
                                DataRow[] NSrow = hisTable.Select("OD_STATUS='No-Show' and EVENT_FG_ID='" + fogOrderNum.Split(',')[l].ToString() + "' and EVENT_USER='" + Sales.Rows[i]["EVENT_USER"].ToString() + "'");
                                if (NSrow.Length > 0)//当前单为初审NS  计算间夜数
                                {
                                    for (int k = 0; k < NSTable.Rows.Count; k++)
                                    {
                                        if (NSTable.Rows[k]["fog_order_num"].ToString() == fogOrderNum.Split(',')[l].ToString())
                                        {
                                            var roomNums = int.Parse(NSTable.Rows[0]["book_room_num"].ToString()) * 1;
                                            row["FIRSTNSRATE"] = int.Parse(row["FIRSTNSRATE"].ToString() == "" ? "0" : row["FIRSTNSRATE"].ToString()) + roomNums; // 初审NS率 --- 初审间夜数
                                            OrderNums += roomNums;//总间夜数
                                            recheckApprovOrders += 1;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < NSTable.Rows.Count; k++)
                                    {
                                        if (NSTable.Rows[k]["fog_order_num"].ToString() == fogOrderNum.Split(',')[l].ToString())
                                        {
                                            OrderNums += int.Parse(NSTable.Rows[0]["book_room_num"].ToString()) * 1;//总间夜数
                                        }
                                    }

                                }
                            }
                        }
                        fogOrderNum = "";
                    }
                }
            }
            else
            {
                row["FIRSTHOTELS"] = "0";
                row["FIRSTTORDERS"] = "0";
                row["FIRSTNSRATE"] = "0";
            }

            fogOrderNum = "";
            if (rowHisCheckTable.Length > 0)
            {
                for (int j = 0; j < rowHisCheckTable.Length; j++)//复审订单号
                {
                    fogOrderNum += rowHisCheckTable[j]["EVENT_FG_ID"].ToString() + ",";
                    if ((j != 0 && j % 50 == 0) || j == rowHisCheckTable.Length - 1)
                    {
                        int num = GetHisTableOrder(fogOrderNum).Rows.Count;//复审 – 复审总酒店数量
                        recheckApproveListHotel += num;
                        row["RECHECKHOTELS"] = int.Parse(row["RECHECKHOTELS"].ToString() == "" ? "0" : row["RECHECKHOTELS"].ToString()) + num;
                        row["RECHECKTORDERS"] = int.Parse(row["RECHECKTORDERS"].ToString() == "" ? "0" : row["RECHECKTORDERS"].ToString()) + rowHisCheckTable.Length;

                        DataTable NSTable = OrderApprovedByOrderDetails(fogOrderNum);//计算间夜数
                        for (int l = 0; l < fogOrderNum.Split(',').Length; l++)
                        {
                            if (fogOrderNum.Split(',')[l].ToString() != "")
                            {
                                DataRow[] NSrow = hisTableByCheck.Select("OD_STATUS='No-Show' and EVENT_FG_ID='" + fogOrderNum.Split(',')[l].ToString() + "' and EVENT_USER='" + Sales.Rows[i]["EVENT_USER"].ToString() + "'");
                                if (NSrow.Length > 0)//当前单为复审NS  计算间夜数
                                {
                                    for (int k = 0; k < NSTable.Rows.Count; k++)
                                    {
                                        if (NSTable.Rows[k]["fog_order_num"].ToString() == fogOrderNum.Split(',')[l].ToString())
                                        {
                                            var roomNums = int.Parse(NSTable != null && NSTable.Rows.Count > 0 ? NSTable.Rows[0]["book_room_num"].ToString() : "0") * 1;
                                            row["RECHECKNSRATE"] = int.Parse(row["RECHECKNSRATE"].ToString() == "" ? "0" : row["RECHECKNSRATE"].ToString()) + roomNums; // 复审NS率 --- 复审间夜数
                                            OrderNums += roomNums;//总间夜数
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < NSTable.Rows.Count; k++)
                                    {
                                        if (NSTable.Rows[k]["fog_order_num"].ToString() == fogOrderNum.Split(',')[l].ToString())
                                        {
                                            OrderNums += int.Parse(NSTable.Rows[0]["book_room_num"].ToString()) * 1;//总间夜数
                                        }
                                    }

                                }
                            }
                        }

                        fogOrderNum = "";
                    }
                }
            }
            else
            {
                row["RECHECKHOTELS"] = "0";
                row["RECHECKTORDERS"] = "0";
                row["RECHECKNSRATE"] = "0";
            }
            dtNewTable.Rows.Add(row);
        }
        #endregion
        for (int i = 0; i < dtNewTable.Rows.Count; i++)
        {
            dtNewTable.Rows[i]["FIRSTNSRATE"] = double.Parse(dtNewTable.Rows[i]["FIRSTNSRATE"].ToString()) / double.Parse(OrderNums.ToString());//初审NS率
            dtNewTable.Rows[i]["FIRSTNSRATE"] = (double.Parse(dtNewTable.Rows[i]["FIRSTNSRATE"].ToString()) * 100).ToString("0.00") + "%";
            dtNewTable.Rows[i]["FIRSTWORKRATE"] = double.Parse(dtNewTable.Rows[i]["FIRSTTORDERS"].ToString()) / double.Parse(Orders.ToString());//初审工作量 占比            
            dtNewTable.Rows[i]["FIRSTWORKRATE"] = (double.Parse(dtNewTable.Rows[i]["FIRSTWORKRATE"].ToString()) * 100).ToString("0.00") + "%";

            dtNewTable.Rows[i]["RECHECKNSRATE"] = double.Parse(dtNewTable.Rows[i]["RECHECKNSRATE"].ToString()) / double.Parse(OrderNums.ToString());//复审NS率
            dtNewTable.Rows[i]["RECHECKNSRATE"] = (double.Parse(dtNewTable.Rows[i]["RECHECKNSRATE"].ToString()) * 100).ToString("0.00") + "%";
            dtNewTable.Rows[i]["RECHECKWORKRATE"] = double.Parse(dtNewTable.Rows[i]["RECHECKTORDERS"].ToString()) / double.Parse(recheckApprovOrders.ToString());//复审工作量 占比 
            dtNewTable.Rows[i]["RECHECKWORKRATE"] = (double.Parse(dtNewTable.Rows[i]["RECHECKWORKRATE"].ToString()) * 100).ToString("0.00") + "%";
        }
        this.mainPanelDiv3.InnerHtml = "";
        this.mainPanelDiv3.InnerHtml += "初审 总酒店数量:" + firstApprovNoHotel.ToString() + "</br>";
        this.mainPanelDiv3.InnerHtml += "复审 总酒店数量:" + recheckApproveListHotel.ToString() + "</br>";
        this.mainPanelDiv3.InnerHtml += "订单总数:" + Orders.ToString() + "</br>";
        this.mainPanelDiv3.InnerHtml += "间夜总数:" + OrderNums.ToString() + "</br>";

        mainPanelDiv.Style.Add("display", "block");
        gridView1.DataSource = dtNewTable;
        gridView1.DataKeyNames = new string[] { };//主键
        gridView1.DataBind();
    }

    public void AssemblyApproveUserText(DataTable Sales)
    {
        #region
        DataTable dtNewTable = new DataTable();
        dtNewTable.Columns.Add("FIRSTSALES");//人员  
        dtNewTable.Columns.Add("FIRSTTORDERS"); //初审订单数
        dtNewTable.Columns.Add("FIRSTNSRATE");//初审NS数
        dtNewTable.Columns.Add("RECHECKTORDERS"); //复审订单数
        dtNewTable.Columns.Add("RECHECKNSRATE");//复审离店数
        #endregion

        for (int i = 0; i < Sales.Rows.Count; i++)
        {
            DataRow row = dtNewTable.NewRow();
            row["FIRSTSALES"] = Sales.Rows[i]["EVENT_USER"].ToString();
            row["FIRSTTORDERS"] = GetOrderListNum(Sales.Rows[i]["EVENT_USER"].ToString(), this.StartDatePanel.Value, this.EndDatePanel.Value).Rows.Count;//每个人的初审订单数
            row["FIRSTNSRATE"] = GetFirstAppNSOrders(Sales.Rows[i]["EVENT_USER"].ToString(), this.StartDatePanel.Value, this.EndDatePanel.Value).Rows.Count;//每个人的初审NS订单数
            row["RECHECKTORDERS"] = GetCheckAppOrders(Sales.Rows[i]["EVENT_USER"].ToString(), this.StartDatePanel.Value, this.EndDatePanel.Value).Rows.Count;//每个人的复审NS订单数
            row["RECHECKNSRATE"] = GetCheckAppNSOrders(Sales.Rows[i]["EVENT_USER"].ToString(), this.StartDatePanel.Value, this.EndDatePanel.Value).Rows.Count;//每个人的复审NS订单数
            dtNewTable.Rows.Add(row);
        }
        gridView1.DataSource = dtNewTable;
        gridView1.DataKeyNames = new string[] { };//主键
        gridView1.DataBind();
    }

    //获取订单详情 计算见间夜数
    public DataTable OrderApprovedByOrderDetails(string orders)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.FOG_ORDER_NUM = orders;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.OrderApprovedByOrderDetails(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    //获取初审  复审 酒店数
    public DataTable GetHisTableOrder(string orders)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.FOG_ORDER_NUM = orders;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.OrderApprovedByCount(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    //根据离店时间  订单城市  获取订单列表 
    public DataTable GetApproveOrderList(string startDate, string endDate, string cityID)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        orderinfoEntity.CITY_ID = cityID;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.OrderApprovedReport(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    //根据审核人员 获取初审 订单列表
    public DataTable GetApprovedOrderList(string startDate, string endDate)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();

        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetApprovedOrderList(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    //根据审核人员 获取复审 订单列表
    public DataTable GetApprovedOrderListByCheck(string startDate, string endDate)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetApprovedOrderListByCheck(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    // 获取所有订单审核人员
    private DataTable GetSales(string startDate, string endDate)
    {
        //WebAutoCompleteBP webBP = new WebAutoCompleteBP();
        //DataTable dtSales = webBP.GetWebCompleteList("approveduser", "", "").Tables[0];

        //return dtSales;
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetApprovedUserListByCheck(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    //获取审核订单
    private DataTable GetOrderListNum(string startDate, string endDate)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetOrderListNum(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }

    #region   订单审核 临时数据   初审：初审订单  初审NS订单   复审：复审订单  复审离店单
    //获取初审订单数
    private DataTable GetOrderListNum(string sales, string startDate, string endDate)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.CONTACT_NAME = sales;
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetFirstAppOrders(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }
    //获取初审No-Show订单数
    private DataTable GetFirstAppNSOrders(string sales, string startDate, string endDate)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.CONTACT_NAME = sales;
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetFirstAppNSOrders(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }
    //获取复审订单数
    private DataTable GetCheckAppOrders(string sales, string startDate, string endDate)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.CONTACT_NAME = sales;
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetCheckAppOrders(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }
    //获取复审No-Show订单数
    private DataTable GetCheckAppNSOrders(string sales, string startDate, string endDate)
    {
        _orderInfoEntity.LogMessages = new HotelVp.Common.Logger.LogMessage();
        _orderInfoEntity.LogMessages.Userid = UserSession.Current.UserAccount;
        _orderInfoEntity.LogMessages.Username = UserSession.Current.UserDspName;
        _orderInfoEntity.LogMessages.IpAddress = UserSession.Current.UserIP;

        _orderInfoEntity.OrderInfoDBEntity = new List<OrderInfoDBEntity>();
        OrderInfoDBEntity orderinfoEntity = new OrderInfoDBEntity();
        orderinfoEntity.CONTACT_NAME = sales;
        orderinfoEntity.EventType = "订单审核";
        orderinfoEntity.StartDate = startDate;
        orderinfoEntity.EndDate = endDate;
        _orderInfoEntity.OrderInfoDBEntity.Add(orderinfoEntity);
        DataTable dtResult = OrderInfoBP.GetCheckAppNSOrders(_orderInfoEntity).QueryResult.Tables[0];

        return dtResult;
    }
    #endregion

    #endregion

    #region   人员分组 完整
    public DataTable SetFitstSales()//构建初审人员表
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("NameCN");
        dtResult.Columns.Add("NameEN");

        for (int i = 0; i < 4; i++)
        {
            DataRow row = dtResult.NewRow();
            switch (i)
            {
                case 0:
                    row["NameCN"] = "贾康妮";
                    row["NameEN"] = "Connie.Jia";
                    break;
                case 1:
                    row["NameCN"] = "沈文颖";
                    row["NameEN"] = "wenying.shen";
                    break;
                case 2:
                    row["NameCN"] = "沈骏";
                    row["NameEN"] = "Shin.Shen";
                    break;
                default:
                    row["NameCN"] = "郑丹凤";
                    row["NameEN"] = "danfeng.zheng";
                    break;
            }
            dtResult.Rows.Add(row);
        }
        return dtResult;
    }

    public DataTable SetRecheckSales()//构建复审人员表
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("NameCN");
        dtResult.Columns.Add("NameEN");

        for (int i = 0; i < 7; i++)
        {
            DataRow row = dtResult.NewRow();
            switch (i)
            {
                case 0:
                    row["NameCN"] = "童嘉伟";
                    row["NameEN"] = "jiawei.tong";
                    break;
                case 1:
                    row["NameCN"] = "夏传森";
                    row["NameEN"] = "chuansen.xia";
                    break;
                case 2:
                    row["NameCN"] = "姜鸿文";
                    row["NameEN"] = "kevin.jiang";
                    break;
                case 3:
                    row["NameCN"] = "薛岗";
                    row["NameEN"] = "richie.xue";
                    break;
                case 4:
                    row["NameCN"] = "赵伟";
                    row["NameEN"] = "visen.zhao";
                    break;
                case 5:
                    row["NameCN"] = "刘祺";
                    row["NameEN"] = "jason.liu";
                    break;
                case 6:
                    row["NameCN"] = "林培华";
                    row["NameEN"] = "Peihua.Lin";
                    break;
                default:
                    row["NameCN"] = "刘亮";
                    row["NameEN"] = "liang.liu";
                    break;
            }
            dtResult.Rows.Add(row);
        }
        return dtResult;
    }

    protected void btnAllApprovSearch_Click(object sender, EventArgs e)
    {
        //离店时间
        string startDate = this.AllStartDatePanel.Value; //DateTime.Parse(this.StartDatePanel.Value).AddDays(-1).ToShortDateString() + " 04:00:00";
        string endDate = this.AllEndDatePanel.Value; //DateTime.Parse(this.EndDatePanel.Value).AddDays(1).ToShortDateString() + " 04:00:00";
        //城市
        string cityID = hidAllSelectCity.Value == "" ? "" : hidAllSelectCity.Value.Substring((hidAllSelectCity.Value.IndexOf('[') + 1), (hidAllSelectCity.Value.IndexOf(']') - 1));

        //DataTable sales = GetSales(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(1).ToShortDateString());//获取 所有询房人员

        DataTable orderTables = GetApproveOrderList(DateTime.Parse(startDate).ToShortDateString() + " 04:00:00", DateTime.Parse(endDate).AddDays(1).ToShortDateString() + " 04:00:00", cityID);//获取固定时间段  订单列表

        AssemblyApproveUserText(orderTables, startDate, endDate);
    }

    public void AssemblyApproveUserText1(DataTable Sales, DataTable orderTables, string startDate, string endDate)
    {
        #region
        int hotelsNum = 0; //酒店总数（可入住单的酒店总数）  
        List<string> listHotelNum = new List<string>();

        int ordersNum = orderTables.Rows.Count;//订单总数（可入住单的订单总数）
        int roomNightNum = 0;//间夜总数（可入住单的酒店总数）

        int UNHotelsNum = 0; //审核未完成酒店数量（未初审+初审NS未进行复审的酒店数量）
        List<string> listUNHotelsNum = new List<string>();
        int UNOrdersNum = 0;//审核未完成订单数量（未初审+初审NS未进行复审的订单数量）
        int UNRoomNightNum = 0;//审核未完成间夜数量（未初审+初审NS未进行复审的间夜数量[房间数量*1]）

        int NSOrdersNum = 0;//NS总订单数（初审为NS+复审NS的总订单数量）
        int NSRoomNightNum = 0;//NS总间夜数（初审为NS+复审NS的总间夜数量）

        string NSPickRateNum = "";//NS拾回率（复审离店间夜数量/复审总间夜数量）
        int RechecktOutRoomNights = 0;//复审离店间夜数量
        int RechecktAllRoomNights = 0;//复审总间夜数量

        string NSRate = "";//NS率（复审NS间夜数量/总间夜数量）
        int RechecktNSRoomNights = 0;//复审NS间夜数量
        int AllRoomNights = 0;//总间夜数量

        #region
        List<string> listFirstHotels = new List<string>();//初审总酒店数量   临时变量
        List<string> listUNFirstHotels = new List<string>();//初审未完成酒店数量   临时变量

        List<string> listRechecktHotels = new List<string>();//复审总酒店数量   临时变量
        List<string> listUNRechecktHotels = new List<string>();//复审未完成酒店数量   临时变量
        #endregion
        #endregion
        #region
        DataTable dtNewTable = new DataTable();
        dtNewTable.Columns.Add("FIRSTSALES", typeof(int));//人员
        dtNewTable.Columns.Add("FIRSTHOTELS", typeof(int));//初审总酒店数量 
        dtNewTable.Columns.Add("FIRSTORDERS", typeof(int));//初审总订单数量
        dtNewTable.Columns.Add("FIRSTROOMNIGHT", typeof(int));//间夜数量
        dtNewTable.Columns.Add("FIRSTUNHOTELS", typeof(int));//初审未完成酒店数量
        dtNewTable.Columns.Add("FIRSTUNORDERS", typeof(int));//初审未完成订单数量
        dtNewTable.Columns.Add("FIRSTUNROOMNIGHT", typeof(int));//初审未完成间夜数量

        dtNewTable.Columns.Add("RECHECKTHOTELS", typeof(int));//复审总酒店数量                  应该是从 初审订单出来 
        dtNewTable.Columns.Add("RECHECKTORDERS", typeof(int));//复审总订单数量
        dtNewTable.Columns.Add("RECHECKTROOMNIGHT", typeof(int));//复审总间夜数量
        dtNewTable.Columns.Add("RECHECKTUNHOTELS", typeof(int));//复审未完成酒店数量
        dtNewTable.Columns.Add("RECHECKTUNORDERS", typeof(int));//复审未完成订单数量
        dtNewTable.Columns.Add("RECHECKTUNROOMNIGHT", typeof(int));//复审未完成间夜数量

        dtNewTable.Columns.Add("RECHECKTOUTORDERS", typeof(int)); //复审离店总订单数 
        dtNewTable.Columns.Add("RECHECKTOUTROOMNIGHT", typeof(int));//复审离店总间夜数

        dtNewTable.Columns["FIRSTSALES"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTHOTELS"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTORDERS"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTROOMNIGHT"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTUNHOTELS"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTUNORDERS"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTUNROOMNIGHT"].DefaultValue = 0;
        dtNewTable.Columns["RECHECKTHOTELS"].DefaultValue = 0;//复审总酒店数量
        dtNewTable.Columns["RECHECKTORDERS"].DefaultValue = 0;//复审总订单数量
        dtNewTable.Columns["RECHECKTROOMNIGHT"].DefaultValue = 0;//复审总间夜数量
        dtNewTable.Columns["RECHECKTUNHOTELS"].DefaultValue = 0;//复审未完成酒店数量
        dtNewTable.Columns["RECHECKTUNORDERS"].DefaultValue = 0;//复审未完成订单数量
        dtNewTable.Columns["RECHECKTUNROOMNIGHT"].DefaultValue = 0;//复审未完成间夜数量
        dtNewTable.Columns["RECHECKTOUTORDERS"].DefaultValue = 0; //复审离店总订单数 
        dtNewTable.Columns["RECHECKTOUTROOMNIGHT"].DefaultValue = 0;//复审离店总间夜数
        #endregion

        for (int i = 0; i < Sales.Rows.Count; i++)
        {
            #region
            listFirstHotels = new List<string>();//初审总酒店数量   临时变量
            listUNFirstHotels = new List<string>();//初审未完成酒店数量   临时变量

            listRechecktHotels = new List<string>();//复审总酒店数量   临时变量
            listUNRechecktHotels = new List<string>();//复审未完成酒店数量   临时变量
            #endregion

            DataRow row = dtNewTable.NewRow();
            row["FIRSTSALES"] = Sales.Rows[i]["EVENT_USER"].ToString();//人员

            DataTable dtFirstTable = GetApprovedOrderList(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(1).ToShortDateString(), Sales.Rows[i]["EVENT_USER"].ToString());//根据审核人员 获取初审 订单列表

            DataTable dtRechecktTable = GetApprovedOrderListByCheck(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(4).ToShortDateString(), Sales.Rows[i]["EVENT_USER"].ToString());//根据审核人员 获取复审 订单列表

            for (int j = 0; j < orderTables.Rows.Count; j++)
            {
                string orderNo = orderTables.Rows[j]["fog_order_num"].ToString();
                #region
                DataRow[] rowsFirst = dtFirstTable.Select("EVENT_FG_ID='" + orderNo + "'");
                if (rowsFirst.Length > 0)//此单已进行初审
                {
                    if (!listFirstHotels.Contains(orderTables.Rows[j]["hotel_id"].ToString()))
                    {
                        listFirstHotels.Add(orderTables.Rows[j]["hotel_id"].ToString());
                        System.IO.File.AppendAllText("C:\\ConsultLog\\订单审核报表-初审总酒店数量-Log.txt", "'" + orderTables.Rows[j]["hotel_id"].ToString() + "',\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                    }
                    row["FIRSTORDERS"] = int.Parse(row["FIRSTORDERS"].ToString() == "" ? "0" : row["FIRSTORDERS"].ToString()) + 1;//初审总订单数量.

                    var roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
                    row["FIRSTROOMNIGHT"] = int.Parse(row["FIRSTROOMNIGHT"].ToString() == "" ? "0" : row["FIRSTROOMNIGHT"].ToString()) + roomNums;  //初审完成间夜数量

                    System.IO.File.AppendAllText("C:\\ConsultLog\\订单审核报表-初审总订单数量-Log.txt", "'" + orderNo + "',\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                }
                else//此单未进行初审
                {
                    if (!listUNFirstHotels.Contains(orderTables.Rows[j]["hotel_id"].ToString()))
                    {
                        listUNFirstHotels.Add(orderTables.Rows[j]["hotel_id"].ToString());
                        System.IO.File.AppendAllText("C:\\ConsultLog\\订单审核报表-初审未完成酒店数量-Log.txt", "'" + orderTables.Rows[j]["hotel_id"].ToString() + "',\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                    }
                    row["FIRSTUNORDERS"] = int.Parse(row["FIRSTUNORDERS"].ToString() == "" ? "0" : row["FIRSTUNORDERS"].ToString()) + 1;//初审未完成订单数量

                    var roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
                    row["FIRSTUNROOMNIGHT"] = int.Parse(row["FIRSTUNROOMNIGHT"].ToString() == "" ? "0" : row["FIRSTUNROOMNIGHT"].ToString()) + roomNums;  //初审未完成间夜数量

                    System.IO.File.AppendAllText("C:\\ConsultLog\\订单审核报表-初审未完成订单数量-Log.txt", "'" + orderNo + "',\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                }
                #endregion

                #region  复审数据
                DataRow[] rowsRecheckt = dtRechecktTable.Select("EVENT_FG_ID='" + orderNo + "'");
                if (rowsRecheckt.Length > 0)//此单已进行复审
                {
                    if (!listRechecktHotels.Contains(orderTables.Rows[j]["hotel_id"].ToString()))
                    {
                        listRechecktHotels.Add(orderTables.Rows[j]["hotel_id"].ToString());
                    }
                    row["RECHECKTORDERS"] = int.Parse(row["RECHECKTORDERS"].ToString() == "" ? "0" : row["RECHECKTORDERS"].ToString()) + 1;//复审总订单数量

                    var roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
                    row["RECHECKTROOMNIGHT"] = int.Parse(row["RECHECKTROOMNIGHT"].ToString() == "" ? "0" : row["RECHECKTROOMNIGHT"].ToString()) + roomNums;  //复审总间夜数量
                    RechecktAllRoomNights = RechecktAllRoomNights + roomNums;//复审总间夜数量

                    if (orderTables.Rows[j]["BOOK_STATUS_OTHER"].ToString() == "8")//复审离店
                    {
                        row["RECHECKTOUTORDERS"] = int.Parse(row["RECHECKTOUTORDERS"].ToString() == "" ? "0" : row["RECHECKTOUTORDERS"].ToString()) + 1;//复审离店总订单数

                        row["RECHECKTOUTROOMNIGHT"] = int.Parse(row["RECHECKTOUTROOMNIGHT"].ToString() == "" ? "0" : row["RECHECKTOUTROOMNIGHT"].ToString()) + roomNums;  //复审离店总间夜数
                    }
                }
                else//此单未进行复审
                {
                    if (!listUNRechecktHotels.Contains(orderTables.Rows[j]["hotel_id"].ToString()))
                    {
                        listUNRechecktHotels.Add(orderTables.Rows[j]["hotel_id"].ToString());
                    }
                    row["RECHECKTUNORDERS"] = int.Parse(row["RECHECKTUNORDERS"].ToString() == "" ? "0" : row["RECHECKTUNORDERS"].ToString()) + 1;//复审未完成订单数量

                    var roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
                    row["RECHECKTUNROOMNIGHT"] = int.Parse(row["RECHECKTUNROOMNIGHT"].ToString() == "" ? "0" : row["RECHECKTUNROOMNIGHT"].ToString()) + roomNums;  //复审未完成间夜数量
                }
                #endregion
            }
            #region
            row["FIRSTHOTELS"] = listFirstHotels.Count.ToString();//初审总酒店数量
            //row["FIRSTORDERS"] = "";//初审总订单数量
            //row["FIRSTROOMNIGHT"] = "";//初审完成间夜数量
            row["FIRSTUNHOTELS"] = listUNFirstHotels.Count.ToString();//初审未完成酒店数量
            //row["FIRSTUNORDERS"] = "";//初审未完成订单数量
            //row["FIRSTUNROOMNIGHT"] = "";//初审未完成间夜数量

            row["RECHECKTHOTELS"] = listRechecktHotels.Count.ToString(); //复审总酒店数量
            //row["RECHECKTORDERS"] = ""; //复审总订单数量
            //row["RECHECKTROOMNIGHT"] = "";//复审总间夜数量
            row["RECHECKTUNHOTELS"] = listUNRechecktHotels.Count.ToString(); //复审未完成酒店数量
            //row["RECHECKTUNORDERS"] = ""; //复审未完成订单数量
            //row["RECHECKTUNROOMNIGHT"] = "";//复审未完成间夜数量

            //row["RECHECKTOUTORDERS"] = ""; //复审离店总订单数
            //row["RECHECKTOUTROOMNIGHT"] = "";//复审离店总间夜数

            dtNewTable.Rows.Add(row);
            #endregion
        }


        DataTable dtAllFirstTable = GetApprovedOrderList(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(1).ToShortDateString());//根据审核人员 获取初审 订单列表

        DataTable dtAllRechecktTable = GetApprovedOrderListByCheck(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(4).ToShortDateString());//根据审核人员 获取复审 订单列表
        for (int i = 0; i < orderTables.Rows.Count; i++)
        {
            if (!listHotelNum.Contains(orderTables.Rows[i]["hotel_id"].ToString()))
            {
                hotelsNum = hotelsNum + 1;//酒店总数（可入住单的酒店总数）  
            }
            int roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
            roomNightNum = roomNightNum + roomNums;//间夜总数（可入住单的酒店总数）

            string orderNo = orderTables.Rows[i]["fog_order_num"].ToString();
            #region 未初审+初审NS未进行复审
            DataRow[] rowsUNFirst = dtAllFirstTable.Select("EVENT_FG_ID='" + orderNo + "'");
            DataRow[] rowsUNFirstNS = dtAllFirstTable.Select("EVENT_FG_ID='" + orderNo + "' and OD_STATUS='No-Show'");
            DataRow[] rowsUNRecheckt = null;
            if (rowsUNFirstNS.Length > 0)
            {
                rowsUNRecheckt = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "'");
            }
            if (rowsUNFirst.Length == 0 && rowsUNFirstNS.Length > 0 && (rowsUNRecheckt == null || rowsUNRecheckt.Length == 0))//未初审+初审NS未进行复审
            {
                if (!listUNHotelsNum.Contains(orderTables.Rows[i]["hotel_id"].ToString()))
                {
                    UNHotelsNum = UNHotelsNum + 1;//审核未完成酒店数量（未初审+初审NS未进行复审的酒店数量）
                    UNOrdersNum = UNOrdersNum + 1;//审核未完成订单数量（未初审+初审NS未进行复审的订单数量）
                    UNRoomNightNum = UNRoomNightNum + roomNums;//审核未完成间夜数量（未初审+初审NS未进行复审的间夜数量[房间数量*1]）
                }
            }
            #endregion

            #region 初审为NS+复审NS
            DataRow[] rowsRechecktNS = null;
            if (rowsUNFirstNS.Length > 0)
            {
                rowsRechecktNS = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "' and OD_STATUS='No-Show'");
            }
            if (rowsUNFirstNS.Length > 0 && rowsUNFirstNS.Length > 0)
            {
                NSOrdersNum = NSOrdersNum + 1;//NS总订单数（初审为NS+复审NS的总订单数量）

                NSRoomNightNum = UNRoomNightNum + roomNums;//NS总间夜数（初审为NS+复审NS的总间夜数量）.
            }
            #endregion

            #region
            DataRow[] rowsRechecktOutOrder = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "' and OD_STATUS='离店'");
            if (rowsRechecktOutOrder.Length > 0)
            {
                RechecktOutRoomNights = RechecktOutRoomNights + roomNums;//复审离店间夜数量
            }
            DataRow[] rowsRechecktOut = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "'");
            if (rowsRechecktOut.Length > 0)
            {
                RechecktAllRoomNights = RechecktAllRoomNights + roomNums;//复审总间夜数量
            }
            DataRow[] rowsRechecktNSRoomNight = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "' and OD_STATUS='No-Show'");
            if (rowsRechecktNSRoomNight.Length > 0)
            {
                RechecktNSRoomNights = RechecktNSRoomNights + roomNums;//复审NS间夜数量
            }
            AllRoomNights = roomNightNum;//总间夜数量
            #endregion
        }
        NSPickRateNum = (double.Parse(RechecktOutRoomNights.ToString()) / double.Parse(RechecktAllRoomNights.ToString())).ToString("0.00");//NS拾回率（复审离店间夜数量/复审总间夜数量） 
        NSRate = (double.Parse(RechecktNSRoomNights.ToString()) / double.Parse(AllRoomNights.ToString())).ToString("0.00");//NS率（复审NS间夜数量/总间夜数量）

        divAllData.InnerHtml = "";
        divAllData.InnerHtml += "酒店总数（可入住单的酒店总数）:" + hotelsNum.ToString() + "</br>";
        divAllData.InnerHtml += "订单总数（可入住单的订单总数）:" + ordersNum.ToString() + "</br>";
        divAllData.InnerHtml += "间夜总数（可入住单的酒店总数）:" + roomNightNum.ToString() + "</br>";
        divAllData.InnerHtml += "审核未完成酒店数量（未初审+初审NS未进行复审的酒店数量）:" + UNHotelsNum.ToString() + "</br>";

        divAllData.InnerHtml += "审核未完成订单数量（未初审+初审NS未进行复审的订单数量）:" + UNOrdersNum.ToString() + "</br>";
        divAllData.InnerHtml += "审核未完成间夜数量（未初审+初审NS未进行复审的间夜数量[房间数量*1]）:" + UNRoomNightNum.ToString() + "</br>";

        divAllData.InnerHtml += "NS总订单数（初审为NS+复审NS的总订单数量）:" + NSOrdersNum.ToString() + "</br>";
        divAllData.InnerHtml += "NS总间夜数（初审为NS+复审NS的总间夜数量）:" + NSRoomNightNum.ToString() + "</br>";

        divAllData.InnerHtml += "NS拾回率（复审离店间夜数量/复审总间夜数量）:" + NSPickRateNum.ToString() + "</br>";

        divAllData.InnerHtml += "NS率（复审NS间夜数量/总间夜数量）:" + NSRate.ToString() + "</br>";

        this.Div4.Style.Add("display", "block");

        gridView2.DataSource = dtNewTable;
        gridView2.DataKeyNames = new string[] { };//主键
        gridView2.DataBind();
    }

    //汇总数据
    public void AssemblyApproveUserText(DataTable orderTables, string startDate, string endDate)
    {
        #region
        int hotelsNum = 0; //酒店总数（可入住单的酒店总数）  
        List<string> listHotelNum = new List<string>();

        int ordersNum = 0;//订单总数（可入住单的订单总数）
        int roomNightNum = 0;//间夜总数（可入住单的酒店总数）

        int UNHotelsNum = 0; //审核未完成酒店数量（未初审+初审NS未进行复审的酒店数量）
        List<string> listUNHotelsNum = new List<string>();
        int UNOrdersNum = 0;//审核未完成订单数量（未初审+初审NS未进行复审的订单数量）
        int UNRoomNightNum = 0;//审核未完成间夜数量（未初审+初审NS未进行复审的间夜数量[房间数量*1]）

        int NSOrdersNum = 0;//NS总订单数（初审为NS+复审NS的总订单数量）
        int NSRoomNightNum = 0;//NS总间夜数（初审为NS+复审NS的总间夜数量）

        string NSPickRateNum = "";//NS拾回率（复审离店间夜数量/复审总间夜数量）
        int RechecktOutRoomNights = 0;//复审离店间夜数量
        int RechecktAllRoomNights = 0;//复审总间夜数量

        string NSRate = "";//NS率（复审NS间夜数量/总间夜数量）
        int RechecktNSRoomNights = 0;//复审NS间夜数量
        int AllRoomNights = 0;//总间夜数量

        int FirstUNHotels = 0;//初审未完成酒店数量
        List<string> listFirstUnHotel = new List<string>();
        int FirstUNOrders = 0;//初审未完成订单数量
        int FirstUNRoomNight = 0;//初审未完成间夜数量 


        int RecheckUNHotels = 0;//复审未完成酒店数量
        List<string> listRecheckUNHotels = new List<string>();
        int RecheckUNOrder = 0;//复审未完成订单数量
        int RecheckUNRoomNight = 0;//复审未完成间夜数量

        #endregion


        DataTable orderFirstTables = AssemblyApproveUserTextFirst(orderTables, startDate, endDate);//初审

        DataTable orderRecheckTables = AssemblyApproveUserTextRecheck(orderFirstTables, orderTables, startDate, endDate);//复审

        DataTable dtAllFirstTable = GetApprovedOrderList(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(1).ToShortDateString());//根据操作时间 获取初审 订单列表

        DataTable dtAllRechecktTable = GetApprovedOrderListByCheck(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(4).ToShortDateString());//根据操作时间 获取复审 订单列表

        for (int i = 0; i < orderTables.Rows.Count; i++)
        {
            string orderNo = orderTables.Rows[i]["fog_order_num"].ToString();
            if (!listHotelNum.Contains(orderTables.Rows[i]["hotel_id"].ToString()))
            {
                listHotelNum.Add(orderTables.Rows[i]["hotel_id"].ToString());
            }
            int roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
            roomNightNum = roomNightNum + roomNums;

            #region 未初审+初审NS未进行复审
            DataRow[] rowsUNFirst = dtAllFirstTable.Select("EVENT_FG_ID='" + orderNo + "'");//是否进行初审
            DataRow[] rowsUNFirstNS = dtAllFirstTable.Select("EVENT_FG_ID='" + orderNo + "' and OD_STATUS='No-Show'");//初审是否为NS
            DataRow[] rowsUNRecheckt = null;
            if (rowsUNFirstNS.Length > 0)//是否进行复审
            {
                rowsUNRecheckt = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "'");
            }
            if (rowsUNFirst.Length == 0 || (rowsUNFirstNS.Length > 0 && (rowsUNRecheckt == null || rowsUNRecheckt.Length == 0)))//未初审+初审NS未进行复审
            {
                if (!listUNHotelsNum.Contains(orderTables.Rows[i]["hotel_id"].ToString()))
                {
                    listUNHotelsNum.Add(orderTables.Rows[i]["hotel_id"].ToString());
                    UNOrdersNum = UNOrdersNum + 1;//审核未完成订单数量（未初审+初审NS未进行复审的订单数量）
                    UNRoomNightNum = UNRoomNightNum + roomNums;//审核未完成间夜数量（未初审+初审NS未进行复审的间夜数量[房间数量*1]）
                }
            }
            
            #endregion

            #region 初审为NS+复审NS
            DataRow[] rowsRechecktNS = null;
            if (rowsUNFirstNS.Length > 0)
            {
                rowsRechecktNS = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "' and OD_STATUS='No-Show'");
            }
            if (rowsUNFirstNS.Length > 0 && rowsUNFirstNS.Length > 0)
            {
                NSOrdersNum = NSOrdersNum + 1;//NS总订单数（初审为NS+复审NS的总订单数量）

                NSRoomNightNum = UNRoomNightNum + roomNums;//NS总间夜数（初审为NS+复审NS的总间夜数量）
            }
            #endregion

            #region NS拾回率 NS率
            DataRow[] rowsRechecktOutOrder = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "' and OD_STATUS='离店'");
            if (rowsRechecktOutOrder.Length > 0)
            {
                RechecktOutRoomNights = RechecktOutRoomNights + roomNums;//复审离店间夜数量
            }
            DataRow[] rowsRechecktOut = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "'");
            if (rowsRechecktOut.Length > 0)
            {
                RechecktAllRoomNights = RechecktAllRoomNights + roomNums;//复审总间夜数量
            }
            DataRow[] rowsRechecktNSRoomNight = dtAllRechecktTable.Select("EVENT_FG_ID='" + orderNo + "' and OD_STATUS='No-Show'");
            if (rowsRechecktNSRoomNight.Length > 0)
            {
                RechecktNSRoomNights = RechecktNSRoomNights + roomNums;//复审NS间夜数量
            }
            AllRoomNights = roomNightNum;//总间夜数量
            #endregion  
        }
        UNHotelsNum = listUNHotelsNum.Count;//审核未完成酒店数量（未初审+初审NS未进行复审的酒店数量）

        hotelsNum = listHotelNum.Count;//酒店总数（可入住单的酒店总数） 
        ordersNum = orderTables.Rows.Count;//订单总数（可入住单的订单总数）


        #region 初审未完成
        string[] strFirstOrders = FirstOrders.Split(',');
        DataTable FirsrOrderTable = new DataTable();
        FirsrOrderTable = orderTables.Copy();
        for (int j = 0; j < strFirstOrders.Length; j++)
        {
            if (string.IsNullOrEmpty(strFirstOrders[j].ToString()))
            {
                DataRow[] rowFirst = FirsrOrderTable.Select("fog_order_num='" + strFirstOrders[j].ToString() + "'");
                if (rowFirst.Length > 0)
                {
                    FirsrOrderTable.Rows.Remove(rowFirst[0]);//订单号  存在 删除当前行  剩下的就是没有进行初审的 
                }
            }
        }

        for (int i = 0; i < FirsrOrderTable.Rows.Count; i++)
        {
            if (!listFirstUnHotel.Contains(FirsrOrderTable.Rows[i]["hotel_id"].ToString()))
            {
                listFirstUnHotel.Add(FirsrOrderTable.Rows[i]["hotel_id"].ToString());
            }
            int roomNums = int.Parse(FirsrOrderTable.Rows[i]["book_room_num"].ToString()) * 1;
            FirstUNRoomNight = FirstUNRoomNight + roomNums;//初审未完成间夜数量 
        }
        FirstUNHotels = listFirstUnHotel.Count;//初审未完成酒店数量
        FirstUNOrders = FirsrOrderTable.Rows.Count;//初审未完成订单数量 
        #endregion

        #region 复审未完成
        string[] strRechecktOrders = RecheckOrders.Split(',');//已复审订单号
        //for (int i = 0; i < FirstNSOrdersList.Count; i++)
        //{
        for (int j = 0; j < strRechecktOrders.Length; j++)
        {
            if (FirstNSOrdersList.Contains(strRechecktOrders[j].ToString()))//初审NS  订单号集合
            {
                FirstNSOrdersList.Remove(strRechecktOrders[j].ToString());//删除已复审的酒店  剩下未复审的订单号 
            }
        }
        //}

        for (int i = 0; i < FirstNSOrdersList.Count; i++)
        {
            DataRow[] rows = orderTables.Select("fog_order_num='" + FirstNSOrdersList[i].ToString() + "'");
            if (!listRecheckUNHotels.Contains(rows[0]["hotel_id"].ToString()))
            {
                listRecheckUNHotels.Add(rows[0]["hotel_id"].ToString());//复审未完成酒店数量
            }
            int roomNums = int.Parse(rows[0]["book_room_num"].ToString()) * 1;
            RecheckUNRoomNight = RecheckUNRoomNight + roomNums;//复审未完成间夜数量
        }
        RecheckUNHotels = listRecheckUNHotels.Count;//复审未完成酒店数量
        RecheckUNOrder = FirstNSOrdersList.Count;//复审未完成订单数量 
        #endregion


        NSPickRateNum = (double.Parse(RechecktOutRoomNights.ToString()) / double.Parse(RechecktAllRoomNights.ToString())).ToString("0.00");//NS拾回率（复审离店间夜数量/复审总间夜数量） 
        NSRate = (double.Parse(RechecktNSRoomNights.ToString()) / double.Parse(AllRoomNights.ToString())).ToString("0.00");//NS率（复审NS间夜数量/总间夜数量）

        divAllData.InnerHtml = "";
        divAllData.InnerHtml += "酒店总数（可入住单的酒店总数）:" + hotelsNum.ToString() + "</br>";
        divAllData.InnerHtml += "订单总数（可入住单的订单总数）:" + ordersNum.ToString() + "</br>";
        divAllData.InnerHtml += "间夜总数（可入住单的酒店总数）:" + roomNightNum.ToString() + "</br>";
        //divAllData.InnerHtml += "审核未完成酒店数量（未初审+初审NS未进行复审的酒店数量）:" + UNHotelsNum.ToString() + "</br>";

        divAllData.InnerHtml += "审核未完成订单数量（未初审+初审NS未进行复审的订单数量）:" + UNOrdersNum.ToString() + "</br>";
        divAllData.InnerHtml += "审核未完成间夜数量（未初审+初审NS未进行复审的间夜数量[房间数量*1]）:" + UNRoomNightNum.ToString() + "</br>";

        divAllData.InnerHtml += "NS总订单数（初审为NS+复审NS的总订单数量）:" + NSOrdersNum.ToString() + "</br>";
        divAllData.InnerHtml += "NS总间夜数（初审为NS+复审NS的总间夜数量）:" + NSRoomNightNum.ToString() + "</br>";

        divAllData.InnerHtml += "NS拾回率（复审离店间夜数量/复审总间夜数量）:" + NSPickRateNum.ToString() + "</br>";

        divAllData.InnerHtml += "NS率（复审NS间夜数量/总间夜数量）:" + NSRate.ToString() + "</br>";

        divAllData.InnerHtml += "初审未完成酒店数量:" + FirstUNHotels.ToString() + "</br>";
        divAllData.InnerHtml += "初审未完成订单数量:" + FirstUNOrders.ToString() + "</br>";
        divAllData.InnerHtml += "初审未完成间夜数量:" + FirstUNRoomNight.ToString() + "</br>";

        divAllData.InnerHtml += "复审未完成酒店数量:" + RecheckUNHotels.ToString() + "</br>";
        divAllData.InnerHtml += "复审未完成订单数量:" + RecheckUNOrder.ToString() + "</br>";
        divAllData.InnerHtml += "复审未完成间夜数量:" + RecheckUNRoomNight.ToString() + "</br>";


        this.Div4.Style.Add("display", "block");

        gridView2.DataSource = orderFirstTables;
        gridView2.DataKeyNames = new string[] { };//主键
        gridView2.DataBind();


        gridView3.DataSource = orderRecheckTables;
        gridView3.DataKeyNames = new string[] { };//主键
        gridView3.DataBind();
    }

    //初审
    public DataTable AssemblyApproveUserTextFirst(DataTable orderTables, string startDate, string endDate)
    {
        #region
        List<string> listFirstHotels = new List<string>();//初审总酒店数量   临时变量
        #endregion

        #region
        DataTable dtNewTable = new DataTable();
        dtNewTable.Columns.Add("FIRSTSALES");//人员
        dtNewTable.Columns.Add("FIRSTHOTELS", typeof(int));//初审总酒店数量 
        dtNewTable.Columns.Add("FIRSTORDERS", typeof(int));//初审总订单数量
        dtNewTable.Columns.Add("FIRSTROOMNIGHT", typeof(int));//间夜数量
        dtNewTable.Columns.Add("FIRSTRNS", typeof(int));//初审NS数
        //dtNewTable.Columns.Add("YALLFOGORDERNUM");//已初审订单号

        dtNewTable.Columns["FIRSTHOTELS"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTORDERS"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTRNS"].DefaultValue = 0;
        dtNewTable.Columns["FIRSTROOMNIGHT"].DefaultValue = 0;

        #endregion

        DataTable Sales = SetFitstSales();
        for (int i = 0; i < Sales.Rows.Count; i++)
        {
            DataRow row = dtNewTable.NewRow();
            //row["FIRSTSALES"] = Sales.Rows[i]["EVENT_USER"].ToString();//人员
            row["FIRSTSALES"] = Sales.Rows[i]["NameEN"].ToString();//人员

            #region
            listFirstHotels = new List<string>();//初审总酒店数量   临时变量
            #endregion

            #region  初审列表
            DataTable dtFirstTable = GetApprovedOrderList(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(1).ToShortDateString(), Sales.Rows[i]["NameEN"].ToString());//根据审核人员 获取初审 订单列表
            //DataTable dtFirstTable = GetApprovedOrderList(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(1).ToShortDateString(), Sales.Rows[i]["EVENT_USER"].ToString());//根据审核人员 获取初审 订单列表
            #endregion

            for (int j = 0; j < orderTables.Rows.Count; j++)
            {
                string orderNo = orderTables.Rows[j]["fog_order_num"].ToString();
                #region
                DataRow[] rowsFirst = dtFirstTable.Select("EVENT_FG_ID='" + orderNo + "'");
                if (rowsFirst.Length > 0)//此单已进行初审
                {
                    if (!listFirstHotels.Contains(orderTables.Rows[j]["hotel_id"].ToString()))
                    {
                        listFirstHotels.Add(orderTables.Rows[j]["hotel_id"].ToString());
                        System.IO.File.AppendAllText("C:\\ConsultLog\\订单审核报表-初审总酒店数量-Log.txt", "'" + orderTables.Rows[j]["hotel_id"].ToString() + "',\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                    }
                    row["FIRSTORDERS"] = int.Parse(row["FIRSTORDERS"].ToString() == "" ? "0" : row["FIRSTORDERS"].ToString()) + 1;//初审总订单数量.

                    var roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
                    row["FIRSTROOMNIGHT"] = int.Parse(row["FIRSTROOMNIGHT"].ToString() == "" ? "0" : row["FIRSTROOMNIGHT"].ToString()) + roomNums;  //初审完成间夜数量

                    System.IO.File.AppendAllText("C:\\ConsultLog\\订单审核报表-初审总订单数量-Log.txt", "'" + orderNo + "',\r\n", System.Text.Encoding.GetEncoding("GB2312"));

                    if (rowsFirst[0]["OD_STATUS"].ToString() == "No-Show")//初审No-Show
                    {
                        row["FIRSTRNS"] = int.Parse(row["FIRSTRNS"].ToString() == "" ? "0" : row["FIRSTRNS"].ToString()) + 1;
                        //FirstNSOrders = FirstNSOrders + orderNo + ",";//初审No-Show订单号
                        FirstNSOrdersList.Add(orderNo);
                    }
                    //row["YALLFOGORDERNUM"] = row["YALLFOGORDERNUM"] + orderNo + ",";//已初审订单号
                    FirstOrders = FirstOrders + orderNo + ",";//已初审订单号
                }
                //else//此单未进行初审
                //{
                //    if (!listUNFirstHotels.Contains(orderTables.Rows[j]["hotel_id"].ToString()))
                //    {
                //        listUNFirstHotels.Add(orderTables.Rows[j]["hotel_id"].ToString());
                //        System.IO.File.AppendAllText("C:\\ConsultLog\\订单审核报表-初审未完成酒店数量-Log.txt", "'" + orderTables.Rows[j]["hotel_id"].ToString() + "',\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                //    }
                //    row["FIRSTUNORDERS"] = int.Parse(row["FIRSTUNORDERS"].ToString() == "" ? "0" : row["FIRSTUNORDERS"].ToString()) + 1;//初审未完成订单数量

                //    var roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
                //    row["FIRSTUNROOMNIGHT"] = int.Parse(row["FIRSTUNROOMNIGHT"].ToString() == "" ? "0" : row["FIRSTUNROOMNIGHT"].ToString()) + roomNums;  //初审未完成间夜数量

                //    System.IO.File.AppendAllText("C:\\ConsultLog\\订单审核报表-初审未完成订单数量-Log.txt", "'" + orderNo + "',\r\n", System.Text.Encoding.GetEncoding("GB2312"));
                //}
                #endregion
            }
            row["FIRSTHOTELS"] = listFirstHotels.Count.ToString();//初审总酒店数量
            dtNewTable.Rows.Add(row);
        }
        return dtNewTable;
    }

    //复审
    public DataTable AssemblyApproveUserTextRecheck(DataTable orderFirstTables, DataTable orderTables, string startDate, string endDate)
    {
        #region
        List<string> listRechecktHotels = new List<string>();//复审总酒店数量   临时变量 
        #endregion

        #region
        DataTable dtNewTable = new DataTable();
        dtNewTable.Columns.Add("FIRSTSALES");//人员
        dtNewTable.Columns.Add("RECHECKTHOTELS", typeof(int));//复审总酒店数量                   
        dtNewTable.Columns.Add("RECHECKTORDERS", typeof(int));//复审总订单数量
        dtNewTable.Columns.Add("RECHECKTROOMNIGHT", typeof(int));//复审总间夜数量
        dtNewTable.Columns.Add("RECHECKTOUTORDERS", typeof(int)); //复审离店总订单数 
        dtNewTable.Columns.Add("RECHECKTOUTROOMNIGHT", typeof(int));//复审离店总间夜数
        //dtNewTable.Columns.Add("FIRSTALLORDER");//已复审订单号

        dtNewTable.Columns["RECHECKTHOTELS"].DefaultValue = 0;//复审总酒店数量
        dtNewTable.Columns["RECHECKTORDERS"].DefaultValue = 0;//复审总订单数量
        dtNewTable.Columns["RECHECKTROOMNIGHT"].DefaultValue = 0;//复审总间夜数量
        dtNewTable.Columns["RECHECKTOUTORDERS"].DefaultValue = 0; //复审离店总订单数 
        dtNewTable.Columns["RECHECKTOUTROOMNIGHT"].DefaultValue = 0;//复审离店总间夜数
        #endregion

        DataTable Sales = SetRecheckSales();

        string orders = "";
        for (int h = 0; h < FirstNSOrdersList.Count; h++)
        {
            orders = orders + FirstNSOrdersList[h].ToString() + ",";
        }
        string[] orderList = orders.Split(',');//只有初审NS的订单  才进行复审

        for (int i = 0; i < Sales.Rows.Count; i++)
        {
            #region
            listRechecktHotels = new List<string>();//复审总酒店数量   临时变量
            #endregion

            DataRow row = dtNewTable.NewRow();
            //row["FIRSTSALES"] = Sales.Rows[i]["EVENT_USER"].ToString();//人员
            row["FIRSTSALES"] = Sales.Rows[i]["NameEN"].ToString();//人员

            DataTable dtRechecktTable = GetApprovedOrderListByCheck(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(4).ToShortDateString(), Sales.Rows[i]["NameEN"].ToString());//根据审核人员 获取复审 订单列表
            //DataTable dtRechecktTable = GetApprovedOrderListByCheck(DateTime.Parse(startDate).AddDays(1).ToShortDateString(), DateTime.Parse(endDate).AddDays(4).ToShortDateString(), Sales.Rows[i]["EVENT_USER"].ToString());//根据审核人员 获取复审 订单列表

            //for (int j = 0; j < orderFirstTables.Rows.Count; j++)
            //{           
            for (int l = 0; l < orderList.Length; l++)
            {
                if (!string.IsNullOrEmpty(orderList[l].ToString()))
                {
                    string orderNo = orderList[l].ToString();
                    #region  复审数据
                    DataRow[] rowsRecheckt = dtRechecktTable.Select("EVENT_FG_ID='" + orderNo + "'");
                    DataRow[] orderRows = orderTables.Select("fog_order_num='" + orderNo + "'");
                    if (rowsRecheckt.Length > 0)//此单已进行复审
                    {
                        if (!listRechecktHotels.Contains(orderRows[0]["hotel_id"].ToString()))
                        {
                            listRechecktHotels.Add(orderRows[0]["hotel_id"].ToString());
                        }
                        row["RECHECKTORDERS"] = int.Parse(row["RECHECKTORDERS"].ToString() == "" ? "0" : row["RECHECKTORDERS"].ToString()) + 1;//复审总订单数量

                        var roomNums = int.Parse(orderRows[0]["book_room_num"].ToString()) * 1;
                        row["RECHECKTROOMNIGHT"] = int.Parse(row["RECHECKTROOMNIGHT"].ToString() == "" ? "0" : row["RECHECKTROOMNIGHT"].ToString()) + roomNums;  //复审总间夜数量 

                        if (rowsRecheckt[0]["OD_STATUS"].ToString() == "离店")//复审No-Show
                        {
                            row["RECHECKTOUTORDERS"] = int.Parse(row["RECHECKTOUTORDERS"].ToString() == "" ? "0" : row["RECHECKTOUTORDERS"].ToString()) + 1;//复审离店总订单数

                            row["RECHECKTOUTROOMNIGHT"] = int.Parse(row["RECHECKTOUTROOMNIGHT"].ToString() == "" ? "0" : row["RECHECKTOUTROOMNIGHT"].ToString()) + roomNums;  //复审离店总间夜数
                        }
                        //row["FIRSTALLORDER"] = row["FIRSTALLORDER"] + orderNo + ",";//已复审订单号
                        RecheckOrders = RecheckOrders + orderNo + ",";//已复审订单号
                    }
                    //else//此单未进行复审
                    //{
                    //    if (!listUNRechecktHotels.Contains(orderRows[0]["hotel_id"].ToString()))
                    //    {
                    //        listUNRechecktHotels.Add(orderRows[0]["hotel_id"].ToString());
                    //    }
                    //    row["RECHECKTUNORDERS"] = int.Parse(row["RECHECKTUNORDERS"].ToString() == "" ? "0" : row["RECHECKTUNORDERS"].ToString()) + 1;//复审未完成订单数量

                    //    var roomNums = int.Parse(orderTables.Rows[i]["book_room_num"].ToString()) * 1;
                    //    row["RECHECKTUNROOMNIGHT"] = int.Parse(row["RECHECKTUNROOMNIGHT"].ToString() == "" ? "0" : row["RECHECKTUNROOMNIGHT"].ToString()) + roomNums;  //复审未完成间夜数量
                    //}
                    #endregion
                }
                //}
            }
            row["RECHECKTHOTELS"] = listRechecktHotels.Count.ToString();//初审总酒店数量
            dtNewTable.Rows.Add(row);
        }
        return dtNewTable;
    }
    #endregion
}