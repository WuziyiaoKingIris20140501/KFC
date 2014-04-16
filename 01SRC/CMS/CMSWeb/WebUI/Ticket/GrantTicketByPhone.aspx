<%@ Page Title="发券给指定的用户" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GrantTicketByPhone.aspx.cs" Inherits="WebUI_Ticket_GrantTicketByPhone" %>

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


       function ClearSearch() {
           document.getElementById("<%=txtPackageCodeSearch.ClientID %>").value = "";
           document.getElementById("<%=txtPackageNameSearch.ClientID %>").value = "";
           
       }


       function checkEmpty() {
           var packageCode = document.getElementById("<%=txtPackageCode.ClientID %>").value;
           if (packageCode == "") {
               alert("<%=PromptTicketCodeNoEmpty %>");
               document.getElementById("<%=txtPackageCode.ClientID %>").focus();
               return false;
           }

//           var PhoneNumber = document.getElementById("<%=txtPhoneNumber.ClientID %>").value;
//           if (PhoneNumber == "") {
//               alert("<%=PromptPhoneNoInput %>");
//               document.getElementById("<%=txtPhoneNumber.ClientID %>").focus();
//               return false;
//           }

//           if ((PhoneNumber != "") && (PhoneNumber.length > 4999)) {
//               alert("一次发券的手机号码个数不要超过500个！");
//               document.getElementById("<%=txtPhoneNumber.ClientID %>").focus();
//               return false;           
//           }


           return true;
       }

       function Clear() {
           document.getElementById("<%=txtPackageCode.ClientID %>").value = "";
           document.getElementById("<%=txtTicketCount.ClientID %>").value = "";
       }

  
       function checkPhoneInputLength() {
           var PhoneNumber = document.getElementById("<%=txtPhoneNumber.ClientID %>").value;
//           if (PhoneNumber.length > 4999) {
//               alert("一次发券的手机号码个数不要超过500个！");
//               //document.getElementById("<%=txtPhoneNumber.ClientID %>").focus();
//               return false;
//           }
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
                    <input id="Button1" type="button" class="btn" value="清空" onclick="ClearSearch()" />
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
                 <asp:CommandField HeaderText="选择" ShowSelectButton="True"/>

                <asp:BoundField DataField="packagecode" HeaderText="领用券代码">
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                    <HeaderStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="领用券名称" DataField="packagename">
                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="usercnt" HeaderText="总可用次数">
                <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="startdate" HeaderText="最早可领用时间">
                <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="enddate" HeaderText="最晚可领用时间">
                <ItemStyle Width="15%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ticketrulecode" HeaderText="使用规则代码">
                <ItemStyle Width="10%" HorizontalAlign="Center" />
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
                    <input type="button" value="关闭" id="Button3" class="btn" name="btnClose" onclick="invokeClose2();" />
                </td>
            </tr>
        </table>       
    </div>

    <div class="frame01">
     <ul>
        <li class="title"><asp:Literal Text="<%$Resources: PackageList%>" ID="lbRuleTitle" runat="server"></asp:Literal></li>
        <li>
    <!---------------------------------->       
        <table>
            <tr>
                <td colspan="5"> <input type="button" id="Add" value="选择" class="btn primary" onclick="AddNew()" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:PackageCode %>"></asp:Literal>
                </td>
                <td>
                    <input runat="server" readonly="readonly" type="text" id="txtPackageCode" name="txtPackageCode" value="" style="vertical-align:middle; width:250px;"/>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:TicketCount %>"></asp:Label>
                </td>
                <td>
                    <input runat="server" type="text" readonly="readonly" id="txtTicketCount" name="txtTicketCount" value="" style="vertical-align:middle; width:250px;"/>
                </td>
           </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="<%$Resources:LeaveTicketCount %>"></asp:Label></td><td><asp:Label ID="lblRestCount" runat="server" Text=""></asp:Label></td>
                <td colspan="3">&nbsp;</td>
           </tr>

            <tr>
                <td>选择文件：</td>
                <td colspan="5" ><asp:FileUpload ID="PhoneFlUpload" runat="server"  Width="500px" /></td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:SendPhone %>"></asp:Label>
                </td>
                <td colspan="5" >
                    <asp:TextBox ID="txtPhoneNumber" runat="server" TextMode="MultiLine" Width="800px" Height="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" >
                    <asp:Button ID="btnGrantTicket" runat="server" Text="<%$Resources:ButtonSendTicket %>" CssClass="btn primary" OnClientClick="return checkEmpty()" onclick="btnGrantTicket_Click" />
                    <div style="display:none">
                    <asp:Button ID="btnSearchRest" CssClass="btn primary" runat="server" Text="查询剩余可生成张数" onclick="btnSearchRest_Click"  />
                    </div>
                </td>
            </tr>
        </table>
        <table>
        <tr>
            <td colspan="3" ><br />
                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:RemarkDesc %>"></asp:Literal>
            </td>
            <td colspan="2" ><br />
                <img src="../../Images/ExcelDemo.png" />
            </td>
       </tr>
        </table>
        </li>
        </ul>
    </div>
</div>
</asp:Content>

