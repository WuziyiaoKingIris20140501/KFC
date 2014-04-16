<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SetTicketUseRule.aspx.cs" Title="关联优惠券与规则" Inherits="Ticket_SetTicketUseRule" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script language="javascript" type="text/javascript">

     function ClickEvent(pcode,selIndex) {
         //调用LinkButton的单击事件，btnBindData是LinkButton的ID

         document.getElementById("<%=hidSelectPacgageCode.ClientID%>").value = pcode;
         document.getElementById("<%=hidSelPackRowIndex.ClientID%>").value = selIndex;  
         document.getElementById("<%=btnSelect.ClientID%>").click();
     }

     function ClickTicketEvent(rulecode) {

         document.getElementById("<%=hidSelectRuleCode.ClientID%>").value = rulecode;
         document.getElementById("<%=btnSelTicket.ClientID%>").click();
     }
    
 </script>
    <asp:HiddenField ID="hidSelectPacgageCode" runat="server" Value="" />
    <asp:HiddenField ID="hidSelectRuleCode" runat="server" Value="" />
    <asp:HiddenField ID="hidSelPackRowIndex" runat="server" Value="" />
    <div id="right">
        <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="<%$Resources: PackageList%>" ID="lbRuleTitle" runat="server"></asp:Literal> </li>
        <li>
         <table align="center" border="0" width="100%" class="Table_BodyCSS">
            
            <%--<tr class="RowTitle"><td colspan="5"><asp:Literal Text="<%$Resources: PackageList%>" ID="lbRuleTitle" runat="server"></asp:Literal> </td></tr>--%>           
                <tr>
                <td align="center" colspan="5" class="tdcell" >                  
                        <asp:GridView ID="gridViewPackage" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" Width="100%" EmptyDataText="<%$Resources:MyGlobal,NoDataText %>" DataKeyNames="ID" 
                          onrowdatabound="gridViewPackage_RowDataBound" AllowPaging="True" 
                          onpageindexchanging="gridViewPackage_PageIndexChanging" CssClass="GView_BodyCSS">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                <asp:BoundField DataField="PACKAGECODE" HeaderText="<%$Resources:Code %>" />
                                <asp:BoundField DataField="PACKAGENAME" HeaderText="<%$Resources:Name %>" />
                                <asp:BoundField DataField="AMOUNT" HeaderText="<%$Resources:PackageAllAmount %>" />
                                <asp:BoundField DataField="STARTDATE" HeaderText="<%$Resources:StartDate %>" />
                                <asp:BoundField DataField="ENDDATE" HeaderText="<%$Resources:EndDate %>" />
                                <asp:BoundField DataField="CLIENTCODE" HeaderText="<%$Resources:UserGroup %>" />
                                <asp:BoundField DataField="USECODE" HeaderText="<%$Resources:UsePlatform %>" />
                                <asp:BoundField DataField="CHANELCODE" HeaderText="<%$Resources:UseChannel %>" />
                                <asp:BoundField DataField="CITYID" HeaderText="<%$Resources:UseCityID %>" />
                            </Columns>
                         
                         <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>                        
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />                        
                        <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                        </asp:GridView>
                </td>
            </tr>
                <tr style="width:100%">
               <td  align="left" style="width:25%;" valign="top" class="tdcell">
               <div class="frame01">
                  <ul>
                    <li class="title"><asp:Label ID="Label2" runat="server" Text="<%$Resources:IncludeTicketDetail %>"></asp:Label> </li>
                    <li>
                    <table width="100%">
                    <%--<tr class="RowTitle"><td><asp:Label ID="Label4" runat="server" Text="<%$Resources:IncludeTicketDetail %>"></asp:Label></td></tr>--%>
                    <tr>
                        <td> 
                            <asp:GridView ID="gridViewTicket" runat="server" AutoGenerateColumns="False" 
                                BackColor="White"  CssClass="GView_BodyCSS"
                                Width="100%" EmptyDataText="<%$Resources:MyGlobal,NoDataText %>" DataKeyNames="ID" 
                                onrowdatabound="gridViewTicket_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                     <asp:TemplateField HeaderText="<%$Resources:MyGlobal,SelectText %>">
                                        <ItemTemplate>                                          
                                            <asp:CheckBox ID="checkticketitem" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TICKETCODE" HeaderText="<%$Resources:Code %>" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TICKETCNT" HeaderText="<%$Resources:Leaf %>" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TICKETAMT" HeaderText="<%$Resources:Amount %>" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TICKETRULECODE" HeaderText="<%$Resources:UseRule %>" />
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
                     </li>
                  </ul>
                </div>
                </td>
                          
                <td style="width:75%;background-color:white;"  align="center" valign="top" class="tdcell">
                <div class="frame01">
                  <ul>
                    <li class="title"><asp:Label ID="Label3" runat="server" Text="<%$Resources:RuleList %>"></asp:Label> </li>
                    <li>
                   <table width="100%">
                    <%--<tr class="RowTitle"><td><asp:Label ID="Label1" runat="server" Text="<%$Resources:RuleList %>"></asp:Label></td></tr>--%>
                    <tr>
                        <td>                                      
                            <asp:GridView ID="gridViewRule" runat="server" AutoGenerateColumns="False" 
                            BackColor="White"  CssClass="GView_BodyCSS" Width="100%" 
                                EmptyDataText="<%$ Resources:MyGlobal,NoDataText %>" DataKeyNames="ID" AllowPaging="True" 
                              onpageindexchanging="gridViewRule_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                 <asp:TemplateField HeaderText="<%$Resources:MyGlobal,SelectText %>">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="checkruleitem" runat="server" AutoPostBack="True" 
                                            oncheckedchanged="checkruleitem_CheckedChanged" />
                                    </ItemTemplate>
                                     <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="TICKETRULECODE" HeaderText="<%$Resources:Code %>" >
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TICKETRULENAME" HeaderText="<%$Resources:Name %>" >
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="STARTDATE" HeaderText="<%$Resources:StartDate %>" >
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ENDDATE" HeaderText="<%$Resources:EndDate %>" >
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="STARTTIME"  HeaderText="<%$Resources:StartTime %>" 
                                    Visible="False" >
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ENDTIME" HeaderText="<%$Resources:EndTme %>" 
                                    Visible="False" >
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ORDAMT" HeaderText="<%$Resources:OrderNeedAmout %>" >
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HOTELID" HeaderText="<%$Resources:HotelID %>" >
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CITYID" HeaderText="<%$Resources:CityID %>" >
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TICKETRULEDESC" HeaderText="<%$Resources:RuleDesc %>" >
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="USECODE" HeaderText="使用平台" />
                            </Columns>
                        
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>                        
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />                        
                            <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                            </asp:GridView>

                        <div style="display:none">
                        <asp:Button ID="btnSelect" CssClass="btn primary" runat="server" Text="选中package行" 
                          onclick="btnSelect_Click"   /> 

                          <asp:Button ID="btnSelTicket" CssClass="btn primary" runat="server" Text="选中Ticket行" 
                          onclick="btnSelTicket_Click"   /> 
                          </div>
                       <asp:Button ID="btnSave" CssClass="btn primary" runat="server" Text="<%$Resources:MyGlobal,SaveText %>" onclick="btnSave_Click" />
                          </td>
                     </tr>
                    </table>
                    </li>
                  </ul>
                </div>
                </td>                
            </tr>
        </table>
        </li>
      </ul>
    </div>
</div>
</asp:Content>
