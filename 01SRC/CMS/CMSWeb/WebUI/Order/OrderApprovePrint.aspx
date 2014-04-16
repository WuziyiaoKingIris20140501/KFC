<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderApprovePrint.aspx.cs" 
    Inherits="OrderApprovePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <style type="text/css">
    .GView_BodyCSS 
    {
        mso-font-charset: 134;
        font-family:font-family:黑体, monospace;
        border:1px  #000000 ;
        border-collapse:collapse;
        color:Black;
	    width:100%;
	    border-width:1px;
    }
    .GView_BodyCSS td
    {
        mso-font-charset: 134;
        font-family:font-family:黑体, monospace;
        border:1px  #000000 solid;
        border-collapse:collapse;
        color:Black;
    }

    .GView_BodyCSS th
    {
        mso-font-charset: 134;
        font-family:font-family:黑体, monospace;
        border:1px  #000000 solid;
        border-collapse:collapse;
        FONT-WEIGHT:normal;
    }
    </style>
</head>
<body style="overflow:scroll;overflow-x:hidden" >
    <form id="form1" runat="server">
    <table style="width: 650px">
         <tr style="height:20px">
         <td align="right" style="font-family:黑体, monospace;mso-font-charset:134;height:20px;">
         </td>
         </tr>
        <tr style="height: 55px">
            <td align="right" valign="middle" style="font-size: 24.0pt; font-family: 黑体, monospace;
                mso-font-charset: 134; height: 55px;">
                <table>
                    <tr>
                        <td><img src="../../Images/hotelvp-logo.jpg" alt="logo" /></td>
                        <td style="font-size: 24.0pt; font-family: 黑体, monospace;
                mso-font-charset: 134; height: 55px;">今夜酒店特价 - 入住审核单</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="right" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
                电话：021-52393373<span style='width: 10px'> </span>传真：021-33321996
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 30px;">
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 20px;">
                TO： <asp:Label ID="lbHotelID" runat="server" Text="上海华亭宾馆（ID：2000000）" />
                 <asp:Label ID="lbTo" runat="server" Text="" Visible="false" />
            </td>
        </tr>
        <tr style="height: 30px">
            <td align="left" valign="top" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 30px;border-bottom: 2.0pt double black;">
                <span style='margin-left: 40px'>电话：<asp:Label ID="lbTelTo" runat="server" Text="021-62416900" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;传真：<asp:Label ID="lbFaxTo" runat="server" Text="021-62596435" /></span>
            </td>
        </tr>
<%--        <tr style="height: 3px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: none; border-right: none; border-bottom: 2.0pt double black; border-left: none;
                height: 3px;">&nbsp;
            </td>
        </tr>--%>
        <tr style="height: 25px">
             <td align="left" valign="middle" style="font-family:黑体, monospace;mso-font-charset:134;height:20px;border-bottom: 2.0pt double black;">
			    <table style="width:100%;">
                        <tr style="height:25px;width:100%;" valign="middle">
                            <td align="left" valign="middle" style="font-family:黑体, monospace;mso-font-charset:134;width:10%;">
                                    传真发送时间：<asp:Label ID="lbFaxDate" runat="server" Text="2013-12-15 12:15:45 PM" />
                            </td>
                        </tr>
			    </table>
             </td>
         </tr>
