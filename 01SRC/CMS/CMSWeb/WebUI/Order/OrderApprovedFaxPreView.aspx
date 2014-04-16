<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderApprovedFaxPreView.aspx.cs" 
    Inherits="OrderApprovePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
    <title>CMS酒店管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>    
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
    <link href="../../Styles/Sites.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/public.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/WdatePicker.js")%>'></script>
    <script language="javascript" type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/common.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-1.7.1.min.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-ui-1.8.18.custom.min.js")%>'></script>
    <style type="text/css">
    .Tb_BodyCSS tr
    {
        height:30px;
    }
    .Tb_BodyCSS td
    {
        height:30px;
        border-right:1px #d5d5d5 solid;
        border-top:1px #d5d5d5 solid;
        padding-left:10px;
    }
        
    .Tb_BodyCSS {
        border-collapse:collapse;
        border-spacing:0;
        border:1pxsolid#ccc;
    }

    .Tb_BodyCSS2 tr
    {
        height:30px;
    }
    .Tb_BodyCSS2 td
    {
        height:30px;
        border-right:1px #d5d5d5 solid;
        border-top:1px #d5d5d5 solid;
    }
        
    .Tb_BodyCSS2 {
        border-collapse:collapse;
        border-spacing:0;
        border:1pxsolid#ccc;
    }


    .popupPreView
    {
        width: 1100px;
        height: 600px;

        position: absolute;
        padding: 1px;
        z-index: 11000;
        display: none;
        background-color: White;
    
    
        top: 15%;
        left: 25%;

        margin-left:-150px!important;/*FF IE7 该值为本身宽的一半 */
        margin-top:-50px!important;/*FF IE7 该值为本身高的一半*/
        margin-left:0px;
        margin-top:0px;

        position:fixed!important;/*FF IE7*/
        position:absolute;/*IE6*/

        _top:       expression(eval(document.compatMode &&
                    document.compatMode=='CSS1Compat') ?
                    documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
                    document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/
        _left:      expression(eval(document.compatMode &&
                    document.compatMode=='CSS1Compat') ?
                    documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 :/*IE6*/
                    document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2);/*IE5 IE5.5*/

    }

    .flipy{
            -moz-transform: scaleY(-1);
            -webkit-transform: scaleY(-1);
            -o-transform: scaleY(-1);
            transform: scaleY(-1); /*IE*/
            filter:FlipH FlipV;
            
            -moz-transform: rotate(180deg);
            -webkit-transform: rotate(180deg);
            -o-transform: rotate(180deg);
    }
    </style>

    <script language="javascript" type="text/javascript">
        function SetImagePreview(hid, otsd, oted, fst, oid, fnu) {
            AJImagePreviewList(hid, otsd, oted, fst, oid, fnu);
        }

        function bbimg(o) {
            var zoom = parseInt(o.style.zoom, 10) || 100;
            zoom += event.wheelDelta / 12;
            if (zoom > 0) o.style.zoom = zoom + '%';
            return false;
        }

        function wheel(event) {
            var delta = 0;
            var div = document.getElementById("<%=dvToBackPre.ClientID%>");
            if (!event) /**//* For IE. */
            {
                event = window.event;
            }
            if (event.wheelDelta) {
                //让一个是IE
                //if(event.srcElement.tagName=="IMG" && div.contains(event.srcElement))
                //{
                //event.srcElement.width+=event.wheelDelta/15;
                // return false;
                //}
            }
            else if (event.detail) {
                //如果是firefox
                var allImg = div.childNodes
                var isThis = false; //现判断鼠标中仑的元素是不是包含在那个div里面
                for (i = 0; i < allImg.length; i++) {
                    if (allImg[i] == event.target) {
                        isThis = true;
                    }
                }
                if (isThis && event.target.tagName == "INPUT") {
                    $(event.target).width(event.target.width + event.detail * 12);
                    $(event.target).height(event.target.height + event.detail * 12);
                    event.returnValue = false;
                }
            }
            return true;
        }

        function RotateRightImage() {
            var div = document.getElementById("<%=dvToBackPre.ClientID%>");
            var lis = div.childNodes;
            for (var i = 0; i < lis.length; i++) {
                if (lis.item(i).className == "") {
                    lis.item(i).setAttribute("class", "flipy");
                    lis.item(i).setAttribute("className", "flipy");
                } else {
                    lis.item(i).setAttribute("class", "");
                    lis.item(i).setAttribute("className", "");
                }
                lis.item(i).onmousewheel = function () { return bbimg(this) };
            }
        }

        function changeImg(btn, arg) //鼠标移入，更换图片
        {
            if (arg == "0") {
                btn.src = "../../Images/antclock_press.png";
            }
            else {
                btn.src = "../../Images/clock_press.png";
            }
        }
        function changeback(btn, arg)  //鼠标移出，换回原来的图片
        {
            if (arg == "0") {
                btn.src = "../../Images/antclock.png";
            }
            else {
                btn.src = "../../Images/clock.png";
            }
        }

        function doPrint() {
            var time = new Date();
            window.showModalDialog("OrderConfirmInfoImagePrint.aspx?ID=" + document.getElementById("<%=hidimgToBackPre.ClientID%>").value + "&time=" + time, window, "dialogWidth:800px;dialogHeight:750px;center:yes;status:no;scroll:yes;help:no;");
        }

    function PreViewFaxImg(imgToBack, aLink, atype) {
        document.getElementById("<%=hidimgToBackPre.ClientID%>").value = imgToBack;
        var output = "";
        if (imgToBack.indexOf(",") != -1) {
            var arrmp = imgToBack.split(",");
            for (i = 0; i < arrmp.length; i++) {
                if (arrmp[i] != "") {
                    output += "<input type=\"image\" id=\"imagediv\" src='" + arrmp[i] + "' onclick=\"return false;\" onload=\"javascript\:if(this.width>screen.width-500)this.style.width=screen.width-500;\" onmousewheel=\"return bbimg(this)\" alt=\"\" style=\"width:100%;height:1100px;\"/><br/>";
                }
            }
        }
        else {
            output += "<input type=\"image\" id=\"imagediv\" src='" + imgToBack + "' onclick=\"return false;\" onload=\"javascript\:if(this.width>screen.width-500)this.style.width=screen.width-500;\" onmousewheel=\"return bbimg(this)\" alt=\"\" style=\"width:100%;height:1100px;\"/>";
        }
        document.getElementById("<%=dvToBackPre.ClientID%>").innerHTML = output;

        if (window.addEventListener) {
            window.addEventListener('DOMMouseScroll', wheel, false); //给firefox添加鼠标滚动事件
        }

        var tbList = document.getElementById("tbPreList");
        if (tbList != null) {
            var rows = tbList.rows;
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].cells(0).innerText == aLink) {
                    rows[i].style.backgroundColor = "#f6f6f6";
                    rows[i].onmouseover = "";
                    rows[i].onmouseout = "";
                }
                else {
                    if (rows[i].cells(1).innerText == "0") {
                        rows[i].style.backgroundColor = "White";
                    }
                    else {
                        rows[i].style.backgroundColor = "#E9EBF2";
                    }
                    rows[i].onmouseover = function () { d = this.style.backgroundColor; this.style.backgroundColor = '#f6f6f6' };
                    rows[i].onmouseout = function () { this.style.backgroundColor = d };
                }
            }
        }
    }

    function AJImagePreviewList(hid, otsd, oted, fst, oid, fnu) {
        $.ajax({
            contentType: "application/json",
            url: "OrderApprovedFaxPreView.aspx/SetImagePreviewList",
            type: "POST",
            dataType: "json",
            data: "{Hid:'" + hid + "',Otsd:'" + otsd + "',Oted:'" + oted + "',Fst:'" + fst + "',Oid:'" + oid + "',Fnu:'" + fnu + "'}",
            success: function (data) {
                var output = "<table id=\"tbPreList\" class=\"Tb_BodyCSS2\" style=\"border:1px #d5d5d5 solid;margin-left:1px;padding:1px;width:99.8%;\">";
                var d = jQuery.parseJSON(data.d);
                var sendFx = "-1";
                var inum = 0;
                $.each(d, function (i) {
                    if (sendFx != d[i].FAXNID) {
                        output += "<tr style=\"background-color:#E9EBF2\" onmouseover=\"d=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'\" onmouseout=\"this.style.backgroundColor=d\">"
                        inum = inum + 1
                        output += "<td style=\"display:none;\" align=\"center\">" + inum + "</td>";
                        output += "<td style=\"display:none;\" align=\"center\">1</td>";
                        output += "<td style=\"width:100%;\" align=\"left\" onclick=\"PreViewFaxImg('" + d[i].FAXTURL + "', '" + inum + "')\"><span id=\"aPreimage\" style=\"margin-left:5px;margin-right:10px;width:100%\">[" + d[i].FAXTST + "]&nbsp;" + d[i].FAXDT + "</span></td>";
                        output += "</tr>";

                        if (d[i].FAXBST != "") {
                            inum = inum + 1
                            output += "<tr onmouseover=\"d=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'\" onmouseout=\"this.style.backgroundColor=d\">"
                            output += "<td style=\"display:none;\" align=\"center\">" + inum + "</td>";
                            output += "<td style=\"display:none;\" align=\"center\">0</td>";
                            output += "<td style=\"width:100%;\" align=\"right\" onclick=\"PreViewFaxImg('" + d[i].FAXBURL + "', '" + inum + "')\"><span id=\"aPreimage\" style=\"margin-right:10px;width:100%\">[" + d[i].FAXBST + "]&nbsp;" + d[i].FAXBDT + "</span></td>";
                            output += "</tr>";
                        }
                        sendFx = d[i].FAXNID;
                    }
                    else {
                        inum = inum + 1
                        output += "<tr onmouseover=\"d=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6'\" onmouseout=\"this.style.backgroundColor=d\">"
                        output += "<td style=\"display:none;\" align=\"center\">" + inum + "</td>";
                        output += "<td style=\"display:none;\" align=\"center\">0</td>";
                        output += "<td style=\"width:100%;\" align=\"right\" onclick=\"PreViewFaxImg('" + d[i].FAXBURL + "', '" + inum + "')\"><span id=\"aPreimage\" style=\"margin-right:10px;width:100%\">[" + d[i].FAXBST + "]&nbsp;" + d[i].FAXBDT + "</span></td>";
                        output += "</tr>";
                    }
                });
                output += "</table>";
                document.getElementById("<%=dvImagePreList.ClientID%>").innerHTML = output;
            },
            error: function (json) {
            }
        });
    }
    </script>
