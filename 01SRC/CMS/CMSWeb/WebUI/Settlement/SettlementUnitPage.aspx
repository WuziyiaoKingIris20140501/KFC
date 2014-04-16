<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="SettlementUnitPage.aspx.cs"  Title="结算单位管理" Inherits="SettlementUnitPage" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete" TagPrefix="uc1" %>
<%@ Register src="../../UserControls/AutoCptControl.ascx" tagname="WebAutoComplete" tagprefix="ac1" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script language="javascript" type="text/javascript">
    function BtnLoadStyle() {
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
        ajaxbg.show();
    }

    function BtnCompleteStyle() {
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
    }

    function SetAClickEvent() {
        $("#MainContent_AspNetPager2 table tbody tr td a[disabled!='disabled']").click(function () { BtnLoadStyle(); });
        $("#MainContent_AspNetPager2 table tbody tr td input[type=submit]").click(function () { BtnLoadStyle(); });

        $("#MainContent_AspNetPager1 table tbody tr td a[disabled!='disabled']").click(function () { BtnLoadStyle(); });
        $("#MainContent_AspNetPager1 table tbody tr td input[type=submit]").click(function () { BtnLoadStyle(); });

        document.getElementById("wctCity").value = document.getElementById("<%=hidCity.ClientID%>").value;
        document.getElementById("wctCity").text = document.getElementById("<%=hidCity.ClientID%>").value;
    }

    function AddUnitEvent(arg, id) {
        document.getElementById("<%=hidActionType.ClientID%>").value = arg;
        document.getElementById("<%=hidUnitID.ClientID%>").value = id;
        document.getElementById("<%=btnReLoad.ClientID%>").click();
    }

    function SetControlValue() {
        document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
    }

    function SetSalesVal() {
        document.getElementById("<%=hidSales.ClientID%>").value = document.getElementById("wctSales").value;
    }

    function invokeOpenList() {
        document.getElementById("popupDiv2").style.display = "block";
        //背景
        var bgObj = document.getElementById("bgDiv2");
        bgObj.style.display = "block";

        document.getElementById("wctSales").value = document.getElementById("<%=hidSales.ClientID%>").value;
        document.getElementById("wctSales").text = document.getElementById("<%=hidSales.ClientID%>").value;
    }

    //隐藏弹出的层
    function invokeCloseList() {
        document.getElementById("popupDiv2").style.display = "none";
        document.getElementById("bgDiv2").style.display = "none";
    }

    function SaveSeltUnit() {
        
    }

    function AddHotelList() {
        document.getElementById("dvAddHotel").style.display = "block";
        //背景
        var bgObj = document.getElementById("dvDg");
        bgObj.style.display = "block";
    }

    function OpenHotelList() {
        document.getElementById("<%=hidSelectHotel.ClientID%>").value = "";
        AddHotelList();
    } 

    function DelePopupArea(Hid, Hnm) {

        if (!confirm("您确认将[" + Hid + "]" + Hnm + "从该结算单位中删除吗?"))
        {
            return;
        }

        document.getElementById("<%=hidDelHotelID.ClientID%>").value = Hid;
        document.getElementById("<%=btnDeletePopupArea.ClientID%>").click();
    }

    function AddHotel() {
        var hotelID = document.getElementById("wctHotel").value

//        if (!(hotelID.indexOf("[") !=-1) || !(hotelID.indexOf("]") !=-1))
//        {
//            document.getElementById("<%=detailMessageContent.ClientID%>").innerHTML = "选择的酒店不合法，请从新选择!";
//        }

        document.getElementById("<%=hidSelectHotel.ClientID%>").value = hotelID;
        SetSalesVal();
    }

    function invokeHotelCloseList() {
        document.getElementById("dvAddHotel").style.display = "none";
        document.getElementById("dvDg").style.display = "none";
    }

    function BtnHotelErrStyle() {
        invokeOpenList();
        AddHotelList();

        document.getElementById("wctHotel").value = document.getElementById("<%=hidSelectHotel.ClientID%>").value;
        document.getElementById("wctHotel").text = document.getElementById("<%=hidSelectHotel.ClientID%>").value; 
        
    }
