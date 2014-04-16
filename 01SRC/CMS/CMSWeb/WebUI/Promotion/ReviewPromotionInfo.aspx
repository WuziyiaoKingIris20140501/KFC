<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ReviewPromotionInfo.aspx.cs"  Title="促销信息管理" Inherits="ReviewPromotionInfo" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("<%=txtProTitle.ClientID%>").value = "";

        document.getElementById("<%=dpStartBeginDate.ClientID%>").value = "";
        document.getElementById("<%=dpStartEndDate.ClientID%>").value = "";
        document.getElementById("<%=dpEndBeginDate.ClientID%>").value = "";
        document.getElementById("<%=dpEndEndDate.ClientID%>").value = "";

        document.getElementById("<%=chkStartUnTime.ClientID%>").checked = true;
        document.getElementById("<%=chkEndUnTime.ClientID%>").checked = true;

        document.getElementById("<%=dpStartBeginDate.ClientID%>").disabled = true;
        document.getElementById("<%=dpStartEndDate.ClientID%>").disabled = true;
        document.getElementById("<%=dpEndBeginDate.ClientID%>").disabled = true;
        document.getElementById("<%=dpEndEndDate.ClientID%>").disabled = true;
    }

    function SetchkStartUnTime() {
        if (document.getElementById("<%=chkStartUnTime.ClientID%>").checked == true) {
            document.getElementById("<%=dpStartBeginDate.ClientID%>").value = "";
            document.getElementById("<%=dpStartEndDate.ClientID%>").value = "";

            document.getElementById("<%=dpStartBeginDate.ClientID%>").disabled = true;
            document.getElementById("<%=dpStartEndDate.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=dpStartBeginDate.ClientID%>").disabled = false;
            document.getElementById("<%=dpStartEndDate.ClientID%>").disabled = false;
        }
    }

    function SetchkEndUnTime() {
        if (document.getElementById("<%=chkEndUnTime.ClientID%>").checked == true) {
            document.getElementById("<%=dpEndBeginDate.ClientID%>").value = "";
            document.getElementById("<%=dpEndEndDate.ClientID%>").value = "";

            document.getElementById("<%=dpEndBeginDate.ClientID%>").disabled = true;
            document.getElementById("<%=dpEndEndDate.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=dpEndBeginDate.ClientID%>").disabled = false;
            document.getElementById("<%=dpEndEndDate.ClientID%>").disabled = false;
        }
    }

    function PopupArea(arg, type) {
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
        var retunValue = window.open("PromotionInfoDetail.aspx?ID=" + arg + "&time=" + time, null, fulls);
        //window.location.href = "PromotionInfoDetail.aspx?ID=" + arg + "&TYPE=" + type + "&time=" + time;
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
    <ContentTemplate>
     <div class="frame01">
        <ul>
            <li class="title">进行中的促销</li>
       </ul>
    </div>
     <div class="frame02">
        <asp:GridView ID="gridViewCSPromotioningList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSPromotioningList_RowDataBound" onpageindexchanging="gridViewCSPromotioningList_PageIndexChanging" 
                            PageSize="5"  CssClass="GView_BodyCSS">
                <Columns>
                     <asp:BoundField DataField="ID" HeaderText="ID" ><ItemStyle HorizontalAlign="Center"  Width="10%" /></asp:BoundField>

                     <asp:HyperLinkField HeaderText="促销标题" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="PromotionInfoDetail.aspx?ID={0}&TYPE=0" 
                        Target="_blank" DataTextField="PROTITLE"><ItemStyle HorizontalAlign="Center" Width="35%" /></asp:HyperLinkField>

<%--                      <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="促销标题">
                          <ItemTemplate>
                          <a href="#" id="afPopupArea" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>', 0)"><%# DataBinder.Eval(Container.DataItem, "PROTITLE")%></a>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" Width="35%"></ItemStyle>
                      </asp:TemplateField>--%>

                     <asp:BoundField DataField="PROTYPENM" HeaderText="促销类型" ><ItemStyle HorizontalAlign="Center"  Width="10%"/></asp:BoundField>
                     <asp:BoundField DataField="STARTDATE" HeaderText="开始时间" ><ItemStyle HorizontalAlign="Center"  Width="15%" /></asp:BoundField>
                     <asp:BoundField DataField="ENDDATE" HeaderText="结束时间" ><ItemStyle HorizontalAlign="Center" Width="15%" /></asp:BoundField>
                     <asp:BoundField DataField="ONLINEDIS" HeaderText="上线状态"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>

                     <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                          <ItemTemplate>
                          <a href="#" id="afPopupArea" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>', 1)"><%# DataBinder.Eval(Container.DataItem, "MODIFYFIELD")%></a>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                      </asp:TemplateField>--%>

                     <asp:HyperLinkField HeaderText="编辑" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="PromotionInfoDetail.aspx?ID={0}&TYPE=1" 
                     Target="_blank" DataTextField="MODIFYFIELD"><ItemStyle HorizontalAlign="Center"  Width="5%" /></asp:HyperLinkField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
    <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">促销管理</li>
          <li>
            <table>
                <tr>
                    <td>
                        促销信息标题：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtProTitle" runat="server" style="width:450px;" maxlength="192" value=""/>
                    </td>
                </tr>
                <tr>
                    <td>
                        促销开始日期：
                    </td>
                    <td>
                         <input id="dpStartBeginDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpStartEndDate\')||\'2020-10-01\'}'})" runat="server"/> 
                         <input id="dpStartEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpStartBeginDate\')}',maxDate:'2020-10-01'})" runat="server"/>
                         <input type="checkbox" name="checkbox" id="chkStartUnTime" runat="server" onclick="SetchkStartUnTime()"/>
                          不限制
                    </td>
                </tr>
                <tr>
                    <td>
                        促销结束日期：
                    </td>
                    <td>
                        <input id="dpEndBeginDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpEndEndDate\')||\'2020-10-01\'}'})" runat="server"/> 
                        <input id="dpEndEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpEndBeginDate\')}',maxDate:'2020-10-01'})" runat="server"/>
                        <input type="checkbox" name="checkbox" id="chkEndUnTime" runat="server" onclick="SetchkEndUnTime()"/>
                        不限制
                    </td>
                </tr>
                <tr><td></td><td></td></tr>
                <tr>
                    <td></td>
                    <td>
                         <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click" />
                         <input type="button" id="btnClear" class="btn" value="重置" onclick="ClearClickEvent();" />
                    </td>
                </tr>
                 <tr><td></td><td></td></tr>
            </table>
          </li>
      </ul>
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
    <ContentTemplate>
    <%--<div class="frame01"><ul><li><div id="messageContent" runat="server" style="color:red;width:800px;"></div></li></ul></div>--%>
    <div class="frame02">
        <asp:GridView ID="gridViewCSPromotionMsgList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                            CellPadding="4" CellSpacing="1" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSPromotionMsgList_RowDataBound" 
                            onpageindexchanging="gridViewCSPromotionMsgList_PageIndexChanging" PageSize="15"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ><ItemStyle HorizontalAlign="Center"  Width="10%" /></asp:BoundField>

                    <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="促销标题">
                          <ItemTemplate>
                          <a href="#" id="afPopupArea" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>', 0)"><%# DataBinder.Eval(Container.DataItem, "PROTITLE")%></a>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" Width="35%"></ItemStyle>
                      </asp:TemplateField>--%>

                     <asp:HyperLinkField HeaderText="促销标题" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="PromotionInfoDetail.aspx?ID={0}&TYPE=0" 
                        Target="_blank" DataTextField="PROTITLE"><ItemStyle HorizontalAlign="Center" Width="35%" /></asp:HyperLinkField>
                     <asp:BoundField DataField="PROTYPENM" HeaderText="促销类型" ><ItemStyle HorizontalAlign="Center"  Width="10%"/></asp:BoundField>
                     <asp:BoundField DataField="STARTDATE" HeaderText="开始时间" ><ItemStyle HorizontalAlign="Center"  Width="15%" /></asp:BoundField>
                     <asp:BoundField DataField="ENDDATE" HeaderText="结束时间" ><ItemStyle HorizontalAlign="Center" Width="15%" /></asp:BoundField>
                     <asp:BoundField DataField="ONLINEDIS" HeaderText="上线状态"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>

                      <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                          <ItemTemplate>
                          <a href="#" id="afPopupArea" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ID") %>', 1)"><%# DataBinder.Eval(Container.DataItem, "MODIFYFIELD")%></a>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                      </asp:TemplateField>--%>

                     <asp:HyperLinkField HeaderText="编辑" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="PromotionInfoDetail.aspx?ID={0}&TYPE=1" 
                     Target="_blank" DataTextField="MODIFYFIELD"><ItemStyle HorizontalAlign="Center"  Width="5%" /></asp:HyperLinkField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
          </asp:GridView>
    </div>
    </ContentTemplate>
   </asp:UpdatePanel>
</div>
</asp:Content>