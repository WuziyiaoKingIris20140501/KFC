<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderOperationPrint.aspx.cs" 
    Inherits="WebUI_Order_OrderOperationPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
    <title></title>
     <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    body{FILTER: progid:DXImageTransform.Microsoft.BasicImage(grayScale :1); }

    img{FILTER: progid:DXImageTransform.Microsoft.BasicImage(grayScale :1);}

    div {filter: progid:dximagetransform.microsoft.basicimage(grayscale :1)}
    object {filter: progid:dximagetransform.microsoft.basicimage(grayscale :1)}
    select {filter: progid:dximagetransform.microsoft.basicimage(grayscale :1)}
    embed {filter: progid:dximagetransform.microsoft.basicimage(grayscale :1)}
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <table width="700" border="0" cellpadding="3" cellspacing="0" 
        padding-bottom: 5px">
        <tr>
           <td width="275" align="center">
                  今夜酒店特价
            </td>
            <td height="25"style="width:240px;text-align:center">
                传真：<asp:Label ID="Label4" runat="server" Text="021-52393323"></asp:Label>
            </td>
           <td align="center">
               电话：<asp:Label ID="Label1" runat="server" Text="021-52393373"></asp:Label>
            </td>
        </tr>
    </table>
    ========================================================================================
    <table width="700" border="0" cellpadding="3" cellspacing="0" >
        <tr>
            <td>
                传真FAX：<asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
            </td>
            <td align="right">
                首发时间：<asp:Label ID="lblSystemDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                预定酒店：<asp:Label ID="lblHotelName" runat="server" Text=""></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                订单号：<asp:Label ID="lblOrderNum" runat="server" Text=""></asp:Label>
            </td>
            <td align="left" >
                
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    ========================================================================================
    <table width="720" border="0" cellpadding="3" cellspacing="0" style="">
        <tr>
            <td style="font-weight: bold" width="350" class="ft_st">
                客人姓名：<asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
            </td>
            <td>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                入住日期：<asp:Label ID="lblInDate" runat="server" Text=""></asp:Label>
            </td>
            <td>
                离店日期：<asp:Label ID="lblOutDate" runat="server" Text=""></asp:Label>
            </td>
            <td>
                入住天数：<asp:Label ID="lblInDay" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                房型名称：<asp:Label ID="lblRoomTypeName" runat="server" Text=""></asp:Label>
            </td>
            <td>
                房间数量：<asp:Label ID="lblRoomNum" runat="server" Text=""></asp:Label>
            </td>
            <td>
                入住人数：<asp:Label ID="lblInPeopleNum" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                计划名称：<asp:Label ID="lblPlanName" runat="server" Text=""></asp:Label>
            </td>
            <td>
                最晚到店时间：<asp:Label ID="lblLasterDate" runat="server" Text=""></asp:Label>
            </td>
            <td>
                付款方式：<asp:Label ID="lblPayType" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    ========================================================================================
    <table width="700" border="0" cellpadding="3" cellspacing="0" style="">
        <tr>
            <td rowspan="2">
                <table width="350" border="0" cellpadding="3" cellspacing="0" >
                    <tr>
                        <td>
                            <div class="frame02">
                                <asp:GridView ID="gridViewCSSystemLogDetail" runat="server" 
                                    AutoGenerateColumns="False" BorderStyle="Dashed" 
                                    BackColor="White" AllowPaging="True" BorderWidth="1px" CellPadding="4" CellSpacing="1" 
                                    Width="100%"  DataKeyNames="ID" PageSize="10" CssClass="GView_BodyCSS" 
                                    onrowdatabound="gridViewCSSystemLogDetail_RowDataBound" >
                                    <Columns>
                                        <asp:BoundField DataField="INDATE" HeaderText="日期" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TWOPRICE" HeaderText="价格" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BREAKFAST" HeaderText="早餐" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <RowStyle CssClass="GView_ItemCSS" />
                                    <HeaderStyle CssClass="GView_HeaderCSS" />
                                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td height="78" valign="bottom" style="font-weight: bold">
                <font size="5"><asp:Label ID="lblBookStatus" runat="server" Text=""></asp:Label></font>
            </td>
        </tr>
        <tr>
            <td height="30%">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td valign="bottom" style="font-weight: bold">
                总房价：RMB:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPriceCount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
                担保/取消信息:
            </td>
            <td height="30%">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblGuaDesc" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="lblCxlDesc" runat="server" Text=""></asp:Label>
            </td>
            <td height="30%">
            </td>
        </tr>
    </table>
    <br />
    <table width="700" border="0" cellpadding="3" cellspacing="0" style="border-bottom: #0000 solid 1px">
        <tr>
            <td>
                备注:<asp:Label ID="lbBOOK_REMARK" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ft_st">
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <table width="700" border="0" cellpadding="3" cellspacing="0">
        <%--<tr>
            <td>
                渠道名称：<asp:Label ID="Label24" runat="server" Text="今夜酒店特价"></asp:Label>
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                修改记录：<asp:Label ID="Label25" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    ========================================================================================
    <table width="700" border="0" cellpadding="3" cellspacing="0" >
        <tr>
            <td colspan="5" align="left" style="font-size: 18px">
                确认信息
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    <input type="checkbox" name="checkbox" id="checkbox" />
                    本单确认</label>
            </td>
            <td>
                <input type="checkbox" name="checkbox2" id="checkbox2" />
                本单不确认
            </td>
            <td width="150">
                确认号：
            </td>
            <td width="150">
                确认人：
            </td>
            <td width="150">
                日期：
            </td>
        </tr>
    </table>
    ========================================================================================
    <table width="700" border="0" cellpadding="3" cellspacing="0">
        <tr>
            <td>
                 房控电话：18001633253 / 18001633397 (09:30 - 21:00)<br />
                 传真：021-52393323
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