</script>

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
z-index: 20000; 
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
z-index: 20001; 
}
.bgDiv2
{
    opacity: 0.5;
    filter: alpha(opacity=50);
    background: #666666;
    z-index: 100;
    display:none;
    bottom: 0;
    left: 0;
    position: fixed;
    right: 0;
    top: 0;
}
.popupDiv2
{
    width: 670px;
    height: 700px;

    position: absolute;
    padding: 1px;
    z-index: 10000;
    display: none;
    background-color: White;
    
    
    top: 20%;
    left: 35%;

    margin-left:-150px!important;/*FF IE7 该值为本身宽的一半 */
    margin-top:-50px!important;/*FF IE7 该值为本身高的一半*/
    margin-left:0px;
    margin-top:0px;

    position:fixed!important;/*FF IE7*/
    position:absolute;/*IE6*/

    _top:       expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
                document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/
    _left:      expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 :/*IE6*/
                document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2);/*IE5 IE5.5*/

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
    padding-left:10px;
}
        
.Tb_BodyCSS {
    border-collapse:collapse;
    border-spacing:0;
    border:1pxsolid#ccc;
}


.dvDg
{
    opacity: 0.5;
    filter: alpha(opacity=50);
    background: #666666;
    z-index: 10010;
    display:none;
    bottom: 0;
    left: 0;
    position: fixed;
    right: 0;
    top: 0;
}
.dvAddHotel
{
    width: 500px;
    height: 190px;

    position: absolute;
    padding: 1px;
    z-index: 10011;
    display: none;
    background-color: White;
    
    
    top: 35%;
    left: 40%;

    margin-left:-150px!important;/*FF IE7 该值为本身宽的一半 */
    margin-top:-50px!important;/*FF IE7 该值为本身高的一半*/
    margin-left:0px;
    margin-top:0px;

    position:fixed!important;/*FF IE7*/
    position:absolute;/*IE6*/

    _top:       expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
                document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/
    _left:      expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 :/*IE6*/
                document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2);/*IE5 IE5.5*/

}
</style>

<%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
  <div id="right">
<%--    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
      <ContentTemplate>--%>
    <div class="frame01">
      <ul>
        <li class="title">查询结算单位</li>
        <li>
            <table style="width:100%;line-height:35px">
                <tr>
                    <td style="width:7%" align="right">名称：</td>
                    <td style="width:10%" align="left">
                        <input type="text" id="txtUnitName" runat="server" style="width:200px;" maxlength="50" value=""/>
                    </td>
                    <td style="width:8%" align="right">城市：</td>
                    <td style="width:10%" align="left">
                        <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" AutoParent="SettlementUnitPage.aspx?Type=city" />
                    </td>
                    <td style="width:10%" align="right">发票抬头：</td>
                    <td style="width:40%" align="left">
                        <input type="text" id="txtInvoiceName" runat="server" style="width:200px;" maxlength="50" value=""/>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" colspan="6">
                        <div>
                            <%--<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>--%>
                            <div style="margin-left:18px;float:left">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Width="110px" OnClientClick="BtnLoadStyle();SetControlValue()" onclick="btnSearch_Click" Text="查询"/>
                                &nbsp;&nbsp;&nbsp;
                                <input type="button" id="btnAdd" class="btn primary" style="width:110px" value="添加结算单位"  onclick="AddUnitEvent('1', '');"/>
                            </div>
                            <div id="MessageContent" runat="server" style="color: red;float:left;margin-left:18px"></div>
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </td>
                </tr>
            </table>
        </li>
      </ul>
    </div>
