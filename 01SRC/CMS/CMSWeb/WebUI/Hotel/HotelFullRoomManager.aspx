<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelFullRoomManager.aspx.cs"  Title="酒店管理" Inherits="HotelFullRoomManager" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    function BtnSelectHotel() {
        document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
    }

    function ClearClickEvent() {
        document.getElementById("wctHotel").value = "";
        document.getElementById("wctHotel").text = "";
        document.getElementById("<%=dpKeepStart.ClientID%>").value = "";
        document.getElementById("<%=dpKeepEnd.ClientID%>").value = "";
        document.getElementById("<%=ddpPriceType.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=hidHotelID.ClientID%>").value = "";
        document.getElementById("<%=dvRoomList.ClientID%>").style.display = 'none';
        document.getElementById("<%=dvHistory.ClientID%>").style.display = 'none';
        document.getElementById("<%=dvAutoComplete.ClientID%>").style.display = '';
        document.getElementById("<%=dvlbHotel.ClientID%>").style.display = 'none';
        document.getElementById("<%=dvSelectHotel.ClientID%>").style.display = ''; 
        GoDel();
    }

    function SetHotelControlVal(arg) {
        document.getElementById("wctHotel").value = arg;
        document.getElementById("wctHotel").text = arg;
    }

//    function DtControlStyle(arg) {
//        document.getElementById("<%=dpKeepStart.ClientID%>").disabled = arg;
//        document.getElementById("<%=dpKeepEnd.ClientID%>").disabled = arg;
//    }

    function GoDel() {
        var tb = document.getElementById("<%=chkHotelRoomList.ClientID%>");

        if (tb == null) {
            return;
        }

        for (var i = tb.rows.length - 1; i >= 0; i--) {
            tb.deleteRow(i);
        }
    }

    function SetContronListVal() {
        document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
        var commidList = "";
        if (document.getElementById("<%=chkHotelRoomList.ClientID%>") != null) {
            var objCheck = document.getElementById("<%=chkHotelRoomList.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < objCheck.length; i++) {
                if (document.getElementById("<%=chkHotelRoomList.ClientID%>" + "_" + i).checked) {

                    commidList = commidList + document.getElementById("<%=chkHotelRoomList.ClientID%>" + "_" + i).value + ',';
                }
            }
            document.getElementById("<%=hidCommonList.ClientID%>").value = commidList;
        }
    }
</script>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">标记满房信息</li>
        <li>
            <table style="line-height:25px;">
               
                 <tr>
                    <td align="right">
                        <font color="red">*</font>选择酒店：
                    </td>
                    <td>
                        
                        <div runat="server" id="dvAutoComplete">
                         <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="HotelFullRoomManager.aspx?Type=hotel" />
                         </div>
                         <div runat="server" id="dvlbHotel" style="display:none">
                         <asp:Label runat="server" ID="lbHotel" />
                         </div>
                         
                    </td>
                </tr>
                
                <tr>
                    <td align="right">
                        <font color="red">*</font>满房持续日期：
                    </td>
                    <td  colspan="2">
                        <div runat="server" id="dvLbTime" style="display:none">
                            <asp:Label runat="server" ID="lbStart" /> -- <asp:Label runat="server" ID="lbEnd" />
                        </div>
                        <div style="display:none"><asp:TextBox ID="txtYestoday" runat="server"/></div>
                        <div runat="server" id="dvKeepTime">
                        <input id="dpKeepStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_txtYestoday\')}',maxDate:'#F{$dp.$D(\'MainContent_dpKeepEnd\')||\'2020-10-01\'}'})" runat="server"/>
                        <input id="dpKeepEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpKeepStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                     <td>
                        <table>
                            <tr>
                                <td>
                                    <div runat="server" id="dvSelectHotel">
                                    <asp:Button ID="btnSelectHotel" runat="server" CssClass="btn primary" Text="搜索房型" OnClientClick="BtnSelectHotel()" onclick="btnSelectHotel_Click"/>
                                    </div>
                                </td>
                                <td><asp:Button ID="btnClear" runat="server" CssClass="btn" Text="重置" OnClientClick="ClearClickEvent()" onclick="btnRest_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <div id="messageContent" runat="server" style="color:red;"></div>
                    </td>
                </tr>
            
            </table>
        </li>
      </ul>
    </div>
   
     <div class="frame01" runat="server" id="dvRoomList" style="display:none">
      <ul>
        <li class="title">房型信息</li>
        <li>
            <table style="line-height:25px;">
                  <tr>
                    <td colspan="2">
                        <%--<asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>--%>
                        <table>
                            <tr>
                                <td align="right"><font color="red">*</font>价格代码类型：</td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddpPriceType" runat="server" CssClass="noborder_inactive" Width="120px"></asp:DropDownList>
                                </td>
                                </tr>
                            <tr>
                                <td align="right">
                                    <font color="red">*</font>选择房型：
                                </td>
                                <td  colspan="2">
                                    <asp:CheckBoxList ID="chkHotelRoomList" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                                </td>
                            </tr>
                        </table>
                       <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                      </td>
                 </tr>
            </table>
        </li>
        <li class="button">
<%--             <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server" >
                <ContentTemplate>--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAddPromotion" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetContronListVal()" onclick="btnSave_Click" />
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </li>
      </ul>
    </div>


    <div class="frame01" runat="server" id="dvHistory" style="display:none">
            <ul>
            <li class="title">计划维护历史信息</li>
            </ul>
    </div>
    <div class="frame02">
        <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" AllowPaging="true" PageSize="20"
                            Width="100%" EmptyDataText="" DataKeyNames="EVENT" 
                            onrowdatabound="gridViewCSList_RowDataBound" 
                            onpageindexchanging="gridViewCSList_PageIndexChanging"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="EVENT" HeaderText="操作类型" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="EVENTTM" HeaderText="操作日期" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="EFFECTDATE" HeaderText="计划日期" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="ROOMNM" HeaderText="房型名称" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="PRICECODE" HeaderText="价格代码" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="STATUSDIS" HeaderText="状态" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="GUAID" HeaderText="取消金" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="CXLID" HeaderText="保证金" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="ROOMNUM" HeaderText="房量" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="ISRESERVE" HeaderText="保留房" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="HOLDROOMNUM" HeaderText="Hold房量" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="TWOPRICE" HeaderText="价格" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="BREAKFASTNUM" HeaderText="早餐" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="ISNETWORK" HeaderText="宽带" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="OFFSETVAL" HeaderText="浮动值" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                    <asp:BoundField DataField="OFFSETUNIT" HeaderText="浮动类型" ><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
   </div>
<asp:HiddenField ID="hidHotelID" runat="server"/>
<asp:HiddenField ID="hidKeepStart" runat="server"/>
<asp:HiddenField ID="hidKeepEnd" runat="server"/>
<asp:HiddenField ID="hidCommonList" runat="server"/>
</asp:Content>