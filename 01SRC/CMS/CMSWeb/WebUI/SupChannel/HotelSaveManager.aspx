<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" MasterPageFile="~/Site.master"  CodeFile="HotelSaveManager.aspx.cs"  Title="酒店基础信息保存管理" Inherits="HotelSaveManager" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />

<script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>

<script language="javascript" type="text/javascript">
    function OpenIssuePage() {
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
        window.open('<%=ResolveClientUrl("~/WebUI/Issue/SaveIssueManager.aspx")%>?RType=1&RID=' + document.getElementById("<%=hidHotelID.ClientID%>").value + "&time=" + time, null, fulls);
    }
</script>

    <div class="frame01" style="margin:8px 14px 5px 14px;">
      <ul>
        <li class="title">酒店基础信息管理</li>
        <li>
            <%--<table>
                <tr>
                    <td>
                        选择酒店：
                    </td>
                    <td>
                        <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel" AutoParent="HotelInfoManager.aspx?Type=hotel" />
                    </td>
                    <td>
                        <asp:Button ID="btnSelect" runat="server" Width="80px" Height="20px" Text="选择" onclick="btnSelect_Click" />
                        <asp:Button ID="btnClear" runat="server" Width="80px" Height="20px" Text="重置" onclick="btnClear_Click" />
                    </td>
                </tr>
                <tr><td colspan="3"><br /></td></tr>
                <tr>
                    <td>
                        酒店名称：
                    </td>
                    <td>
                       <asp:Label ID="lbHotelNM" runat="server" Text="" />
                    </td>
                    <td></td>
                </tr>
            </table>--%>

            <div style="margin:5px 14px 5px 14px;">
                <div id="tabs" style="background:#FFFFFF;border: 0px solid #FFFFFF;">
	                <ul style="background:#FFFFFF;border: 0px solid #FFFFFF;">
		                <li><a href="#tabs-1">  酒店基础信息  </a></li>
		                <li style="display:none"><a href="#tabs-2">  酒店签约信息  </a></li>
		                <li style="display:none"><a href="#tabs-3">  酒店结算信息  </a></li>
	                </ul>
	                <div id="tabs-1" style="border: 1px solid #D5D5D5;">
                        <div>
                             <table>
                             <tr>
                                <td align="left">酒 店 ID ：</td>
                                <td><asp:Label ID="lbHotelID" runat="server" Text="" /></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="left">酒店名称：</td>
                                <td><asp:TextBox ID="txtHotelNM" runat="server" Width="385px" MaxLength="200"/><font color="red">*</font></td>
                                <td align="right">酒店英文名称：</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtHotelNMEN" runat="server" Width="385px" MaxLength="200"/></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">酒店星级：</td>
                                <td><asp:DropDownList ID="ddpStarRating" CssClass="noborder_inactive" runat="server" Width="200px"/></td>
                                 <td align="right">
                                    所在城市：
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddpCity" CssClass="noborder_inactive" runat="server" Width="153px"/>
                                            </td>
                                            <td align="right" style="width:80px">
                                                行政区：
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddpAdministration" CssClass="noborder_inactive" runat="server" Width="153px"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                <td align="left">酒店电话：</td>
                                <td><asp:TextBox ID="txtHotelTel" runat="server" Width="193px" MaxLength="200"/></td>
                                <td align="right">酒店地址：</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="txtAddress" runat="server" Width="300px" MaxLength="200"/></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td align="left">酒店传真：</td>
                                <td><asp:TextBox ID="txtHotelFax" runat="server" Width="193px" MaxLength="200"/></td>
                                <td align="right">
                                    经  纬  度：
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            
                                            <td>
                                                <asp:TextBox ID="txtLongitude" runat="server" Width="145px" MaxLength="200"/>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLatitude" runat="server" Width="145px" MaxLength="200"/>&nbsp;
                                                <input type="button" id="btnChkMap" class="btn primary" value="Find" onclick="PopupHotelMap()" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">酒店品牌：</td>
                                <td><asp:DropDownList ID="ddpHotelGroup" CssClass="noborder_inactive" runat="server" Width="200px"/></td>
                                <td align="right">酒店商圈：</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtArea" runat="server" Width="300px" MaxLength="200"/>&nbsp;
                                                <input type="button" id="btnAdd" class="btn primary" value="Add"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">开业日期：</td>
                                <td>
                                    <table>
                                        <tr>
                                            
                                            <td><input id="dpOpenDate" class="Wdate" type="text" style="width:145px" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/></td>
                                            <td align="right" style="width:80px">最后装修：</td>
                                            <td><input id="dpRepairDate" class="Wdate" type="text" style="width:145px" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:400px;" colspan="2">
                                    <div id="dvAreaList" runat="server" style="margin-left:60px"></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">酒店服务：</td>
                                <td align="right" style="width:120px">酒店设施：</td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:TextBox ID="txtHServe" runat="server" TextMode="MultiLine" Width="460px" Height="100px"/></td>
                                <td colspan="2"><div style="margin-left:60px"><asp:TextBox ID="txtHFacility" runat="server" TextMode="MultiLine" Width="460px" Height="100px"/></div></td>
                            </tr>
                            <tr>
                                <td colspan="2">酒店一句话描述：<span style="color:#AAAAAA">&nbsp;最多1000个字符</span></td>
                                <td align="right" style="width:120px">酒店详情：</td>
                            </tr>
                           <tr>
                                <td colspan="2"><asp:TextBox ID="txtSimpleDescZh" runat="server" TextMode="MultiLine" Width="460px" Height="100px"/></td>
                                <td colspan="2"><div style="margin-left:60px"><asp:TextBox ID="txtDescZh" runat="server" TextMode="MultiLine" Width="460px" Height="100px"/></div></td>
                            </tr>
                    </table>
                        </div>
                        <div>
                            <table width="94%">
                           <tr>
                                    <td align="right">
                                        <asp:Button ID="btnAddRoom" runat="server" CssClass="btn primary" Text="添加房型" onclick="btnAddRoom_Click" />
                                    </td>
                                </tr>
                           <tr>
                                <td align="left" valign="top">
                                    <asp:GridView ID="gridViewEvaluationList" runat="server" 
                                        AutoGenerateColumns="False" BackColor="White" 
                                        CellPadding="4" CellSpacing="1" 
                                        Width="100%" EmptyDataText="" DataKeyNames="Content"
                                        CssClass="GView_BodyCSS" onrowdeleting="gridViewEvaluationList_RowDeleting" >
                                        <Columns>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEvalist" Text ='<%# Bind("Content") %>' runat="server" MaxLength="10" Width="320px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="true" DeleteText="删除" >
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:CommandField>
                                        </Columns>
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                                        <PagerStyle HorizontalAlign="Right" />
                                        <RowStyle CssClass="GView_ItemCSS" />
                                        <HeaderStyle CssClass="GView_HeaderCSS" />
                                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                                    </asp:GridView>
                                </td>
                            </tr>
                     </table>
                        </div>
                        <div id="save" style="text-align:left;margin-left:25%">
                            <div id="divBtnList" runat="server">
                        <%--<asp:Button ID="btnFog" runat="server" Width="120px" Height="20px" Text="读取FOG信息" onclick="btnReadFog_Click" />&nbsp;&nbsp;--%>
                        <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" onclick="btnSave_Click" />&nbsp;&nbsp;
                        <%--<asp:Button ID="btnReset" runat="server" Width="80px" Height="20px" Text="取消编辑" onclick="btnReset_Click" />&nbsp;&nbsp;
                        <input type="button" id="btnOpenIssue" style="width:100px;height:20px;" value="创建Issue单" onclick="OpenIssuePage();" />--%>
                    </div>
                        </div>
	                </div>
	                <div id="tabs-2" style="border: 1px solid #D5D5D5;">
                        <div>
                    <table>
                         <tr>
                            <td align="right" >销售人员：</td>
                            <td align="left" colspan="4">
                                <uc1:WebAutoComplete ID="wctSales" CTLID="wctSales" runat="server"  AutoType="sales" AutoParent="HotelInfoManager.aspx?Type=sales" />
                            </td>
                        </tr>
                         <tr>
                            <td align="right">合同日期：</td>
                            <td align="left" colspan="4">
                                 <input id="dpSalesStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpSalesEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                                <input id="dpSalesEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpSalesStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                            </td>
                         </tr>
                         <tr>
                            <td align="right">LM联系电话：</td>
                            <td><asp:TextBox ID="txtPhone" runat="server" Width="300px" MaxLength="40"/><font color="red">*</font></td>
                            <td style="width:50px"></td>
                            <td align="right">LM订单传真：</td>
                            <td><asp:TextBox ID="txtFax" runat="server" Width="300px" MaxLength="20"/><font color="red">*</font></td>
                        </tr>
                        <tr>
                            <td align="right">LM联系人：</td>
                            <td><asp:TextBox ID="txtContactPer" runat="server" Width="300px" MaxLength="100"/></td>
                            <td style="width:50px"></td>
                            <td align="right">LM联系邮箱：</td>
                            <td><asp:TextBox ID="txtContactEmail" runat="server" Width="300px" MaxLength="100"/></td>
                        </tr>
                        <tr>
                            <td align="right" style="height:20px"></td>
                        </tr>
                    </table>
                </div>
                        <div id="dvSales" style="text-align:left;" runat="server">
                    <asp:Button ID="btnSaveSales" runat="server" CssClass="btn primary" Text="保存" OnClientClick="BtnSelectSales()" onclick="btnSaveSales_Click" />
                </div>
                        <div class="frame01" style="margin-top:15px;margin-left:5px">
                    <ul>
                        <li class="title">合同变更历史</li>
                   </ul>
                </div>
                        <div class="frame02" style="margin-left:5px">
                    <asp:GridView ID="gridViewCSSalesList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                                        CellPadding="4" CellSpacing="1" 
                                        Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" 
                                        onrowdatabound="gridViewCSSalesList_RowDataBound" onpageindexchanging="gridViewCSSalesList_PageIndexChanging" 
                                        PageSize="5"  CssClass="GView_BodyCSS">
                        <Columns>
                                <asp:BoundField DataField="SALESNM" HeaderText="销售人员" ><ItemStyle HorizontalAlign="Center"  Width="10%" /></asp:BoundField>
                                <asp:BoundField DataField="STARTDTIME" HeaderText="合同开始日期" ><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                                <asp:BoundField DataField="ENDDTIME" HeaderText="合同截止日期" ><ItemStyle HorizontalAlign="Center" Width="10%" /></asp:BoundField>
                                <asp:BoundField DataField="FAX" HeaderText="LM订单传真" ><ItemStyle HorizontalAlign="Center" Width="10%" /></asp:BoundField>
                                <asp:BoundField DataField="PER" HeaderText="LM联系人"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                                <asp:BoundField DataField="TEL" HeaderText="LM联系电话"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                                <asp:BoundField DataField="EML" HeaderText="LM联系邮箱"><ItemStyle HorizontalAlign="Center" Width="15%"/></asp:BoundField>
                                <asp:BoundField DataField="CREATEUSER" HeaderText="操作人"><ItemStyle HorizontalAlign="Center" Width="10%"/></asp:BoundField>
                                <asp:BoundField DataField="CREATEDT" HeaderText="操作日期"><ItemStyle HorizontalAlign="Center" Width="15%"/></asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                    </asp:GridView>
                </div>
	                </div>
	                <div id="tabs-3" style="border: 1px solid #D5D5D5;">
                        <div>
                    <table>
                        <tr>
                            <td align="right">
                                价格代码：
                            </td>
                             <td>
                                <asp:DropDownList ID="ddpPriceCode" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                选择房型：
                            </td>
                             <td>
                                <asp:CheckBoxList ID="chkHotelRoomList" runat="server" RepeatDirection="Vertical" RepeatColumns="8" RepeatLayout="Table" CellSpacing="8"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                入住时间：
                            </td>
                             <td>
                                <input id="dpInDTStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpInDTEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                                <input id="dpInDTEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpInDTStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                佣金类型：
                            </td>
                             <td>
                                <asp:DropDownList ID="ddpBalType" runat="server" Width="150px"></asp:DropDownList>&nbsp;&nbsp;值：<asp:TextBox ID="txtBalVal" runat="server" Width="80px" MaxLength="7"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="height:20px"></td>
                        </tr>
                    </table>
                    <div id="dvBalAdd" runat="server">
                        <asp:Button ID="btnBalAdd" runat="server" CssClass="btn primary" Text="保存结算信息" onclick="btnBalAdd_Click" />
                    </div>
                </div>
                        <div class="frame01" style="margin-top:15px;margin-left:5px">
                    <ul>
                        <li class="title">结算信息快速查询</li>
                        <li>
                           <table>
                                 <tr>
                                    <td align="right">查询日期：</td>
                                    <td align="left">
                                        <input id="dpBalStart" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_dpBalEnd\')||\'2020-10-01\'}'})" runat="server"/> 
                                        <input id="dpBalEnd" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_dpBalStart\')}',maxDate:'2020-10-01'})" runat="server"/>
                                    </td>
                                    <td style="width:5%"></td>
                                    <td align="right">选择房型：</td>
                                    <td><asp:DropDownList ID="ddpRoomList" runat="server" Width="150px"></asp:DropDownList></td>
                                    <td></td>
                                    <td>
                                        <div id="dvBalSearch" runat="server">
                                            <asp:Button ID="btnBalSearch" runat="server" CssClass="btn primary" Text="查询" onclick="btnBalSearch_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnExportBal" runat="server" CssClass="btn primary" Text="导出" onclick="btnExportBal_Click" />
                                         </div>
                                    </td>
                                </tr>
                            </table>
                        </li>
                   </ul>
                </div>
                        <div class="frame01" style="margin-top:15px;margin-left:5px">
                    <ul>
                        <li class="title">结算信息列表</li>
                   </ul>
                </div>
                        <div class="frame02" style="margin-left:5px;width:1150px;overflow:auto" id="dvBalGridList" runat="server">
                    <asp:GridView ID="gridViewCSBalList" runat="server" BackColor="White" AllowPaging="True" 
                                        CellPadding="4" CellSpacing="1" 
                                        Width="100%" EmptyDataText="没有数据" 
                                        onrowdatabound="gridViewCSBalList_RowDataBound" onpageindexchanging="gridViewCSBalList_PageIndexChanging" 
                                        OnRowCreated="gridViewCSBalList_RowCreated" 
                                        PageSize="10"  CssClass="GView_BodyCSS">
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <HeaderStyle CssClass="GView_HeaderCSS" />
                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
                    </asp:GridView>
                </div>
	                </div>
                </div>
            </div>
        </li>
        <li><div id="MessageContent" runat="server" style="color:red;width:800px;"></div></li>
      </ul>
    </div>

    
<asp:HiddenField ID="hidColsNM" runat="server"/>
<asp:HiddenField ID="hidLMCount" runat="server"/>
<asp:HiddenField ID="hidLM2Count" runat="server"/>
<asp:HiddenField ID="hidHotelID" runat="server"/>
<asp:HiddenField ID="hidHotelNo" runat="server"/>
<asp:HiddenField ID="hidSelectedID" runat="server"/>
<asp:HiddenField ID="hidSalesID" runat="server"/>
<script type="text/javascript">
    $(function () {
        //        $("#tabs").tabs();
        var sid = document.getElementById("<%=hidSelectedID.ClientID%>").value;
        if (sid == "" || sid == "0") {
            $("#tabs").tabs();
        }
        else {
            $('#tabs').tabs({ selected: sid, select: function (event, ui) { document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index } });
        }

        $('#tabs').bind('tabsselect', function (event, ui) {
            document.getElementById("<%=hidSelectedID.ClientID%>").value = ui.index
        });

    });
</script>
</asp:Content>