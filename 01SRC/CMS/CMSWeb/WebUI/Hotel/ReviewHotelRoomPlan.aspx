<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.master"
    CodeFile="ReviewHotelRoomPlan.aspx.cs" Title="销售计划查询" Inherits="ReviewHotelRoomPlan" %>

<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <style type="text/css">
        .bgDiv2
        {
            width: 100%;
            height:100%;
            display: none;   
            position:absolute;
            top: 0px;
            left: 0px;
            right:0px;
            background-color: #777;
            filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75);
            opacity: 0.6;
            margin:0px auto;
        }

        .popupDiv2
        {
            top:20%;
            padding: 1px;
            width:600px; 
            height:500px; 
            left:50%; 
            margin:0px 0 0 -300px; 
            position:absolute;
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
        function showDiv(priceCode, roomame, roomCode, twoPrice, roomnum, status, isreserve, effectDate) {

            
            document.getElementById("<%=HiddenRoomType.ClientID%>").value = roomame;

            
            document.getElementById("<%=HiddenPrice.ClientID%>").value = twoPrice;

            document.getElementById("<%=HiddenPriceCode.ClientID%>").value = priceCode; //价格代码

            

            
            document.getElementById("<%=HiddenEffectDate.ClientID%>").value = effectDate;
            document.getElementById("<%=HiddenRoomCode.ClientID%>").value = roomCode;
            invokeOpen2();
        }

        //显示弹出的层
        function invokeOpen2() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
//            bgObj.style.width = document.body.offsetWidth + "px";
            //            bgObj.style.height = screen.height + "px";
            ShowDivGo();
        }

        //隐藏弹出的层
        function invokeClose2() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function showManagerDiv() {
            document.getElementById("managerTxtRoomCount").style.display = "block";
            document.getElementById("manegerCkReserve").style.display = "block";
        }

        function closeManagerDiv() {
            document.getElementById("managerTxtRoomCount").style.display = "none";
            document.getElementById("manegerCkReserve").style.display = "none";
        }

        function ShowHisPlanInfo(pCode, pRoom, pTime) {
            BtnLoadStyle()
            document.getElementById("<%=HiddenPriceCode.ClientID%>").value = pCode;
            document.getElementById("<%=HiddenRoomCode.ClientID%>").value = pRoom;
            document.getElementById("<%=HiddenEffectDate.ClientID%>").value = pTime;
            document.getElementById("<%=btnShwoInfo.ClientID%>").click();
        }

        function AlterLoadError() {
            alert('请选择酒店！')
            BtnCompleteStyle();
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

        function DvChangeEvent(dvId, trId) {
//            var ajaxAdd = $("#"+arg);
//            if ($("#" + arg).is(":hidden")) {
//                ajaxAdd.show(500);
//            }
//            else {
//                ajaxAdd.hide(500);
            //            }
            BtnCompleteStyle();

            var sbMeno = document.getElementById(dvId);
            var trLine = document.getElementById(trId);
            if (sbMeno.style.display == 'block' || sbMeno.style.display == '') {
                sbMeno.style.display = 'none';

                sbMeno.style.backgroundColor = "#FFFFFF";
                trLine.style.backgroundColor = "#FFFFFF";

            } else {
                sbMeno.style.display = '';

                sbMeno.style.backgroundColor = "#E8E8E8";
                trLine.style.backgroundColor = "#E8E8E8";
            }
        }

        function ShowDivGo(f) {
            var d = document.getElementById('bgDiv2'), wh = getWH(f);
            d.style.cssText += ";width:" + wh.w + 50 + 'px;height:' + wh.h + 'px'
        }
        var getWH = function () {
            var d = document, doc = d[d.compatMode == "CSS1Compat" ? 'documentElement' : 'body'];
            return function (f) {
                return {
                    w: doc[(f ? 'client' : 'scroll') + 'Width'],
                    h: f ? doc.clientHeight : Math.max(doc.clientHeight, doc.scrollHeight)
                }
            }
        } ()
    </script>
    <div id="right">
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
     <ContentTemplate>
    <div class="frame01" style="width:97%">
        <ul>
            <li class="title">销售计划查询</li>
            <li>
                <table>
                    <tr>
                        <td align="right">
                            计划起止日期：
                        </td>
                        <td>
                            <input id="planStartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_planEndDate\')||\'2020-10-01\'}'})"
                                runat="server" />
                        </td>
                        <td align="right">
                            至：
                        </td>
                        <td>
                            <input id="planEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_planStartDate\')}',maxDate:'2020-10-01'})"
                                runat="server" />
                        </td>
                        <td align="right">
                            选择酒店：
                        </td>
                        <td>
                            <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel"
                                AutoParent="ReviewHotelRoomPlan.aspx?Type=hotel"  />
                        </td>
                        <td>
                             <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                             <ContentTemplate>
                                 <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="选择" OnClientClick="BtnLoadStyle();" 
                                     onclick="btnSelect_Click" />
                                 <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="重置" Visible="false" />
                             </ContentTemplate>
                             </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                </table>
            </li>
        </ul>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
     <ContentTemplate>
    <div class="frame01" id="DivhotelDetails" runat="server" style="width:97%">
        <ul>
            <li class="title">酒店信息<asp:Label ID="lblHotelDetails" runat="server" Text=""></asp:Label></li>
        </ul>
        <ul>
            <li>
                <table>
                    <tr style="border-left: 50px;vertical-align:text-bottom">
                        <td colspan="2">
                            <%-- [8393] 上海东方航空宾馆--%>
                            <div runat="server" visible="false">
                                <asp:Label ID="lblHotelID" runat="server" Text="" Visible="false" ></asp:Label>
                                <asp:Label ID="lblhotelName" runat="server" Text="" Width="200px" ></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            酒店联系人：
                        </td>
                        <td>
                            <asp:Label ID="ContactMan" runat="server" Text="" Width="200px"></asp:Label>
                        </td>
                        <td style="width: 50px">
                        </td>
                        <td align="right">
                            电话：
                        </td>
                        <td>
                            <asp:Label ID="ContactTel" runat="server" Text="" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            HotelVP酒店销售：
                        </td>
                        <td>
                            <asp:Label ID="LinkMan" runat="server" Text="" Width="200px"></asp:Label>
                        </td>
                        <td style="width: 50px">
                        </td>
                        <td align="right">
                            电话：
                        </td>
                        <td>
                            <asp:Label ID="LinkTel" runat="server" Text="" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td>
                            <asp:Button ID="btnShwoInfo" runat="server" CssClass="btn primary" Text="Show" 
                                     onclick="btnShwoInfo_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </li>
        </ul>
    </div>

    <div class="frame01" id="DivPlanDetails" runat="server" style="width:97%">
        <ul>
            <li class="title">计划信息</li>
        </ul>
        <ul>
           <li>
                <table width="95%">
                    <tr align="right">
                        <td>
                            <table border="1"style="border-color:#D5D5D5;border-collapse: collapse;" >
                                <tr align="center">
                                    <td style="width: 60px">
                                        无计划
                                    </td>
                                    <td style="background-color: #E6B9B6; font-style: inherit; width: 60px">
                                        满房
                                    </td>
                                    <td style="background-color: #E96928; font-style: inherit; width: 80px">
                                        CC操作满房
                                    </td>
                                    <td style="background-color: #999999; font-style: inherit; width: 60px">
                                        计划关闭
                                    </td>
                                    <td style="width: 60px;">
                                        <span style="color:Red;text-align:center;margin-top:auto;">*</span>保留房
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>  
                </table>
            </li>

            <li style="margin:0px;padding-top: 0px;">
                <div  style= "width:1160px;overflow-x:auto;border:1px   solid   white; "> 
                 <h4 id="lmbar2H" runat="server" style="padding-top: 0px; margin-top: 0px; margin-bottom: 0px;">LMBAR2</h4>
                 <table border="1" style="border-color:#D5D5D5;border-collapse: collapse;padding-top:0px" cellpadding="0" cellspacing="0">
                    <%for (int i = 0; i < RoomList.Rows.Count; i++)
                      {
                          if (RoomList.Rows[i]["ROOMCODE"].ToString() == "")
                          {
                          %>
                            <tr align="center" style="border-collapse:collapse">
                                <%for (int j = 0; j < TimeList.Rows.Count; j++)
                                  {
                                      if (!string.IsNullOrEmpty(TimeList.Rows[j]["time"].ToString()))
                                      {
                                          if (DateTime.Parse(TimeList.Rows[j]["time"].ToString()).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(TimeList.Rows[j]["time"].ToString()).DayOfWeek.ToString() == "Sunday")
                                          { 
                                            %>
                                                <td  onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#CDEBFF'" style="background-color: #CDEBFF; font-style: inherit;width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;">&nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;</td>
                                            <%
                                          }
                                          else
                                          { 
                                            %>
                                                <td  onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'" style="width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;">&nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;</td>
                                            <%
                                          }
                                      }
                                      else
                                      {
                                      %>
                                        <td onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'"  style="width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;">&nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;</td>
                                      <%
                                      }
                                  }%>
                            </tr>
                          <%}
                          else
                          { 
                            %>
                                <tr align="center">
                                <%if (i % 2 == 0)
                                  { %>
                                <td style="width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;background-color:#F6F6F6;"><%=RoomList.Rows[i]["ROOMNM"].ToString()%></td>
                                <%}
                                  else
                                  { 
                                   %>
                                <td style="width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;background-color:White;"><%=RoomList.Rows[i]["ROOMNM"].ToString()%></td>
                                <%
                                  }%>                                    
                                    <%for (int j = 1; j < TimeList.Rows.Count; j++)
                                      {
                                          if (i % 2 == 0)
                                          {  %>
                                          <td style=" border: solid #D5D5D5 1px;height:40px;background-color:#F6F6F6;"  onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#F6F6F6'">
                                          <%}
                                          else
                                          { 
                                          %>
                                            <td style=" border: solid #D5D5D5 1px;height:40px;background-color:White;"  onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='White'">
                                          <%  
                                          }
                                          for (int l = 0; l < Lmbar2PlanList.Rows.Count; l++)
                                          {
                                              if (Lmbar2PlanList.Rows[l]["ROOMTYPECODE"].ToString() == RoomList.Rows[i]["ROOMCODE"].ToString() && DateTime.Parse(Lmbar2PlanList.Rows[l]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(TimeList.Rows[j]["time"].ToString()))
                                              {
                                                  %>
                                                        <table cellpadding="0" cellspacing="0" width="100%" style="border-bottom: none;height:100%" onclick="ShowHisPlanInfo('LMBAR2','<%=RoomList.Rows[i]["ROOMCODE"].ToString() %>','<%=Lmbar2PlanList.Rows[l]["EFFECTDATESTRING"].ToString() %>')" >
                                                            <%if (Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString() == "0")
                                                              {
                                                                  if (Lmbar2PlanList.Rows[l]["STATUS"].ToString() == "false")
                                                                  {
                                                                  %>
                                                                  <tr><td style="background-color: #999999;font-style: inherit;text-align:center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#999999'" ><table>
                                                                       <tr align="center">
                                                                           <td >
                                                                           <span><%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                           <%
if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
{
                                                                                 %>
                                                                                  <span style="color:Red">*</span>
                                                                                 <%
}
                                                                           %>
                                                                           </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span>￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= Lmbar2PlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                        </table></td></tr>
                                                                  <%
                                                                  }
                                                                  else if (Lmbar2PlanList.Rows[l]["ISROOMFUL"].ToString() == "1")
                                                                  { 
                                                                  %>
                                                                  <tr><td style="background-color: #E96928;font-style: inherit;text-align:center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#E96928'" ><table>
                                                                        <tr align="center">
                                                                            <td >
                                                                            <span><%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                            <%
if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
{
                                                                                 %>
                                                                                          <span style="color:Red">*</span>
                                                                                         <%
}
                                                                            %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span>￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= Lmbar2PlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                        </table></td></tr>
                                                                    <%
                                                                  }
                                                                  else
                                                                  { 
                                                                    %>
                                                                     <tr><td style="background-color: #E6B9B6;font-style: inherit;text-align:center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#E6B9B6'" ><table>
                                                                        <tr align="center">
                                                                            <td">
                                                                            <span><%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                            <%
if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
{
                                                                                 %>
                                                                                          <span style="color:Red">*</span>
                                                                                         <%
}
                                                                            %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td><span>￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= Lmbar2PlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                        </table></td></tr>
                                                                    <%
                                                                  }
                                                              }
                                                              else {
                                                                  if (Lmbar2PlanList.Rows[l]["STATUS"].ToString() == "false")
                                                                  {
                                                                  %>
                                                                   <tr><td style="background-color: #999999;font-style: inherit;text-align:center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#999999'" ><table>
                                                                       <tr align="center">
                                                                            <td>
                                                                            <span><%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                            <%
                                                                              if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                                              {
                                                                                 %>
                                                                                  <span style="color:Red">*</span>
                                                                                 <%
                                                                                }
                                                                           %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td><span>￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= Lmbar2PlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                        </table></td></tr>
                                                                  <%
                                                                  }
                                                                  else
                                                                  { 
                                                                    %>
                                                                    <tr><td onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#F6F6F6'" ><table>
                                                                        <tr align="center">
                                                                            <td >
                                                                            <span><%= Lmbar2PlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                            <%
                                                                              if (Lmbar2PlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                                              {
                                                                                 %>
                                                                                  <span style="color:Red">*</span>
                                                                                 <%
                                                                                }
                                                                           %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td><span>￥<%= Lmbar2PlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= Lmbar2PlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                        </table></td></tr>
                                                                    <%
                                                                  }
                                                              } %>
                                                            
                                                        </table>
                                                  <%
                                              }
                                          }
                                      } %></td>
                                </tr>
                            <%
                          }
                      } %>
                </table>

                <h4  id="lmbarH" runat="server" style="padding-top: 0px; margin-top: 10px; margin-bottom: 0px;">LMBAR</h4>
                <table  border="1" style="border-color:#D5D5D5;border-collapse: collapse;" cellpadding="0" cellspacing="0">
                    <%for (int i = 0; i < RoomList.Rows.Count; i++)
                      {
                          if (RoomList.Rows[i]["ROOMCODE"].ToString() == "")
                          {
                          %>
                            <tr align="center" style="border-collapse:collapse">
                                <%for (int j = 0; j < TimeList.Rows.Count; j++)
                                  {
                                     if (!string.IsNullOrEmpty(TimeList.Rows[j]["time"].ToString()))
                                      {
                                          if (DateTime.Parse(TimeList.Rows[j]["time"].ToString()).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(TimeList.Rows[j]["time"].ToString()).DayOfWeek.ToString() == "Sunday")
                                          { 
                                            %>
                                                <td style="background-color: #CDEBFF; font-style: inherit;width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">&nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;</td>
                                            <%
                                          }
                                          else
                                          { 
                                            %>
                                                <td style="width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'" >&nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;</td>
                                            <%
                                          }
                                      }
                                      else
                                      {
                                      %>
                                        <td style="width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'">&nbsp;&nbsp;&nbsp;<%= TimeList.Rows[j]["timeMD"].ToString()%>&nbsp;&nbsp;&nbsp;</td>
                                      <%
                                      }
                                  } %>
                            </tr>
                          <%}
                          else
                          { 
                            %>
                                <tr align="center">
                                  <%if (i % 2 == 0)
                                  { %>
                                <td style="width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;background-color:#F6F6F6;"><%=RoomList.Rows[i]["ROOMNM"].ToString() %></td>
                                <%}
                                  else
                                  { 
                                   %>
                                <td style="width:100px;height:40px;white-space:nowrap; border: solid #D5D5D5 1px;background-color:White;"><%=RoomList.Rows[i]["ROOMNM"].ToString() %></td>
                                <%
                                  }%>

                                    
                                    <%for (int j = 1; j < TimeList.Rows.Count; j++)
                                      {
                                          if (i % 2 == 0)
                                          {  %>
                                          <td style=" border: solid #D5D5D5 1px;background-color:#F6F6F6;"  onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#F6F6F6'">
                                          <%}
                                          else
                                          { 
                                          %>
                                            <td style=" border: solid #D5D5D5 1px;background-color:White;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='white'">
                                          <%  
                                          }
                                          for (int l = 0; l < LmbarPlanList.Rows.Count; l++)
                                          {
                                              if (LmbarPlanList.Rows[l]["ROOMTYPECODE"].ToString() == RoomList.Rows[i]["ROOMCODE"].ToString() && DateTime.Parse(LmbarPlanList.Rows[l]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(TimeList.Rows[j]["time"].ToString()))
                                              {
                                                  %>
                                                        <table cellpadding="0" cellspacing="0" width="100%" style="border-bottom: none;height:100%" onclick="ShowHisPlanInfo('LMBAR','<%=RoomList.Rows[i]["ROOMCODE"].ToString() %>','<%=LmbarPlanList.Rows[l]["EFFECTDATESTRING"].ToString() %>')">
                                                            <%if (LmbarPlanList.Rows[l]["ROOMNUM"].ToString() == "0")
                                                              {
                                                                  if (LmbarPlanList.Rows[l]["STATUS"].ToString() == "false")
                                                                  {
                                                                  %>
                                                                    <tr><td style="background-color: #999999;font-style: inherit;text-align:center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#999999'" ><table>
                                                                       <tr align="center">
                                                                           <td >
                                                                           <span><%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                           <%
                                                                              if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                                              {
                                                                                 %>
                                                                                  <span style="color:Red">*</span>
                                                                                 <%
                                                                                }
                                                                           %>
                                                                           </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span>￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= LmbarPlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                         </table></td></tr>
                                                                  <%
                                                                  }
                                                                  else if (LmbarPlanList.Rows[l]["ISROOMFUL"].ToString() == "1")
                                                                  { 
                                                                  %>
                                                                  <tr><td style="background-color: #E96928;font-style: inherit;text-align:center" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#E96928'" ><table>
                                                                        <tr align="center">
                                                                            <td >
                                                                            <span><%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                            <%
                                                                      if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
{
                                                                                 %>
                                                                                          <span style="color:Red">*</span>
                                                                                         <%
}
                                                                            %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span>￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= LmbarPlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                        </table></td></tr>
                                                                    <%
                                                                  }
                                                                  else
                                                                  { 
                                                                    %>
                                                                    <tr><td style="background-color: #E6B9B6;font-style: inherit;text-align:center;width:100%" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#E6B9B6'" ><table>
                                                                        <tr align="center">
                                                                            <td >
                                                                            <span><%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                            <%
                                                                              if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                                              {
                                                                                 %>
                                                                                  <span style="color:Red">*</span>
                                                                                 <%
                                                                                }
                                                                           %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span>￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= LmbarPlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                        </table></td></tr>
                                                                    <%
                                                                  }
                                                              }
                                                              else {
                                                                  if (LmbarPlanList.Rows[l]["STATUS"].ToString() == "false")
                                                                  {
                                                                  %>
                                                                  <tr><td style="background-color: #999999;font-style: inherit;text-align:center;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#999999'" ><table>
                                                                       <tr align="center">
                                                                            <td >
                                                                            <span><%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                            <%
                                                                              if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                                              {
                                                                                 %>
                                                                                  <span style="color:Red">*</span>
                                                                                 <%
                                                                                }
                                                                           %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span>￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                       </tr>
                                                                       <tr align="center">
                                                                            <td ><span><%= LmbarPlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                       </table></td></tr>
                                                                  <%
                                                                  }
                                                                  else
                                                                  { 
                                                                    %>
                                                                    <tr><td  onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#F6F6F6'" ><table>
                                                                        <tr align="center">
                                                                            <td >
                                                                            <span><%= LmbarPlanList.Rows[l]["ROOMNUM"].ToString()%></span>
                                                                            <%
                                                                              if (LmbarPlanList.Rows[l]["ISRESERVE"].ToString() == "0")
                                                                              {
                                                                                 %>
                                                                                  <span style="color:Red">*</span>
                                                                                 <%
                                                                                }
                                                                           %>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td><span>￥<%= LmbarPlanList.Rows[l]["TWOPRICE"].ToString()%></span></td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td ><span><%= LmbarPlanList.Rows[l]["SOURCE"].ToString()%></span></td>
                                                                        </tr>
                                                                        </table></td></tr>
                                                                    <%
                                                                  }
                                                              } %>
                                                            
                                                        </table>
                                                    
                                                  <%
                                              }
                                          }
                                      } %></td>
                                </tr>
                            <%
                          }
                      } %>
                </table>
                 </div>
            </li>
        </ul>
    </div>
           </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
     <ContentTemplate>

   <div id="bgDiv2" class="bgDiv2">        
    </div>
    <div id="popupDiv2" class="popupDiv2" style="text-align:center">
        <table style="margin:auto">
            <tr>
                <td align="center">
                    <asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Auto" Width="550px">
                    <div id="dvHisPlanInfoList" runat="server"/>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    </br>
                    <input type="button" value="关闭" class="btn" onclick="invokeClose2()" />
                </td>
            </tr>
        </table>
    </div>

   <div id="background" class="pcbackground" style="display: none; "></div> 
   <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 

   <input type="hidden" runat="server" id="HiddenEffectDate" />
   <input type="hidden" runat="server" id="HiddenRoomCode" />
   <input type="hidden" runat="server" id="HiddenRoomType" />
   <input type="hidden" runat="server" id="HiddenPriceCode" />
   <input type="hidden" runat="server" id="HiddenPrice" />
   <input type="hidden" runat="server" id="HidHotelID" />

       </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
 