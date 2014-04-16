<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayTicketRule.aspx.cs" Inherits="Ticket_DisplayTicketRule"  Title="查看规则详情" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">--%>
<head id="Head1" runat="server">
    <title>查看规则详情</title> 
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
    <table align="center" class="Table_BodyCSS">
        <tr class="RowTitle"><td colspan="4"><asp:Literal Text="<%$Resources: RuleDetailLabel%>" ID="lbRuleTitle" runat="server"></asp:Literal> </td></tr>    
       <tr><td class="tdcell"><asp:Literal ID="Literal2" runat="server" Text="<%$Resources:RuleNameLabel %>"></asp:Literal></td><td colspan="3"><asp:TextBox ID="txtRuleName" CssClass="noborder_inactive" runat="server" Width="99%"  ReadOnly="true"></asp:TextBox></td></tr>
       <tr><td class="tdcell" style="width:15%"><asp:Literal ID="Literal3" runat="server" Text="<%$Resources:StartDateLabel %>"></asp:Literal></td>
            <td class="tdcell" style="width:35%">       
                <asp:TextBox ID="fromDate" runat="server" CssClass="noborder_inactive" Width="99%"  ReadOnly="true"></asp:TextBox>       
            </td>
           <td class="tdcell" style="width:15%"><asp:Literal ID="Literal4" runat="server" Text="<%$Resources:EndDateLabel %>"></asp:Literal></td><td class="style1" style="width:35%">           
                <asp:TextBox ID="endDate" runat="server" Width="99%" CssClass="noborder_inactive"  ReadOnly="true"></asp:TextBox>
           </td>
       </tr>
       <tr><td class="tdcell"><asp:Literal ID="Literal5" runat="server" Text="<%$Resources:StartTimeLabel %>"></asp:Literal></td>
            <td class="tdcell"><asp:TextBox ID="txtStartTime" runat="server" CssClass="noborder_inactive" Width="99%" ReadOnly="true"></asp:TextBox></td>
            <td class="tdcell"><asp:Literal ID="Literal6" runat="server" Text="<%$Resources:EndTimeLabel %>"></asp:Literal></td><td> <asp:TextBox ID="txtEndTime" runat="server" CssClass="noborder_inactive" Width="99%" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
            <td class="tdcell"><asp:Literal ID="Literal7" runat="server" Text="<%$Resources:OrderAmountLabel %>"></asp:Literal></td>
            <td class="tdcell"> <asp:TextBox ID="txtOrdAmt" runat="server" CssClass="noborder_inactive" ReadOnly="true"></asp:TextBox></td>
            <td class="tdcell"><asp:Literal ID="Literal8" runat="server" Text="<%$Resources:RelateCityLabel %>"></asp:Literal></td>
            <td class="tdcell"><asp:DropDownList ID="cityid" CssClass="noborder_inactive" runat="server"></asp:DropDownList></td>
       </tr>        
         <tr>
           <td  class="tdcell" >
            <asp:Label ID="Label1" runat="server" Text="销售渠道"></asp:Label></td>
            <td align="left"  class="tdcell" >
                <asp:ListBox ID="LBSaleChannel" runat="server" Width="99%" CssClass="noborder_inactive"
                    SelectionMode="Multiple">
                    <asp:ListItem Value="">不限制</asp:ListItem>
                    <asp:ListItem Value="HOTELVP">HOTELVP</asp:ListItem>                    
                </asp:ListBox>
            </td>
             <td class="tdcell"><asp:Label ID="lblPriceCode" runat="server" Text="价格代码"></asp:Label></td>
            <td align="left" class="tdcell">
                 <asp:ListBox ID="LBPriceCode" runat="server" Width="96%" Height="80px"
                    SelectionMode="Multiple">
                    <asp:ListItem Value="">不限制</asp:ListItem>
                    <asp:ListItem Value="LMBAR">LMBAR</asp:ListItem>
                    <asp:ListItem Value="LMBAR2">LMBAR2</asp:ListItem>
                    <asp:ListItem Value="BAR">BAR</asp:ListItem>
                    <asp:ListItem Value="BARB">BARB</asp:ListItem>
                </asp:ListBox>
            </td>
        </tr>
       <tr><td class="tdcell"><asp:Literal ID="Literal9" runat="server" Text="<%$Resources:RelateHotelLabel %>"></asp:Literal></td>
            <td colspan="3" class="tdcell">                
                <input readonly="readonly" runat="server" type="text" id="txtHotelName" name="txtHotelName" value="" Class="noborder_inactive" style="vertical-align:middle; width:99%; height:50px"/>                   
                <input type="hidden" id="txthotelid" name="txthotelid" runat="server" />                
                &nbsp;
                </td>
        </tr>  
             
        <tr><td class="tdcell"><asp:Literal ID="Literal10" runat="server" Text="<%$Resources:RelateUsePlatForm %>"></asp:Literal></td>
            <td align="left" class="tdcell">
                <asp:ListBox ID="LBUseCode"  runat="server" Width="99%" CssClass="noborder_inactive"
                    SelectionMode="Multiple">
                    <asp:ListItem Value="IOS">IOS</asp:ListItem>
                    <asp:ListItem>ANDROID</asp:ListItem>
                    <asp:ListItem>WAP</asp:ListItem>
                </asp:ListBox>
            </td>
            <td class="tdcell"><asp:Literal ID="Literal11" runat="server" Text="<%$Resources:UserGroupLabel %>"></asp:Literal></td>
            <td class="tdcell">
                <asp:ListBox ID="LBUserGroup" runat="server" Width="99%" 
                    SelectionMode="Multiple" CssClass="noborder_inactive">
                    <asp:ListItem>116114</asp:ListItem>
                    <asp:ListItem>解放日报</asp:ListItem>
                    <asp:ListItem>其他</asp:ListItem>
                </asp:ListBox>
            </td>
            </tr>

        <tr><td class="tdcell"><asp:Literal ID="Literal12" runat="server" Text="<%$Resources:RuleDescLabel %>"></asp:Literal></td>
            <td colspan="3" class="tdcell"><asp:TextBox ID="txtRuleDesc" runat="server" TextMode="MultiLine" 
                    Width="99%" Rows="8" ReadOnly="true" CssClass="noborder_inactive"></asp:TextBox></td>
        </tr>
        <tr><td colspan=4 align="center" class="tdcell"> <input id="btnClose" class="btn" type="button" value="<%=closeText %>" onclick="javascript:Close();" /></td></tr>
    </table>
    </form>
</body>
