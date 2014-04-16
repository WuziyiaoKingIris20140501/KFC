<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BatchCreateTicket.aspx.cs"  Title="批量生成抵用券" Inherits="WebUI_Ticket_BatchCreateTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css" >
    .bgDiv2
    {
        display: none;   
        position:absolute;
        top: 0px;
        left: 0px;
        right:0px;
        background-color: #777;
        filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75)
        opacity: 0.6;
    }

    .popupDiv2
    {
        width: 800px;
        height:640px;
        margin-top:50px;
        margin-left:150px;
        position:absolute;
        padding: 1px;
        vertical-align: middle;
        text-align:center;
        border: solid 2px #ff8300;
        z-index: 10001;
        display: none;   
        background-color:White;
    }
 
 </style>
  <script type="text/javascript">
     //显示弹出的层
      function invokeOpen2() {
          document.getElementById("popupDiv2").style.display = "block";
          //背景
          var bgObj = document.getElementById("bgDiv2");
          bgObj.style.display = "block";
          bgObj.style.width = document.body.offsetWidth + "px";
          bgObj.style.height = screen.height + "px";

          //定义窗口
          //var msgObj=document.getElementById("popupDiv");
          //msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";        
      }

    //隐藏弹出的层
      function invokeClose2() {
          document.getElementById("popupDiv2").style.display = "none";
          document.getElementById("bgDiv2").style.display = "none";
      }

      function AddNew() {
          invokeOpen2();
      }


      //===============for 生成抵用券按钮====================
      //显示弹出的背景遮照层
      function invokeOpenBgDiv() {

          document.getElementById("popupDiv").style.display = "block";
          //背景
          var bgObj = document.getElementById("bgDiv");
          bgObj.style.display = "block";
          bgObj.style.width = document.body.offsetWidth + "px";
          bgObj.style.height = screen.height + "px";

      }

      //隐藏弹出的背景遮照层
      function invokeCloseBgDiv() {
          document.getElementById("popupDiv").style.display = "none";
          document.getElementById("bgDiv").style.display = "none";
      }
  </script>