</head>
<body style="overflow:scroll;overflow-x:hidden" >
    <form id="form1" runat="server">
    <table width="100%;height:100%">
            <tr>
                <td style="width:20%;height:100%" valign="top" align="right">
                    <table style="border:0px; padding:0px; margin:0px 0px 0px 2px;width:101%">
                        <tr align="left">
                            <td align="left" valign="middle" style="background-color:#6379B2; height:30px; padding:5px 5px 3px 9px; font-weight:bold;color:White;">传真详情</td>
                        </tr>
                    </table>
                    <div ID="dvImagePreList" runat="server" style="width: 99.9%; height:850px; overflow-y:auto;margin-left:3px;margin-top:-3px">
                    </div>
                </td>
                <td style="width: 80%;height:100%" valign="top" align="left">
                    <table style="border:0px; padding:0px; margin:0px 0px 0px 5px;width:100%">
                    <tr align="left">
                        <td align="left" valign="middle" style="background-color:#6379B2; height:30px; padding:5px 5px 3px 10px; font-weight:bold;color:White;">
                            <div style="float:left;margin-top:5px">
                                传真预览
                            </div>
                            <div style="float:left;margin-left:15px;margin-top:-3px">
                                <img alt="" style="width:30px;height:30px" src="../../Images/clock.png" title="传真向右旋转90°"onmousedown="changeImg(this, '1')" onmouseout="changeback(this, '1')" onclick="RotateRightImage()"/>
                            </div>
                            <div style="float:right;margin-right:5px">
                                <input type="button" class="btn" id="btnPrintImage" value='打印' onclick="doPrint()"/>
                            </div>
                        </td>
                    </tr>
                </table>
                    <table runat="server" id="tbPreList" class="Tb_BodyCSS2" style="border:1px #d5d5d5 solid; padding:1px; margin:-3px 0px 0px 7px;width:99.6%">
                        <tr style="height:750px;width:100%">
                            <td style="height:750px;width:100%">
                                <div runat="server" id="dvToBackPre" style="float:left;width:100%; height:850px; overflow-y:auto;">
                                    <%--<asp:Image runat="server" ID="imgToBackPre" Width="100%" Height="900px" />--%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidimgToBackPre" runat="server" />
    </form>
</body>
</html>
