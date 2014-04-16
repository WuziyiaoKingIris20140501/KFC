<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HotelPlanExam.aspx.cs" Inherits="WebUI_Hotel_HotelPlanExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<%-- <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
 <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
  <style type="text/css" >
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
    width: 1000px;
    height:640px;
    margin-top:50px;
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
  <script type="text/javascript">
        //显示弹出的层
        function invokeOpen2() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";

            //定义窗口
            //var msgObj=document.getElementById("popupDiv");
            //msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";        
        }

        //隐藏弹出的层
        function invokeClose2() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
        }

        function AddNew(pid) 
        {        
            invokeOpen2();
            LoadDetail(pid);
        }
        
        function checkLimit() 
        {
            var objLimit = document.getElementById("chkLimit");
            if (objLimit.checked == true) 
            {
                document.getElementById("<%=dtStartDate.ClientID%>").disabled = true;
                document.getElementById("<%=dtEndDate.ClientID%>").disabled = true;
                document.getElementById("<%=dtStartDate.ClientID%>").value = "";
                document.getElementById("<%=dtEndDate.ClientID%>").value = "";
            }
            else 
            {
                document.getElementById("<%=dtStartDate.ClientID%>").disabled = false;
                document.getElementById("<%=dtEndDate.ClientID%>").disabled = false;            
            }
        }

        //点搜索
        function checkEmpty() {

            var StartDate = document.getElementById("<%=dtStartDate.ClientID%>").value;
            var EndDate = document.getElementById("<%=dtEndDate.ClientID%>").value;

            //如果领用起始日期有一个为空，则提示.
            if ((StartDate == "" && EndDate != "") || (StartDate != "" && EndDate == "")) {
                alert("开始日期和结束日期必须同时有值或者不做限制！");
                document.getElementById("<%=dtStartDate.ClientID%>").focus();
                return false;
            }

            if (StartDate != "" && EndDate != "" && StartDate > EndDate) {
                alert("开始日期不能大于结束日期！");
                document.getElementById("<%=dtStartDate.ClientID%>").focus();
                return false;
            }

            return true;
        }

  </script>
    
   <!-----------for popup--------------->
    <div id="bgDiv2" class="bgDiv2">        
    </div>

     <div id="popupDiv2" class="popupDiv2">
          <table style="width:99%;" align="center">
             <tr>
               <td style="width:50%;" class="tdcell" >原状态</td>                                
               <td style="width:50%;"  class="tdcell" >修改状态</td>
            </tr>
            <tr>
                <td align="center"  style="height:22px;"> 
                    <input type="button" value="load" id ="btnLoad" class="btn primary" name ="btnClose"   onclick="LoadDetail();" />         
                    <input type="button" value="关闭" id="btnClose" class="btn" name="btnClose" onclick="invokeClose2();" />
                </td>
            </tr>
        </table>  
     </div>     
  <!---------------------------------->      
 <table align="center" border="0" width="100%" class="Table_BodyCSS">
    <tr class="RowTitle"><td colspan="4"><asp:Literal Text="审核单" ID="lblExamTitle" runat="server"></asp:Literal> </td></tr>               
    <tr>
           <td>
              <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  ChildrenAsTriggers="false">
               <ContentTemplate>
           <br />      
               <fieldset>
               <legend title="搜索审核单">搜索审核单</legend>       
                    <table align="center" border="0" width="100%">
                    <tr>
                        <td  class="tdcell" style="width:10%;text-align:left"  >
                            <div style="margin-left:15px"><asp:Label ID="lblCity" runat="server" Text="审核单类型：">审核单类型</asp:Label></div></td>
                        <td class="tdcell" align="left"  style="width:15%" >
                            <asp:DropDownList ID="ddlExamType" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td  class="tdcell" style="width:10%;text-align:left"  >
                         <div style="margin-left:15px"><asp:Label ID="Label1" runat="server" Text="是否已处理">是否已处理</asp:Label></div></td>
                        <td class="tdcell" align="left"  style="width:15%" >
                            <asp:DropDownList ID="ddlProccess" runat="server">
                                <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="1">已处理</asp:ListItem>
                                <asp:ListItem Value="0" Selected>未处理</asp:ListItem>
                            </asp:DropDownList>                
                        </td> 
                        <td  class="tdcell" style="width:10%;text-align:left"  >
                         <div style="margin-left:15px"><asp:Label ID="Label3" runat="server" Text="处理结果">处理结果</asp:Label></div></td>
                        <td class="tdcell" align="left"  style="width:15%" >
                            <asp:DropDownList ID="ddlProcessResult" runat="server">
                                <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="1">同意</asp:ListItem>
                                <asp:ListItem Value="0">拒绝</asp:ListItem>
                            </asp:DropDownList>                
                        </td> 
                     </tr>
                     <tr>                             
                        <td  class="tdcell" style="width:10%;text-align:left"  >
                         <div style="margin-left:15px"><asp:Label ID="Label2" runat="server" Text="开始日期："></asp:Label></div>
                         </td>
                        <td class="tdcell" align="left"  style="width:15%" >
                           <input id="dtStartDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/>            
                        </td> 
                        <td  class="tdcell" style="width:10%;text-align:left"  >
                         <div style="margin-left:15px"><asp:Label ID="Label4" runat="server" Text="结束日期："></asp:Label></div>
                         </td>
                        <td class="tdcell" align="left"  style="width:15%"  colspan=3 >
                           <input id="dtEndDate" class="Wdate" type="text" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/>
                           <input id="chkLimit" type="checkbox" value="1" onclick="javascript:checkLimit();"/>不限制
                        </td> 

                    </tr>
                      <tr> 
                        <td class="tdcell" align="left" colspan="6"> 
                            <div style="margin-left:15px"> <asp:Button ID="btnSearch" runat="server" CssClass="btn primary" 
                                Text="<%$ Resources:MyGlobal,SearchText %>" OnClientClick="return checkEmpty();" onclick="btnSearch_Click"/> </div>                    
                        </td>
                     </tr>
                    </table>
               </fieldset>
               <br /> 
               </ContentTemplate>
             </asp:UpdatePanel>           
           </td>            
     </tr> 
    <tr>
         <td> 
            <fieldset>
               <legend title="当前待审核">当前待审核</legend>                
               <div runat="server" id="divExamCount" style="margin-left:15px"></div>
            </fieldset>
         </td>            
     </tr>     
     
     <tr>
        <td>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
               <ContentTemplate> 
        <fieldset>
        <legend title="搜索结果">搜索结果</legend>  
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
                <tr>
                    <td align="center" class="tdcell" colspan="4">              
                        <asp:GridView ID="gridViewHotelPlanExam"  runat="server" AutoGenerateColumns="False" 
                            BackColor="White"  AllowPaging="True" PageSize="100" 
                            CssClass="GView_BodyCSS">
                            <Columns>
                                <asp:TemplateField HeaderText="任务ID">
                                    <ItemTemplate>                                    
                                   <%--<a  onclick="AddNew(<%#Eval("TASK_ID").ToString()%>)" href="#" ><asp:Label ID="lblTaskID" runat="server"  Text='<%# Eval("TASK_ID").ToString() %>'></asp:Label></a>  --%>
                                   <a onclick="OpenWnd('HotelPlanDetail.aspx?taskid=<%#Eval("TASK_ID").ToString()%>&planid=<%#Eval("REFID").ToString() %>','审核详单')" href="#" ><asp:Label ID="Label5" runat="server"  Text='<%# Eval("TASK_ID").ToString() %>'></asp:Label></a>                                  
                                    </ItemTemplate>                                    
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                              <%--  <asp:HyperLinkField HeaderText="任务ID"   DataTextField="TASK_ID" DataNavigateUrlFields="TASK_ID" 
                                DataNavigateUrlFormatString="HotelPlanDetail.aspx?taskid={0}" Target="_blank" 
                                NavigateUrl="~/WebUI/Hotel/HotelPlanDetail.aspx" >                              
                                <ItemStyle Width="5%" />
                                </asp:HyperLinkField>--%>

                                <asp:BoundField DataField="TASK_NAME" HeaderText="审核单类型" >                      
                                <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TASK_CREATETIME" HeaderText="创建时间" >
                                <ItemStyle Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TASK_CREATEBY" HeaderText="创建人" >
                                <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TASK_STATUS" HeaderText="是否已处理">                                    
                                <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TASK_APPROVEREJECT" HeaderText="处理结果" >
                                <ItemStyle Width="10%" />
                                </asp:BoundField>                                
                                <asp:TemplateField HeaderText="最后处理人">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblAutoTrust" runat="server"  Text='<%# Eval("TASK_CURPROCUSER").ToString()%>'></asp:Label>                                    
                                </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="最后处理时间">
                                    <ItemTemplate>                                    
                                    <asp:Label ID="lblStatus" runat="server"  Text='<%# Eval("TASK_UPDATETIME").ToString() %>'></asp:Label>                                    
                                </ItemTemplate>                                    
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>                                  
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
        </fieldset>
        </ContentTemplate>
        </asp:UpdatePanel>         
        </td>       
     </tr>     
         
   </table>

</asp:Content>

