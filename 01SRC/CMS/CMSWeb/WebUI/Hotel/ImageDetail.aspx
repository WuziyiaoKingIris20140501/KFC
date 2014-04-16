<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageDetail.aspx.cs" Title="城市管理明细"
    Inherits="ImageDetail" %>

<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>CMS酒店管理系统</title>
    <base target="_self">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
        function GetList() {
            var cover = document.getElementById("<%=chkCover.ClientID%>");
            var ddl = document.getElementById("<%=ddlPicType.ClientID%>");
            var index = ddl.selectedIndex;
            var picTypeValue = ddl.options[index].value;
            if (cover.checked) {
                if (picTypeValue == "1") {
                    alert("房型图片不能设为封面图,请重新选择!");
                    return false;
                }
            }
            if (picTypeValue == "-1") {
                alert("请选择图片类型！");
                return false;
            }
            if (picTypeValue == "1") {
                var ddl2 = document.getElementById("<%=ddlRoomList.ClientID%>");
                var eindex = ddl2.selectedIndex;
                roomTypevalue = ddl2.options[eindex].value;
                if (roomTypevalue == "-1") {
                    alert("请选择房型！");
                    return false;
                }
                var roomCode = document.getElementById("<%=HidRQRoomCode.ClientID%>").value;
                var strs = new Array();
                strs = roomCode.split(",");
                for (var i = 0; i < strs.length; i++) {
                    if (roomTypevalue == strs[i]) {
                        if (confirm("该房型图片已经存在，是否要替换？")) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        function OnchangeRoom() {
            var ddl = document.getElementById("<%=ddlPicType.ClientID%>");
            var index = ddl.selectedIndex;
            var picTypeValue = ddl.options[index].value;
            if (picTypeValue == "1") {
                document.getElementById("<%=RoomListDiv.ClientID%>").style.display = "";
            } else {
                document.getElementById("<%=RoomListDiv.ClientID%>").style.display = "none";
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 91px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="right">
        <div class="frame01">
            <ul>
                <li class="title">图片明细</li>
                <li>
                    <table>
                        <tr>
                            <td colspan="4" style="">
                                <img id="ImgSrc" runat="server" style="width: 400px; height: 300px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                图片URL:
                            </td>
                            <td colspan="3">
                                <span id="spanUrl" runat="server"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                封面图:
                            </td>
                            <td colspan="3">
                                <input type="checkbox" id="chkCover" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                图片类型：
                            </td>
                            <td>
                                <select id="ddlPicType" runat="server" onchange="OnchangeRoom()">
                                    <option value="-1">请选择</option>
                                    <option value="2">酒店外观</option>
                                    <option value="1">房型图片</option>
                                    <option value="4">酒店大堂</option>
                                    <option value="5">餐饮娱乐</option>
                                    <option value="6">酒店图标</option>
                                    <option value="7">其他图片</option>
                                </select>
                            </td>
                            <td colspan="2">
                                <div id="RoomListDiv" runat="server" style="display: none">
                                    <table>
                                        <tr>
                                            <td>
                                                房型:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRoomList" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                *设置为封面图片后，原来的封面图片会自动变成普通酒店图片。
                            </td>
                        </tr>
                    </table>
                </li>
                <li>
                    <div id="detailMessageContent" runat="server" style="color: red">
                    </div>
                </li>
                <li class="button">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="保存" OnClientClick="return GetList()"
                        OnClick="btnUpdate_Click" />
                </li>
            </ul>
        </div>
    </div>
    <asp:HiddenField ID="HidHotelID" runat="server" />
    <asp:HiddenField ID="HidRoomCode" runat="server" />
    <asp:HiddenField ID="HidIsRoomType" runat="server" />
    <asp:HiddenField ID="HidRQRoomCode" runat="server" />
    </form>
</body>
</html>
