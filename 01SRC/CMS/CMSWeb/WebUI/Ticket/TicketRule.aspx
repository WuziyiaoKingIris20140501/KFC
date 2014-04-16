<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TicketRule.aspx.cs" Inherits="Ticket_TicketRule" Title="设置基本规则" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
#bgAlertDiv
{
    display: none;
    position: absolute;
    top: 0px;
    left: 0px;
    right:0px;
    background-color: #777;
    filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75)
    opacity: 0.6;
}
#ruleAlertDiv
{
width: 400px;
height:300px;
margin-top:50px;
margin-left:150px;
position:absolute;
padding: 1px;
vertical-align: middle;
text-align:center;
border: solid 2px #ff8300;
z-index: 1001;
display: none;
background-color:White;
}
</style>

    <script language="javascript" type="text/javascript">
    var winMain = null;
    function OpenWin(url) {
        if (winMain != null) {
            try {
                winMain.close();

            } catch (e2) {

                winMain = null;
            }

        }
        winMain = window.open(url, null, "width=800,height=600");
    }

    function OpenTopWin(url) {
        window.showModalDialog(url, "", "dialogWidth:300px;dialogHeight:300px;scroll:no;status:no")
    } 

    function Clear() {
        document.getElementById("<%=txtHotelName.ClientID%>").value = "";
    }


    //点保存的时候判断是否都输入了
    function checkEmpty() {
        var RuleName = document.getElementById("<%=txtRuleName.ClientID%>").value;
        if (RuleName == "") {
            alert("<%=RuleNameCanNotEmpty %>");
            document.getElementById("<%=txtRuleName.ClientID%>").focus();
            return false;
        }
        if (RuleName != "" && RuleName.length>25) {
            alert("优惠券规则名称最多只能输入25个字符！");
            document.getElementById("<%=txtRuleName.ClientID%>").focus();
            return false;
        }

        var FromDate = document.getElementById("<%=fromDate.ClientID%>").value;
        if (FromDate == "") {
            alert("<%=StartDateCanNotEmpty %>");
            document.getElementById("<%=fromDate.ClientID%>").focus();
            return false;
        }

        var EndDate = document.getElementById("<%=endDate.ClientID%>").value;
        if (EndDate == "") {
            alert("<%=EndDateCanNotEmpty%>");
            document.getElementById("<%=endDate.ClientID%>").focus();
            return false;
        }

        //判断开始日期和结束日期大小
        if (FromDate > EndDate) 
        {
            alert("<%=EndDateGreaterThanStartDate%>");
            document.getElementById("<%=fromDate.ClientID%>").focus();
            return false;
        }


        var OrdAmt = document.getElementById("<%=txtOrdAmt.ClientID%>").value;
        var inputRules = /^\d+(\.\d+)?$/; //只能是数字且不能为负数
        if (inputRules.test(OrdAmt) == false) {
            alert("<%=PromptOrderAmtMustNum %>");
            document.getElementById("<%=txtOrdAmt.ClientID%>").focus();
            return false;
        }

        var ruleDesc = document.getElementById("<%=txtRuleDesc.ClientID%>").value;
        if (ruleDesc != "" && ruleDesc.length>500) {
            alert("优惠券描述最多只能输入500个字符！");
            document.getElementById("<%=txtRuleDesc.ClientID%>").focus();
            return false;
        }

        if (window.confirm("规则一旦保存后，不能修改，确定要保存吗？") != true) {
            return false;
        }       

        return true;
    }

</script>
<!-----------for popup--------------->
<script language="javascript"  type="text/javascript">
    function invokeOpen() 
    {
        document.getElementById("ruleAlertDiv").style.display = "block";
        //背景
        var bgObj = document.getElementById("bgAlertDiv");
        bgObj.style.display = "block";
        bgObj.style.width = document.body.offsetWidth + "px";
        bgObj.style.height = screen.height + "px";       
    }

    function invokeClose() 
    {
        document.getElementById("ruleAlertDiv").style.display = "none";
        document.getElementById("bgAlertDiv").style.display = "none";      
    }
  
</script> 
    <div id="bgAlertDiv">        
    </div>
    <div id="ruleAlertDiv">
    <div class="frame01">
      <ul>
        <li class="title">新建规则操作返回结果</li>
        <li>
        <table style="text-align:center; width:100%">
            <tr><td colspan=2 style="height:41px; font-weight:bold;">新增规则成功!</td></tr>
            <tr  style="height:35px;">
                <td><input id="btnContinue" type="button" class="btn primary" value="继续添加基本规则" onclick="invokeClose();"/></td>              
                <td><input id="btnPackage" type="button" class="btn primary" value="新建优惠券礼包" onclick="javascript:location.href='TicketPackage.aspx'"/></td>
            </tr>
        </table>
         </li>
      </ul>
    </div>
    </div> 
    <!---------------------------------->
