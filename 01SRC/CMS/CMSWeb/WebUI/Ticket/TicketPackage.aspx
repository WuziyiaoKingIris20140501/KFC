<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master" CodeFile="TicketPackage.aspx.cs"   Culture="zh-cn" UICulture="zh-cn" Title="优惠券礼包管理" Inherits="Ticket_TicketPackage" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
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
<script language="javascript"  type="text/javascript">
    function invokeOpen() {
        document.getElementById("ruleAlertDiv").style.display = "block";
        //背景
        var bgObj = document.getElementById("bgAlertDiv");
        bgObj.style.display = "block";
        bgObj.style.width = document.body.offsetWidth + "px";
        bgObj.style.height = screen.height + "px";
    }

    function invokeClose() {
        document.getElementById("ruleAlertDiv").style.display = "none";
        document.getElementById("bgAlertDiv").style.display = "none";
    }
  
</script> 
   <div id="bgAlertDiv">        
    </div>
    <div id="ruleAlertDiv">
    <div class="frame01">
      <ul>
        <li class="title">新建优惠券礼包返回结果</li>
        <li>
        <table style="text-align:center; width:100%">
            <tr><td colspan=2 style="height:41px; font-weight:bold;">新增优惠券礼包成功!</td></tr>
            <tr style="height:35px;">
                <td><input id="btnContinue" type="button" class="btn primary" value="继续添加" onclick="invokeClose();"/></td>              
                <td><input id="btnPackage" type="button" class="btn primary" value="关联使用规则" onclick="javascript:location.href='SetTicketUseRule.aspx'"/></td>
            </tr>
        </table>
        </li>
      </ul>
    </div>
    </div> 
    <!---------------------------------->

