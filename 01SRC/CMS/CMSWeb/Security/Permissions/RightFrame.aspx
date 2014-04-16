<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" Title="菜单权限管理"  AspCompat="true" CodeFile="RightFrame.aspx.cs" Inherits="Security_Permissions_RightFrame" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" >
<script language="JavaScript" type="text/javascript" src="../../Scripts/MzTreeView/MzTreeView10.js"></script> 

        <input id="Hidden1" type ="hidden" value ="1" />        
            <div id="right">
    <div class="frame01">
      <ul>
        <li class="title"><asp:Literal Text="菜单权限设置" ID="lbRightTitle" runat="server"></asp:Literal></li>
        <li>
        <table cellpadding="0" cellspacing="0" style="width:100%; height :800px; margin-top:10px" border="0" align="center" >
            <tr>
              <td style="width: 20%" class="tdTree" valign="top">
              <div style ="height:520px; overflow-y :auto; ">
              <script  language ="javascript" type ="text/javascript" >
              var tree = new MzTreeView("tree");
              var urlPath; //url路径
              var nodeText;//文本
              var menid;//id
              tree.icons["property"] = "property.gif";
              tree.icons["css"] = "collection.gif";
              tree.icons["book"]  = "book.gif";
              tree.iconsExpand["book"] = "bookopen.gif"; //展开时对应的图片
              tree.setIconPath("../../Scripts/MzTreeView/images/"); //可用相对路径
              //获得MenuList 并拼装树结构
              var ListItem;
              ListItem = <%=MenuList.GetItem(this) %>;
             
              tree.nodes["0_-1"] =""; //"text:<%=strParentName %>";
              var i = 1;
              var iTwoTemp =1000;
              var iTemp = 10000;
              for(var item in ListItem)
              {
                  ///debugger;
                  //一级目录
                  var str = "-1_" + i;
                  tree.nodes[str] = "text:" + item.split('|')[0]+";url:SetRight.aspx?mlevel=1&mname="+escape(item.split('|')[0]) + "&mnid="+item.split('|')[1];
                  
                    //二、三级目录
                    var strTEmp = ListItem[item];
                    for(var item1 in strTEmp.split(','))
                    {
                       iTwoTemp += parseInt(item1);
                       var str = i + "_" + iTwoTemp;
                       var strlist = ListItem[item].split(',')[parseInt(item1)].split('*')[0].split('|')[0];
                       var strmid = ListItem[item].split(',')[parseInt(item1)].split('*')[0].split('|')[2];
                       if(ListItem[item].split(',')[parseInt(item1)].split('*').length == 1)
                       {
                         
                          urlPath="SetRight.aspx?mlevel=2&mname="+escape(strlist)+"&mnid="+strmid; //ListItem[item].split(',')[parseInt(item1)].split('*')[0].split('|')[1];
                          nodeText =strlist;
                          tree.nodes[str]="text:" + nodeText + ";url:"+  urlPath ;
                          continue;
                       }
                       else
                       {
                          for(var item2 in ListItem[item].split(',')[parseInt(item1)].split('*')) 
                          {
                             if(item2 == 0)
                             {
                                tree.nodes[str]="text:" + strlist;
                                continue;
                             }
                             iTemp += parseInt(item2);
                             var str1 = iTwoTemp+ "_" + iTemp;
                             //ListItem[item].split(',')[parseInt(item1)].split('*')[parseInt(item2)].split('|')[1];
                             nodeText=ListItem[item].split(',')[parseInt(item1)].split('*')[parseInt(item2)].split('|')[0];
                             menid=ListItem[item].split(',')[parseInt(item1)].split('*')[parseInt(item2)].split('|')[2];
                             urlPath="SetRight.aspx?mlevel=2&mname="+escape(nodeText)+"&mnid=" + menid; 
                             tree.nodes[str1] = "text:" + nodeText + ";url:"+ urlPath ;               
                          }
                       }
                    }
               i++;
               iTwoTemp++;
               iTemp++;
              }
              tree.setTarget("frame_set");
              document.write(tree.toString());
              if(document.getElementById("Hidden1").value != "")
              {
                 tree.expandAll();
              }
          </script>
             </div>
                </td>
              <td style="padding:0 10px; vertical-align:top;text-align: left; width:80%;height :100%">
                    <iframe id="frame_set" name="frame_set" src="SetRight.aspx"  frameborder="0"  width="100%"  scrolling="auto" style="height: 100%;"></iframe>
                </td>
            </tr>
        </table>
                </li>
      </ul>
    </div>
</div>
</asp:Content>