<div id="right">
    <div class="frame01">
      <ul>
        <li class="title">新增规则</li>
        <li>
        <table align="center" border="0" width="100%" class="Table_BodyCSS">
        <%--<tr class="RowTitle"><td colspan=4><asp:Literal Text="<%$Resources: AddRule%>" ID="lbRuleTitle" runat="server"></asp:Literal> </td></tr>--%>
       <tr>
        <td class="tdcell"><asp:Label ID="lblRuleName" runat="server" Text="<%$Resources:RuleName %>"></asp:Label></td>
       <td colspan="3" class="tdcell"><asp:TextBox ID="txtRuleName" runat="server" Width="96%" MaxLength="25"></asp:TextBox><font color="red">*</font></td></tr>
       <tr><td class="tdcell" style="width:15%;">
           <asp:Label ID="lblStartDate" runat="server" Text="<%$Resources:StartDate %>"></asp:Label></td>
           <td class="tdcell" style="width:35%;">             
                <%--<input id="fromDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_fromDate\')||\'2020-10-01\'}'})" runat="server"/><font color="red">*</font>--%>
                <input id="fromDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/><font color="red">*</font>
           </td>
           <td class="tdcell" style="width:15%;">
               <asp:Label ID="lblEndDate" runat="server" Text="<%$Resources:EndDate %>"></asp:Label></td>
           <td class="tdcell" style="width:35%;">            
                <input id="endDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_fromDate\')}'})" runat="server"/><font color="red">*</font>            
           </td>
           </tr>
       <tr><td  class="tdcell" ><asp:Label ID="lblStartTime" runat="server" Text="<%$Resources:StartTime %>"></asp:Label></td><td class="tdcell"><asp:TextBox ID="txtStartTime" runat="server"  Width="95%" Enabled="false"></asp:TextBox></td>
       <td class="tdcell"><asp:Label ID="lblEndTime" runat="server" Text="<%$ Resources:EndTime%>"></asp:Label></td><td  class="tdcell"> <asp:TextBox ID="txtEndTime" runat="server"  Width="95%" Enabled="false"></asp:TextBox></td></tr>
       <tr>
            <td class="tdcell"><asp:Label ID="lblOrderAmount" runat="server" Text="<%$Resources:OrderNeedAmout %>"></asp:Label></td>
            <td class="tdcell"><asp:TextBox ID="txtOrdAmt" runat="server"></asp:TextBox><font color="red">*</font></td>
            <td class="tdcell"><asp:Label ID="lblRelatedCity" runat="server" Text="<%$Resources:RelatedCity %>"></asp:Label></td>
            <td class="tdcell"><asp:DropDownList ID="cityid" runat="server" Enabled="false"></asp:DropDownList></td>           
       </tr>
        <tr>
           <td  class="tdcell" >
            <asp:Label ID="Label1" runat="server" Text="销售渠道："></asp:Label></td>
            <td align="left"  class="tdcell" >
                <asp:ListBox ID="LBSaleChannel" runat="server" Width="96%" Height="80px"
                    SelectionMode="Multiple">
                    <asp:ListItem Value="">不限制</asp:ListItem>
                    <asp:ListItem Value="HOTELVP">HOTELVP</asp:ListItem>
                </asp:ListBox>
            </td>
            <td class="tdcell"><asp:Label ID="lblPriceCode" runat="server" Text="<%$Resources:PriceCode %>"></asp:Label></td>
            <td align="left" class="tdcell">
                 <asp:ListBox ID="LBPriceCode" runat="server" Width="96%" Height="80px"
                    SelectionMode="Multiple">
                    <asp:ListItem Value="">不限制</asp:ListItem>
                    <asp:ListItem Value="LMBAR">LMBAR</asp:ListItem>
                    <asp:ListItem Value="LMBAR2">LMBAR2</asp:ListItem>
                    <asp:ListItem Value="BAR">BAR</asp:ListItem>
                    <asp:ListItem Value="BARB">BARB</asp:ListItem>
                </asp:ListBox>
            </td>
        </tr>
        <tr><td  class="tdcell" >
            <asp:Label ID="lblUserPlatform" runat="server" Text="<%$Resources:UserPlatform%>"></asp:Label></td>
            <td align="left"  class="tdcell" >
                <asp:ListBox ID="LBUsePlatForm" runat="server" Width="96%" Height="80px"
                    SelectionMode="Multiple">
                    <asp:ListItem Value="">不限制</asp:ListItem>
                    <asp:ListItem Value="IOS">IOS</asp:ListItem>
                    <asp:ListItem>ANDROID</asp:ListItem>
                    <asp:ListItem>WAP</asp:ListItem>
                </asp:ListBox>
            </td>
            <td  class="tdcell" >
               <div style="display:none"><asp:Label ID="lblUseGroup" runat="server" Text="<%$Resources:UserGroup %>"></asp:Label>
               </div>
               </td>
            <td  class="tdcell" >
               <div style="display:none"> <asp:ListBox ID="LBUserGroup" runat="server" Width="96%" 
                    SelectionMode="Multiple">
                    <asp:ListItem>下单量大于300</asp:ListItem>                   
                </asp:ListBox>  
               </div>            
            </td>            
            </tr>
       
         <%--   <tr><td  class="tdcell" >--%>
                <div style="display:none"><asp:Label ID="lblRelatedHotel" runat="server" Text="<%$Resources:RelatedHotel %>"></asp:Label></div>
           <%--</td>
               <td colspan=3  class="tdcell" > --%>
                 <div style="display:none">               
                    <input readonly="readonly" runat="server" type="text" id="txtHotelName" name="txtHotelName" value="" style="vertical-align:middle; width:80%; height:50px"/>                   
                    <input type="hidden" id="txthotelid" name="txthotelid" runat="server" />                
                
                    <input type="button" id="btnselect" value="<%=SelectButtonLabel%>" name="btnselect" class="btn primary"  onclick="OpenWin('../../Common/sel_hotel.aspx?FormType=MultiSelHotel')" /> 
                    <input id="btnClear" type="button" value="<%=ClearButtonLabel %>" class="btn"  onclick="Clear()" />
                 </div>
          <%--          </td>
            </tr> --%>
      
        <tr><td  class="tdcell" >
            <asp:Label ID="lblRuleDesc" runat="server" Text="<%$Resources:RuleDesction %>"></asp:Label></td>
            <td colspan="3"  class="tdcell" ><asp:TextBox ID="txtRuleDesc" runat="server" TextMode="MultiLine"  Width="96%" Rows="8" MaxLength="500"></asp:TextBox></td>
        </tr>
        <tr><td colspan="4" align="center" class="tdcell" >
            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,NewText %>" onclick="btnAdd_Click" OnClientClick="return checkEmpty();" /><br />
                        <font color="red"><strong><asp:Literal Text="<%$Resources: Remark%>" ID="Literal1" runat="server" ></asp:Literal></strong></font>

        </td></tr>
    </table>        
 
        <br />
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">           
            <tr>
                <td align="center" colspan="5">
                    <asp:GridView ID="gridViewRegion"  runat="server" AutoGenerateColumns="False" 
                        BackColor="White" EmptyDataText="<%$ Resources:MyGlobal,NoDataText %>" DataKeyNames="ID" 
                        onrowdeleting="gridViewRegion_RowDeleting" 
                        onrowcommand="gridViewRegion_RowCommand" 
                        onrowdatabound="gridViewRegion_RowDataBound" AllowPaging="True" 
                        onpageindexchanging="gridViewRegion_PageIndexChanging" PageSize="16" 
                        CssClass="GView_BodyCSS">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                            <asp:BoundField DataField="TICKETRULECODE" HeaderText="<%$ Resources:Code%>" >
                                
                            <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TICKETRULENAME" HeaderText="规则名称" >                           
                            <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STARTDATE" HeaderText="开始日期" >
                                
                            <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ENDDATE" HeaderText="结束日期" >
                                
                            <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORDAMT" HeaderText="最低订单金额" >
                                
                            <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HOTELID" HeaderText="<%$ Resources:HotelID%>" 
                                Visible="False" >
                                
                            <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TICKETRULEDESC" HeaderText="优惠券描述" >                             
                            <ItemStyle Width="20%" />
                            </asp:BoundField>

                            <asp:HyperLinkField HeaderText="<%$ Resources:MyGlobal,DetailText %>" Text="<%$ Resources:MyGlobal,DetailText %>" DataNavigateUrlFields="TICKETRULECODE" 
                                DataNavigateUrlFormatString="DisplayTicketRule.aspx?ticketrulecode={0}" Target="_blank" 
                                NavigateUrl="~/WebUI/Ticket/DisplayTicketRule.aspx" >                              
                            <ItemStyle Width="5%" />
                           </asp:HyperLinkField>

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
        </li>
      </ul>
    </div>
</div>
</asp:Content>
