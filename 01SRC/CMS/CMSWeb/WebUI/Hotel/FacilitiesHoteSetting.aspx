<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="FacilitiesHoteSetting.aspx.cs"  Title="酒店服务设施管理" Inherits="FacilitiesHoteSetting" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        var checkBoxList = document.getElementsByTagName("input");
        var ckValue = "";
        for (var i = 0; i < checkBoxList.length; i++) {
            if (checkBoxList[i].type == "checkbox" && checkBoxList[i].checked) {
                checkBoxList[i].checked = false;
            }
        }
        document.getElementById("<%=MessageContent.ClientID%>").innerHTML = "";
        document.getElementById("<%=lbHotelNM.ClientID%>").innerHTML = "";
        document.getElementById("<%=hidHotelID.ClientID%>").value = "";
    }

    function CheckBoxs() {
        var checkBoxList = document.getElementsByTagName("input");
        var ckValue = "";
        for (var i = 0; i < checkBoxList.length; i++) {
            if (checkBoxList[i].type == "checkbox" && checkBoxList[i].checked) {
                ckValue += checkBoxList[i].value + ",";
            }
        }
        document.getElementById("<%=hidFTList.ClientID%>").value = ckValue;
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">

    <div class="frame01">
      <ul>
        <li class="title">酒店服务设施管理</li>
        <li>
            <table>
              <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                 <ContentTemplate>
                <tr>
                    <td>
                        选择酒店：
                    </td>
                    <td>
                         <table>
                            <tr>
                                <td>
                                    <uc1:WebAutoComplete ID="WebAutoComplete" runat="server" CTLID="WebAutoComplete" AutoType="hotel" AutoParent="FacilitiesHoteSetting.aspx" />
                                </td>
                                <td>
                                    <asp:Button ID="btnService" runat="server" CssClass="btn primary" Text="选择" onclick="btnSelect_Click" />
                                    <input type="button" id="btnClear" class="btn" value="重置" onclick="ClearClickEvent();" />
                                </td>
                            </tr>
                         </table>
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr><td colspan="3"><br /></td></tr>
                <tr>
                    <td>
                        酒店名称：
                    </td>
                    <td>
                       <asp:Label ID="lbHotelNM" runat="server" Text=""/>
                    </td>
                    <td></td>
                </tr>
                </ContentTemplate>
                </asp:UpdatePanel>
            </table>
            
        </li>
        <li>
        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="MessageContent" runat="server" style="color:red;width:800px;"></div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </li>
      </ul>
    </div>
  
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="dvChkList" style="margin-left:10px;width:98%" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" >
        <ContentTemplate>
            <div id="save" style="text-align:center">
                <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" OnClientClick="CheckBoxs()" onclick="btnSave_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" CssClass="btn" Text="还原" onclick="btnReset_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
 </div>
<asp:HiddenField ID="hidHotelID" runat="server"/>
<input type="hidden" id="hidFTList" runat="server" />
</asp:Content>
