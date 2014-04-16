<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="LmOrderSurveyAnalysis.aspx.cs"
    Title="LM订单Survey分析" Inherits="WebUI_DBQuery_LmOrderSurveyAnalysis" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.jqplot.min.css" rel="stylesheet" type="text/css" />
    <!--[if IE]><script language="javascript" type="text/javascript" src="../../Scripts/jqlot/excanvas.min.js"></script><![endif]-->
    <script src="../../Scripts/jqlot/jquery.jqplot.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jqlot/jqplot.cursor.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jqlot/jqplot.highlighter.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jqlot/jqplot.cursor.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jqlot/jqplot.dateAxisRenderer.min.js" type="text/javascript"></script>
    <style type="text/css">
        .pcbackground
        {
            display: block;
            width: 100%;
            height: 100%;
            opacity: 0.4;
            filter: alpha(opacity=40);
            background: while;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 2000;
        }
        .pcprogressBar
        {
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
        
        .colhidden
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function BtnLoadStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
            ajaxbg.show();
        }

        function BtnCompleteStyle() {
            var ajaxbg = $("#background,#progressBar");
            ajaxbg.hide();
        }

        function ClearClickEvent() {
            document.getElementById("<%=StartDate.ClientID%>").value = "";
            document.getElementById("<%=EndDate.ClientID%>").value = "";

            if (document.getElementById("<%=chkPlatForm.ClientID%>") != null) {
                var objCheck = document.getElementById("<%=chkPlatForm.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objCheck.length; i++) {
                    if (document.getElementById("<%=chkPlatForm.ClientID%>" + "_" + i).checked) {
                        document.getElementById("<%=chkPlatForm.ClientID%>" + "_" + i).checked = false;
                    }
                }
            }

            if (document.getElementById("<%=chkPayCode.ClientID%>") != null) {
                var objPayCheck = document.getElementById("<%=chkPayCode.ClientID%>").getElementsByTagName("input");
                for (var i = 0; i < objPayCheck.length; i++) {
                    if (document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked) {
                        document.getElementById("<%=chkPayCode.ClientID%>" + "_" + i).checked = false;
                    }
                }
            }
        }


    </script>
    <script language="javascript" type="text/javascript">
        function DrawingLines() {
            //var d1 =[[1, 2], [3, 5.12], [5, 13.1], [7, 33.6], [9, 85.9], [11, 219.9]];
            //var d2 =[[1, 5], [3, 36.56], [5, 13.1], [7, 33.6], [9, 55.9], [11, 119.9]];
            var chartDataNPS = <%=chartDataNPS%>;
            var chartData2 = <%=chartData%>;
//            var d1 = [['2013-02-02', 1.23], ['2013-02-03', 2], ['2013-02-04', 5.12], ['2013-02-05', 13.1], ['2013-02-06', 33.6], ['2013-02-07', 85.9], ['2013-02-08', 219.9], ['2013-02-09', 259.9]];
//            var d2 = [['2013-02-02', 2.23], ['2013-02-03', 5], ['2013-02-04', 36.56], ['2013-02-05', 13.1], ['2013-02-06', 33.6], ['2013-02-07', 55.9], ['2013-02-08', 119.9], ['2013-02-09', 139.9]];
            //var d1 = [['2013-02-01', -0.30],['2013-02-02', -0.30],['2013-02-03', -0.30],['2013-02-04', -0.30],['2013-02-05', -0.30],['2013-02-06', -0.30],['2013-02-07', -0.30],['2013-02-08', -0.30],['2013-02-09', -0.30],['2013-02-10', -0.30],['2013-02-11', -0.30],['2013-02-12', -0.30],['2013-02-13', -0.30],['2013-02-14', -0.30],['2013-02-15', -0.30],['2013-02-16', -0.30],['2013-02-17', -0.30],['2013-02-18', -0.30],['2013-02-19', -0.30],['2013-02-20', -0.30],['2013-02-21', -0.30],['2013-02-22', -0.30],['2013-02-23', -0.30],['2013-02-24', -0.30],['2013-02-25', -0.30],['2013-02-26', -0.30],['2013-02-27', -0.30]];
            //var d2 =[['2013-02-01', 0.00],['2013-02-02', 0.00],['2013-02-03', 0.00],['2013-02-04', 0.00],['2013-02-05', 0.00],['2013-02-06', 0.00],['2013-02-07', 0.00],['2013-02-08', 0.00],['2013-02-09', 0.00],['2013-02-10', 0.00],['2013-02-11', 0.00],['2013-02-12', 0.00],['2013-02-13', 0.00],['2013-02-14', 0.00],['2013-02-15', 0.00],['2013-02-16', -1.00],['2013-02-17', -1.00],['2013-02-18', 0.00],['2013-02-19', 0.00],['2013-02-20', 0.00],['2013-02-21', 0.00],['2013-02-22', -0.50],['2013-02-23', 0.00],['2013-02-24', 0.00],['2013-02-25', -0.67],['2013-02-26', -0.67],['2013-02-27', 0.00]];
            //$.jqplot('chart1', [d1,d2],
            $.jqplot('chart1', [chartDataNPS,chartData2],
            {
                title: {  
                    text: 'Survey',   // 设置当前图的标题  
                    show: true//设置当前标题是否显示  
                }, 
                axes: { 
                    xaxis: { 
                        renderer: $.jqplot.DateAxisRenderer, 
                        tickOptions: {
//                          formatString:'%#m/%#d/%y',
                            formatString:'%#m-%#d',
                            fontSize:'15px',
                            fontFamily: 'Tahoma'                            
                            }
                        },
                    yaxis:{
                        tickOptions: { 
                            formatString: '%.2f',
                            fontSize:'15px',
                            fontFamily: 'Tahoma' }
                    }
                },
                seriesDefaults:{
                    show:true,
                    xaxis:'xaxis',
                    yaxis: 'yaxis', 
                    markerOptions: {   
                        show:true,
                        style: 'filledCircle'
                    },
                    pointLabels: {
                        location: 's', //数据标签显示在数据点附近的方位  
                        ypadding: 2   //数据标签距数据点在纵轴方向上的距离  
                    }  
                },
                highlighter: {
                    show: true,
                    lineWidthAdjust: 2.5,
                    tooltipFormatString: '%.5P',
                    sizeAdjust: 7.5
                },
                cursor: {
                    show: false,
                    tooltipFormatString: '%.4P',
                    useAxesFormatters: true
                },
                series:[{
                lineWidth:1, 
                markerOptions:{style:'square'},
                color: '#5FAB78'
                }],
                legend:{
                    show:true,
                    location: 'ne',
                     xoffset: 12,  
                     yoffset: 12,
                     labels: ['总平均NPS值','日平均NPS值']
                }
            }); 
        } 
    </script>
    <div id="right">
        <div class="frame01">
            <ul>
                <li class="title">订单Survey分析</li>
                <li>
                    <table>
                        <tr>
                            <td align="right">
                                订单开始日期：
                            </td>
                            <td>
                                <input id="StartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_EndDate\')||\'2020-10-01\'}'})"
                                    runat="server" /><font color="red">*</font>
                            </td>
                            <td align="right">
                                订单结束日期：
                            </td>
                            <td>
                                <input id="EndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_StartDate\')}',maxDate:'2020-10-01'})"
                                    runat="server" /><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                订单平台选择：
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkPlatForm" runat="server" RepeatDirection="Vertical" RepeatLayout="Table"
                                    RepeatColumns="8" />
                            </td>
                            <td align="right">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                订单状态筛选：
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkPayCode" runat="server" RepeatDirection="Vertical" RepeatLayout="Table"
                                    RepeatColumns="8" />
                            </td>
                            <td align="right">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="btn primary" Text="查询" OnClientClick="BtnLoadStyle();"
                                    OnClick="btnSearch_Click" />
                                <input type="button" id="Button2" class="btn" value="重置" onclick="ClearClickEvent()" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div id="messageContent" runat="server" style="color: red">
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
        </div>
        <div id="NPSDiv" runat="server" style="display: none; padding-left: 40px;">
            平均NPS值：<asp:Label runat="server" ID="lblNPS" Text=""></asp:Label></div>
        <div class="frame01" style="width: 1450px; overflow-x: auto; border: 1px   solid   white;"
            id="DateDiv" runat="server">
        </div>
        <div class="frame01" id="chartDiv" runat="server" style="display: none;">
            <div id="chart1" style="width: auto; height: 400px">
            </div>
        </div>

        <div class="frame02">
            <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                                CellPadding="4" CellSpacing="1" 
                                Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                                onrowdatabound="gridViewCSList_RowDataBound" 
                                onpageindexchanging="gridViewCSList_PageIndexChanging" PageSize="25"  CssClass="GView_BodyCSS">
                    <Columns>
                         <asp:HyperLinkField HeaderText="订单ID" DataNavigateUrlFields="fog_order_num" DataNavigateUrlFormatString="LmSystemLogDetailPageByNew.aspx?ID={0}" 
                            Target="_blank" DataTextField="fog_order_num"><ItemStyle HorizontalAlign="Center" Width="12%" /></asp:HyperLinkField>
                         <asp:BoundField DataField="hotel_name" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center"  Width="20%"/></asp:BoundField>
                         <asp:BoundField DataField="price_code" HeaderText="价格代码" ><ItemStyle HorizontalAlign="Center"  Width="8%" /></asp:BoundField>
                         <asp:BoundField DataField="contact_tel" HeaderText="联系人电话" ><ItemStyle HorizontalAlign="Center" Width="10%" /></asp:BoundField>
                         <asp:BoundField DataField="DSCORE" HeaderText="分数"><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                         <asp:BoundField DataField="feedback" HeaderText="回复"><ItemStyle HorizontalAlign="Center" Width="20%"/></asp:BoundField>
                         <asp:BoundField DataField="BCREATETIME" HeaderText="创建时间"><ItemStyle HorizontalAlign="Center" Width="15%"/></asp:BoundField>
                         <asp:BoundField DataField="BOOKSTATUSONM" HeaderText="订单状态"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
              </asp:GridView>
        </div>

        <div id="background" class="pcbackground" style="display: none;">
        </div>
        <div id="progressBar" class="pcprogressBar" style="display: none;">
            数据加载中，请稍等...</div>
    </div>
    <asp:HiddenField ID="hidHotel" runat="server" />
    <asp:HiddenField ID="hidCity" runat="server" />
    <asp:HiddenField ID="hidCommonList" runat="server" />
    <asp:HiddenField ID="hidRowID" runat="server" />
    <asp:HiddenField ID="hidFormatString" runat="server" />
</asp:Content>
