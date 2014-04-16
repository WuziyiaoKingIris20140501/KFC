<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="PushManagerPage.aspx.cs"  Title="Push消息管理" Inherits="PushManagerPage" %>
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
</style>
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>--%>
<script language="javascript" type="text/javascript" src="../../Scripts/ga.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    String.prototype.trim = function ()    //除去字符串左右的空格的函数
    {
        return this.replace(/(^\s+)|\s+$/g, "");
    }

    function ClearClickEvent() {
        document.getElementById("<%=messageContent.ClientID%>").innerText = "";
        document.getElementById("dvAdd").style.display = 'block';
        document.getElementById("dvSend").style.display = 'none';
        document.getElementById("dvUserGroup").style.display = 'none';
        document.getElementById("dvUserFiles").style.display = 'none';
        document.getElementById("wctUserGroup").value = "";
        document.getElementById("wctUserGroup").text = "";
        var userbtnObject = document.getElementById("<%=dvUserGroupList.ClientID%>");
        var usrbtnInput = userbtnObject.getElementsByTagName("input");
        var userbtnLength = usrbtnInput.length;
        for (var i = userbtnLength - 1; i >= 0; i--) {
            if (usrbtnInput[i].type = "button") {
                userbtnObject.removeChild(usrbtnInput[i]);
            }
        }
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
        var retunValue = window.open("LmSystemLogDetailPageByNew.aspx?ID=" + arg + "&time=" + time, null, fulls);
    }

    function BtnLoadStyle() {
        document.getElementById("progressBar").innerText = document.getElementById("<%=hidMsg.ClientID%>").value
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
        ajaxbg.show();
    }

    function BtnCompleteStyle() {
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
        window.clearInterval(0);
    }

    function RemainTimeBtn() {
        BtnLoadStyle();
        DvHidChangeEvent();
        var getTime = document.getElementById("<%=hidRemainSecond.ClientID%>").value;
        if (getTime <= 0) {
            BtnCompleteStyle();
            return;
        }
        var remSecond = getTime - 1;
        document.getElementById("<%=hidRemainSecond.ClientID%>").value = remSecond;
        if (remSecond == 0) {
            document.getElementById("<%=btnRefush.ClientID%>").click();
        }
    }

    function SerRbtNameValue(arg) {
        document.getElementById("<%=messageContent.ClientID%>").innerText = "";
        var cur = document.getElementById("<%=hidPushType.ClientID%>").value;
        if (cur != "" && cur == arg) {
            return;
        }
        if (arg == "0") {
            document.getElementById("dvUserGroup").style.display = 'none';
            document.getElementById("dvUserFiles").style.display = 'none';
            document.getElementById("wctUserGroup").value = "";
            document.getElementById("wctUserGroup").text = "";
            var userbtnObject = document.getElementById("<%=dvUserGroupList.ClientID%>");
            var usrbtnInput = userbtnObject.getElementsByTagName("input");
            var userbtnLength = usrbtnInput.length;
            for (var i = userbtnLength - 1; i >= 0; i--) {
                if (usrbtnInput[i].type = "button") {
                    userbtnObject.removeChild(usrbtnInput[i]);
                }
            }
            document.getElementById('<%=hidUserGroupList.ClientID%>').value = "";
        }
        else if (arg == "1") {
            document.getElementById("dvUserGroup").style.display = 'block';
            document.getElementById("dvUserFiles").style.display = 'none';
        }
        else if (arg == "2") {
            document.getElementById("dvUserGroup").style.display = 'none';
            document.getElementById("dvUserFiles").style.display = 'block';
            document.getElementById("wctUserGroup").value = "";
            document.getElementById("wctUserGroup").text = "";
            var userbtnObject = document.getElementById("<%=dvUserGroupList.ClientID%>");
            var usrbtnInput = userbtnObject.getElementsByTagName("input");
            var userbtnLength = usrbtnInput.length;
            for (var i = userbtnLength - 1; i >= 0; i--) {
                if (usrbtnInput[i].type = "button") {
                    userbtnObject.removeChild(usrbtnInput[i]);
                }
            }
            document.getElementById('<%=hidUserGroupList.ClientID%>').value = "";
        }

        document.getElementById("<%=hidPushType.ClientID%>").value = arg;
    }

    function DvChangeEvent() {
        var ajaxAdd = $("#dvAdd");
        var ajaxSed = $("#dvSend");

        if ($("#dvAdd").is(":hidden")) {
            ajaxAdd.show(500);
            ajaxSed.hide(500);
            document.getElementById("<%=messageContent.ClientID%>").innerText = "";
            ResetPushData();
        }
        else {
            ajaxAdd.hide(500);
            ajaxSed.show(500);
        }
    }

    function DvHidChangeEvent() {
        document.getElementById("dvAdd").style.display = 'none';
        document.getElementById("dvSend").style.display = 'block';
    }

    function DvShChangeEvent() {
        document.getElementById("dvAdd").style.display = 'block';
        document.getElementById("dvSend").style.display = 'none';
        ResetPushData();
    }

    function DoSaveEvent() {
        BtnCompleteStyle();
        DvChangeEvent();
    }

    function BtnAddUserGroup() {
        var idVal = document.getElementById("wctUserGroup").value;
        if (idVal.trim() == "") {
            return;
        }

        var btnid = "btnCommon_" + idVal.substring(0, idVal.indexOf("]"));
        var dpValue = document.getElementById("wctUserGroup").value + "   ";
        if (btnid == "btnCommon_") {
            document.getElementById("wctUserGroup").value = "";
            document.getElementById("wctUserGroup").text = "";
            return;
        }
        if (document.getElementById(btnid) != null) {
            document.getElementById("wctUserGroup").value = "";
            document.getElementById("wctUserGroup").text = "";
            return;
        }
        var board = document.getElementById("<%=dvUserGroupList.ClientID%>");
        var e = document.createElement("input");
        e.type = "button";
        e.setAttribute("id", btnid);
        e.value = dpValue;
        e.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
        e.onclick = function () {
            e.parentNode.removeChild(this);
        }
        board.appendChild(e);

        document.getElementById("wctUserGroup").value = "";
        document.getElementById("wctUserGroup").text = "";
    }

    function SetContronListVal() {

        var cur = document.getElementById("<%=hidPushType.ClientID%>").value;
        if (cur == "2") {
            // FileUpload
            document.getElementById("<%=hidTokenFlUpload.ClientID%>").value = document.getElementById("<%=TokenFlUpload.ClientID%>").value;
        }
        else if (cur == "1") {
            var userboard = document.getElementById("<%=dvUserGroupList.ClientID%>");
            var useridList = "";
            for (i = 0; i < userboard.childNodes.length; i++) {
                //useridList = useridList + userboard.childNodes[i].id.substring(11) + ','

                useridList = useridList + userboard.childNodes[i].value + ','
            }
            document.getElementById("<%=hidUserGroupList.ClientID%>").value = useridList;
        }
    }

    function ResetPushData() {
        var PushType = document.getElementById("<%=hidPushType.ClientID%>").value;
        if (PushType =="0") {
            document.getElementById("rbtnAll").checked = true;
            document.getElementById("dvUserGroup").style.display = 'none';
            document.getElementById("dvUserFiles").style.display = 'none';
        }
        else if (PushType == "1") {
            document.getElementById("rbtUserGroup").checked = true;
            document.getElementById("dvUserGroup").style.display = 'block';
            document.getElementById("dvUserFiles").style.display = 'none';

            if (document.getElementById('<%=hidUserGroupList.ClientID%>').value != "") {
                var ProDetailUserGroup = document.getElementById('<%=hidUserGroupList.ClientID%>').value;
                var comName = new Array();
                comName = ProDetailUserGroup.split(","); //字符分割

                var userbtnid;
                var userboard = document.getElementById("<%=dvUserGroupList.ClientID%>");
                for (i = 0; i < comName.length; i++) {
                    if (comName[i].trim() == "") {
                        continue;
                    }
                    var userbtnid = comName[i].substring(1, comName[i].indexOf("]")); ;
                    if (document.getElementById(userbtnid) != null) {
                        continue;
                    }
                    var userE = document.createElement("input");
                    userE.type = "button";
                    userE.setAttribute("id", userbtnid);
                    userE.value = comName[i] + "   ";
                    userE.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
                    userboard.appendChild(userE);
                }
                setUserGroupButtonClick();
            }
        }
        else if (PushType == "2") {
            document.getElementById("rbtUserFiles").checked = true;
            document.getElementById("dvUserGroup").style.display = 'none';
            document.getElementById("dvUserFiles").style.display = 'block';
            document.getElementById("<%=lbTokenFlUpload.ClientID%>").innerText = document.getElementById("<%=hidTokenFlUpload.ClientID%>").value;
        }
    }

    function setUserGroupButtonClick() {
        var btnObject = document.getElementById("<%=dvUserGroupList.ClientID%>");
        var btnInput = btnObject.getElementsByTagName("input");
        if (btnInput != null) {
            var btnLength = btnInput.length;
            for (var i = btnLength - 1; i >= 0; i--) {
                if (btnInput[i].type = "button") {
                    btnInput[i].onclick = function () {
                        btnObject.removeChild(this);
                    };
                }
            }
        }
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="frame01" id="dvAdd">
              <ul>
                <li class="title">Push任务</li>
                <li>
                    <table>
                        <tr>
                            <td align="right">
                                Push任务标题：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPushTitle" runat="server" Width="500px" MaxLength="32" /><font color="#AAAAAA"> * 限制输入32个中文字符</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left">
                                <font color="#AAAAAA">* Push消息标题只做管理用途，不会显示在客户端</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Push信息正文：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPushContent" runat="server" TextMode="MultiLine" Width="850" Height="100"/><font color="#AAAAAA"> * 限制输入150个中文字符</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                 Push目标：
                            </td>
                            <td align="left">
                                <asp:HiddenField ID="hidPushType" runat="server"/>
                                <input type="radio" name="RbtPushType" id="rbtnAll" value="0" checked="checked" onclick="SerRbtNameValue('0')"/>所有有效用户
                                <input type="radio" name="RbtPushType" id="rbtUserGroup" value="1" onclick="SerRbtNameValue('1')"/>指定用户组
                                <input type="radio" name="RbtPushType" id="rbtUserFiles" value="2" onclick="SerRbtNameValue('2')"/>上传发送列表
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left">
                                <div id="dvUserGroup">
                                    <%--用户组：<asp:DropDownList ID="ddpUserGroup" runat="server" Width="150px" Height="25px"></asp:DropDownList>--%>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <uc1:WebAutoComplete ID="wctUserGroup" CTLID="wctUserGroup" runat="server" AutoType="usergroup" AutoParent="PushManagerPage.aspx?Type=usergroup" />
                                            </td>
                                            <td align="left">
                                                <input id="btnAddUserGroup" type ="button" value ="添加" class="btn primary" onclick ="BtnAddUserGroup()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 350px">
                                                    <asp:HiddenField ID="hidUserGroupList" runat="server"/>
                                                    <div id="dvUserGroupList" style="width:350px" runat="server"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="dvUserFiles" style="display:none">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                选择文件：
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hidTokenFlUpload" runat="server"/>
                                                <asp:FileUpload ID="TokenFlUpload" runat="server"  Width="500px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label id="lbTokenFlUpload" style="width:350px" runat="server"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                            </td>
                            <td align="left">
                                <asp:HiddenField ID="hidTaskID" runat="server"/>
                                <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetContronListVal();BtnLoadStyle();" onclick="btnSave_Click" />
                                <%--<input type="button" id="btnClear" style="width:80px;height:20px;" value="重置"  onclick="ClearClickEvent();" />--%>
                            </td>
                        </tr>
                    </table>
                </li>
              </ul>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>

     <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server" >
        <ContentTemplate>
            <div class="frame01" id="dvSend">
              <ul>
                <li class="title">Push任务Review</li>
                <li>
                    <table width="90%">
                        <tr>
                            <td style="width:10%" align="right">Push任务标题：</td>
                            <td align="left" colspan="3">
                            <asp:Label ID="lbPushTitle" runat="server" /> &nbsp;&nbsp;&nbsp;
                            预计发送条数：<asp:Label ID="lbPushAllNum" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">Push信息正文：</td>
                            <td colspan="3" align="left"><asp:TextBox ID="txtRePushContent" runat="server" TextMode="MultiLine" Width="850" Height="40" ReadOnly="true"/></td>
                        </tr>
                        <tr>
                            <td align="right" valign="top"></td>
                            <td colspan="3" align="left">
                                <asp:HiddenField ID="hidRemainSecond" runat="server"/>
                                <asp:HiddenField ID="hidMsg" runat="server"/>
                                <asp:Button ID="btnSend" runat="server" CssClass="btn primary" Text="发送" OnClientClick="BtnLoadStyle();" onclick="btnSend_Click" />
                                <div id="dvRefush" style="display:none;"><asp:Button ID="btnRefush" runat="server" Width="80px" Height="20px" Text="刷新" onclick="btnRefush_Click" /></div>
                                &nbsp;&nbsp;&nbsp;
                                <input type="button" id="btnBack" class="btn" value="返回修改"  onclick="DvChangeEvent();" />
                            </td>
                        </tr>
                    </table>
                </li>
              </ul>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSend" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" >
        <ContentTemplate>
            <div id="background" class="pcbackground" style="display: none; "></div> 
            <div id="progressBar" class="pcprogressBar" style="display: none; "></div> 
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
        <ContentTemplate>
            <div class="frame01">
                <ul>
                <li class="title">近期Push任务</li>
                </ul>
            </div>
            <div class="frame02">
            <div id="messageContent" runat="server" style="color:red"></div>
                <%--<div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>--%>
                    <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                    onrowdatabound="gridViewCSReviewList_RowDataBound" PageSize="15" AllowPaging="true" 
                    onpageindexchanging="gridViewCSReviewList_PageIndexChanging" CssClass="GView_BodyCSS">
                    <Columns>
                            <asp:BoundField DataField="TID" HeaderText="ID" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                            <asp:BoundField DataField="PTITLE" HeaderText="消息标题" ><ItemStyle HorizontalAlign="Center" Width="45%"/></asp:BoundField>
                            <asp:BoundField DataField="ALLNUM" HeaderText="总条数"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                            <asp:BoundField DataField="SUCNUM" HeaderText="成功发送条数"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                            <asp:BoundField DataField="SEDTIME" HeaderText="发送时间"><ItemStyle HorizontalAlign="Center" Width="20%" /></asp:BoundField>
                            <asp:HyperLinkField HeaderText="查看详情" DataNavigateUrlFields="TID" DataNavigateUrlFormatString="PushSearchDeatilPage.aspx?ID={0}" 
                        Target="_blank" DataTextField="MODIFY"><ItemStyle HorizontalAlign="Center" Width="5%" /></asp:HyperLinkField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                    <PagerStyle HorizontalAlign="Right" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                </asp:GridView>
                <%--         <div style="margin-left:10px;">
                    <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb"  ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%" ShowPageIndexBox="always" AlwaysShow="true" width="100%" LayoutType="Table" onpagechanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                </div>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>