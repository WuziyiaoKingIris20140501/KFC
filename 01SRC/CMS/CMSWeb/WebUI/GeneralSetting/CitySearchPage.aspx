<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="CitySearchPage.aspx.cs"  Title="城市管理" Inherits="CitySearchPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
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
</style>
<script language="javascript" type="text/javascript">
    function PopupArea(arg, type) {
        var obj = new Object();
        obj.id = arg;
        var time = new Date();
        var retunValue = window.showModalDialog("CityDetail.aspx?ID=" + arg + "&time=" + time, obj, "dialogWidth=800px;dialogHeight=500px");
        if (retunValue) {
            document.getElementById("<%=refushFlag.ClientID%>").value = retunValue;
            document.getElementById("<%=btnSearch.ClientID%>").click();
        } 
    }

    function SetchkRegistUnTime() {
        if (document.getElementById("<%=chkUnTime.ClientID%>").checked == true) {
            document.getElementById("<%=dpStart.ClientID%>").value = "";
            document.getElementById("<%=dpEnd.ClientID%>").value = "";

            document.getElementById("<%=dpStart.ClientID%>").disabled = true;
            document.getElementById("<%=dpEnd.ClientID%>").disabled = true;
        }
        else {

            document.getElementById("<%=dpStart.ClientID%>").disabled = false;
            document.getElementById("<%=dpEnd.ClientID%>").disabled = false;
        }
    }

    function CheckFogCityName() {
//        var FogCityName = document.getElementById("<%=txtCityName.ClientID %>").value;
//        if (FogCityName == "") {
//            alert("搜索Fog城市名不能为空");
//            return;
//        }
        BtnLoadFogStyle();
    }

    function BtnLoadFogStyle() {
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
        ajaxbg.show();
    }

    function BtnLoadCityStyle() {
        var ajaxbg = $("#backgroundCity,#progressBarCity");
        ajaxbg.hide();
        ajaxbg.show();
    }

    function BtnSearchLoadCityStyle() {
        var ajaxbg = $("#loadBackgroundCity,#loadProgressBarCity");
        ajaxbg.hide();
        ajaxbg.show();
    }
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div id="right">
   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
      <ContentTemplate>
        <div class="frame01">
          <ul>
            <li class="title">添加LM城市</li>
            <li>FOG城市名称：
                <asp:DropDownList ID="ddpCityList" CssClass="noborder_inactive" runat="server" Visible="false">
                    </asp:DropDownList> 
                <asp:TextBox ID="txtCityName" runat="server" Width="20%" AutoCompleteType="Disabled" ></asp:TextBox>
             <%-- <input name="textfield" type="text" id="txtCityName" value="" runat="server"/>
                城市代码：
              <input name="textfield" type="text" id="txtCityID" value="" runat="server" />--%>
              <asp:Button ID="Button1" runat="server" CssClass="btn primary" Text="查询" OnClientClick="CheckFogCityName();" onclick="Button1_Click" />
              <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加到LM" onclick="btnAdd_Click"/>
            </li>
            <li class="button">
            
                <%--<input type="button" id="btnClear" style="width:80px;height:20px;" value="重置"  onclick="ClearClickEvent()" />--%>
                <%--<img src="../../images/button.gif" runat="server" width="92" height="21" align="absmiddle" onclick="SaveClickEvent()" style="cursor:pointer;"/>--%>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;新建项目默认为下线状态</li>
            <li><div id="messageContent" runat="server" style="color:red"></div></li>
          </ul>
        </div>

        <div  class="frame02">
         <asp:GridView ID="gridView1" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" 
                AllowPaging="true" PageSize="15"
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                CssClass="GView_BodyCSS" Height="86px" 
                onrowdatabound="gridView1_RowDataBound" 
                onpageindexchanging="gridView1_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                       <%-- <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="cityid" HeaderText="ID"   />
                    <asp:BoundField DataField="areaid" HeaderText="AREAID" Visible="False"  />
                    <asp:BoundField DataField="name_zh" HeaderText="城市中午名称" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="name_en" HeaderText="城市英文名称" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="pinyin_short" HeaderText="简写" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="cityNM" HeaderText="FOG城市名" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField> 
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
        </div>
                <div id="background" class="pcbackground" style="display: none; "></div> 
            <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
     </ContentTemplate>
   </asp:UpdatePanel>
     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
      <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">搜索现有城市</li>
        <li>城市名称：
          <label for="textfield"></label>
          <input type="text" name="textfield" id="txtSelChannelName" runat="server" />
        </li>
        <li>创建日期：
          <%--<select name="" size="1" >
            <option>2011－11－07 至 2011－11－14</option>
          </select>--%>
         <%-- <input id="dpStart" class="Wdate" type="text" onfocus ="WdatePicker({maxDate:'#F{$dp.$D(\'dpEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpEnd" class="Wdate" type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'dpStart\')}',maxDate:'2020-10-01'})" runat="server"/>--%>
          <input id="dpStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpStart\')}',maxDate:'2020-10-01'})" runat="server"/>

          <input type="checkbox" name="checkbox" id="chkUnTime" runat="server" onclick="SetchkRegistUnTime()"/>
          不限制
          <label for="checkbox"></label>
        </li>
        <li>在线状态：
           <asp:RadioButton ID="rdbAll" GroupName="rdbOnline" runat="server" Text="不限制" Checked="true" />
            <asp:RadioButton ID="rdbOnL" GroupName="rdbOnline" runat="server" Text="上线"/>
            <asp:RadioButton ID="rdbOff" GroupName="rdbOnline" runat="server" Text="下线"/>
        </li>
        <li class="button">
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" OnClientClick="BtnSearchLoadCityStyle()" onclick="btnSearch_Click" />
        </li>
        <%--<li class="button"><img src="../../images/button01.gif" runat="server" width="92" height="21" align="absmiddle" style="cursor:pointer;"/></li>--%>
      </ul>
    </div>
      </ContentTemplate>
   </asp:UpdatePanel>
   <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
      <ContentTemplate>
    <div class="frame02">
        

        <div runat="server" id="divAdjust" style="display:block; margin-right:15px;">
        <asp:Button ID="Button2" runat="server" Text="调整顺序" onclick="Button2_Click" Visible="false" /></div>
        <div runat="server" id="divSaveAdjust" style="display:none;margin-right:15px;">
            <asp:Button ID="btnSaveAdjust" runat="server" Text="保存" CssClass="btn primary" OnClientClick="BtnLoadCityStyle();" onclick="btnSaveAdjust_Click"  />
            <asp:Button ID="btnCancelAdjust" runat="server" Text="取消修改" CssClass="btn" onclick="btnCancelAdjust_Click" />
        </div>

        <asp:GridView ID="gridViewCSCityList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" AllowPaging="false" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSCityList_RowDataBound" 
                            onpageindexchanging="gridViewCSCityList_PageIndexChanging"  CssClass="GView_BodyCSS">
                <Columns>
                    <%--<asp:BoundField DataField="ID" HeaderText="ID" Visible="False"  />--%>
                    <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                   <asp:Label ID="lblID" runat="server"  Text='<%# Eval("ID")%>'></asp:Label>                                    
                               </ItemTemplate>
                               <ItemStyle  HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CITYID" HeaderText="CHANELID" Visible="False"  />
                    <asp:BoundField DataField="CITYNM" HeaderText="城市名称" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="PINYIN" HeaderText="城市拼音" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <%--<asp:BoundField DataField="SEQ" HeaderText="优先级" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>
                    <asp:TemplateField HeaderText="优先级">
                        <EditItemTemplate>
                            <asp:Label ID="lblSeq" runat="server" Text='<%# Eval("SEQ").ToString() %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtSeqRead" runat="server" Enabled="False" 
                                Text='<%# Eval("SEQ").ToString() %>' Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="ONLINEDIS" HeaderText="在线状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>
                    <asp:BoundField DataField="ONLINESTATUS" HeaderText="在线CODE" Visible="False" />
                    <asp:TemplateField HeaderText="销售开始时间">
                                <ItemTemplate>
                                   <asp:Label ID="lblMackData" runat="server"  Text='<%# Eval("SALE_HOUR")%>'></asp:Label>                                    
                               </ItemTemplate>
                               <ItemStyle  HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ONLINEDIS" HeaderText="在线状态" ReadOnly="True"><ItemStyle HorizontalAlign="Center" Width="7%"/></asp:BoundField>
                    <asp:TemplateField HeaderText="城市类型">
                                <ItemTemplate>
                                   <asp:Label ID="lblCityTypes" runat="server"  Text='<%# Eval("CITYTYPES")%>'></asp:Label>                                    
                               </ItemTemplate>
                               <ItemStyle  HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CDTIME" HeaderText="创建时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="UDTIME" HeaderText="编辑时间" ReadOnly="True"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                      <ItemTemplate>
                      <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "CITYID") %>')">编辑</a>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                  </asp:TemplateField>
                    <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="在线状态">
                        <ItemTemplate>
                           <asp:DropDownList ID="ddlOnline" runat="server" DataSource='<%# ddlOnlinebind()%>' DataValueField="ONLINESTATUS" DataTextField="ONLINEDIS" Enabled="false">
                                    </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:CommandField HeaderText="选择" ShowSelectButton="True" />--%>
                    <%--<asp:CommandField HeaderText="编辑" ShowEditButton="True"><ItemStyle HorizontalAlign="Center" /></asp:CommandField>--%>
                    <%--<asp:CommandField HeaderText="删除" ShowDeleteButton="True" />--%>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <RowStyle CssClass="GView_ItemCSS" />                        
                <HeaderStyle CssClass="GView_HeaderCSS" />                                                    
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
    <div id="backgroundCity" class="pcbackground" style="display: none; "></div> 
    <div id="progressBarCity" class="pcprogressBar" style="display: none; ">数据保存中，请稍等...</div> 
    <div id="loadBackgroundCity" class="pcbackground" style="display: none; "></div> 
    <div id="loadProgressBarCity" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div> 
    </ContentTemplate>
   </asp:UpdatePanel>
</div>
<asp:HiddenField ID="refushFlag" runat="server"/>
</asp:Content>