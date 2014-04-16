<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelInfoInlineImage.aspx.cs"
    Inherits="WebUI_Hotel_HotelInfoInlineImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <style type="text/css">
        #sortable1, #sortable2, #sortable3
        {
            list-style-type: none;
            margin: 0;
            padding: 0 0 0.5em;
            float: left;
            margin-right: 10px;
        }
        #sortable1 li, #sortable2 li, #sortable3 li
        {
            margin: 0 5px 5px 5px;
            padding: 5px;
            font-size: 1.2em;
            width: 120px;
            height: 110px;
            float: left;
        }
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
    </style>
    <script src="../../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script type="text/javascript">
        function AddNew() {
            var hotelId = document.getElementById("<%=HidHotelID.ClientID%>").value;
            if (hotelId == "") {
                alert("请先选择酒店！");
                return false;
            }

            var iTop = (window.screen.availHeight - 30 - 585) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - 630) / 2; //获得窗口的水平位置;

            var cityId = document.getElementById("<%=HidCityID.ClientID%>").value;

            window.open('PhotoPatchUploadNew.aspx?hotelId=' + hotelId + '&cityId=' + cityId, 'newwindow', 'height=570, width=630, top=' + iTop + ', left=' + iLeft + ', toolbar=no, menubar=no, scrollbars=yes,Resizable=no,location=no, status=no')
            //var retunValue = window.showModalDialog('PhotoPatchUpload.aspx?hotelId=' + hotelId + '&cityId=' + cityId, '', 'dialogWidth=800px;dialogHeight=500px');
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

        function deletePic(obj) {
            $.ajax({
                type: "post",
                url: "HotelInfoInlineImage.aspx", //服务端处理程序 
                //data: { id: dtCoverPicIds, order: new_order1, type: "sortable1", id: dtHotelPicIds, order: new_order2, type: "sortable2", id: dtRoomTypePicIds, order: new_order3, type: "sortable3" }, //id:新的排列对应的ID,order：原排列顺序 
                data: { 'id': obj, 'type': 'DeletePic' },
                beforeSend: function () {
                    //alert("数据更新中，请稍等...");
                    BtnLoadStyle();
                },
                success: function (msg) {
                    BtnCompleteStyle();
                    document.getElementById("<%=btnRefresh.ClientID%>").click();
                }
            });
        }

        function updatePic(arg, hid) {
            var obj = new Object();
            obj.id = arg;
            obj.hotelId = hid;
            var time = new Date();
            var retunValue = window.showModalDialog("ImageDetail.aspx?ID=" + arg + "&hotelId=" + hid + "&time=" + time, obj, "dialogWidth=800px;dialogHeight=500px");
            if (retunValue) {
                document.getElementById("<%=btnRefresh.ClientID%>").click();
            }
        }
    </script>
    <script type="text/javascript">
//        $(function () {
//            var hotelID = document.getElementById("<%=HidHotelID.ClientID%>").value;
//            var dtCoverPicIds = document.getElementById("<%=HidCoverPicIds.ClientID%>").value;
//            var dtHotelPicIds = document.getElementById("<%=HidHotelPicIds.ClientID%>").value;
//            var dtRoomTypePicIds = document.getElementById("<%=HidRoomTypePicIds.ClientID%>").value;

//            var $sortable1 = $("#sortable1");
//            var $sortable2 = $("#sortable2");
//            var $sortable3 = $("#sortable3");

