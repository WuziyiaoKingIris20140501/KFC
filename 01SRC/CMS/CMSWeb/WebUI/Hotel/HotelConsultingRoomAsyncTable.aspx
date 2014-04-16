<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HotelConsultingRoomAsyncTable.aspx.cs" Inherits="WebUI_Hotel_HotelConsultingRoomAsyncTable" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <script language="javascript" type="text/javascript">
        $(function(){
             if($.browser.msie) {
                document.getElementById("<%=HidBrowser.ClientID%>").value = "IE";
             }else if($.browser.opera) {
                document.getElementById("<%=HidBrowser.ClientID%>").value = "Opera";
             }else if($.browser.mozilla) {
                document.getElementById("<%=HidBrowser.ClientID%>").value = "FireFox";
             }else if($.browser.safari) {
                document.getElementById("<%=HidBrowser.ClientID%>").value = "Safari";            
             }
        });

        function ClickEvent(pcode, pid, selIndex, cityId,flag) {
            if(flag=="true"){
              BtnLoadStyle();
            }
            document.getElementById("<%=HidPcode.ClientID%>").value = pcode;
            document.getElementById("<%=HidPid.ClientID%>").value = pid;
            document.getElementById("<%=HidSelIndex.ClientID%>").value = selIndex;
            document.getElementById("<%=HidCityID.ClientID%>").value = cityId;
            document.getElementById("<%=Button1.ClientID%>").click();
        }
        function RecordPostion(obj) {
            var div1 = obj;
            var sx = document.getElementById("<%=dvscrollX.ClientID%>");
            var sy = document.getElementById("<%=dvscrollY.ClientID%>");
            sy.value = div1.scrollTop;
            sx.value = div1.scrollLeft;
        }

        function GetResultFromServer() {
            try {
                if (document.getElementById("<%=HidScrollValue.ClientID%>").value == '' || document.getElementById("<%=HidScrollValue.ClientID%>").value == 'NaN') {
                    document.getElementById("<%=HidScrollValue.ClientID%>").value = '0';
                }
                var sx = document.getElementById("<%=dvscrollX.ClientID%>");
                var sy = parseInt(document.getElementById("<%=dvscrollY.ClientID%>").value) + parseInt(document.getElementById("<%=HidScrollValue.ClientID%>").value);
                document.getElementById("<%=dvGridView.ClientID%>").scrollTop = sy;
                document.getElementById("<%=dvGridView.ClientID%>").scrollLeft = sx.value;
                document.getElementById("<%=HidScrollValue.ClientID%>").value = '0';
            }
            catch (e) { }
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

        function showDiv(hotelName, priceCode, roomame, roomCode, twoPrice, roomnum, status, isreserve, effectDate) {
            BtnLoadStyle();
            invokeOpen2();

            document.getElementById("<%=HiddenRoomType.ClientID%>").value = roomame;
            document.getElementById("<%=HiddenPrice.ClientID%>").value = twoPrice;
            document.getElementById("<%=HiddenPriceCode.ClientID%>").value = priceCode; //价格代码 
            document.getElementById("<%=HiddenEffectDate.ClientID%>").value = effectDate;
            document.getElementById("<%=HiddenRoomCode.ClientID%>").value = roomCode;
            document.getElementById("<%=HiddenRoomNum.ClientID%>").value = roomnum;
            document.getElementById("<%=HiddenPlanStatus.ClientID%>").value = status;
            document.getElementById("<%=HiddenIsReserve.ClientID%>").value = isreserve;
            
            document.getElementById("<%=lblDivHotelName.ClientID%>").innerHTML = document.getElementById("<%=HidPcode.ClientID%>").value;
            document.getElementById("<%=lblDivRoomType.ClientID%>").innerHTML = roomame;
            document.getElementById("<%=lblDivPrice.ClientID%>").innerHTML = twoPrice;
            document.getElementById("<%=txtDivRoomCount.ClientID%>").value = roomnum;
            document.getElementById("<%=DivlblLinkDetails.ClientID%>").innerHTML = document.getElementById("<%=HidLinkDetails.ClientID%>").value;
            document.getElementById("<%=DivlblContactDetails.ClientID%>").innerHTML = document.getElementById("<%=HidContactDetails.ClientID%>").value;
             if(status=="true")
             {
                showRoomDiv();
                document.getElementById("<%=dropDivStatusOpen.ClientID%>").checked = true;
                document.getElementById("<%=dropDivStatusClose.ClientID%>").checked = false;
             }
             else
             {
                closeRoomDiv();
                document.getElementById("<%=dropDivStatusOpen.ClientID%>").checked = false;
                document.getElementById("<%=dropDivStatusClose.ClientID%>").checked = true;
             }

             if(isreserve=="0")
             {
                document.getElementById("<%=ckDivReserve.ClientID%>").checked = true;
             }
             else
             {
                document.getElementById("<%=ckDivReserve.ClientID%>").checked = false;
             } 
             document.getElementById("<%=divPlanStartDate.ClientID%>").value=effectDate;
             document.getElementById("<%=divPlanEndDate.ClientID%>").value=effectDate;

             document.getElementById("<%=LmbarRemarkHistory.ClientID%>").innerHTML="";

             
            $.ajax({
                async : true,
                contentType: "application/json",
                url: "HotelConsultingRoomAsyncTable.aspx/GetHistoryRemarkByJson",
                type: "POST",
                dataType: "json",
                data: "{CityID:'" + document.getElementById("<%=HidCityID.ClientID%>").value + "',HotelID:'" + document.getElementById("<%=HidPid.ClientID%>").value + "',PriceCode:'" + document.getElementById("<%=HiddenPriceCode.ClientID%>").value + "',RoomCode:'" + document.getElementById("<%=HiddenRoomCode.ClientID%>").value + "',PlanDTime:'" + document.getElementById("<%=HiddenEffectDate.ClientID%>").value  + "'}",
                success: function (data) {
                debugger;
                    var output = "<table style=\"width:100%\" cellpadding=\"0\" cellspacing=\"0\"><tr style=\"line-height:30px;\"><td style=\"width:135px;text-align:center\">操作时间</td><td style=\"width:100px;text-align:center\">操作人</td><td style=\"width:70px;text-align:center\">价格</td><td style=\"width:50px;text-align:center\">状态</td><td style=\"width:50px;text-align:center\">房量</td><td style=\"width:50px;text-align:center\">保留房</td><td>备注</td></tr>";
                    var d=jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        output+="<tr style=\"line-height:30px;\"><td style=\"width:135px;text-align:center\">"+d[i].Create_Time+"</td><td style=\"width:100px;text-align:center\">"+d[i].Create_User+"</td><td style=\"width:70px;text-align:center\">"+d[i].TwoPrice+"</td><td style=\"width:50px;text-align:center\">"+d[i].Status+"</td><td style=\"width:50px;text-align:center\">"+d[i].RoomNum+"</td><td style=\"width:50px;text-align:center\">"+d[i].IsReserve+"</td>";
                        if(d[i].Remark!="" && d[i].Remark.length>20){
                        output+="<td title=\""+d[i].Remark+"\">"+d[i].Remark.substring(0,20)+"..."+"</td></tr>";
                        }else
                        {
                        output+="<td title=\""+d[i].Remark+"\">"+d[i].Remark+"</td></tr>";    
                        }
                    });
                    output+="</table>";
                    document.getElementById("<%=LmbarRemarkHistory.ClientID%>").innerHTML = output;
                },
                error: function (json) {
                    
                }
            });            
        } 

        function showA() {
            $.ajax({
                async: true,
                contentType: "application/json",
                url: "HotelConsultingRoomAsyncTable.aspx/GetNextOrLastHotelDetails",
                type: "POST",
                dataType: "json",
                data: "{JudgeLast:'" + document.getElementById("<%=HidJudgeLast.ClientID%>").value + "',JudgeNext:'" + document.getElementById("<%=HidJudgeNext.ClientID%>").value + "',startDate:'" + document.getElementById("<%=planStartDate.ClientID%>").value + "',endDate:'" + document.getElementById("<%=planEndDate.ClientID%>").value + "',LastHotelSelectName:'" + document.getElementById("<%=HidLastHotelSelectName.ClientID%>").value + "',NextHotelSelectName:'" + document.getElementById("<%=HidNextHotelSelectName.ClientID%>").value + "'}",
                success: function (data) {
                },
                error: function (json) {

                }
            });
        }

        //显示弹出的层
        function invokeOpen2() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
        }

        //隐藏弹出的层
        function invokeClose2() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
            GetResultFromServer();
        }

        //显示弹出的层  AlertRemark  DivAlertRemark  DivAlertRemarkMain
        function invokeOpenRemark() {
            document.getElementById("DivAlertRemark").style.display = "block";
            //背景
            var bgObj = document.getElementById("DivAlertRemarkMain");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
        }

        //隐藏弹出的层  AlertRemark
        function invokeCloseRemark() {
            document.getElementById("DivAlertRemark").style.display = "none";
            document.getElementById("DivAlertRemarkMain").style.display = "none";
            GetResultFromServer();
        }


        function showRoomDiv() {
            $("#MainContent_txtDivRoomCount").removeAttr("disabled", "");
            $("#MainContent_ckDivReserve").removeAttr("disabled", "");
        }

        function closeRoomDiv() {
            document.getElementById("MainContent_txtDivRoomCount").disabled = "true";
            document.getElementById("MainContent_ckDivReserve").disabled = "true";
        }

        function GetCheckBoxListValue() {
            document.getElementById("<%=btnDivRenewPlan.ClientID%>").value = '更新中...';

            var dropStatusOpen = document.getElementById("<%=dropDivStatusOpen.ClientID%>").checked; //状态  开启 关闭  
            if (dropStatusOpen) {
                var RoomCount = document.getElementById("<%=txtDivRoomCount.ClientID%>").value;
                if (RoomCount == "") {
                    alert("请输入需要更新房型的房量!");
                    document.getElementById("<%=btnDivRenewPlan.ClientID%>").value = '更新计划';
                    return false;
                }
            }
            var divPlanStartDate = document.getElementById("<%=divPlanStartDate.ClientID%>").value;
            var divPlanEndDate = document.getElementById("<%=divPlanEndDate.ClientID%>").value;
            if (divPlanStartDate == "" || divPlanEndDate == "") {
                alert("请选择批量更新计划的时间!");
                document.getElementById("<%=btnDivRenewPlan.ClientID%>").value = '更新计划';
                return false;
            }
            BtnLoadStyle();
            return true;
        }

        function LastOrNextByHotel(obj) {
            BtnLoadStyle();
            if (obj == '1') {
                document.getElementById("<%=HidScrollValue.ClientID%>").value = '30';
            } else {
                document.getElementById("<%=HidScrollValue.ClientID%>").value = '-30';
            }
            document.getElementById("<%=HidLastOrNextByHotel.ClientID%>").value = obj;
            document.getElementById("<%=Button3.ClientID%>").click();
        }

        function SetSalesRoom(obj) {
            document.getElementById("wcthvpInventoryControl").value = obj;
        }
        function SetControlValue() {
            document.getElementById("<%=hidSelectHotel.ClientID%>").value = document.getElementById("wctHotel").value;
            document.getElementById("<%=hidSelectCity.ClientID%>").value = document.getElementById("wctCity").value;
            document.getElementById("<%=hidSelectBussiness.ClientID%>").value = document.getElementById("wcthvpTagInfo").value;
            document.getElementById("<%=hidSelectSalesID.ClientID%>").value = document.getElementById("wcthvpInventoryControl").value;

             var select2 = document.all.<%= DropDownList2.ClientID %>;
            var selectvalue = select2.options[select2.selectedIndex].value;
            document.getElementById("<%=HidDdlSelectValue.ClientID%>").value = selectvalue;
        }

        function MarkFullRoom(obj) {
            var s = '';
            $('input[name="chkMarkFullRoom"]:checked').each(function () {
                s += $(this).val() + ',';
            });
            if (s == '') {
                alert("你还没有选择任何内容！");
                return false;
            } else {
                s = s.substring(0, s.length - 1);
                document.getElementById("<%=HidMarkFullRoom.ClientID%>").value = s;
                document.getElementById("<%=HidCloseOrFullByRoom.ClientID%>").value = obj;
                invokeOpenRemark();
            }
            return true;
        }

        function btnAlert()
        {
            alert("1.当天无计划的酒店，全部过滤掉；\n2.当选择房控人员时，过滤房控人员下面所有当天计划全部被关闭，并且关闭人全部是销售人员的酒店；\n3.当天下线的酒店，全部过滤掉；\n4.过滤所有非自签酒店；\n5过滤HUB1签约的锦江之星");
        }

        function ClearLock()
        {
            document.getElementById("<%=btnClearLock.ClientID%>").style.display ="none";
            document.getElementById("<%=btnEditRemark.ClientID%>").style.display ="";

            document.getElementById("<%=SPANHotelEXLinkRemark.ClientID%>").style.display ="none";
            document.getElementById("<%=TXTotelEXLinkRemark.ClientID%>").style.display ="";

            document.getElementById("<%=SPANHotelEXLinkMan.ClientID%>").style.display ="none";
            document.getElementById("<%=TXTHotelEXLinkMan.ClientID%>").style.display ="";

            document.getElementById("<%=SPANHotelEXLinkTel.ClientID%>").style.display ="none";
            document.getElementById("<%=TXTHotelEXLinkTel.ClientID%>").style.display ="";
        }

        function EditRemark()
        {
            BtnLoadStyle();
            document.getElementById("<%=Button8.ClientID%>").click();
        }

        function AlertLMLinkWay()
        {
            var Details = document.getElementById("<%=HidAlertContactDetails.ClientID%>").value;
            alert(Details);
        }
    </script>
    <style type="text/css">
        .pcbackground
        {
            display: block;
            width: 100%;
            height: 100%;
            opacity: 0.4;
            filter: alpha(opacity=40);
            background: #666666;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 2000;
        }
        .pcprogressBar
        {
            border: solid 2px #3A599C;
            background: white url("/images/progressBar_m.gif") no-repeat 10px 10px;
            display: block;
            width: 148px;
            height: 28px;
            position: fixed;
            top: 40%;
            left: 40%;
            margin-left: -74px;
            margin-top: -14px;
            padding: 10px 10px 10px 50px;
            text-align: left;
            line-height: 27px;
            font-weight: bold;
            position: absolute;
            z-index: 2001;
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
        .popupDiv2
        {
            width: 800px;
            height: 620px;
            top: 15%;
            left: 18%;
            position: absolute;
            padding: 1px;
            vertical-align: middle;
            border: solid 2px #ff8300;
            z-index: 1001;
            display: none;
            background-color: White;
            top: 15%;
            left: 25%;
            margin-left: -150px !important; /*FF IE7 该值为本身宽的一半 */
            margin-top: -50px !important; /*FF IE7 该值为本身高的一半*/
            margin-left: 0px;
            margin-top: 0px;
            position: fixed !important; /*FF IE7*/
            position: absolute; /*IE6*/
            _top: expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
                document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2); /*IE5 IE5.5*/
            _left: expression(eval(document.compatMode &&
                document.compatMode=='CSS1Compat') ?
                documentElement.scrollLeft + (document.documentElement.clientWidth-this.offsetWidth)/2 :/*IE6*/
                document.body.scrollLeft + (document.body.clientWidth - this.clientWidth)/2); /*IE5 IE5.5*/
        }
        #txtRemark
        {
            height: 56px;
            width: 422px;
        }
        .style1
        {
            width: 34px;
            text-align: right;
        }
        .GView_ItemCSS
        {
            /*奇数行*/ /*background:url(../images/bg-frame0201.gif);*/
            background: white;
            line-height: 30px;
        }
        
        .GView_AlternatingItemCSS
        {
            /*偶数行*/
            background: #f6f6f6;
            border-top: 1px #e6e5e5 solid;
            border-bottom: 1px #e6e5e5 solid;
            line-height: 30px;
            border-bottom-color: #e6e5e5;
            border-top-color: #e6e5e5;
        }
        
        .lblLinkDetails
        {
            font-weight: bold;
            font-size: large;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="36000" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="frame01" style="margin: 8px 14px 5px 14px;">
                <ul>
                    <li class="title">酒店巡房表</li>
                    <li>
                        <table width="98%">
                            <tr>
                                <td>
                                    选择酒店：
                                </td>
                                <td>
                                    <uc1:WebAutoComplete ID="wctHotel" CTLID="wctHotel" runat="server" AutoType="hotel"
                                        AutoParent="HotelConsultingRoomTable.aspx?Type=hotel" />
                                </td>
                                <td>
                                    城市：
                                </td>
                                <td colspan="3">
                                    <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city"
                                        EnableViewState="false" AutoParent="HotelConsultingRoomTable.aspx?Type=city" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    商圈：
                                </td>
                                <td>
                                    <uc1:WebAutoComplete ID="wcthvpTagInfo" runat="server" CTLID="wcthvpTagInfo" AutoType="hvptaginfo"
                                        EnableViewState="false" AutoParent="HotelConsultingRoomTable.aspx?Type=hvptaginfo" />
                                </td>
                                <td>
                                    房控人员:
                                </td>
                                <td colspan="3">
                                    <uc1:WebAutoComplete ID="wcthvpInventoryControl" CTLID="wcthvpInventoryControl" runat="server"
                                        EnableViewState="false" AutoType="hvpInventoryControl" AutoParent="HotelConsultingRoomTable.aspx?Type=hvpInventoryControl" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    查房频率:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    开卖时间：
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                                        <asp:ListItem Value="6">6点</asp:ListItem>
                                        <asp:ListItem Value="14">14点</asp:ListItem>
                                        <asp:ListItem Value="18">18点</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    计划起止日期：
                                </td>
                                <td>
                                    <input id="planStartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_planEndDate\')||\'2020-10-01\'}'})"
                                        runat="server" />
                                    至：
                                    <input id="planEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_planStartDate\')}',maxDate:'2020-10-01'})"
                                        runat="server" />
                                </td>
                                <td>
                                    排序方式:
                                </td>
                                <td style="width: 100px;">
                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem Value=" ordercount desc ">销量排序</asp:ListItem>
                                        <asp:ListItem Value=" prop_name_zh asc ">名称排序</asp:ListItem>
                                        <asp:ListItem Value=" CITYID asc ">城市排序</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right; width: 80px;">
                                    上线时间：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="radioListBookStatus" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSelect" runat="server" CssClass="btn primary" Text="选择" OnClientClick="BtnLoadStyle();SetControlValue();"
                                                OnClick="btnSelect_Click" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="重置" Visible="false" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="5">
                                    <input type="button" id="btnAlert" style="float: right;" class="btn primary" runat="server"
                                        value="过滤提示" onclick="btnAlert()" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-left: 10px;">
                                                <div id="messageContent" runat="server" style="color: red; width: 400px;">
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%" style="height: 600px;">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="frame01" style="width: 100%; height: 600px; margin-left: 5px;">
                                        <ul>
                                            <li class="title">酒店列表 <span style="float: right;"><span id="operandNum" runat="server">
                                                0</span>/<span id="countNum" runat="server">0</span></span></li>
                                        </ul>
                                        <ul>
                                            <li style="padding: 0px 0px 0px 0px;">
                                                <div id="dvGridView" style="overflow: auto; height: 570px; width: 100%;" runat="server"
                                                    onscroll="RecordPostion(this);">
                                                    <asp:GridView ID="gridHotelList" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        Width="100%" DataKeyNames="PROP,CITYID,LINKMAN,LINKTEL,LINKEMAIL,SALES_ACCOUNT,PROP_NAME_ZH,EXLinkMan,EXLinkTel,EXRemark,BackPropName"
                                                        OnRowDataBound="gridHotelList_RowDataBound" CssClass="GView_BodyCSS">
                                                        <Columns>
                                                            <asp:BoundField DataField="CITYID" HeaderText="CITYID" />
                                                            <asp:TemplateField>
                                                                <HeaderStyle Width="240px" />
                                                                <HeaderTemplate>
                                                                    酒店名称</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl02" runat="server" Text='<%#Eval("PROP_NAME_ZH").ToString().Length>17? Eval("PROP_NAME_ZH").ToString().Substring(0,16)+"...":Eval("PROP_NAME_ZH").ToString()%>'
                                                                        ToolTip='<%#Bind("PROP_NAME_ZH") %>'></asp:Label>
                                                                    <br />
                                                                    <%--<div style="background-color:#ECECEC" id="LinkDiv" runat="server"><asp:Label ID="Label1" runat="server" Text='<%# Eval("EXLinkMan")+""+Eval("EXLinkTel")+"" %> '
                                                                        ToolTip='<%#Bind("BackPropName") %>' ></asp:Label></div>--%>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("EXLinkMan")+""+Eval("EXLinkTel")+"" %> '
                                                                        ToolTip='<%#Bind("BackPropName") %>' BackColor="#ECECEC" Width="100%"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CITYID" HeaderText="CITYID" Visible="false" />
                                                            <asp:BoundField DataField="isplan" HeaderText="是否有计划" Visible="false" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <RowStyle CssClass="GView_ItemCSS" />
                                                        <HeaderStyle CssClass="GView_HeaderCSS" />
                                                        <AlternatingRowStyle CssClass="GView_AlternatingItemCSS" />
                                                        <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
                                                    </asp:GridView>
                                                    <div id="Popup" class="transparent" style="z-index: 200" runat="server">
                                                        <table border="0" cellpadding="0" style="font-size: x-small" width="200px">
                                                            <tr>
                                                                <td id="td1">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 1%;">
            </td>
            <td style="width: 75%;">
                <table width="98%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="frame01" style="width: 100%; height: 600px; margin-left: -5px;">
                                        <ul>
                                            <li class="title"><span id="spanHotelInfo" runat="server"></span><span style="float: right;">
                                                <span style="text-align: right; color: White">█ </span>无计划&nbsp;&nbsp;&nbsp;<span
                                                    style="text-align: right; color: #CDEBFF;">█ </span>周末&nbsp;&nbsp;&nbsp;<span style="text-align: right;
                                                        color: #E6B9B6">█ </span>满房&nbsp;&nbsp;&nbsp;<span style="text-align: right; color: #E96928">█
                                                        </span>CC操作满房&nbsp;&nbsp;&nbsp;<span style="text-align: right; color: #999999">█
                                                </span>计划关闭&nbsp;&nbsp;&nbsp;<span style="text-align: right; color: Red">* </span>
                                                保留房&nbsp;&nbsp;&nbsp;</span></li>
                                        </ul>
                                        <ul>
                                            <li>
                                                <div style="height: 60px; width: 100%; margin-left: -10px;">
                                                    <div style="float: right; vertical-align: super; width: 100%; display: none;" id="DivLastOrNext"
                                                        runat="server">
                                                        <asp:Label ID="lblHotelInfo" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblLinkDetails" CssClass="lblLinkDetails" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblContactDetails" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <table style="width: 100%; border: 1px solid #8D8D8D; background-color: #E9E9E9"
                                                                    cellpadding="0" cellspacing="0">
                                                                    <tr style="height: 34px; margin: 0px 5px 5px 0px;">
                                                                        <td style="border-bottom: 1px solid #8D8D8D; width: 45%;">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;酒店联系人：
                                                                                    </td>
                                                                                    <td>
                                                                                        <div id="SPANHotelEXLinkMan" runat="server">
                                                                                            <asp:Label ID="HotelEXLinkMan_span" runat="server" Text=""></asp:Label></div>
                                                                                        <div id="TXTHotelEXLinkMan" runat="server" style="display: none; float: left">
                                                                                            <asp:TextBox ID="HotelEXLinkMan_txt" runat="server"></asp:TextBox></div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="border-bottom: 1px solid #8D8D8D; width: 45%;">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        电话：
                                                                                    </td>
                                                                                    <td>
                                                                                        <div id="SPANHotelEXLinkTel" runat="server">
                                                                                            <asp:Label ID="HotelEXLinkTel_span" runat="server" Text=""></asp:Label></div>
                                                                                        <div id="TXTHotelEXLinkTel" runat="server" style="display: none; float: left">
                                                                                            <asp:TextBox ID="HotelEXLinkTel_txt" runat="server"></asp:TextBox></div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="border-bottom: 1px solid #8D8D8D; width: 10%;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border: 1px solid #8D8D8D">
                                                                        <td colspan="2">
                                                                            <div id="SPANHotelEXLinkRemark" runat="server" style="text-align: left; vertical-align: inherit;">
                                                                                <div id="HotelEXLinkRemark_span" runat="server" style="text-align: left; vertical-align: inherit;
                                                                                    min-height: 68px; max-width: 98%; word-break: break-all; word-wrap: break-word;
                                                                                    margin-left: 15px;">
                                                                                </div>
                                                                            </div>
                                                                            <div id="TXTotelEXLinkRemark" runat="server" style="display: none">
                                                                                <textarea id="HotelEXLinkRemark_txt" runat="server" cols="115" style="min-height: 68px;
                                                                                    margin-left: 15px; word-break: break-all; word-wrap: break-word;"></textarea>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" id="btnClearLock" runat="server" class="btn primary" value="&nbsp;&nbsp;修改&nbsp;&nbsp;"
                                                                                style="margin-bottom: 3px;" onclick="ClearLock()" />
                                                                            <input type="button" id="btnEditRemark" runat="server" class="btn primary" value="&nbsp;&nbsp;保存&nbsp;&nbsp;"
                                                                                onclick="EditRemark()" style="margin-bottom: 3px; display: none;" />
                                                                            <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                                                                <ContentTemplate>
                                                                                    <input type="button" id="btnAlertLink" runat="server" class="btn" value="LM联系人" onserverclick="btnAlertLink_Click" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div style="margin: 5px 5px 5px 5px;">
                                                            <input type="button" id="Button7" runat="server" class="btn primary" onclick="return MarkFullRoom('ExecuteRoom')"
                                                                value="批量执行" />&nbsp;&nbsp;&nbsp;
                                                            <input type="button" id="Button4" runat="server" class="btn primary" style="color: #FF6666"
                                                                onclick="return MarkFullRoom('CloseRoom')" value="批量关房" />&nbsp;&nbsp;&nbsp;
                                                            <input type="button" id="Button6" runat="server" class="btn primary" onclick="return MarkFullRoom('OpenRoom')"
                                                                value="批量开房" />&nbsp;&nbsp;&nbsp;
                                                            <input type="button" id="btnMarkFullRoom" runat="server" class="btn primary" onclick="return MarkFullRoom('FullRoom')"
                                                                value="标记满房" visible="false" />&nbsp;&nbsp;&nbsp;
                                                            <input type="button" id="lastHotel" runat="server" class="btn primary" style="margin-left: 200px;"
                                                                onclick="LastOrNextByHotel('-1')" value="上一个" />&nbsp;&nbsp;&nbsp;
                                                            <input type="button" id="nextHotel" runat="server" class="btn primary" onclick="LastOrNextByHotel('1')"
                                                                value="下一个" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divMain" style="height: 500px; width: 100%; margin-left: -10px; overflow-x: auto;
                                                    overflow-y: auto;" runat="server">
                                                    <%
                                                        //AssemblyDiv(rowLmbar2, rowLmbar, drRoomListLMBAR2, drRoomListLMBAR, dtTime);
                                                        if (IsErgodic)
                                                        {
                                                            string lmbar2W = "120px";
                                                            string lmbar2TitleWidth = (double.Parse(drRoomListLMBAR2 == null || drRoomListLMBAR2.Length.ToString() == "0" ? "1" : drRoomListLMBAR2.Length.ToString()) * (double.Parse("120"))).ToString("0.0") + "px";
                                                            string lmbarTitleWidth = (double.Parse(drRoomListLMBAR == null || drRoomListLMBAR.Length.ToString() == "0" ? "1" : drRoomListLMBAR.Length.ToString()) * (double.Parse("120"))).ToString("0.0") + "px";
                                                            string sumWidth = (240.0 + (double.Parse(drRoomListLMBAR2 == null || drRoomListLMBAR2.Length.ToString() == "0" ? "1" : drRoomListLMBAR2.Length.ToString()) * (double.Parse("120"))) + (double.Parse(drRoomListLMBAR == null || drRoomListLMBAR.Length.ToString() == "0" ? "1" : drRoomListLMBAR.Length.ToString()) * (double.Parse("120")))).ToString("0.0") + "px";
                                                    %>
                                                    <table width="<%= sumWidth %>" style="border-collapse: collapse; border: none;" cellpadding="0"
                                                        cellspacing="0">
                                                        <tr align="center">
                                                            <td rowspan="2" style="width: 120px; border: solid #8D8D8D 1px;">
                                                                批量操作
                                                            </td>
                                                            <td rowspan="2" style="width: 120px; border: solid #8D8D8D 1px;">
                                                                日期/房型
                                                            </td>
                                                            <td style="width: <%= lmbar2TitleWidth %>; border: solid #8D8D8D 1px;">
                                                                LMBAR2
                                                            </td>
                                                            <td style="width: <%= lmbarTitleWidth %>; border: solid #8D8D8D 1px;">
                                                                LMBAR
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <%
                                                            //循环LMBAR2 房型CODE
                                                            if (drRoomListLMBAR2 != null && drRoomListLMBAR2.Length > 0)
                                                            {
                                                            %>
                                                            <td style="width: <%= lmbar2TitleWidth %>; border: solid #8D8D8D 1px;">
                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0">
                                                                    <tr align="center">
                                                                        <%
                                                                for (int i = 0; i < drRoomListLMBAR2.Length; i++)
                                                                {
                                                                    if (drRoomListLMBAR2.Length - 1 == i)
                                                                    {
                                                                        %><td style="width: <%= lmbar2W %>;">
                                                                            <%
                                                                    }
                                                                    else
                                                                    {
                                                                            %><td style="border-right: solid #8D8D8D 1px; width: <%= lmbar2W %>;">
                                                                                <%
                                                                    }
                                                                                %>
                                                                                <span>
                                                                                    <%=drRoomListLMBAR2[i]["ROOMNM"].ToString()%></span> </br> <span>
                                                                                        <%=drRoomListLMBAR2[i]["ROOMCODE"].ToString()%><span>
                                                                            </td>
                                                                            <%
                                                                }
                                                                            %>
                                                                            <%
                                                            }
                                                            else
                                                            {
                                                                            %>
                                                                            <td style="width: <%= lmbar2TitleWidth %>; border: solid #8D8D8D 1px;">
                                                                                <table width="100%" style="border-collapse: collapse; border: none;" cellpadding="0"
                                                                                    cellspacing="0">
                                                                                    <tr align="center">
                                                                                        <td style="border-right: solid #8D8D8D 1px; width: <%= lmbar2W %>;">
                                                                                        </td>
                                                                                        <%
                                                            }
                                                                                        %>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <% //循环LMBAR 房型CODE
                                                            if (drRoomListLMBAR != null && drRoomListLMBAR.Length > 0)
                                                            {
                                                                            %>
                                                                            <td style="width: <%= lmbarTitleWidth %>; border: solid #8D8D8D 1px;">
                                                                                <table width="100%" height="100%" style="border-collapse: collapse; border: none;"
                                                                                    cellpadding="0" cellspacing="0">
                                                                                    <tr align="center">
                                                                                        <%
                                                                for (int i = 0; i < drRoomListLMBAR.Length; i++)
                                                                {
                                                                    if (drRoomListLMBAR.Length - 1 == i)
                                                                    {
                                                                                        %><td style="width: <%= lmbar2W %>; padding-top: 5px;">
                                                                                            <%
                                                                    }
                                                                    else
                                                                    {
                                                                                            %><td style="border-right: solid #8D8D8D 1px; width: <%= lmbar2W %>; padding-top: 5px;">
                                                                                                <%
                                                                    }
                                                                                                %><span><%= drRoomListLMBAR[i]["ROOMNM"].ToString()%></span></br> <span>
                                                                                                    <%=drRoomListLMBAR[i]["ROOMCODE"].ToString()%><span>
                                                                                            </td>
                                                                                            <%
                                                                }
                                                                                            %>
                                                                                            <%
                                                            }
                                                            else
                                                            { 
                                                                                            %>
                                                                                            <td style="width: <%= lmbarTitleWidth %>; border: solid #8D8D8D 1px; border-collapse: collapse;">
                                                                                                <table width="100%" style="border-collapse: collapse; border: none;" cellpadding="0"
                                                                                                    cellspacing="0">
                                                                                                    <tr align="center">
                                                                                                        <td style="border-right: solid #8D8D8D 1px; width: <%= lmbar2W %>;">
                                                                                                        </td>
                                                                                                        <%
                                                            }
                                                                                                        %>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                    </tr>
                                                                                    <%
                                                            //循环计划信息
                                                            bool IsDayOfWeek = false;
                                                            for (int i = 0; i < dtTime.Rows.Count; i++)
                                                            {
                                                                                    %>
                                                                                    <tr align="center">
                                                                                        <% //日期
                                                                if (DateTime.Parse(dtTime.Rows[i]["time"].ToString()).DayOfWeek.ToString() == "Saturday" || DateTime.Parse(dtTime.Rows[i]["time"].ToString()).DayOfWeek.ToString() == "Sunday")
                                                                {
                                                                    IsDayOfWeek = true;
                                                                                                
                                                                                        %>
                                                                                        <td style="width: 80px; border: solid #8D8D8D 1px; background-color: #CDEBFF; height: 40px;">
                                                                                            <input type="checkbox" id="chkMarkFullRoom<%=i.ToString() %>" name="chkMarkFullRoom"
                                                                                                value="<%= dtTime.Rows[i]["time"].ToString()  %>" />
                                                                                        </td>
                                                                                        <td style="width: 80px; border: solid #8D8D8D 1px; background-color: #CDEBFF; height: 40px;"
                                                                                            onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                                                            <%= dtTime.Rows[i]["time"].ToString()%>
                                                                                        </td>
                                                                                        <%
                                                                }
                                                                else
                                                                {
                                                                    IsDayOfWeek = false;
                                                                                        %>
                                                                                        <td style="width: 80px; border: solid #8D8D8D 1px; height: 40px;">
                                                                                            <input type="checkbox" id="chkMarkFullRoom<%= i %>" name="chkMarkFullRoom" value="<%= dtTime.Rows[i]["time"].ToString() %>" />
                                                                                        </td>
                                                                                        <td style="width: 80px; border: solid #8D8D8D 1px; height: 40px;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                            onmouseout="javacript:this.style.backgroundColor='white'">
                                                                                            <%= dtTime.Rows[i]["time"].ToString()%>
                                                                                        </td>
                                                                                        <%
                                                                }
                                                                                        %>
                                                                                        <td width="<%= lmbar2TitleWidth %>">
                                                                                            <table width="<%= lmbar2TitleWidth %>" style="border-collapse: collapse; border: none;"
                                                                                                cellpadding="0" cellspacing="0">
                                                                                                <tr align="center">
                                                                                                    <%
                                                                //循环LMBAR2   酒店计划中的房型数量和价格
                                                                if (rowLmbar2 != null && rowLmbar2.Length > 0)
                                                                {
                                                                    bool flag = false;
                                                                    if (drRoomListLMBAR2.Length > 0)
                                                                    {
                                                                        for (int j = 0; j < drRoomListLMBAR2.Length; j++)
                                                                        {
                                                                            flag = false;
                                                                            for (int k = 0; k < rowLmbar2.Length; k++)
                                                                            {
                                                                                if (drRoomListLMBAR2[j]["ROOMCODE"].ToString() == rowLmbar2[k]["ROOMTYPECODE"].ToString() && DateTime.Parse(rowLmbar2[k]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(dtTime.Rows[i]["time"].ToString()))
                                                                                {
                                                                                    flag = true;
                                                                                    if (rowLmbar2[k]["ROOMNUM"].ToString() == "0")
                                                                                    {
                                                                                        if (rowLmbar2[k]["STATUS"].ToString() == "false")
                                                                                        {
                                                                                                    %>
                                                                                                    <td style="background-color: #999999; border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px;
                                                                                                        border-left-width: 0px; border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                                        onmouseout="javacript:this.style.backgroundColor='#999999'">
                                                                                                        <%
                                                                                        }
                                                                                        else if (rowLmbar2[k]["ISROOMFUL"].ToString() == "1")
                                                                                        {
                                                                                                        %>
                                                                                                        <td style="background-color: #E96928; border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px;
                                                                                                            border-left-width: 0px; border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                                            onmouseout="javacript:this.style.backgroundColor='#E96928'">
                                                                                                            <%
                                                                                        }
                                                                                        else
                                                                                        { 
                                                                                                            %>
                                                                                                            <td style="background-color: #E6B9B6; border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px;
                                                                                                                border-left-width: 0px; border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                                                onmouseout="javacript:this.style.backgroundColor='#E6B9B6'">
                                                                                                                <%
                                                                                        }
                                                                                                                %>
                                                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;"
                                                                                                                    onclick="showDiv('<%= this.HidPcode.Value %>','LMBAR2','<%=drRoomListLMBAR2[j]["ROOMNM"].ToString() %>','<%= drRoomListLMBAR2[j]["ROOMCODE"].ToString() %>','<%= rowLmbar2[k]["TWOPRICE"].ToString() %>','<%= rowLmbar2[k]["ROOMNUM"].ToString() %>','<%= rowLmbar2[k]["STATUS"].ToString() %>','<%= rowLmbar2[k]["ISRESERVE"].ToString()  %>','<%= rowLmbar2[k]["EFFECTDATESTRING"].ToString()  %>') ">
                                                                                                                    <tr align="center">
                                                                                                                        <td>
                                                                                                                            <%= rowLmbar2[k]["ROOMNUM"].ToString()%>
                                                                                                                            <%
                                                                                        if (rowLmbar2[k]["ISRESERVE"].ToString() == "0")
                                                                                        {
                                                                                                                            %><span style="color: Red">*</span>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <%
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                                                    %>
                                                                                                            </td>
                                                                                                </tr>
                                                                                                <%
                                                                                        }
                                                                                                %>
                                                                                                <tr align="center">
                                                                                                    <td>
                                                                                                        ￥<%= rowLmbar2[k]["TWOPRICE"].ToString()%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <%
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (rowLmbar2[k]["STATUS"].ToString() == "false")
                                                                                        { 
                                                                                        %>
                                                                                        <td style="background-color: #999999; border-right: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                            border-top-width: 0px; border-bottom: solid #8D8D8D 1px; width: <%= lmbar2W %>;"
                                                                                            onmousemove="javacript:this.style.backgroundColor='#CDEBFF'" onmouseout="javacript:this.style.backgroundColor='#999999'">
                                                                                            <%
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (!IsDayOfWeek)
                                                                                            {
                                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                                border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                                onmouseout="javacript:this.style.backgroundColor='white'">
                                                                                                <%
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                                    border-top-width: 0px; width: <%= lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                                    onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                                                                    <%
                                                                                            }
                                                                                        }
                                                                                                    %>
                                                                                                    <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;"
                                                                                                        onclick="showDiv('<%= this.HidPcode.Value %>','LMBAR2','<%=drRoomListLMBAR2[j]["ROOMNM"].ToString() %>','<%= drRoomListLMBAR2[j]["ROOMCODE"].ToString() %>','<%= rowLmbar2[k]["TWOPRICE"].ToString() %>','<%= rowLmbar2[k]["ROOMNUM"].ToString() %>','<%= rowLmbar2[k]["STATUS"].ToString() %>','<%= rowLmbar2[k]["ISRESERVE"].ToString()  %>','<%= rowLmbar2[k]["EFFECTDATESTRING"].ToString()  %>')">
                                                                                                        <tr align="center">
                                                                                                            <td>
                                                                                                                <%= rowLmbar2[k]["ROOMNUM"].ToString()%>
                                                                                                                <%
                                                                                        if (rowLmbar2[k]["ISRESERVE"].ToString() == "0")
                                                                                        {
                                                                                                                %><span style="color: Red">*</span>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <%
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                                        %>
                                                                                                </td>
                                                                                    </tr>
                                                                                    <%
                                                                                        }
                                                                                    %>
                                                                                    <tr align="center">
                                                                                        <td>
                                                                                            ￥<%= rowLmbar2[k]["TWOPRICE"].ToString()%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                                    }
                                                                                    break;
                                                                                }
                                                                            }
                                                                            if (!flag)
                                                                            {
                                                                                #region
                                                                                if (!IsDayOfWeek)
                                                                                {
                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='white'">
                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                                }
                                                                                else
                                                                                {
                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                border-top-width: 0px; width: <%= lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                                }
                                                                                #endregion
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        #region
                                                                        if (!IsDayOfWeek)
                                                                        {
                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='white'">
                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                        }
                                                                        else
                                                                        {
                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                border-top-width: 0px; width: <%= lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                        }
                                                                        #endregion
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    #region
                                                                    if (!IsDayOfWeek)
                                                                    {
                                                                        if (drRoomListLMBAR2.Length > 0)
                                                                        {
                                                                            for (int j = 0; j < drRoomListLMBAR2.Length; j++)
                                                                            {
                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='white'">
                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='white'">
                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (drRoomListLMBAR2.Length > 0)
                                                                        {
                                                                            for (int j = 0; j < drRoomListLMBAR2.Length; j++)
                                                                            {
                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                border-top-width: 0px; width: <%= lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                                border-top-width: 0px; width: <%= lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                                                <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td style="border: solid #8D8D8D 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <%
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                                            %>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="<%= lmbarTitleWidth %>">
                                                                <table width="<%= lmbarTitleWidth %>" style="border-collapse: collapse; border: none;"
                                                                    cellpadding="0" cellspacing="0">
                                                                    <tr align="center">
                                                                        <%//循环LMBAR   酒店计划中的房型数量和价格
                                                                if (rowLmbar != null && rowLmbar.Length > 0)
                                                                {
                                                                    bool flag = false;
                                                                    if (drRoomListLMBAR.Length > 0)
                                                                    {
                                                                        for (int j = 0; j < drRoomListLMBAR.Length; j++)
                                                                        {
                                                                            flag = false;
                                                                            for (int k = 0; k < rowLmbar.Length; k++)
                                                                            {
                                                                                if (drRoomListLMBAR[j]["ROOMCODE"].ToString() == rowLmbar[k]["ROOMTYPECODE"].ToString() && DateTime.Parse(rowLmbar[k]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(dtTime.Rows[i]["time"].ToString()))
                                                                                {
                                                                                    flag = true;
                                                                                    if (rowLmbar[k]["ROOMNUM"].ToString() == "0")
                                                                                    {
                                                                                        if (rowLmbar[k]["STATUS"].ToString() == "false")
                                                                                        {
                                                                        %><td style="background-color: #999999; border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px;
                                                                            border-left-width: 0px; border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                            onmouseout="javacript:this.style.backgroundColor='#999999'">
                                                                            <%
                                                                                        }
                                                                                        else if (rowLmbar[k]["ISROOMFUL"].ToString() == "1")
                                                                                        {
                                                                            %><td style="background-color: #E96928; border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px;
                                                                                border-left-width: 0px; border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                onmouseout="javacript:this.style.backgroundColor='#E96928'">
                                                                                <%
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                %><td style="background-color: #E6B9B6; border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px;
                                                                                    border-left-width: 0px; border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                                    onmouseout="javacript:this.style.backgroundColor='#E6B9B6'">
                                                                                    <%
                                                                                        }
                                                                                    %><table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;"
                                                                                        onclick="showDiv('<%= this.HidPcode.Value %>','LMBAR','<%=drRoomListLMBAR[j]["ROOMNM"].ToString() %>','<%= drRoomListLMBAR[j]["ROOMCODE"].ToString() %>','<%= rowLmbar[k]["TWOPRICE"].ToString() %>','<%= rowLmbar[k]["ROOMNUM"].ToString() %>','<%= rowLmbar[k]["STATUS"].ToString() %>','<%= rowLmbar[k]["ISRESERVE"].ToString()  %>','<%= rowLmbar[k]["EFFECTDATESTRING"].ToString()  %>') ">
                                                                                        <%
                                                                                        if (rowLmbar[k]["ISRESERVE"].ToString() == "0")
                                                                                        {
                                                                                        %><span style="color: Red">*</span>
                                                                                </td>
                                                                    </tr>
                                                                    <%
                                                                                        }
                                                                                        else
                                                                                        {
                                                                    %>
                                                            </td>
                                                        </tr>
                                                        <%
                                                                                        }
                                                        %><tr align="center">
                                                            <td>
                                                                ￥<%=rowLmbar[k]["TWOPRICE"].ToString()%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    </td>
                                                    <%
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (rowLmbar[k]["STATUS"].ToString() == "false")
                                                                                        {
                                                    %><td style="background-color: #999999; border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px;
                                                        border-left-width: 0px; border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                        onmouseout="javacript:this.style.backgroundColor='#999999'">
                                                        <%
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (!IsDayOfWeek)
                                                                                            {
                                                        %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%= lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='white'">
                                                            <%
                                                                                            }
                                                                                            else
                                                                                            {
                                                            %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                                border-top-width: 0px; width: <%= lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                                onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                                <%
                                                                                            }
                                                                                        }
                                                                %><table width="100%" cellpadding="0" cellspacing="0" style="border: none;" height="40px;"
                                                                    onclick="showDiv('<%= this.HidPcode.Value %>','LMBAR','<%=drRoomListLMBAR[j]["ROOMNM"].ToString() %>','<%= drRoomListLMBAR[j]["ROOMCODE"].ToString() %>','<%= rowLmbar[k]["TWOPRICE"].ToString() %>','<%= rowLmbar[k]["ROOMNUM"].ToString() %>','<%= rowLmbar[k]["STATUS"].ToString() %>','<%= rowLmbar[k]["ISRESERVE"].ToString()  %>','<%= rowLmbar[k]["EFFECTDATESTRING"].ToString()  %>') ">
                                                                    <tr align="center">
                                                                        <td>
                                                                            <%=rowLmbar[k]["ROOMNUM"].ToString()%>
                                                                            <%if (rowLmbar[k]["ISRESERVE"].ToString() == "0")
                                                                              {
                                                                            %><span style="color: Red">*</span>
                                                                        </td>
                                                                    </tr>
                                                                    <%
                                                                              }
                                                                              else
                                                                              {
                                                                    %>
                                                            </td>
                                                            </tr><%
                                                                              }
                                                            %><tr align="center">
                                                                <td>
                                                                    ￥<%=rowLmbar[k]["TWOPRICE"].ToString()%>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                                    }
                                                                                }
                                                                            }
                                                                            if (!flag)
                                                                            {
                                                                                if (!IsDayOfWeek)
                                                                                {
                                                        %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%=lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='white'">
                                                            <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                                }
                                                                                else
                                                                                {
                                                        %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%=lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                            <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        #region
                                                                        if (!IsDayOfWeek)
                                                                        {
                                                        %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%=lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='white'">
                                                            <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                        }
                                                                        else
                                                                        {
                                                        %>
                                                        <td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%=lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                            <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                        }
                                                                        #endregion
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (!IsDayOfWeek)
                                                                    {
                                                                        if (drRoomListLMBAR.Length > 0)
                                                                        {
                                                                            for (int j = 0; j < drRoomListLMBAR.Length; j++)
                                                                            {
                                                        %>
                                                        <td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%=lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='white'">
                                                            <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                        %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%=lmbar2W %>;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='white'">
                                                            <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (drRoomListLMBAR.Length > 0)
                                                                        {
                                                                            for (int j = 0; j < drRoomListLMBAR.Length; j++)
                                                                            {
                                                        %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%=lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                            <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                        %><td style="border-right: solid #8D8D8D 1px; border-bottom: solid #8D8D8D 1px; border-left-width: 0px;
                                                            border-top-width: 0px; width: <%=lmbar2W %>; background-color: #CDEBFF;" onmousemove="javacript:this.style.backgroundColor='#CDEBFF'"
                                                            onmouseout="javacript:this.style.backgroundColor='#CDEBFF'">
                                                            <table width="100%" style="border: none;" cellpadding="0" cellspacing="0" height="40px;">
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td style="border: solid #8D8D8D 0px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%
                                                                        }
                                                                    }
                                                                }
                                                        %>
                                                        </tr></table>
                                                    </td>
                                                    <%
                                                            }
                                                        }
                                                    %>
                                                    </table>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="background" class="pcbackground" style="display: none;">
    </div>
    <div id="progressBar" class="pcprogressBar" style="display: none;">
        数据加载中，请稍等...</div>
    <div id="bgDiv2" class="bgDiv2">
    </div>
    <div id="popupDiv2" class="popupDiv2">
        <div class="frame01">
            <ul>
                <li class="title">酒店计划</li>
            </ul>
            <ul>
                <li style="padding-left: 0px;">
                    <table style="width: 100%;" class="GView_BodyCSS">
                        <tr style="height: 35px; vertical-align: middle;">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                酒店:
                            </td>
                            <td style="width: 190px; border: 1px solid #DCDCDC;">
                                <asp:Label ID="lblDivHotelName" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 390px; border: 1px solid #DCDCDC;" colspan="2">
                                <asp:Label ID="DivlblLinkDetails" CssClass="lblLinkDetails" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 210px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                房型:
                            </td>
                            <td style="border: 1px solid #DCDCDC;">
                                <asp:Label ID="lblDivRoomType" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="border: 1px solid #DCDCDC;" colspan="2">
                                <asp:Label ID="DivlblContactDetails" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 210px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                价格:
                            </td>
                            <td style="border: 1px solid #DCDCDC;" colspan="3">
                                <asp:Label ID="lblDivPrice" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 210px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                状态:
                            </td>
                            <td style="border: 1px solid #DCDCDC;" colspan="3">
                                <input type="radio" runat="server" id="dropDivStatusOpen" name="dropDivStatus" value="开启"
                                    onclick="showRoomDiv()" />开启&nbsp;&nbsp;&nbsp;<input type="radio" runat="server"
                                        id="dropDivStatusClose" name="dropDivStatus" value="关闭" onclick="closeRoomDiv()" />关闭
                            </td>
                        </tr>
                        <tr style="width: 220px; height: 35px; vertical-align: middle;" align="left">
                            <div id="divRoomCount" runat="server">
                                <td class="style1" style="border: 1px solid #DCDCDC;">
                                    房量:
                                </td>
                                <td style="white-space: nowrap; border: 1px solid #DCDCDC;" colspan="3">
                                    <div id="divckReserve" runat="server">
                                        <asp:TextBox ID="txtDivRoomCount" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="ckDivReserve" runat="server" Text="保留房" />
                                    </div>
                                </td>
                            </div>
                        </tr>
                        <tr style="width: 200px; height: 35px; vertical-align: middle;" align="left">
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                批量
                            </td>
                            <td colspan="3" style="border: 1px solid #DCDCDC;">
                                <div id="IsBatchUpdateDiv" runat="server">
                                    <input id="divPlanStartDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_divPlanEndDate\')||\'2020-10-01\'}'})"
                                        runat="server" />&nbsp;至&nbsp;
                                    <input id="divPlanEndDate" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_divPlanStartDate\')}',maxDate:'2020-10-01'})"
                                        runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="border: 1px solid #DCDCDC;">
                                备注:
                            </td>
                            <td style="border: 1px solid #DCDCDC;" colspan="3">
                                <textarea id="txtRemark" runat="server" cols="30" style="height: 48px;"></textarea>
                            </td>
                        </tr>
                        <tr style="width: 210px; border: 1px solid #DCDCDC;">
                            <td colspan="4" align="center" style="border: 1px solid #DCDCDC;">
                                <br />
                                <asp:Button ID="btnDivRenewPlan" runat="server" Text="更新计划" CssClass="btn primary"
                                    OnClientClick="return GetCheckBoxListValue()" OnClick="btnDivRenewPlan_Click" />
                                <input type="button" value="取消" class="btn" onclick="invokeClose2()" />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div id="LmbarRemarkHistory" runat="server" style="width: 100%; height: 223px; overflow-y: auto;">
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
        </div>
    </div>
    <div id="DivAlertRemarkMain" style="display: none; position: absolute; top: 0px;
        left: 0px; right: 0px; background-color: #000000; filter: alpha(Opacity=80);
        -moz-opacity: 0.5; opacity: 0.5; z-index: 100; background-color: #000000; opacity: 0.6;">
    </div>
    <div id="DivAlertRemark" style="width: 360px; height: 120px; top: 55%; left: 45%;
        position: absolute; padding: 1px; vertical-align: middle; text-align: center;
        border: solid 2px #ff8300; z-index: 100; display: none; background-color: White;">
        <ul>
            <li class="title">操作原因</li>
        </ul>
        <ul>
            <li style="padding-left: 0px;">
                <table>
                    <tr>
                        <td class="style1" style="border-bottom: 1px solid #DCDCDC;">
                            备注:
                        </td>
                        <td style="border-bottom: 1px solid #DCDCDC;" colspan="3">
                            <textarea id="divOperateRoomRemark" runat="server" cols="30" style="height: 48px;"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 210px; border-bottom: 1px solid #DCDCDC;">
                        <td colspan="4" align="center">
                            <asp:Button ID="Button5" runat="server" Text="确定" CssClass="btn primary" OnClientClick="BtnLoadStyle();"
                                OnClick="btnCloseOrFullRoom_Click" />
                            <input type="button" value="取消" class="btn" onclick="invokeCloseRemark()" />
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <input type="button" id="Button1" runat="server" onserverclick="btnSingleHotel_Click"
                style="display: none;" />
            <input type="button" id="Button3" runat="server" onserverclick="Button3_Click" style="display: none;" />
            <input type="button" id="Button8" runat="server" onserverclick="btnEditRemark_Click"
                style="display: none;" />
            <asp:HiddenField ID="HidPcode" runat="server" />
            <asp:HiddenField ID="HidPid" runat="server" />
            <asp:HiddenField ID="HidCityID" runat="server" />
            <asp:HiddenField ID="HidSelIndex" runat="server" />
            <asp:HiddenField ID="dvscrollX" runat="server" />
            <asp:HiddenField ID="dvscrollY" runat="server" />
            <asp:HiddenField ID="HiddenRoomType" runat="server" />
            <asp:HiddenField ID="HiddenPrice" runat="server" />
            <asp:HiddenField ID="HiddenPriceCode" runat="server" />
            <asp:HiddenField ID="HiddenEffectDate" runat="server" />
            <asp:HiddenField ID="HiddenRoomCode" runat="server" />
            <asp:HiddenField ID="HiddenRoomNum" runat="server" />
            <asp:HiddenField ID="HiddenPlanStatus" runat="server" />
            <asp:HiddenField ID="HiddenIsReserve" runat="server" />
            <asp:HiddenField ID="HidLastOrNextByHotel" runat="server" />
            <asp:HiddenField ID="hidSelectHotel" runat="server" />
            <asp:HiddenField ID="hidSelectCity" runat="server" />
            <asp:HiddenField ID="hidSelectBussiness" runat="server" />
            <asp:HiddenField ID="hidSelectSalesID" runat="server" />
            <asp:HiddenField ID="HidDdlSelectValue" runat="server" />
            <asp:HiddenField ID="HidLinkDetails" runat="server" />
            <asp:HiddenField ID="HidContactDetails" runat="server" />
            <asp:HiddenField ID="HidAlertContactDetails" runat="server" />
            <asp:HiddenField ID="HidMarkFullRoom" runat="server" />
            <asp:HiddenField ID="HidScrollValue" runat="server" />
            <asp:HiddenField ID="HidCloseOrFullByRoom" runat="server" />
            <asp:HiddenField ID="EXHotelInfo" runat="server" />
            <asp:HiddenField ID="EXLinkFax" runat="server" />
            <asp:HiddenField ID="EXExTime" runat="server" />
            <asp:HiddenField ID="EXExMode" runat="server" />
            <asp:HiddenField ID="HidLmbarRoomCode" runat="server" />
            <asp:HiddenField ID="HidLmbar2RoomCode" runat="server" />
            <asp:HiddenField ID="HidBrowser" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidSelIndexOld" runat="server" />
            <asp:HiddenField ID="HidIsAsync" runat="server" />
            <asp:HiddenField ID="HidJudgeLast" runat="server" />
            <%--判断上一个是否需要查询--%>
            <asp:HiddenField ID="HidJudgeNext" runat="server" />
            <%--判断下一个是否需要查询--%>
            <asp:HiddenField ID="HidLastHotelSelectName" runat="server" />
            <%--上一个酒店ID --%>
            <asp:HiddenField ID="HidNextHotelSelectName" runat="server" />
            <%--下一个酒店ID --%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
