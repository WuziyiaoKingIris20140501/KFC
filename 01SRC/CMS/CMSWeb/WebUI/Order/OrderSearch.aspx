<%@ Page Title="订单查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OrderSearch.aspx.cs" Inherits="WebUI_Order_OrderSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" border="0" width="100%" class="Table_BodyCSS">
        <tr class="RowTitle"><td colspan="2"><asp:Literal Text="<%$ Resources:OrderSearchTitle %>" ID="lblOnlineTitle" runat="server"></asp:Literal> </td></tr>
        <tr><td  class="tdcell" ><asp:Label ID="lblCity" runat="server" Text="<%$ Resources:lblSelectCity %>"></asp:Label></td>
            <td class="tdcell" align="left">
                <asp:DropDownList ID="ddlCity" runat="server">
                </asp:DropDownList>                
            </td>
       </tr>
       <tr>
            <td  class="tdcell" ><asp:Label ID="lblStatus" runat="server" Text="<%$ Resources:lblOrderDate %>"></asp:Label></td>
            <td class="tdcell" style="width:85%;"  colspan="3">                      
           <asp:Label ID="lblFrom" runat="server" Text="<%$Resources:lblFrom%>"></asp:Label><input id="dtStartTime" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dtEndTime\')||\'2020-10-01\'}'})"  runat="server"/> 
           <asp:Label ID="lblEndDate" runat="server" Text="<%$Resources:lblTo %>"></asp:Label><input id="dtEndTime" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dtStartTime\')||\'2020-10-01\'}'})" runat="server"/>                      
           </td>
       </tr>
       <tr>
        <td class="tdcell" style="width:15%"><asp:Label ID="Label1" runat="server" Text="<%$Resources:lblOrderStatus%>"></asp:Label></td>
        <td class="tdcell" style="width:85%; text-align:left">
            <asp:RadioButtonList ID="radioListBookStatus" runat="server" RepeatDirection="Horizontal">                     
            </asp:RadioButtonList>
        </td>
        </tr>
        <tr>
        <td class="tdcell" style="width:15%"><asp:Label ID="Label2" runat="server" Text="<%$Resources:lblPayStatus%>"></asp:Label></td>
        <td class="tdcell" style="width:85%; text-align:left">
            <asp:RadioButtonList ID="radioListPayStatus" runat="server" RepeatDirection="Horizontal">                    
            </asp:RadioButtonList>
        </td>
       </tr>

       <tr>
        <td class="tdcell" style="width:15%"><asp:Label ID="lblHotelID" runat="server" Text="<%$Resources:lblHotelID%>"></asp:Label></td>
        <td class="tdcell"><asp:TextBox ID="txtHotelID" runat="server" Width="33%"  MaxLength="10"></asp:TextBox></td> 
        </tr>
        <tr>      
        <td class="tdcell" style="width:15%"><asp:Label ID="lblHotelName" runat="server" Text="<%$Resources:lblHotelName%>"></asp:Label></td>
        <td class="tdcell"><asp:TextBox ID="txtHotelName" runat="server" Width="33%" MaxLength="50"></asp:TextBox></td>
       </tr>

       <tr>      
        <td class="tdcell" style="width:15%"><asp:Label ID="Label3" runat="server" Text="<%$Resources:lblOrderNo%>"></asp:Label></td>
        <td class="tdcell"><asp:TextBox ID="txtOrderID" runat="server" Width="33%" MaxLength="50"></asp:TextBox></td>
       </tr>
       
       <tr>
        <td></td>
        <td  class="tdcell" align="left"> 
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,SearchText %>" OnClientClick="return checkValid();" onclick="btnSearch_Click"/> 
            <input type="button" value="<%=lblReset %>" id="btnClear" class="btn" onclick="clearText();" />          
            <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="<%$Resources:lblExportExcel%>"  onclick="btnExport_Click"/> 
        </td>        
        </tr> 
    </table> 
    <br />  
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">           
        <tr>
            <td align="center">              
                <asp:GridView ID="gridViewOrder"  runat="server" AutoGenerateColumns="False" 
                    BackColor="White" EmptyDataText="<%$ Resources:MyGlobal,NoDataText %>"                 
                        AllowPaging="True" PageSize="20" CssClass="GView_BodyCSS" 
                    onpageindexchanging="gridViewOrder_PageIndexChanging">
                    <Columns>                        
                        <asp:TemplateField HeaderText="查看使用规则">
                            <ItemTemplate>                           
                                <a onclick="OpenWnd('../DBQuery/LmSystemLogDetailPageByNew.aspx?FOGID=<%#Eval("FOG_ORDER_NUM").ToString() %>','订单详情')" href="#" ><asp:Label ID="Label5" runat="server"  Text='<%#Eval("FOG_ORDER_NUM").ToString()%>'></asp:Label></a>                                  
                                </ItemTemplate>                                    
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>
                       <%-- <asp:BoundField DataField="FOG_ORDER_NUM" HeaderText="<%$ Resources:lblOrderNo%>" /> --%>
                        <asp:BoundField DataField="PRICE_CODE" HeaderText="<%$ Resources:lblPriceCode%>" />                     
                        <asp:BoundField DataField="CITY_ID" HeaderText="<%$ Resources:lblCityID%>" />
                        <asp:BoundField DataField="HOTEL_NAME" HeaderText="<%$ Resources:lblHotelName%>" />
                        <asp:BoundField DataField="ROOM_TYPE_NAME" HeaderText="<%$ Resources:lblRoomType%>" />
                        <asp:BoundField DataField="IN_DATE" HeaderText="<%$ Resources:lblInDate%>" />
                        <asp:BoundField DataField="GUEST_NAMES" HeaderText="<%$ Resources:lblGuestName%>" />
                        <asp:BoundField DataField="CONTACT_TEL" HeaderText="<%$ Resources:lblContactTel%>" />
                        <asp:BoundField DataField="BOOK_STATUS" HeaderText="<%$ Resources:lblOrderStatus%>" />
                         <asp:BoundField DataField="PAY_STATUS" HeaderText="<%$ Resources:lblPayStatus%>" />
                        <asp:BoundField DataField="BOOK_SOURCE" HeaderText="<%$ Resources:lblOrderChannel%>" />
                        <asp:BoundField DataField="BOOK_TOTAL_PRICE" HeaderText="<%$ Resources:lblOrderAmt%>" />
                        <asp:BoundField DataField="TICKET_AMOUNT" HeaderText="<%$ Resources:lblUseTicketAmt%>" />
                        <asp:BoundField DataField="PROMOTION_AMOUNT" HeaderText="<%$ Resources:lbPromotionAmt%>" />
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

    <script language="javascript" type="text/javascript">
        function clearText() {
            document.getElementById("<%=txtHotelID.ClientID%>").value = "";
            document.getElementById("<%=txtHotelName.ClientID%>").value = "";
            document.getElementById("<%=dtStartTime.ClientID%>").value = "";
            document.getElementById("<%=dtEndTime.ClientID%>").value = "";
            document.getElementById("<%=ddlCity.ClientID%>").value = "";
            document.getElementById("<%=txtOrderID.ClientID%>").value = "";            
        }

        function checkValid() 
        {
            var cityid = document.getElementById("<%=ddlCity.ClientID%>").value;
            var startdate = document.getElementById("<%=dtStartTime.ClientID%>").value;
            var enddate = document.getElementById("<%=dtEndTime.ClientID%>").value;
            var hotelid = document.getElementById("<%=txtHotelID.ClientID%>").value;
            var orderid = document.getElementById("<%=txtOrderID.ClientID%>").value;

            if (startdate == "" && enddate == "") {
                alert("<%=lblSelectDate %>");
                document.getElementById("<%=dtStartTime.ClientID%>").focus();
                return false;
            }


            if (startdate >enddate) {
                alert("<%=PromptStartDateMustGreaterThanEnd %>");
                document.getElementById("<%=dtStartTime.ClientID%>").focus();
                return false;
            }

            if (hotelid != "") {
                if (isNaN(hotelid) == true) {
                    alert("<%=PromptHotelIDMustIsNumber %>");
                    document.getElementById("<%=txtHotelID.ClientID%>").focus();
                    return false;
                }
            }

            if (orderid != "") {
                if (isNaN(orderid) == true) {
                    alert("<%=PromptOrderIDMustIsNumber %>");
                    document.getElementById("<%=txtOrderID.ClientID%>").focus();
                    return false;
                }
            }

            return true;
        }
    
    </script>
</asp:Content>