//            $("#sortable1, #sortable2, #sortable3").sortable({
//                update: function () {
//                    var new_order1 = [];
//                    var new_order2 = [];
//                    var new_order3 = [];
//                    $sortable1.children(".ui-state-default").each(function () {
//                        new_order1.push(this.title);
//                    });
//                    $sortable2.children(".ui-state-default").each(function () {
//                        new_order2.push(this.title);
//                    });
//                    $sortable3.children(".ui-state-default").each(function () {
//                        new_order3.push(this.title);
//                    });
//                    $.ajax({
//                        type: "post",
//                        url: "HotelInfoInlineImage.aspx", //服务端处理程序 
//                        //data: { id: dtCoverPicIds, order: new_order1, type: "sortable1", id: dtHotelPicIds, order: new_order2, type: "sortable2", id: dtRoomTypePicIds, order: new_order3, type: "sortable3" }, //id:新的排列对应的ID,order：原排列顺序 
//                        data: { 'sort': [{ 'id': dtCoverPicIds, 'order': new_order1, 'type': 'sortable1', 'hotelID': hotelID }, { 'id': dtHotelPicIds, 'order': new_order2, 'type': 'sortable2', 'hotelID': hotelID }, { 'id': dtRoomTypePicIds, 'order': new_order3, 'type': 'sortable3', 'hotelID': hotelID}] },
//                        beforeSend: function () {
//                            //alert("数据更新中，请稍等...");
//                            BtnLoadStyle();
//                        },
//                        success: function (msg) {
//                            BtnCompleteStyle();
//                            document.getElementById("<%=btnRefresh.ClientID%>").click();
//                            //window.location.replace(window.location.href);
//                        }
//                    });
//                },
//                connectWith: ".connectedSortable",
//                dropOnEmpty: true,
//                opacity: 0.6,
//                revert: true
//            }).disableSelection();
//        });


        $(function () {
            var hotelID = document.getElementById("<%=HidHotelID.ClientID%>").value;
            var dtCoverPicIds = document.getElementById("<%=HidCoverPicIds.ClientID%>").value;
            var dtHotelPicIds = document.getElementById("<%=HidHotelPicIds.ClientID%>").value;
            //var dtRoomTypePicIds = document.getElementById("<%=HidRoomTypePicIds.ClientID%>").value;

            var $sortable1 = $("#sortable1");
            var $sortable2 = $("#sortable2");
            //var $sortable3 = $("#sortable3");

            $("#sortable1, #sortable2").sortable({
                change: function () {
                    var ele = document.getElementById("specialID");
                    ele.style = "width: 1px; border-width: 0px; background-color: White; border-color: White; margin-left: 0px; margin-right: 0px; padding: 0px; margin-bottom: 0px;";
                },
                stop: function () {
                    var new_order1 = [];
                    var new_order2 = [];
                    //var new_order3 = [];
                    $sortable1.children(".ui-state-default").each(function () {
                        new_order1.push(this.title);
                    });
                    $sortable2.children(".ui-state-default").each(function () {
                        new_order2.push(this.title);
                    });
                    //$sortable3.children(".ui-state-default").each(function () {
                    //       new_order3.push(this.title);
                    //});
                    $.ajax({
                        type: "post",
                        url: "HotelInfoInlineImage.aspx", //服务端处理程序 
                        //data: { id: dtCoverPicIds, order: new_order1, type: "sortable1", id: dtHotelPicIds, order: new_order2, type: "sortable2", id: dtRoomTypePicIds, order: new_order3, type: "sortable3" }, //id:新的排列对应的ID,order：原排列顺序 
                        //data: { 'sort': [{ 'id': dtCoverPicIds, 'order': new_order1, 'type': 'sortable1', 'hotelID': hotelID }, { 'id': dtHotelPicIds, 'order': new_order2, 'type': 'sortable2', 'hotelID': hotelID }, { 'id': dtRoomTypePicIds, 'order': new_order3, 'type': 'sortable3', 'hotelID': hotelID}] },
                        data: { 'sort': [{ 'id': dtCoverPicIds, 'order': new_order1, 'type': 'sortable1', 'hotelID': hotelID }, { 'id': dtHotelPicIds, 'order': new_order2, 'type': 'sortable2', 'hotelID': hotelID}], 'type': '0' },
                        beforeSend: function () {
                            BtnLoadStyle();
                        },
                        success: function (msg) {
                            BtnCompleteStyle();
                            document.getElementById("<%=btnRefresh.ClientID%>").click();
                        }
                    });
                },
                connectWith: ".connectedSortable",
                dropOnEmpty: true,
                opacity: 0.6,
                revert: true
            }).disableSelection();
        });

        $(function () {
            var hotelID = document.getElementById("<%=HidHotelID.ClientID%>").value;
            var dtRoomTypePicIds = document.getElementById("<%=HidRoomTypePicIds.ClientID%>").value;

            var $sortable3 = $("#sortable3");

            $("#sortable3").sortable({
                stop: function () {
                    var new_order3 = [];
                    $sortable3.children(".ui-state-default").each(function () {
                        new_order3.push(this.title);
                    });
                    $.ajax({
                        type: "post",
                        url: "HotelInfoInlineImage.aspx", //服务端处理程序 
                        //data: { id: dtCoverPicIds, order: new_order1, type: "sortable1", id: dtHotelPicIds, order: new_order2, type: "sortable2", id: dtRoomTypePicIds, order: new_order3, type: "sortable3" }, //id:新的排列对应的ID,order：原排列顺序 
                        data: { 'sort': [{ 'id': dtRoomTypePicIds, 'order': new_order3, 'type': 'sortable3', 'hotelID': hotelID}], 'type': '1' },
                        beforeSend: function () {
                            //alert("数据更新中，请稍等...");
                            BtnLoadStyle();
                        },
                        success: function (msg) {
                            BtnCompleteStyle();
                            document.getElementById("<%=btnRefresh.ClientID%>").click();
                        }
                    });
                },
                dropOnEmpty: true,
                opacity: 0.6,
                revert: true
            }).disableSelection();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 92%;">
        <input type="button" id="Add" value="上传" class="btn primary" onclick="AddNew()" /></div>
    <div class="frame01" id="dtCoverPicDiv" runat="server" style="margin: 15px 14px 15px 14px;
        display: none">
    </div>
    <div class="frame01" id="dtHotelPicDiv" runat="server" style="margin: 15px 14px 15px 14px;
        display: none">
    </div>
    <div class="frame01" id="dtRoomTypePicDiv" runat="server" style="margin: 15px 14px 15px 14px;
        display: none">
    </div>
    <div style="display: none">
        <asp:Button ID="btnRefresh" runat="server" Text="Button" CssClass="btn primary" OnClick="btnRefresh_Click" /></div>
    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
    <asp:HiddenField ID="HidHotelID" runat="server" />
    <asp:HiddenField ID="HidCityID" runat="server" />
    <asp:HiddenField ID="HidCoverPicIds" runat="server" />
    <asp:HiddenField ID="HidHotelPicIds" runat="server" />
    <asp:HiddenField ID="HidRoomTypePicIds" runat="server" />
    </form>
</body>
</html>
