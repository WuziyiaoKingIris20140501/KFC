<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="PartnerMangerPage.aspx.cs"  Title="合作渠道链接管理" Inherits="PartnerMangerPage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
<style type="text/css" >
.bgDivList
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

.popupDivList
{
    width: 1000px;
    height:800px;
    margin-left:50px;
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
        document.getElementById("<%=txtPartnerID.ClientID%>").value = "";
        document.getElementById("<%=txtPartnerLink.ClientID%>").value = "";
        document.getElementById("<%=txtPartnerTitle.ClientID%>").value = "";
        document.getElementById("<%=txtCost.ClientID%>").value = "";
        document.getElementById("<%=txtRemark.ClientID%>").value = "";
    }

    //显示弹出的层
    function invokeOpenlist() {
        document.getElementById("popupDiv").style.display = "block";
        //背景
        var bgObj = document.getElementById("bgDiv");
        bgObj.style.display = "block";
        bgObj.style.width = document.body.offsetWidth + "px";
        bgObj.style.height = document.body.offsetHeight + "px";

        //定义窗口
        //var msgObj=document.getElementById("popupDiv");
        //msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";        
    }

    //隐藏弹出的层
    function invokeCloselist() {
        document.getElementById("popupDiv").style.display = "none";
        document.getElementById("bgDiv").style.display = "none";
        document.getElementById("<%=hidRbtType.ClientID%>").value = document.getElementById("rbt1hh").value;
    }

    function AddNewlist(val, pid, plk, rk, tl, ct, pt, at, wl) {
        invokeOpenlist();
        document.getElementById("<%=hidSysID.ClientID%>").value = val;
        document.getElementById("<%=txtDelPartnerID.ClientID%>").value = pid;
        document.getElementById("<%=txtDelPartnerLink.ClientID%>").value = plk;
        document.getElementById("<%=txtDelRemark.ClientID%>").value = rk;
        document.getElementById("<%=txtDelPartnerTitle.ClientID%>").value = tl;
        document.getElementById("<%=txtDelCost.ClientID%>").value = ct;

        document.getElementById("<%=lbPartnerct.ClientID%>").innerText = pt;
        document.getElementById("<%=hidPartnerct.ClientID%>").value = pt;

        document.getElementById("<%=lbAvgpt.ClientID%>").innerText = at;
        document.getElementById("<%=hidAvgpt.ClientID%>").value = at;

        document.getElementById("<%=lbWapLink.ClientID%>").innerText = wl;
        document.getElementById("<%=hidWapLink.ClientID%>").value = wl;

        var arg = document.getElementById("<%=hidRbtType.ClientID%>").value;
        if (document.getElementById("rbt15mi").value == arg) {
            document.getElementById("rbt15mi").checked = true;
        }
        else if (document.getElementById("rbt30mi").value == arg) {
            document.getElementById("rbt30mi").checked = true;
        }
        else if (document.getElementById("rbt1hh").value == arg) {
            document.getElementById("rbt1hh").checked = true;
        }
        else if (document.getElementById("rbt1day").value == arg) {
            document.getElementById("rbt1day").checked = true;
        }
        else {
            document.getElementById("rbt1hh").checked = true;
        }
    }

    function SerRbtNameValue(arg) {
        document.getElementById("<%=hidRbtType.ClientID%>").value = arg;
    }

    function SerRbtValue() {
        var arg = "";
        if (document.getElementById("rbt15mi").checked == true)
        {
            arg = "15";
        }
        else if (document.getElementById("rbt30mi").checked == true)
        {
            arg = "30";
        }
        else if (document.getElementById("rbt1hh").checked == true)
        {
            arg = "60";
        }
        else if (document.getElementById("rbt1day").checked == true)
        {
            arg = "1440";
        }
        else
        {
            arg = "60";
        }
        document.getElementById("<%=hidRbtType.ClientID%>").value = arg;
    }