<div id="right">
    <input type="hidden" id="hiddenIndex" value="0" runat="server" />

     <%-- <fieldset title="addrule" >
    <legend><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: PackageSettingTitle%>" /></legend>--%>
     <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="<%$Resources: PackageSettingTitle%>" ID="lbRuleTitle" runat="server"></asp:Literal> </li>
        <li>
       <table align="center" border="0" width="100%" class="Table_BodyCSS">
        <%--<tr class="RowTitle"><td colspan=4><asp:Literal Text="<%$Resources: PackageSettingTitle%>" ID="lbRuleTitle" runat="server"></asp:Literal> </td></tr>    --%>
        <tr>
            <td class="tdcell" style="width:250px">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:CustomNumber %>"/>
            </td>
            <td colspan="3" class="tdcell">
                <asp:TextBox ID="txtCustomNumber" runat="server" Text="" MaxLength="10" 
                    ontextchanged="txtCustomNumber_TextChanged" AutoPostBack="true" ></asp:TextBox><asp:CheckBox ID="chkCustomNumber" Text="使用自定义优惠券号码" runat="server" AutoPostBack="true" OnCheckedChanged="ChkCustomValue_Click"/>&nbsp;&nbsp;<asp:Label ID="lbCustomNumMsg" runat="server" Text="" ForeColor="red"/>
            </td>
        </tr>
        <tr>
            <td class="tdcell"><asp:Label ID="lbl1" runat="server" Text="<%$ Resources:PackageNameLabel %>"/></td>
            <td colspan="3" class="tdcell"><asp:TextBox ID="txtPackageName" runat="server" Width="85%" Height="19px" MaxLength="64"></asp:TextBox><font color="red">*</font></td>
        </tr>
        <tr>
            <td class="tdcell"><asp:Label ID="lbl2" runat="server" Text="<%$ Resources:PackageAllAmount %>"/></td>
            <td  class="tdcell"><input id="txtAllAmount" name="txtAllAmount" type="text"  runat="server"  onblur="changeRemainValue()"  /><font color="red">*</font></td>
            <td class="tdcell">
                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:PackageType %>"/>
            </td>
            <td class="tdcell">
                <input type="radio" name="RbtPackageType" id="rbtnOperate" value="0" onclick="SerRbtNameValue('0')"/>运营
                <input type="radio" name="RbtPackageType" id="rbtMarket" value="1" onclick="SerRbtNameValue('1')"/>市场
                <input type="radio" name="RbtPackageType" id="rbtOther" value="2" checked="checked" onclick="SerRbtNameValue('2')"/>其他
                <font color="red">*</font>
                <input type="hidden" id="hidPackageType" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdcell"><asp:Label ID="lbl3" runat="server" Text="<%$ Resources:PackageStartDate %>"/></td>
            <td class="tdcell">
            <%--  <TW:DateTextBox ID="FromDate" runat="server" IsDisplayTime="False" Height="19px"></TW:DateTextBox>--%>
            <input id="FromDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'MainContent_EndDate\')||\'2020-10-01\'}'})" runat="server"/><font color="red">*</font>
            
            </td>
            <td class="tdcell">
                <asp:Label ID="lbl4" runat="server" Text="<%$ Resources:PackageEndDate %>"/></td>
                <td class="tdcell">
                    <%--<TW:DateTextBox ID="EndDate" runat="server" IsDisplayTime="False" ></TW:DateTextBox>--%>
                <input id="EndDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'MainContent_FromDate\')||\'2020-10-01\'}'})" runat="server"/><font color="red">*</font>
              
            </td>
        </tr>
        
        <tr><td class="tdcell"><asp:Label ID="lbl5" runat="server" Text="<%$ Resources:PackageInCludeTicket %>"/></td>
            <td  colspan=3 align="left" class="tdcell">
                <table>
                    <tr><td>
                            <asp:TextBox ID="txtNumber" Width="40" runat="server"></asp:TextBox>
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Leaf%>" />
                            <asp:TextBox ID="txtAmount" Width="40"  runat="server"></asp:TextBox>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: Yuan%>" />                             
                            <input type="button" value="<%=addLabel %>" class="btn primary" onclick="return checkTicketInput();" />
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: Remain%>" />
                            <font color="red"><asp:Label ID="lbRemainAmount"  runat="server" Text="0"></asp:Label></font>
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources: Yuan%>" /><font color="red">*</font>
                        </td>
                    </tr>
                     <tr >
                        <td style="width:100%;">
                            <table id="dt_ticket" cellpadding="0" cellspacing="0" width="99%"  >
                            <tr>
                                <td width="10px;" align="right">
                                </td>
                                 <td width="20px;">张
                                </td>  
                                   <td width="20px;">元
                                </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
      <tr>
            <td class="tdcell"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:UserConsumeCount %>"/></td>
            <td class="tdcell"><asp:TextBox ID="txtUserCount" runat="server" MaxLength="8"></asp:TextBox><font color="red">*</font></td>
             <td class="tdcell"><asp:Label ID="Label7" runat="server" Text="<%$ Resources:UserRepCount %>"/></td>
            <td class="tdcell"><asp:TextBox ID="txtUserRepCount" runat="server" MaxLength="8"></asp:TextBox><font color="red">*</font></td>
       </tr>
        <tr><td class="tdcell"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:UsePlatform %>"/></td>
            <td align="left" class="tdcell">
                <asp:ListBox ID="LBUsePlatForm" runat="server" Width="80%" Height="80px"
                    SelectionMode="Multiple">
                    <asp:ListItem Value="">无限制</asp:ListItem>
                    <asp:ListItem Value="IOS">IOS</asp:ListItem>
                    <asp:ListItem>ANDROID</asp:ListItem>
                    <asp:ListItem>WAP</asp:ListItem>
                </asp:ListBox>
            </td>
            <td class="tdcell">
             <div style="display:none"><asp:Label ID="Label4" runat="server" Text="<%$ Resources:UserGroup %>"/></div>
            </td>
            <td class="tdcell">
                 <div style="display:none">
                 <asp:ListBox ID="LBUserGroup" runat="server" Width="80%" 
                    SelectionMode="Multiple">
                    <asp:ListItem>下单数大于300</asp:ListItem>                  
                </asp:ListBox>
                </div>
            </td>
            </tr>
        <tr>
            <td class="tdcell"><asp:Label ID="Label5" runat="server" Text="<%$ Resources:UserChannel%>"/></td>
            <td colspan="3" align="left" class="tdcell">
                <asp:ListBox ID="LBSaleChanel" runat="server" Width="90%" Height="80px"
                    SelectionMode="Multiple">
                    <asp:ListItem>HOTELVP</asp:ListItem>                 
                </asp:ListBox>
            </td>
        </tr>
            
       <%-- <tr>
            <td class="tdcell">--%>
                <div style="display:none"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:Cities%>"/></div>
           <%-- </td>
            <td colspan="3" align="left" class="tdcell">--%>
                 <div style="display:none">
                    <input readonly="readonly" runat="server" type="text" id="txtCityID" name="txtCityID" value="" style="vertical-align:middle; width:80%"/> 
                    <input type="button"  id="btnSelect" value="<%=selectLabel %>" name="btnSelect" class="btn primary" onclick="OpenWin('../../Common/sel_city.aspx?FormType=MultiSelCity')" />
                    <input id="btnClear" type="button" value="<%=clearLabel %>" onclick="Clear()" class="btn"/>
                </div>
           <%-- </td>
        </tr>   --%> 
        <tr>
            <td colspan=4 align="center"><asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="<%$ Resources:MyGlobal,SaveText %>" onclick="btnSave_Click" OnClientClick="return checkEmpty();" />
                <br />
                  <font color="red"><strong><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources: Remark%>" /></strong></font>
             </td>
        </tr>
    </table>
   <%-- </fieldset>
     <fieldset title="rulelist"  >
     <legend><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:PackageList%>" /></legend>--%>
     <br />
         <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">           
            <%--<tr>
                <th align="center" colspan="5" style="height: 20px">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:TicketOfHadSetting %>"></asp:Label></th>
            </tr>--%>
            
            <tr>
                <td align="center">
                    <asp:GridView ID="gridViewRegion" runat="server" AutoGenerateColumns="False"                         
                         EmptyDataText="<%$Resources:MyGlobal,NoDataText %>" DataKeyNames="ID" AllowPaging="True" 
                        onpageindexchanging="gridViewRegion_PageIndexChanging" BackColor="White"  CssClass="GView_BodyCSS">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                            <asp:BoundField DataField="PACKAGECODE" HeaderText="<%$ Resources:CodeLabel%>" />
                            <asp:BoundField DataField="PACKAGENAME" HeaderText="<%$ Resources:NameLabel%>" />
                            <asp:BoundField DataField="AMOUNT" HeaderText="<%$ Resources:AllAmountLabel%>" />
                            <asp:BoundField DataField="STARTDATE" HeaderText="<%$ Resources:StartDateLabel%>" />
                            <asp:BoundField DataField="ENDDATE" HeaderText="<%$ Resources:EndDateLabel%>" />
                            <asp:CommandField ShowDeleteButton="True" Visible="False" />

                           <asp:HyperLinkField HeaderText="<%$ Resources:MyGlobal,UpdateText %>" Text="<%$ Resources:MyGlobal,UpdateText %>" DataNavigateUrlFields="ID" 
                                DataNavigateUrlFormatString="UpdateTicketPackage.aspx?ID={0}" Target="_blank" 
                                NavigateUrl="~/Ticket/UpdateTicketPackage.aspx" Visible="False" >
                           </asp:HyperLinkField>

                            <asp:HyperLinkField HeaderText="<%$ Resources:MyGlobal,DetailText %>" Text="<%$ Resources:MyGlobal,DetailText %>" DataNavigateUrlFields="PACKAGECODE" 
                                DataNavigateUrlFormatString="TicketPackageDetail.aspx?PACKAGECODE={0}" 
                                Target="_blank" />

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
        </div>
