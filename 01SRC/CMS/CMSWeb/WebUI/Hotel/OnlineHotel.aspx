<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OnlineHotel.aspx.cs" Inherits="WebUI_Hotel_OnlineHotel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
  <ContentTemplate>
  <table align="center" border="0" width="100%" class="Table_BodyCSS">
        <tr class="RowTitle"><td colspan=2><asp:Literal Text="<%$Resources:OnlineTitleLabel%>" ID="lblOnlineTitle" runat="server"></asp:Literal> </td></tr>

       <tr>
        <td class="tdcell" style="width:15%"><asp:Label ID="lblHotelID" runat="server" Text="<%$Resources:HotelIDLabel%>"></asp:Label></td>
        <td class="tdcell"><asp:TextBox ID="txtHotelID" runat="server" Width="33%"  MaxLength="10"></asp:TextBox></td>
       </tr>

      <tr>
        <td class="tdcell" style="width:15%"><asp:Label ID="lblHotelName" runat="server" Text="<%$Resources:HotelNameLabel%>"></asp:Label></td>
        <td class="tdcell"><asp:TextBox ID="txtHotelName" runat="server" Width="33%" MaxLength="50"></asp:TextBox></td>
       </tr>

       <tr><td  class="tdcell" ><asp:Label ID="lblCity" runat="server" Text="<%$Resources:CityLabel %>"></asp:Label></td>
            <td class="tdcell" align="left">
                <asp:DropDownList ID="ddlCity" runat="server">
                </asp:DropDownList>                
            </td>
       </tr>
       <tr>
            <td  class="tdcell" ><asp:Label ID="lblStatus" runat="server" Text="<%$Resources:HotelOnlineStatusLabel %>"></asp:Label></td>
           <td class="tdcell" align="left"> 
            <asp:RadioButtonList ID="radioListStatus" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="active" Text="<%$Resources:OnlineLabel %>"></asp:ListItem>
            <asp:ListItem Value="close" Text="<%$Resources:OfflineLabel %>"></asp:ListItem>           
            <asp:ListItem Value="" Text="<%$ Resources:MyGlobal,NoLimitText %>" Selected></asp:ListItem>           
            </asp:RadioButtonList> 
            </td>
       </tr>
      <%-- <tr>
            <td  class="tdcell" ><asp:Label ID="lblIsLM" runat="server" Text="<%$Resources:IsLMLabel %>"></asp:Label></td>
           <td class="tdcell" align="left"> 
            <asp:RadioButtonList ID="radioListLMStatus" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="1" Text="<%$Resources:LMHotelLabel %>"></asp:ListItem>
            <asp:ListItem Value="0" Text="<%$Resources:NotLMHotelLabel %>"></asp:ListItem>           
            </asp:RadioButtonList> 
            </td>
       </tr>  --%>
           
       <tr>
        <td></td>
        <td  class="tdcell" align="left"> 
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,SearchText %>" OnClientClick="return checkValid();" onclick="btnSearch_Click"/> 
            <input type="button" value="<%=ResetText %>" id="btnClear" class="btn" onclick="clearText();" />          
        </td>        
        </tr> 
    </table>
  </ContentTemplate>
  </asp:UpdatePanel>
    <br />  
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">           
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <asp:GridView ID="gridViewOnline"  runat="server" AutoGenerateColumns="False" 
                    BackColor="White" EmptyDataText="<%$ Resources:MyGlobal,NoDataText %>" DataKeyNames="PROP"                         
                        AllowPaging="True" PageSize="16" CssClass="GView_BodyCSS" 
                    onpageindexchanging="gridViewOnline_PageIndexChanging" 
                    onrowcancelingedit="gridViewOnline_RowCancelingEdit" 
                    onrowediting="gridViewOnline_RowEditing" 
                    onrowupdating="gridViewOnline_RowUpdating">
                    <Columns>                       
                        <asp:TemplateField HeaderText="<%$ Resources:HotelIDLabel%>">
                            <ItemTemplate>
                                <asp:Label ID="lblProp" runat="server" Text='<%# Eval("PROP") %>'></asp:Label>
                            </ItemTemplate>
                        <ItemStyle Width="5%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$ Resources:HotelNameLabel%>">
                            <ItemTemplate>
                                <asp:Label ID="lblHotelName" runat="server" Text='<%# Eval("PROP_NAME_ZH") %>'></asp:Label>
                            </ItemTemplate>
                        <ItemStyle Width="25%" />
                        </asp:TemplateField>                                             

                         <asp:TemplateField HeaderText="<%$ Resources:TradeArealabel%>">
                            <ItemTemplate>
                                <asp:Label ID="lblTradeArea" runat="server" Text='<%# Eval("TRADEAREA_ZH") %>'></asp:Label>
                            </ItemTemplate>
                        <ItemStyle Width="15%" />
                        </asp:TemplateField>
                                                                    
                        <asp:TemplateField HeaderText="<%$ Resources:CityLabel%>">
                            <ItemTemplate>
                                <asp:Label ID="lblCityID" runat="server" Text='<%# Eval("CITYID") %>'></asp:Label>
                            </ItemTemplate>
                        <ItemStyle Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$ Resources:HotelAddressLabel%>">
                            <ItemTemplate>
                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS1_ZH") %>'></asp:Label>
                            </ItemTemplate>
                        <ItemStyle Width="25%" />
                        </asp:TemplateField> 
                    
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:IsOnlineLabel %>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlOnline" runat="server" Enabled="False" 
                                    SelectedValue='<%# Eval("status") %>'>
                                    <asp:ListItem Text="<%$Resources:OnlineLabel %>" Value="active"></asp:ListItem>
                                    <asp:ListItem Text="<%$Resources:OfflineLabel %>" Value="close"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#Eval("status").ToString() == "active" ? strOnlineLabel:strOfflineLabel %>                                
                            </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>

                        <asp:CommandField HeaderText="<%$ Resources:MyGlobal,EditText%>"  ShowEditButton="True" ShowHeader="True" />

                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />                        
                    <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                       

                </asp:GridView>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        function clearText() {
            document.getElementById("<%=txtHotelID.ClientID%>").value = "";
            document.getElementById("<%=txtHotelName.ClientID%>").value = "";
            document.getElementById("<%=ddlCity.ClientID%>").value = "";

            var RadioTable = document.getElementById("<%=radioListStatus.ClientID%>");
            var RadioInput = RadioTable.getElementsByTagName("INPUT");
            if (RadioInput.length > 1)
            { RadioInput[2].checked = true; }
        }

        function checkValid() {
            var hotelid = document.getElementById("<%=txtHotelID.ClientID%>").value;
            if (hotelid != "") {
                if (isNaN(hotelid)) {
                    alert("<%=PromptHotelIDIsNaN%>");
                    return false;
                }
            }
            return true;
        }
    
    </script>
</asp:Content>

