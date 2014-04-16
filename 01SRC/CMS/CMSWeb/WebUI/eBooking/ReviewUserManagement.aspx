<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ReviewUserManagement.aspx.cs" Inherits="WebUI_eBooking_ReviewUserManagement" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <style type="text/css">
        .bgDiv2
        {
            width: 100%;
            height: 100%;
            display: none;
            position: fixed;
            top: 0px;
            left: 0px;
            right: 0px;
            background-color: #000000;
            filter: alpha(Opacity=80);
            -moz-opacity: 0.5;
            opacity: 0.5;
            z-index: 1000;
            background-color: #000000;
            opacity: 0.6;
        }
        .popupDiv2
        {
            font-size: 14px;
            top: 35%;
            left: 30%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            z-index: 1001;
            display: none;
            background-color: White;
            margin-left: 0px !important; /*FF IE7 该值为本身宽的一半 */
            margin-top: 0px !important; /*FF IE7 该值为本身高的一半*/
            margin-left: 0px;
            margin-top: 0px;
            position: fixed !important; /*FF IE7*/
            position: absolute; /*IE6*/
            _top: expression(eval(document.compatMode &&                 document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 : /*IE6*/ document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2); /*IE5 IE5.5*/
            _left: expression(eval(document.compatMode &&                 document.compatMode=='CSS1Compat') ? documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 : /*IE6*/ document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2); /*IE5 IE5.5*/
        }
        
        .bgDivHotel
        {
            width: 100%;
            height: 100%;
            display: none;
            position: fixed;
            top: 0px;
            left: 0px;
            right: 0px;
            background-color: #000000;
            filter: alpha(Opacity=80);
            -moz-opacity: 0.5;
            opacity: 0.5;
            z-index: 1002;
            background-color: #000000;
            opacity: 0.6;
        }
        .popupDivHotel
        {
            font-size: 14px;
            top: 35%;
            left: 25%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            z-index: 1003;
            display: none;
            background-color: White;
            margin-left: 0px !important; /*FF IE7 该值为本身宽的一半 */
            margin-top: 0px !important; /*FF IE7 该值为本身高的一半*/
            margin-left: 0px;
            margin-top: 0px;
            position: fixed !important; /*FF IE7*/
            position: absolute; /*IE6*/
            _top: expression(eval(document.compatMode &&                 document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 : /*IE6*/ document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2); /*IE5 IE5.5*/
            _left: expression(eval(document.compatMode &&                 document.compatMode=='CSS1Compat') ? documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 : /*IE6*/ document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2); /*IE5 IE5.5*/
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAddDivHotels").click(function () {
                $("#userHotels").html("");
                var userboard = document.getElementById("<%=DivHotels.ClientID%>");
                var useridList = "";
                var outHTML = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:White;border:1px solid black;text-align:center;\"><tr><td style=\"border:1px solid black;\">酒店ID</td><td style=\"border:1px solid black;\">酒店名称</td><td style=\"border:1px solid black;\">删除</td></tr>";
                outHTML += document.getElementById("hidSelectHotels").value;
                for (i = 0; i < userboard.childNodes.length; i++) {
                    var nodeValue = userboard.childNodes[i].value;
                    if (typeof (nodeValue) != "undefined") {
                        outHTML += "<tr><td style=\"border:1px solid black;\">" + nodeValue.substring(1, nodeValue.indexOf("]")) + "</td><td style=\"border:1px solid black;\">" + nodeValue.substr(nodeValue.indexOf("]") + 1) + "</td><td style=\"border:1px solid black;cursor:pointer;\"><span onclick=\"$(this).parent().parent().remove();\">删除</span></td></tr>";
                    }
                }
                outHTML += "</table>";
                $("#userHotels").html(outHTML);

                invokeCloseDivHotels();
            });
            $("#btnUpdateOrSave").click(function () {
                var type = document.getElementById("hidType").value;
                var userName = $("#<%=txtDivUserName.ClientID%>").val();
                var userPwd = $("#<%=txtDivPwd.ClientID%>").val();
                var userTel = $("#<%=txtDivTel.ClientID%>").val();
                var userboard = document.getElementById("userHotels");
                var hotelIds = "";
                for (i = 0; i < userboard.childNodes.length; i++) {
                    for (var j = 1; j < userboard.childNodes[i].childNodes[0].childNodes.length; j++) {
                        var nodeValue = userboard.childNodes[i].childNodes[0].childNodes[j].childNodes[0].innerHTML;
                        //var nodeName = userboard.childNodes[i].childNodes[0].childNodes[j].childNodes[1].innerHTML;
                        hotelIds += nodeValue + ",";
                    }
                }
                var remark = $("#<%=txtDivRemark.ClientID%>").val();
                var userID = document.getElementById("hidUserID").value;
                $.ajax({
                    type: "Post",
                    url: "ReviewUserManagement.aspx/UpdateOrSave",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'type':'" + type + "','userName':'" + userName + "','userPwd':'" + userPwd + "','userTel':'" + userTel + "','hotelIds':'" + hotelIds + "','remark':'" + remark + "','userID':'" + userID + "'}",
                    success: function (data) {
                        //返回的数据用data.d获取内容
                        var d = jQuery.parseJSON(data.d);
                        if (d.d == "[success]") {
                            invokeCloseDiv();
                            $("#<%=btnSelectHotels.ClientID%>").click();
                        } else {
                            alert(d.d);
                            return;
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    }
                });
            });
        });
        //显示弹出的层
        function invokeOpenDiv(obj) {
            if (obj != "") {
                document.getElementById("hidType").value = "1";
                document.getElementById("spanTitle").innerHTML = "编辑用户";
                $.ajax({
                    type: "Post",
                    url: "ReviewUserManagement.aspx/GetUserHotel",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'userName':'" + obj + "'}",
                    success: function (data) {
                        //返回的数据用data.d获取内容
                        var d = jQuery.parseJSON(data.d);
                        if (d.length > 0) {
                            document.getElementById("hidUserID").value = d[0].USERID;
                            $("#<%=txtDivUserName.ClientID%>").val(d[0].USERNAME);
                            $("#<%=txtDivTel.ClientID%>").val(d[0].USERTEL);
                            $("#<%=txtDivPwd.ClientID%>").val(d[0].USERPWD);
                            $("#<%=txtDivRemark.ClientID%>").text(d[0].REMARK);
                            var outHTML = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:White;text-align:center;\"><tr><td style=\"border:1px solid black;\">酒店ID</td><td style=\"border:1px solid black;\">酒店名称</td><td style=\"border:1px solid black;\">删除</td></tr>";
                            $.each(d, function (i) {
                                outHTML += "<tr><td style=\"border:1px solid black;\">" + d[i].HOTELID + "</td><td style=\"border:1px solid black;\">" + d[i].HOTELNAME + "</td><td style=\"border:1px solid black;cursor:pointer;\"><span onclick=\"$(this).parent().parent().remove();\">删除</span></td></tr>";
                                document.getElementById("hidSelectHotels").value += "<tr><td style=\"border:1px solid black;\">" + d[i].HOTELID + "</td><td style=\"border:1px solid black;\">" + d[i].HOTELNAME + "</td><td style=\"border:1px solid black;cursor:pointer;\"><span onclick=\"$(this).parent().parent().remove();\">删除</span></td></tr>";
                            });
                            outHTML += "</table>";
                            $("#userHotels").html(outHTML);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("XMLHttpRequest:" + XMLHttpRequest + ",textStatus:+" + textStatus + ", errorThrown:" + errorThrown);
                    }
                });
            } else {
                document.getElementById("hidType").value = "0";
                document.getElementById("spanTitle").innerHTML = "添加用户";
                var outHTML = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:White;border:1px solid black;text-align:center;\"><tr><td style=\"border:1px solid black;\">酒店ID</td><td style=\"border:1px solid black;\">酒店名称</td><td style=\"border:1px solid black;\">删除</td></tr>";
                outHTML += "</table>";
                $("#userHotels").html(outHTML);
            }
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            var userName = "kevin1";
        }

        //隐藏弹出的层
        function invokeCloseDiv() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }
        //显示弹出的层
        function invokeOpenDivHotels() {
            document.getElementById("popupDivHotel").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDivHotel");
            bgObj.style.display = "block";
        }

        //隐藏弹出的层
        function invokeCloseDivHotels() {
            document.getElementById("popupDivHotel").style.display = "none";
            document.getElementById("bgDivHotel").style.display = "none";
        }

        function addHotelByDiv() {
            var idVal = document.getElementById("wctDivHotel").value;
            if (idVal.trim() == "") {
                return;
            }
            var btnid = "btnCommon_" + idVal.substring(0, idVal.indexOf("]"));
            var dpValue = document.getElementById("wctDivHotel").value + "   ";
            if (btnid == "btnCommon_") {
                document.getElementById("wctDivHotel").value = "";
                document.getElementById("wctDivHotel").text = "";
                return;
            }

            if (document.getElementById(btnid) != null && document.getElementById(btnid) != "") {
                document.getElementById("wctDivHotel").value = "";
                document.getElementById("wctDivHotel").text = "";
                return;
            }
            var board = document.getElementById("<%=DivHotels.ClientID%>");
            var e = document.createElement("input");
            e.type = "button";
            e.setAttribute("id", btnid);
            e.value = idVal;
            e.setAttribute("style", "margin:10px 10px;padding-right:20px;background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
            e.onclick = function () {
                e.parentNode.removeChild(this);
            }
            board.appendChild(e);

            document.getElementById("wctDivHotel").value = "";
            document.getElementById("wctDivHotel").text = "";
        }

        function SetControlValue() {
            document.getElementById("<%=HidSelectHotelID.ClientID%>").value = document.getElementById("wctHotel").value;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="frame01" style="margin: 8px 14px 5px 14px;">
                <ul>
                    <li class="title">系统设置</li>
                    <li>
                        <table>
                            <tr>
                                <td>
                                    酒店：
                                </td>
                                <td>
                                    <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel"
                                        AutoParent="ReviewUserManagement.aspx?Type=hotel" />
                                </td>
                                <td>
                                    用户：
                                </td>
                                <td>
                                    <input type="text" id="txtUserName" runat="server" />
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnLoadStyle();setValue();"
                                                OnClick="btnSelect_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnAddUser" runat="server" CssClass="btn primary" Text="增加用户" OnClientClick="invokeOpenDiv('')" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div class="frame02">
                <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White"
                    CellPadding="4" CellSpacing="1" AllowPaging="false" Width="100%" EmptyDataText="没有数据"
                    PageSize="2" DataKeyNames="ID" OnRowDataBound="gridViewCSList_RowDataBound" CssClass="GView_BodyCSS">
                    <Columns>
                        <asp:BoundField DataField="USERNAME" HeaderText="用户名" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HOTELNAME" HeaderText="管理酒店" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreateTime" HeaderText="创建时间" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="#" onclick="invokeOpenDiv('<%# DataBinder.Eval(Container.DataItem, "USERNAME") %>')">
                                    编辑</a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <RowStyle CssClass="GView_ItemCSS" />
                    <HeaderStyle CssClass="GView_HeaderCSS" />
                    <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                </asp:GridView>
            </div>
            <div style="margin-left: 10px;">
                <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb" ID="AspNetPager1"
                    runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页"
                    CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页"
                    ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%"
                    ShowPageIndexBox="always" AlwaysShow="true" Width="100%" LayoutType="Table" OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:AspNetPager>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="bgDiv2" class="bgDiv2">
    </div>
    <div id="popupDiv2" class="popupDiv2" style="background-color: #EDEDED; font-size: 14px;">
        <div class="frame01" style="margin: 0px 0px; width: 510px; height: 430px;">
            <ul>
                <li class="title"><span id="spanTitle"></span></li>
                <li>
                    <table style="background-color: #EDEDED;width:100%">
                        <tr style="line-height: 30px;">
                            <td style="width: 20%; text-align: right">
                                用户名:
                            </td>
                            <td>
                                <input type="text" id="txtDivUserName" runat="server" style="width: 300px; margin-left: 15px;" />
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td style="width: 20%; text-align: right">
                                电话:
                            </td>
                            <td>
                                <input type="text" id="txtDivTel" runat="server" style="width: 300px; margin-left: 15px;" />
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td style="width: 20%; text-align: right">
                                密码:
                            </td>
                            <td>
                                <input type="text" id="txtDivPwd" runat="server" style="width: 300px; margin-left: 15px;" />
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td style="width: 24%; text-align: right">
                                管理酒店
                            </td>
                            <td style="text-align: right">
                                <input type="button" id="btnAddHotel" runat="server" value="添加酒店" class="btn primary"
                                    onclick="invokeOpenDivHotels()" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="userHotels" style="max-height:160px;width:100%;overflow-y:scroll;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 24%; text-align: right">
                                备注：
                            </td>
                            <td>
                                <textarea id="txtDivRemark" runat="server" rows="2" cols="40"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <input type="button" id="btnUpdateOrSave" value="确定" class="btn primary" />&nbsp;&nbsp;&nbsp;&nbsp;<input
                                    type="button" id="Button2" runat="server" value="取消" class="btn" onclick="invokeCloseDiv()" />
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
        </div>
    </div>
    <div id="bgDivHotel" class="bgDivHotel">
    </div>
    <div id="popupDivHotel" class="popupDivHotel" style="background-color: #EDEDED; font-size: 14px;">
        <div class="frame01" style="margin: 0px 0px; width: 660px; height: 400px;">
            <ul>
                <li class="title">添加酒店</li>
                <li>
                    <table>
                        <tr>
                            <td>
                                酒店名称:
                            </td>
                            <td>
                                <uc1:WebAutoComplete ID="wctDivHotel" runat="server" CTLID="wctDivHotel" AutoType="hotel"
                                    AutoParent="ReviewUserManagement.aspx?Type=hotel" />
                            </td>
                            <td>
                                <input type="button" id="Button1" value="添加" onclick="addHotelByDiv()" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <input type="button" id="btnAddDivHotels" value="确定" />&nbsp;&nbsp;&nbsp;&nbsp;<input
                                    type="button" id="btnCloseDivHotels" onclick="invokeCloseDivHotels()" value="取消" />
                            </td>
                        </tr>
                    </table>
                    <div id="DivHotels" runat="server">
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
    <input type="button" id="btnSelectHotels" style="display: none" runat="server" onserverclick="btnSelect_Click" />
    <asp:HiddenField ID="HidSelectHotelID" runat="server" />
    <input type="hidden" id="hidUserID" />
    <input type="hidden" id="hidType" />
    <input type="hidden" id="hidSelectHotels" />
    <input type="hidden" id="hidSelectHotelIDS" />
</asp:Content>
