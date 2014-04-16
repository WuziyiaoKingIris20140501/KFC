<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ReviewUserPage.aspx.cs"  Title="用户查询" Inherits="ReviewUserPage" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtTelphone.ClientID%>").value = "";
     
        document.getElementById("<%=ddpRegChannelList.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpPlatformList.ClientID%>").selectedIndex = 0;

//        document.getElementById("<%=chkCreateUnTime.ClientID%>").checked = true;
        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
//        document.getElementById("<%=dpCreateStart.ClientID%>").disabled = true;
        //        document.getElementById("<%=dpCreateEnd.ClientID%>").disabled = true; 

        document.getElementById("<%=dpLoginStart.ClientID%>").value = "";
        document.getElementById("<%=dpLoginEnd.ClientID%>").value = "";

        document.getElementById("<%=dpLoginSizeStart.ClientID%>").value = "";
        document.getElementById("<%=dpLoginSizeEnd.ClientID%>").value = "";

        document.getElementById("rbtOrdAll").checked = true;
        document.getElementById("rbtSucOrdAll").checked = true;
        document.getElementById("<%=dvSucOrd.ClientID%>").style.display = 'none';
        document.getElementById("<%=dvOrd.ClientID%>").style.display = 'none';

        document.getElementById("<%=hidOrdType.ClientID%>").value = "0";
        document.getElementById("<%=hidSucOrdType.ClientID%>").value = "0";
        document.getElementById("<%=messageContent.ClientID%>").innerHTML = "";
    }

    function SetchkCreateUnTime() {
//        if (document.getElementById("<%=chkCreateUnTime.ClientID%>").checked == true) {
//            document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
//            document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";

//            document.getElementById("<%=dpCreateStart.ClientID%>").disabled = true;
//            document.getElementById("<%=dpCreateEnd.ClientID%>").disabled = true;
//        }
//        else {

//            document.getElementById("<%=dpCreateStart.ClientID%>").disabled = false;
//            document.getElementById("<%=dpCreateEnd.ClientID%>").disabled = false;
//        }
    }

    function SetchkLoginUnTime() {
    }

    function SetControlEnable() {
//        document.getElementById("<%=chkCreateUnTime.ClientID%>").checked = true;
        document.getElementById("<%=dpCreateStart.ClientID%>").value = "";
        document.getElementById("<%=dpCreateEnd.ClientID%>").value = "";
        document.getElementById("<%=dpLoginStart.ClientID%>").value = "";
        document.getElementById("<%=dpLoginEnd.ClientID%>").value = "";
        document.getElementById("<%=dpLoginSizeStart.ClientID%>").value = "";
        document.getElementById("<%=dpLoginSizeEnd.ClientID%>").value = "";

//        document.getElementById("<%=dpCreateStart.ClientID%>").disabled = true;
//        document.getElementById("<%=dpCreateEnd.ClientID%>").disabled = true; 
    }

    function PopupArea(arg) {
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
        var retunValue = window.open("UserGroupDetailPage.aspx?ID=" + arg + "&time=" + time, null, fulls);
        //window.location.href = "UserDetailPage.aspx?ID=" + arg + "&time=" + time;
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

    function SerRbtOrdValue(arg) {
        if (arg == "1") {
            document.getElementById("<%=dvOrd.ClientID%>").style.display = 'block'; 
        }
        else {
            document.getElementById("<%=dvOrd.ClientID%>").style.display = 'none';
        }
        document.getElementById("<%=hidOrdType.ClientID%>").value = arg;
    }

    function SerRbtSucOrdValue(arg) {
        if (arg == "1") {
            document.getElementById("<%=dvSucOrd.ClientID%>").style.display = 'block'; 
        }
        else {
            document.getElementById("<%=dvSucOrd.ClientID%>").style.display = 'none';
        }
        document.getElementById("<%=hidSucOrdType.ClientID%>").value = arg;
    }

    function SetAClickEvent() {
        $("#MainContent_AspNetPager2 table tbody tr td a[disabled!='disabled']").click(function () { BtnLoadStyle(); });
        $("#MainContent_AspNetPager2 table tbody tr td input[type=submit]").click(function () { BtnLoadStyle(); });

        $("#MainContent_AspNetPager1 table tbody tr td a[disabled!='disabled']").click(function () { BtnLoadStyle(); });
        $("#MainContent_AspNetPager1 table tbody tr td input[type=submit]").click(function () { BtnLoadStyle(); });
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
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div id="right">
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
      <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">用户查询</li>
        <li>
            <table>
                <tr>
                    <td>手机号码：</td>
                    <td>
                        <input name="textfield" type="text" id="txtTelphone" runat="server" style="width:320px;" maxlength="200" value=""/>
                    </td>
                    <td style="text-align:right">历史登录日期：</td>
                    <td style="width:400px"> 
                        <input id="dpLoginSizeStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpLoginSizeEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpLoginSizeEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpLoginSizeStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">注册日期：</td>
                    <td style="width:400px"> 
                        <input id="dpCreateStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpCreateEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpCreateEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpCreateStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                        <input type="checkbox" name="checkbox" id="chkCreateUnTime" visible="false" runat="server" onclick="SetchkCreateUnTime()"/>
                    </td>
                    <td style="text-align:right">最后登录日期：</td>
                    <td style="width:400px"> 
                        <input id="dpLoginStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpLoginEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpLoginEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpLoginStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                        <input type="checkbox" name="checkbox" id="chkLoginUnTime" visible="false" runat="server" onclick="SetchkLoginUnTime()"/>
                    </td>
                </tr>
                <tr>
                    <td>注册渠道：</td>
                    <td><asp:DropDownList ID="ddpRegChannelList" CssClass="noborder_inactive" runat="server" Width="155px"></asp:DropDownList></td>
                    <td style="text-align:right">注册平台：</td>
                    <td><asp:DropDownList ID="ddpPlatformList" CssClass="noborder_inactive" runat="server" Width="155px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>有否订单：</td>
                    <td style="width: 350px">
                        <table style="width: 120%">
                            <tr>
                                <td align="left" style="width:45%">
                                    <%--<asp:RadioButtonList ID="rdbtnlist" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="">不限制</asp:ListItem>
                                        <asp:ListItem Value="999999">有订单</asp:ListItem>
                                        <asp:ListItem Value="0">无订单</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                    <input type="radio" name="RbtOrdType" id="rbtOrdAll" value="0" checked="checked"  onclick="SerRbtOrdValue('0')"/>不限制
                                    <input type="radio" name="RbtOrdType" id="rbtOrdYes" value="1" onclick="SerRbtOrdValue('1')"/>有订单
                                    <input type="radio" name="RbtOrdType" id="rbtOrdNo" value="2" onclick="SerRbtOrdValue('2')"/>无订单
                                    <asp:HiddenField ID="hidOrdType" runat="server"/>
                                </td>
                                <td align="left">
                                    <div id="dvOrd" style="display:none" runat="server">
                                        <input name="textfield" type="text" id="txtOrdFrom" runat="server" style="width:50px;" maxlength="3" value=""/>
                                        &nbsp;～&nbsp;
                                        <input name="textfield" type="text" id="txtOrdTo" runat="server" style="width:50px;" maxlength="3" value=""/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align:right">有否成功订单：</td>
                    <td style="width: 350px">
                        <table style="width: 120%">
                            <tr>
                                <td align="left" style="width:45%">
                                    <%--<asp:RadioButtonList ID="rdbtnSuclist" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="">不限制</asp:ListItem>
                                        <asp:ListItem Value="999999">有订单</asp:ListItem>
                                        <asp:ListItem Value="0">无订单</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                    <input type="radio" name="RbtSucOrdType" id="rbtSucOrdAll" value="0" checked="checked"  onclick="SerRbtSucOrdValue('0')"/>不限制
                                    <input type="radio" name="RbtSucOrdType" id="rbtSucOrdYes" value="1" onclick="SerRbtSucOrdValue('1')"/>有订单
                                    <input type="radio" name="RbtSucOrdType" id="rbtSucOrdNo" value="2" onclick="SerRbtSucOrdValue('2')"/>无订单
                                    <asp:HiddenField ID="hidSucOrdType" runat="server"/>
                                </td>
                                <td align="left">
                                    <div id="dvSucOrd" style="display:none" runat="server">
                                        <input name="textfield" type="text" id="txtSucOrdFrom" runat="server" style="width:50px;" maxlength="3" value=""/>
                                        &nbsp;～&nbsp;
                                        <input name="textfield" type="text" id="txtSucOrdTo" runat="server" style="width:50px;" maxlength="3" value=""/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle()" onclick="btnSearch_Click" Text="搜索"/>
                        <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" class="button" />
                        <asp:Button ID="btnExport" runat="server" CssClass="btn primary" Text="导出Excel" onclick="btnExport_Click"/> 
                        <asp:HiddenField ID="hidSelectType" runat="server"/>
                    </td>
                    <td></td>
                </tr>
            </table>
        </li>
      </ul>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
                <ContentTemplate>
      <%--<div class="frame01">
      <ul>
        <li>
            总记录数：<asp:Label ID="lbCount" runat="server" Text="" />
        </li>
        <li></li>
      </ul>
    </div>--%>
    <%--<div class="frame01"><ul><li><div id="messageContent" runat="server" style="color:red;width:800px;"></div></li></ul></div>--%>
    <div class="frame02">
         <div style="margin-left:10px;"><div id="messageContent" runat="server" style="color:red;width:800px;"></div></div>
         <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
         <asp:GridView ID="gridViewCSReviewUserList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:HyperLinkField HeaderText="手机号码" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="UserDetailPage.aspx?ID={0}" 
                    Target="_blank" DataTextField="LOGINMOBILE"><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>
                     <asp:BoundField DataField="CREATETIME" HeaderText="注册日期" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="REGCHANELNM" HeaderText="注册渠道" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="PLATFORMNM" HeaderText="注册平台" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="TODAYLOGIN" HeaderText="最后登陆时间" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="SIGN_KEY" HeaderText="验证码" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="VERSION" HeaderText="最后登陆版本" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="ALLCOUNT" HeaderText="总订单数" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="COMPLECOUNT" HeaderText="成功订单数" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
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
 <Triggers>
        <asp:PostBackTrigger ControlID="AspNetPager2" />
    </Triggers>
    </asp:UpdatePanel>
</div>
</asp:Content>