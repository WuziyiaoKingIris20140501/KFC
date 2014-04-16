<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="CreateUserGroupPage.aspx.cs"  Title="用户相关" Inherits="CreateUserGroupPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtUserGroupNM.ClientID%>").value = "";
        document.getElementById("<%=hidRegChannelList.ClientID%>").value = "";
        document.getElementById("<%=hidPlatformList.ClientID%>").value = "";
        document.getElementById("<%=dpRegistStart.ClientID%>").value = "";
        document.getElementById("<%=dpRegistEnd.ClientID%>").value = "";
        document.getElementById("<%=dpLoginStart.ClientID%>").value = "";
        document.getElementById("<%=dpLoginEnd.ClientID%>").value = "";
        document.getElementById("<%=txtSubmitOrderFrom.ClientID%>").value = "";
        document.getElementById("<%=txtSubmitOrderTo.ClientID%>").value = "";
        document.getElementById("<%=txtCompleteOrderFrom.ClientID%>").value = "";
        document.getElementById("<%=txtCompleteOrderTo.ClientID%>").value = "";
        document.getElementById("<%=dpLastOrderStart.ClientID%>").value = "";
        document.getElementById("<%=dpLastOrderEnd.ClientID%>").value = "";
        document.getElementById("<%=txtManualAdd.ClientID%>").value = "";


        var chkObject = document.getElementById("<%=chkPlatformList.ClientID%>");
        if (chkObject != null) {
            var chkInput = chkObject.getElementsByTagName("input");
            for (var i = 0; i < chkInput.length; i++) {
                if (chkInput[i].type = "checkbox") {
                    if (chkInput[i].checked)
                        chkInput[i].checked = false;
                        chkInput[i].disabled = true;
                }
            }
        }

            document.getElementById("<%=chkPlatformList.ClientID%>").checked = true;
            document.getElementById("<%=chkAll.ClientID%>").checked = true;
            document.getElementById("<%=chkAllRegChannel.ClientID%>").checked = true;
            document.getElementById("<%=chkRegistUnTime.ClientID%>").checked = true;
            document.getElementById("<%=chkLoginUnTime.ClientID%>").checked = true;
            document.getElementById("<%=chkSubmitOrderUn.ClientID%>").checked = true;
            document.getElementById("<%=chkCompleteOrderUn.ClientID%>").checked = true;
            document.getElementById("<%=chkLastOrderUnTime.ClientID%>").checked = true;

        var btnObject = document.getElementById("dvRegChannelList");
        var btnInput = btnObject.getElementsByTagName("input");
        var btnLength = btnInput.length;
        for (var i = btnLength -1; i >= 0; i--) {
            if (btnInput[i].type = "button") {
                btnObject.removeChild(btnInput[i]);
            }
        }
        document.getElementById("btnAddRegChannel").disabled = true;
        document.getElementById("<%=ddpRegChannelList.ClientID%>").disabled = true;
        document.getElementById("<%=ddpRegChannelList.ClientID%>").selectedIndex = 0;


        document.getElementById("<%=dpRegistStart.ClientID%>").disabled = true;
        document.getElementById("<%=dpRegistEnd.ClientID%>").disabled = true;

        document.getElementById("<%=dpLoginStart.ClientID%>").disabled = true;
        document.getElementById("<%=dpLoginEnd.ClientID%>").disabled = true;

        document.getElementById("<%=dpLastOrderStart.ClientID%>").disabled = true;
        document.getElementById("<%=dpLastOrderEnd.ClientID%>").disabled = true;

        document.getElementById("<%=txtSubmitOrderFrom.ClientID%>").disabled = true;
        document.getElementById("<%=txtSubmitOrderTo.ClientID%>").disabled = true;

        document.getElementById("<%=txtCompleteOrderFrom.ClientID%>").disabled = true;
        document.getElementById("<%=txtCompleteOrderTo.ClientID%>").disabled = true;
    }

    function SetddpRegChannelList() {
        //var chkobject = document.getElementById("<%=chkAllRegChannel.ClientID%>");
        //var chkInput = document.getElementsByTagName("chkAllRegChannel");

        if (document.getElementById("<%=chkAllRegChannel.ClientID%>").checked == true) {
        //if (chkInput.length > 0 && chkInput[0].type == "checkbox" && chkInput[0].checked) {

            var btnObject = document.getElementById("dvRegChannelList");
            var btnInput = btnObject.getElementsByTagName("input");
            var btnLength = btnInput.length;
            for (var i = btnLength - 1; i >= 0; i--) {
                if (btnInput[i].type = "button") {
                    btnObject.removeChild(btnInput[i]);
                }
            }
            document.getElementById("<%=ddpRegChannelList.ClientID%>").selectedIndex = 0;
            document.getElementById("btnAddRegChannel").disabled = true; 
            document.getElementById("<%=ddpRegChannelList.ClientID%>").disabled = true;
        }
        else {
            document.getElementById("btnAddRegChannel").disabled = false; 
            document.getElementById("<%=ddpRegChannelList.ClientID%>").disabled = false;
        }
    }

    function SetchkPlatformList() {

        if (document.getElementById("<%=chkAll.ClientID%>").checked == true) {
            var idchkList = "";
            var chkObject = document.getElementById("<%=chkPlatformList.ClientID%>");
            if (chkObject != null) {
                var chkInput = chkObject.getElementsByTagName("input");
                for (var i = 0; i < chkInput.length; i++) {
                    if (chkInput[i].type = "checkbox") {
                        chkInput[i].checked = false;
                        chkInput[i].disabled = true;
                    }
                }
            }
        }
        else {
            var idchkList = "";
            var chkObject = document.getElementById("<%=chkPlatformList.ClientID%>");
            if (chkObject != null) {
                var chkInput = chkObject.getElementsByTagName("input");
                for (var i = 0; i < chkInput.length; i++) {
                    if (chkInput[i].type = "checkbox") {
                        chkInput[i].disabled = false;
                    }
                }
            }
        }
    }

    function SetMsgSaveClick() {
        document.getElementById("<%=messageContent.ClientID%>").innerHTML = "用户组信息创建中，请稍后...";
    }

    function SetchkRegistUnTime() {
        if (document.getElementById("<%=chkRegistUnTime.ClientID%>").checked == true) {
            document.getElementById("<%=dpRegistStart.ClientID%>").value = "";
            document.getElementById("<%=dpRegistEnd.ClientID%>").value = "";

            document.getElementById("<%=dpRegistStart.ClientID%>").disabled = true;
            document.getElementById("<%=dpRegistEnd.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=dpRegistStart.ClientID%>").disabled = false;
            document.getElementById("<%=dpRegistEnd.ClientID%>").disabled = false;
        }
    }

    function SetchkLoginUnTime() {
        if (document.getElementById("<%=chkLoginUnTime.ClientID%>").checked == true) {
            document.getElementById("<%=dpLoginStart.ClientID%>").value = "";
            document.getElementById("<%=dpLoginEnd.ClientID%>").value = "";

            document.getElementById("<%=dpLoginStart.ClientID%>").disabled = true;
            document.getElementById("<%=dpLoginEnd.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=dpLoginStart.ClientID%>").disabled = false;
            document.getElementById("<%=dpLoginEnd.ClientID%>").disabled = false;
        }
    }

    function SetchkLastOrderUnTime() {
        if (document.getElementById("<%=chkLastOrderUnTime.ClientID%>").checked == true) {
            document.getElementById("<%=dpLastOrderStart.ClientID%>").value = "";
            document.getElementById("<%=dpLastOrderEnd.ClientID%>").value = "";

            document.getElementById("<%=dpLastOrderStart.ClientID%>").disabled = true;
            document.getElementById("<%=dpLastOrderEnd.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=dpLastOrderStart.ClientID%>").disabled = false;
            document.getElementById("<%=dpLastOrderEnd.ClientID%>").disabled = false;
        }
    }

    function SetchkSubmitOrderUn() {
        if (document.getElementById("<%=chkSubmitOrderUn.ClientID%>").checked == true) {
            document.getElementById("<%=txtSubmitOrderFrom.ClientID%>").value = "";
            document.getElementById("<%=txtSubmitOrderTo.ClientID%>").value = "";

            document.getElementById("<%=txtSubmitOrderFrom.ClientID%>").disabled = true;
            document.getElementById("<%=txtSubmitOrderTo.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=txtSubmitOrderFrom.ClientID%>").disabled = false;
            document.getElementById("<%=txtSubmitOrderTo.ClientID%>").disabled = false;
        }
    }

    function SetchkCompleteOrderUn() {
        if (document.getElementById("<%=chkCompleteOrderUn.ClientID%>").checked == true) {
            document.getElementById("<%=txtCompleteOrderFrom.ClientID%>").value = "";
            document.getElementById("<%=txtCompleteOrderTo.ClientID%>").value = "";

            document.getElementById("<%=txtCompleteOrderFrom.ClientID%>").disabled = true;
            document.getElementById("<%=txtCompleteOrderTo.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=txtCompleteOrderFrom.ClientID%>").disabled = false;
            document.getElementById("<%=txtCompleteOrderTo.ClientID%>").disabled = false;
        }
    }

    function BtnAddRegChannel() {
        var btnid = "btnRegChannel_" + document.getElementById("<%=ddpRegChannelList.ClientID%>").value;
        if (document.getElementById(btnid) != null)
        {
            return;
        }

        if (document.getElementById("<%=ddpRegChannelList.ClientID%>").options.length == 0) {
            return;
        }
        var board = document.getElementById("dvRegChannelList");
        var e = document.createElement("input");
        e.type = "button";
        e.setAttribute("id", btnid);
        e.value = document.getElementById("<%=ddpRegChannelList.ClientID%>").options[document.getElementById("<%=ddpRegChannelList.ClientID%>").selectedIndex].text + "   ";
        //document.getElementById("<%=ddpRegChannelList.ClientID%>").value;
        e.setAttribute("style", "background-color: Transparent;background-image: url(../../images/imageButton.png); background-position: right center;background-repeat: no-repeat");
        e.onclick = function () {
            e.parentNode.removeChild(this);
        }
        board.appendChild(e);
    }

    function SetRegChannelList() {
        var idList = "";
        var btnObject = document.getElementById("dvRegChannelList");
        var btnInput = btnObject.getElementsByTagName("input");
        var btnLength = btnInput.length;
        for (var i = btnLength - 1; i >= 0; i--) {
            if (btnInput[i].type = "button") {
                idList = idList + btnInput[i].id + ',';
            }
        }

        document.getElementById("<%=hidRegChannelList.ClientID%>").value = idList;

        SetPlatformList();
    }

    function SetPlatformList() {
        var idchkList = "";
        var chkObject = document.getElementById("<%=chkPlatformList.ClientID%>");
        if (chkObject != null) {
            var chkInput = chkObject.getElementsByTagName("input");
            for (var i = 0; i < chkInput.length; i++) {
                if (chkInput[i].type = "checkbox") {
                    if (chkInput[i].checked)
                        idchkList = idchkList + chkInput[i].value + ','
                }
            }
        }
        document.getElementById("<%=hidPlatformList.ClientID%>").value = idchkList;
    }