<%--    </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
                <ContentTemplate>--%>
    <div class="frame02">
         <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
         <asp:GridView ID="gridViewCSReviewUserList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="结算单位名称">
                      <ItemTemplate >
                        <a href="###" onclick="BtnLoadStyle();AddUnitEvent('0', '<%# DataBinder.Eval(Container.DataItem, "UNITID") %>')"><%# DataBinder.Eval(Container.DataItem, "UNITNAME")%></a>
                      </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="HOTELNM" HeaderText="包含酒店" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="INVOICENM" HeaderText="发票抬头" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="PER" HeaderText="联系人" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="TEL" HeaderText="联系电话" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
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

    <asp:HiddenField ID="hidCity" runat="server" />
    <asp:HiddenField ID="hidActionType" runat="server" />
    <asp:HiddenField ID="hidSelectHotel" runat="server" />
    <asp:HiddenField ID="hidDelHotelID" runat="server" />
    <asp:HiddenField ID="hidUnitID" runat="server" />

    <asp:HiddenField ID="hidSales" runat="server" />

    <div style="display:none">
        <asp:Button ID="btnDeletePopupArea" runat="server" CssClass="btn primary" Width="60px" onclick="btnDeletePopupArea_Click" Text="删除"/>
        <asp:Button ID="btnReLoad" runat="server" CssClass="btn primary" Width="60px" onclick="btnReLoad_Click" Text="ReLoad"/>
    </div>

    <div id="background" class="pcbackground" style="display: none; "></div>
    <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div>

    <div id="bgDiv2" class="bgDiv2"></div>
    <div id="popupDiv2" class="popupDiv2">
        <div style="width:99%;height:99%;margin:0,0,0,0">
        <table width="100%">
        <tr>
            <td>
                <table style="border:0px; padding:0px;margin-left:3px;width:100.6%">
                    <tr align="left">
                        <td align="left" valign="middle" style="background-color:#6379B2; height:30px; padding:5px 5px 3px 10px;font-weight:bold;color:White;">订单详情</td>
                    </tr>
                </table>
                <table style="border:1px #d5d5d5 solid; padding:1px;margin:-2px 0px 0px 5px;width:100%;height:100%;line-height:30px;">
                    <tr>
                        <td align="right" width="16%">
                            结算单位名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_UnitNm" runat="server" Width="185px" MaxLength="50"/>
                        </td>
                        <td align="right">
                            结算联系人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_Per" runat="server" Width="185px" MaxLength="20"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            开票抬头：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_InvoiceNm" runat="server" Width="185px" MaxLength="50"/>
                        </td>
                        <td align="right">
                            结算联系电话：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_Tel" runat="server" Width="185px" MaxLength="30"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            结算账期：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddpterm" CssClass="noborder_inactive" runat="server" Width="195px"/>
                        </td>
                        <td align="right">
                            结算联系传真：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_fax" runat="server" Width="185px" MaxLength="30"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            账期起止日：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddptermStDt" CssClass="noborder_inactive" runat="server" Width="195px"/>
                        </td>
                        
                        <td align="right">
                            酒店负责销售：
                        </td>
                        <td align="left" style="display:none">
                            <asp:TextBox ID="txt_sales" runat="server" Width="185px" MaxLength="30"/>
                        </td>
                        <td align="left">
                            <ac1:WebAutoComplete ID="wctSales" runat="server" CTLID="wctSales" CTLLEN="185px" AutoType="sales" AutoParent="SettlementUnitPage.aspx?Type=sales" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            发票项目：
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txt_billItem" runat="server" Width="185px" MaxLength="100"/>
                        </td>
                        
                        <td align="right">
                            在线状态：
                        </td>
                        <td align="left">
                             <asp:RadioButton ID="rdbOnL" GroupName="rdbOnline" runat="server" Checked="true" Text="上线"/>
                             <asp:RadioButton ID="rdbOff" GroupName="rdbOnline" runat="server" Text="下线"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            酒店税点：
                        </td>
                        <td align="left">
                             <asp:TextBox ID="txt_tax" runat="server" Width="50px" MaxLength="6"/>%
                        </td>

                        
                        <td align="right">
                            付款账号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_payno" runat="server" Width="185px" MaxLength="30"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            酒店税号：
                        </td>
                        <td align="left" colspan="3">
                           <asp:TextBox ID="txt_taxno" runat="server" Width="185px" MaxLength="30"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            结算地址：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txt_address" runat="server" Width="480px" MaxLength="100"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <div>
                                <div style="float:left;margin-left:20px">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Width="80px" OnClientClick="BtnLoadStyle();SetSalesVal()" onclick="btnSave_Click" Text="确认"/>
                                    <input type="button" class="btn" id="btnCancel" style="width:80px" value='取消' onclick="invokeCloseList()"/>
                                </div>
                                <div id="dvUnitMessageContent" runat="server" style="color: red;float:left;margin-left:10px"></div>
                                <div style="float:right;margin-right:30px">
                                    <input type="button" class="btn" id="btnAddHotel" value='在本单位下添加酒店' onclick="OpenHotelList()"/>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5">
                        <div style="width: 100%; height: 330px; overflow-y:auto;" id="dvHotelList">
                        <asp:GridView ID="gridViewHotelList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" PageSize="20"  CssClass="GView_BodyCSS">
                            <Columns>
                                 <asp:BoundField DataField="HOTELID" HeaderText="酒店ID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                 <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                 <asp:BoundField DataField="ODTYPE" HeaderText="订单类型" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="操作">
                                    <ItemTemplate >
                                    <a href="###" onclick="DelePopupArea('<%# DataBinder.Eval(Container.DataItem, "HOTELID") %>','<%# DataBinder.Eval(Container.DataItem, "HOTELNM") %>')">删除</a>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                            <PagerStyle HorizontalAlign="Right" />
                            <RowStyle CssClass="GView_ItemCSS" />
                            <HeaderStyle CssClass="GView_HeaderCSS" />
                            <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                        </asp:GridView>
                        </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
        </div>
    </div>

    <div id="dvDg" class="dvDg"></div>
    <div id="dvAddHotel" class="dvAddHotel">
        <div style="width:99%;height:99%;margin:0,0,0,0">
        <table width="100%">
        <tr>
            <td>
                <table style="border:0px; padding:0px;margin-left:3px;width:100.6%">
                    <tr align="left">
                        <td align="left" valign="middle" style="background-color:#6379B2; height:30px; padding:5px 5px 3px 10px;font-weight:bold;color:White;">添加酒店</td>
                    </tr>
                </table>
                <table style="border:1px #d5d5d5 solid; padding:1px;margin:-2px 0px 0px 5px;width:100%;height:100%;line-height:30px;">
                    <tr>
                        <td align="right" width="16%">
                            选择酒店：
                        </td>
                        <td align="left">
                            <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="SettlementUnitPage.aspx?Type=hotel" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            订单类型：
                        </td>
                        <td align="left">
                            <input type="checkbox" id="chkLMBAR" runat="server" />预付
                            <input type="checkbox" id="chkLMBAR2" runat="server" />现付
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            酒店供应商：
                        </td>
                        <td align="left">
                            <input type="checkbox" id="chkALL" runat="server" />所有
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <div>
                                <div style="float:left">
                                    <asp:Button ID="btnHotelADD" runat="server" CssClass="btn primary" Width="60px" OnClientClick="BtnLoadStyle();AddHotel()" onclick="btnHotelADD_Click" Text="添加"/>
                                    <input type="button" class="btn" id="btnHotelClose" style="width:60px" value='取消' onclick="invokeHotelCloseList()"/>
                                </div>
                                <div id="detailMessageContent" runat="server" style="color: red;float:left;margin-left:10px"></div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
        </div>
    </div>

<%--    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="AspNetPager2" />
    </Triggers>
    </asp:UpdatePanel>--%>

  </div>
</asp:Content>