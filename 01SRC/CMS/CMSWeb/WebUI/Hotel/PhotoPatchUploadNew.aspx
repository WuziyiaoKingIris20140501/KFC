<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoPatchUploadNew.aspx.cs"
    Inherits="WebUI_Hotel_PhotoPatchUploadNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            regUpload();
        });
        function regUpload() {//在这里我列出了常用的参数和注释，更多的请直接看jquery.uploadify.js
            var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
            var ASPSESSID = document.getElementById("<%=HiddenField1.ClientID%>").value;
            var hotelId = document.getElementById("<%=HidHotelID.ClientID%>").value;
            $("#uploadify").uploadify({
                swf: '../../Scripts/uploadify/uploadify.swf', //上传的Flash，不用管，路径对就行
                uploader: 'uploadFiles.ashx?hotelID=' + hotelId, // 服务端上传路径 //Post文件到指定的处理文件
                auto: false,
                //        buttonClass:'JQButton',//浏览按钮的class
                buttonText: '浏览', //浏览按钮的Text
                cancelImage: '../../Scripts/uploadify/uploadify-cancel.png', //取消按钮的图片地址
                fileTypeDesc: '*.bmp;*.tiff;*.jpg;*.gif;*.jpeg;', //需过滤文件类型
                fileTypeExts: '*.bmp;*.tiff;*.jpg;*.gif;*.jpeg;', //需过滤文件类型的提示
                folder: 'UploadFile',
                //		    height: 28,//浏览按钮高
                //		    width:52,//浏览按钮宽
                multi: true, //是否允许多文件上传
                uploadLimit: 999, //同时上传多小个文件z
                queueSizeLimit: 999, //队列允许的文件总数
                removeCompleted: false, //当上传成功后是否将该Item删除
                onSelect: function (file) {
                    $('#uploadify').uploadifySettings('postData', { 'ASPSESSID': ASPSESSID, 'AUTHID': auth });
                    document.getElementById("<%=uploadADiv.ClientID%>").style.display = "";
                }, //选择文件时触发事件
                onSelectError: function (file, errorCode, errorMsg) { }, //选择文件有误触发事件
                onUploadComplete: function (file) { }, //上传成功触发事件
                onQueueComplete: function () {
                    document.getElementById("<%=uploadADiv.ClientID%>").style.display = "";
                    document.getElementById("<%=btnOver.ClientID%>").click();
                },
                onUploadError: function (file, errorCode, errorMsg) { }, //上传失败触发事件
                onUploadProgress: function (file, fileBytesLoaded, fileTotalBytes) { }, //上传中触发事件
                onUploadStart: function (file) { }, //上传开始触发事件
                postData: { 'ASPSESSID': ASPSESSID, 'AUTHID': auth }
                //onUploadSuccess: function(event, response, status){AddDocItem($.parseJSON(response));}  //当单个文件上传成功后激发的事件

            });
        }
    </script>
    <script type="text/javascript">
        function checkRoomType(obj, num) {
            var picType = document.getElementById(obj.id).value;
            if (picType == "1") {
                document.getElementById("roomTypeDiv" + num + "").style.display = "";
            } else {
                document.getElementById("roomTypeDiv" + num + "").style.display = "none";
            }
        }

        function GetList() {
            var flag = true;
            var num = document.getElementById("<%=HiddenNum.ClientID%>").value;
            var Answer = "";
            var roomTypevalue = "";
            for (var i = 0; i < num; i++) {
                var htpPathBak = document.getElementById("htpPathBak" + i + "").value;
                var CoverPic = document.getElementById("CoverPic" + i + "").checked;
                var ImgSrc = document.getElementById("ImgSrc" + i + "").src;
                var fileName = document.getElementById("fileName" + i + "").innerHTML;
                var ddl = document.getElementById("picType" + i + "");
                var index = ddl.selectedIndex;
                var picTypeValue = ddl.options[index].value;
                if (picTypeValue == "-1") {
                    alert("请选择图片类型");
                    return false;
                }
                if (picTypeValue == "1") {
                    var ddl2 = document.getElementById("roomType" + i + "");
                    var eindex = ddl2.selectedIndex;
                    roomTypevalue = ddl2.options[eindex].value;
                    if (roomTypevalue == "-1") {
                        alert("请选择房型");
                        return false;
                    }
                }
                Answer = Answer + "CoverPic&" + CoverPic + ",ImgSrc&" + ImgSrc + ",fileName&" + fileName + ",picTypeValue&" + picTypeValue + ",roomTypevalue&" + roomTypevalue + ",htpPathBak&" + htpPathBak + "|";
            }
            document.getElementById("<%=HiddenAnswer.ClientID%>").value = Answer;
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="PicUpload" runat="server" style="margin-left: 15px;">
        <input type="file" name="uploadify" id="uploadify" />
        <div id="uploadADiv" runat="server" style="display: none;">
            <a href="javascript:$('#uploadify').uploadifyUpload('*')">上传</a> <a href="javascript:$('#uploadify').uploadifyClearQueue()">
                清除列表</a></div>
        <div id="fileQueue">
        </div>
        <div>
            <table width="400px">
                <tr align="center">
                    <td>
                        <div id="btnDiv" runat="server" style="display: none">
                            <asp:Button ID="btnOver" runat="server" CssClass="btn primary" Text="下一步" OnClick="btnOver_Click" /></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="display: block; margin-left: 55px;" runat="server" id="upLoadPic">
        <table>
            <tr>
                <td>
                    <div runat="server" id="upLoadPicDiv">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <div runat="server" id="uploadBtnDiv" style="display: none;">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="完成" OnClientClick="return GetList();"
                            OnClick="Button2_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HiddenAnswer" runat="server" />
    <asp:HiddenField ID="HiddenNum" runat="server" />
    <asp:HiddenField ID="HidHotelID" runat="server" />
    <asp:HiddenField ID="HidCityID" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="AspSessID" runat="server" />
    </form>
</body>
</html>
