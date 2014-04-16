<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.master"
    CodeFile="PromotionTypeManager.aspx.cs" Inherits="WebUI_Promotion_PromotionTypeManager"
    Title="促销方式" %>

<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script src="../../Scripts/lhgcore.min.js" type="text/javascript"></script>
    <script src="../../Scripts/lhgdialog.min.js" type="text/javascript"></script>
    <%-- <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.0/themes/base/jquery-ui.css" />--%>
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script src="http://code.jquery.com/ui/1.9.0/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript">
        function ClearClickEvent() {
            document.getElementById("<%=txtPromotionTypeName.ClientID%>").value = "";
        }

        function PopupArea(id, name, seq) {
            var obj = new Object();
            obj.id = id;
            obj.name = name;
            obj.seq = seq;
            var time = new Date();
            var retunValue = window.showModalDialog("PromotionTypeDetail.aspx?id=" + id + "&name=" + name + "&seq=" + seq + "&time=" + time, obj, "dialogWidth=800px;dialogHeight=400px");
            if (retunValue) {
                document.getElementById("<%=btnSearch.ClientID%>").click();
            }
        }
        //显示弹出的层
        function invokeOpen2(id, name, seq) {
            document.getElementById("<%=hidden1.ClientID%>").value = id;
            document.getElementById("<%=divTxtSelPromotionTypeName.ClientID%>").value = name;
            document.getElementById("<%=hidden2.ClientID%>").value = seq;
            if (seq != "" && seq != null) {
                document.getElementById("<%=topRd.ClientID%>").checked = true;
            } else {
                document.getElementById("<%=btmRd.ClientID%>").checked = true;
            }
            document.getElementById("updateDiv").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
        }

        //隐藏弹出的层
        function invokeClose2() {
            document.getElementById("updateDiv").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }
    </script>
    <style type="text/css">
        #module_list
        {
            margin-left: 0px;
        }
        .modules
        {
            float: left;
            width: 100%;
            border: 1px solid #D5D5D5;
        }
        .m_title
        {
            height: 30px;
            display: -moz-inline-box;
            display: inline-block;
            width: 30%;
            margin-top: 10px;
        }
        #loader
        {
            text-align: center;
        }
        .updateDiv
        {
            width: 240px;
            height: 185px;
            top: 30%;
            left: 43%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            text-align: center;
            border: solid 2px #ff8300;
            z-index: 10001;
            display: none;
            background-color: White;
        }
        .bgDiv2
        {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            right: 0px;
            background-color: #000000;
            filter: alpha(Opacity=80);
            -moz-opacity: 0.5;
            opacity: 0.5;
            z-index: 100;
            background-color: #000000;
            opacity: 0.6;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".m_title").bind('mouseover', function () {
                $(this).css("cursor", "move")
            });
            var $show = $("#loader");
            var $orderidlist = $("#orderidlist");
            var $ordernamelist = $("#ordernamelist");
            var $list = $("#module_list");

            $list.sortable({
                delay: 10,
                distance: 10,
                opacity: 0.6,
                revert: true,
                cursor: 'move',
                handle: '.m_title',
                update: function () {
                    var new_order = [];
                    $list.children(".modules").each(function () {
                        new_order.push(this.title);
                    });
                    var newid = new_order.join(',');
                    var oldid = $orderidlist.val();
                    var names = $ordernamelist.val();
                    $.ajax({
                        type: "post",
                        url: "PromotionTypeManager.aspx", //服务端处理程序 
                        data: { id: newid, order: oldid, name: names, type: "xx" }, //id:新的排列对应的ID,order：原排列顺序 
                        beforeSend: function () {
                            $show.html("数据更新中，请稍等...");
                        },
                        success: function (msg) {
                            $show.html("");
                            window.location.replace(window.location.href);
                        }
                    });
                }
            });
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="right">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="frame01">
                    <ul>
                        <li class="title">添加促销方式</li>
                        <li>促销方式名称：
                            <input name="textfield" type="text" id="txtPromotionTypeName" value="" runat="server"
                                maxlength="32" /><font color="red">*</font>&nbsp;&nbsp;&nbsp;<input type="checkbox"
                                    id="StickPromotion" runat="server" />置顶促销 </li>
                        <li class="button">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加" OnClick="btnAdd_Click" />
                            <input type="button" id="btnClear" class="btn" value="重置"
                                onclick="ClearClickEvent()" />
                            <li>
                                <div id="messageContent" runat="server" style="color: red">
                                </div>
                            </li>
                    </ul>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div class="frame01" style="display: none">
                    <ul>
                        <li class="title">置顶促销方式管理</li>
                        <li>促销方式名称：
                            <label for="textfield">
                            </label>
                            <input type="text" name="textfield" id="txtSelPromotionTypeName" runat="server" />
                        </li>
                        <li class="button" style="display: none">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClick="btnSearch_Click" />
                        </li>
                    </ul>
                </div>
                <div id="bgDiv2" class="bgDiv2">
                </div>
                <div id="updateDiv" class="updateDiv">
                    <table>
                        <tr>
                            <td colspan="2">
                                促销方式名称：<input type="text" id="divTxtSelPromotionTypeName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                促销方式：<input type="radio" id="topRd" runat="server" />置顶促销&nbsp;&nbsp;<input type="radio" id="btmRd" runat="server" />非置顶促销
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="确定" OnClientClick="invokeClose2()"
                                    OnClick="btnUpdate_Click" />
                            </td>
                            <td>
                                <input type="button" id="btnCancle" class="btn" value="取消" onclick="invokeClose2()" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="frame01">
                    <ul>
                        <li class="title">置顶促销方式管理</li>
                    </ul>
                    <div id="main">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    排序
                                </td>
                                <td style="width: 30%">
                                    名称
                                </td>
                                <td style="width: 30%">
                                    ID
                                </td>
                                <td style="width: 10%">
                                    操作
                                </td>
                            </tr>
                        </table>
                        <div id="loader" >
                            <%--<div id="background" class="pcbackground" style="display: none;">
                            </div>
                            <div id="progressBar" class="pcprogressBar" style="display: none;">
                                数据加载中，请稍等...</div>--%>
                        </div>
                        <div id="module_list">
                            <%if (NewDs.Tables.Count > 0 && NewDs.Tables[0].Rows.Count > 0)
                              {
                                  System.Data.DataRow[] dr = NewDs.Tables[0].Select("seq is not null");
                                  string ids = "";
                                  for (int i = 0; i < NewDs.Tables[0].Rows.Count; i++)
                                  {
                                      if (!string.IsNullOrEmpty(NewDs.Tables[0].Rows[i]["SEQ"].ToString()) && Convert.ToInt32(NewDs.Tables[0].Rows[i]["SEQ"].ToString()) >= 0)
                                      {
                                          ids += NewDs.Tables[0].Rows[i]["ID"].ToString() + ",";
                                      }
                                  }
                                  ids = ids.TrimEnd(',');
                            %>
                            <input type="hidden" id="orderidlist" value='<%=ids %>' />
                            <%
                                System.Data.DataRow[] drr = NewDs.Tables[0].Select("seq is not null");
                                for (int i = 0; i < drr.Length; i++)
                                  {
                                      string seq = drr[i]["SEQ"].ToString();
                                      if (!string.IsNullOrEmpty(seq) && Convert.ToInt32(seq) >= 0)
                                      {
                                          string id = drr[i]["ID"].ToString();
                                          string name = drr[i]["Name"].ToString();
                                  
                                  
                            %>
                            <div class="modules" title='<%=id %>|<%=name %>'>
                                <span class="m_title">
                                    <%=i%></span><span class="m_title">
                                        <%=name%></span><span class="m_title">
                                            <%=id%></span> <span><a href="#" onclick="invokeOpen2('<%=id %>','<%=name %>','<%=seq %>')">
                                                编辑</a></span>
                            </div>
                            <%
                                  }
                              }
                              }%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="frame01">
                    <div>
                        <ul>
                            <li class="title">其他促销方式管理</li>
                        </ul>
                    </div>
                    <div id="Div1">
                        <table width="100%">
                            <tr>
                               <%-- <td style="width: 30%">
                                    序号
                                </td>--%>
                                <td style="width: 30%">
                                    名称
                                </td>
                                <td style="width: 30%">
                                    ID
                                </td>
                                <td style="width: 10%">
                                    操作
                                </td>
                            </tr>
                            <%
                                if (NewDs.Tables[0] != null && NewDs.Tables[0].Rows.Count > 0)
                                {
                                    System.Data.DataRow[] dr = NewDs.Tables[0].Select("seq is null");
                                    for (int i = 0; i < dr.Length; i++)
                                    {
                                        string seq = dr[i]["SEQ"] == null ? "" : dr[i]["SEQ"].ToString();
                                        if (seq == "")
                                        {
                                            string id = dr[i]["ID"].ToString();
                                            string name = dr[i]["Name"].ToString();
                            %>
                            <%-- <tr style="border-bottom: 1px solid #f6f6f6;">--%>
                            <tr style="">
                               <%-- <td style="height: 30px; margin-top: 10px; border-bottom: 1px solid #D5D5D5;">
                                    <%=i%>
                                </td>--%>
                                <td style="height: 30px; margin-top: 10px; border-bottom: 2px solid #D5D5D5;">
                                    <%=name%>
                                </td>
                                <td style="height: 30px; margin-top: 10px; border-bottom: 2px solid #D5D5D5;">
                                    <%=id%>
                                </td>
                                <td style="height: 30px; margin-top: 10px; border-bottom: 2px solid #D5D5D5;">
                                    <a href="#" onclick="invokeOpen2('<%=id %>','<%=name %>','<%=seq %>')">编辑</a>
                                </td>
                            </tr>
                            <%-- <div style="float: left;width: 100%;border: 1px solid #f6f6f6;">
                             <span style="height: 30px;display: -moz-inline-box;display: inline-block;width: 30%;margin-top:10px;"><%=i%></span>
                             <span style="height: 30px;display: -moz-inline-box;display: inline-block;width: 30%;margin-top:10px;"><%=name%></span>
                             <span style="height: 30px;display: -moz-inline-box;display: inline-block;width: 30%;margin-top:10px;"><%=id%></span>
                             <span><a href="#" onclick="invokeOpen2('<%=id %>','<%=name %>','<%=seq %>')">编辑</a></span>
                        </div>--%>
                            <%
                                        }
                                    }
                                }
                            %>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <input type="hidden" name="hid" id="hidden1" runat="server" value="" />
    <input type="hidden" name="hid" id="hidden2" runat="server" value="" />
</asp:Content>