<%--     </fieldset>--%>

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

          //清除
          function Clear() {
              document.getElementById("<%=txtCityID.ClientID%>").value = "";
          }
          //===============================================

          var rowindexBound = 1;
          function Addrow(Hideindex, tableid) {
              var varNumber = document.getElementById("<%=txtNumber.ClientID%>").value;
              var varAmount = document.getElementById("<%=txtAmount.ClientID%>").value;
              var index = document.getElementById(Hideindex).value;

              var Newrow = document.getElementById(tableid).insertRow(document.getElementById(tableid).rows.length == 1 ? 1 : document.getElementById(tableid).rows.length);

              Newrow.id = "tr" + (document.getElementById(tableid).rows.length == 1 ? 1 : document.getElementById(tableid).rows.length);

              var NewcountCell = document.getElementById(tableid).rows.item(0).cells.length;
              document.getElementById(Hideindex).value = parseInt(document.getElementById(Hideindex).value) + 1
              index = document.getElementById(Hideindex).value;
              for (var j = 1; j < NewcountCell + 1; j++) {
                  var cell = Newrow.insertCell(j - 1);
                  cell.valign = "middle";
                  cell.align = "left";
                  switch (j) {
                      case 1:
                          cell.innerHTML = "<div align='center'><img src='img/delete.gif' width='26' height='28' alt='Delete' id='img" + Hideindex + "' name='i4" + "'onclick=\"delRow(this,'" + Hideindex + "','" + tableid + "')\" /></div>";
                          cell.id = "Cell_" + index + "_" + j;
                          //cell.width = "10px";
                          break;
                      case 2:
                          cell.innerHTML = "<div align='left'><input type='hidden'  id='number" + Hideindex + "' name='lblNumber' value='" + varNumber + "' />" + varNumber + "</div>";

                          cell.id = "Cell_" + index + "_" + j;
                          //cell.width = "30px";
                          break;
                      case 3:
                          cell.innerHTML = "<div align='left'><input type='hidden' id='amount" + Hideindex + "' name='lblAmount' value='" + varAmount + "' />" + varAmount + "</div>";
                          cell.id = "Cell_" + index + "_" + j;
                          //cell.width = "30px";
                          break;
                  }
              }
              // }
          }


          //delete_row
          function delRow(obj, Hideindex, tableid) {


              //lblNumber;
              //lblAmount;

              var num = document.getElementById(tableid).rows.length - 1;
              var Row = obj.parentNode;
              while (Row.tagName.toLowerCase() != "tr") {
                  Row = Row.parentNode;
              }
              switch (Hideindex) {
                  //case "hiddenIndex":
                  case "<%=hiddenIndex.ClientID %>":

                      if (confirm("<%= promptDelete %>")) {

                          //debugger;
                          var tr_row = document.getElementById(Row.id);
                          var imgValue = tr_row.cells[0].innerText;
                          var number = tr_row.cells[1].innerText; //张数
                          var amount = tr_row.cells[2].innerText; //金额

                          var ramainAmount = document.getElementById("<%=lbRemainAmount.ClientID%>").innerText; //剩余金额
                          var subAmount = parseInt(number) * parseInt(amount);
                          document.getElementById("<%=lbRemainAmount.ClientID%>").innerText = parseInt(ramainAmount) + subAmount;

                          Row.parentNode.removeChild(Row);

                      }
                      break;
                  default:
                      break;
              }
          }

          //输入总金额后，改变剩余价格的值
          function changeRemainValue() { 
                       
              var AllAmount = document.getElementById("<%=txtAllAmount.ClientID%>").value;
              if (AllAmount == "") {                  
                  alert("<%=promptTicketAmountIsEmpty %>");
                  //document.getElementById("<%=txtAllAmount.ClientID%>").focus();
                  return false;
              }

//              if (AllAmount != "" && isNaN(AllAmount)) {                  
//                  alert("<%=promptTicketAmountIsNaN %>");
//                  document.getElementById("<%=txtAllAmount.ClientID%>").focus();
//                  return false;
//              }

              var inputRules = /^\+?[1-9][0-9]*$/;///^\d+(\.\d+)?$/; //只能是数字且不能为负数
              if (inputRules.test(AllAmount) == false) {
                  alert("<%=promptTicketAmountIsNaN %>");
                  document.getElementById("<%=txtAllAmount.ClientID%>").focus();
                  return false;
              }
                  

              document.getElementById("<%=lbRemainAmount.ClientID%>").innerText = AllAmount;

              //删除table中的数据  
              var tbobj = document.getElementById("dt_ticket");
              var num = document.getElementById("dt_ticket").rows.length;
              if (num > 1) {
                  while (tbobj.rows.length > 1) {
                      tbobj.deleteRow(tbobj.rows.length - 1);
                  }
              }
              return true;
          }


          //清除这个包中有的Ticket数量
          function clearTicketInput() {
              document.getElementById("<%=txtNumber.ClientID%>").value = "";
              document.getElementById("<%=txtAmount.ClientID%>").value = "";
          }


          //检查是否为数字
          function checkTicketInput() {

              //先判断总额有没有输入，输入必须为数字
              var AllAmount = document.getElementById("<%=txtAllAmount.ClientID%>").value;
              if (AllAmount == "") {                  
                  alert("<%=promptAllAmountNoEmpty %>");
                  document.getElementById("<%=txtAllAmount.ClientID%>").focus();
                  return false;
              }

              if (AllAmount != "" && isNaN(AllAmount)) {                
                  alert("<%=promptAllAmountNoEmpty %>");
                  document.getElementById("<%=txtAllAmount.ClientID%>").focus();
                  return false;
              }

              var number = document.getElementById("<%=txtNumber.ClientID%>").value;
              var amount = document.getElementById("<%=txtAmount.ClientID%>").value;
              if (number == "" || isNaN(number) == true) {
                  alert("<%=promptTicketCountError%>");
                  document.getElementById("<%=txtNumber.ClientID%>").focus();
                  return false;
              }

              if (amount == "" || isNaN(amount) == true) {
                  alert("<%=promptTicketCountError%>");
                  document.getElementById("<%=txtAmount.ClientID%>").focus();
                  return false;
              }

              if (parseInt(amount) <= 0 || parseInt(number) <= 0) {
                  alert("<%=promptTicketCountAmountIsNan %>");
                  document.getElementById("<%=txtAmount.ClientID%>").focus();
                  return false;
              }


              var ramainAmount = document.getElementById("<%=lbRemainAmount.ClientID%>").innerText; //剩余金额
              var inputAmount = parseInt(number) * parseInt(amount);

              if (parseInt(AllAmount) >= inputAmount) {
                  if (ramainAmount >= inputAmount) {
                      document.getElementById("<%=lbRemainAmount.ClientID%>").innerText = parseInt(ramainAmount) - inputAmount;
                  }
                  else //输入金额大于剩余金额
                  {
                      alert("<%=amountBigRemain %>");

                      return false;
                  }
              }
              else {
                  alert("<%=promptAmountError %>");

                  return false;
              }

              //增加一行数据
              Addrow('<%=hiddenIndex.ClientID %>', 'dt_ticket');

              //清空刚才输入的信息
              clearTicketInput();

              return true;
          }

          //点保存的时候判断是否都输入了
          function checkEmpty() {
              var PackageName = document.getElementById("<%=txtPackageName.ClientID%>").value;
              if (PackageName == "") {
                  alert("<%=promptTicketNameIsEmpty %>");                  
                  document.getElementById("<%=txtPackageName.ClientID%>").focus();
                  return false;
              }

              if (PackageName != "" && PackageName.length>25) {
                  alert("优惠券礼包名称的长度不能大于25个字符！");
                  document.getElementById("<%=txtPackageName.ClientID%>").focus();
                  return false;
              }

              var AllAmount = document.getElementById("<%=txtAllAmount.ClientID%>").value;
              if (AllAmount == "") {
                  alert("<%=promptTicketCountError %>");                 
                  document.getElementById("<%=txtAllAmount.ClientID%>").focus();
                  return false;
              }

              if (AllAmount != "" && isNaN(AllAmount)) {                  
                  alert("<%=promptAllAmountIsNaN %>");
                  document.getElementById("<%=txtAllAmount.ClientID%>").focus();
                  return false;
              }

              var FromDate = document.getElementById("<%=FromDate.ClientID %>").value;
              if (FromDate == "") {                  
                  alert("<%=promptStartDateIsEmpty %>");
                  document.getElementById("<%=FromDate.ClientID %>").focus();
                  return false;
              }

              var EndDate = document.getElementById("<%=EndDate.ClientID %>").value;
              if (EndDate == "") {
                  alert("<%=promptEndDateIsEmpty %>");                  
                  document.getElementById("<%=EndDate.ClientID %>").focus();
                  return false;
              }

              var objNumber = document.getElementsByName("lblNumber");
              var objAmount = document.getElementsByName("lblAmount");
              if (objNumber == null || objAmount == null || objNumber.length == 0 || objAmount.length == 0) {
                  //alert("内含抵用券没有设置！");
                  alert("<%=promptIncludeTicketIsEmpty %>");
                  return false;
              }

              var ramainAmount = document.getElementById("<%=lbRemainAmount.ClientID%>").innerText; //剩余金额
              if (parseInt(ramainAmount) != 0) {
                  alert("<%=includeAmountNotEqualAllAmount %>");
                  return false;
              }

              var UserCount = document.getElementById("<%=txtUserCount.ClientID%>").value;
              if (UserCount == "") {
                  alert("<%=promptUserCountIsEmpty %>");
                  document.getElementById("<%=txtUserCount.ClientID%>").focus();
                  return false;
              }

              var inputRules = /^\+?[1-9][0-9]*$/;///^\d+(\.\d+)?$/; //只能是数字且不能为负数
              if (inputRules.test(UserCount) == false) {
                  alert("<%=promptUserCountIsNaN %>");
                  document.getElementById("<%=txtUserCount.ClientID%>").focus();
                  return false;
              }

              var UserRepCount = document.getElementById("<%=txtUserRepCount.ClientID%>").value;
              if (UserRepCount == "") {
                  alert("<%=promptUserRepCountIsEmpty %>");
                  document.getElementById("<%=txtUserRepCount.ClientID%>").focus();
                  return false;
              }

              if (inputRules.test(UserRepCount) == false) {
                  alert("<%=promptUserRepCountIsNaN %>");
                  document.getElementById("<%=txtUserRepCount.ClientID%>").focus();
                  return false;
              }


              if (!chkCustom(document.getElementById("<%=txtCustomNumber.ClientID %>").value)) {
                  alert("<%=customernumberErrorMsg %>");
                  return false;
              }

              if (window.confirm("<%=confirmSave %>") != true) {
                  return false;
              }
              return true;
          }

          function chkCustom(arg) {

              return true;
          }
//          function ChkCustomValue() {
//              if (document.getElementById("<%=chkCustomNumber.ClientID%>").checked == true) {
//                  document.getElementById("<%=txtCustomNumber.ClientID %>").value = "";
//                  document.getElementById("<%=txtCustomNumber.ClientID%>").disabled = true;
//              }
//              else {
//                  document.getElementById("<%=txtCustomNumber.ClientID%>").disabled = false;
//              }
          //          }
          function SerRbtNameValue(arg) {
              document.getElementById("<%=hidPackageType.ClientID %>").value = arg;
          }
    </script>

</asp:Content>

