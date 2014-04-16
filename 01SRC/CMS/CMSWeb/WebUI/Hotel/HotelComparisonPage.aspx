<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelComparisonPage.aspx.cs"  Title="酒店比价" Inherits="HotelComparisonPage" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
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

.Tb_BodyCSS tr
{
    height:30px;
}
.Tb_BodyCSS td
{
    height:30px;
    border-right:1px #d5d5d5 solid;
    border-top:1px #d5d5d5 solid;
}
        
.Tb_BodyCSS {
    border-collapse:collapse;
    border-spacing:0;
    border:1pxsolid#ccc;
}
</style>

<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {

        document.getElementById("wctHotel").value = "";
        document.getElementById("wctHotel").text = "";
        document.getElementById("wctCity").value = "";
        document.getElementById("wctCity").text = "";

    }

    function SetControlValue() {
        document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
        document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
        document.getElementById("<%=hidSales.ClientID%>").value = document.getElementById("wctSales").value;
    }

    function SerRbtDSValue(arg) {
        document.getElementById("<%=hidDSourceType.ClientID%>").value = arg;
    }

    function SerRbtDDValue(arg) {
        document.getElementById("<%=hidDSourceData.ClientID%>").value = arg;
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
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                 <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">查看订单操作历史</li>
        <li>
            <table style="line-height:30px">
                <tr>
                    <td align="right">
                        选择酒店：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="HotelComparisonPage.aspx?Type=hotel" />
                    </td>
                    <td align="right" style="width:150px">
                        选择城市：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" AutoParent="HotelComparisonPage.aspx?Type=city" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        酒店销售：
                    </td>
                    <td>
                         <uc1:WebAutoComplete ID="wctSales" CTLID="wctSales" runat="server" AutoType="sales" AutoParent="HotelComparisonPage.aspx?Type=sales" />
                    </td>
                    <td align="right">
                        比价基准：
                    </td>
                    <td>
                        <input type="radio" name="RbtDSourceType" id="rbtnElong" value="0" checked="checked" onclick="SerRbtDSValue('ELONG')"/>艺龙
                        <input type="radio" name="RbtDSourceType" id="rbtnCtrip" value="1" onclick="SerRbtDSValue('CTRIP')"/>携程
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        比价结果：
                    </td>
                    <td>
                        <input type="radio" name="RbtDSourceData" id="rbtnAll" value="0" checked="checked" onclick="SerRbtDDValue('')"/>全部结果
                        <input type="radio" name="RbtDSourceData" id="rbtnBL" value="1" onclick="SerRbtDDValue('1')"/>高价酒店
                        <input type="radio" name="RbtDSourceData" id="rbtnLL" value="2" onclick="SerRbtDDValue('2')"/>低价酒店
                    </td>
                    <td align="right">
                        折扣类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddpDiscount" runat="server" Width="150px"></asp:DropDownList>
                    </td>
                </tr>

                 <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="SetControlValue();BtnLoadStyle();" onclick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="导出Excel"  onclick="btnExport_Click"/>
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
                <ContentTemplate>
        <div class="frame02">
            <div style="font-weight:bold;margin:-3px 0px 0px 5px;"><br/>比价数据更新时间：<asp:Label ID="lbUpdatTime" runat="server" Text="" /></div>
            <div>
                <br/>
                <table class="Tb_BodyCSS" style="border:1px #d5d5d5 solid; padding:1px; margin:-3px 0px 0px 5px;width:80%;height:100%">
                    <tr style="background-color:#E9E9E9;font-weight:bold;">
                        <td style="width:13%"></td>
                        <td align="center" style="width:25%">HVP价格高于签约价</td>
                        <td align="center" style="width:25%">HVP价格低于签约价</td>
                        <td align="center" style="width:25%">HVP价格等于签约价</td>
                    </tr>
                    <tr style="background-color:#E9E9E9;">
                        <td align="center" style="font-weight:bold;">酒店数</td>
                        <td align="center"><asp:Label ID="lbBHLID" runat="server" Text="" /></td>
                        <td align="center"><asp:Label ID="lbLHLID" runat="server" Text="" /></td>
                        <td align="center"><asp:Label ID="lbDHLID" runat="server" Text="" /></td>
                    </tr>
                    <tr style="background-color:#E9E9E9;">
                        <td align="center" style="font-weight:bold;">房间数</td>
                        <td align="center"><asp:Label ID="lbBRMCD" runat="server" Text="" /></td>
                        <td align="center"><asp:Label ID="lbLRMCD" runat="server" Text="" /></td>
                        <td align="center"><asp:Label ID="lbDRMCD" runat="server" Text="" /></td>
                    </tr>
                </table>
            </div>
             <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
                    <asp:GridView ID="gridViewCSReviewLmSystemLogList" onrowdatabound="gridViewCSReviewLmSystemLogList_RowDataBound"  runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" AllowSorting="true" PageSize="20"  CssClass="GView_BodyCSS">
                    <Columns>
                       
                         <asp:BoundField DataField="Hotel_ID" HeaderText="酒店ID" ><ItemStyle HorizontalAlign="Center" Width="6%"/></asp:BoundField>
                         <asp:BoundField DataField="Hotel_Name" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" Width="20%"/></asp:BoundField>
                         <asp:BoundField DataField="Room_Name" HeaderText="房型名称" ><ItemStyle HorizontalAlign="Center" Width="12%"/></asp:BoundField>
                         <asp:BoundField DataField="DTypeNM" HeaderText="合同折扣" ><ItemStyle HorizontalAlign="Center" Width="6%"/></asp:BoundField>
                         <asp:BoundField DataField="DValue" HeaderText="折扣" ><ItemStyle HorizontalAlign="Center" Width="6%" /></asp:BoundField>
                         <asp:BoundField DataField="Two_Price" HeaderText="今日LM价格" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                         <asp:BoundField DataField="Mapping_Price" HeaderText="今日艺龙价格" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                         <asp:BoundField DataField="Act_Price" HeaderText="今日艺龙价格*折扣" ><ItemStyle HorizontalAlign="Center" Width="12%"/></asp:BoundField>
                         <asp:BoundField DataField="ActCut" HeaderText="实际LM折扣" ><ItemStyle HorizontalAlign="Center" Width="8%" /></asp:BoundField>
                         <asp:BoundField DataField="User_DspName" HeaderText="酒店销售" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
         <div style="margin-left:10px;">
            <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb"  ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%" ShowPageIndexBox="always" AlwaysShow="true" width="100%" LayoutType="Table" onpagechanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
         </div>
        </div>
        <div id="background" class="pcbackground" style="display: none; "></div> 
        <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
     </ContentTemplate>
    </asp:UpdatePanel>
</div>
<asp:HiddenField ID="hidHotel" runat="server"/>
<asp:HiddenField ID="hidCity" runat="server"/>
<asp:HiddenField ID="hidSales" runat="server"/>

<asp:HiddenField ID="hidDSourceType" runat="server"/>
<asp:HiddenField ID="hidDSourceData" runat="server"/>
</asp:Content>