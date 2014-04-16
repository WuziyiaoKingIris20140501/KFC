<%@ Page Title="酒店上下线状态查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  CodeFile="HotelPlanSearch.aspx.cs" Inherits="WebUI_Hotel_HotelPlanSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <table align="center" border="0" width="100%" class="Table_BodyCSS"  >   
        <tr class="RowTitle">
            <td colspan="4"><asp:Literal Text="酒店上下线状态查询" ID="lblOnlineTitle" runat="server"></asp:Literal> </td>
        </tr> 
                      
       <tr>
           <td>
           <br />      
           <fieldset>
           <legend title="搜索酒店">搜索酒店</legend>       
           <table align="center" border="0" width="100%" >
            <tr>
                <td  class="tdcell" style="width:10%;text-align:right"  ><asp:Label ID="lblCity" runat="server" Text="选择城市："></asp:Label></td>
                <td class="tdcell" align="left"  style="width:10%" >
                    <asp:DropDownList ID="ddlCity" runat="server">
                    </asp:DropDownList>                
                </td> 

                <td  class="tdcell" style="width:10%;text-align:right"><asp:Label ID="Label1" runat="server" Text="价格代码："></asp:Label></td>
                <td class="tdcell" align="left"  style="width:10%" >
                    <asp:DropDownList ID="ddlLMBAR" runat="server">
                        <asp:ListItem Value="">不限制</asp:ListItem>
                        <asp:ListItem>LMBAR</asp:ListItem>
                        <asp:ListItem>LMBAR2</asp:ListItem>
                    </asp:DropDownList>                
                </td>  
            </tr>
            <tr>       
                <td  class="tdcell" style="width:10%;text-align:right"  ><asp:Label ID="Label2" runat="server" Text="开始日期："></asp:Label></td>
                <td class="tdcell" align="left"  style="width:10%" >
                   <input id="dtStartTime" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/>            
                </td> 

                <td  class="tdcell" style="width:10%;text-align:right"  ><asp:Label ID="Label4" runat="server" Text="结束日期："></asp:Label></td>
                <td class="tdcell" align="left"  style="width:10%" >
                   <input id="dtEndTime" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/>            
                </td> 
            </tr>
              <tr> 
                <td class="tdcell" align="left" colspan="6"> 
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,SearchText %>" OnClientClick="return checkValid();" onclick="btnSearch_Click"/>                     
                </td>
             </tr>
        </table>
           </fieldset>
               <br />            
           </td>            
       </tr> 
       <tr>
           <td>
               <div>
                   <asp:DataList ID="DateList" runat="server" RepeatColumns="14" Width="100%"  RepeatDirection="Horizontal" onitemcommand="DateList_ItemCommand" >
                            <ItemStyle HorizontalAlign="Justify" Width="35px" />
                            <ItemTemplate>               
                            <asp:Button   ID="btnDateSearch" runat="server" CssClass="btn primary" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"id")%> '   CommandName="search"   
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"id")%> ' 
                                    onclick="btnDateSearch_Click"/>  
                      </ItemTemplate>
                    </asp:DataList>          
               </div>       
           </td>
       </tr>
         <tr>
           <td>               
            <div id="divSearchResult" runat="server"></div>
           </td>
         </tr>
         <tr>
            <td>
                <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td align="center" class="tdcell" colspan="4">              
                            <asp:GridView ID="gridViewHotelPlan"  runat="server" AutoGenerateColumns="False" 
                                BackColor="White"  AllowPaging="True" PageSize="200" 
                                CssClass="GView_BodyCSS" onrowdatabound="gridViewHotelPlan_RowDataBound" 
                                onpageindexchanging="gridViewHotelPlan_PageIndexChanging">
                                <Columns>         
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />                
                                    <asp:BoundField DataField="HOTEL_ID" HeaderText="酒店ID" />                      
                                    <asp:BoundField DataField="HOTEL_NAME" HeaderText="酒店名称" />
                                    <asp:BoundField DataField="ROOM_TYPE_NAME" HeaderText="房型名称" />
                                    <asp:BoundField DataField="TWO_PRICE" HeaderText="供应商价格" />                                  
                                    <asp:TemplateField HeaderText="供应商状态">
                                     <ItemTemplate>                                    
                                        <asp:Label ID="lblStatus" runat="server"  Text='<%# Eval("STATUS").ToString()=="1"?"上线":"下线" %>'></asp:Label>                                    
                                    </ItemTemplate>                                    
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="HotelVp状态">
                                        <ItemTemplate>                                            
                                         <asp:DropDownList ID="ddlHotelVPStatus" runat="server" 
                                                SelectedValue='<%# Eval("HOTELVP_STATUS") %>' Enabled="false">
                                                <asp:ListItem Value="">未设置</asp:ListItem>
                                                <asp:ListItem Value="1">上线</asp:ListItem>
                                                <asp:ListItem Value="0">下线</asp:ListItem>
                                            </asp:DropDownList>                                                                            
                                        </ItemTemplate> 
                                                                              
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="APP状态">
                                     <ItemTemplate>                                    
                                        <asp:Label ID="lblAppStatus" runat="server"  Text='<%# Eval("APP_STATUS").ToString()=="1"?"上线":"下线" %>'></asp:Label>                                    
                                    </ItemTemplate>                                    
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RATE_CODE" HeaderText="价格代码" />                                    
                                    <asp:BoundField DataField="GMT_CREATED" HeaderText="最后操作时间" />
                                    <asp:BoundField DataField="CREATOR" HeaderText="最后操作人" />                                    
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
 </table>
  <script language="javascript" type="text/javascript">
      function clearText() {
          document.getElementById("<%=ddlCity.ClientID%>").value = "";
          document.getElementById("<%=ddlLMBAR.ClientID%>").value = "";
          document.getElementById("<%=dtStartTime.ClientID%>").value = "";
          document.getElementById("<%=dtEndTime.ClientID%>").value = "";          
      }

      function checkValid() {
          var cityid = document.getElementById("<%=ddlCity.ClientID%>").value;
          var lmbar = document.getElementById("<%=ddlLMBAR.ClientID%>").value;
          var startdate = document.getElementById("<%=dtStartTime.ClientID%>").value;
          var enddate = document.getElementById("<%=dtEndTime.ClientID%>").value;

//          if (cityid == "") {
//              alert("请选择城市！");
//              document.getElementById("<%=ddlCity.ClientID%>").focus();
//              return false;

//          }
          if (startdate == "" || enddate == "") {
              alert("请选择开始日期和结束日期！");
              document.getElementById("<%=dtStartTime.ClientID%>").focus();
              return false;
          }

          if (startdate > enddate) {
              alert("开始日期不能大于结束日期！");
              document.getElementById("<%=dtStartTime.ClientID%>").focus();
              return false;
          }        
          return true;
      }
    
    </script>
</asp:Content>

