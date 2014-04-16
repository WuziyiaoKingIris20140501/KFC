<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="SaveIssueManager.aspx.cs"  Title="Issue管理" Inherits="SaveIssueManager"%>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<style type="text/css" >
.pcbackground { 
display: block; 
width: 100%; 
height: 100%; 
opacity: 0.4; 
filter: alpha(opacity=40); 
background:while; 
position: absolute; 
top: 0; 
left: 0; 
z-index: 2000; 
} 
.pcprogressBar { 
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

.checkLT td
{
width:100px;
}

.bgDiv2
{
    display: none;   
    position:absolute;
    top: 0px;
    left: 0px;
    right:0px;
    background-color: #777;
    filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75)
    opacity: 0.6;
}

.popupDiv2
{
    width: 800px;
    height:640px;
    margin-top:50px;
    margin-left:150px;
    position:absolute;
    padding: 1px;
    vertical-align: middle;
    text-align:center;
    border: solid 2px #ff8300;
    z-index: 10001;
    display: none;   
    background-color:White;
}
</style>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=hidIsIndemnify.ClientID%>").value = "0";
        document.getElementById("tdIndPrice").style.display = 'none';
        document.getElementById("<%=txtPackageCode.ClientID%>").value = "";
        document.getElementById("<%=txtPackageCode.ClientID%>").text = "";
        document.getElementById("dvUpdate").style.display = "none";
        document.getElementById("trHis").style.display = "none";
        document.getElementById("dvGridList").style.display = "none";
        document.getElementById("rbtNo").checked = true;

        document.getElementById("wctUser").value = "";
        document.getElementById("wctUser").text = "";
        document.getElementById("Add").value = "选择";
        document.getElementById("tdSendMsg").style.display = "";

        document.getElementById("<%=txtRelatedID.ClientID%>").value = "";
        document.getElementById("<%=txtRelatedID.ClientID%>").text = "";
        document.getElementById("<%=txtRemark.ClientID%>").value = "";
        document.getElementById("<%=txtRemark.ClientID%>").text = "";
        document.getElementById("<%=txtTitle.ClientID%>").value = "";
        document.getElementById("<%=txtTitle.ClientID%>").text = "";
        document.getElementById("<%=txtIndemnifyPrice.ClientID%>").value = "";
        document.getElementById("<%=txtIndemnifyPrice.ClientID%>").text = "";

        document.getElementById("<%=chkAsginTo.ClientID%>").checked = false;
        document.getElementById("<%=chkMsgUser.ClientID%>").checked = false;

        document.getElementById("<%=txtAsginText.ClientID%>").value = "";
        document.getElementById("<%=txtAsginText.ClientID%>").text = "";
        document.getElementById("<%=txtMsgUser.ClientID%>").value = "";
        document.getElementById("<%=txtMsgUser.ClientID%>").text = "";

        document.getElementById("tdmsgAssgin").style.display = "none";
        document.getElementById("tdMsgUser").style.display = "none";

        document.getElementById("<%=ddpRelated.ClientID%>").selectedIndex = 0;
        document.getElementById("<%=ddpPriority.ClientID%>").selectedIndex = 2;
        document.getElementById("<%=ddpStatusList.ClientID%>").selectedIndex = 0;

        document.getElementById('<%=hidUserID.ClientID%>').value = "";
        document.getElementById('<%=hidPageCode.ClientID%>').value = "";

        var vpChkid = document.getElementById("<%=chkIssueType.ClientID%>");
        //得到所有radio
        var vpChkidList = vpChkid.getElementsByTagName("INPUT");
        for (var i = 0; i < vpChkidList.length; i++) {
            vpChkidList[i].checked = false;
        }
    }

    function SerRbtValue(arg) {
        document.getElementById("<%=hidIsIndemnify.ClientID%>").value = arg;
        if (arg == "0") {
            document.getElementById("tdIndPrice").style.display = 'none';
            
        }
        else if (arg == "1") {
            document.getElementById("tdIndPrice").style.display = 'block';
            
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

    function BtnUpdateComplete() {
        BtnCompleteStyle();
        setTimeout("window.opener = null; window.open('', '_self'); window.close();", 2000);
    }

    function SaveBtnCompleteStyle() {
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
        document.getElementById("dvGridList").style.display = "";
    }

    function PageBtnEvent() {
        var pagecode = document.getElementById("<%=txtPackageCode.ClientID%>").value;
        if (pagecode == "")
        {
            AddNew();
        }
        else
        {
            ClearPage();
            document.getElementById("Add").value = "选择";
        }
    }

    function ClearPage() {
        document.getElementById("<%=txtPackageCode.ClientID%>").value = "";
        document.getElementById("<%=txtPackageCode.ClientID%>").text = "";
    }

    function SetBtnStyle(st, val, msg) {
        if ("0" == st) {
            document.getElementById("dvUpdate").style.display = "none";
            document.getElementById("trHis").style.display = "none";
            document.getElementById("dvGridList").style.display = "none"; 
        } else {
            document.getElementById("dvUpdate").style.display = "";
            document.getElementById("dvGridList").style.display = ""; 
        }

        if ("0" == val) {
            document.getElementById("rbtNo").checked = true;
            document.getElementById("tdIndPrice").style.display = 'none';
            
        } else {
            document.getElementById("rbtnYes").checked = true;
            document.getElementById("tdIndPrice").style.display = 'block';
            
        }
        document.getElementById("<%=hidIsIndemnify.ClientID%>").value = val;
        document.getElementById("wctUser").value = document.getElementById('<%=hidUserID.ClientID%>').value;
        document.getElementById("wctUser").text = document.getElementById('<%=hidUserID.ClientID%>').value;

        var pagecode = document.getElementById("<%=txtPackageCode.ClientID%>").value;
        if (pagecode == "") {
            document.getElementById("Add").value = "选择";
        }
        else {
            document.getElementById("Add").value = "清除";
        }

        if ("0" == msg)
        {
            document.getElementById("tdSendMsg").style.display = "";
        }
        else {
            document.getElementById("tdSendMsg").style.display = "none";
        }
    }

    function GetControlVal() {
        document.getElementById('<%=hidUserID.ClientID%>').value = document.getElementById("wctUser").value;
        document.getElementById('<%=hidPageCode.ClientID%>').value = document.getElementById("<%=txtPackageCode.ClientID%>").value;
    }

    function PopupArea() {
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
        var url = document.getElementById("<%=hidLinkUrl.ClientID%>").value;
        window.open(url + "&time=" + time, null, fulls);
    }

    function AddNew() {
        invokeOpen2();
    }
    function invokeOpen2() {
        document.getElementById("popupDiv2").style.display = "block";
        //背景
        var bgObj = document.getElementById("bgDiv2");
        bgObj.style.display = "block";
        bgObj.style.width = document.body.offsetWidth + 100 + "px";
        bgObj.style.height = screen.height + 100 + "px";

        //定义窗口
        //var msgObj=document.getElementById("popupDiv");
        //msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";        
    }

    //隐藏弹出的层
    function invokeClose2() {
        document.getElementById("popupDiv2").style.display = "none";
        document.getElementById("bgDiv2").style.display = "none";
    }

    function ClearSearch() {
        document.getElementById("<%=txtPackageCodeSearch.ClientID %>").value = "";
        document.getElementById("<%=txtPackageNameSearch.ClientID %>").value = "";
    }

    function SetchkAsginTo() {
        if (document.getElementById("<%=chkAsginTo.ClientID%>").checked == true) {
            document.getElementById("<%=hidChkAssginTo.ClientID %>").value = "1";
            document.getElementById("tdmsgAssgin").style.display = "";
            document.getElementById("<%=txtAsginText.ClientID %>").value = document.getElementById("<%=txtTitle.ClientID %>").value

            var idVal = document.getElementById("<%=ddpRelated.ClientID%>").value;
            if ("0" == idVal || "1" == idVal) {
                var relatedid = document.getElementById("<%=txtRelatedID.ClientID%>").value;
                AJRelatedHis(relatedid, idVal);
            }
        }
        else {
            document.getElementById("tdmsgAssgin").style.display = "none";
            document.getElementById("<%=txtAsginText.ClientID %>").value = "";
            document.getElementById("<%=hidChkAssginTo.ClientID %>").value = "0";
        }
        BtnCompleteStyle();
    }

    function AJRelatedHis(key, type) {
        $.ajax({
            async: false,
            contentType: "application/json",
            url: "SaveIssueManager.aspx/SetRelatedVal",
            type: "POST",
            dataType: "json",
            data: "{strKey:'" + key + "',strType:'" + type + "'}",
            success: function (data) {
                document.getElementById("<%=txtAsginText.ClientID %>").value = document.getElementById("<%=txtTitle.ClientID %>").value + data.d;
            }
        });
    }

    function SetchkMsgUser() {
        if (document.getElementById("<%=chkMsgUser.ClientID%>").checked == true) {
            document.getElementById("<%=hidChkMsgUser.ClientID %>").value = "1";
            document.getElementById("tdMsgUser").style.display = "";
        }
        else {
            document.getElementById("tdMsgUser").style.display = "none";
            document.getElementById("<%=hidChkMsgUser.ClientID %>").value = "0";
        }
    }

    function OnChangeRelated() {
        var idVal = document.getElementById("<%=ddpRelated.ClientID%>").value;
        if ("0" == idVal) {
            document.getElementById("tdSendMsg").style.display = "";
        }
        else {
            document.getElementById("tdSendMsg").style.display = "none";
        }
    }

    function SetUserControl(arg, rb) {
        document.getElementById("wctUser").value = arg;
        SerRbtValue(rb);
    }

</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server"></asp:ScriptManager>
  <div id="right">
     <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
     <ContentTemplate>
    <div id="bgDiv2" class="bgDiv2">        
    </div>
    <div id="popupDiv2" class="popupDiv2">
          <table width="98%" align="center" border="0" class="Table_BodyCSS">
            <tr>
                <td style="width:12%;" class="tdcell" >领用券包代码</td>
                <td style="width:28%;" class="tdcell" >
                    <asp:TextBox ID="txtPackageCodeSearch" runat="server" CssClass="textBlurNew" 
                        SkinID="txtchange" Width="90%"></asp:TextBox>
                </td>                    
                <td style="width:12%;"  class="tdcell" >领用券包名称</td>
                <td style="width:28%;" class="tdcell"><asp:TextBox ID="txtPackageNameSearch" 
                        runat="server" CssClass="textBlurNew" SkinID="txtchange"  Width="90%"></asp:TextBox></td>
                
            </tr>
            <tr>                
                <td  align="center" colspan=4 >
                    <asp:Button ID="btnSearch" CssClass="btn primary" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    <input id="Button1" type="button" value="清空" class="btn" onclick="ClearSearch()" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView Width="99%" HorizontalAlign="Center" ID="myGridView"     
              runat="server" AutoGenerateColumns="False"  CssClass="GView_BodyCSS"    
              AllowPaging="True" PageSize="15"  
              onpageindexchanging="myGridView_PageIndexChanging" 
              onselectedindexchanged="myGridView_SelectedIndexChanged">          
             <Columns>
                 <asp:CommandField HeaderText="选择" ShowSelectButton="True"/>

                <asp:BoundField DataField="packagecode" HeaderText="领用券代码">
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                    <HeaderStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="领用券名称" DataField="packagename">
                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="usercnt" HeaderText="总可用次数">
                <ItemStyle HorizontalAlign="Center" Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="startdate" HeaderText="最早可领用时间">
                <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="enddate" HeaderText="最晚可领用时间">
                <ItemStyle Width="15%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ticketrulecode" HeaderText="使用规则代码">
                <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AMOUNT" HeaderText="总金额">
                <ItemStyle Width="10%" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
              <EmptyDataTemplate>             
                           没有满足条件的信息！                 
            </EmptyDataTemplate> 
        </asp:GridView><br />
        <table style="width:99%;" align="center">
            <tr>
                <td align="center"  style="height:22px;">                 
                    <input type="button" value="关闭" id="Button3" name="btnClose" class="btn" onclick="invokeClose2();" />
                </td>
            </tr>
        </table>       
    </div> 
    </ContentTemplate>
     </asp:UpdatePanel>
     <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
     <ContentTemplate>
     <div class="frame01" id="dvUpdate" style="display:none">
      <ul>
        <li class="title">Issue单</li>
        <li>
            <table>
            <tr>
                <td align="right">
                    Issue单ID：
                </td>
                <td>
                    <asp:Label ID="lbIssueID" runat="server" Text="" Width="180" />
                </td>
                <td style="width:30px"></td>
                <td align="right">
                    处理总时长：
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    <asp:Label ID="lbTimeDiffTal" runat="server" Text="" Width="180" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
               <td align="right">
                    创建人：
                </td>
                <td>
                    <asp:Label ID="lbIssueCtPer" runat="server" Text="" Width="180" />
                </td>
                <td style="width:30px"></td>
                <td align="right">
                    创建时间：
                </td>
                <td>
                    <asp:Label ID="lbIssueCtDt" runat="server" Text="" Width="180" />
                </td>
            </tr>
            </table>
        </li>
        <li></li>
     </ul>
     </div>
     <div class="frame01">
      <ul>
        <li class="title"><asp:Label ID="lbCtTitle" runat="server" Text="创建Issue单" Width="180" /></li>
        <li>
            <table>
            <tr>
                <td valign="top">
                    <table>
                         <tr>
                            <td align="right">
                                Title：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTitle" runat="server" Width="345px" MaxLength="150"/>
                                        </td>
                                        <td  align="right">
                                            优先级：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddpPriority" CssClass="noborder_inactive" runat="server" Width="150px"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                分类：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="chkIssueType" CssClass="checkLT" runat="server" RepeatDirection="Vertical" RepeatColumns="5" RepeatLayout="Table" ></asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                Issue指派：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <uc1:WebAutoComplete ID="wctUser" CTLID="wctUser" runat="server" AutoType="user" AutoParent="SaveIssueManager.aspx?Type=user" />
                                        </td>
                                        <td  align="right">
                                            状态：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server" Width="150px"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                是否赔偿：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <input type="radio" name="RbtIndemnify" id="rbtNo" value="0" checked="checked" onclick="SerRbtValue('0')"/>否
                                            <input type="radio" name="RbtIndemnify" id="rbtnYes" value="1" onclick="SerRbtValue('1')"/>是
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                            </td>
                            <td id="tdIndPrice">
                                <table>
                                    <tr>
                                         <td align="right">
                                            赔偿金额：
                                        </td>
                                        <td><asp:TextBox ID="txtIndemnifyPrice" runat="server" Width="145px" MaxLength="10"/></td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                            &nbsp;优惠券礼包 - 金额：
                                            <asp:TextBox ID="txtPackageCode" runat="server" Width="200px" ReadOnly="true"/>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <input type="button" id="Add" value="选择" class="btn primary" onclick="PageBtnEvent()" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                关联类型：
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddpRelated" CssClass="noborder_inactive" runat="server" Width="150px" onchange="OnChangeRelated()"/>
                                        </td>
                                        <td align="right">
                                            关联ID：
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtRelatedID" runat="server" Width="145px" MaxLength="30"/>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                        <asp:Button ID="btnLink" runat="server" CssClass="btn primary" Text="GO" onclick="btnGo_Click" />
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td align="right" valign="top">
                                增加批注：
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtRemark" runat="server" Width="300px" Height="80px" 
                                    TextMode="MultiLine"/>
                            </td>
                        </tr>
                        <tr style="display:none" id="trHis">
                            <td align="right" valign="top">
                                批注历史：
                            </td>
                            <td valign="top">
                                <asp:Label ID="lbIssueHis" runat="server" Text="" Width="630"  Height="120"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                发送短信给指派人<input type="checkbox" name="checkbox" id="chkAsginTo" runat="server" onclick="BtnLoadStyle();SetchkAsginTo()"/>
                            </td>
                            <td valign="top" id="tdmsgAssgin" style="display:none">
                                <asp:TextBox ID="txtAsginText" runat="server" Width="300px" Height="80px" TextMode="MultiLine"/>
                            </td>
                        </tr>
                        <tr id="tdSendMsg">
                            <td align="right" valign="top">
                                发送短信给用户<input type="checkbox" name="checkbox" id="chkMsgUser" runat="server" onclick="SetchkMsgUser()"/>
                            </td>
                            <td valign="top" id="tdMsgUser" style="display:none">
                                <asp:TextBox ID="txtMsgUser" runat="server" Width="300px" Height="80px" TextMode="MultiLine"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
        </li>
        <li></li>
     </ul>
     </div>
     </ContentTemplate>
     </asp:UpdatePanel>

      <div class="frame01">
      <ul>
        <li>
            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
             <ContentTemplate>
                <div id="messageContent" runat="server" style="color:red;"></div>
             </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
             <table>
                <tr>
                     <td style="text-align:left;margin-left:15px">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" OnClientClick="GetControlVal();BtnLoadStyle();" onclick="btnSave_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="重置" onclick="btnClear_Click" />
                    </td>
                </tr>
             </table>
            </ContentTemplate>
                </asp:UpdatePanel>
        </li>
        <li></li>
      </ul>
    </div>
        <div class="frame02" id="dvGridList" style="display:none">
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" >
                <ContentTemplate>
                <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据"
                AllowSorting="true" PageSize="100"  CssClass="GView_BodyCSS">
                <Columns>
                        <asp:BoundField DataField="Title" HeaderText="标题" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="Priority" HeaderText="优先级" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="IssueTypeNM" HeaderText="分类" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="AssignUser" HeaderText="Issue指派" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="DISStatus" HeaderText="状态" ><ItemStyle HorizontalAlign="Center" Width="3%" /></asp:BoundField>

                        <asp:BoundField DataField="IsIndemnifyNM" HeaderText="是否赔偿" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="IndemnifyPrice" HeaderText="赔偿金额" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="TicketPage" HeaderText="优惠券礼包-金额" ><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                        <asp:BoundField DataField="RelatedTypeNM" HeaderText="关联类型" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="RelatedID" HeaderText="关联ID" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="Remark" HeaderText="批注" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="CreateTime" HeaderText="创建时间" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="CreateUser" HeaderText="创建人" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="ActionTime" HeaderText="处理时间" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>

                        <asp:BoundField DataField="ChkAssginMsg" HeaderText="指派人短信" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="AssginMsg" HeaderText="短信内容" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="AssginMsgRS" HeaderText="发送结果" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="ChkUserMsg" HeaderText="用户短信" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="UserMsg" HeaderText="短信内容" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                        <asp:BoundField DataField="UserMsgRS" HeaderText="发送结果" ><ItemStyle HorizontalAlign="Center" Width="5%"/></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
     <div class="frame01">
      <ul>
        <li>
            <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
             <ContentTemplate>
                <div id="background" class="pcbackground" style="display: none; "></div> 
                <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div>
                <asp:HiddenField ID="hidCommonList" runat="server"/>
                <asp:HiddenField ID="hidIsIndemnify" runat="server"/>
                <asp:HiddenField ID="hidIssueID" runat="server"/>
                <asp:HiddenField ID="hidActionType" runat="server"/>
                <asp:HiddenField ID="hidLinkUrl" runat="server"/>
                <asp:HiddenField ID="hidUserID" runat="server"/>
                <asp:HiddenField ID="hidAssginTo" runat="server"/>
                <asp:HiddenField ID="hidPageCode" runat="server"/>
                <asp:HiddenField ID="hidChkAssginTo" runat="server"/>
                <asp:HiddenField ID="hidChkMsgUser" runat="server"/>
                <asp:HiddenField ID="hidCloseFlag" runat="server"/>
             </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li>
             <table>
                <tr>
                    <td colspan="2">
                        <span style="color:#AAAAAA">
                        Title：填写Issue描述信息。<br />
                        优先级：0：1小时内处理完成&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1：1个工作日内（当天）处理完成<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2：2个工作日内处理完成&nbsp;&nbsp;3：3个工作日内处理完成<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4：4个工作日内处理完成<br />
                        分类：根据产生Issue单的原因进行选择,可以多选。<br />
                        Issue指派：为指派下一个处理人员。<br />
                        状态：表示目前Issue单的处理状态,可正向和逆向修改。<br />
                        是否赔偿与赔偿金额：当选择是否赔偿-是,则需填写赔偿金额。否则不需。<br />
                        关联类型与关联ID：通过选择关联类型来判断与Issue相关信息。并可进行跳转查看。<br />
                        类型:订单 -> 关联ID:订单ID&nbsp;&nbsp;类型:酒店 -> 关联ID:酒店ID&nbsp;&nbsp;类型:发票 -> 关联ID:订单ID<br />
                        类型:用户 -> 关联ID:登录手机号&nbsp;&nbsp;类型:提现 -> 关联ID:提现ID<br />
                        增加批注：每个处理人员可再增加批注栏内追加批注信息,以便后续跟踪Issue单流程。<br /><br />
                        </span>
                    </td>
                </tr>
             </table>
        </li>
        <li></li>
      </ul>
    </div>
  </div>
</asp:Content>