//    $(document).ready(function () {

//        $('#btnAddUserGroup').click(function () {

//            var param = ''
//            param += 'UserGroupNM:{' + document.getElementById("<%=txtUserGroupNM.ClientID%>").value + '},'
//            param += 'RegChannelList:{' + document.getElementById("<%=hidRegChannelList.ClientID%>").value + '},'
//            param += 'chkPlatformList:{' + document.getElementById("<%=hidPlatformList.ClientID%>").value + '},'
//            param += 'dpRegistStart:{' + document.getElementById("<%=dpRegistStart.ClientID%>").value + '},'
//            param += 'dpRegistEnd:{' + document.getElementById("<%=dpRegistEnd.ClientID%>").value + '},'
//            param += 'dpLoginStart:{' + document.getElementById("<%=dpLoginStart.ClientID%>").value + '},'
//            param += 'dpLoginEnd:{' + document.getElementById("<%=dpLoginEnd.ClientID%>").value + '},'
//            param += 'txtSubmitOrderFrom:{' + document.getElementById("<%=txtSubmitOrderFrom.ClientID%>").value + '},'
//            param += 'txtSubmitOrderTo:{' + document.getElementById("<%=txtSubmitOrderTo.ClientID%>").value + '},'
//            param += 'txtCompleteOrderFrom:{' + document.getElementById("<%=txtCompleteOrderFrom.ClientID%>").value + '},'
//            param += 'txtCompleteOrderTo:{' + document.getElementById("<%=txtCompleteOrderTo.ClientID%>").value + '},'
//            param += 'dpLastOrderStart:{' + document.getElementById("<%=dpLastOrderStart.ClientID%>").value + '},'
//            param += 'dpLastOrderEnd:{' + document.getElementById("<%=dpLastOrderEnd.ClientID%>").value + '},'
//            param += 'txtManualAdd:{' + document.getElementById("<%=txtManualAdd.ClientID%>").value + '}'

