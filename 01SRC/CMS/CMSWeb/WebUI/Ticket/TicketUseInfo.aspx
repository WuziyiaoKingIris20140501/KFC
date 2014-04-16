<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TicketUseInfo.aspx.cs" Inherits="WebUI_Ticket_TicketUseInfo" %>

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

.pcbackground { 
display: block; 
width: 100%; 
height: 100%; 
opacity: 0.4; 
filter: alpha(opacity=40); 
background:while; 
position: absolute; 
top: 0; 
left: 0; 
z-index: 2000; 
} 
.pcprogressBar { 
border: solid 2px #3A599C; 
background: white url("/images/progressBar_m.gif") no-repeat 10px 10px; 
display: block; 
width: 148px; 
height: 28px; 
position: fixed; 
top: 50%; 
left: 50%; 
margin-left: -74px; 
margin-top: -14px; 
padding: 10px 10px 10px 50px; 
text-align: left; 
line-height: 27px; 
font-weight: bold; 
position: absolute; 
z-index: 2001; 
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
      }

      //隐藏弹出的层
      function invokeClose2() {
          document.getElementById("popupDiv2").style.display = "none";
          document.getElementById("bgDiv2").style.display = "none";
      }

      function AddNew() {
          invokeOpen2();
      }

      function ClearEvent() {
          document.getElementById("<%=txtPackageCodeSearch.ClientID%>").value = "";
          document.getElementById("<%=txtPackageNameSearch.ClientID%>").value = "";
      }

      function PopupTicketArea(arg) {
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
          //var retunValue = window.open("TicketUseInfo.aspx?TYPE=" + arg + "&time=" + time, null, fulls);
          //window.location.href = "TicketUseInfo.aspx?TYPE=" + arg + "&time=" + time;
          window.location.href = "TicketUseInfo.aspx?TYPE=" + arg + "&pknm=" + encodeURI(document.getElementById("<%=hidPKName.ClientID%>").value) + "&atk=" + encodeURI(document.getElementById("<%=hidATFrom.ClientID%>").value) + "&att=" + encodeURI(document.getElementById("<%=hidATTo.ClientID%>").value) + "&pkf=" + encodeURI(document.getElementById("<%=hidPKFrom.ClientID%>").value) + "&pkt=" + encodeURI(document.getElementById("<%=hidPKTo.ClientID%>").value) + "&tkt=" + document.getElementById("<%=hidTKTime.ClientID%>").value + "&time=" + time;
      }

      function PopupUserArea(type, arg) {
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
          //window.open('<%=ResolveClientUrl("~/WebUI/UserGroup/ReviewUserPage.aspx")%>?TYPE=' + type + "&DATA=" + arg, null, fulls);
          window.location.href = '<%=ResolveClientUrl("~/WebUI/UserGroup/ReviewUserPage.aspx")%>?TYPE=' + type + "&DATA=" + arg + "&pknm=" + encodeURI(document.getElementById("<%=hidPKName.ClientID%>").value) + "&atk=" + encodeURI(document.getElementById("<%=hidATFrom.ClientID%>").value) + "&att=" + encodeURI(document.getElementById("<%=hidATTo.ClientID%>").value) + "&pkf=" + encodeURI(document.getElementById("<%=hidPKFrom.ClientID%>").value) + "&pkt=" + encodeURI(document.getElementById("<%=hidPKTo.ClientID%>").value) + "&tkt=" + document.getElementById("<%=hidTKTime.ClientID%>").value + "&time=" + time;
      }

      function PopupOrderArea(type, arg, paycode) {
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
          //window.open('<%=ResolveClientUrl("~/WebUI/DBQuery/LmOrderLogPage.aspx")%>?TYPE=' + type + "&DATA=" + arg + "&PCOD=" + paycode, null, fulls);
          window.location.href = '<%=ResolveClientUrl("~/WebUI/DBQuery/LmOrderSearchPage.aspx")%>?TYPE=' + type + "&DATA=" + arg + "&PCOD=" + paycode + "&pknm=" + encodeURI(document.getElementById("<%=hidPKName.ClientID%>").value) + "&atk=" + encodeURI(document.getElementById("<%=hidATFrom.ClientID%>").value) + "&att=" + encodeURI(document.getElementById("<%=hidATTo.ClientID%>").value) + "&pkf=" + encodeURI(document.getElementById("<%=hidPKFrom.ClientID%>").value) + "&pkt=" + encodeURI(document.getElementById("<%=hidPKTo.ClientID%>").value) + "&tkt=" + document.getElementById("<%=hidTKTime.ClientID%>").value + "&time=" + time;
      }

      function BtnLoadStyle() {
          var ajaxbg = $("#background,#progressBar");
          ajaxbg.hide();
          ajaxbg.show();
      }

      function BtnCompleteStyle() {
          var ajaxbg = $("#background,#progressBar");
          ajaxbg.hide();
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
                        SkinID="txtchange" Width="90%"></asp:TextBox>
                </td>                    
                <td style="width:12%;"  class="tdcell" >领用券包名称</td>
                <td style="width:28%;" class="tdcell"><asp:TextBox ID="txtPackageNameSearch" 
                        runat="server" CssClass="textBlurNew" SkinID="txtchange"  Width="90%"></asp:TextBox></td>
                
            </tr>
            <tr>                
                <td  align="center" colspan=4 >
                    <asp:Button ID="btnSearch2" runat="server" CssClass="btn primary" Text="查询" OnClick="btnSearch2_Click" />
                    <input id="btnClear2" type="button" class="btn" value="清空" onclick="ClearEvent()" />
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
                    <input type="button" value="关闭" id="Button3" name="btnClose" class="btn" onclick="invokeClose2();" />
                </td>
            </tr>
        </table>       
    </div> 
    <!---------------------------------->
    <div id="right">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <table align="center" border="0" width="100%" class="Table_BodyCSS">
    <tr>
           <td>  
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  ChildrenAsTriggers="false">
               <ContentTemplate>
                   <div class="frame01">
                  <ul>
                    <li class="title"><asp:Literal Text="优惠券活动搜索条件" ID="Literal2" runat="server"></asp:Literal> </li>
                    <li>
                       <table align="center" border="0" width="100%" >
                            <tr><td></td></tr>
                            <tr>
                                <td style="width:10%;"  >
                                    <div style="margin-left:15px;" ><asp:Label ID="lblCity" runat="server" Text="优惠券活动名称：">优惠券活动名称：</asp:Label></div>
                                </td>
                                <td align="left"  style="width:80%" >                          
                                    <asp:TextBox ID="txtPackageName" runat="server" Width="80%" MaxLength="30"></asp:TextBox> 
                                    <input type="button" id="Add" value="选择" class="btn primary" onclick="AddNew()" />
                                </td>
                            </tr>
                             <tr>
                                <td style="width:10%;margin-left:10px"  >
                                <div style="margin-left:15px;" ><asp:Label ID="Label2" runat="server" Text="优惠券活动金额：">优惠券活动金额：</asp:Label></div></td>
                                <td align="left"  style="width:80%" >                          
                                    <asp:TextBox ID="txtAmountFrom" runat="server" Width="145px"></asp:TextBox> 到：
                                    <asp:TextBox ID="txtAmountTo" runat="server" Width="145px"></asp:TextBox> 
                                    <input id="chkLimitAmount" type="checkbox" value="1" onclick="javascript:checkLimit('0');"/>不限制                     
                                </td>
                            </tr>
                           <tr>
                                <td style="width:10%;margin-left:10px"  >
                                <div style="margin-left:15px;" ><asp:Label ID="Label1" runat="server" Text="领用开始日期：">领用开始日期：</asp:Label></div></td>
                                <td align="left"  style="width:80%" >                          
                                    <input id="PickFromDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/> 到：
                                    <input id="PickToDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/>
                                    <input id="chkLimitPickDate" type="checkbox" value="1" onclick="javascript:checkLimit('1');"/> 不限制                     
                                </td>
                            </tr>
                           <%-- <tr>
                                <td  class="tdcell" style="width:10%;margin-left:10px"  >
                                <div style="margin-left:15px;" ><asp:Label ID="Label4" runat="server" Text="可抵用开始日期：">可抵用开始日期：</asp:Label></div></td>
                                <td class="tdcell" align="left"  style="width:80%" >                          
                                    <input id="UseFromDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/> 到：
                                    <input id="UseToDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/>
                                    <input id="chkLimitUseDate" type="checkbox" value="1" onclick="javascript:checkLimit('2');"/> 不限制                     
                                </td>
                            </tr>          --%>                         
                             <tr>
                               <td style="width:10%;margin-left:10px"  >
                                <div style="margin-left:15px;" ><asp:Label ID="Label3" runat="server" Text="优惠券有效期：">优惠券有效期：</asp:Label></div></td>
                                <td align="left"  style="width:80%" >
                                    <asp:DropDownList ID="ddpTicketTime" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                                </td>
                             </tr>
                              <tr> 
                                <td align="left" colspan="2" > 
                                    <div style="margin-left:15px;" >
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索"  OnClientClick="javascript:return checkEmpty();"
                                            onclick="btnSearch_Click" Height="21px"/>
                                         <input type="button" id="btnReset" class="btn" value="清空" onclick="javascript:clear();" /> 
                                     </div>
                                </td>
                             </tr>
                            </table>
                    </li>
                  </ul>
                </div>
               </ContentTemplate>
               </asp:UpdatePanel>
           </td>
     </tr> 
     <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
               <ContentTemplate>
            <asp:HiddenField ID="hidPKName" runat="server"/>
            <asp:HiddenField ID="hidATFrom" runat="server"/>
            <asp:HiddenField ID="hidATTo" runat="server"/>
            <asp:HiddenField ID="hidPKFrom" runat="server"/>
            <asp:HiddenField ID="hidPKTo" runat="server"/>
            <asp:HiddenField ID="hidTKTime" runat="server"/>
            <div id="background" class="pcbackground" style="display: none; "></div> 
            <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
            <table width="100%">
                <tr>
                  <td align="left">
                    <table cellspacing="1" cellpadding="4" style="background-color:White;border-width:1px;border-collapse:collapse;border-style:solid;width:100%;border-color:#E6E5E5;">
                        <tr class="GView_HeaderCSS"><td align="center" colspan="4" style="border-color:#E6E5E5;"><b>运营</b></td></tr>
                        <tr>
                            <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;">共有优惠券活动：</td>
                            <td colspan="3" style="border-color:#E6E5E5;"><a id="A1" href="#" runat="server" onclick="PopupTicketArea('0')"><asp:Label ID="lbTicketOperate" runat="server" Text="0" /></a></td>
                        </tr>
                        <tr>
                            <td style="width:30%;border-color:#E6E5E5;border-width:1px;border-style:solid;">共领用用户数：</td>
                            <td style="width:20%;border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A4" href="#" runat="server" onclick="PopupUserArea('0', '0')"><asp:Label ID="lbOperateAllUser" runat="server" Text="0" /></a></td>
                            <td style="width:30%;border-color:#E6E5E5;border-width:1px;border-style:solid;">共使用用户数：</td>
                            <td style="width:20%;border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A5" href="#" runat="server" onclick="PopupUserArea('0', '1')"><asp:Label ID="lbOperateUsed" runat="server" Text="0" /></a></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table width="100%" cellspacing="1" cellpadding="4" style="background-color:White;border-width:1px;border-style:solid;border-collapse:collapse;width:100%;border-color:#E6E5E5 ">
                                    <tr class="GView_HeaderCSS">
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">订单类型</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">LMBAR</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">LMBAR2</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">BAR/BARB</td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">总产生订单</td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A10" href="#" runat="server" onclick="PopupOrderArea('0', '0','LMBAR')"><asp:Label ID="lbOperateLmOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A11" href="#" runat="server" onclick="PopupOrderArea('0', '0','LMBAR2')"><asp:Label ID="lbOperateLm2Order" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A12" href="#" runat="server" onclick="PopupOrderArea('0', '0','BARB')"><asp:Label ID="lbOperateBarbOrder" runat="server" Text="0" /></a></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">总产生成功订单</td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A13" href="#" runat="server" onclick="PopupOrderArea('0', '1','LMBAR')"><asp:Label ID="lbOperateLmSuOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A14" href="#" runat="server" onclick="PopupOrderArea('0', '1','LMBAR2')"><asp:Label ID="lbOperateLm2SuOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A15" href="#" runat="server" onclick="PopupOrderArea('0', '1','BARB')"><asp:Label ID="lbOperateBarbSuOrder" runat="server" Text="0" /></a></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table cellspacing="1" cellpadding="4" style="background-color:White;border-width:1px;border-collapse:collapse;border-style:solid;width:100%;border-color:#E6E5E5;">
                        <tr class="GView_HeaderCSS"><td align="center" colspan="4" style="border-color:#E6E5E5;"><b>市场</b></td></tr>
                        <tr>
                            <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;">共有优惠券活动：</td>
                            <td colspan="3" style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A2" href="#" runat="server" onclick="PopupTicketArea('1')"><asp:Label ID="lbTickeMarket" runat="server" Text="0" /></a></td>
                        </tr>
                        <tr>
                            <td style="width:30%;border-color:#E6E5E5;border-width:1px;border-style:solid;">共领用用户数：</td>
                            <td style="width:20%;border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A6" href="#" runat="server" onclick="PopupUserArea('1', '0')"><asp:Label ID="lbMarketAllUser" runat="server" Text="0" /></a></td>
                            <td style="width:30%;border-color:#E6E5E5;border-width:1px;border-style:solid;">共使用用户数：</td>
                            <td style="width:20%;border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A7" href="#" runat="server" onclick="PopupUserArea('1', '1')"><asp:Label ID="lbMarketUsed" runat="server" Text="0" /></a></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table width="100%" cellspacing="1" cellpadding="4" style="background-color:White;border-width:1px;border-style:solid;width:100%;border-collapse:collapse;border-color:#E6E5E5 ">
                                    <tr class="GView_HeaderCSS">
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">订单类型</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">LMBAR</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">LMBAR2</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">BAR/BARB</td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">总产生订单</td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A16" href="#" runat="server" onclick="PopupOrderArea('1', '0','LMBAR')"><asp:Label ID="lbMarketLmOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A17" href="#" runat="server" onclick="PopupOrderArea('1', '0','LMBAR2')"><asp:Label ID="lbMarketLm2Order" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A18" href="#" runat="server" onclick="PopupOrderArea('1', '0','BARB')"><asp:Label ID="lbMarketBarbOrder" runat="server" Text="0" /></a></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">总产生成功订单</td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A19" href="#" runat="server" onclick="PopupOrderArea('1', '1','LMBAR')"><asp:Label ID="lbMarketLmSuOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A20" href="#" runat="server" onclick="PopupOrderArea('1', '1','LMBAR2')"><asp:Label ID="lbMarketLm2SuOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A21" href="#" runat="server" onclick="PopupOrderArea('1', '1','BARB')"><asp:Label ID="lbMarketBarbSuOrder" runat="server" Text="0" /></a></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table cellspacing="1" cellpadding="4" style="background-color:White;border-width:1px;border-collapse:collapse;border-style:solid;width:100%;border-color:#E6E5E5;">
                        <tr class="GView_HeaderCSS"><td align="center" colspan="4" style="border-color:#E6E5E5;"><b>其他</b></td></tr>
                        <tr>
                            <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;">共有优惠券活动：</td>
                            <td colspan="3" style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A3" href="#" runat="server" onclick="PopupTicketArea('2')"><asp:Label ID="lbTicketOther" runat="server" Text="0" /></a></td>
                        </tr>
                        <tr>
                            <td style="width:30%;border-color:#E6E5E5;border-width:1px;border-style:solid;">共领用用户数：</td>
                            <td style="width:20%;border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A8" href="#" runat="server" onclick="PopupUserArea('2', '0')"><asp:Label ID="lbOtherAllUser" runat="server" Text="0" /></a></td>
                            <td style="width:30%;border-color:#E6E5E5;border-width:1px;border-style:solid;">共使用用户数：</td>
                            <td style="width:20%;border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A9" href="#" runat="server" onclick="PopupUserArea('2', '1')"><asp:Label ID="lbOtherUsed" runat="server" Text="0" /></a></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table width="100%" cellspacing="1" cellpadding="4" style="background-color:White;border-width:1px;border-style:solid;width:100%;border-collapse:collapse;border-color:#E6E5E5 ">
                                    <tr class="GView_HeaderCSS">
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">订单类型</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">LMBAR</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">LMBAR2</td>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">BAR/BARB</td>
                                    </tr>
                                   <tr>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">总产生订单</td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A22" href="#" runat="server" onclick="PopupOrderArea('2', '0','LMBAR')"><asp:Label ID="lbOtherLmOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A23" href="#" runat="server" onclick="PopupOrderArea('2', '0','LMBAR2')"><asp:Label ID="lbOtherLm2Order" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A24" href="#" runat="server" onclick="PopupOrderArea('2', '0','BARB')"><asp:Label ID="lbOtherBarbOrder" runat="server" Text="0" /></a></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-color:#E6E5E5;border-width:1px;border-style:solid;">总产生成功订单</td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A25" href="#" runat="server" onclick="PopupOrderArea('2', '1','LMBAR')"><asp:Label ID="lbOtherLmSuOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A26" href="#" runat="server" onclick="PopupOrderArea('2', '1','LMBAR2')"><asp:Label ID="lbOtherLm2SuOrder" runat="server" Text="0" /></a></td>
                                        <td style="border-color:#E6E5E5;border-width:1px;border-style:solid;"><a id="A27" href="#" runat="server" onclick="PopupOrderArea('2', '1','BARB')"><asp:Label ID="lbOtherBarbSuOrder" runat="server" Text="0" /></a></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
            </table>
            </ContentTemplate>
               </asp:UpdatePanel>
        </td>
     </tr>
     <tr>
        <td><br /></td>
     </tr>
     <tr>
        <td>
             <div class="frame01">
                <ul>
                <li class="title"><asp:Literal Text="优惠券活动搜索结果" ID="Literal1" runat="server"></asp:Literal> </li>
                <li>
                <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" class="tdcell" colspan="4">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                    <asp:GridView ID="gridViewTicketUseInfo"  runat="server" AutoGenerateColumns="False" 
                        BackColor="White"  AllowPaging="True" PageSize="20" 
                        CssClass="GView_BodyCSS" 
                        onpageindexchanging="gridViewTicketUseInfo_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="PACKAGECODE" HeaderText="优惠活动号码" >                      
                            <ItemStyle Width="20%" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="PACKAGENAME" HeaderText="优惠活动名称" >                      
                            <ItemStyle Width="25%" />
                            </asp:BoundField>                              
                            <asp:BoundField DataField="STARTDATE_ENDDATE" HeaderText="领用起止日期" >
                            <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AMOUNT" HeaderText="总优惠金额">                                    
                            <ItemStyle Width="10%" />
                            </asp:BoundField>
                                
                            <asp:BoundField DataField="TICKETCOUNT" HeaderText="包含优惠券张数">                                    
                            <ItemStyle Width="10%" />
                            </asp:BoundField>  
                                <asp:BoundField DataField="USERCNT" HeaderText="可领用次数">                                    
                            <ItemStyle Width="10%" />
                            </asp:BoundField> 
                            <asp:TemplateField HeaderText="查看使用规则">
                                <ItemTemplate>
                                <a onclick="OpenWnd('TicketUseDetail.aspx?packagecode=<%#Eval("PACKAGECODE").ToString() %>','优惠券使用详情')" href="#" ><asp:Label ID="Label5" runat="server"  Text='查看'></asp:Label></a>                                  
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
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
                </li>
                </ul>
            </div>
        </td>
     </tr>
   </table>
   </div>
     <script language="javascript" type="text/javascript">
        //点击清空按钮
         function clear() 
         {
             document.getElementById("<%=txtPackageName.ClientID%>").value = "";
             document.getElementById("<%=txtAmountFrom.ClientID%>").value = "";
             document.getElementById("<%=txtAmountTo.ClientID%>").value = "";
             document.getElementById("chkLimitAmount").checked = false;             
             
             document.getElementById("<%=PickFromDate.ClientID%>").value = "";
             document.getElementById("<%=PickToDate.ClientID%>").value = "";
             document.getElementById("chkLimitPickDate").checked = false;

             document.getElementById("<%=ddpTicketTime.ClientID%>").selectedIndex = 0;
         }

          //点搜索
         function checkEmpty() 
         {
             var PackageName = document.getElementById("<%=txtPackageName.ClientID%>").value;
             var AmountFrom = document.getElementById("<%=txtAmountFrom.ClientID%>").value;
             var AmountTo = document.getElementById("<%=txtAmountTo.ClientID%>").value;
             var PickFromDate = document.getElementById("<%=PickFromDate.ClientID%>").value;
             var PickToDate = document.getElementById("<%=PickToDate.ClientID%>").value; 
             
             var inputRules = /^\d+(\.\d+)?$/; //只能是数字且不能为负数            
             if (AmountFrom != "" && inputRules.test(AmountFrom) == false) {
                 alert("优惠券活动金额只能输入数字！");
                 document.getElementById("<%=txtAmountFrom.ClientID%>").focus();
                 return false;
             }

             if (AmountTo != "" && inputRules.test(AmountTo) == false) {
                 alert("优惠券活动金额只能输入数字！");
                 document.getElementById("<%=txtAmountTo.ClientID%>").focus();
                 return false;
             }

             if (AmountFrom != "" && AmountTo != "" && parseFloat(AmountFrom) > parseFloat(AmountTo)) {
                 alert("优惠券活动金额开始值不能大于结束值！");
                 document.getElementById("<%=txtAmountFrom.ClientID%>").focus();
                 return false;
             }

//             if (AmountFrom != "" && AmountTo != "" && AmountFrom > AmountTo) 
//             {
//                 alert("优惠券活动金额的开始值必须小于终止值！");
//                 document.getElementById("<%=txtAmountFrom.ClientID%>").focus();
//                 return false;
//             }

             //如果领用起始日期有一个为空，则提示.
             if ((PickFromDate == "" && PickToDate != "") || (PickFromDate != "" && PickToDate == ""))
             {             
                alert("领用起止日期两个必须同时有值或者不做限制！");
                document.getElementById("<%=PickFromDate.ClientID%>").focus();
                return false;
             }

             if (PickFromDate != "" && PickToDate != "" && PickFromDate > PickToDate) 
             {
                 alert("领用起止日期的开始日期不能大于终止日期！");
                 document.getElementById("<%=PickFromDate.ClientID%>").focus();
                 return false;             
             }
             
             BtnLoadStyle();
             return true;
         }

         //选择不限制的复选框
         function checkLimit(arg) {
             var AmountFrom = document.getElementById("<%=txtAmountFrom.ClientID%>");
             var AmountTo = document.getElementById("<%=txtAmountTo.ClientID%>");
             var PickFromDate = document.getElementById("<%=PickFromDate.ClientID%>");
             var PickToDate = document.getElementById("<%=PickToDate.ClientID%>");
             if (arg == "0") 
             {
                 var chk0 = document.getElementById("chkLimitAmount");
                 if (chk0.checked == true) {
                     AmountFrom.value = "";
                     AmountTo.value = "";
                     AmountFrom.disabled = true;
                     AmountTo.disabled = true;
                 }
                 else {
                     AmountFrom.disabled = false;
                     AmountTo.disabled = false;                 
                 }
             }
             if (arg == "1") {
                 var chk1 = document.getElementById("chkLimitPickDate");
                 if (chk1.checked == true) {
                     PickFromDate.value = "";
                     PickToDate.value = "";
                     PickFromDate.disabled = true;
                     PickToDate.disabled = true;
                 }
                 else 
                 {
                     PickFromDate.disabled = false;
                     PickToDate.disabled = false;
                 }
             }                        
         }
     </script>

</asp:Content>