<div id="right">
    <script type="text/javascript">
          var winMain = null;
          function OpenWin(url) {
              if (winMain != null) {
                  try {
                      winMain.close();

                  } catch (e2) {

                      winMain = null;
                  }
              }
              winMain = window.open(url, null, "width=800,height=600");
          }


          //如果有有效，则该按钮为不可点。
          function checkEmpty() {
              var packageCode = document.getElementById("<%=txtPackageCode.ClientID%>").value;
              if (packageCode == "") {
                  alert("<%=PromptTicketCodeNoEmpty %>");
                  document.getElementById("<%=txtPackageCode.ClientID%>").focus();
                  return false;
              }

              invokeOpenBgDiv(); //调用背景遮照层
              return true;
          }


          function ClearEvent() {
              document.getElementById("<%=txtPackageCodeSearch.ClientID%>").value = "";
              document.getElementById("<%=txtPackageNameSearch.ClientID%>").value = "";
          }

          function checkEmptyforExport() {
              var packageCode = document.getElementById("<%=txtPackageCode.ClientID%>").value;
              if (packageCode == "") {
                  alert("<%=PromptTicketCodeNoEmpty %>");
                  document.getElementById("<%=txtPackageCode.ClientID%>").focus();
                  return false;
              }
              return true;
          }
          
    </script>    
    
 
     <!-----------for popup--------------->
    <div id="bgDiv2" class="bgDiv2">        
    </div>
    <div id="popupDiv2" class="popupDiv2">
          <table width="98%" align="center" border="0" class="Table_BodyCSS">
            <tr>
                <td style="width:12%;" class="tdcell" >领用券包代码</td>
                <td style="width:28%;" class="tdcell" >
                    <asp:TextBox ID="txtPackageCodeSearch" runat="server" CssClass="textBlurNew" 
                        SkinID="txtchange" Width="99%"></asp:TextBox>
                </td>                    
                <td style="width:12%;"  class="tdcell" >领用券包名称</td>
                <td style="width:28%;" class="tdcell"><asp:TextBox ID="txtPackageNameSearch" 
                        runat="server" CssClass="textBlurNew" SkinID="txtchange"  Width="99%"></asp:TextBox></td>
                
            </tr>
            <tr>                
                <td  align="center" colspan=4 >
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="查询" OnClick="btnSearch_Click" />
                    <input id="Button1" type="button" class="btn" value="清空" onclick="ClearEvent()" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView Width="99%" HorizontalAlign="Center" ID="myGridView"     
              runat="server" AutoGenerateColumns="False"  CssClass="GView_BodyCSS"    
              AllowPaging="True" PageSize="15"  
              onpageindexchanging="myGridView_PageIndexChanging" 
              onselectedindexchanged="myGridView_SelectedIndexChanged">          
             <Columns>
                 <asp:CommandField HeaderText="选择" ShowSelectButton="True" />
                <asp:BoundField DataField="packagecode" HeaderText="领用券代码">
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                    <HeaderStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="领用券名称" DataField="packagename">
                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="usercnt" HeaderText="总可用次数">
                <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="startdate" HeaderText="最早可领用时间">
                <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="enddate" HeaderText="最晚可领用时间">
                <ItemStyle Width="20%" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
              <EmptyDataTemplate>             
                           没有满足条件的城市信息！                 
            </EmptyDataTemplate> 
        </asp:GridView><br />
        <table style="width:99%;" align="center">
            <tr>
                <td align="center"  style="height:22px;">
                   <%--<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click"/>&nbsp; &nbsp;&nbsp;--%>
                    <input type="button" value="关闭" id="Button3" class="btn" name="btnClose" onclick="invokeClose2();" />
                </td>
            </tr>
        </table>       
    </div> 
    <!---------------------------------->

    <!---------------用于点击批量生成的时候调用该遮罩的效果------------------->
    <div id="bgDiv" class="bgDiv2"></div>
    <div id="popupDiv" class="popupDiv2">
    
        <ul style="margin-top:50px">
            <li><img src="../../Images/loading.gif" alt="正在执行" />正在执行，请稍后...</li>
        </ul>       
    </div>
    <!--------------------------------->
        <div class="frame01">
     <ul>
        <li class="title"><asp:Literal Text="<%$Resources: CreateTicketLabel%>" ID="Literal1" runat="server"></asp:Literal></li>
        <li>
    <table align="center" border="0" width="100%" class="Table_BodyCSS">
        <%--<tr class="RowTitle"><td colspan="5"><asp:Literal Text="<%$Resources: CreateTicketLabel%>" ID="lbRuleTitle" runat="server"></asp:Literal> </td></tr>--%>

        <tr><td colspan="5"> <input type="button" id="Add" value="选择"  class="btn primary" onclick="AddNew()" /></td></tr>
       <tr>
            <td style="width:15%" class="tdcell"><asp:Literal ID="Literal2" runat="server" Text="<%$Resources:PackageCode %>"></asp:Literal></td>
           
            <td style="width:20%" class="tdcell"><input runat="server" readonly="readonly" type="text" id="txtPackageCode" name="txtPackageCode" value="" style="vertical-align:middle; width:80%;"/></td>
            <td style="width:20%" class="tdcell"><asp:Literal ID="Literal3" runat="server" Text="<%$Resources:AllCreateCount %>"></asp:Literal></td>
            <td style="width:30%" class="tdcell">
            <input runat="server" type="text" readonly="readonly" id="txtTicketCount" name="txtTicketCount" value="" style="vertical-align:middle; width:70%;"/>
            </td>

            <%--<td style="width:15%" class="tdcell">
              <input type="button" id="btnselect" value="<%=selectLabel %>" name="btnselect"  onclick="OpenWin('../../Common/sel_package.aspx?FormType=SelPackage')" /> 
                <div style="display:none"><input type="button" id="btnClear" value="<%=clearLabel %>" name="btnClear"  onclick="Clear();" /></div>
            </td>--%>
       </tr>
       <tr>
            <td class="tdcell"><asp:Label ID="Label1" runat="server" Text="<%$Resources:LeaveCreateCount %>"></asp:Label></td><td><asp:Label ID="lblRestCount" runat="server" Text=""></asp:Label></td>
            <td colspan=3 class="tdcell"><asp:Label ID="Label2" runat="server" Text="<%$Resources:IfBatchCount %>"></asp:Label><font color=red><asp:Label ID="Label3" runat="server" Text="<%$Resources:FiveThousandCount %>"></asp:Label></font></td>
       </tr>
       <tr><td colspan="5" align="center" class="tdcell">
            <div id="divCreateButton" style="display:block"  runat="server">
           <asp:Button ID="btnCreate" runat="server" CssClass="btn primary" Text="<%$Resources:CreateTicketLabel %>"  OnClientClick="return checkEmpty();"  onclick="btnCreate_Click"  />
           <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="<%$Resources:ExportTicketLabel %>"  OnClientClick="return checkEmptyforExport();"  onclick="btnExport_Click"  />           
           </div>
            <div style="display:none">
                <asp:Button ID="btnSearchRest" CssClass="btn primary" runat="server" Text="查询剩余可生成张数" onclick="btnSearchRest_Click"  />
            </div>
           </td>
           
       </tr>
       <tr>
           <td colspan="5" class="tdcell"><br />
           <asp:Label ID="Label4" runat="server" Text="<%$Resources:RemarkDescription %>"></asp:Label>
           </td>
       </tr>

    </table>
       </li>
      </ul>
    </div>
 </div>

</asp:Content>