//            $.ajax({
//                type: "POST",   //访问WebService使用Post方式请求
//                contentType: "application/json", //WebService 会返回Json类型
//                url: "CreateUserGroupPage.aspx/AddUserGroup", //调用WebService的地址和方法名称组合 ---- WsURL/方法名
//                data: "{parm:'" + param + "'}", //这里是要传递的参数，格式为 data: "{paraName:paraValue}",下面将会看到       
//                dataType: 'json',
//                success: function (result) {     //回调函数，result，返回值
//                    alert(result.d);
//                }
//            });
//        });
//    });

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
        var retunValue = window.open("UserGroupDetailPage.aspx?ID=" + arg + "&time=" + time, null, fulls);
        //window.location.href = "UserGroupDetailPage.aspx?ID=" + arg + "&time=" + time;
    }

//    function SetDataTable() {
//        $.ajax({
//            type: "POST",
//            url: "<%=Request.Url.ToString() %>/GetDataTable",  /* 注意后面的名字对应CS的方法名称 */
//            data: "{}", /* 注意参数的格式和名称 */
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (result) {
//                data = jQuery.parseJSON(result.d);  /*这里是否解析要看后台返回的数据格式，如果不返回表名则无需要 parseJSON */
//                t = "<table border='1' >";
//                t += "<tr class='GView_HeaderCSS' style='background-color:#C6C3C6;color:Black;' align='center'><td style='width:5%'><b>用户组ID</b></td><td style='width:15%'><b>用户组名</b></td><td style='width:16%'><b>创建时间</b></td><td style='width:10%'><b>用户数</b></td><td style='width:20%'><b>注册时间</b></td><td style='width:20%'><b>最后登录时间</b></td><td style='width:7%'><b>历史订单</b></td><td style='width:7%'><b>成功订单</b></td></tr>";
//                $.each(data.BlogUser, function (i, item) { /* BlogUser是返回的表名 */
//                    t += "<tr class='GView_ItemCSS' align='center'>";
//                    t += "<td><a href='#' onclick='PopupArea(" + item.UserId + ")'><font color='blue'>" + item.UserId + "</font></a></td>";
//                    t += "<td>" + item.UserName + "</td>";
//                    t += "<td><a href='#' onclick='PopupArea(" + item.UserId + ")'><font color='blue'>" + item.UserName + "</font></a></td>";

//                    t += "</tr>";
//                })
//                t += "</table>";
//                $("#result").html(t);
//            }
//        });
//    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server"></asp:ScriptManager>
  <div id="right">
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
      <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">添加用户组</li>
        <li>用户组名：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input name="textfield" type="text" id="txtUserGroupNM" runat="server" style="width:450px;" maxlength="32" value=""/>
            <font color="red">*</font>
        </li>
        <li>
           <%-- <asp:Button ID="btnAddRegChannel" Width="60px" Height="20px" Text="添加" onclick="BtnAddRegChannel()" />--%>
            <table>
            <tr>
                <td>
                    注册渠道：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddpRegChannelList" CssClass="noborder_inactive" runat="server"></asp:DropDownList>
                    <input id="btnAddRegChannel" type ="button" value ="添加" class="btn primary" onclick ="BtnAddRegChannel()" />
                    <input type="checkbox" name="chkAllRegChannel" id="chkAllRegChannel" runat="server" onclick="SetddpRegChannelList()"/>不限制
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div id="dvRegChannelList" style="width:800px"/>
                </td>
            </tr>
            </table>
           <%-- <div id="dvRegChannelList">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div> --%>
        </li>  
        <li>
            <table>
                <tr>
                    <td>
                        注册平台：&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                         <asp:CheckBoxList ID="chkPlatformList" runat="server" RepeatDirection="Vertical" RepeatColumns="10" RepeatLayout="Flow" />
                    </td>
                    <td>
                        <input type="checkbox" name="checkbox" id="chkAll" runat="server" onclick="SetchkPlatformList()"/>不限制
                    </td>
                </tr>
            </table>
        </li>
        <li>注册时间：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <input id="dpRegistStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpRegistEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpRegistEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpRegistStart\')}',maxDate:'2020-10-01'})" runat="server"/>
          <input type="checkbox" name="checkbox" id="chkRegistUnTime" runat="server" onclick="SetchkRegistUnTime()"/>
          不限制
        </li>
         <li>最后登录时间：
          <input id="dpLoginStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpLoginEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpLoginEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpLoginStart\')}',maxDate:'2020-10-01'})" runat="server"/>
          <input type="checkbox" name="checkbox" id="chkLoginUnTime" runat="server" onclick="SetchkLoginUnTime()"/>
          不限制
          <label for="checkbox"></label>
        </li>
        <li>提交订单：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input name="textfield" type="text" id="txtSubmitOrderFrom" value="" runat="server" maxlength="4" style="width:40px;"/>

            到
            <input name="textfield" type="text" id="txtSubmitOrderTo" value="" runat="server" maxlength="4" style="width:40px;"/>
            <input type="checkbox" name="checkbox" id="chkSubmitOrderUn" runat="server" onclick="SetchkSubmitOrderUn()"/>
              不限制
            <label for="checkbox"></label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            成功订单：&nbsp;&nbsp;&nbsp;
            <input name="textfield" type="text" id="txtCompleteOrderFrom" value="" runat="server" maxlength="4" style="width:40px;"/>
            到
            <input name="textfield" type="text" id="txtCompleteOrderTo" value="" runat="server" maxlength="4" style="width:40px;"/>
            <input type="checkbox" name="checkbox" id="chkCompleteOrderUn" runat="server" onclick="SetchkCompleteOrderUn()"/>
              不限制
            <label for="checkbox"></label>
        </li>
        <li>最近订单时间：
          <input id="dpLastOrderStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpLastOrderEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpLastOrderEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpLastOrderStart\')}',maxDate:'2020-10-01'})" runat="server"/>
          <input type="checkbox" name="checkbox" id="chkLastOrderUnTime" runat="server" onclick="SetchkLastOrderUnTime()"/>
          不限制
          <label for="checkbox"></label>
        </li>
        <li>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" valign="top">
                        手动添加：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtManualAdd" runat="server" TextMode="MultiLine" Width="500px" Height="200px"></asp:TextBox>

                        <span style="color:#AAAAAA">&nbsp;请输入手机号码，多个手机号码中间用 "," 分隔</span>
                    </td>
                </tr>
            </table>
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAddUserGroup" runat="server" CssClass="btn primary" Text="添加用户组" OnClientClick="SetRegChannelList();SetMsgSaveClick();" onclick="btnSave_Click" />

            <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent();" />
        </li>
      </ul>
    </div>
      </ContentTemplate>
   </asp:UpdatePanel>
   
  <%-- <AntarDev:ProgressBar ID="ProgressBar1" runat="server" Font-Size="11pt" ForeColor="#3399FF"
    ImageSet="PurpleStripes" Maximum="100" Minimum="0" Step="10" Text="{1}" TextAlign="Center" />
<br />--%>

   <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server" >
      <ContentTemplate>
   <div class="frame01"><ul><li><div id="messageContent" runat="server" style="color:red;width:800px;"></div></li></ul></div>
    <div class="frame02">
       <%-- <div id="result"></div>--%>
        <%--<div id="messageContent" runat="server" style="color:red;"></div>  --%>
        <asp:GridView ID="gridViewCSCreateUserGroupList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSCreateUserGroupList_RowDataBound"  OnRowEditing="gridViewCSCreateUserGroupList_RowEditing"
                            OnRowUpdating="gridViewCSCreateUserGroupList_RowUpdating" OnRowCancelingEdit="gridViewCSCreateUserGroupList_RowCancelingEdit" CssClass="GView_BodyCSS">
                <Columns>
                     <asp:BoundField DataField="ID" HeaderText="用户组ID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="用户组名">
                        <ItemTemplate>
                           <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>')"><%# DataBinder.Eval(Container.DataItem, "USERGROUPNAME")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="CREATETIME" HeaderText="创建时间" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="USERCOUNT" HeaderText="用户数" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="REGISTTIME" HeaderText="注册时间" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="LOGINTIME" HeaderText="最后登录时间" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="SUBMITORDER" HeaderText="历史订单" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                     <asp:BoundField DataField="COMPLETEORDER" HeaderText="成功订单" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
    <div class="frame01"><li class="button">&nbsp;最近更新的10个用户组</li></div>

 </ContentTemplate>
   </asp:UpdatePanel>
   </div>
<asp:HiddenField ID="hidRegChannelList" runat="server"/>
<asp:HiddenField ID="hidPlatformList" runat="server"/>
</asp:Content>