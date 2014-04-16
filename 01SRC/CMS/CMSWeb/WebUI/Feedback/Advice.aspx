<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Advice.aspx.cs" Inherits="WebUI_Feedback_Advice" Title="用户意见反馈" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" type="text/javascript">
        function OpenIssuePage(arg) {
            var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=1";    //定义弹出窗口的参数
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                fulls += ",height=" + ah;
                fulls += ",innerHeight=" + ah;
                fulls += ",width=" + aw;
                fulls += ",innerWidth=" + aw;
                fulls += ",resizable"
            } else {
                fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
            }
            var time = new Date();
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=5&RID=' + arg + "&time=" + time, null, fulls);
        }

        function OpenSavePage(arg1,arg2,arg3,arg4,arg5,arg6,arg7,arg8) {
            var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=1";    //定义弹出窗口的参数
            if (window.screen) {
                var ah = screen.availHeight - 30;
                var aw = screen.availWidth - 10;
                fulls += ",height=" + ah;
                fulls += ",innerHeight=" + ah;
                fulls += ",width=" + aw;
                fulls += ",innerWidth=" + aw;
                fulls += ",resizable"
            } else {
                fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually
            }
            var time = new Date();
            window.open('<%=ResolveClientUrl("~/WebUI/Feedback/UpdateAdviceStatus.aspx")%>?id=' + arg1 + "&tel=" + arg2 + "&grade=" + arg3 + "&usecode=" + arg4 + "&content=" + escape(arg5) + "&createTime=" + arg6 + "&status=" + arg7 + "&userver=" + arg8 + "&time=" + time, null, fulls);
        }
