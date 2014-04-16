<%@ Page Title="酒店计划上下线设置" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HotelPlanOffline.aspx.cs" Inherits="WebUI_Hotel_HotelPlanOffline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<div>
  <table align="center" border="0" width="100%" class="Table_BodyCSS">

        <tr class="RowTitle"><td colspan="4"><asp:Literal Text="<%$Resources:OnlineTitleLabel%>" ID="lblOnlineTitle" runat="server"></asp:Literal> </td></tr>               
       <tr>
           <td>
           <br />      
           <fieldset>
           <legend title="搜索酒店">搜索酒店</legend>       
           <table align="center" border="0" width="100%" >
            <tr>
                <td  class="tdcell" style="width:10%;text-align:right"  ><asp:Label ID="lblCity" runat="server" Text="<%$Resources:CityLabel %>"></asp:Label></td>
                <td class="tdcell" align="left"  style="width:15%" >
                    <asp:DropDownList ID="ddlCity" runat="server">
                    </asp:DropDownList>                
                </td> 

                 <td  class="tdcell" style="width:10%;text-align:right"  ><asp:Label ID="Label1" runat="server" Text="价格代码">价格代码</asp:Label></td>
                <td class="tdcell" align="left"  style="width:15%" >
                    <asp:DropDownList ID="ddlLMBAR" runat="server">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem>LMBAR</asp:ListItem>
                        <asp:ListItem>LMBAR2</asp:ListItem>
                    </asp:DropDownList>                
                </td>           
                <td  class="tdcell" style="width:10%;text-align:right"  ><asp:Label ID="Label2" runat="server" Text="选择日期："></asp:Label></td>
                <td class="tdcell" align="left"  style="width:15%" >
                   <input id="dtEffectDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/>            
                </td> 
                
            </tr>
              <tr> 
                <td class="tdcell" align="left" colspan="6"> 
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,SearchText %>" onclick="btnSearch_Click"/>                     
                </td>
             </tr>
             <tr> 
                <td class="tdcell" align="left" colspan="6"> 
                    <div id="divSearchResult" runat="server"></div>
                </td>

             </tr>

        </table>
           </fieldset>
               <br />            
           </td>            
       </tr>  
       <tr>
            <td> 
            <fieldset>
            <legend title="酒店控制">酒店控制</legend>  
                <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td align="center" class="tdcell" colspan="4">              
                            <asp:GridView ID="gridViewHotelPlan"  runat="server" AutoGenerateColumns="False" 
                                BackColor="White"  AllowPaging="True" PageSize="200" 
                                CssClass="GView_BodyCSS" onrowdatabound="gridViewHotelPlan_RowDataBound" 
                                onpageindexchanging="gridViewHotelPlan_PageIndexChanging">
                                <Columns>         
                                    <asp:BoundField DataField="ID" HeaderText="ID" >                
                                    <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HOTEL_ID" HeaderText="酒店ID" >                      
                                    <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HOTEL_NAME" HeaderText="酒店名称" >
                                    <ItemStyle Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ROOM_TYPE_NAME" HeaderText="房型名称" >
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ROOM_TYPE_CODE" HeaderText="房型代码">                                    
                                    <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TWO_PRICE" HeaderText="供应商价格" >
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="信任酒店">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lblAutoTrust" runat="server"  Text='<%# Eval("AUTO_TRUST").ToString()=="1"?"是":"否" %>'></asp:Label>                                    
                                    </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="供应商状态">
                                     <ItemTemplate>                                    
                                        <asp:Label ID="lblStatus" runat="server"  Text='<%# Eval("STATUS").ToString()=="1"?"上线":"下线" %>'></asp:Label>                                    
                                    </ItemTemplate>                                    
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlHotelVPStatus" runat="server" 
                                                SelectedValue='<%# Eval("HOTELVP_STATUS") %>'>
                                                <asp:ListItem Value="">未设置</asp:ListItem>
                                                <asp:ListItem Value="1">上线</asp:ListItem>
                                                <asp:ListItem Value="0">下线</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label3" runat="server" Text="HotelVP状态"></asp:Label><br />
                                            <asp:Button ID="btnSyncStatus" runat="server" CssClass="btn primary" Enabled="false" Text="同步供应商状态" 
                                                onclick="btnSyncStatus_Click" />
                                        </HeaderTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>

                                  <%--  <asp:BoundField DataField="APP_STATUS" HeaderText="APP状态" >
                                    <ItemStyle Width="5%" />
                                    </asp:BoundField>--%>

                                     <asp:TemplateField HeaderText="APP状态">                                     
                                     <ItemTemplate>                                    
                                        <asp:Label ID="lblAppStatus" runat="server"  Text='<%# Eval("APP_STATUS").ToString()=="1"?"上线":"下线" %>'></asp:Label>                                    
                                    </ItemTemplate> 
                                                                     
                                        <%--<ItemTemplate>
                                            <asp:DropDownList ID="ddlAPPStatus" runat="server" 
                                                SelectedValue='<%# Eval("APP_STATUS") %>'>
                                                <asp:ListItem Value="">未设置</asp:ListItem>
                                                <asp:ListItem Value="1">上线</asp:ListItem>
                                                <asp:ListItem Value="0">下线</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate> --%>                                                                          
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="RATE_CODE" HeaderText="价格代码" >                                    
                                    <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GMT_CREATED" HeaderText="最后操作时间" >
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CREATOR" HeaderText="最后操作人" >                                    
                                    <ItemStyle Width="10%" />
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
                    <tr>
                        <td class="tdcell"><asp:Label ID="lblStartDate" runat="server" Text="开始日期："></asp:Label><div style="display:none"><asp:TextBox ID="txtYestoday" runat="server"></asp:TextBox><asp:TextBox ID="txtLastTwoWeek" runat="server"></asp:TextBox></div></td><td class="tdcell"><input id="dtStartDate" class="Wdate" type="text"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_txtYestoday\')}',maxDate:'2020-10-01'})" runat="server"/></td>
                        <td class="tdcell"><asp:Label ID="lblEndDate" runat="server" Text="结束日期："></asp:Label></td><td class="tdcell"><input id="dtEndDate" class="Wdate" type="text"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_txtYestoday\')}',maxDate:'#F{$dp.$D(\'MainContent_txtLastTwoWeek\')}'})" runat="server"/></td>
                    </tr>
                    <tr >       
                        <td  class="tdcell" align="center" colspan=4>                           
                           <div id="divEdit" runat="server" style="display:block"><asp:Button ID="btnEdit" runat="server" CssClass="btn primary" Text="编辑" onclick="btnEdit_Click"/></div>
                           <div id="divSave" style="display:none" runat="server">                               
                                <asp:Button ID="btnReset" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,ResetText %>" OnClientClick="return alertResetMsg();" onclick="btnReset_Click"/> 
                                &nbsp;&nbsp;&nbsp;&nbsp;                                    
                                <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,SaveText %>" OnClientClick="return alertSaveMsg();" onclick="btnSave_Click"/> 
                           </div>
                         </td>        
                    </tr> 
                </table>  
            </fieldset>         
            </td>       
       </tr>           
      
       
        
 </table>

</div> 
  <script language="javascript" type="text/javascript">
      function alertResetMsg() 
      {
          if (window.confirm("你确定要重置酒店计划信息吗？") == false) {
              return false;
          }
          return true;
      }

      function alertSaveMsg() {
          if (window.confirm("你确定要保存设置的酒店计划吗？") == false) 
          {
              return false;
          }

          var startdate = document.getElementById("<%=dtStartDate.ClientID%>").value;
          var enddate = document.getElementById("<%=dtEndDate.ClientID%>").value;
          if (startdate == "" || enddate == "") {
              alert("请选择开始日期和结束日期！");
              document.getElementById("<%=dtStartDate.ClientID%>").focus();
              return false;
          }

          if (startdate > enddate) {
              alert("开始日期不能大于结束日期！");
              document.getElementById("<%=dtStartDate.ClientID%>").focus();
              return false;
          }       

          return true;
      }
  </script>
</asp:Content>

