<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="HotelSupMappingPage.aspx.cs"  Title="供应商酒店绑定" Inherits="HotelSupMappingPage" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>

 <script language="javascript" type="text/javascript">
    function ClearClickEvent() {
        document.getElementById("wctHotel").value = "";
        document.getElementById("<%=messageContent.ClientID%>").innerHTML = "";
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

    function SetAClickEvent() {
        $("#MainContent_AspNetPager2 table tbody tr td a[disabled!='disabled']").click(function () { BtnLoadStyle(); });
        $("#MainContent_AspNetPager2 table tbody tr td input[type=submit]").click(function () { BtnLoadStyle(); });

        $("#MainContent_AspNetPager1 table tbody tr td a[disabled!='disabled']").click(function () { BtnLoadStyle(); });
        $("#MainContent_AspNetPager1 table tbody tr td input[type=submit]").click(function () { BtnLoadStyle(); });
    }

    function SetControlValue() {
        document.getElementById("<%=hidHotel.ClientID%>").value = document.getElementById("wctHotel").value;
        document.getElementById("<%=hidSales.ClientID%>").value = document.getElementById("wctSales").value;
    }

    document.onkeydown = function (e) {
        e = e ? e : window.event;
        var keyCode = e.which ? e.which : e.keyCode;
        if (keyCode == 27)
            invokeCloseList();
    }

    //显示弹出的层
    function invokeOpenList() {
        document.getElementById("popupDiv2").style.display = "block";
        //背景
        var bgObj = document.getElementById("bgDiv2");
        bgObj.style.display = "block";
        BtnCompleteStyle();

        if (document.getElementById("MainContent_tdGridAdd") != null) {
            var gdList = document.getElementById("dvgv")
            document.getElementById("MainContent_tdGridAdd").appendChild(gdList);

            if (document.getElementById("<%=hidStyle.ClientID%>").value == "1") {
                document.getElementById("dvgv").style.display = "none";
                var ajaxtdMsg = $("#dvgv");
                if ($("#dvgv").is(":hidden")) {
                    ajaxtdMsg.slideDown(400);
                }
            }
        }

        if (document.getElementById("<%=hidMsg.ClientID%>").value != "") {
            alert(document.getElementById("<%=hidMsg.ClientID%>").value);
        }
    }

    //隐藏弹出的层
    function invokeCloseList() {
        document.getElementById("popupDiv2").style.display = "none";
        document.getElementById("bgDiv2").style.display = "none";
        document.getElementById("<%=hidRoomInfoID.ClientID%>").value = "";
        document.getElementById("<%=btnRefush.ClientID%>").click();
        BtnCompleteStyle();
    }

    function PopupArea(arg, hnm) {
        document.getElementById("<%=hidStyle.ClientID%>").value = "";
        document.getElementById("<%=hidRowID.ClientID%>").value = arg;
        document.getElementById("<%=hidHLNM.ClientID%>").value = hnm;
        document.getElementById("<%=btnLoad.ClientID%>").click();
    }

    function AJSourceList() {
        $.ajax({
            contentType: "application/json",
            url: "HotelSupMappingPage.aspx/SetHotelRoomMappingList",
            type: "POST",
            dataType: "json",
            data: "{HidRowID:'" + document.getElementById("<%=hidRowID.ClientID%>").value + "'}",
            success: function (data) {
            },
            error: function (json) {
            }
        });
    }

    function LaodRoomInfo(arg) {
        document.getElementById("<%=hidStyle.ClientID%>").value = "1";
        document.getElementById("<%=hidRoomInfoID.ClientID%>").value = arg;
        document.getElementById("<%=btnLaodRoomInfo.ClientID%>").click();
    }

    function btnAddRoomMapping(arg) {
        document.getElementById("<%=hidStyle.ClientID%>").value = "";
        document.getElementById("<%=hidRoomInfoID.ClientID%>").value = arg;
        document.getElementById("<%=btnAddRoomMapping.ClientID%>").click();
    }
</script>

 <style type="text/css" >
    .pcbackground  
    {
        opacity: 0.01;
        filter: alpha(opacity=0);
        background: #666666;
        z-index: 10001;
        display:block;
        bottom: 0;
        left: 0;
        position: fixed;
        right: 0;
        top: 0;
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
    z-index: 20001; 
    }
    .bgDiv2
    {
        opacity: 0.5;
        filter: alpha(opacity=50);
        background: #666666;
        z-index: 100;
        display:none;
        bottom: 0;
        left: 0;
        position: fixed;
        right: 0;
        top: 0;
    }
    .popupDiv2
    {
        width: 800px;
        height: 600px;

        position: absolute;
        padding: 1px;
        z-index: 10000;
        display: none;
        background-color: White;
    
    
        top: 15%;
        left: 30%;

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
    
   .GView_BodyCSS2
   {
        margin-left:-11px;
        margin-top:-2px;
        font-size:12px;
        font-family:"Microsoft YaHei",微软雅黑,"MicrosoftJhengHei",华文细黑,STHeiti,MingLiu ; 
        border-collapse:collapse;
        border-spacing:0;
        border-top:1px solid #ccc;
        border-left:1px solid #ccc;
        border-bottom:0px;
        border-right:0px;
   }
   
    .GView_BodyCSS2 td
    {
        border-collapse:collapse;
        border-spacing:0;
        border-width:0px;
        height:30px;
    }
    
    
    
    .GView_BodyCSS3 
   {
        font-size:12px;
        font-family:"Microsoft YaHei",微软雅黑,"MicrosoftJhengHei",华文细黑,STHeiti,MingLiu ; 
        border:1px #d5d5d5;
        border-collapse:collapse;
        color:Black;
	    width:100%;
	    height:100%;
	    border-width:1px;
	    margin-left:-10px;
	    margin-top:-1px;
	    margin-bottom:0px;
   }

    .GView_BodyCSS3 td
    {
        border:1px #d5d5d5 solid;
        border-collapse:collapse;
        color:Black;
        height:25px;
    }

    .GView_BodyCSS3 th
    {
        border:1px #d5d5d5 solid;
        border-collapse:collapse;
        height:25px;
        background-color:white;
    }
    </style>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div id="right">
   <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
      <ContentTemplate>
    <div class="frame01">
      <ul>
        <li class="title">供应商酒店绑定</li>
        <li>
            <table style="line-height:35px">
                <tr>
                    <td>酒店匹配：</td>
                    <td>
                        <asp:RadioButton ID="rdbAll" GroupName="rdbOnline" runat="server" Text="不限制" Checked="true" />
                        <asp:RadioButton ID="rdbOn" GroupName="rdbOnline" runat="server" Text="已绑定供应商"/>
                        <asp:RadioButton ID="rdbOff" GroupName="rdbOnline" runat="server" Text="未绑定供应商"/>
                    </td>
                    <td>房型匹配：</td>
                    <td>
                        <asp:RadioButton ID="rdbRoomAll" GroupName="rdbOnRoom" runat="server" Text="不限制" Checked="true" />
                        <asp:RadioButton ID="rdbRoomOn" GroupName="rdbOnRoom" runat="server" Text="已绑定所有房型"/>
                        <asp:RadioButton ID="rdbRoomOff" GroupName="rdbOnRoom" runat="server" Text="未完全绑定"/>
                        <asp:RadioButton ID="rdbRoomOffAll" GroupName="rdbOnRoom" runat="server" Text="未绑定任何房型"/>
                    </td>
                </tr>
                <tr>
                    <td>选择酒店：</td>
                    <td>
                        <uc1:WebAutoComplete ID="wctHotel" runat="server" CTLID="wctHotel" AutoType="hotel" AutoParent="HotelSupMappingPage.aspx?Type=hotel" />
                        <%--<input name="textfield" type="text" id="txtHVPID" runat="server" style="width:200px;" maxlength="200" value=""/>--%>
                    </td>
                    <td>销售人员：</td>
                    <td>
                        <uc1:WebAutoComplete ID="wctSales" CTLID="wctSales" runat="server" AutoType="sales" AutoParent="HotelSupMappingPage.aspx?Type=sales" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" OnClientClick="SetControlValue();BtnLoadStyle()" onclick="btnSearch_Click" Text="搜索"/>
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
         <div style="margin-left:10px;"><div id="messageContent" runat="server" style="color:red;width:800px;"></div></div>
         <div style="margin-left:10px;"><webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager></div>
         <asp:GridView ID="gridViewCSReviewList" runat="server" AutoGenerateColumns="False" BackColor="White" onrowdatabound="gridViewCSReviewList_RowDataBound" 
         CellPadding="4" CellSpacing="1" Width="100%" EmptyDataText="没有数据" DataKeyNames="ID" PageSize="20"  CssClass="GView_BodyCSS">
                <Columns>
                    <asp:BoundField DataField="HVPID" HeaderText="HVP酒店ID" ><ItemStyle HorizontalAlign="Center" Width="20%"/></asp:BoundField>
                    <asp:BoundField DataField="HOTELNM" HeaderText="酒店名称" ><ItemStyle HorizontalAlign="Center" Width="40%"/></asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="酒店绑定情况">
                      <ItemTemplate>
                      <asp:Image ID="imgHotelPic" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "HBIMG") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "HBMSG") %>'/>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                  </asp:TemplateField>
                  <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="房型绑定情况">
                      <ItemTemplate>
                      <asp:Image ID="imgRoomPic" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "RBIMG") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "RBMSG") %>'/>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                  </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                <PagerStyle HorizontalAlign="Right" />
                <RowStyle CssClass="GView_ItemCSS" />
                <HeaderStyle CssClass="GView_HeaderCSS" />
                <AlternatingRowStyle CssClass="GView_AlternatingItemCSS"  />
            </asp:GridView>
            <div style="margin-left:10px;">
            <webdiyer:AspNetPager CssClass="paginator" CurrentPageButtonClass="cpb"  ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" CustomInfoHTML="总记录数：%RecordCount%&nbsp;&nbsp;&nbsp;总页数：%PageCount%&nbsp;&nbsp;&nbsp;当前为第&nbsp;%CurrentPageIndex%&nbsp;页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" CustomInfoSectionWidth="80%" ShowPageIndexBox="always" AlwaysShow="true" width="100%" LayoutType="Table" onpagechanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
         </div>
        </div>


        <div id="background" class="pcbackground" style="display: none; "></div>
        <div id="progressBar" class="pcprogressBar" style="display: none; ">数据加载中，请稍等...</div>
        <asp:HiddenField ID="hidHotel" runat="server"/>
        <asp:HiddenField ID="hidSales" runat="server"/>
        <asp:HiddenField ID="hidRowID" runat="server"/>
        <asp:HiddenField ID="hidHLNM" runat="server"/>
        <asp:HiddenField ID="hidRoomInfoID" runat="server"/>
        <asp:HiddenField ID="hidStyle" runat="server"/>
        <asp:HiddenField ID="hidMsg" runat="server"/>
        <div style="display:none"><asp:Button ID="btnLoad" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle()" onclick="btnLoad_Click" Text="搜索"/></div>
        <div id="bgDiv2" class="bgDiv2"></div>
        </ContentTemplate>
         <Triggers>
        <asp:PostBackTrigger ControlID="AspNetPager2" />
        </Triggers>
    </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                 <div id="popupDiv2" class="popupDiv2">
            <div style="width:99%;height:99%;margin:0,0,0,0">
                <table style="border:0px; padding:0px; margin:5px 0px 0px 3px;width:99.6%">
                    <tr align="left">
                        <td align="left" valign="middle" style="background-color:#6379B2; height:30px; padding:5px 5px 3px 10px; font-weight:bold;color:White;">
                        <div style="float:left;margin-top:0px">供应商酒店绑定</div>
                        <div style="float:right;margin-right:-5px">
                            <input type="button" style="background-color:#6379B2;border:none;display:block;color:White;" id="btnClose" value="╳" onclick="invokeCloseList()" />
                        </div>
                        </td>
                    </tr>
                </table>
                <div style="width:100%;height:550px; overflow-y:auto;margin-top:-2px">
                <table runat="server" id="tbDetail" class="Tb_BodyCSS" style="border:1px #d5d5d5 solid; padding:1px; margin:-3px 0px 0px 5px;width:99.1%;">
                <tr>
                    <td style="width:10%">
                        酒店名称：
                    </td>
                    <td style="height:35px">
                        <div style="float:left;margin-top:5px"><asp:Label ID="lbHotelNM" runat="server"></asp:Label></div>
                        <div style="float:right;margin-top:2px;margin-right:5px">
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                        <div style="display:none">
                            <asp:Button ID="btnRefush" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle()" onclick="btnRefush_Click" Text="搜索"/>
                            <asp:Button ID="btnLaodRoomInfo" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle()" onclick="btnLaodRoomInfo_Click" Text="搜索"/>
                            <asp:Button ID="btnAddRoomMapping" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle()" onclick="btnAddRoomMapping_Click" Text=" + 添加房型供应商"/>
                        </div>
                        <asp:Button ID="btnAddHotelMapping" runat="server" CssClass="btn primary" OnClientClick="BtnLoadStyle()" onclick="btnAddHotelMapping_Click" Text=" + 添加酒店供应商"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        酒店绑定：
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                         <asp:GridView ID="gridViewCSList" runat="server" AutoGenerateColumns="False" BackColor="White" 
                            CellPadding="4" CellSpacing="1" Width="101.8%" CssClass="GView_BodyCSS2" 
                                    ShowHeader="false" EmptyDataText = "&nbsp;&nbsp;未绑定供应商酒店"
                            onrowcancelingedit="gridViewCSList_RowCancelingEdit" 
                                    onrowediting="gridViewCSList_RowEditing" 
                                    onrowupdating="gridViewCSList_RowUpdating" 
                                    onrowcreated="gridViewCSList_RowCreated">
                            <Columns>
                            <asp:TemplateField HeaderText="供应商">
                                <ItemTemplate>
                                    <asp:Label ID="lblSORC" runat="server"  Text='<%# Eval("SORC")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Right" Width="11%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商">
                                <EditItemTemplate>
                                        <asp:DropDownList ID="ddlSupDp" runat="server" DataSource='<%# ddlDDpbind()%>' Width="100px" DataValueField="SUPID" DataTextField="SUPNM">
                                            </asp:DropDownList>
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="lblSUPNM" runat="server"  Text='<%# Eval("SUPNM")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left" Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商IDT">
                                <ItemTemplate>
                                    <asp:Label ID="lblSORCID" runat="server"  Text='<%# Eval("SORCID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Right" Width="15%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="供应商ID">
                                <EditItemTemplate>
                                        <asp:TextBox ID="txtSUPIDEdit" runat="server" Text='<%# Eval("SUPID")%>' Width="100" MaxLength="20"></asp:TextBox>
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="lbSUPIDEdit" runat="server"  Text='<%# Eval("SUPID")%>'></asp:Label>
                                </ItemTemplate> 
                                <ItemStyle  Width="15%" HorizontalAlign="Left" />
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="状态T">
                                <ItemTemplate>
                                    <asp:Label ID="lblINUSERT" runat="server"  Text='<%# Eval("INUSERT")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Right" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态">
                                <EditItemTemplate>
                                        <asp:CheckBox ID="chkInUse" runat="server" Text="上线"/>
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="lbInUse" runat="server"  Text='<%# Eval("INUSERDIS")%>'></asp:Label>
                                </ItemTemplate> 
                                <ItemStyle  Width="10%" HorizontalAlign="Left" />
                            </asp:TemplateField> 
                            <asp:CommandField ShowEditButton="True" HeaderText="编辑" EditText="编辑" UpdateText="保存" CancelText="取消">
                                <ItemStyle HorizontalAlign="Center" Width="14%" />
                            </asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <AlternatingRowStyle CssClass="GView_ItemCSS" />
                        </asp:GridView>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        房型绑定：
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                            <%--<div id="dvRoomBand" runat="server"></div> EnableViewState='false' CausesValidation='false'--%>
                            <table width="102%" style="margin-left:-11.5px;margin-top:-2px" runat="server" id="tbRoomBand" class="Tb_BodyCSS" >
                            
                            </table>

                            <%--<table width="101.8%" style="margin-left:-10px;margin-top:-2px">
                            <tr>
                                <td>
                                <div style="float:left;">
                                    <img src="../../Styles/images/star.png" alt=""/> 
                                </div>
                                <div style="float:left;margin-top:5px">&nbsp;[FT] 高级双床房</div>
                                </td>
                            </tr>
                            <tr>
                               <td style="background:#6379B2;height:35px" onclick="LaodRoomInfo('SD')">
                                    <div style="float:left;">
                                        <img src="../../Styles/images/hstar.png" alt=""/> 
                                    </div>
                                    <div style="float:left;margin-top:6px">&nbsp;[SD] 标间</div>
                                    <div style="float:right;margin-top:2px;margin-right:5px">
                                        <input type="button" class="btn primary" id="btnAddRoom" value=" + 添加房型供应商" onclick="btnAddRoomMapping('SD')" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="101.7%" style="margin-left:-10px;margin-top:-2px">
                                        <tr>
                                            <td style="width:25%;background-color:#DDDDDD" align="center">供应商</td>
                                            <td style="width:25%;background-color:#DDDDDD" align="center">供应商ID</td>
                                            <td style="width:25%;background-color:#DDDDDD" align="center">状态</td>
                                            <td style="width:25%;background-color:#DDDDDD" align="center">操作</td>
                                        </tr>
                                        <tr>
                                            <td align="center">艺龙</td>
                                            <td align="center">122391</td>
                                            <td align="center">上线</td>
                                            <td align="center"><input type="button" class="btn" id="btnModify" value='编辑' onclick="" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center"><asp:DropDownList ID="ddpSupRDDP" runat="server"></asp:DropDownList></td>
                                            <td align="center"><asp:DropDownList ID="ddpSupRID" runat="server"></asp:DropDownList></td>
                                            <td align="center"><asp:CheckBox ID="CheckBox1" runat="server" Text="上线" /></td>
                                            <td align="center"><input type="button" class="btn primary" id="Button4" value='保存' onclick="" style="margin-right:5px" /><input type="button" class="btn" id="Button5" value='取消' onclick="" style="margin-right:5px" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <div style="float:left;">
                                    <img src="../../Styles/images/star.png" alt=""/> 
                                </div>
                                <div style="float:left;margin-top:5px">&nbsp;[FT] 高级双床房</div>
                                </td>
                            </tr>
                            <tr>
                                <td runat="server" id="tdGrid">
                                    
                                </td>
                            </tr>
                        </table>--%>
                            <div style="width:100%;" id="dvgv">
                            <asp:GridView ID="gridViewRoom" runat="server" AutoGenerateColumns="False" BackColor="White"
                            CellPadding="4" CellSpacing="1" Width="101.7%" CssClass="GView_BodyCSS3" EmptyDataText = "&nbsp;&nbsp;未绑定供应商房型"
                            onrowcancelingedit="gridViewRoom_RowCancelingEdit" onrowediting="gridViewRoom_RowEditing" onrowupdating="gridViewRoom_RowUpdating"
                            OnRowCreated ="gridViewRoom_RowCreated"
                            >
                            <Columns>
                            <asp:TemplateField HeaderText="供应商">
                                <EditItemTemplate>
                                        <asp:DropDownList ID="ddlSupDp" runat="server" DataSource='<%# ddlDDpRoombind()%>' Width="100px" DataValueField="SUPID" DataTextField="SUPNM" AutoPostBack="true" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged">
                                            </asp:DropDownList>
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="lblSUPNM" runat="server"  Text='<%# Eval("SUPNM")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Center" Width="15%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="供应商房型ID">
                                <EditItemTemplate>
                                        <%--<asp:TextBox ID="txtSUPIDEdit" runat="server" Text='<%# Eval("SUPID")%>' Width="100" MaxLength="20"></asp:TextBox>--%>
                                        <div runat="server" id="dvGDP" style="display:none"><asp:DropDownList ID="DDlRoom" runat="server" Width="280px" ></asp:DropDownList></div>
                                        <div runat="server" id="dvGTX" style="display:none"><asp:TextBox ID="txtRoomIDEdit" runat="server" Width="150" MaxLength="20"></asp:TextBox></div>
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="lbSUPIDEdit" runat="server"  Text='<%# Eval("SUPID")%>'></asp:Label>
                                </ItemTemplate> 
                                <ItemStyle  Width="35%" HorizontalAlign="Center" />
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="状态">
                                <EditItemTemplate>
                                        <asp:CheckBox ID="chkInUse" runat="server" Text="上线"/>
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="lbInUse" runat="server"  Text='<%# Eval("INUSERDIS")%>'></asp:Label>
                                </ItemTemplate> 
                                <ItemStyle  Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField> 
                            <asp:CommandField ShowEditButton="True" HeaderText="操作" EditText="编辑" UpdateText="保存" CancelText="取消" >
                                <ItemStyle HorizontalAlign="Center" Width="15%"/>
                            </asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black"/>
                        <PagerStyle HorizontalAlign="Right" />
                        <RowStyle CssClass="GView_ItemCSS" />
                        <AlternatingRowStyle CssClass="GView_ItemCSS" />
                        </asp:GridView>
                        </div>

                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                </table>
                </div>
            </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>