<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TicketPackageDetail.aspx.cs" Inherits="Ticket_TicketPackageDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看领用券包详情</title>  
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
    <div>    
       <table align="center" border="0" width="100%" class="Table_BodyCSS">
        <tr class="RowTitle"><td colspan="4"><asp:Literal Text="<%$Resources: ViewTicketRuleDetail%>" ID="lbRuleTitle" runat="server"></asp:Literal> </td></tr>          
        <tr>
            <td class="tdcell"  style="width:15%"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: TicketName%>"></asp:Literal></td>
            <td class="tdcell" colspan=3>
            <asp:TextBox ID="txtPackageName" runat="server" Width="90%" Height="20px" CssClass="noborder_inactive" ReadOnly="true"></asp:TextBox></td></tr>
        <tr>
            <td class="tdcell" ><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: TicketAllAmount%>"></asp:Literal></td>
            <td class="style1">
                <input id="txtAllAmount" name="txtAllAmount" type="text"  runat="server" class="noborder_inactive" readonly=readonly />
            </td>
            <td class="tdcell">
                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:PackageType %>"/>
            </td>
            <td class="tdcell">
                <asp:Label ID="lbPackageType" runat="server"/>
            </td>
        </tr>
        <tr>
                <td   style="width:15%" class="tdcell" ><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: StartDate%>"></asp:Literal></td>
                <td   style="width:35%" class="tdcell" ><asp:TextBox ID="FromDate" CssClass="noborder_inactive" runat="server" Width="85%" Height="20px" ReadOnly=true></asp:TextBox></td>
                <td   style="width:15%" class="tdcell" ><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources: EndDate%>"></asp:Literal></td>
                <td   style="width:35%" class="tdcell" ><asp:TextBox ID="EndDate" runat="server" CssClass="noborder_inactive" Width="85%" Height="20px" ReadOnly=true></asp:TextBox></td>
        </tr>
        
        <tr><td class="tdcell" ><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources: IncludeTicket%>"></asp:Literal></td>
            <td  colspan="3" class="tdcell"  align="left">
                <table width="100%">
                    <tr><td>                     
                     <asp:GridView ID="gridViewTicket" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" CssClass="GView_BodyCSS"
                            Width="100%" EmptyDataText="<%$ Resources:MyGlobal,NoDataText %>" DataKeyNames="ID">
                            <Columns>
                                <asp:BoundField DataField="TICKETCODE" HeaderText="<%$ Resources:Code%>" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TICKETCNT" HeaderText="<%$ Resources:Count %>" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TICKETAMT" HeaderText="<%$ Resources:Amount %>" >

                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />                        
                            <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                        </asp:GridView>
                       
                        </td>
                     </tr>                 
                </table>
            </td>
        </tr>

      <tr>
            <td class="tdcell" ><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources: CanUseCount%>"></asp:Literal></td>
            <td class="tdcell" ><asp:TextBox ID="txtUserCount" runat="server" CssClass="noborder_inactive"></asp:TextBox></td>

            <td class="tdcell" ><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: CanUserRepCount%>"></asp:Literal></td>
            <td class="tdcell" ><asp:TextBox ID="txtUserRepCount" runat="server" CssClass="noborder_inactive"></asp:TextBox></td>
        </tr>
        <tr><td class="tdcell" ><asp:Literal ID="Literal8" runat="server" Text="<%$ Resources: UsePlatform%>"></asp:Literal></td>
            <td align="left" class="tdcell" >
                <asp:ListBox ID="LBUseCode" runat="server" Width="90%" 
                    SelectionMode="Multiple" Enabled="false">
                    <asp:ListItem Value="">无限制</asp:ListItem>
                    <asp:ListItem Value="IOS">IOS</asp:ListItem>
                    <asp:ListItem>ANDROID</asp:ListItem>
                    <asp:ListItem>WAP</asp:ListItem>
                </asp:ListBox>
            </td>
            <td class="tdcell" ><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources: UserGroup%>"></asp:Literal></td>
            <td class="tdcell" >
                <asp:ListBox ID="LBUserGroup" runat="server" Width="90%" 
                    SelectionMode="Multiple" Enabled="false">
                    <asp:ListItem>下单超过300的用户</asp:ListItem>                   
                </asp:ListBox>
            </td>
            </tr>
        <tr>
            <td class="tdcell" ><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources: UseChannel%>"></asp:Literal></td>
            <td colspan="3" align="left" class="tdcell" >
                <asp:ListBox ID="LBSaleChannel" runat="server" Width="90%" 
                    SelectionMode="Multiple" Enabled="false">
                    <asp:ListItem>HOHTELVP</asp:ListItem>                   
                </asp:ListBox>
            </td>
        </tr>    
        <tr>
            <td class="tdcell" ><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources: UseCity%>"></asp:Literal></td>
            <td colspan="3" align="left" class="tdcell" >
                <input readonly="readonly" class="noborder_inactive" runat="server" type="text" id="txtCityID" name="txtCityID" value="" style="vertical-align:middle; width:90%"/>&nbsp;&nbsp;
            </td>
        </tr>    
        <tr>
        <td colspan=4 align="center" class="tdcell" >
            <input id="btnClose" type="button" value="<%=closeText %>" onclick="javascript:Close();" class="btn"/>
        </td></tr>
    </table>
    
    </div>
</form>
</body>

