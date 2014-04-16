<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelSalesPlanDetail.aspx.cs"  Title="酒店管理" Inherits="HotelSalesPlanDetail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<style type="text/css" >
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
.dvHourCss { 
float:left; 
border:1px #999999 solid;
width:35px;
text-align:center;
vertical-align:middle;
height:20px;
margin-left:-1px;
}
</style>
<script language="javascript" type="text/javascript">
    function CloseClickEvent() {
        window.opener = null;
        window.open("", "_self");
        window.close();
    }

    function checkRoomStatus(val) {
        if ("true" == val) {
            $("#roomNumTd").show();
            $("#roomNumTr").show();
        } else if ("false" == val) {
            $("#roomNumTd").hide();
            $("#roomNumTr").hide();
        }
    }

    function SetPlanStyle(val) {
        if ("0" == val) {
            document.getElementById("<%=dvDTime.ClientID%>").style.display = "none";
            document.getElementById("<%=dvEachFor.ClientID%>").style.display = "none";
        } else if ("1" == val) {
            document.getElementById("<%=dvDTime.ClientID%>").style.display = "";
            document.getElementById("<%=dvEachFor.ClientID%>").style.display = "none";
        } else if ("2" == val) {
            document.getElementById("<%=dvDTime.ClientID%>").style.display = "none";
            document.getElementById("<%=dvEachFor.ClientID%>").style.display = "";
        }
    }

    function SetPlanWeekListVal(arg) {
        if (document.getElementById("<%=chkPlanWeek.ClientID%>") != null) {
            var objCheck = document.getElementById("<%=chkPlanWeek.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < objCheck.length; i++) {
                if (arg == "1") {
                    document.getElementById("<%=chkPlanWeek.ClientID%>" + "_" + i).checked = true;
                }
                else {
                    document.getElementById("<%=chkPlanWeek.ClientID%>" + "_" + i).checked = false;
                }
            }
        }
    }

    function SetContronListVal() {
        document.getElementById("<%=messageContent.ClientID%>").innerText = "";
        var commidList = "";

        var weekList = "";
        if (document.getElementById("<%=chkWeekList.ClientID%>") != null) {
            var objWeekCheck = document.getElementById("<%=chkWeekList.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < objWeekCheck.length; i++) {
                if (document.getElementById("<%=chkWeekList.ClientID%>" + "_" + i).checked) {

                    weekList = weekList + document.getElementById("<%=chkWeekList.ClientID%>" + "_" + i).value + ',';
                }
            }
            document.getElementById("<%=hidWeekList.ClientID%>").value = weekList;
        }

        var planWeekList = "";
        if (document.getElementById("<%=chkPlanWeek.ClientID%>") != null) {
            var objPlanWeekCheck = document.getElementById("<%=chkPlanWeek.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < objPlanWeekCheck.length; i++) {
                if (document.getElementById("<%=chkPlanWeek.ClientID%>" + "_" + i).checked) {

                    planWeekList = planWeekList + document.getElementById("<%=chkPlanWeek.ClientID%>" + "_" + i).value + ',';
                }
            }
            document.getElementById("<%=hidPlanWeekList.ClientID%>").value = planWeekList;
        }
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
  <div id="right">
     <div class="frame01">
      <ul>
        <li class="title">计划起止日期</li>
        <li>
            <table>
            <tr>
                <td align="right" style="width:92px">
                    <font color="red">*</font>起始日期：
                </td>
                <td>
                    <div style="display:none"><asp:TextBox ID="txtYestoday" runat="server"/></div>
                    <input id="dpKeepStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_txtYestoday\')}',maxDate:'#F{$dp.$D(\'MainContent_dpKeepEnd\')||\'2020-10-01\'}'})" runat="server"/>
                </td>
                <td style="width:12px"></td>
                <td align="right">
                    <font color="red">*</font>结束日期：
                </td>
                <td>
                    <input id="dpKeepEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpKeepStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                </td>
                <%--<td style="width:12px"></td>
                <td align="right">
                    <font color="red">*</font>开放时间：
                </td>
                <td>
                    <asp:DropDownList ID="ddpEffHour" runat="server" CssClass="noborder_inactive" Width="120px"></asp:DropDownList>
                </td>--%>
            </tr>
            </table>
            <table> 
                <tr>
                    <td style="width:22px"></td>
                    <td>
                        <asp:CheckBoxList ID="chkWeekList" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                    </td>
                    <td>
                        <input type="button" id="btnAll" class="btn primary"" value="全选"  onclick="SetWeekListVal('1')" />
                        <input type="button" id="btnUnAll" class="btn" value="反选"  onclick="SetWeekListVal('0')" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <font color="red">*</font>开放时间：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpEffHour" runat="server" CssClass="noborder_inactive" Width="120px"></asp:DropDownList>
                    </td>
                    <td align="center" valign="middle">
                        <div runat="server" id="dvHourList">
                            <div class="dvHourCss" runat="server"  id="dvHr0">0-1点</div><div class="dvHourCss" runat="server"  id="dvHr1">1-2点</div><div class="dvHourCss" runat="server"  id="dvHr2">2</div><div class="dvHourCss" runat="server"  id="dvHr3">3</div><div class="dvHourCss" runat="server"  id="dvHr4">4</div><div class="dvHourCss" runat="server"  id="dvHr5">5</div><div class="dvHourCss" runat="server"  id="dvHr6">6</div><div class="dvHourCss" runat="server"  id="dvHr7">7</div><div class="dvHourCss" runat="server"  id="dvHr8">8</div><div class="dvHourCss" runat="server"  id="dvHr9">9</div>
                            <div class="dvHourCss" runat="server"  id="dvHr10">10</div><div class="dvHourCss" runat="server"  id="dvHr11">11</div><div class="dvHourCss" runat="server"  id="dvHr12">12</div><div class="dvHourCss" runat="server"  id="dvHr13">13</div><div class="dvHourCss" runat="server"  id="dvHr14">14</div><div class="dvHourCss" runat="server"  id="dvHr15">15</div><div class="dvHourCss" runat="server"  id="dvHr16">16</div><div class="dvHourCss" runat="server"  id="dvHr17">17</div><div class="dvHourCss" runat="server"  id="dvHr18">18</div>
                            <div class="dvHourCss" runat="server"  id="dvHr19">19</div><div class="dvHourCss" runat="server"  id="dvHr20">20</div><div class="dvHourCss" runat="server"  id="dvHr21">21</div><div class="dvHourCss" runat="server"  id="dvHr22">22</div><div class="dvHourCss" runat="server"  style="width:50px" id="dvHr23">23-24点</div>
                        </div>
                    </td>
                </tr>
            </table>
        </li>
        <li></li>
     </ul>
     </div>
     <div class="frame01">
      <ul>
        <li class="title">选择酒店房型</li>
        <li>
             <table style="line-height:25px;">
                 <tr>
                    <td align="right" style="width:82px">
                        酒店ID：
                    </td>
                    <td>
                         <asp:Label runat="server" ID="lbHotel" />
                    </td>
                    <td align="right">价格代码类型：</td>
                    <td>
                        <asp:Label runat="server" ID="lbRateCode" />
                    </td>
                    <td>
                        货币：CNY-人民币
                    </td>
                 </tr>
                 <tr>
                 <td style="height:5px"></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table>
                            <tr>
                                <td align="right" style="width:80px">
                                    选择房型：
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbHotelRoomList" />
                                </td>
                                <td style="width:120px"></td>
                                <td>计划状态：</td>
                                <td>
                                    <asp:DropDownList ID="ddpRoomStatus" Width="77px" runat="server" onchange="checkRoomStatus(this.value);"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right"><span id="roomNumTr">&nbsp;房量设置：</span></td>
                                <td align="left"><span id="roomNumTd">
                                    <asp:TextBox ID="txtRoomCount" runat="server" Width="70px" MaxLength="3"/>&nbsp;间&nbsp;</span>
                                </td>
                                <td style="height:20px"></td>
                                <td align="right">保留房：</td>
                                <td>
                                    <asp:DropDownList ID="ddpIsReserve" Width="77px" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width:80px">
                        保证金制度：
                    </td>
                    <td align="left" colspan="4">
                        <asp:DropDownList ID="ddpGuaid" runat="server" CssClass="noborder_inactive" Width="450px"></asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td align="right">
                        取消制度：
                    </td>
                    <td align="left" colspan="4">
                        <asp:DropDownList ID="ddpCxlid" runat="server" CssClass="noborder_inactive" Width="450px"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </li>
        <li></li>
     </ul>
     </div>
     <div class="frame01">
      <ul>
        <li class="title">设置价格</li>
        <li>
            <table>
                <tr>
                    <td style="width:22px"></td>
                    <td style="display:none">单人价</td>
                    <td><font color="red">*计划供应商</font></td>
                    <td><font color="red">*</font>二人价</td>
                    <td>三人价</td>
                    <td>四人价</td>
                    <td>加床价</td>
                    <td>网络价</td>
                    <td>早餐数量</td>
                    <td>每份早餐价格</td>
                    <td>免费宽带</td>
                    <td>价格浮动设置</td>
                </tr>
                <tr>
                    <td></td>
                    <td style="display:none"><asp:TextBox ID="txtOnePrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:DropDownList ID="ddpSup" runat="server" Width="77px"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtTwoPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:TextBox ID="txtThreePrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:TextBox ID="txtFourPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:TextBox ID="txtBedPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:TextBox ID="txtNetPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:DropDownList ID="ddpBreakfastNum" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtBreakPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:DropDownList ID="ddpIsNetwork" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtOffsetval" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:DropDownList ID="ddpOffsetunit" runat="server" Width="77px"></asp:DropDownList></td>
                </tr>
            </table>
        </li>
        <li></li>
     </ul>
     </div>
     <div class="frame01">
      <ul>
        <li class="title">保存计划</li>
        <li>
            <div id="dvDTime" style="display:none" runat="server">
                <font color="red">*</font>保存时间：<input id="dpPlanDTime" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" runat="server"/>
            </div>
            <div id="dvEachFor" style="display:none" runat="server">
            <table>
                <tr>
                    <td align="right">
                        <font color="red">*</font>保存时间：
                    </td>
                    <td colspan="4">
                        <input id="dpPlanTime" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'H:mm:ss'})" runat="server"/>
                    </td>
                </tr>
                <tr style="display:none">
                    <td align="right" style="width:92px">
                        <font color="red">*</font>起始日期：
                    </td>
                    <td>
                        <input id="dpPlanStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpPlanEnd\')||\'2020-10-01\'}'})" runat="server"/>
                    </td>
                    <td style="width:12px"></td>
                    <td align="right">
                        <font color="red">*</font>结束日期：
                    </td>
                    <td>
                        <input id="dpPlanEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpPlanStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                </tr>
            </table>
            <table style="display:none">
                <tr>
                    <td style="width:22px"></td>
                    <td>
                        <asp:CheckBoxList ID="chkPlanWeek" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                    </td>
                    <td>
                        <input type="button" id="btnPlanAll" class="btn primary" value="全选"  onclick="SetPlanWeekListVal('1')" />
                        <input type="button" id="btnPlanUnAll" class="btn" value="反选"  onclick="SetPlanWeekListVal('0')" />
                    </td>
                </tr>
            </table>
            </div>
        </li>
        <li>
             <table>
                <tr>
                    <td style="width:28px"></td>
                    <td>
                        保存方式：
                    </td>
                     <td>
                        <asp:Label runat="server" ID="lbSaveType" />
                    </td>
                    <td style="width:28px"></td>
                     <td>
                        自动执行状态：
                    </td>
                     <td>
                        <asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server"/>
                        <div style="display:none" runat="server" id="dvPlanStatus">
                            <asp:Label runat="server" ID="lbPlanStatus" />
                        </div>
                    </td>
                </tr>
             </table>
        </li>
        <li>
           <div id="messageContent" runat="server" style="color:red;"></div>
           <div id="background" class="pcbackground" style="display: none; "></div> 
           <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
        </li>
        <li>
             <table>
                <tr>
                    <td style="width:30px"></td>
                    <td>
                        <div id="dvSaveStyle" runat="server" style="display:none">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetContronListVal();BtnLoadStyle();" onclick="btnSave_Click" />
                        </div>
                    </td>
                     <td>
                        <input type="button" id="btnClear" class="btn" value="关闭" onclick="CloseClickEvent()" />
                    </td>
                </tr>
             </table>
        </li>
        <li></li>
      </ul>
    </div>
     <div class="frame01">
      <ul>
        <li class="title">修改历史</li>
        <li>
            <asp:GridView ID="gridViewCSServiceList" runat="server" 
                AutoGenerateColumns="False" BackColor="White" CellPadding="4" CellSpacing="1"
                        Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                        onrowdatabound="gridViewCSServiceList_RowDataBound" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="SAVETYPENM" HeaderText="更新方式" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                    <asp:BoundField DataField="PLANTIME" HeaderText="定时执行时间" ><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
                    <asp:BoundField DataField="PLANSTART" HeaderText="定时开始日期" ><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
                    <asp:BoundField DataField="PLANEND" HeaderText="定时结束日期" ><ItemStyle HorizontalAlign="Center" Width="8%"/></asp:BoundField>
                    <asp:BoundField DataField="PLANWEEK" HeaderText="星期详情" ><ItemStyle HorizontalAlign="Center" Width="8%" /></asp:BoundField>
                    <asp:BoundField DataField="UPDATETIME" HeaderText="最后修改时间" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                    <asp:BoundField DataField="UPDATEUSER" HeaderText="最后修改人" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
           </asp:GridView>
        </li>
      </ul>
   </div>
<asp:HiddenField ID="hidHotelID" runat="server"/>
<asp:HiddenField ID="hidPlanID" runat="server"/>
<asp:HiddenField ID="hidSaveType" runat="server"/>
<asp:HiddenField ID="hidWeekList" runat="server"/>
<asp:HiddenField ID="hidPlanWeekList" runat="server"/>
<asp:HiddenField ID="hidPriceType" runat="server"/>
<asp:HiddenField ID="hidHotelRoomList" runat="server"/>
<asp:HiddenField ID="hidEffHour" runat="server"/>
</div>
</asp:Content>