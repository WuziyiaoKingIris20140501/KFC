<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.master"
    CodeFile="HotelInfoManager.aspx.cs" Title="酒店基础信息管理" Inherits="HotelInfoManager" %>

<%@ Register Assembly="HotelVp.ServiceControl" Namespace="HotelVp.ServiceControl"
    TagPrefix="cc1" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/AutoCptControl.ascx" TagName="WebAutoComplete"
    TagPrefix="ac1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
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
            top: 40%;
            left: 40%;
            margin-left: -74px;
            margin-top: -14px;
            padding: 10px 10px 10px 50px;
            text-align: left;
            line-height: 27px;
            font-weight: bold;
            position: absolute;
            z-index: 2001;
        }
        .bgDiv2
        {
            opacity: 0.5;
            filter: alpha(opacity=50);
            background: #666666;
            z-index: 100;
            display: none;
            bottom: 0;
            left: 0;
            position: fixed;
            right: 0;
            top: 0;
        }
        .popupDiv2
        {
            width: 560px;
            height: 380px;
            position: absolute;
            padding: 1px;
            z-index: 10000;
            display: none;
            background-color: White;
            top: 25%;
            left: 40%;
            margin-left: -150px !important; /*FF IE7 该值为本身宽的一半 */
            margin-top: -50px !important; /*FF IE7 该值为本身高的一半*/
            margin-left: 0px;
            margin-top: 0px;
            position: fixed !important; /*FF IE7*/
            position: absolute; /*IE6*/
            _top: expression(eval(document.compatMode &&
                    document.compatMode=='CSS1Compat') ?
                    documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
                    document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2); /*IE5 IE5.5*/
            _left: expression(eval(document.compatMode &&
                    document.compatMode=='CSS1Compat') ?
                    documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 :/*IE6*/
                    document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2); /*IE5 IE5.5*/
        }
    </style>
    <script language="javascript" type="text/javascript">
        function selectChange(obj) {
            var dropDownList = document.getElementById("<%=ddpStatusList.ClientID %>"); //获取DropDownList控件
            var dropDownListValue = dropDownList.options[dropDownList.selectedIndex].value; //获取选择项的值
            if (dropDownListValue == "0") {
                document.getElementById("<%=ddpStatusListRemark.ClientID %>").style.display = '';
            } else {
                document.getElementById("<%=ddpStatusListRemark.ClientID %>").style.display = 'none';
            }
        }

        function selectChangeByNewHotel(obj) {
            var dropDownList = document.getElementById("<%=ddpOnline.ClientID %>"); //获取DropDownList控件
            var dropDownListValue = dropDownList.options[dropDownList.selectedIndex].value; //获取选择项的值
            if (dropDownListValue == "0") {
                document.getElementById("<%=ddpStatusListRemarkNew.ClientID %>").style.display = '';
            } else {
                document.getElementById("<%=ddpStatusListRemarkNew.ClientID %>").style.display = 'none';
            }
        }

        function ClearClickEvent() {
            document.getElementById("<%=lbHotelNM.ClientID%>").innerText = "";
            $("#lbHotelNM").html("");
            document.getElementById("<%=hidHotelID.ClientID%>").value = "";
            document.getElementById("<%=txtHotelNM.ClientID%>").value = "";
            document.getElementById("<%=txtHotelNMEN.ClientID%>").value = "";


            document.getElementById("<%=txtUHotelPN.ClientID%>").value = "";
            document.getElementById("<%=txtUTotalRooms.ClientID%>").value = "";
            document.getElementById("<%=txtUHotelJP.ClientID%>").value = "";
            document.getElementById("<%=txtUZip.ClientID%>").value = "";
            document.getElementById("<%=txtUPriceLow.ClientID%>").value = "";
            document.getElementById("<%=txtUContactNameZh.ClientID%>").value = "";
            document.getElementById("<%=txtUHotelFax.ClientID%>").value = "";
            document.getElementById("<%=txtUContactPhone.ClientID%>").value = "";
            document.getElementById("<%=txtUHotelTel.ClientID%>").value = "";
            document.getElementById("<%=txtUContactEmail.ClientID%>").value = "";
            document.getElementById("<%=txtURemark.ClientID%>").value = "";

            document.getElementById("<%=ddpStatusList.ClientID%>").selectedIndex = 0;

            document.getElementById("<%=dpSalesStart.ClientID%>").value = "";
            document.getElementById("<%=dpSalesEnd.ClientID%>").value = "";
            document.getElementById("<%=dpBalStart.ClientID%>").value = "";
            document.getElementById("<%=dpBalEnd.ClientID%>").value = "";

            document.getElementById("<%=ddpPriceCode.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddpBalType.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=dpInDTStart.ClientID%>").value = "";
            document.getElementById("<%=dpInDTEnd.ClientID%>").value = "";
            document.getElementById("<%=txtBalVal.ClientID%>").value = "";
            document.getElementById("<%=chkIsPushFog.ClientID%>").checked = false;
            document.getElementById("<%=ddpRoomList.ClientID%>").selectedIndex = 0;
            document.getElementById("<%=ddpUStarRating.ClientID%>").selectedIndex = 0;

            document.getElementById("wctUCity").value = "";
            document.getElementById("wctUCity").text = "";
            document.getElementById("<%=dpOpenDate.ClientID%>").value = "";
            document.getElementById("<%=dpRepairDate.ClientID%>").value = "";
            document.getElementById("<%=txtAddress.ClientID%>").value = "";
            document.getElementById("<%=txtWebSite.ClientID%>").value = "";
            document.getElementById("<%=txtPhone.ClientID%>").value = "";
            document.getElementById("<%=txtFax.ClientID%>").value = "";
            document.getElementById("<%=txtContactPer.ClientID%>").value = "";
            document.getElementById("<%=txtContactEmail.ClientID%>").value = "";

            document.getElementById("<%=txtLongitude.ClientID%>").value = "";
            document.getElementById("<%=txtLatitude.ClientID%>").value = "";

            document.getElementById("<%=txtBDLongitude.ClientID%>").value = "";
            document.getElementById("<%=txtBDLatitude.ClientID%>").value = "";

            document.getElementById("<%=txtSimpleDescZh.ClientID%>").value = "";
            document.getElementById("<%=txtDescZh.ClientID%>").value = "";
            document.getElementById("<%=MessageContent.ClientID%>").innerText = "";
            document.getElementById("<%=hidFogStatus.ClientID%>").value = "";
            document.getElementById("<%=lbFogStatus.ClientID%>").innerText = "";
            ChangeBtnClick('1');
            document.getElementById("<%=dvLink.ClientID%>").style.display = "none";
            document.getElementById("<%=dvEvlGrid.ClientID%>").style.display = "none";
        }

        function SetControlEnable(val) {
            document.getElementById("<%=txtHotelNM.ClientID%>").disabled = val;
            document.getElementById("<%=txtHotelNMEN.ClientID%>").disabled = val;

            document.getElementById("<%=txtUHotelPN.ClientID%>").disabled = val;
            document.getElementById("<%=txtUTotalRooms.ClientID%>").disabled = val;
            document.getElementById("<%=txtUHotelJP.ClientID%>").disabled = val;
            document.getElementById("<%=txtUZip.ClientID%>").disabled = val;
            document.getElementById("<%=txtUPriceLow.ClientID%>").disabled = val;
            document.getElementById("<%=txtUContactNameZh.ClientID%>").disabled = val;
            document.getElementById("<%=txtUHotelFax.ClientID%>").disabled = val;
            document.getElementById("<%=txtUContactPhone.ClientID%>").disabled = val;
            document.getElementById("<%=txtUHotelTel.ClientID%>").disabled = val;
            document.getElementById("<%=txtUContactEmail.ClientID%>").disabled = val;
            document.getElementById("<%=txtURemark.ClientID%>").disabled = val;

            document.getElementById("<%=ddpStatusList.ClientID%>").disabled = val;
            document.getElementById("<%=ddpUStarRating.ClientID%>").disabled = val;
            document.getElementById("wctUCity").disabled = val;
            document.getElementById("<%=dpOpenDate.ClientID%>").disabled = val;
            document.getElementById("<%=dpRepairDate.ClientID%>").disabled = val;
            document.getElementById("<%=txtAddress.ClientID%>").disabled = val;
            document.getElementById("<%=txtWebSite.ClientID%>").disabled = val;
            document.getElementById("<%=txtPhone.ClientID%>").disabled = val;
            document.getElementById("<%=txtFax.ClientID%>").disabled = val;
            document.getElementById("<%=txtLongitude.ClientID%>").disabled = val;
            document.getElementById("<%=txtLatitude.ClientID%>").disabled = val;
            document.getElementById("<%=txtBDLongitude.ClientID%>").disabled = val;
            document.getElementById("<%=txtBDLatitude.ClientID%>").disabled = val;

            document.getElementById("<%=txtContactPer.ClientID%>").disabled = val;
            document.getElementById("<%=txtContactEmail.ClientID%>").disabled = val;
            document.getElementById("<%=txtSimpleDescZh.ClientID%>").disabled = val;
            document.getElementById("<%=txtDescZh.ClientID%>").disabled = val;

            document.getElementById("<%=dpSalesStart.ClientID%>").disabled = val;
            document.getElementById("<%=dpSalesEnd.ClientID%>").disabled = val;
            document.getElementById("<%=dpBalStart.ClientID%>").disabled = val;
            document.getElementById("<%=dpBalEnd.ClientID%>").disabled = val;
            document.getElementById("<%=ddpRoomList.ClientID%>").disabled = val;

            document.getElementById("<%=ddpPriceCode.ClientID%>").disabled = val;
            document.getElementById("<%=dpInDTStart.ClientID%>").disabled = val;
            document.getElementById("<%=dpInDTEnd.ClientID%>").disabled = val;
            document.getElementById("<%=ddpBalType.ClientID%>").disabled = val;
            document.getElementById("<%=txtBalVal.ClientID%>").disabled = val;
            document.getElementById("<%=chkIsPushFog.ClientID%>").disabled = val;
        }

        function invokeOpenList() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            BtnCompleteStyle();
        }

        function invokeCloseList() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function invokeOpenUList() {
            document.getElementById("popupDiv3").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv3");
            bgObj.style.display = "block";
            BtnCompleteStyle();
        }

        function invokeCloseUList() {
            document.getElementById("popupDiv3").style.display = "none";
            document.getElementById("bgDiv3").style.display = "none";
        }

        function SetChkControlEnable(val, btn) {
            document.getElementById("btnChkMap").disabled = val;
            document.getElementById("btnUKeyWords").disabled = val;
            //            if (btn == "1") {
            //                SetSelectEnable('');
            //            }
            //            else if (btn == "2") {
            //                SetSelectEnable('disabled');
            //            }

            SetSalesControlVal();
        }

        function BingControlValEnable(val, btn) {
            document.getElementById("btnChkMap").disabled = val;
            document.getElementById("btnUKeyWords").disabled = val;

            //            if (btn == "1") {
            //                SetSelectEnable('');
            //            }
            //            else if (btn == "2") {
            //                SetSelectEnable('disabled');
            //            }

            SetSalesControlVal();
        }

        function SetSalesControlVal() {
            document.getElementById("wctSales").value = document.getElementById('<%=hidSalesID.ClientID%>').value;
            document.getElementById("wctSales").text = document.getElementById('<%=hidSalesID.ClientID%>').value;

            document.getElementById("wctBed").value = document.getElementById("<%=hidBedCD.ClientID%>").value;
            document.getElementById("wctBed").text = document.getElementById("<%=hidBedCD.ClientID%>").value;

            document.getElementById("wctUCity").value = document.getElementById("<%=hidUCityID.ClientID%>").value;
            document.getElementById("wctUCity").text = document.getElementById("<%=hidUCityID.ClientID%>").value;

            document.getElementById("wctHotelGroupCode").value = document.getElementById("<%=hidHotelGroup.ClientID%>").value;
            document.getElementById("wctHotelGroupCode").text = document.getElementById("<%=hidHotelGroup.ClientID%>").value;


            if (document.getElementById("<%=hidSaveHotelID.ClientID%>").value != null) {
                document.getElementById("wctHotel").value = document.getElementById("<%=hidSaveHotelID.ClientID%>").value;
                document.getElementById("wctHotel").text = document.getElementById("<%=hidSaveHotelID.ClientID%>").value;
            }

            GetDDPChangeStyle();

            var strs = document.getElementById("<%=hidUKeyWords.ClientID%>").value.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    document.getElementById("<%=dvUKeyWords.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + strs[i] + "</span>&nbsp;&nbsp;";
                }
            }
        }

        function ChangeModifyBtnClick(arg) {

        }

        function ChangeBtnClick(arg) {
            if (arg == "0") {
                document.getElementById("<%=divBtnList.ClientID%>").style.display = "";
                document.getElementById("<%=dvSales.ClientID%>").style.display = "";
                document.getElementById("<%=dvBalSearch.ClientID%>").style.display = "";
                document.getElementById("<%=dvBalAdd.ClientID%>").style.display = "";
                document.getElementById("<%=dvHotelEX.ClientID%>").style.display = "";
            }
            else {
                document.getElementById("<%=divBtnList.ClientID%>").style.display = "none";
                document.getElementById("<%=dvSales.ClientID%>").style.display = "none";
                document.getElementById("<%=dvBalSearch.ClientID%>").style.display = "none";
                document.getElementById("<%=dvBalAdd.ClientID%>").style.display = "none";
                document.getElementById("<%=dvHotelEX.ClientID%>").style.display = "none";
            }
        }

        function SetUCityID() {
            document.getElementById("<%=hidUCityID.ClientID%>").value = document.getElementById("wctUCity").value;

            document.getElementById("<%=hidHotelGroup.ClientID%>").value = document.getElementById("wctHotelGroupCode").value;
        }

        //        function SetSelectEnable(arg) {
        //            document.getElementById("<%=btnSelect.ClientID%>").disabled = arg;
        //        }

        function PopupHotelMap() {
            var lat = document.getElementById("<%=txtLatitude.ClientID%>").value;
            var lon = document.getElementById("<%=txtLongitude.ClientID%>").value;
            var hotel = encodeURI(document.getElementById("<%=lbHotelNM.ClientID%>").innerText);
            var time = new Date();
            window.showModalDialog("HotelInfoManagerMap.aspx?latitude=" + lat + "&longitude=" + lon + "&hotelname=" + hotel + "&time=" + time, obj, "dialogWidth=1000px;dialogHeight=800px");
        }

        function BtnSelectSales() {
            document.getElementById("<%=hidSalesID.ClientID%>").value = document.getElementById("wctSales").value;
        }

        function SaveHotelID() {
            document.getElementById("<%=hidSaveHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
        }

        function OpenIssuePage() {
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
            window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=1&RID=' + document.getElementById("<%=hidHotelID.ClientID%>").value + "&time=" + time, null, fulls);
        }

        function PopupArea() {
            var cityName = document.getElementById("wctUCity").value;
            if (cityName.indexOf("]") >= 0) {
                cityName = cityName.substring(1, cityName.indexOf("]"));
            }

            var hotelId = document.getElementById("<%=hidHotelID.ClientID %>").value;
            var time = new Date();
            var retunValue = window.showModalDialog("HotelBusinessCircle.aspx?type=page&city=" + cityName + "&hotelId=" + hotelId + "&time=" + time, "", "dialogWidth=800px;dialogHeight=400px");

            if (retunValue != "undefinded" && retunValue != null) {
                document.getElementById("<%=dvUserGroupList.ClientID %>").innerHTML = "";
                var strs = new Array();
                strs = retunValue.split("><");
                for (i = 0; i < strs.length; i++) {
                    //var indexValue = strs[i].indexOf("value");
                    //var indexK = strs[i].indexOf("");
                    //var subValue = strs[i].substring(indexValue + 7, indexK);
                    var indexValue = strs[i].indexOf("value");
                    var subValue = strs[i].substring(indexValue + 7);
                    subValue = subValue.substring(0, subValue.indexOf("\""));
                    document.getElementById("<%=dvUserGroupList.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + subValue + "</span>&nbsp;&nbsp;&nbsp;";
                }
                document.getElementById("<%=dvUserGroupList.ClientID %>").innerHTML += "  " + "<input type='button' id='Button122' value='修改' class='btn primary' onclick='PopupArea()' />";
            }
        }

        function SetRoomActionType(arg) {
            document.getElementById("<%=hidRoomACT.ClientID%>").value = arg;
        }

        function SetBedTypeType() {
            document.getElementById("<%=hidBedCD.ClientID%>").value = document.getElementById("wctBed").value;
        }

        function AddRoomStyle(arg) {
            document.getElementById("<%=MessageContent.ClientID%>").innerText = "";
            if (arg == "0") {
                document.getElementById("<%=dvSaveRoom.ClientID%>").style.display = "none";
                document.getElementById("<%=dvRoomGrid.ClientID%>").style.display = "";
            }
            else {
                document.getElementById("<%=dvSaveRoom.ClientID%>").style.display = "";
                document.getElementById("<%=dvRoomGrid.ClientID%>").style.display = "none";
            }

            document.getElementById("<%=dvRoomHis.ClientID%>").style.display = "none";
            document.getElementById("<%=dvRoomCD.ClientID%>").style.display = "";
            document.getElementById("<%=lbRoomCD.ClientID%>").innerText = "";
            document.getElementById("<%=lbRoomCD.ClientID%>").innerHTML = "";

            if (document.getElementById("MainContent_cddpRoomNm_ctl00$MainContent$cddpRoomNm") != null) {
                document.getElementById("MainContent_cddpRoomNm_ctl00$MainContent$cddpRoomNm").value = "";
            }

            document.getElementById("<%=ddtbKeyVal.ClientID%>").value = "";
            document.getElementById("wctBed").value = "";
            document.getElementById("wctBed").text = "";
            document.getElementById("<%=txtRoomArea.ClientID%>").value = "";
            document.getElementById("<%=ddlRoomPer.ClientID%>").selectedIndex = 0;

            SetRadioBtnListVal("<%=rdlRoomStatus.ClientID%>");
            SetRadioBtnListVal("<%=rdlWlan.ClientID%>");
            SetRadioBtnListVal("<%=rdlGuesType.ClientID%>");
            SetRadioBtnListVal("<%=rdlWindow.ClientID%>");
            SetRadioBtnListVal("<%=rdlSmoke.ClientID%>");
        }

        function PopupUpdateArea(arg) {
            document.getElementById("<%=hidRoomCD.ClientID%>").value = arg;
            document.getElementById("<%=hidRoomACT.ClientID%>").value = "0";
            document.getElementById("<%=btnLdRoomData.ClientID%>").click();
        }

        function SetRadioBtnListVal(arg) {
            var vRbtid = document.getElementById(arg);
            //得到所有radio
            var vRbtidList = vRbtid.getElementsByTagName("INPUT");
            if (vRbtidList.length > 0) {
                vRbtidList[0].checked = true;
            }
        }

        function SetPrRoomStyle(arg) {
            document.getElementById("<%=MessageContent.ClientID%>").innerText = "";
            if (arg == "0") {
                document.getElementById("<%=dvPrRoom.ClientID%>").style.display = "none";
                document.getElementById("<%=dvPrRoomGrid.ClientID%>").style.display = "";
            }
            else {
                document.getElementById("<%=dvPrRoom.ClientID%>").style.display = "";
                document.getElementById("<%=dvPrRoomGrid.ClientID%>").style.display = "none";
            }

            var vRbtid = document.getElementById("<%=rbtlPriceCD.ClientID%>");
            //得到所有radio
            var vRbtidList = vRbtid.getElementsByTagName("INPUT");
            if (vRbtidList.length > 0) {
                vRbtidList[0].checked = true;
                vRbtidList[0].disabled = "";
            }

            var vpRbtid = document.getElementById("<%=rbtlPrSt.ClientID%>");
            //得到所有radio
            var vpRbtidList = vpRbtid.getElementsByTagName("INPUT");
            if (vpRbtidList.length > 0) {
                vpRbtidList[0].checked = true;
            }

            var vpChkid = document.getElementById("<%=chklRooms.ClientID%>");
            if (vpChkid != null) {
                var vpChkidList = vpChkid.getElementsByTagName("INPUT");
                if (vpChkidList != null && vpChkidList.length > 0) {
                    for (var i = 0; i < vpChkidList.length; i++) {
                        vpChkidList[i].checked = false;
                    }
                }
            }
            document.getElementById("<%=chkUpdatePLan.ClientID%>").checked = false;

            //            document.getElementById("<%=rbtlPriceCD.ClientID%>").disabled = "disabled"; 
        }

        function PopupUpdatePR(arg, st) {
            document.getElementById("<%=hidPRCD.ClientID%>").value = arg;
            document.getElementById("<%=hidPRST.ClientID%>").value = st;
            document.getElementById("<%=hidPRRoomACT.ClientID%>").value = "0";
            document.getElementById("<%=btnLdPRRoomData.ClientID%>").click();
        }

        function SetPRRoomActionType(arg) {
            document.getElementById("<%=hidPRRoomACT.ClientID%>").value = arg;
            if (arg == "1") {
                document.getElementById("<%=dvPrPlan.ClientID%>").style.display = "none";
            }
        }

        function ChangeModel() {
            document.getElementById("<%=dvCRHotel.ClientID%>").style.display = "";
            document.getElementById("<%=dvUDHotel.ClientID%>").style.display = "none";
            document.getElementById("<%=hidModel.ClientID%>").value = "0";
            document.getElementById("<%=lbHotelNM.ClientID%>").innerText = "";
            document.getElementById("<%=MessageContent.ClientID%>").innerText = "";
            document.getElementById("<%=txtPriceLow.ClientID%>").value = "100";
            document.getElementById("<%=ddpOnline.ClientID%>").selectedIndex = 1;
            document.getElementById("MainContent_ddpStatusListRemarkNew").style.display = "none"; //下线原因
            document.getElementById("wctHotel").value = "";
            document.getElementById("wctHotel").text = "";
        }

        function PopupMapArea() {
            var obj = new Object();
            var time = new Date();
            var cityName = encodeURI(document.getElementById("<%=txtCAddress.ClientID%>").value);
            var retunValue = window.showModalDialog("GoogleMapIn.aspx?lng=" + document.getElementById("<%=txtCLongitude.ClientID%>").value + "&lat=" + document.getElementById("<%=txtCLatitude.ClientID%>").value + "&srcadd=" + cityName + "&time=" + time, obj, "dialogWidth=900px;dialogHeight=650px");
            if (retunValue) {
                document.getElementById("<%=retLgVal.ClientID%>").value = retunValue;
                var comName = new Array();
                comName = retunValue.split(","); //字符分割
                if (comName.length > 0) {
                    document.getElementById("<%=txtCLongitude.ClientID%>").value = comName[0];
                    document.getElementById("<%=txtCLatitude.ClientID%>").value = comName[1];
                    document.getElementById("<%=txtCLongitude.ClientID%>").text = comName[0];
                    document.getElementById("<%=txtCLatitude.ClientID%>").text = comName[1];

                    AJCBDlonglatTude(comName[0], comName[1]);
                }
            }
        }

        function AJCBDlonglatTude(Longitude, Latitude) {
            $.ajax({
                contentType: "application/json",
                url: "HotelInfoManager.aspx/SetBDlonglatTude",
                type: "POST",
                dataType: "json",
                data: "{Longitude:'" + Longitude + "',Latitude:'" + Latitude + "'}",
                success: function (data) {
                    var d = jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        document.getElementById("<%=txtCBDLongitude.ClientID%>").value = d[i].BDLongitude;
                        document.getElementById("<%=txtCBDLatitude.ClientID%>").value = d[i].BDLatitude;
                        document.getElementById("<%=txtCBDLongitude.ClientID%>").text = d[i].BDLongitude;
                        document.getElementById("<%=txtCBDLatitude.ClientID%>").text = d[i].BDLatitude;
                    });
                },
                error: function (json) {
                }
            });
        }

        function PopupUMapArea() {
            var obj = new Object();
            var time = new Date();
            var cityName = encodeURI(document.getElementById("<%=txtAddress.ClientID%>").value);
            var retunValue = window.showModalDialog("GoogleMapIn.aspx?lng=" + document.getElementById("<%=txtLongitude.ClientID%>").value + "&lat=" + document.getElementById("<%=txtLatitude.ClientID%>").value + "&srcadd=" + cityName + "&time=" + time, obj, "dialogWidth=900px;dialogHeight=650px");
            if (retunValue) {
                document.getElementById("<%=retLgVal.ClientID%>").value = retunValue;
                var comName = new Array();
                comName = retunValue.split(","); //字符分割
                if (comName.length > 0) {
                    document.getElementById("<%=txtLongitude.ClientID%>").value = comName[0];
                    document.getElementById("<%=txtLatitude.ClientID%>").value = comName[1];
                    document.getElementById("<%=txtLongitude.ClientID%>").text = comName[0];
                    document.getElementById("<%=txtLatitude.ClientID%>").text = comName[1];

                    AJUBDlonglatTude(comName[0], comName[1]);
                }
            }
        }

        function AJUBDlonglatTude(Longitude, Latitude) {
            $.ajax({
                contentType: "application/json",
                url: "HotelInfoManager.aspx/SetBDlonglatTude",
                type: "POST",
                dataType: "json",
                data: "{Longitude:'" + Longitude + "',Latitude:'" + Latitude + "'}",
                success: function (data) {
                    var d = jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        document.getElementById("<%=txtBDLongitude.ClientID%>").value = d[i].BDLongitude;
                        document.getElementById("<%=txtBDLatitude.ClientID%>").value = d[i].BDLatitude;
                        document.getElementById("<%=txtBDLongitude.ClientID%>").text = d[i].BDLongitude;
                        document.getElementById("<%=txtBDLatitude.ClientID%>").text = d[i].BDLatitude;
                    });
                },
                error: function (json) {
                }
            });
        }

        function PopupBussArea() {
            var cityName = document.getElementById("wctCity").value;
            document.getElementById("<%=dvAddBuss.ClientID%>").innerText = "";
            if (cityName.length == 0) {
                document.getElementById("<%=dvAddBuss.ClientID%>").innerText = "无法添加商圈，请先选择城市！";
                return;
            }
            if (cityName.indexOf("]") >= 0) {
                cityName = cityName.substring(1, cityName.indexOf("]"));
            }
            var argList = encodeURI(document.getElementById("<%=hidBussList.ClientID%>").value);
            var time = new Date();
            var retunValue = window.showModalDialog("CBusinessCircle.aspx?city=" + cityName + "&argList=" + argList + "&time=" + time, "", "dialogWidth=800px;dialogHeight=400px");
            if (retunValue != "undefinded" && retunValue != null) {
                document.getElementById("<%=hidBussList.ClientID %>").value = retunValue;
                document.getElementById("<%=dvAreaList.ClientID %>").innerHTML = "";
                var strs = new Array();
                strs = retunValue.split(",");
                for (i = 0; i < strs.length; i++) {
                    if (strs[i] != "") {
                        document.getElementById("<%=dvAreaList.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + strs[i] + "</span>&nbsp;&nbsp;";
                    }
                }
            }
        }

        function PopupUKeyWordArea() {
            invokeOpenUList();

            document.getElementById("<%=dvUKWList.ClientID%>").innerHTML = "";
            document.getElementById("<%=txtUKW.ClientID%>").value = "";
            document.getElementById("<%=txtUKW.ClientID%>").text = "";

            var board = document.getElementById("<%=dvUKWList.ClientID%>");
            var strs = document.getElementById("<%=hidUKeyWords.ClientID%>").value.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    var idVal = strs[i];
                    var dpValue = strs[i] + "   ";
                    var btnid = "btnCommon_" + idVal;
                    var e = document.createElement("input");
                    e.type = "button";
                    e.setAttribute("id", btnid);
                    e.value = dpValue;
                    e.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
                    board.appendChild(e);
                }
            }
            setCKWButtonClick("<%=dvUKWList.ClientID%>");
        }

        function PopupCKeyWordArea() {
            invokeOpenList();

            document.getElementById("<%=dvCKWList.ClientID%>").innerHTML = "";
            document.getElementById("<%=txtCKW.ClientID%>").value = "";
            document.getElementById("<%=txtCKW.ClientID%>").text = "";

            var board = document.getElementById("<%=dvCKWList.ClientID%>");
            var strs = document.getElementById("<%=hidCKeyWords.ClientID%>").value.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    var idVal = strs[i];
                    var dpValue = strs[i] + "   ";
                    var btnid = "btnCommon_" + idVal;
                    var e = document.createElement("input");
                    e.type = "button";
                    e.setAttribute("id", btnid);
                    e.value = dpValue;
                    e.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
                    board.appendChild(e);
                }
            }
            setCKWButtonClick("<%=dvCKWList.ClientID%>");
        }

        function setCKWButtonClick(arg) {
            var btnObject = document.getElementById(arg);
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

        function SaveCKWList() {
            document.getElementById("<%=dvCKeyWords.ClientID %>").innerHTML = "";
            var ckwboard = document.getElementById("<%=dvCKWList.ClientID%>");
            var ckwList = "";
            for (i = 0; i < ckwboard.childNodes.length; i++) {
                ckwList = ckwList + ckwboard.childNodes[i].id.substring(10) + ','
            }

            document.getElementById("<%=hidCKeyWords.ClientID%>").value = ckwList;
            var strs = document.getElementById("<%=hidCKeyWords.ClientID%>").value.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    document.getElementById("<%=dvCKeyWords.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + strs[i] + "</span>&nbsp;&nbsp;";
                }
            }
            invokeCloseList();
        }

        function AddCKWList() {
            var idVal = document.getElementById("<%=txtCKW.ClientID%>").value;

            var arr = idVal.match(/[,，]/g);
            if (arr != null && arr.length > 0) {
                document.getElementById("<%=dvCKWMsg.ClientID%>").innerHTML = "关键字不能包含符号，请修改！";
                return
            }

            var dpValue = document.getElementById("<%=txtCKW.ClientID%>").value;

            if (idVal.Trim() == "") {
                return;
            }

            var btnid = "btnCommon_" + idVal.Trim();

            if (btnid == "btnCommon_") {
                return;
            }

            if (document.getElementById(btnid) != null) {
                return;
            }
            var board = document.getElementById("<%=dvCKWList.ClientID%>");
            var e = document.createElement("input");
            e.type = "button";
            e.setAttribute("id", btnid);
            e.value = dpValue.Trim() + "   ";
            e.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
            e.onclick = function () {
                e.parentNode.removeChild(this);
            }
            board.appendChild(e);
        }

        function SetCHControlVal() {
            document.getElementById("<%=hidCityID.ClientID%>").value = document.getElementById("wctCity").value;

            document.getElementById("<%=hidHotelGroupNew.ClientID%>").value = document.getElementById("wctHotelGroupCodeNew").value;

        }

        function ReSetCreateVal() {
            document.getElementById("wctCity").value = document.getElementById('<%=hidCityID.ClientID%>').value;
            document.getElementById("wctCity").text = document.getElementById('<%=hidCityID.ClientID%>').value;


            document.getElementById("wctHotelGroupCodeNew").value = document.getElementById('<%=hidHotelGroupNew.ClientID%>').value;
            document.getElementById("wctHotelGroupCodeNew").text = document.getElementById('<%=hidHotelGroupNew.ClientID%>').value;

            var retunValue = document.getElementById("<%=hidBussList.ClientID %>").value;
            document.getElementById("<%=dvAreaList.ClientID %>").innerHTML = "";
            var strs = new Array();
            strs = retunValue.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    document.getElementById("<%=dvAreaList.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + strs[i] + "</span>&nbsp;&nbsp;";
                }
            }

            document.getElementById("<%=dvCKeyWords.ClientID %>").innerHTML = "";
            var strs = document.getElementById("<%=hidCKeyWords.ClientID%>").value.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    document.getElementById("<%=dvCKeyWords.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + strs[i] + "</span>&nbsp;&nbsp;";
                }
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

        function BtnCERCompleteStyle() {
            BtnCompleteStyle()
            document.getElementById("<%=dvCKeyWords.ClientID %>").innerHTML = "";
            var strs = document.getElementById("<%=hidCKeyWords.ClientID%>").value.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    document.getElementById("<%=dvCKeyWords.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + strs[i] + "</span>&nbsp;&nbsp;";
                }
            }
        }

        function GetDDPChangeStyle() {
            t = document.getElementById("<%=gridViewRather.ClientID%>")
            if (t == null) {
                return;
            }
            var cellNum = 2 //第3列
            for (i = 1; i < t.rows.length; i++) {
                sels = t.rows[i].cells[cellNum - 1].getElementsByTagName("select")[0];
                inputs = t.rows[i].cells[cellNum].getElementsByTagName("INPUT")[0];

                if (0 == sels.selectedIndex) {
                    inputs.disabled = "disabled";
                    inputs.value = "";
                }
                else {
                    inputs.disabled = "";
                }
            }
        }

        function SaveUKWList() {
            document.getElementById("<%=dvUKeyWords.ClientID %>").innerHTML = "";
            var ckwboard = document.getElementById("<%=dvUKWList.ClientID%>");
            var ckwList = "";
            for (i = 0; i < ckwboard.childNodes.length; i++) {
                ckwList = ckwList + ckwboard.childNodes[i].id.substring(10) + ','
            }

            document.getElementById("<%=hidUKeyWords.ClientID%>").value = ckwList;
            var strs = document.getElementById("<%=hidUKeyWords.ClientID%>").value.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "") {
                    document.getElementById("<%=dvUKeyWords.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + strs[i] + "</span>&nbsp;&nbsp;";
                }
            }
            invokeCloseUList();
        }

        function AddUKWList() {
            var idVal = document.getElementById("<%=txtUKW.ClientID%>").value;
            var arr = idVal.match(/[,，]/g);
            if (arr != null && arr.length > 0) {
                document.getElementById("<%=dvUKWMsg.ClientID%>").innerHTML = "关键字不能包含符号，请修改！";
                return
            }

            var dpValue = document.getElementById("<%=txtUKW.ClientID%>").value;

            if (idVal.Trim() == "") {
                return;
            }

            var btnid = "btnCommon_" + idVal.Trim();

            if (btnid == "btnCommon_") {
                return;
            }

            if (document.getElementById(btnid) != null) {
                return;
            }
            var board = document.getElementById("<%=dvUKWList.ClientID%>");
            var e = document.createElement("input");
            e.type = "button";
            e.setAttribute("id", btnid);
            e.value = dpValue.Trim() + "   ";
            e.setAttribute("style", "margin-right:10px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
            e.onclick = function () {
                e.parentNode.removeChild(this);
            }
            board.appendChild(e);
        }


    </script>
    <div class="frame01" style="margin: 8px 14px 5px 14px;">
        <ul>
            <li class="title">酒店基础信息管理</li>
            <li>
                <table>
                    <tr>
                        <td align="left">
                            选择酒店：
                        </td>
                        <td align="left">
                            <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel"
                                AutoParent="HotelInfoManager.aspx?Type=hotel" />
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="选择" OnClientClick="SaveHotelID()"
                                OnClick="btnSelect_Click" />
                        </td>
                        <td align="right" style="width: 550px; margin-right: 10px">
                            <%--   --%>
                            <input type="button" id="btnClear" class="btn primary" value="新建酒店" onclick="ChangeModel();" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td align="left" colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td align="left">
                            酒店名称：
                        </td>
                        <td align="left" colspan="3">
                            <asp:Label ID="lbHotelNM" runat="server" Text="" />
                        </td>
                    </tr>
                </table>
            </li>
            <li>
                <div id="MessageContent" runat="server" style="color: red; width: 800px;">
                </div>
            </li>
        </ul>
    </div>
    <div id="bgDiv2" class="bgDiv2">
    </div>
    <div id="popupDiv2" class="popupDiv2">
        <div class="frame01" style="width: 99%; height: 99%; margin-left: 0px">
            <ul>
                <li class="title" style="text-align: left">添加搜索关键字</li>
                <li>
                    <table width="97%">
                        <tr>
                            <td align="right">
                                关键字：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCKW" runat="server" Width="250px" onclick="this.select()" />
                                &nbsp;&nbsp;
                                <input type="button" id="btnAddCKW" class="btn primary" value="添加" onclick="AddCKWList();" />
                            </td>
                        </tr>
                    </table>
                    <div id="dvCKWList" runat="server" style="width: 100%; height: 260px; overflow-y: auto">
                    </div>
                </li>
                <li style="text-align: right">
                    <div id="dvCKWMsg" runat="server" style="color: red; float: left">
                    </div>
                    <div style="float: right">
                        <input type="button" id="btnSaveCKW" class="btn primary" value="确定" onclick="SaveCKWList();" />&nbsp;&nbsp;
                        <input type="button" id="btnCKWClose" class="btn" value="取消" onclick="invokeCloseList();" />&nbsp;&nbsp;
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <div id="bgDiv3" class="bgDiv2">
    </div>
    <div id="popupDiv3" class="popupDiv2">
        <div class="frame01" style="width: 99%; height: 99%; margin-left: 0px">
            <ul>
                <li class="title" style="text-align: left">添加搜索关键字</li>
                <li>
                    <table width="97%">
                        <tr>
                            <td align="right">
                                关键字：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtUKW" runat="server" Width="250px" onclick="this.select()" />
                                &nbsp;&nbsp;
                                <input type="button" id="btnAddUKW" class="btn primary" value="添加" onclick="AddUKWList();" />
                            </td>
                        </tr>
                    </table>
                    <div id="dvUKWList" runat="server" style="width: 100%; height: 260px; overflow-y: auto">
                    </div>
                </li>
                <li style="text-align: right">
                    <div id="dvUKWMsg" runat="server" style="color: red; float: left">
                    </div>
                    <div style="float: right">
                        <input type="button" id="btnSaveUKW" class="btn primary" value="确定" onclick="SaveUKWList();" />&nbsp;&nbsp;
                        <input type="button" id="btnUKWClose" class="btn" value="取消" onclick="invokeCloseUList();" />&nbsp;&nbsp;
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <div style="margin: 5px 14px 5px 14px; display: none;" id="dvCRHotel" runat="server">
        <div id="ctabs" style="background: #FFFFFF; border: 0px solid #FFFFFF;">
            <ul style="background: #FFFFFF; border: 0px solid #FFFFFF;">
                <li><a href="#ctabs-1">新建酒店基础信息 </a></li>
            </ul>
            <div id="ctabs-1" style="border: 1px solid #D5D5D5;">
                <table cellspacing="2" cellpadding="1" width="95%">
                    <tr>
                        <td align="right" style="width: 11%">
                            酒店中文名：
                        </td>
                        <td>
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtCHotelNM" runat="server" Width="385px" MaxLength="1000" AutoPostBack="True"
                                    OnTextChanged="txtCHotelNM_TextChanged" /><font color="red">*</font>
                            </div>
                        </td>
                        <td align="right">
                            上下线状态：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddpOnline" CssClass="noborder_inactive" runat="server" Width="160px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddpStatusListRemarkNew" CssClass="noborder_inactive"
                                runat="server" Width="160px" />
                            <font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            酒店英文名：
                        </td>
                        <td>
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtHotelEN" runat="server" Width="150px" MaxLength="100" /></div>
                        </td>
                        <td align="right">
                            酒店拼音：
                        </td>
                        <td>
                            <asp:TextBox ID="txtHotelPN" runat="server" Width="150px" MaxLength="100" /><font
                                color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            房间数：
                        </td>
                        <td>
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtTotalRooms" runat="server" Width="150px" MaxLength="5" /><font
                                    color="red">*</font></div>
                        </td>
                        <td align="right">
                            酒店简拼：
                        </td>
                        <td>
                            <asp:TextBox ID="txtHotelJP" runat="server" Width="150px" MaxLength="100" /><font
                                color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            城市：
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <ac1:WebAutoComplete ID="wctCity" runat="server" CTLID="wctCity" CTLLEN="150px" AutoType="city"
                                            AutoParent="HotelInfoManager.aspx?Type=city" />
                                    </td>
                                    <td align="left">
                                        <font color="red">*</font>
                                    </td>
                                    <td align="right" style="width: 65px; display: none">
                                        行政区：
                                    </td>
                                    <td style="display: none">
                                        <asp:DropDownList ID="ddpAdministration" CssClass="noborder_inactive" runat="server"
                                            Width="153px" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            星级：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddpCStarRating" CssClass="noborder_inactive" runat="server"
                                Width="160px" />
                            <font color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            地址：
                        </td>
                        <td>
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtCAddress" runat="server" Width="385px" MaxLength="150" /><font
                                    color="red">*</font></div>
                        </td>
                        <td align="right">
                            开业时间：
                        </td>
                        <td>
                            <input id="dpCOpenDate" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            邮编：
                        </td>
                        <td>
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtZip" runat="server" Width="150px" MaxLength="10" /></div>
                        </td>
                        <td align="right">
                            最后装修日期：
                        </td>
                        <td>
                            <input id="dpCRepairDate" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            经纬度(谷歌)：
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCLongitude" runat="server" Width="150px" MaxLength="20" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCLatitude" runat="server" Width="150px" MaxLength="20" /><font
                                            color="red">*</font>&nbsp;
                                        <input type="button" id="btnChkCMap" class="btn primary" value="查询" onclick="PopupMapArea()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            最低安全价格：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPriceLow" runat="server" Width="150px" MaxLength="4" /><font
                                color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            经纬度(百度)：
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCBDLongitude" runat="server" Width="150px" MaxLength="20" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCBDLatitude" runat="server" Width="150px" MaxLength="20" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            搜索关键字：
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <div id="dvCKeyWords" runat="server">
                                        </div>
                                    </td>
                                    <td>
                                        <input type="button" id="btnCKeyWords" class="btn primary" value="添加" onclick="PopupCKeyWordArea();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            酒店商圈：
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <div id="dvAreaList" runat="server">
                                        </div>
                                    </td>
                                    <td>
                                        <%--<asp:TextBox ID="txtArea" runat="server" Width="300px" MaxLength="200"/>--%>
                                        <input type="button" id="btnAdd" class="btn primary" value="添加" onclick="PopupBussArea();" /><font
                                            color="red">*</font>
                                    </td>
                                    <td>
                                        <div id="dvAddBuss" runat="server" style="color: red;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            酒店销售：
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactNameZh" runat="server" Width="150px" MaxLength="100" /><font
                                color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            常规单传真：
                        </td>
                        <td>
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtHotelFax" runat="server" Width="150px" MaxLength="100" /><font
                                    color="red">*</font></div>
                        </td>
                        <td align="right">
                            酒店销售电话：
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactPhone" runat="server" Width="150px" MaxLength="30" /><font
                                color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            常规单电话：
                        </td>
                        <td>
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtHotelTel" runat="server" Width="150px" MaxLength="40" /><font
                                    color="red">*</font></div>
                        </td>
                        <td align="right">
                            酒店销售邮箱：
                        </td>
                        <td>
                            <asp:TextBox ID="txtCContactEmail" runat="server" Width="150px" MaxLength="100" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            酒店网址：
                        </td>
                        <td>
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtCWebSite" runat="server" Width="380px" MaxLength="200" /></div>
                        </td>
                        <%--<td align="right">FOG酒店上下线状态：</td>
                        <td>上线</td>--%>
                        <td align="right">
                            酒店集团：
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <uc1:WebAutoComplete ID="wctHotelGroupCodeNew" runat="server" CTLID="wctHotelGroupCodeNew"
                                            AutoType="hotelgroup" AutoParent="HotelInfoManager.aspx?Type=hotelgroup" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            是否为自签酒店:
                        </td>
                        <td>
                             <asp:DropDownList ID="ddlIsMyHotel" CssClass="noborder_inactive" runat="server"
                                Width="160px" >
                                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:DropDownList>
                            <font color="red">*</font>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            酒店一句话描述：
                        </td>
                        <td align="left" valign="top">
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtCSimpleDescZh" runat="server" TextMode="MultiLine" Width="380px"
                                    Height="70px" /><br />
                                <font color="red">*</font><span style="color: #AAAAAA">&nbsp;最多200个字符</span></div>
                        </td>
                        <td align="right" valign="top" style="display:none">
                            酒店详情：
                        </td>
                        <td align="left" valign="top" style="display:none">
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtCDescZh" runat="server" TextMode="MultiLine" Width="380px" Height="70px" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            小贴士：
                        </td>
                        <td align="left" valign="top">
                            <asp:GridView ID="gridViewCEvaluationList" runat="server" AutoGenerateColumns="False"
                                BackColor="White" CellPadding="4" CellSpacing="1" Width="400px" EmptyDataText=""
                                DataKeyNames="Content" CssClass="GView_BodyCSS" OnRowDeleting="gridViewCEvaluationList_RowDeleting">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCEvalist" Text='<%# Bind("Content") %>' runat="server" MaxLength="10"
                                                Width="320px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" DeleteText="删除">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            </asp:GridView>
                            <div id="dvLk" style="text-align: left">
                                <asp:Button ID="lkBtnCAdd" runat="server" CssClass="btn primary" Text="添加小贴士" OnClientClick="SetCHControlVal();"
                                    OnClick="lkBtnCAdd_Click" /><span style="color: #AAAAAA">&nbsp;每条小贴士最多10个中文字</span>
                            </div>
                        </td>
                        <td align="right" valign="top" style="display:none">
                            酒店备注：
                        </td>
                        <td align="left" valign="top" style="display:none">
                            <div style="margin-left: 3px">
                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="380px" Height="70px" /><br />
                                <span style="color: #AAAAAA">&nbsp;最多600个中文字</span></div>
                        </td>
                    </tr>
                </table>
                <table width="95%">
                    <tr>
                        <td>
                            <div id="dvCsave" style="text-align: left; margin-left: 40%">
                                <div id="background" class="pcbackground" style="display: none;">
                                </div>
                                <div id="progressBar" class="pcprogressBar" style="display: none;">
                                    数据加载中，请稍等...</div>
                                <asp:Button ID="btnCreateHL" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetCHControlVal();BtnLoadStyle();"
                                    OnClick="btnCreateHL_Click" />&nbsp;<font color="red">*</font><span style="color: #AAAAAA">为必填字段，请注意填写</span>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div style="margin: 5px 14px 5px 14px;" id="dvUDHotel" runat="server">
        <div id="tabs" style="background: #FFFFFF; border: 0px solid #FFFFFF;">
            <ul style="background: #FFFFFF; border: 0px solid #FFFFFF;">
                <li><a href="#tabs-1">酒店基础信息 </a></li>
                <li><a href="#tabs-2">酒店房型管理 </a></li>
                <li><a href="#tabs-3">酒店图片管理 </a></li>
                <li><a href="#tabs-4">酒店签约信息 </a></li>
                <li><a href="#tabs-5">价格代码信息 </a></li>
                <li><a href="#tabs-6">酒店结算信息 </a></li>
                <li><a href="#tabs-7">酒店执行信息 </a></li>
            </ul>
            <div id="tabs-1" style="border: 1px solid #D5D5D5;">
                <div>
                    <table cellspacing="2" cellpadding="1" width="95%">
                        <tr>
                            <td align="right" style="width: 11%">
                                酒店中文名：
                            </td>
                            <td>
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtHotelNM" runat="server" Width="385px" MaxLength="1000" AutoPostBack="True"
                                        OnTextChanged="txtHotelNM_TextChanged" /><font color="red">*</font>
                                </div>
                            </td>
                            <td align="right">
                                上下线状态：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server"
                                    Width="160px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddpStatusListRemark" CssClass="noborder_inactive"
                                    runat="server" Width="160px" />
                                <font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                酒店英文名：
                            </td>
                            <td>
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtHotelNMEN" runat="server" Width="150px" MaxLength="100" /></div>
                            </td>
                            <td align="right">
                                酒店拼音：
                            </td>
                            <td>
                                <asp:TextBox ID="txtUHotelPN" runat="server" Width="150px" MaxLength="100" /><font
                                    color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                房间数：
                            </td>
                            <td>
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtUTotalRooms" runat="server" Width="150px" MaxLength="5" /><font
                                        color="red">*</font></div>
                            </td>
                            <td align="right">
                                酒店简拼：
                            </td>
                            <td>
                                <asp:TextBox ID="txtUHotelJP" runat="server" Width="150px" MaxLength="100" /><font
                                    color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                城市：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <ac1:WebAutoComplete ID="wctUCity" runat="server" CTLID="wctUCity" CTLLEN="150px"
                                                AutoType="city" AutoParent="HotelInfoManager.aspx?Type=city" />
                                        </td>
                                        <td align="left">
                                            <font color="red">*</font>
                                        </td>
                                        <td align="right" style="width: 65px; display: none">
                                            行政区：
                                        </td>
                                        <td style="display: none">
                                            <asp:DropDownList ID="ddpUAdministration" CssClass="noborder_inactive" runat="server"
                                                Width="153px" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                星级：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddpUStarRating" CssClass="noborder_inactive" runat="server"
                                    Width="160px" />
                                <font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                地址：
                            </td>
                            <td>
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtAddress" runat="server" Width="385px" MaxLength="150" /><font
                                        color="red">*</font></div>
                            </td>
                            <td align="right">
                                开业时间：
                            </td>
                            <td>
                                <input id="dpOpenDate" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                邮编：
                            </td>
                            <td>
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtUZip" runat="server" Width="150px" MaxLength="10" /></div>
                            </td>
                            <td align="right">
                                最后装修日期：
                            </td>
                            <td>
                                <input id="dpRepairDate" class="Wdate" type="text" style="width: 150px" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                经纬度(谷歌)：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtLongitude" runat="server" Width="150px" MaxLength="20" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLatitude" runat="server" Width="150px" MaxLength="20" /><font
                                                color="red">*</font>&nbsp;
                                            <input type="button" id="btnChkMap" class="btn primary" value="查询" onclick="PopupUMapArea()" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                最低安全价格：
                            </td>
                            <td>
                                <asp:TextBox ID="txtUPriceLow" runat="server" Width="150px" MaxLength="4" /><font
                                    color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                经纬度(百度)：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtBDLongitude" runat="server" Width="150px" MaxLength="20" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBDLatitude" runat="server" Width="150px" MaxLength="20" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                搜索关键字：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <div id="dvUKeyWords" runat="server">
                                            </div>
                                        </td>
                                        <td>
                                            <input type="button" id="btnUKeyWords" class="btn primary" value="添加" onclick="PopupUKeyWordArea();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="display: none">
                                FOG酒店上下线状态：
                            </td>
                            <td style="display: none">
                                <asp:Label ID="lbFogStatus" runat="server" Text="" /><asp:HiddenField ID="hidFogStatus"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                酒店商圈：
                            </td>
                            <td>
                                <div id="dvUserGroupList" runat="server" style="margin-left: 3px">
                                </div>
                            </td>
                            <td align="right">
                                酒店销售：
                            </td>
                            <td>
                                <asp:TextBox ID="txtUContactNameZh" runat="server" Width="150px" MaxLength="100" /><font
                                    color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                常规单传真：
                            </td>
                            <td>
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtUHotelFax" runat="server" Width="150px" MaxLength="100" /><font
                                        color="red">*</font></div>
                            </td>
                            <td align="right">
                                酒店销售电话：
                            </td>
                            <td>
                                <asp:TextBox ID="txtUContactPhone" runat="server" Width="150px" MaxLength="30" /><font
                                    color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                常规单电话：
                            </td>
                            <td>
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtUHotelTel" runat="server" Width="150px" MaxLength="40" /><font
                                        color="red">*</font></div>
                            </td>
                            <td align="right">
                                酒店销售邮箱：
                            </td>
                            <td>
                                <asp:TextBox ID="txtUContactEmail" runat="server" Width="150px" MaxLength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                酒店网址：
                            </td>
                            <td>
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtWebSite" runat="server" Width="380px" MaxLength="200" /></div>
                            </td>
                            <td align="right">
                                酒店集团：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <uc1:WebAutoComplete ID="wctHotelGroupCode" runat="server" CTLID="wctHotelGroupCode"
                                                AutoType="hotelgroup" AutoParent="HotelInfoManager.aspx?Type=hotelgroup" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                是否为自签酒店:
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlUpdateIsMyHotel" CssClass="noborder_inactive" runat="server"
                                    Width="160px" >
                                        <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:DropDownList>
                                <font color="red">*</font>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                酒店一句话描述：
                            </td>
                            <td align="left" valign="top">
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtSimpleDescZh" runat="server" TextMode="MultiLine" Width="380px"
                                        Height="70px" /><br />
                                    <font color="red">*</font><span style="color: #AAAAAA">&nbsp;最多200个字符</span></div>
                            </td>
                            <td align="right" valign="top" style="display:none">
                                酒店详情：
                            </td>
                            <td align="left" valign="top" style="display:none">
                                <asp:TextBox ID="txtDescZh" runat="server" TextMode="MultiLine" Width="380px" Height="70px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                小贴士：
                            </td>
                            <td align="left" valign="top">
                                <div id="dvEvlGrid" runat="server" style="display: none;">
                                    <asp:GridView ID="gridViewEvaluationList" runat="server" AutoGenerateColumns="False"
                                        BackColor="White" CellPadding="4" CellSpacing="1" Width="400px" EmptyDataText=""
                                        DataKeyNames="Content" CssClass="GView_BodyCSS" OnRowDeleting="gridViewEvaluationList_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEvalist" Text='<%# Bind("Content") %>' runat="server" MaxLength="10"
                                                        Width="320px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="true" DeleteText="删除">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                        </Columns>
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <PagerStyle HorizontalAlign="Right" />
                                        <RowStyle CssClass="GView_ItemCSS" />
                                        <HeaderStyle CssClass="GView_HeaderCSS" />
                                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                    </asp:GridView>
                                </div>
                                <div id="dvLink" runat="server" style="display: none; text-align: left">
                                    <asp:Button ID="lkBtnAdd" runat="server" CssClass="btn primary" Text="添加小贴士" OnClientClick="SetUCityID();"
                                        OnClick="lkBtnAdd_Click" /><span style="color: #AAAAAA">&nbsp;每条小贴士最多10个中文字</span>
                                </div>
                            </td>
                            <td align="right" valign="top" style="display: none;">
                                酒店备注：
                            </td>
                            <td align="left" valign="top" style="display: none;">
                                <div style="margin-left: 3px">
                                    <asp:TextBox ID="txtURemark" runat="server" TextMode="MultiLine" Width="380px" Height="70px" /><br />
                                    <span style="color: #AAAAAA">&nbsp;最多600个中文字</span></div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="save" style="text-align: left; margin-left: 25%">
                    <%--<div id="divModify" runat="server">
                        <asp:Button ID="btnModify" runat="server" Width="80px" Height="20px" Text="修改" Visible="false" OnClientClick="ChangeBtnClick('0');SetControlEnable('');SetSelectEnable('disabled');" onclick="btnModify_Click" />
                    </div>--%>
                    <div id="divBtnList" runat="server">
                        <%-- <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
                        <asp:Button ID="btnFog" runat="server" CssClass="btn primary" Text="读取FOG信息" OnClick="btnReadFog_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetUCityID()"
                            OnClick="btnSave_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn primary" Text="取消编辑" OnClick="btnReset_Click" />&nbsp;&nbsp;
                        <input type="button" id="btnOpenIssue" class="btn primary" value="创建Issue单" onclick="OpenIssuePage();" />
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
            <div id="tabs-2" style="border: 1px solid #D5D5D5;">
                <div style="display: ;">
                    <div id="dvSaveRoom" runat="server" style="display: none">
                        <table>
                            <tr style="height: 40px">
                                <td>
                                    房型名称：
                                </td>
                                <td>
                                    <div style="float: left">
                                        <cc1:DropDownTextBox ID="cddpRoomNm" runat="server" Width="200px" Height="20px" MaxLength="30" />
                                    </div>
                                    <div style="float: right">
                                        <font color="red">*</font>
                                    </div>
                                </td>
                                <td style="padding-left: 20px">
                                    上下线状态：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdlRoomStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="上线" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="下线"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr style="height: 40px">
                                <td>
                                    房型代码：
                                </td>
                                <td>
                                    <div id="dvRoomCD" runat="server">
                                        <asp:TextBox ID="ddtbKeyVal" runat="server" Width="216px" MaxLength="40" /><font
                                            color="red">*</font>
                                    </div>
                                    <asp:Label ID="lbRoomCD" runat="server" Width="216px" MaxLength="40" />
                                </td>
                                <td style="padding-left: 20px">
                                    床 型：
                                </td>
                                <td>
                                    <div style="float: left">
                                        <uc1:WebAutoComplete ID="wctBed" CTLID="wctBed" runat="server" AutoType="bedtype"
                                            AutoParent="HotelInfoManager.aspx?Type=bedtype" />
                                    </div>
                                    <div style="float: right">
                                        <font color="red">*</font>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    入住人数：
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRoomPer" runat="server" Width="222px" />
                                    <font color="red">*</font>
                                </td>
                                <td style="padding-left: 20px">
                                    房型面积：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRoomArea" runat="server" Width="216px" MaxLength="20" /><font
                                        color="red">*</font><span style="color: #AAAAAA">&nbsp;平方米</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    宽带情况：
                                </td>
                                <td>
                                    <%--<asp:RadioButtonList ID="chklWLAN" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>--%>
                                    <asp:RadioButtonList ID="rdlWlan" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="无" Text="无宽带" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="有线" Text="有线&nbsp;&nbsp;"></asp:ListItem>
                                        <asp:ListItem Value="wifi" Text="WIFI&nbsp;&nbsp;"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="padding-left: 20px">
                                    是否外宾：
                                </td>
                                <td>
                                    <%--<asp:RadioButtonList ID="chklGuesType" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>--%>
                                    <asp:RadioButtonList ID="rdlGuesType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Text="无限制&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="只可内宾"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="只可外宾"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    窗户：
                                </td>
                                <td>
                                    <%--<asp:RadioButtonList ID="chklWindow" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>--%>
                                    <asp:RadioButtonList ID="rdlWindow" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Text="无窗&nbsp;&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="有窗&nbsp;&nbsp;"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="部分有窗"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="padding-left: 20px">
                                    是否无烟：
                                </td>
                                <td>
                                    <%--<asp:RadioButtonList ID="chklSmoke" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>--%>
                                    <asp:RadioButtonList ID="rdlSmoke" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Text="未区分&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="无烟房&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="无烟处理"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div id="dvRoomMessage" runat="server" style="color: red; width: 500px;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSaveRoom" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetBedTypeType();"
                                        OnClick="btnSaveRoom_Click" />
                                </td>
                                <td align="left">
                                    <input type="button" id="btnCancelRoom" class="btn" value="取消" onclick="AddRoomStyle('0');" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <div id="dvRoomHis" runat="server">
                            <asp:GridView ID="gridViewRoomHis" runat="server" AutoGenerateColumns="False" BackColor="White"
                                CellPadding="4" CellSpacing="1" PageSize="10" AllowPaging="True" Width="100%"
                                EmptyDataText="没有数据" DataKeyNames="ROOMNM" OnPageIndexChanging="gridViewRoomHis_PageIndexChanging"
                                CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:BoundField DataField="ROOMNM" HeaderText="房型名称">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BED" HeaderText="床型">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GUEST" HeaderText="入住人数">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ROOMAREA" HeaderText="房型面积">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="WLAN" HeaderText="宽带">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GUESTYPE" HeaderText="外宾">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="WINDOW" HeaderText="窗户">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NOSMOKE" HeaderText="是否无烟">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="STATUSDIS" HeaderText="上线状态">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UPDATETIME" HeaderText="操作时间">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UPDATEUSER" HeaderText="操作人">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="dvRoomGrid" runat="server">
                        <div id="dvbtnRoom" runat="server">
                            <input type="button" id="btnAddRoom" class="btn primary" value="添加房型" onclick="SetRoomActionType('1');AddRoomStyle('1');" />
                        </div>
                        <div style="display: none">
                            <asp:Button ID="btnLdRoomData" runat="server" CssClass="btn primary" Text="Load房型"
                                OnClick="btnLdRoomData_Click" /></div>
                        <div class="frame01" style="margin-top: 15px; margin-left: 5px">
                            <ul>
                                <li class="title">房型信息列表</li>
                            </ul>
                        </div>
                        <div class="frame02" style="margin-left: 5px; width: 1150px; overflow: auto" id="dvGridRoom"
                            runat="server">
                            <asp:GridView ID="gridViewRoomList" runat="server" AutoGenerateColumns="False" BackColor="White"
                                CellPadding="4" CellSpacing="1" PageSize="15" AllowPaging="True" Width="100%"
                                EmptyDataText="没有数据" DataKeyNames="FTID" OnPageIndexChanging="gridViewRoomList_PageIndexChanging"
                                CssClass="GView_BodyCSS">
                                <Columns>
                                    <%--                            <asp:BoundField DataField="ROOMNM" HeaderText="房型名称">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>--%>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="房型名称">
                                        <ItemTemplate>
                                            <a href="#" id="afPopupArea" onclick="PopupUpdateArea('<%# DataBinder.Eval(Container.DataItem, "ROOMCD") %>')">
                                                <font color="blue">
                                                    <%# DataBinder.Eval(Container.DataItem, "ROOMNM")%></font></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ROOMCD" HeaderText="房型代码">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ROOMDIS" HeaderText="状态">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="WLAN" HeaderText="宽带">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UPDATEUSER" HeaderText="最后操作">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UPDATETIME" HeaderText="操作时间">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:BoundField>
                                    <%--<asp:CommandField ShowDeleteButton="True" HeaderText="操作" DeleteText="删除房型" >
                                <ItemStyle HorizontalAlign="Center" Width="8%" ForeColor="Blue"/>
                            </asp:CommandField>--%>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tabs-3" style="border: 1px solid #D5D5D5;">
                <div style="display: ;">
                    <div id="detailMessageContent" runat="server" style="color: red">
                        请选择酒店！
                    </div>
                    <iframe id="hotelInfoInlineImage" runat="server" border="0" scrolling="auto" width="100%"
                        height="750px" frameborder="0" style="color: White; border-width: 0px;"></iframe>
                </div>
            </div>
            <div id="tabs-4" style="border: 1px solid #D5D5D5;">
                <div>
                    <table>
                        <tr>
                            <td align="right">
                                销售人员：
                            </td>
                            <td align="left" colspan="4">
                                <uc1:WebAutoComplete ID="wctSales" CTLID="wctSales" runat="server" AutoType="sales"
                                    AutoParent="HotelInfoManager.aspx?Type=sales" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                合同日期：
                            </td>
                            <td align="left" colspan="4">
                                <input id="dpSalesStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpSalesEnd\')||\'2020-10-01\'}'})"
                                    runat="server" />
                                <input id="dpSalesEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpSalesStart\')}',maxDate:'2020-10-01'})"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="display: none;">
                                LM联系电话：
                            </td>
                            <td style="display: none;">
                                <asp:TextBox ID="txtPhone" runat="server" Width="300px" MaxLength="40" /><font color="red">*</font>
                            </td>
                            <td style="width: 50px; display: none;">
                            </td>
                            <td align="right" style="display: none;">
                                LM订单传真：
                            </td>
                            <td style="display: none;">
                                <asp:TextBox ID="txtFax" runat="server" Width="300px" MaxLength="20" /><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="display: none;">
                                LM联系人：
                            </td>
                            <td style="display: none;">
                                <asp:TextBox ID="txtContactPer" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td style="width: 50px; display: none;">
                            </td>
                            <td align="right" style="display: none;">
                                LM联系邮箱：
                            </td>
                            <td style="display: none;">
                                <asp:TextBox ID="txtContactEmail" runat="server" Width="300px" MaxLength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="height: 20px">
                            </td>
                        </tr>
                    </table>
                </div>
                <%--AutoPostBack="true" OnSelectedIndexChanged="ddpDiscount_SelectedIndexChanged"--%>
                <div style="margin-left: 5px">
                    <asp:GridView ID="gridViewRather" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CellPadding="4" CellSpacing="1" Width="70%" EmptyDataText="没有房型数据" DataKeyNames="ID"
                        OnRowDataBound="gridViewRather_RowDataBound" CssClass="GView_BodyCSS">
                        <Columns>
                            <asp:BoundField DataField="ROOMNM" HeaderText="房型名称">
                                <ItemStyle HorizontalAlign="Center" Width="30%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="合同折扣" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddpDiscount" runat="server" Width="90%" DataSource='<%# ddlDDpbind()%>'
                                        DataValueField="DISCOUNTCD" DataTextField="DISCOUNTDIS" onchange="GetDDPChangeStyle()">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="折扣" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDiscount" runat="server" Text='<%# Eval("DISCOUNTXT")%>' Width="90%"
                                        MaxLength="7"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                HeaderText="已绑定艺龙">
                                <ItemTemplate>
                                    <asp:Image ID="imgRoomPic" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "RBIMG") %>'
                                        ToolTip='<%# DataBinder.Eval(Container.DataItem, "RBMSG") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                    </asp:GridView>
                    <br />
                </div>
                <div id="dvSales" style="text-align: left; margin-left: 15px" runat="server">
                    <%--<asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
                    <asp:Button ID="btnSaveSales" runat="server" CssClass="btn primary" Text="保存" OnClientClick="BtnSelectSales()"
                        OnClick="btnSaveSales_Click" />
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
                <div class="frame01" style="margin-top: 15px; margin-left: 5px">
                    <ul>
                        <li class="title">合同变更历史</li>
                    </ul>
                </div>
                <div class="frame02" style="margin-left: 5px">
                    <asp:GridView ID="gridViewCSSalesList" runat="server" AutoGenerateColumns="False"
                        BackColor="White" AllowPaging="True" CellPadding="4" CellSpacing="1" Width="100%"
                        EmptyDataText="没有数据" DataKeyNames="ID" OnRowDataBound="gridViewCSSalesList_RowDataBound"
                        OnPageIndexChanging="gridViewCSSalesList_PageIndexChanging" PageSize="5" CssClass="GView_BodyCSS">
                        <Columns>
                            <asp:BoundField DataField="SALESNM" HeaderText="销售人员">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STARTDTIME" HeaderText="合同开始日期">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ENDDTIME" HeaderText="合同截止日期">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FAX" HeaderText="LM订单传真">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PER" HeaderText="LM联系人">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TEL" HeaderText="LM联系电话">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EML" HeaderText="LM联系邮箱">
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATEUSER" HeaderText="操作人">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATEDT" HeaderText="操作日期">
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                    </asp:GridView>
                </div>
            </div>
            <div id="tabs-5" style="border: 1px solid #D5D5D5;">
                <div style="display: ;">
                    <div id="dvPrRoom" runat="server" style="display: none">
                        <table>
                            <tr>
                                <td>
                                    价格代码类型：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtlPriceCD" runat="server" RepeatDirection="Horizontal"
                                        CellSpacing="8">
                                        <asp:ListItem Value="LMBAR2" Text="LMBAR2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="LMBAR" Text="LMBAR"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    价格代码状态：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtlPrSt" runat="server" RepeatDirection="Horizontal" CellSpacing="8">
                                        <asp:ListItem Value="1" Text="激活" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="非激活"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    关联房型：
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="chklRooms" runat="server" RepeatDirection="Vertical" RepeatColumns="6"
                                        RepeatLayout="Table" CellSpacing="8" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div id="prRoomMessage" runat="server" style="color: red; width: 500px;">
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="5" cellpadding="5">
                            <tr>
                                <td align="center">
                                    <div id="dvPrPlan" runat="server">
                                        <asp:CheckBox ID="chkUpdatePLan" runat="server" Text="更新价格计划" />
                                    </div>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnPrRoom" runat="server" CssClass="btn primary" Text="保存" OnClick="btnPrRoom_Click" />
                                </td>
                                <td align="left">
                                    <input type="button" id="btnPrRoomCal" class="btn" value="取消" onclick="SetPrRoomStyle('0');" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="dvPrRoomGrid" runat="server">
                        <div id="dvbtnPrRoom" runat="server">
                            <input type="button" id="btnAddPrRoom" class="btn primary" value="添加价格代码" onclick="SetPRRoomActionType('1');SetPrRoomStyle('1');" />
                        </div>
                        <div style="display: none">
                            <asp:Button ID="btnLdPRRoomData" runat="server" CssClass="btn primary" Text="Load价格代码房型"
                                OnClick="btnLdPRRoomData_Click" /></div>
                        <div class="frame01" style="margin-top: 15px; margin-left: 5px">
                            <ul>
                                <li class="title">价格代码信息列表</li>
                            </ul>
                        </div>
                        <div class="frame02" style="margin-left: 5px; width: 1150px; overflow: auto" id="Div7"
                            runat="server">
                            <asp:GridView ID="gridViewPrRoomList" runat="server" AutoGenerateColumns="False"
                                BackColor="White" CellPadding="4" CellSpacing="1" PageSize="15" AllowPaging="True"
                                Width="100%" EmptyDataText="没有数据" DataKeyNames="FTID" OnPageIndexChanging="gridViewPrRoomList_PageIndexChanging"
                                CssClass="GView_BodyCSS">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="价格代码">
                                        <ItemTemplate>
                                            <a href="#" id="afPopupUpdatePR" onclick="PopupUpdatePR('<%# DataBinder.Eval(Container.DataItem, "PRCD") %>','<%# DataBinder.Eval(Container.DataItem, "STAT") %>')">
                                                <font color="blue">
                                                    <%# DataBinder.Eval(Container.DataItem, "PRCD")%></font></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PRROOMDIS" HeaderText="状态">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UPDATETIME" HeaderText="最后修改日期">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UPDATEUSER" HeaderText="最后修改人">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle HorizontalAlign="Right" />
                                <RowStyle CssClass="GView_ItemCSS" />
                                <HeaderStyle CssClass="GView_HeaderCSS" />
                                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tabs-6" style="border: 1px solid #D5D5D5;">
                <div>
                    <table>
                        <tr>
                            <td align="right">
                                价格代码：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddpPriceCode" runat="server" Width="150px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddpPriceCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                选择房型：
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkHotelRoomList" runat="server" RepeatDirection="Vertical"
                                    RepeatColumns="8" RepeatLayout="Table" CellSpacing="8" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                入住时间：
                            </td>
                            <td>
                                <input id="dpInDTStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpInDTEnd\')||\'2020-10-01\'}'})"
                                    runat="server" />
                                <input id="dpInDTEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpInDTStart\')}',maxDate:'2020-10-01'})"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                佣金类型：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddpBalType" runat="server" Width="150px">
                                </asp:DropDownList>
                                &nbsp;&nbsp;值：<asp:TextBox ID="txtBalVal" runat="server" Width="80px" MaxLength="7" />&nbsp;&nbsp;<asp:CheckBox
                                    ID="chkIsPushFog" runat="server" Text="是否同步FOG" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                    </table>
                    <div id="dvBalAdd" runat="server">
                        <asp:Button ID="btnBalAdd" runat="server" CssClass="btn primary" Text="保存结算信息" OnClick="btnBalAdd_Click" />
                    </div>
                </div>
                <div class="frame01" style="margin-top: 15px; margin-left: 5px">
                    <ul>
                        <li class="title">结算信息快速查询</li>
                        <li>
                            <table>
                                <tr>
                                    <td align="right">
                                        查询日期：
                                    </td>
                                    <td align="left">
                                        <input id="dpBalStart" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpBalEnd\')||\'2020-10-01\'}'})"
                                            runat="server" />
                                        <input id="dpBalEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpBalStart\')}',maxDate:'2020-10-01'})"
                                            runat="server" />
                                    </td>
                                    <td style="width: 5%">
                                    </td>
                                    <td align="right">
                                        选择房型：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddpRoomList" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <div id="dvBalSearch" runat="server">
                                            <asp:Button ID="btnBalSearch" runat="server" CssClass="btn primary" Text="查询" OnClick="btnBalSearch_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnExportBal" runat="server" CssClass="btn primary" Text="导出" OnClick="btnExportBal_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
                <div class="frame01" style="margin-top: 15px; margin-left: 5px">
                    <ul>
                        <li class="title">结算信息列表</li>
                    </ul>
                </div>
                <div class="frame02" style="margin-left: 5px; width: 1150px; overflow: auto" id="dvBalGridList"
                    runat="server">
                    <asp:GridView ID="gridViewCSBalList" runat="server" BackColor="White" AllowPaging="True"
                        CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" OnRowDataBound="gridViewCSBalList_RowDataBound"
                        OnPageIndexChanging="gridViewCSBalList_PageIndexChanging" OnRowCreated="gridViewCSBalList_RowCreated"
                        PageSize="10" CssClass="GView_BodyCSS">
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                    </asp:GridView>
                </div>
            </div>
            <div id="tabs-7" style="border: 1px solid #D5D5D5;">
                <div style="display: block;">
                    <div class="frame01" style="margin-top: 5px; margin-left: 5px">
                        <ul>
                            <li class="title">房控查询联系方式</li>
                            <li>
                                <table>
                                    <tr>
                                        <td style="text-align: right;">
                                            查房频率：
                                        </td>
                                        <td>
                                            <input type="radio" id="rdEveryDay" name="rdQueryRoomRate" runat="server" />每天&nbsp;&nbsp;&nbsp;&nbsp;<input
                                                type="radio" id="rdTwoDay" name="rdQueryRoomRate" runat="server" />两天一次&nbsp;&nbsp;&nbsp;&nbsp;<input
                                                    type="radio" id="rdEver" name="rdQueryRoomRate" runat="server" />永不询房
                                        </td>
                                        <td style="text-align: right;">
                                            上线时间：
                                        </td>
                                        <td>
                                            <input type="radio" id="rdOnLine14" name="rdOnLine" runat="server" />14点&nbsp;&nbsp;&nbsp;&nbsp;<input
                                                type="radio" id="rdOnLine18" name="rdOnLine" runat="server" />18点
                                        </td>
                                        <td style="text-align: right;">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            查房联系人：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQueryRoomLinkMan" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            查房联系电话：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQueryRoomLinkTel" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            查房传真：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQueryRoomLinkFax" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            查房备注：
                                        </td>
                                        <td colspan="5">
                                            <textarea id="txtQueryRoomRemark" runat="server" cols="115" style="height: 48px;"></textarea>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ul>
                    </div>
                    <div class="frame01" style="margin-top: 15px; margin-left: 5px">
                        <ul>
                            <li class="title">订单确认方式</li>
                            <li>
                                <table>
                                    <tr>
                                        <td style="text-align: right;">
                                            日间联系人：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOrderAffirmDayLinkMan" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right;">
                                            日间联系电话：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOrderAffirmDayLinkTel" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right;">
                                            日间传真：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOrderAffirmDayLinkFax" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            夜间联系人：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOrderAffirmNightLinkMan" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            夜间联系电话：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOrderAffirmNightLinkTel" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            夜间传真：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOrderAffirmNightLinkFax" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            订单确认备注：
                                        </td>
                                        <td colspan="5">
                                            <textarea id="txtOrderAffirmRemark" runat="server" cols="115" style="height: 48px;"></textarea>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ul>
                    </div>
                    <div class="frame01" style="margin-top: 15px; margin-left: 5px">
                        <ul>
                            <li class="title">订单审核方式</li>
                            <li>
                                <table>
                                    <tr>
                                        <td style="text-align: right;">
                                            日/夜审：
                                        </td>
                                        <td>
                                            <input type="radio" id="rdOrderVerifyTimeDay" name="rdOrderVerifyTime" runat="server" />日审&nbsp;&nbsp;&nbsp;&nbsp;<input
                                                type="radio" id="rdOrderVerifyTimeNight" name="rdOrderVerifyTime" runat="server" />夜审
                                        </td>
                                        <td style="text-align: right;">
                                            审核方式：
                                        </td>
                                        <td>
                                            <input type="radio" id="rdOrderVerifyTypeFax" name="rdOrderVerifyType" runat="server" />传真&nbsp;&nbsp;&nbsp;&nbsp;<input
                                                type="radio" id="rdOrderVerifyTypeTel" name="rdOrderVerifyType" runat="server" />电话
                                        </td>
                                        <td style="text-align: right;">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            审核联系人：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="rdOrderVerifyLinkMan" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            审核联系电话：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="rdOrderVerifyLinkTel" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            审核联系传真：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="rdOrderVerifyLinkFax" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            审核备注：
                                        </td>
                                        <td colspan="5">
                                            <textarea id="rdOrderVerifyRemark" runat="server" cols="115" style="height: 48px;"></textarea>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ul>
                    </div>
                    <div style="margin-left: 60px;" id="dvHotelEX" runat="server">
                        <asp:Button ID="btnAddHotelEX" runat="server" Text="保存" CssClass="btn primary" OnClick="btnAddHotelEX_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidColsNM" runat="server" />
    <asp:HiddenField ID="hidLMCount" runat="server" />
    <asp:HiddenField ID="hidLM2Count" runat="server" />
    <asp:HiddenField ID="hidHotelID" runat="server" />
    <asp:HiddenField ID="hidHotelNo" runat="server" />
    <asp:HiddenField ID="hidSelectedID" runat="server" />
    <asp:HiddenField ID="hidSalesID" runat="server" />
    <asp:HiddenField ID="hidCityName" runat="server" />
    <asp:HiddenField ID="hidRoomACT" runat="server" />
    <asp:HiddenField ID="hidRoomCD" runat="server" />
    <asp:HiddenField ID="hidBedCD" runat="server" />
    <asp:HiddenField ID="hidPRCD" runat="server" />
    <asp:HiddenField ID="hidPRST" runat="server" />
    <asp:HiddenField ID="hidPRRoomACT" runat="server" />
    <asp:HiddenField ID="hidSaveHotelID" runat="server" />
    <asp:HiddenField ID="hidModel" runat="server" />
    <asp:HiddenField ID="hidBussList" runat="server" />
    <asp:HiddenField ID="hidCityID" runat="server" />
    <asp:HiddenField ID="hidUCityID" runat="server" />
    <asp:HiddenField ID="hidHotelGroup" runat="server" />
    <asp:HiddenField ID="hidHotelGroupNew" runat="server" />
    <asp:HiddenField ID="retLgVal" runat="server" />
    <asp:HiddenField ID="hidCKeyWords" runat="server" />
    <asp:HiddenField ID="hidUKeyWords" runat="server" />
    <asp:HiddenField ID="hidOnline" runat="server" />
    <script type="text/javascript">
        $(function () {
            //        $("#tabs").tabs();
            var sid = document.getElementById("<%=hidSelectedID.ClientID%>").value;
            if (sid == "" || sid == "0") {
                $("#tabs").tabs();
            }
            else {
                $('#tabs').tabs({ selected: sid, select: function (event, ui) { document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index } });
            }

            $('#tabs').bind('tabsselect', function (event, ui) {
                document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index
            });

            $("#ctabs").tabs();
        });
    </script>
</asp:Content>
