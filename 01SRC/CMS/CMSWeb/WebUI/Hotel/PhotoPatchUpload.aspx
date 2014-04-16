<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoPatchUpload.aspx.cs"
    Inherits="WebUI_Hotel_PhotoPatchUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Scripts/jquery.uploadify-v2.1.0/example/css/default.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Scripts/jquery.uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Scripts/jquery.uploadify-v2.1.0/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
            var ASPSESSID = "<%= Session.SessionID %>";
            var hotelId = document.getElementById("<%=HidHotelID.ClientID%>").value;
            var cityId = document.getElementById("<%=HidCityID.ClientID%>").value;
            $("#uploadify").uploadify({
                'uploader': '../../Scripts/jquery.uploadify-v2.1.0/uploadify.swf',
                'script': 'uploadFiles.ashx?hotelID=' + hotelId, // 服务端上传路径
                'cancelImg': '../../Scripts/jquery.uploadify-v2.1.0/cancel.png',
                'buttonText': 'FileUpload',
                'fileTypeDesc': 'Image Files',
                //允许上传的文件后缀
                'fileTypeExts': '*.gif; *.jpg; *.png',
                'queueSizeLimit': '30',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'method': "get",
                'auto': false,
                'multi': true,
                'sizeLimit': '209715200',
                'scriptData': { 'ASPSESSID': ASPSESSID, 'AUTHID': auth }
            });
            //选择文件  初始化
            $('#uploadify').bind("uploadifySelect", function (file) {
                $('#uploadify').uploadifySettings('scriptData', { 'ASPSESSID': ASPSESSID, 'AUTHID': auth });
                alert(formDate);

            });
            //选择文件  初始化
            $('#uploadify').bind("uploadifySelectOnce", function (event, data) {
                document.getElementById("<%=uploadADiv.ClientID%>").style.display = "";
            });
            //文件全部上传完成  
            $('#uploadify').bind("uploadifyAllComplete", function (up, files) {
                //document.getElementById("<%=btnDiv.ClientID%>").style.display = "";
                document.getElementById("<%=uploadADiv.ClientID%>").style.display = "";
                document.getElementById("<%=btnOver.ClientID%>").click();
            });
        });
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
        <h2>
            文件上传:</h2>
        <div id="fileQueue" style="width: 480px; height: 450px;">
        </div>
        <div>
            <table>
                <tr>
                    <td style="white-space: nowrap">
                        <div>
                            <input type="file" name="uploadify" id="uploadify" />
                        </div>
                    </td>
                    <td>
                        <div id="uploadADiv" runat="server" style="display: none;">
                            <a href="javascript:$('#uploadify').uploadifyUpload()">上传</a> | <a href="javascript:$('#uploadify').uploadifyClearQueue()">
                                取消上传</a>
                        </div>
                    </td>
                </tr>
            </table>
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
