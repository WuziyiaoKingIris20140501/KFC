<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelPlanDetail.aspx.cs" Inherits="WebUI_Hotel_HotelPlanDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>酒店计划审核详情</title>
<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>  
<meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
 <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
  <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
  <script language="javascript" type="text/javascript">
      function checkOldInput() 
      {
          if (window.confirm("你确定要保存原值吗？") == false) {
              return false;
          } 
          return true;
      }

      function checkNewInput() 
      {
          if (window.confirm("你确定要标记已处理吗？") == false) {
              return false;
          }
          return true;
      }

      //关闭当前页面
      function closeWin() {
          window.opener.location.href = window.opener.location.href;      
          window.opener = null;
          window.close();      
      }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>

    <table style="width:100%;"  class="Table_BodyCSS" >
        <tr class="RowTitle"><td colspan="2"><asp:Literal Text="酒店计划审核" ID="lblPlanExam" runat="server"></asp:Literal> </td></tr>
          <tr>
            <td align="center" class="tdcell" colspan="2"> 
            <input type="hidden" id="hidplanid" name="hidplanid" value="" runat="server" />
            <fieldset>
                <legend title="审核单处理状态">审核单处理状态</legend>
                <table align="center" border="0" width="100%" >
                    <tr>
                        <td>
                            <asp:GridView ID="gridViewHotelPlanExam"  runat="server" AutoGenerateColumns="False" 
                                BackColor="White"  AllowPaging="True" PageSize="10" 
                                CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:TemplateField HeaderText="任务ID">
                                        <ItemTemplate>
                                       <asp:Label ID="Label5" runat="server"  Text='<%# Eval("TASK_ID").ToString() %>'></asp:Label>                               
                                    </ItemTemplate>                                    
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>   
                                    <asp:BoundField DataField="TASK_NAME" HeaderText="审核单类型" >                      
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TASK_CREATETIME" HeaderText="创建时间" >
                                    <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TASK_CREATEBY" HeaderText="创建人" >
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>

                              <%--      <asp:BoundField DataField="TASK_STATUS" HeaderText="是否已处理">                                    
                                    <ItemStyle Width="5%" />
                                    </asp:BoundField>--%>

                                    <asp:TemplateField HeaderText="是否已处理">
                                        <ItemTemplate>
                                       <asp:Label ID="lblTaskStatus" runat="server"  Text='<%# Eval("TASK_STATUS").ToString()%>'></asp:Label>                               
                                    </ItemTemplate>                                    
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>  
                                    <asp:BoundField DataField="TASK_APPROVEREJECT" HeaderText="处理结果" >
                                    <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="最后处理人">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lblAutoTrust" runat="server"  Text='<%# Eval("TASK_CURPROCUSER").ToString()%>'></asp:Label>                                    
                                    </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="最后处理时间">
                                    <ItemTemplate>                                    
                                    <asp:Label ID="lblStatus" runat="server"  Text='<%# Eval("TASK_UPDATETIME").ToString() %>'></asp:Label>                                    
                                </ItemTemplate>                                    
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>                                                              
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
             </fieldset>
            </td>
        </tr>
        <tr>
            <td><br /><br /></td>
         </tr>

       <tr>
           <td>
               <div>
                   <asp:DataList ID="DateList" runat="server" RepeatColumns="14" Width="100%"  RepeatDirection="Horizontal" onitemcommand="DateList_ItemCommand" >
                            <ItemStyle HorizontalAlign="Justify" Width="35px" />
                            <ItemTemplate>               
                            <asp:Button   ID="btnTaskSearch" runat="server" CssClass="btn primary"
                                    Text='<%#DataBinder.Eval(Container.DataItem,"id")%> '   CommandName="search"   
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"id")%> ' 
                                    onclick="btnTaskSearch_Click"/>  
                      </ItemTemplate>
                    </asp:DataList>          
               </div>       
           </td>
       </tr>

        <tr>
            <td align="center" class="tdcell" colspan="2"> 
                <fieldset>
                    <legend title="审核单详情">审核单详情</legend>
                    <div style="text-align:left">
                        <table>
                            <tr>
                                <td align="left" style="height:30px">
                                    酒店ID：
                                </td>
                                <td align="left">
                                    <asp:Label ID="lbHotelID" runat="server" Text="" />
                                </td>
                                <td align="left" style="height:30px">
                                    酒店名称：
                                </td>
                                <td align="left">
                                    <asp:Label ID="lbHotelNM" runat="server" Text="" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table align="center" border="0" width="100%" >
                   <%-- <tr class="RowTitle">
                        <td style="width:50%;" class="tdcell">原状态</td>
                        <td style="width:50%;" class="tdcell">修改内容</td>
                    </tr>--%>
                    
                   <%-- <tr>
                        <td style="width:50%" class="tdcell"><div id="divOldContent" runat="server"></div></td>
                        <td style="width:50%" class="tdcell"><div id="divNewContent" runat="server"></div></td>
                    </tr>--%>
                    <tr><td colspan=2>
                        <asp:GridView ID="gridViewData"  runat="server" AutoGenerateColumns="False" 
                                BackColor="White"  AllowPaging="True" PageSize="100" 
                                CssClass="GView_BodyCSS" onrowdatabound="gridViewData_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="OldColName" HeaderText="字段名" >                      
                                    <ItemStyle Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OldColValue" HeaderText="原值" >
                                    <ItemStyle Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NewColName" HeaderText="字段名" Visible="false" >
                                        <ItemStyle Width="25%"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NewColValue" HeaderText="新值">                                    
                                    <ItemStyle Width="25%" />
                                    </asp:BoundField>                                                                  
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />                        
                                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                              <%--  <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />--%>
                            </asp:GridView>
                        </td></tr>
                    <tr>
                        <td colspan=2>
                            
                            <table style="width:100%">
                                <tr>
                                    <td style="width:50%" class="tdcell" align="center" colspan=4>
                                        <table>
                                        <tr>
                                            <td><div id="divButton" runat="server">
                                            <asp:Button ID="btnRetain" runat="server" CssClass="btn primary" Text="保留原值" OnClientClick="return checkOldInput();"  onclick="btnRetain_Click" />                                   
                                            <asp:Button ID="btnUseNew" runat="server" CssClass="btn primary" Text="已处理" OnClientClick="return checkNewInput();"  onclick="btnUseNew_Click" />
                                            </div></td>
                                            <td><input type="button" value="关闭当前页" class="btn" onclick="javascript:closeWin()" /></td>
                                        </tr>
                                        </table> 
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                     </table>
                </fieldset>
            </td>
      </tr>

    </table>
    </div>
    </form>
</body>
</html>
