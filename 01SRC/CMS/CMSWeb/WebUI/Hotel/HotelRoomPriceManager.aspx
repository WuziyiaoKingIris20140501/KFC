<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelRoomPriceManager.aspx.cs"  Title="酒店管理" Inherits="HotelRoomPriceManager" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    function BtnSelectHotel() {
        document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
    }

    function ClearClickEvent() {
        document.getElementById("wctHotel").value = "";
        document.getElementById("wctHotel").text = "";
        document.getElementById("<%=hidHotelID.ClientID%>").value = "";
    }

    function GoDel() {
        var tb = document.getElementById("<%=chkHotelRoomList.ClientID%>");

        if (tb == null) {
            return;
        }

        for (var i = tb.rows.length - 1; i >= 0; i--) {
            tb.deleteRow(i);
        }
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
    function SetContronListVal() {
        document.getElementById("<%=messageContent.ClientID%>").innerText = "";
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
    }

    function SetWeekListVal(arg) {
        if (document.getElementById("<%=chkWeekList.ClientID%>") != null) {
            var objCheck = document.getElementById("<%=chkWeekList.ClientID%>").getElementsByTagName("input");
            for (var i = 0; i < objCheck.length; i++) {
                if (arg == "1") {
                    document.getElementById("<%=chkWeekList.ClientID%>" + "_" + i).checked = true;
                }
                else {
                    document.getElementById("<%=chkWeekList.ClientID%>" + "_" + i).checked = false;
                }
            }
        }
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
     <div class="frame01">
      <ul>
        <li class="title">选择酒店房型</li>
        <li>
             <table style="line-height:25px;">
                 <tr>
                    <td align="right">
                        <font color="red">*</font>选择酒店：
                    </td>
                    <td>
                         <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="HotelRoomPriceManager.aspx?Type=hotel" />
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                     <asp:Button ID="btnSelectHotel" runat="server" CssClass="btn primary" Text="选择" OnClientClick="BtnSelectHotel()" onclick="btnSelectHotel_Click"/>
                                     </ContentTemplate>
                                     </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right"><font color="red">*</font>价格代码类型：</td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                    <asp:DropDownList ID="ddpPriceType" runat="server" CssClass="noborder_inactive" 
                                        Width="120px" onselectedindexchanged="ddpPriceType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    &nbsp;&nbsp;<font color="red">*</font>货币：CNY-人民币
                                    </ContentTemplate>
                                     </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                 </tr>
             </table>
             <table>
                <tr>
                    <td align="right" style="width:92px">
                        选择房型：
                    </td>
                    <td  colspan="2">
                         <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                        <asp:CheckBoxList ID="chkHotelRoomList" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                        </ContentTemplate>
                         </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </li>
        <li></li>
     </ul>
     </div>
     <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
     <ContentTemplate>
     <div class="frame01">
      <ul>
        <li class="title">保证金取消金设置</li>
        <li>
            <div id="dvlm" runat="server">
                <table>
                    <tr>
                        <td align="right" style="width:92px">
                            保证金制度：
                        </td>
                        <td align="left">
                            <asp:Label ID="lbNote1" runat="server" Text="【PP】需要预付100%总房费。" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            取消制度：
                        </td>
                        <td align="left">
                            <asp:Label ID="lbNote11" runat="server" Text="【PT100】如欲取消，需支付全额房费。" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvlm2" runat="server" style="display:none">
                <table>
                    <tr>
                        <td align="right" style="width:92px">
                            保证金制度：
                        </td>
                        <td align="left">
                            <asp:Label ID="lbNote2" runat="server" Text="【RH04】订单保留至凌晨4点。" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            取消制度：
                        </td>
                        <td align="left">
                            <asp:Label ID="lbNote22" runat="server" Text="【NP24】取消无罚金。" />
                        </td>
                    </tr>
                </table>
            </div>
        </li>
        <li></li>
     </ul>
     </div>
     </ContentTemplate>
     </asp:UpdatePanel>
     <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
     <ContentTemplate>
     <div class="frame01">
      <ul>
        <li class="title">设置日期</li>
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
            </tr>
            </table>
            <table> 
                <tr>
                    <td style="width:22px"></td>
                    <td>
                        <asp:CheckBoxList ID="chkWeekList" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                    </td>
                    <td>
                        <input type="button" id="btnAll" class="btn primary" value="全选"  onclick="SetWeekListVal('1')" />
                        <input type="button" id="btnUnAll" class="btn" value="反选"  onclick="SetWeekListVal('0')" />
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
                    <td>单人价</td>
                    <td><font color="red">*</font>二人价</td>
                    <td>三人价</td>
                    <td>四人价</td>
                    <td>加床价</td>
                    <td>早餐数量</td>
                    <td>每份早餐价格</td>
                    <td>免费宽带</td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:TextBox ID="txtOnePrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:TextBox ID="txtTwoPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:TextBox ID="txtThreePrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:TextBox ID="txtFourPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:TextBox ID="txtBedPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:DropDownList ID="ddpBreakfastNum" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtBreakPrice" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:DropDownList ID="ddpIsNetwork" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>浮动设置</td>
                    <td></td>
                    <td></td>
                    <td><font color="red">*</font>状态</td>
                    <td><span id="roomNumTr">&nbsp;房量设置</span></td>
                    <td></td>
                    <td>&nbsp;是否保留房</td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="width:22px"></td>
                    <td><asp:TextBox ID="txtOffsetval" runat="server" Width="70px" MaxLength="6"/></td>
                    <td><asp:DropDownList ID="ddpOffsetunit" runat="server" Width="77px"></asp:DropDownList></td>
                    <td style="width:77px"></td>
                    <td>
                       <asp:DropDownList ID="ddpRoomStatus" Width="77px" runat="server" onchange="checkRoomStatus(this.value);"></asp:DropDownList>
                    </td>
                    <td align="right"><span id="roomNumTd">
                        <asp:TextBox ID="txtRoomCount" runat="server" Width="70px" MaxLength="3"/>&nbsp;间</span>
                    </td>
                    <td style="width:50px"></td>
                    <td><asp:DropDownList ID="ddpIsReserve" Width="77px" runat="server"></asp:DropDownList></td>
                </tr>
            </table>
        </li>
        <li></li>
     </ul>
     </div>
          </ContentTemplate>
     </asp:UpdatePanel>
     <div class="frame01">
      <ul>
        <li>
             <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
                            <ContentTemplate>
             <div id="messageContent" runat="server" style="color:red;"></div>
             </ContentTemplate>
                </asp:UpdatePanel>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAddPromotion" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetContronListVal()" onclick="btnSave_Click" />
            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="重置" OnClientClick="ClearClickEvent();" onclick="btnRest_Click" />
            </ContentTemplate>
                </asp:UpdatePanel>
        </li>
        <li></li>
      </ul>
    </div>

   </div>
<asp:HiddenField ID="hidHotelID" runat="server"/>
<asp:HiddenField ID="hidCommonList" runat="server"/>
<asp:HiddenField ID="hidWeekList" runat="server"/>
</asp:Content>