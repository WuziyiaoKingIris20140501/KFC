<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="PromotionInfoManager.aspx.cs"  Title="促销管理" Inherits="PromotionInfoManager" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    function SetRbtTypeList(arg) {
        alert(arg);
    }

    function SetBtnStyleList(userType, commonType) {
        if (userType == "1") {
            document.getElementById("btnAddUserGroup").disabled = true;
        }
        else {
            document.getElementById("btnAddUserGroup").disabled = false;
        }

        if (commonType == "1") {
            document.getElementById("btnAddCommon").style.display = 'none';
            document.getElementById("<%=dvHotelChkCommon.ClientID%>").style.display = 'none'; 
        }
        else {
            document.getElementById("btnAddCommon").style.display = 'block';
            document.getElementById("<%=dvHotelChkCommon.ClientID%>").style.display = 'block'; 
        }
    }

    function ClearClickEvent() {

        document.getElementById("<%=txtPromotionTitle.ClientID%>").value = "";
        document.getElementById("<%=txtPromDescZh.ClientID%>").value = "";
        //document.getElementById("txtImgFileUpload").value = "";
        $("#PreList").empty();
        document.getElementById("<%=ddpPriorityList.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=dpKeepStart.ClientID%>").value = "";
        document.getElementById("<%=dpKeepEnd.ClientID%>").value = "";
        document.getElementById("<%=chkAllCommon.ClientID%>").checked = false;
        document.getElementById("btnAddCommon").disabled = false;

        var btnObject = document.getElementById("<%=dvCommonList.ClientID%>");
        var btnInput = btnObject.getElementsByTagName("input");
        var btnLength = btnInput.length;
        for (var i = btnLength - 1; i >= 0; i--) {
            if (btnInput[i].type = "button") {
                btnObject.removeChild(btnInput[i]);
            }
        }

        document.getElementById("rbtnAll").checked = true;
        document.getElementById("<%=hidCommonType.ClientID%>").value = 0;
        document.getElementById("<%=lbCommonNM.ClientID%>").innerText = "链接URL：";

        document.getElementById("wctHotel").value = "";
        document.getElementById("wctHotel").text = "";
        document.getElementById("wctCity").value = "";
        document.getElementById("wctCity").text = "";
        document.getElementById("wctHotelGroup").value = "";
        document.getElementById("wctHotelGroup").text = "";

        document.getElementById("<%=txtLinkUrl.ClientID%>").value = "";
        document.getElementById("<%=dvALL.ClientID%>").style.display = '';
        
        document.getElementById("<%=dvCityList.ClientID%>").style.display = 'none';
        document.getElementById("<%=dvHotelList.ClientID%>").style.display = 'none';
        document.getElementById("<%=dvHotelGroupList.ClientID%>").style.display = 'none';
        document.getElementById("btnAddCommon").style.display = 'none';
        document.getElementById("<%=dvHotelChkCommon.ClientID%>").style.display = 'none'; 

        document.getElementById("<%=chkAllUserGroup.ClientID%>").checked = true
        var userbtnObject = document.getElementById("<%=dvUserGroupList.ClientID%>");
        var usrbtnInput = userbtnObject.getElementsByTagName("input");
        var userbtnLength = usrbtnInput.length;
        for (var i = userbtnLength - 1; i >= 0; i--) {
            if (usrbtnInput[i].type = "button") {
                userbtnObject.removeChild(usrbtnInput[i]);
            }
        }

        document.getElementById("btnAddUserGroup").disabled = true;
        document.getElementById("wctUserGroup").disabled = true;
        document.getElementById("wctUserGroup").value = "";
        document.getElementById("wctUserGroup").text = "";

        document.getElementById("<%=dvwctHotelRoom.ClientID%>").style.display = 'none';
        document.getElementById("<%=dvHotelRoomList.ClientID%>").style.display = 'none';
        document.getElementById("wctHotelRoom").value = "";
        document.getElementById("wctHotelRoom").text = "";
        document.getElementById("<%=hidHotelID.ClientID%>").value = "";
        document.getElementById("<%=txtImgFilePath.ClientID%>").value = "";
        document.getElementById("<%=ddpPriceType.ClientID%>").selectedIndex = 0;
        GoDel();
    }

    function SerRbtNameValue(arg) {
        var cur = document.getElementById("<%=hidCommonType.ClientID%>").value;
        if (cur != "" && cur == arg) {
            return;
        }
        document.getElementById("<%=hidChkCommonType.ClientID%>").value = "0";
        document.getElementById("<%=chkAllCommon.ClientID%>").checked = false;
        document.getElementById("btnAddCommon").disabled = false;
        if (arg == "0") {
            document.getElementById("<%=txtLinkUrl.ClientID%>").value = "";
            document.getElementById("<%=dvALL.ClientID%>").style.display = '';

            document.getElementById("<%=dvCityList.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelList.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelGroupList.ClientID%>").style.display = 'none';
            document.getElementById("btnAddCommon").style.display = 'none';
            document.getElementById("<%=dvHotelChkCommon.ClientID%>").style.display = 'none'; 
            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
            document.getElementById("wctCity").value = "";
            document.getElementById("wctCity").text = "";
            document.getElementById("wctHotelGroup").value = "";
            document.getElementById("wctHotelGroup").text = "";

            document.getElementById("<%=lbCommonNM.ClientID%>").innerText = "链接URL：";
            document.getElementById("<%=dvwctHotelRoom.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelRoomList.ClientID%>").style.display = 'none';
            document.getElementById("wctHotelRoom").value = "";
            document.getElementById("wctHotelRoom").text = "";
            document.getElementById("<%=hidHotelID.ClientID%>").value = "";
            GoDel();
        }
        else if (arg == "1") {
            document.getElementById("<%=txtLinkUrl.ClientID%>").value = "";
            document.getElementById("<%=dvALL.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvCityList.ClientID%>").style.display = 'block';
            document.getElementById("<%=dvHotelList.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelGroupList.ClientID%>").style.display = 'none';
            document.getElementById("btnAddCommon").style.display = 'block';
            document.getElementById("<%=dvHotelChkCommon.ClientID%>").style.display = 'block'; 
            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
            document.getElementById("wctCity").value = "";
            document.getElementById("wctCity").text = "";
            document.getElementById("wctHotelGroup").value = "";
            document.getElementById("wctHotelGroup").text = "";
            document.getElementById("<%=lbCommonNM.ClientID%>").innerText = "促销城市：";
            document.getElementById("<%=dvwctHotelRoom.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelRoomList.ClientID%>").style.display = 'none';
            document.getElementById("wctHotelRoom").value = "";
            document.getElementById("wctHotelRoom").text = "";
            document.getElementById("<%=hidHotelID.ClientID%>").value = "";
            document.getElementById("wctCity").disabled = false;
            GoDel();
        }
        else if (arg == "2") {
            document.getElementById("<%=txtLinkUrl.ClientID%>").value = "";
            document.getElementById("<%=dvALL.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvCityList.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelList.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelGroupList.ClientID%>").style.display = 'block';
            document.getElementById("btnAddCommon").style.display = 'block';
            document.getElementById("<%=dvHotelChkCommon.ClientID%>").style.display = 'block'; 
            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
            document.getElementById("wctCity").value = "";
            document.getElementById("wctCity").text = "";
            document.getElementById("wctHotelGroup").value = "";
            document.getElementById("wctHotelGroup").text = "";
            document.getElementById("<%=lbCommonNM.ClientID%>").innerText = "酒店集团：";
            document.getElementById("<%=dvwctHotelRoom.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelRoomList.ClientID%>").style.display = 'none';
            document.getElementById("wctHotelRoom").value = "";
            document.getElementById("wctHotelRoom").text = "";
            document.getElementById("<%=hidHotelID.ClientID%>").value = "";
            document.getElementById("wctHotelGroup").disabled = false;
            GoDel();
        }
        else if (arg == "3") {
            document.getElementById("<%=txtLinkUrl.ClientID%>").value = "";
            document.getElementById("<%=dvALL.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvCityList.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelList.ClientID%>").style.display = 'block';
            document.getElementById("<%=dvHotelGroupList.ClientID%>").style.display = 'none';
            document.getElementById("btnAddCommon").style.display = 'block';
            document.getElementById("<%=dvHotelChkCommon.ClientID%>").style.display = 'block'; 
            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
            document.getElementById("wctCity").value = "";
            document.getElementById("wctCity").text = "";
            document.getElementById("wctHotelGroup").value = "";
            document.getElementById("wctHotelGroup").text = "";
            document.getElementById("<%=lbCommonNM.ClientID%>").innerText = "促销酒店：";
            document.getElementById("<%=dvwctHotelRoom.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelRoomList.ClientID%>").style.display = 'none';
            document.getElementById("wctHotelRoom").value = "";
            document.getElementById("wctHotelRoom").text = "";
            document.getElementById("<%=hidHotelID.ClientID%>").value = "";
            document.getElementById("wctHotel").disabled = false;
            GoDel();
        }
        else if (arg == "4") {
            document.getElementById("<%=txtLinkUrl.ClientID%>").value = "";
            document.getElementById("<%=dvALL.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvCityList.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelList.ClientID%>").style.display = 'none';
            document.getElementById("<%=dvHotelGroupList.ClientID%>").style.display = 'none';
            document.getElementById("btnAddCommon").style.display = 'none';
            document.getElementById("<%=dvHotelChkCommon.ClientID%>").style.display = 'block'; 
            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
            document.getElementById("wctCity").value = "";
            document.getElementById("wctCity").text = "";
            document.getElementById("wctHotelGroup").value = "";
            document.getElementById("wctHotelGroup").text = "";
            document.getElementById("<%=lbCommonNM.ClientID%>").innerText = "酒店房型：";
            document.getElementById("<%=dvwctHotelRoom.ClientID%>").style.display = 'block';
            document.getElementById("<%=dvHotelRoomList.ClientID%>").style.display = 'block';
            document.getElementById("wctHotelRoom").disabled = false;
            document.getElementById("<%=btnSelectHotel.ClientID%>").disabled = false;
        }
        else {

        }

        //清空
        var btnObject = document.getElementById("<%=dvCommonList.ClientID%>");
        var btnInput = btnObject.getElementsByTagName("input");
        var btnLength = btnInput.length;
        for (var i = btnLength - 1; i >= 0; i--) {
            if (btnInput[i].type = "button") {
                btnObject.removeChild(btnInput[i]);
            }
        }
        document.getElementById("<%=hidCommonType.ClientID%>").value = arg;
    }

    function SetChkAllCommonStyle() {
//        var arg = "";
//        if (document.getElementById("rbtnAll").checked == true)
//        {
//            arg = "0";
//        }
//        if (document.getElementById("rbtCity").checked == true)
//        {
//            arg = "1";
//        }
//        if (document.getElementById("rbtHotelGroup").checked == true)
//        {
//            arg = "2";
//        }
//        if (document.getElementById("rbtHotel").checked == true)
//        {
//            arg = "3";
//        }
//        if (document.getElementById("rbtRoom").checked == true)
//        {
//            arg = "4";
//        }

        var arg = document.getElementById("<%=hidCommonType.ClientID%>").value;

        if (arg == "0") {
        }
        else if (arg == "1") {
            if (document.getElementById("<%=chkAllCommon.ClientID%>").checked == true) {
                document.getElementById("wctCity").value = "";
                document.getElementById("wctCity").text = "";
                document.getElementById("wctCity").disabled = true;
                document.getElementById("btnAddCommon").disabled = true;
            }
            else {
                document.getElementById("btnAddCommon").disabled = false;
                document.getElementById("wctCity").disabled = false;
            }
        }
        else if (arg == "2") {
            if (document.getElementById("<%=chkAllCommon.ClientID%>").checked == true) {
                document.getElementById("wctHotelGroup").value = "";
                document.getElementById("wctHotelGroup").text = "";
                document.getElementById("wctHotelGroup").disabled = true;
                document.getElementById("btnAddCommon").disabled = true;
            }
            else {
                document.getElementById("wctHotelGroup").disabled = false;
                document.getElementById("btnAddCommon").disabled = false;
            }
        }
        else if (arg == "3") {
            if (document.getElementById("<%=chkAllCommon.ClientID%>").checked == true) {
                document.getElementById("wctHotel").value = "";
                document.getElementById("wctHotel").text = "";
                document.getElementById("wctHotel").disabled = true;
                document.getElementById("btnAddCommon").disabled = true;
            }
            else {
                document.getElementById("wctHotel").disabled = false;
                document.getElementById("btnAddCommon").disabled = false;
            }
        }
        else if (arg == "4") {
            if (document.getElementById("<%=chkAllCommon.ClientID%>").checked == true) {
                document.getElementById("wctHotelRoom").value = "";
                document.getElementById("wctHotelRoom").text = "";
                document.getElementById("wctHotelRoom").disabled = true;
                document.getElementById("<%=btnSelectHotel.ClientID%>").disabled = true;
                document.getElementById("<%=hidHotelID.ClientID%>").value = "";
                GoDel();
            }
            else {
                document.getElementById("wctHotelRoom").disabled = false;
                document.getElementById("<%=btnSelectHotel.ClientID%>").disabled = false;
            }
        }
        else {

        }

        //清空
        var btnObject = document.getElementById("<%=dvCommonList.ClientID%>");
        var btnInput = btnObject.getElementsByTagName("input");
        var btnLength = btnInput.length;
        for (var i = btnLength - 1; i >= 0; i--) {
            if (btnInput[i].type = "button") {
                btnObject.removeChild(btnInput[i]);
            }
        }

        if (document.getElementById("<%=chkAllCommon.ClientID%>").checked == true)
        {
            document.getElementById("<%=hidChkCommonType.ClientID%>").value = "1";
        }
        else
        {
            document.getElementById("<%=hidChkCommonType.ClientID%>").value = "0";
        }
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

    function SetddpUserGroupList() {
        if (document.getElementById("<%=chkAllUserGroup.ClientID%>").checked == true) {
            var btnObject = document.getElementById("<%=dvUserGroupList.ClientID%>");
            var btnInput = btnObject.getElementsByTagName("input");
            var btnLength = btnInput.length;
            for (var i = btnLength - 1; i >= 0; i--) {
                if (btnInput[i].type = "button") {
                    btnObject.removeChild(btnInput[i]);
                }
            }
            document.getElementById("btnAddUserGroup").disabled = true;
            document.getElementById("wctUserGroup").disabled = true;
            document.getElementById("wctUserGroup").value = "";
            document.getElementById("wctUserGroup").text = "";
        }
        else {
            document.getElementById("btnAddUserGroup").disabled = false;
            document.getElementById("wctUserGroup").disabled = false;
        }
    }

    function BtnSelectHotel() {
        document.getElementById("<%=hidHotelID.ClientID%>").value = document.getElementById("wctHotelRoom").value;
    }

    function BtnCommonList() {
        var cur = document.getElementById("<%=hidCommonType.ClientID%>").value;
        var btnid;
        var dpValue;
        var idVal;

        if (cur == "1") {
            idVal = document.getElementById("wctCity").value;
            dpValue = document.getElementById("wctCity").value + "   ";
            document.getElementById("wctCity").value = "";
            document.getElementById("wctCity").text = "";
        }
        else if (cur == "2") {
            idVal = document.getElementById("wctHotelGroup").value;
            dpValue = document.getElementById("wctHotelGroup").value + "   ";
            document.getElementById("wctHotelGroup").value = "";
            document.getElementById("wctHotelGroup").text = "";
        }
        else if (cur == "3") {
            idVal = document.getElementById("wctHotel").value;
            dpValue = document.getElementById("wctHotel").value + "   ";
            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
        }

        if (idVal.trim() == "") {
            return;
        }

        btnid = "btnCommon_" + idVal.substring(0, idVal.indexOf("]"));

        if (btnid == "btnCommon_") {
            return;
        }

        if (document.getElementById(btnid) != null) {
            return;
        }
        var board = document.getElementById("<%=dvCommonList.ClientID%>");
        var e = document.createElement("input");
        e.type = "button";
        e.setAttribute("id", btnid);
        e.value = dpValue;
        e.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
        e.onclick = function () {
            e.parentNode.removeChild(this);
        }
        board.appendChild(e);
    }

    function SetContronListVal() {
        var commidList = "";
        var cur = document.getElementById("<%=hidCommonType.ClientID%>").value;
        if (cur == "1" || cur == "2" || cur == "3") {
            var commboard = document.getElementById("<%=dvCommonList.ClientID%>");

            for (i = 0; i < commboard.childNodes.length; i++) {
                commidList = commidList + commboard.childNodes[i].id.substring(11) + ',';
            }
            document.getElementById("<%=hidCommonList.ClientID%>").value = commidList;
        }
        else if (cur == "4") {
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
        else {

        }

        var userboard = document.getElementById("<%=dvUserGroupList.ClientID%>");
        var useridList = "";
        for (i = 0; i < userboard.childNodes.length; i++) {
            useridList = useridList + userboard.childNodes[i].id.substring(11) + ','
        }

        document.getElementById("<%=hidUserGroupList.ClientID%>").value = useridList;
    }

    function BtnSelectImgFiles(arg, type) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        var retunValue = window.showModalDialog("../../Common/com_fileUpload.aspx?multiLine=1&displaypre=0&id=" + arg + "&type=" + type + "&time=" + time, window, "dialogWidth=800px;dialogHeight=400px");
        if (retunValue) {
            if (type == "0") {

            }
            else {

            }
        }

        //$("#PreList").empty();
        //document.getElementById("txtImgFileUpload").value = "";
        //        if (document.getElementById("<%=img_photo.ClientID%>").value != null && document.getElementById("<%=img_photo.ClientID%>").value != "") {
        //            var data = "<div><img src='../../temp/" + document.getElementById("<%=img_photo.ClientID%>").value + "' alt='' style='width:150px;height:150px'/></div>";
        //            $("#PreList").append(data);
        //            document.getElementById("txtImgFileUpload").value = unescape(document.getElementById('<%=image_src.ClientID %>').value);
        //        }
    }

    function callback(img_photo, image_src) {
        document.getElementById('<%=img_photo.ClientID %>').value = img_photo;
        document.getElementById('<%=image_src.ClientID %>').value = image_src;
    }

    $().ready(function () {
        //        if (document.getElementById("<%=img_photo.ClientID%>").value != null && document.getElementById("<%=img_photo.ClientID%>").value != "") {
        //            var data = "<div><img src='../../temp/" + document.getElementById("<%=img_photo.ClientID%>").value + "' alt='' style='width:150px;height:150px'/></div>";
        //            $("#PreList").empty();
        //            $("#PreList").append(data);
        //        }
        SetBtnStyleList('1', '1');
        document.getElementById("<%=hidCommonType.ClientID%>").value = "0";
    });


</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">添加促销信息</li>
        <li>
            <table style="line-height:25px">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                 <ContentTemplate>
                <tr>
                    <td align="right">
                        促销信息标题：
                    </td>
                    <td style="width: 90%">
                        <table>
                            <tr>
                                <td>
                                    <input name="textfield" type="text" id="txtPromotionTitle" runat="server" style="width:300px;" maxlength="45" value=""/><font color="red">*</font>
                                </td>
                                <td>
                                    <div id="dvStatus" style="display:none"><asp:Label ID="lbStatus" runat="server" Text="上下线状态：" /><asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server" Width="60px"></asp:DropDownList></div>
                                </td>
                                <td>
                                    优先级：<asp:DropDownList ID="ddpPriorityList" runat="server" CssClass="noborder_inactive" Width="120px"></asp:DropDownList>
                                </td>
                                <td>
                                    促销方式：<asp:DropDownList ID="ddpPromotionType" runat="server" CssClass="noborder_inactive" Width="120px"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        促销持续时间：
                    </td>
                    <td style="width: 90%">
                        <input id="dpKeepStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',maxDate:'#F{$dp.$D(\'MainContent_dpKeepEnd\')||\'2020-10-01\'}'})" runat="server"/>
                        <input id="dpKeepEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'MainContent_dpKeepStart\')}',maxDate:'2020-10-01'})" runat="server"/><font color="red">*</font>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        促销类型：
                    </td>
                    <td style="width: 90%">
                        <input type="radio" name="RbtPromotionType" id="rbtnAll" value="0" checked="checked" onclick="SerRbtNameValue('0')"/>全局促销
                        <input type="radio" name="RbtPromotionType" id="rbtCity" value="1" onclick="SerRbtNameValue('1')"/>城市促销
                        <input type="radio" name="RbtPromotionType" id="rbtHotelGroup" value="2" onclick="SerRbtNameValue('2')"/>酒店集团促销
                        <input type="radio" name="RbtPromotionType" id="rbtHotel" value="3" onclick="SerRbtNameValue('3')"/>酒店促销
                        <input type="radio" name="RbtPromotionType" id="rbtRoom" value="4" onclick="SerRbtNameValue('4')"/>房型促销
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="width: 90%">
                        <table>
                            <tr>
                                <td><asp:Label ID="lbCommonNM" runat="server" Text="" /></td>
                                <td>
                                     <div id="dvALL" runat="server"><asp:TextBox ID="txtLinkUrl" runat="server" Width="350px" MaxLength="500"/></div>
                                     <div id="dvCityList" runat="server" style="display:none"><uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" AutoParent="PromotionInfoManager.aspx?Type=city" /></div>
                                     <div id="dvHotelGroupList" runat="server" style="display:none"><uc1:WebAutoComplete ID="wctHotelGroup" CTLID="wctHotelGroup" runat="server" AutoType="hotelgroup" AutoParent="PromotionInfoManager.aspx?Type=hotelgroup" /></div>
                                     <div id="dvHotelList" runat="server" style="display:none"><uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel" AutoParent="PromotionInfoManager.aspx?Type=hotel" /></div>
                                     <div id="dvwctHotelRoom" runat="server" style="display:none"><uc1:WebAutoComplete ID="wctHotelRoom" CTLID="wctHotelRoom" runat="server" AutoType="hotel" AutoParent="PromotionInfoManager.aspx?Type=hotel" /></div>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <input id="btnAddCommon" type ="button" value ="添加" class="btn primary" onclick ="BtnCommonList()" />
                                                <div id="dvHotelRoomList" runat="server" style="display:none">
                                                     <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                    <asp:Button ID="btnSelectHotel" runat="server" CssClass="btn primary" Text="选择" OnClientClick="BtnSelectHotel()" onclick="btnSelectHotel_Click"/>
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                </div>
                                            </td>
                                            <td style="width:120px;">
                                                <div id="dvHotelChkCommon" runat="server"><input type="checkbox" name="chkAllCommon" id="chkAllCommon" runat="server" onclick="SetChkAllCommonStyle()"/>不限制</div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                     <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                            <asp:CheckBoxList ID="chkHotelRoomList" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                                    </ContentTemplate>
                                         </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>  
                    </td>
                 </tr>
                <tr>
                    <td ></td>
                    <td style="width: 90%">
                        <div id="dvCommonList" runat="server" style="width:800px"/>
                    </td>
                 </tr>
                <tr>
                    <td align="right">价格代码类型：</td>
                    <td style="width: 90%">
                        <asp:DropDownList ID="ddpPriceType" runat="server" CssClass="noborder_inactive" Width="120px"></asp:DropDownList>
                    </td>
                 </tr>
                <tr>
                    <td align="right">
                        促销用户组：
                    </td>
                    <td style="width: 90%">
                         <%--<asp:DropDownList ID="ddpUserGroupList" runat="server" CssClass="noborder_inactive" Width="350px"></asp:DropDownList>--%>
                         <table>
                            <tr>
                                <td>
                                    <uc1:WebAutoComplete ID="wctUserGroup" CTLID="wctUserGroup" runat="server" AutoType="usergroup" AutoParent="PromotionInfoManager.aspx?Type=usergroup" />
                                </td>
                                <td style="width:120px">
                                    <input id="btnAddUserGroup" type ="button" value ="添加" class="btn primary" onclick ="BtnAddUserGroup()" />
                                    <input type="checkbox" name="chkAllUserGroup" id="chkAllUserGroup" runat="server" onclick="SetddpUserGroupList()" />不限制
                                </td>
                            </tr>
                         </table>
                    </td>
                 </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td style="width: 90%">
                         <div id="dvUserGroupList" style="width:800px" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        促销信息正文：

                    </td>
                    <td style="width: 90%">
                        <asp:TextBox ID="txtPromDescZh" runat="server" TextMode="MultiLine" 
                            Width="65%" Height="95px"/><span style="color:#AAAAAA"><font color="red">*</font>&nbsp;最多180个中文字</span>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        相关图片：
                    </td>
                    <td  style="width: 90%">
                        <input type="text" id="txtImgFilePath" style="width:65%;" runat="server" maxlength="1000"/>
                    </td>
               </tr>    
                 </ContentTemplate>
                </asp:UpdatePanel>
              
                <%--<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <tr>
                    <td align="right" valign="top">
                        相关图片：
                    </td>
                    <td  style="width: 90%">
                        <input type="text" id="txtImgFileUpload" style="width:450px;" value="" readonly="readonly"/>
                        <input id="btnSelectImgFiles" type ="button" value ="选择图片" style="width:100px;height:20px;" onclick ="BtnSelectImgFiles('','0')" />
                    </td>
               </tr>               
                <tr>
                    <td></td>
                    <td style="width: 750px">
                        <div id="PreList"></div>
                    </td>
                </tr>
                 </ContentTemplate>
                </asp:UpdatePanel> --%>
                <tr>
                    <td></td>
                    <td>
                        <span style="color:#AAAAAA">新建的促销信息默认为下线状态</span>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                         <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                        <div id="messageContent" runat="server" style="color:red;width:65%;"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel> 
                    </td>
                </tr> 
               
            </table>
        </li>
        <li class="button">
             <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server" >
                <ContentTemplate>
                <div id="dvBtnCreateList" >
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAddPromotion" runat="server" CssClass="btn primary" Text="添加" OnClientClick="SetContronListVal()" onclick="btnAdd_Click" />
                    <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" />
                </div>
             </ContentTemplate>
                </asp:UpdatePanel>
        </li>
      </ul>
    </div>
   </div>
<asp:HiddenField ID="hidCommonType" runat="server"/>
<asp:HiddenField ID="hidChkCommonType" runat="server"/>
<asp:HiddenField ID="hidHotelID" runat="server"/>
<asp:HiddenField ID="hidCommonList" runat="server"/>
<asp:HiddenField ID="hidUserGroupList" runat="server"/>
<asp:HiddenField ID="img_photo" runat="server"/>
<asp:HiddenField ID="image_src" runat="server"/>
</asp:Content>