<%--        <tr style="height: 3px">
            <td align="center" valign="top" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top:  2.0pt double black; border-right: none; border-bottom: none; border-left: none;height:3px">&nbsp;
            </td>
        </tr>--%>
        <tr>
            <td>
                <table style="width: 100%;margin-top:5px">
                    <tr style="height: 20px; width: 100%;">
                        <td align="right" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px; width: 15%;">
                            发送人：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px; width: 35%;">
                            <asp:Label ID="lbFrom" runat="server" Text="审核部 杭英" /></td>
                        <td align="right" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px; width: 15%;">
                            联系电话：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px; width: 35%;">
                            <asp:Label ID="lbTelFrom" runat="server" Text="021-64363131*3000" />
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td align="right" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            联系传真：
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                            <asp:Label ID="lbFaxFrom" runat="server" Text="021-33321996" /></td>
                        <td align="right" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                        </td>
                        <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                            height: 20px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width:100%">
            <td style="width:100%">
                <%--<asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="10" CellSpacing="1" Width="100%" EmptyDataText="没有数据" CssClass="GView_BodyCSS"
                    OnRowCreated ="gridViewCSList_RowCreated">
                    <Columns>
                        <asp:BoundField DataField="ORDROOM" HeaderText="<div  style='margin-top:0px;height:5px;'>订单号</div><br/><div style='border-top: none; border-right: none; border-bottom: 0.5pt solid black;
                                border-left: none; width: 117%; height:1px;margin-left:-10px;'></div><div style='margin-top:10px;height:15px;'>房型</div>" HtmlEncode="false" ><ItemStyle HorizontalAlign="Center" Width="22%" Wrap="true"/></asp:BoundField>
                        <asp:BoundField DataField="GUESTNM" HeaderText="客户姓名" ><ItemStyle HorizontalAlign="Center" Width="14%" Wrap="true" /></asp:BoundField>
                        <asp:BoundField DataField="ROOMPR" HeaderText="房价" ><ItemStyle HorizontalAlign="Center" Width="9%" Wrap="true"/></asp:BoundField>
                        <asp:BoundField DataField="ROOMNUM" HeaderText="间数" ><ItemStyle HorizontalAlign="Center" Width="9%" Wrap="true"/></asp:BoundField>
                        <asp:BoundField DataField="INOUTDT" HeaderText="入住-离店" ><ItemStyle HorizontalAlign="Center" Width="19%" Wrap="true"/></asp:BoundField>
                        <asp:BoundField DataField="ROOMID" HeaderText="房号" ><ItemStyle HorizontalAlign="Center" Width="10%" Wrap="true"/></asp:BoundField>
                        <asp:BoundField DataField="APPRNUM" HeaderText="确认号" ><ItemStyle HorizontalAlign="Center" Width="17%" Wrap="true"/></asp:BoundField>
                    </Columns>
                    <HeaderStyle Font-Names ="黑体" Font-Bold="false"/>
                    <RowStyle Font-Names ="黑体" Font-Bold="false" />
                </asp:GridView>--%>

                <div id="dvOrderList" runat="server"></div>
<%--

                <table style="width: 100%; border: 1px black solid; padding: 1px; border-collapse: collapse;border-spacing: 0;">
                    <tr>
                        <td style="width:22%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;" align="center">订单号</td>
                        <td rowspan="2" style="width:14%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;" align="center">客户姓名</td>
                        <td rowspan="2" style="width:9%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;" align="center">房价</td>
                        <td rowspan="2" style="width:9%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;" align="center">间数</td>
                        <td rowspan="2" style="width:19%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;" align="center">入住-离店</td>
                        <td rowspan="2" style="width:10%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;" align="center">房号</td>
                        <td rowspan="2" style="width:17%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;" align="center">确认号</td>
                    </tr>
                    <tr>
                        <td  align="center" style="width:14%;font-family: 黑体, monospace; mso-font-charset: 134;border-right: 1px black solid; border-top: 1px black solid;">房型</td>
                    </tr>
                </table>--%>

            </td>
        </tr>
        
        <tr style="height: 20px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: none; border-right: none; border-bottom: none; border-left: none;height: 20px">
            </td>
        </tr>
        <tr style="height: 68px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 68px;">
                <table style="width: 100%">
                    <tr style="width: 100%; height: 34px">
                        <td style="width: 50%;">
                        </td>
                        <td align="right" style="width:20%;height: 20px;">
                            审核确认人：
                        </td>
                        <td style="width: 30%;" valign="bottom">
                            <div style="border-top: none; border-right: none; border-bottom: 1.0pt solid black;
                                border-left: none; width: 200px; height:100%;">
                            </div>
                        </td>
                    </tr>
                    <tr style="width: 100%; height: 34px">
                        <td style="width: 50%;">
                        </td>
                        <td align="right">
                            审核确认日期：
                        </td>
                        <td valign="bottom">
                            <div style="border-top: none; border-right: none; border-bottom: 1.0pt solid black;
                                border-left: none; width: 200px; height:100%;">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: none; border-right: none; border-bottom: none; border-left: none;height: 20px">
            </td>
        </tr>
<%--        <tr style="height:10px;">
            <td align="center" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                border-top: 2.0pt double black; border-right: none; border-bottom: none; border-left: none;height:10px;">&nbsp;
            </td>
        </tr>--%>
        <tr style="height: 50px">
            <td align="left" valign="middle" style="font-family: 黑体, monospace; mso-font-charset: 134;
                height: 50px;border-top: 2.0pt double black; ">
                <span style="margin-left: 5px">请协助核对我司客户的入住与离店情况，补充房号与确认号等信息。</span><br />
                <span style="margin-left: 5px">核对完成后如有疑问，请及时与我方工作人员联系。感谢合作！</span>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