</script>
<style type="text/css" >
.checkLT td
{
width:120px;
}
.radioLT td
{
width:100px;
}
</style>
<div id="right">
    <table align="center" border="0" width="100%" class="Table_BodyCSS">
        <tr class="RowTitle"><td colspan=2><asp:Literal Text="<%$Resources:UserAdviceTitleLabel%>" ID="lbAdviceTitle" runat="server"></asp:Literal> </td></tr>
       <tr>
        <td class="tdcell"><asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:MobileNumberLabel %>"></asp:Label></td>
       <td class="tdcell"><asp:TextBox ID="txtMobileNumber" runat="server" Width="33%"></asp:TextBox></td></tr>
       <tr><td class="tdcell" style="width:15%;">           
           <asp:Label ID="lblStartDate" runat="server" Text="<%$Resources:PublishDateLabel %>"></asp:Label></td>
           <td class="tdcell" style="width:85%;">                      
           <input id="dtStartTime" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/> 
           <asp:Label ID="lblEndDate" runat="server" Text="<%$Resources:ToLabel %>"></asp:Label>
           <input id="dtEndTime" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dtStartTime\')}'})" runat="server"/>                      
           </td></tr>

       <tr><td  class="tdcell" ><asp:Label ID="lblPlatForm" runat="server" Text="<%$Resources:PlatFormLabel %>"></asp:Label></td>
            <td class="tdcell" align="left">
                <asp:CheckBoxList ID="chkListPlatForm" runat="server" CssClass="checkLT"
                    RepeatDirection="Horizontal" DataSourceID="ObjectDataSource1" 
                    DataTextField="PLATFORM_NAME" DataValueField="PLATFORM_CODE">
                    <asp:ListItem>IOS</asp:ListItem>
                    <asp:ListItem>Android</asp:ListItem>
                </asp:CheckBoxList>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetPlatForm" 
                    TypeName="PlatForm"></asp:ObjectDataSource>
            </td>
       </tr>
       <tr>
            <td  class="tdcell" ><asp:Label ID="lblPrcStatus" runat="server" Text="<%$Resources:PrcStatusLabel %>"></asp:Label></td>
           <td class="tdcell" align="left"> 
            <asp:RadioButtonList ID="radioListPrcStatus" CssClass="radioLT" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="1" Text="<%$ Resources:ProcessedLabel%>"></asp:ListItem>
            <asp:ListItem Value="0" Text="<%$Resources:NotProcessedLabel %>"></asp:ListItem>
            <asp:ListItem Value="" Text="<%$ Resources:MyGlobal,NoLimitText%>" Selected ></asp:ListItem>
            </asp:RadioButtonList> 
            </td></tr>    
          
       <tr>
        <td></td>
        <td  class="tdcell" align="left"> 
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary"
                Text="<%$ Resources:MyGlobal,SearchText %>" onclick="btnSearch_Click" OnClientClick="javascript:return checkValid();"/> 
            <%--<asp:Button ID="btnReset" runat="server" Width="80px" Text="<%$ Resources:MyGlobal,ResetText %>" OnClientClick="javascript:clear();"/>--%>

            <input type="button" id="btnReset" class="btn" value="重置" onclick="ClearEvent()" />

            <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel"  onclick="btnExport_Click"/>
        </td>        
        </tr> 
    </table>
  
    <br />  
  
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">           
            <tr>
                <td align="center">
                    <asp:GridView ID="gridViewAdvice"  runat="server" AutoGenerateColumns="False" 
                        BackColor="White" EmptyDataText="<%$ Resources:MyGlobal,NoDataText %>" DataKeyNames="ID"                         
                         AllowPaging="True" PageSize="20" CssClass="GView_BodyCSS" 
                        AllowSorting="True" onpageindexchanging="gridViewAdvice_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" >
                            <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tel" HeaderText="<%$ Resources:MobileNumberLabel%>" >                                                            
                            <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="USER_ID" HeaderText="用户ID" >                                                            
                            <ItemStyle Width="5%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="USE_CODE_VERSION" HeaderText="版本号" >                                                            
                            <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="grade" HeaderText="<%$ Resources:ScoreLabel%>" >                           
                            <ItemStyle Width="5%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CONTENT" HeaderText="<%$ Resources:AdviceLabel%>" >                                
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="USE_CODE" HeaderText="<%$ Resources:PlatFormLabel%>" >                                
                            <ItemStyle Width="10%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CREATE_TIME" HeaderText="<%$ Resources:PostTimeLabel%>" >                                
                            <ItemStyle Width="10%" />
                            </asp:BoundField>                            

                           <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:PrcStatusLabel %>">                            
                            <ItemTemplate>
                                <%#Eval("status").ToString() == "1" ? strProcessedLabel : strNotProcessedLabel%>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:TemplateField>

                             <asp:BoundField DataField="UPDATE_TIME"  HeaderText="<%$ Resources:ProcessTimeLabel %>" >                             
                            <ItemStyle Width="15%" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="详情" >
                            <ItemTemplate>
                                <%--<a href="javascript:OpenWnd('UpdateAdviceStatus.aspx?id=<%#Eval("ID").ToString()%>&tel=<%#Eval("tel") %>&grade=<%#Eval("grade")%>&usecode=<%#Eval("useCode")%>&content=<%#Server.UrlEncode(Eval("content").ToString())%>&createTime=<%#Eval("createTime")%>&status=<%#Eval("status")%>')" ><asp:Label ID="lblDetail" runat="server" Text="<%$Resources:MyGlobal,DetailText %>"></asp:Label></a>                                    --%>
                                  <%--<a href="UpdateAdviceStatus.aspx?id=<%#Eval("ID").ToString()%>&tel=<%#Eval("tel") %>&grade=<%#Eval("grade")%>&usecode=<%#Eval("USE_CODE")%>&content=<%#(Eval("content").ToString())%>&createTime=<%#Eval("CREATE_TIME")%>&status=<%#Eval("status")%>&userver=<%#Eval("USE_CODE_VERSION")%>" target="_blank" ><asp:Label ID="Label1" runat="server" Text="<%$Resources:MyGlobal,DetailText %>"></asp:Label></a>                                    --%>

                                  <a href="#" onclick="OpenSavePage('<%#Eval("ID").ToString()%>','<%#Eval("tel") %>','<%#Eval("grade")%>','<%#Eval("USE_CODE")%>','<%#Server.UrlEncode(Eval("content").ToString())%>','<%#Eval("CREATE_TIME")%>','<%#Eval("status")%>','<%#Eval("USE_CODE_VERSION")%>')"><asp:Label ID="Label1" runat="server" Text="<%$Resources:MyGlobal,DetailText %>"></asp:Label></a>

                                  <a href="#" onclick="OpenIssuePage('<%#Eval("ID").ToString()%>')"><asp:Label ID="lbIssue" runat="server" Text="创建Issue单"></asp:Label></a>                                    
                            </ItemTemplate>

                            <ItemStyle Width="10%" />
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
        </div>
  <script language="javascript" type="text/javascript">
      var win = null;
      function OpenWnd(URL) {
          if (win != null) {
              try {
                  win.close();
              }
              catch (e2) {
                  win = null;
              }
          }
          win = window.open(URL, "", "scrollbars=yes, resizable=yes, toolbar=no, menubar=no, location=no, directories=no,width=800,height=600,top=20,left=20");
      }

      function ClearEvent() {
          document.getElementById("<%=txtMobileNumber.ClientID%>").value = "";

          var myDate = new Date();
          var dpVal = myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate();
          document.getElementById("<%=dtStartTime.ClientID%>").value = dpVal;
          document.getElementById("<%=dtEndTime.ClientID%>").value = dpVal;


          var RadioTable = document.getElementById("<%=radioListPrcStatus.ClientID%>");
          var RadioInput = RadioTable.getElementsByTagName("INPUT");
          if (RadioInput.length > 1)
          { RadioInput[2].checked = true; }

          var chkfObject = document.getElementById("<%=chkListPlatForm.ClientID%>");
          if (chkfObject != null) {
              var chkfInput = chkfObject.getElementsByTagName("input");
              for (var i = 0; i < chkfInput.length; i++) {
                  if (chkfInput[i].type = "checkbox") {
                      chkfInput[i].checked = false;
                  }
              }
          }

      }

      function checkValid() 
      {
          var mobile = document.getElementById("<%=txtMobileNumber.ClientID%>").value;
          var startdate = document.getElementById("<%=dtStartTime.ClientID%>").value;
          var enddate = document.getElementById("<%=dtEndTime.ClientID%>").value;
          if (startdate == "" && enddate == "") {
              alert("请选择发表日期范围！");
              document.getElementById("<%=dtStartTime.ClientID%>").focus();
              return false;
          }

          if (startdate > enddate) {
              alert("起始日期不能大于结束日期！");
              document.getElementById("<%=dtStartTime.ClientID%>").focus();
              return false;
          }
          return true;
      }

    </script>
  
</asp:Content>

