<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="WebUI_GeneralSetting_FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    //点保存的时候判断是否都输入了
    function checkEmpty() {
        var Question = document.getElementById("<%=txtQuestion.ClientID%>").value;
        if (Question == "") {
            alert("问题不能为空！");
            document.getElementById("<%=txtQuestion.ClientID%>").focus();
            return false;
        }

        if ((Question != "") && (Question.length > 100)) {
            alert("问题的总长度最多只能输入100个字符！");
            document.getElementById("<%=txtQuestion.ClientID%>").focus();
            return false;
        }


        var Answer = document.getElementById("<%=txtAnswer.ClientID%>").value;
        if (Answer == "") {
            alert("请输入回答的内容！");
            document.getElementById("<%=txtAnswer.ClientID%>").focus();
            return false;
        }

        if ((Answer != "") && (Answer.length > 300)) {
            alert("答案的内容的总长度不能大于300个字符！");
            document.getElementById("<%=txtAnswer.ClientID%>").focus();
            return false;
        }
        return true;
    }

 </script>

 <div>
    <table cellpadding="0" cellspacing="0" align="center" width="100%" class="Table_BodyCSS">
            <tr class="RowTitle"><td colspan=2><asp:Literal Text="FAQ设置页面" ID="lbAdviceTitle" runat="server"></asp:Literal> </td></tr>
            <tr>
                <td style="width:10%; margin-left:15px;" class="tdcell"><div style="margin-left:15px">问题：</div></td>
                <td class="tdcell"><asp:TextBox ID="txtQuestion" runat="server" Width="96%" Rows="2" 
                        TextMode="MultiLine"  MaxLength="100"></asp:TextBox><font color="red">*</font></td>
            </tr>
            <tr>
                <td style="width:10%;" class="tdcell"><div style="margin-left:15px">回答：</div></td>
                <td class="tdcell"><asp:TextBox ID="txtAnswer" runat="server" Width="96%" Rows="10" 
                        TextMode="MultiLine" MaxLength="300"></asp:TextBox><font color="red">*</font></td>                         
            </tr>
            <tr>
                <td colspan="2" align="center" class="tdcell">                                
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn primary" OnClientClick="javascript:return checkEmpty();" onclick="btnSave_Click"  />
                    <font color=red>*</font>新增问题默认排序在最后
                </td>                         
            </tr>
                 
            <tr>
                <td colspan="2" align="right" class="tdcell">                                
                    <div runat="server" id="divAdjust" style="display:block; margin-right:15px;">
                    <asp:Button ID="btnAdjust" runat="server" Text="调整顺序" CssClass="btn primary" onclick="btnAdjust_Click"/></div>
                    <div runat="server" id="divSaveAdjust" style="display:none;margin-right:15px;">
                        <asp:Button ID="btnSaveAdjust" runat="server" Text="保存" CssClass="btn primary" onclick="btnSaveAdjust_Click"  />
                        <asp:Button ID="btnCancelAdjust" runat="server" Text="取消修改" CssClass="btn" onclick="btnCancelAdjust_Click"  />
                    </div>
                </td>                         
            </tr>
            <tr>
                <td class="tdcell" colspan=2>
                    <asp:GridView ID="FAQGridView" runat="server" CssClass="GView_BodyCSS" 
                        AllowSorting="True" Width="100%"
                        AutoGenerateColumns="False" OnRowCreated="FAQGridView_RowCreated"
                        PageSize="15" onrowcancelingedit="FAQGridView_RowCancelingEdit" 
                        onrowediting="FAQGridView_RowEditing" 
                        onrowupdating="FAQGridView_RowUpdating" DataKeyNames="ID" 
                        onrowdeleting="FAQGridView_RowDeleting" 
                        onrowdatabound="FAQGridView_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                   <asp:Label ID="lblID" runat="server"  Text='<%# Eval("ID")%>'></asp:Label>                                    
                               </ItemTemplate>
                               <ItemStyle  HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="问题">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditQuestion" runat="server" Rows="10" Text='<%# Eval("QUSETION_HEAD") %>' TextMode="MultiLine" Width="95%" MaxLength="100"></asp:TextBox>
                                </EditItemTemplate>
                               <ItemTemplate>
                                   <asp:Label ID="lblQuestion" runat="server"  Text='<%# Eval("QUSETION_HEAD") %>'></asp:Label>                                    
                               </ItemTemplate>
                                <ItemStyle  Width="20%" HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="回答">
                                 <EditItemTemplate>
                                     <asp:TextBox ID="txtEditAnswer" runat="server" Rows="10" 
                                         Text='<%# Eval("ANSWER_BODY") %>' TextMode="MultiLine" Width="95%" MaxLength="300"></asp:TextBox>
                                 </EditItemTemplate>
                               <ItemTemplate>
                                   <asp:Label ID="lblAnswer" runat="server"  Text='<%# Eval("ANSWER_BODY") %>'></asp:Label>                                    
                               </ItemTemplate> 
                               <ItemStyle  Width="55%" HorizontalAlign="Center" VerticalAlign="Top" />
                             </asp:TemplateField> 

                            <asp:TemplateField HeaderText="排序">
                                <EditItemTemplate>
                                    <asp:Label ID="lblSeq" runat="server" Text='<%# Eval("SEQ").ToString() %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSeqRead" runat="server" Enabled="False" 
                                        Text='<%# Eval("SEQ").ToString() %>' Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:CommandField ShowEditButton="True" >
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True" >
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
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
    
</asp:Content>

