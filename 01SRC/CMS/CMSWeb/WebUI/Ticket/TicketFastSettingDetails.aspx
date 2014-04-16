<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TicketFastSettingDetails.aspx.cs"
    Inherits="WebUI_Ticket_TicketFastSettingDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看优惠券详细</title>
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript">
        function Close() {
            window.opener = null;
            window.open('', '_self');
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="right">
        <table align="center" border="0" width="100%" class="Table_BodyCSS">
            <tr class="RowTitle">
                <td colspan="4">
                    <asp:Literal Text="优惠券详细" ID="lbRuleTitle" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    <asp:Label ID="Lable1" runat="server" Text="名称:"></asp:Label>
                </td>
                <td colspan="3" style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtRuleName" runat="server" Width="96%" MaxLength="25" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    优惠券类型：
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtPackageType" runat="server" Width="96%" MaxLength="25" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="tdcell">
                    最低订单金额：
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtOrdAmt" runat="server" ReadOnly="true"></asp:TextBox>元
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    领用券总金额：</td>
                <td colspan="3" style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtAllAmount" runat="server" Text="" MaxLength="10" 
                        ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdcell" style="width: 15%;">
                    最早可使用日期:
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <input id="fromDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                        runat="server" readonly="readonly" />
                </td>
                <td class="tdcell" style="width: 15%;">
                    最晚可使用日期:
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <input id="endDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_fromDate\')}'})"
                        runat="server" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    最早可领用日期:
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <input id="recipientFormDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                        runat="server" readonly="readonly" />
                </td>
                <td class="tdcell">
                    最晚可领用日期：
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <input id="recipientEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_recipientFormDate\')}'})"
                        runat="server" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    <asp:Literal ID="Literal6" runat="server" Text="优惠券金额："></asp:Literal>
                </td>
                <td colspan="3" class="tdcell" align="left">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gridViewTicket" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    CssClass="GView_BodyCSS" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID">
                                    <Columns>
                                        <asp:BoundField DataField="TICKETCODE" HeaderText="代码">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TICKETCNT" HeaderText="张数">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TICKETAMT" HeaderText="金额">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <RowStyle CssClass="GView_ItemCSS" />
                                    <HeaderStyle CssClass="GView_HeaderCSS" />
                                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    不同用户可领用次数：
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtUserCount" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="tdcell">
                    同一用户可领用次数：
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtUserRepCount" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    优惠券描述(展示在客户端)：
                </td>
                <td colspan="3" style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:TextBox ID="txtRuleDesc" runat="server" TextMode="MultiLine" Width="96%" Rows="3"
                        MaxLength="500" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    销售渠道:
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBSaleChannel" runat="server" Width="96%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="HOTELVP">HOTELVP</asp:ListItem>
                    </asp:ListBox>
                </td>
                <td class="tdcell">
                    发放渠道：
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBSaleChanel" runat="server" Width="90%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem>HOTELVP</asp:ListItem>
                    </asp:ListBox>
                </td>
            </tr>
            <tr>
                <td class="tdcell">
                    使用平台：
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBUsePlatForm" runat="server" Width="96%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="IOS">IOS</asp:ListItem>
                        <asp:ListItem>ANDROID</asp:ListItem>
                        <asp:ListItem>WAP</asp:ListItem>
                    </asp:ListBox>
                </td>
                <td class="tdcell">
                    领用平台限制:
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBUsePlatFormListBox" runat="server" Width="80%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem Value="">无限制</asp:ListItem>
                        <asp:ListItem Value="IOS">IOS</asp:ListItem>
                        <asp:ListItem>ANDROID</asp:ListItem>
                        <asp:ListItem>WAP</asp:ListItem>
                    </asp:ListBox>
                </td>
            </tr>
            <tr class="tdcell">
                <td class="tdcell">
                    价格代码：
                </td>
                <td style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                    <asp:ListBox ID="LBPriceCode" runat="server" Width="96%" Height="80px" SelectionMode="Multiple">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem Value="LMBAR">LMBAR</asp:ListItem>
                        <asp:ListItem Value="LMBAR2">LMBAR2</asp:ListItem>
                        <asp:ListItem Value="BAR">BAR</asp:ListItem>
                        <asp:ListItem Value="BARB">BARB</asp:ListItem>
                    </asp:ListBox>
                </td>
                <td class="tdcell">
                </td>
                <td  style="border:1px #e6e5e5 solid;height:20px;font-family:Arial;color:Black;word-break:break-all;">
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" class="tdcell">
                    <input id="btnClose" type="button" value="关闭" class="btn" onclick="javascript:Close();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
