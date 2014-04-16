<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="LmOrderAnalysis.aspx.cs"  Title="LM订单趋势分析" Inherits="LmOrderAnalysis" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<link href="../../Styles/jquery.jqplot.min.css" rel="stylesheet" type="text/css" />
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

.colhidden { display:none;}

</style>


<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtQueryNm.ClientID%>").value = "";
        document.getElementById("<%=txtOrderID.ClientID%>").value = "";
        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
        document.getElementById("<%=txtMobile.ClientID%>").value = "";
        document.getElementById("<%=dpInStart.ClientID%>").value = "";
        document.getElementById("<%=dpInEnd.ClientID%>").value = "";

        document.getElementById("wctHotel").value = "";
        document.getElementById("wctHotel").text = "";
        document.getElementById("wctCity").value = "";
        document.getElementById("wctCity").text = "";

        document.getElementById("<%=ddpBookStatus.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpPayStatus.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpTicket.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpAprove.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpHotelComfirm.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpPlatForm.ClientID%>").selectedIndex = 0;

        document.getElementById("<%=chkAllCommon.ClientID%>").checked = true;
        if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
            var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < objCheck.length; i++) {
                if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked) {
                    document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked = false;
                }
                document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).disabled = true;
            }
        }
        document.getElementById("<%=dvBtnUpdate.ClientID%>").style.display = 'none';
        document.getElementById("<%=dvBtnInsert.ClientID%>").style.display = ''; 
    }

    function UpdateRow(arg) {
        document.getElementById("<%=hidRowID.ClientID%>").value = arg;
        document.getElementById("<%=btnUpdate.ClientID%>").click();
    }

    function SetControlValue() {
        document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
        document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
        var commidList = "";
        if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
            var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < objCheck.length; i++) {
                if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked) {

                    commidList = commidList + document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).value + ',';
                }
            }
            document.getElementById("<%=hidCommonList.ClientID%>").value = commidList;
        }
    }

    function SetChkAllCommonStyle() {
        if (document.getElementById("<%=chkAllCommon.ClientID%>").checked == true) {
            if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
                var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objCheck.length; i++) {
                    if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked) {
                        document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked = false;
                    }
                    document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).disabled = true;
                }
            }
        }
        else {
            if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
                var objCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objCheck.length; i++) {
                    if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).disabled) {
                        document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).disabled = false;
                    }
                }
            }
        }

        SetControlValue();
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
<script language="javascript" type="text/javascript">
    function DrawingLines() {
        var chartData = <%=chartData%>;
        var chartName = <%=chartName %>;
        var plot1 = $.jqplot('chart1', chartData, {
        title:'  Order  Analytics  ',
        axes:{
            xaxis:{
                renderer:$.jqplot.DateAxisRenderer,
                tickOptions:{
                    formatString: document.getElementById("<%=hidFormatString.ClientID%>").value
                }
            },
            yaxis:{
                tickOptions:{
                    }
            }
        },
        highlighter: {
            show: true,
            sizeAdjust: 7.5
        },
        cursor: {
            show: false
        },
        legend: {
            show: true,
            placement: 'outsideGrid',
            labels: chartName
        }
        });

        ClearClickEvent();
        BtnCompleteStyle();
    }
</script>