</script>
    <div id="bgDiv" class="bgDivList"></div>
    <div id="popupDiv" class="popupDivList">
          <br />
          <div class="frame01">
            <ul>
                <li class="title" style="text-align:left">合作渠道链接编辑</li>
                <li>
                    <table width="100%" align="center" >
                        <tr>
                            <td align="right" >合作渠道Title：</td>
                            <td align="left" >
                                <input name="textfield" type="text" id="txtDelPartnerTitle" value="" runat="server" maxlength="200"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" >合作渠道ID：</td>
                            <td align="left" >
                                <input name="textfield" type="text" id="txtDelPartnerID" value="" runat="server" maxlength="10"/><font color="red">*</font>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">合作渠道链接：</td>
                            <td align="left" >
                                <asp:Label ID="lbWapLink" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">目标地址：</td>
                            <td align="left" >
                                <input name="textfield" type="text" id="txtDelPartnerLink" value="" runat="server" style="width:600px" maxlength="2000"/><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" >
                                总点击量：
                            </td>
                            <td align="left" >
                                <asp:Label ID="lbPartnerct" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">总成本（单位:元）：</td>
                            <td align="left" >
                                <input name="textfield" type="text" id="txtDelCost" value="" runat="server" maxlength="15"/>
                                 &nbsp;&nbsp;&nbsp;
                                平均成本（单位:元）：
                                <asp:Label ID="lbAvgpt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" >备注：</td>
                            <td align="left" >
                                <%--<input name="textfield" type="text" id="txtDelRemark" value="" style="width:400px" runat="server" maxlength="200"/>--%>
                                <asp:TextBox ID="txtDelRemark" Width="600px" runat="server" TextMode="MultiLine" Height="100px" />
                            </td>
                        </tr>
                        <tr><td><br /></td></tr>
                        <tr>
                            <td align="right"></td>
                            <td align="left">
                                <asp:Button ID="btnSave" CssClass="btn primary" runat="server" Text="保存" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnCLose" runat="server" CssClass="btn" Text="关闭" OnClientClick="invokeCloselist()" onclick="btnCLose_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td  align="left" colspan="2" >
                                <div id="detailMessageContent" runat="server" style="color:red"></div>
                            </td>
                        </tr>
                        </table>
                    <asp:HiddenField ID="hidSysID" runat="server"/>
                    <asp:HiddenField ID="hidPartnerct" runat="server"/>
                    <asp:HiddenField ID="hidAvgpt" runat="server"/>
                    <asp:HiddenField ID="hidWapLink" runat="server"/>
                </li>
            </ul>
          </div>
          <div class="frame01">
            <ul>
                <li class="title" style="text-align:left">合作渠道趋势图</li>
                <li>
                    <table width="100%" align="center" >
                         <tr>
                            <td align="right" >统计基数：</td>
                            <td align="left" >
                                <input type="radio" name="RbtType" id="rbt15mi" value="15" onclick="SerRbtNameValue('15')"/>15分钟
                                <input type="radio" name="RbtType" id="rbt30mi" value="30" onclick="SerRbtNameValue('30')"/>30分钟
                                <input type="radio" name="RbtType" id="rbt1hh" value="60" onclick="SerRbtNameValue('60')"/>1小时
                                <input type="radio" name="RbtType" id="rbt1day" value="1440" checked="checked" onclick="SerRbtNameValue('1440')"/>1天
                                <asp:HiddenField ID="hidRbtType" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" >时间区间：</td>
                            <td align="left" >
                                <input id="dpChartStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',maxDate:'#F{$dp.$D(\'MainContent_dpChartEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                                <input id="dpChartEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'MainContent_dpChartStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" ></td>
                            <td align="left" >
                                <asp:Button ID="btnChart" CssClass="btn primary" runat="server" Text="确认" OnClientClick="SerRbtValue()" OnClick="btnChart_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="width:100%">
                                <asp:Chart ID="Chart1" runat="server" Width="850px">
                                    <series>
                                        <asp:Series Name="Series1">
                                        </asp:Series>
                                    </series>
                                    <chartareas>
                                        <asp:ChartArea Name="ChartArea1">
                                        </asp:ChartArea>
                                    </chartareas>
                                </asp:Chart>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
          </div>
          <br />
    </div>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">添加合作渠道链接</li>
        <li>
            <table>
                <tr>
                    <td align="right">
                        合作渠道Title：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtPartnerTitle" value="" runat="server" maxlength="200"/>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        合作渠道ID：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtPartnerID" value="" runat="server" maxlength="10"/><font color="red">*</font>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        目标地址：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtPartnerLink" value="" runat="server" style="width:600px" maxlength="2000"/><font color="red">*</font>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        总成本(单位:元)：
                    </td>
                    <td>
                        <input name="textfield" type="text" id="txtCost" value="" runat="server" maxlength="15"/>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        备注：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRemark" Width="600px" runat="server" TextMode="MultiLine" Height="100px" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加" onclick="btnAdd_Click" />
                        <input type="button" id="btnClear" class="btn" value="重置"  onclick="ClearClickEvent()" />
                    </td>
                </tr>
            </table>
        </li>
        <li><div id="messageContent" runat="server" style="color:red"></div></li>
      </ul>
    </div>
    <div class="frame01">
      <ul>
        <li class="title">搜索现有合作渠道</li>
        <li>合作渠道ID：
          <label for="textfield"></label>
          <input type="text" name="textfield" id="txtSelPartnerID" runat="server" maxlength="10" />
        </li>
        <li>创建日期：
          &nbsp;&nbsp;
          <input id="dpStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd 00:00:00',maxDate:'#F{$dp.$D(\'MainContent_dpEnd\')||\'2020-10-01\'}'})" runat="server"/> 
          <input id="dpEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd 23:59:59',minDate:'#F{$dp.$D(\'MainContent_dpStart\')}',maxDate:'2020-10-01'})" runat="server"/>
          <input type="checkbox" name="checkbox" id="chkUnTime" runat="server"/>
          不限制
          <label for="checkbox"></label>
        </li>
        <li class="button">&nbsp;&nbsp;
            <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" Text="搜索" onclick="btnSearch_Click" />
        </li>
      </ul>
    </div>
    
    <div class="frame02">
        <asp:GridView ID="gridViewCSPartnerList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" AllowPaging="true" PageSize="15" 
                            Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                            onrowdatabound="gridViewCSPartnerList_RowDataBound" onpageindexchanging="gridViewCSPartnerList_PageIndexChanging" CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False"/>
                    <asp:BoundField DataField="PARTNERTITLE" HeaderText="合作渠道Title" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="PARTNERID" HeaderText="合作渠道ID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>

                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="合作渠道链接">
                      <ItemTemplate>
                      <a href="#" id="afPopupArea" onclick="AddNewlist('<%# DataBinder.Eval(Container.DataItem, "ID") %>','<%# DataBinder.Eval(Container.DataItem, "PARTNERID") %>','<%# DataBinder.Eval(Container.DataItem, "PARTNERLINK") %>','<%# DataBinder.Eval(Container.DataItem, "REMARK") %>','<%# DataBinder.Eval(Container.DataItem, "PARTNERTITLE") %>','<%# DataBinder.Eval(Container.DataItem, "PCOST") %>','<%# DataBinder.Eval(Container.DataItem, "PARTNERCT") %>','<%# DataBinder.Eval(Container.DataItem, "AVGPT") %>', '<%# DataBinder.Eval(Container.DataItem, "WAPLINK") %>')"><%# DataBinder.Eval(Container.DataItem, "WAPLINK")%></a>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%"></ItemStyle>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="PARTNERLINK" HeaderText="合作渠道链接"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>--%>
                    <asp:BoundField DataField="PCOST" HeaderText="总成本(单位:元)"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="PARTNERCT" HeaderText="总点击量"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="AVGPT" HeaderText="平均成本(单位:元)"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="REMARK" HeaderText="备注"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="CDTIME" HeaderText="创建时间"><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
    </div>
</div>
</asp:Content>