<!--[if IE]><script language="javascript" type="text/javascript" src="../../Scripts/jqlot/excanvas.min.js"></script><![endif]-->
<script src="../../Scripts/jqlot/jquery.jqplot.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.cursor.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.highlighter.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.pointLabels.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.logAxisRenderer.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.canvasTextRenderer.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.canvasAxisTickRenderer.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.canvasAxisLabelRenderer.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.dateAxisRenderer.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.categoryAxisRenderer.min.js" type="text/javascript"></script>
<script src="../../Scripts/jqlot/jqplot.barRenderer.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">订单趋势分析</li>
        <li>
            <table>
                <tr>
                    <td align="right">
                        统计条件名称：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtQueryNm" runat="server" style="width:348px;" maxlength="200" value=""/><font color="red">*</font>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        订单ID：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtOrderID" runat="server" style="width:348px;" maxlength="200" value=""/>
                    </td>
                   <td align="right">
                        手机号码：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtMobile" runat="server" style="width:140px;" maxlength="15" value=""/>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        选择城市：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" AutoParent="LmOrderAnalysis.aspx?Type=city" />
                    </td>
                    <td align="right">
                        选择酒店：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="LmOrderAnalysis.aspx?Type=hotel" />
                    </td>
                </tr>
                <tr>
                     <td align="right">
                        下单时间：
                    </td>
                    <td>
                        <input id="dpCreateStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server"/><font color="red">*</font>
                    </td>
                   <td align="right">
                        入住时间：
                    </td>
                    <td>
                        <input id="dpInStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpInEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpInEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpInStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                </tr>
                 <tr>
                    <td align="right">
                        订单类型：
                    </td>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="chkPayCode" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="8" />
                                </td>
                                <td>
                                    <div id="dvHotelChkCommon" runat="server"><input type="checkbox" name="chkAllCommon" id="chkAllCommon" runat="server" onclick="SetChkAllCommonStyle()"/>不限制</div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        订单状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpBookStatus" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                    </td>
                    <td align="right">
                        酒店确认状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpHotelComfirm" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                    </td>
                   
                </tr>
                <tr>
                     <td align="right">
                        支付状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpPayStatus" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                    </td>
                    <td align="right">
                        审核状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpAprove" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td align="right">
                        使用优惠券：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpTicket" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                    </td>
                    <td align="right">
                        订单来源：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpPlatForm" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div id="messageContent" runat="server" style="color:red"></div>
                    </td>
                </tr>
                </table>
        </li>
      </ul>
    </div>
    <div class="frame02">
          <table width="98%">
            <tr>
                <td style="width:3%"></td>
                <td colspan="3" style="text-align:left;vertical-align:top;width:20%">
                    <div id="dvBtnInsert" runat="server">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="添加统计条件" OnClientClick="SetControlValue()" onclick="btnSearch_Click" />
                    </div>
                    <div style="display:none">
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="Update" onclick="btnUpdate_Click" />
                    </div>
                    <div id="dvBtnUpdate" runat="server" style="display:none">
                        <asp:Button ID="btnModify" runat="server" CssClass="btn primary" Text="更新" OnClientClick="SetControlValue()" onclick="btnModify_Click" />
                        <input type="button" id="btnClear" class="btn" value="取消"  onclick="ClearClickEvent();" />
                    </div>
                </td>
                <td style="width:75%">
                    <asp:GridView ID="gridViewCSReviewLmSystemLogList" runat="server" 
                            AutoGenerateColumns="False" BackColor="White" 
                        CellPadding="4" CellSpacing="1" Width="100%" 
                            EmptyDataText="" DataKeyNames="ID" 
                        onrowdatabound="gridViewCSReviewLmSystemLogList_RowDataBound" 
                        onrowdeleting="gridViewCSReviewLmSystemLogList_RowDeleting" 
                        CssClass="GView_BodyCSS">
                        <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID"><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="QueryNm" HeaderText="统计条件名称" ><ItemStyle HorizontalAlign="Center" Width="90%"/></asp:BoundField>
                                <asp:BoundField DataField="OrderID" HeaderText="订单ID" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden" /></asp:BoundField>
                                <asp:BoundField DataField="Mobile" HeaderText="手机号码" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="CityID" HeaderText="选择城市" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="HotelID" HeaderText="选择酒店" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="StartDTime" HeaderText="下单时间开始日期" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="EndDTime" HeaderText="下单时间结束日期" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="InStart" HeaderText="入住时间开始日期" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="InEnd" HeaderText="入住时间结束日期" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="PayCode" HeaderText="订单类型" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="BookStatus" HeaderText="订单状态" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="HotelComfirm" HeaderText="酒店确认状态" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="PayStatus" HeaderText="支付状态" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="Aprove" HeaderText="审核状态" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="Ticket" HeaderText="使用优惠券" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>
                                <asp:BoundField DataField="PlatForm" HeaderText="订单来源" ><ItemStyle HorizontalAlign="Center" CssClass="colhidden"/></asp:BoundField>

                                <asp:CommandField ShowDeleteButton="true" DeleteText="删除" >
                                    <ItemStyle HorizontalAlign="Center" Width="5%"/>
                                </asp:CommandField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">
                                    <ItemTemplate>
                                        <a href="#" onclick="UpdateRow('<%# DataBinder.Eval(Container.DataItem, "ID") %>')">编辑</a>
                                    </ItemTemplate>
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
    <div class="frame01">
        <table>
            <tr>
                <td style="width:42px"></td>
                <td align="right">
                        统计间隔：
                </td>
                <td>
                    <asp:DropDownList ID="ddpTimeSpace" runat="server" Width="150px" Height="25px"></asp:DropDownList>
                </td>
                <td>
                     <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="绘制趋势图" OnClientClick="BtnLoadStyle()" onclick="btnExport_Click"/>
                </td>
            </tr>
        </table>
    </div>
    <div class="frame01">
        <div id="chart1" style="width:1200px; height:400px"></div>
    </div>
    
    <div id="background" class="pcbackground" style="display: none; "></div> 
    <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
</div>
<asp:HiddenField ID="hidHotel" runat="server"/>
<asp:HiddenField ID="hidCity" runat="server"/>
<asp:HiddenField ID="hidCommonList" runat="server"/>
<asp:HiddenField ID="hidRowID" runat="server"/>
<asp:HiddenField ID="hidFormatString" runat="server"/>
</asp:Content>