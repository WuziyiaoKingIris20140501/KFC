<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HotelConsultingRoomAllAsyncTable.aspx.cs" Inherits="WebUI_Hotel_HotelConsultingRoomAllAsyncTable" %>

<%@ Register Src="../../UserControls/WebAutoComplete.ascx" TagName="WebAutoComplete"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
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
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>
    <script type="text/javascript">
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

        function aaa()
        {
            var AllRows = document.getElementById("<%=gridHotelList.ClientID%>").getElementsByTagName("tr");
            for (var i = 0; i < AllRows.length; i++) {
                var rowOldColor =AllRows[i].cells[4].innerHTML;
                var colOldColor =AllRows[i].cells[5].innerHTML;
                if (document.getElementById("<%=HidBrowser.ClientID%>").value == "IE")
                {
                    if(AllRows[i].style.backgroundColor=="#ffcc66"){
                        AllRows[i].style.backgroundColor=rowOldColor;
                        AllRows[i].childNodes[1].childNodes[3].style.backgroundColor=colOldColor;
                    }
                        
                }else
                {    
                    if(AllRows[i].style.backgroundColor=="#ffcc66" || AllRows[i].style.backgroundColor=="rgb(255, 204, 102)"){
                        AllRows[i].style.backgroundColor=rowOldColor;
                        AllRows[i].childNodes[2].childNodes[5].style.backgroundColor=colOldColor;
                    }
                }
            }
        }

         function ClickEvent(pcode, pid, selIndex, cityId, EXLinkMan, EXLinkTel, EXRemark, flag,obj,isRefresh) {
            //单个酒店的Click事件
            //根据酒店ID，判断当前酒店在缓存数据中是否存在，如果存在，拼装计划列表，反之，根据酒店ID，触发单个酒店的Click事件，并且在后台直接拼装计划列表，展示
            if(flag=="true"){
                BtnLoadStyle(); 
            }
            
            document.getElementById("<%=HidSelIndexOld.ClientID%>").value = document.getElementById("<%=HidSelIndex.ClientID%>").value;

            document.getElementById("<%=spanHotelInfo.ClientID%>").innerHTML="[" + pid+ "]&nbsp; - &nbsp;" +pcode;
            document.getElementById("<%=HidIsBackstage.ClientID%>").value = "0";
            document.getElementById("<%=HidPcode.ClientID%>").value = pcode;
            document.getElementById("<%=HidPid.ClientID%>").value = pid;
            document.getElementById("<%=HidSelIndex.ClientID%>").value = selIndex;
            document.getElementById("<%=HidCityID.ClientID%>").value = cityId;
            document.getElementById("<%=HidHotelEXLinkMan.ClientID%>").value = EXLinkMan;//酒店执行联系人
            document.getElementById("<%=HotelEXLinkMan_span.ClientID%>").innerHTML=EXLinkMan;
            document.getElementById("<%=HotelEXLinkMan_txt.ClientID%>").value=EXLinkMan;
            document.getElementById("<%=HidHotelEXLinkTel.ClientID%>").value = EXLinkTel;//酒店执行电话
            document.getElementById("<%=HotelEXLinkTel_span.ClientID%>").innerHTML=EXLinkTel;
            document.getElementById("<%=HotelEXLinkTel_txt.ClientID%>").value=EXLinkTel;
            document.getElementById("<%=HidHotelEXLinkRemark.ClientID%>").value = EXRemark;//酒店执行备注   
            document.getElementById("<%=HotelEXLinkRemark_span.ClientID%>").innerHTML=EXRemark;
            document.getElementById("<%=HotelEXLinkRemark_txt.ClientID%>").value=EXRemark;
            //document.getElementById("<%=HidIsRefresh.ClientID%>").value=isRefresh;
            

            document.getElementById("<%=HidLinkDetails.ClientID%>").value= "酒店联系人:" + EXLinkMan + "   &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  电话:      " +EXLinkTel;
             
            if(obj=="true")
            {
                document.getElementById("<%=HidIsBackstage.ClientID%>").value="0";  
                document.getElementById("<%=btnSingleHotel.ClientID%>").click();
            }
            else
            {
                if(document.getElementById("<%=HidIsLoadFinish.ClientID%>").value == "1"){
                    //判断上一个酒店 缓存是否存在
                    if(document.getElementById("<%=HidLastHotelSelectID.ClientID%>").value==pid){//酒店ID 
                        if (document.getElementById("<%=HidSelIndexOld.ClientID%>").value != selIndex)
                        {
                            if (document.getElementById("<%=HidSelIndexOld.ClientID%>").value!="")
                            {
                                var AllRows = document.getElementById("<%=gridHotelList.ClientID%>").getElementsByTagName("tr");
                                var rowOldColor =AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].cells[4].innerHTML;
                                var colOldColor =AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].cells[5].innerHTML;

                                if (document.getElementById("<%=HidBrowser.ClientID%>").value == "IE")
                                {
                                    AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].style.backgroundColor=rowOldColor;
                                    AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].childNodes[1].childNodes[3].style.backgroundColor=colOldColor;
                                    if(AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseover")=="" || AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseover")==null){
                                        AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].onmouseover= function(){this.style.backgroundColor='#ebebce';this.childNodes[1].childNodes[3].style.backgroundColor='#ebebce';}
                                    }
                                    if(AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseout")=="" || AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseout")==null){
                                        var t = AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].style.backgroundColor;
                                        var c = AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].childNodes[1].childNodes[3].style.backgroundColor;
                                        AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].onmouseout= function(){this.style.backgroundColor=t;this.childNodes[1].childNodes[3].style.backgroundColor=c;}
                                    }
                                }else
                                {
                                    AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].style.backgroundColor=rowOldColor;
                                    AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].childNodes[2].childNodes[5].style.backgroundColor=colOldColor;
                                    if(AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseover")=="" || AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseover")==null){
                                        AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].onmouseover= function(){this.style.backgroundColor='#ebebce';this.childNodes[2].childNodes[5].style.backgroundColor='#ebebce';}
                                    }
                                    if(AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseout")=="" || AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseout")==null){
                                        var t = AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].style.backgroundColor;
                                        var c = AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].childNodes[2].childNodes[5].style.backgroundColor;
                                        AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].onmouseout= function(){this.style.backgroundColor=t;this.childNodes[2].childNodes[5].style.backgroundColor=c;}
                                    }
                                }
                            }
                            //if(AllRows[selIndex].style.backgroundColor=="#ffcc66"){
                                AllRows[selIndex].onmouseover= null;
                            //}
                            //if(AllRows[selIndex].style.backgroundColor=="#ebebce"){
                                AllRows[selIndex].onmouseout= null;
                            //}
//                            AllRows[selIndex].style.backgroundColor="blue";
//                            AllRows[selIndex].childNodes[1].childNodes[3].style.backgroundColor="blue";
                            if (document.getElementById("<%=HidBrowser.ClientID%>").value == "IE")
                            {
                                AllRows[selIndex].style.backgroundColor="#FFCC66";
                                AllRows[selIndex].childNodes[1].childNodes[3].style.backgroundColor="#FFCC66";

                                AllRows[selIndex].style.backgroundColor="#FFCC66";
                                AllRows[selIndex].childNodes[1].childNodes[3].style.backgroundColor="#FFCC66";
                            }else
                            {
                                AllRows[selIndex].style.backgroundColor="#FFCC66";
                                AllRows[selIndex].childNodes[2].childNodes[5].style.backgroundColor="#FFCC66";                                
                            }
                        } 

                        //取房型
                        var LastHotelRoomListLMBARCode = document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value;//上一个酒店的Lmbar房型Code
                        var LastHotelRoomListLMBAR = document.getElementById("<%=HidLastHotelRoomListLMBAR.ClientID%>").value;//上一个酒店的Lmbar房型
                
                        var LastHotelRoomListLMBAR2Code = document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value;//上一个酒店的Lmbar2房型Code
                        var LastHotelRoomListLMBAR2 = document.getElementById("<%=HidLastHotelRoomListLMBAR2.ClientID%>").value;//上一个酒店的Lmbar2房型

                        //取计划
                        var LastHotelPlanListLmbar = document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value;//上一个酒店的Lmbar计划
                        var LastHotelPlanListLmbar2 = document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value;//上一个酒店的Lmbar2计划   

                        //取时间段
                        var DtTime = document.getElementById("<%=HidDtTime.ClientID%>").value;
                
                        AssemblyDiv(LastHotelRoomListLMBARCode,LastHotelRoomListLMBAR2Code,LastHotelPlanListLmbar,LastHotelPlanListLmbar2,DtTime);

                        //document.getElementById("<%=BtnHotelSite.ClientID%>").click(); 
                       judgeLastOrNext(selIndex);
                       GetResultFromServer();
                       BtnCompleteStyle(); 
                       showA();         
                    }
                    //判断下一个酒店 缓存是否存在
                    else if(document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value==pid){//酒店ID
                        if (document.getElementById("<%=HidSelIndexOld.ClientID%>").value != selIndex)
                        {
                            if (document.getElementById("<%=HidSelIndexOld.ClientID%>").value!="")
                            {
                                var AllRows = document.getElementById("<%=gridHotelList.ClientID%>").getElementsByTagName("tr");
                                var rowOldColor =AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].cells[4].innerHTML;
                                var colOldColor =AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].cells[5].innerHTML;

                                if (document.getElementById("<%=HidBrowser.ClientID%>").value == "IE")
                                {
                                    AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].style.backgroundColor=rowOldColor;
                                    AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].childNodes[1].childNodes[3].style.backgroundColor=colOldColor;
                                    if(AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseover")=="" || AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseover")==null){
                                        AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].onmouseover= function(){this.style.backgroundColor='#ebebce';this.childNodes[1].childNodes[3].style.backgroundColor='#ebebce';}
                                    }
                                    if(AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseout")=="" || AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseout")==null){
                                        var t = AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].style.backgroundColor;
                                        var c = AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].childNodes[1].childNodes[3].style.backgroundColor;
                                        AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].onmouseout= function(){this.style.backgroundColor=t;this.childNodes[1].childNodes[3].style.backgroundColor=c;}
                                    }
                                }else
                                {
                                    AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].style.backgroundColor=rowOldColor;
                                    AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].childNodes[2].childNodes[5].style.backgroundColor=colOldColor;
                                    if(AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseover")=="" || AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseover")==null){
                                        AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].onmouseover= function(){this.style.backgroundColor='#ebebce';this.childNodes[2].childNodes[5].style.backgroundColor='#ebebce';}
                                    }
                                    if(AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseout")=="" || AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].getAttribute("onmouseout")==null){
                                        var t = AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].style.backgroundColor;
                                        var c = AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].childNodes[2].childNodes[5].style.backgroundColor;
                                        AllRows[document.getElementById("<%=HidSelIndexOld.ClientID%>").value].onmouseout= function(){this.style.backgroundColor=t;this.childNodes[2].childNodes[5].style.backgroundColor=c;}
                                    }
                                }
                            }
                            //if(AllRows[selIndex].style.backgroundColor=="#ffcc66"){
                                AllRows[selIndex].onmouseover= null;
                            //}
                            //if(AllRows[selIndex].style.backgroundColor=="#ebebce"){
                                AllRows[selIndex].onmouseout= null;
                            //}
//                            AllRows[selIndex].style.backgroundColor="blue";
//                            AllRows[selIndex].childNodes[1].childNodes[3].style.backgroundColor="blue";
                            if (document.getElementById("<%=HidBrowser.ClientID%>").value == "IE")
                            {
                                AllRows[selIndex].style.backgroundColor="#FFCC66";
                                AllRows[selIndex].childNodes[1].childNodes[3].style.backgroundColor="#FFCC66";
                            }else
                            {
                                AllRows[selIndex].style.backgroundColor="#FFCC66";
                                AllRows[selIndex].childNodes[2].childNodes[5].style.backgroundColor="#FFCC66";                                
                            }
                        } 
//                        if(document.getElementById("<%=HidSelIndexOld.ClientID%>").value != selIndex)
//                        {
//                            document.getElementById("<%=HidSelIndexOld.ClientID%>").value = selIndex;
//                        }
                        //取房型
                        var NextHotelRoomListLMBARCode = document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value;//下一个酒店的Lmbar房型Code
                        var NextHotelRoomListLMBAR = document.getElementById("<%=HidNextHotelRoomListLMBAR.ClientID%>").value;//下一个酒店的Lmbar房型
                 
                        var NextHotelRoomListLMBAR2Code = document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value;//下一个酒店的Lmbar2房型Code
                        var NextHotelRoomListLMBAR2 = document.getElementById("<%=HidNextHotelRoomListLMBAR2.ClientID%>").value;//下一个酒店的Lmbar2房型

                        //取计划
                        var NextHotelPlanListLmbar = document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value;//下一个酒店的Lmbar计划
                        var NextHotelPlanListLmbar2 = document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value;//下一个酒店的Lmbar2计划

                        //取时间段
                        var DtTime = document.getElementById("<%=HidDtTime.ClientID%>").value;
                
                        AssemblyDiv(NextHotelRoomListLMBARCode,NextHotelRoomListLMBAR2Code,NextHotelPlanListLmbar,NextHotelPlanListLmbar2,DtTime);

                        //document.getElementById("<%=BtnHotelSite.ClientID%>").click();
                       
                       judgeLastOrNext(selIndex);
                       GetResultFromServer();
                       BtnCompleteStyle();
                       showA();  
                       
                                      
                    }else
                    {
                        document.getElementById("<%=HidIsBackstage.ClientID%>").value="0";  
                        document.getElementById("<%=btnSingleHotel.ClientID%>").click();
                    } 
                }else
                {
                    document.getElementById("<%=HidIsBackstage.ClientID%>").value="0";  
                    document.getElementById("<%=btnSingleHotel.ClientID%>").click();
                } 
            }        
        }
        
        //拼装(Lmbar房型 \  Lmbar2房型   \  Lmbar计划   \  Lmbar2计划  \  相差日期数 )
        function AssemblyDiv(HotelRoomListLMBARCode,HotelRoomListLMBAR2Code,HotelPlanListLmbar,HotelPlanListLmbar2,DtTime)
        {
            //////debugger;
            var sb ='';
            document.getElementById("<%=divMain.ClientID%>").innerHTML="";
            var sumWidth=160;//总宽度
            var lmbar2W = "80px";

            var lmbar2TitleWidth;//LMBAR2宽度
            //if(HotelRoomListLMBAR2Code==""||HotelRoomListLMBAR2Code.split('&').length==0)
            if(HotelRoomListLMBAR2Code=="" || typeof(HotelRoomListLMBAR2Code) == "undefined") 
            {
                sumWidth=accAdd(sumWidth,80);
                lmbar2TitleWidth="80px";
            }else
            {
                var sl = accMul(HotelRoomListLMBAR2Code.split('&').length,80);
                sumWidth=accAdd(sumWidth,sl);
                lmbar2TitleWidth=sl+"px";
            } 
            var lmbarTitleWidth;//LMBAR宽度
            if(HotelRoomListLMBARCode=="" || typeof(HotelRoomListLMBARCode) == "undefined")//||HotelRoomListLMBARCode.split('&').length==0)
            {
                sumWidth=accAdd(sumWidth,80);
                lmbarTitleWidth="80px";
            }else
            {
                var sl2 = accMul(HotelRoomListLMBARCode.split('&').length,80);
                sumWidth=accAdd(sumWidth,sl2);
                lmbarTitleWidth=sl2+"px";
            } 
            
            sumWidth=sumWidth+"px";

            sb +="<table width=\"" + sumWidth + "\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
            sb +="<tr align=\"center\">";
            sb +="<td rowspan=\"2\" style=\"width: 80px; border: solid #8D8D8D 1px;\">批量操作</td>";
            sb +="<td rowspan=\"2\" style=\"width: 80px; border: solid #8D8D8D 1px;\">日期" + "\\" + "房型</td>";
            sb +="<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\">LMBAR2</td><td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;\">LMBAR</td>";
            sb +="</tr>";
            sb +="<tr>";

            //循环LMBAR2 房型CODE    最后一个COde   td的style  去掉.
            document.getElementById("<%=HidLastHotelRoomListLMBAR2.ClientID%>").value ="";
            var RoomListLMABAR2=0;
            if(HotelRoomListLMBAR2Code!=""&& typeof(HotelRoomListLMBAR2Code) != "undefined")
            {
                RoomListLMABAR2 = HotelRoomListLMBAR2Code.split('&').length;
            }
            if (RoomListLMABAR2 > 0)
            {
                sb +="<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" style=\" border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb +="<tr align=\"center\">";
                for (i = 0; i < RoomListLMABAR2; i++)
                {
                    if (RoomListLMABAR2 - 1 == i)
                    {
                        sb +="<td style=\"width:" + lmbar2W + ";\">";
                    }
                    else
                    {
                        sb +="<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">";
                    }
                    sb +="<span>" + jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[i])["ROOMNM"] + "</span></br><span>" + jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[i])["ROOMCODE"] + "<span>";
                    sb +="</td>";

                    document.getElementById("<%=HidLastHotelRoomListLMBAR2.ClientID%>").value += jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[i])["ROOMCODE"]+",";
                }
            }
            else
            {
                sb +="<td style=\"width: " + lmbar2TitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb +="<tr align=\"center\">";

                sb +="<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">";
                sb +="</td>";
            }
            sb +="</tr> </table></td>";
        
            // 循环LMBAR 房型CODE     最后一个COde   td的style  去掉
            document.getElementById("<%=HidLastHotelRoomListLMBAR.ClientID%>").value ="";
            var RoomListLMABAR =0;
            if(HotelRoomListLMBARCode!="" && typeof(HotelRoomListLMBARCode) != "undefined")
            {
                RoomListLMABAR = HotelRoomListLMBARCode.split('&').length;
            }
            if (RoomListLMABAR > 0)
            {
                sb +="<td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;\"><table width=\"100%\" height=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb +="<tr align=\"center\">";
                for (i = 0; i < RoomListLMABAR; i++)
                {
                    if (RoomListLMABAR - 1 == i)
                    {
                        sb +="<td style=\"width:" + lmbar2W + ";padding-top:5px;\">";
                    }
                    else
                    {
                        sb +="<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";padding-top:5px;\">";
                    }
                    sb +="<span>" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[i])["ROOMNM"] + "</span></br><span>" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[i])["ROOMCODE"] + "<span>";
                    sb +="</td>";

                    document.getElementById("<%=HidLastHotelRoomListLMBAR.ClientID%>").value += jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[i])["ROOMCODE"]+",";
                }
            }
            else
            {
                sb +="<td style=\"width: " + lmbarTitleWidth + "; border: solid #8D8D8D 1px;border-collapse:collapse;\"><table width=\"100%\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb +="<tr align=\"center\">";
                sb +="<td style=\"border-right: solid #8D8D8D 1px;width:" + lmbar2W + ";\">";
                sb +="";
                sb +="</td>";
            }

            sb +="</tr></table></td>";
            sb +="</tr>";


            var IsDayOfWeek = false;
            var day = 0;
            if(DtTime!="" && typeof(DtTime) != "undefined")
            {
                day = DtTime.split('&').length;
            }
            for (i = 0; i < day; i++)
            {
                sb +="<tr align=\"center\">";//DtTime
                // 日期
                if (jQuery.parseJSON(DtTime.split('&')[i])["IsWeek"] == "true")
                {
                    IsDayOfWeek = true;
                    sb +="<td style=\"width: 80px; border: solid #8D8D8D 1px;background-color: #CDEBFF;height:40px;\"><input type=\"checkbox\" id=\"chkMarkFullRoom" + i + "\" name=\"chkMarkFullRoom\"  value=\"" + jQuery.parseJSON(DtTime.split('&')[i])["time"] + "\"/></td>";
                    sb +="<td style=\"width: 80px; border: solid #8D8D8D 1px;background-color: #CDEBFF;height:40px;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">" + jQuery.parseJSON(DtTime.split('&')[i])["time"] + "</td>";
                }
                else
                {
                    IsDayOfWeek = false;
                    sb +="<td style=\"width: 80px; border: solid #8D8D8D 1px;height:40px;\"><input type=\"checkbox\" id=\"chkMarkFullRoom" + i + "\"  name=\"chkMarkFullRoom\" value=\"" + jQuery.parseJSON(DtTime.split('&')[i])["time"] + "\"/></td>";
                    sb +="<td style=\"width: 80px; border: solid #8D8D8D 1px;height:40px;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">" + jQuery.parseJSON(DtTime.split('&')[i])["time"] + "</td>";
                }
                sb +="<td width=\"" + lmbar2TitleWidth + "\" > <table width=\"" + lmbar2TitleWidth + "\" style=\"border-collapse: collapse; border: none; \" cellpadding=\"0\" cellspacing=\"0\">";
                sb +="<tr align=\"center\" > ";


                 //循环LMBAR2   酒店计划中的房型数量和价格
                var PlanLMBAR2 =0;
                if(HotelPlanListLmbar2!="" && typeof(HotelPlanListLmbar2) != "undefined")
                {
                    PlanLMBAR2 = HotelPlanListLmbar2.split('&').length;
                }
                if (PlanLMBAR2 > 0)
                {
                    var flag = false;
                    if (RoomListLMABAR2 > 0)
                    {
                        for (j = 0; j < RoomListLMABAR2; j++)
                        {
                            flag = false;
                            for (k = 0; k < PlanLMBAR2; k++)
                            {
                                //Date.parse(a.replace(/\-/g,"/"))-Date.parse(b)==0
                                //if (jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMCODE"] == jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMTYPECODE"] && jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["EFFECTDATESTRING"] == jQuery.parseJSON(DtTime.split('&')[i])["time"])
                                //if (RoomListLMABAR2[j]["ROOMCODE"].ToString() == PlanLMBAR2[k]["ROOMTYPECODE"].ToString() && DateTime.Parse(PlanLMBAR2[k]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(dtTime.Rows[i]["time"].ToString()))
                                if (jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMCODE"] == jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMTYPECODE"] && (Date.parse(jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["EFFECTDATESTRING"].replace(/\-/g,"/"))-Date.parse(jQuery.parseJSON(DtTime.split('&')[i])["time"])==0))
                                {
                                    flag = true;
                                    if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"] == "0")
                                    {
                                        if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["STATUS"] == "false")
                                        {
                                            sb +="<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">";
                                            sb +="<table width=\"100%\" style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR2','" +jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"];
                                            if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] == "0")
                                            {
                                                sb +="<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else
                                            {
                                                sb +="</td></tr>";
                                            }
                                            sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                            sb +="</td>";
                                        }
                                        else if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISROOMFUL"] == "1")
                                        {
                                            sb +="<td style=\"background-color:#E96928;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E96928'\">";
                                            sb +="<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR2','" +jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"];
                                            if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] == "0")
                                            {
                                                sb +="<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else
                                            {
                                                sb +="</td></tr>";
                                            }
                                            sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                            sb +="</td>";
                                        }
                                        else
                                        {
                                            sb +="<td style=\"background-color:#E6B9B6;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E6B9B6'\">";
                                            sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR2','" +jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"];
                                            if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] == "0")
                                            {
                                                sb +="<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else
                                            {
                                                sb +="</td></tr>";
                                            }
                                            sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                            sb +="</td>";
                                        }
                                    }
                                    else
                                    {
                                        if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["STATUS"] == "false")
                                        {
                                            sb +="<td style=\"background-color:#999999;border-right: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;border-bottom: solid #8D8D8D 1px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">";
                                            sb +="<table width=\"100%\"  style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR2','" +jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"];
                                            if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] == "0")
                                            {
                                                sb +="<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else
                                            {
                                                sb +="</td></tr>";
                                            }
                                            sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                            sb +="</td>";
                                        }
                                        else
                                        {
                                            if (!IsDayOfWeek)
                                            {
                                                sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                                sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR2','" +jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"];
                                                if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] == "0")
                                                {
                                                    sb +="<span style=\"color: Red\">*</span></td></tr>";
                                                }
                                                else
                                                {
                                                    sb +="</td></tr>";
                                                }
                                                sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                                sb +="</td>";
                                            }
                                            else
                                            {
                                                sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                                sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\"  height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR2','" +jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBAR2Code.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ROOMNUM"];
                                                if (jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["ISRESERVE"] == "0")
                                                {
                                                    sb +="<span style=\"color: Red\">*</span></td></tr>";
                                                }
                                                else
                                                {
                                                    sb +="</td></tr>";
                                                }
                                                sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar2.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                                sb +="</td>";
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                if (!IsDayOfWeek)
                                {
                                    sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                    sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                    sb +="</td>";
                                }
                                else
                                {
                                    sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                    sb +="<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                    sb +="</td>";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!IsDayOfWeek)
                        {
                            sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                            sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb +="</td>";
                        }
                        else
                        {
                            sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                            sb +="<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb +="</td>";
                        }
                    }
                }
                else
                {
                    if (!IsDayOfWeek)
                    {
                        if (RoomListLMABAR2 > 0)
                        {
                            for (j = 0; j < RoomListLMABAR2; j++)
                            {
                                sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                sb +="</td>";
                            }
                        }
                        else
                        {
                            sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                            sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb +="</td>";
                        }
                    }
                    else
                    {
                        if (RoomListLMABAR2 > 0)
                        {
                            for (j = 0; j < RoomListLMABAR2; j++)
                            {
                                sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                sb +="</td>";
                            }
                        }
                        else
                        {
                            sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                            sb +="<table width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb +="</td>";
                        }
                    }
                }

                sb +="</tr>";
                sb +="</table></td>";

                sb +="<td width=\"" + lmbarTitleWidth + "\" ><table width=\"" + lmbarTitleWidth + "\" style=\"border-collapse: collapse; border: none;\" cellpadding=\"0\" cellspacing=\"0\">";
                sb +="<tr align=\"center\">";
             
                // 循环LMBAR   酒店计划中的房型数量和价格
                var PlanLMBAR =0;
                if(HotelPlanListLmbar!="" && typeof(HotelPlanListLmbar) != "undefined")
                {
                    PlanLMBAR = HotelPlanListLmbar.split('&').length;
                }
                if (PlanLMBAR > 0)
                {
                    var flag = false;
                    if (RoomListLMABAR > 0)
                    {
                        for (j = 0; j < RoomListLMABAR; j++)
                        {
                            flag = false;
                            for (k = 0; k < PlanLMBAR; k++)
                            {
                                //if (RoomListLMABAR[j]["ROOMCODE"].ToString() == PlanLMBAR[k]["ROOMTYPECODE"].ToString() && DateTime.Parse(PlanLMBAR[k]["EFFECTDATESTRING"].ToString()) == DateTime.Parse(dtTime.Rows[i]["time"].ToString()))
                                if (jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMCODE"] == jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMTYPECODE"] && (Date.parse(jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["EFFECTDATESTRING"].replace(/\-/g,"/"))-Date.parse(jQuery.parseJSON(DtTime.split('&')[i])["time"])==0))
                                {
                                    flag = true;
                                    if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"] == "0")
                                    {
                                        if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["STATUS"] == "false")
                                        {
                                            sb +="<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">";
                                            sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"  onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"];
                                            if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] == "0")
                                            {
                                                sb +="<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else
                                            {
                                                sb +="</td></tr>";
                                            }
                                            sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                            sb +="</td>";
                                        }
                                        else if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISROOMFUL"] == "1")
                                        {
                                            sb +="<td style=\"background-color: #E96928;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E96928'\">";
                                            sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"];
                                            if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] == "0")
                                            {
                                                sb +="<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else
                                            {
                                                sb +="</td></tr>";
                                            }
                                            sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])[k]["TWOPRICE"] + "</td></tr></table>";
                                            sb +="</td>";
                                        }
                                        else
                                        {
                                            sb +="<td style=\"background-color: #E6B9B6;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#E6B9B6'\">";
                                            sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"];
                                            if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] == "0")
                                            {
                                                sb +="<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else
                                            {
                                                sb +="</td></tr>";
                                            }
                                            sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                            sb +="</td>";
                                        }
                                    }
                                    else
                                    {
                                        if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["STATUS"] == "false")
                                        {
                                            sb +="<td style=\"background-color: #999999;border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#999999'\">";
                                            sb +="<table width=\"100%\"  cellpadding=\"0\" cellspacing=\"0\" style=\"border: none;\"  height=\"40px;\"  onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"];
                                            if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] == "0")
                                            {
                                                sb +="<span style=\"color: Red\">*</span></td></tr>";
                                            }
                                            else
                                            {
                                                sb +="</td></tr>";
                                            }
                                            sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                            sb +="</td>";
                                        }
                                        else
                                        {
                                            if (!IsDayOfWeek)
                                            {
                                                sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                                sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"];
                                                if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] == "0")
                                                {
                                                    sb +="<span style=\"color: Red\">*</span></td></tr>";
                                                }
                                                else
                                                {
                                                    sb +="</td></tr>";
                                                }
                                                sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                                sb +="</td>";
                                            }
                                            else
                                            {
                                                sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                                sb +="<table width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" onclick=\"showDiv('" + document.getElementById("<%=HidPid.ClientID%>").value + "','LMBAR','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMNM"] + "','" + jQuery.parseJSON(HotelRoomListLMBARCode.split('&')[j])["ROOMCODE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["STATUS"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] + "','" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["EFFECTDATESTRING"] + "')\"><tr align=\"center\"><td>" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ROOMNUM"];
                                                if (jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["ISRESERVE"] == "0")
                                                {
                                                    sb +="<span style=\"color: Red\">*</span></td></tr>";
                                                }
                                                else
                                                {
                                                    sb +="</td></tr>";
                                                }
                                                sb +="<tr align=\"center\"><td>￥" + jQuery.parseJSON(HotelPlanListLmbar.split('&')[k])["TWOPRICE"] + "</td></tr></table>";
                                                sb +="</td>";
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                if (!IsDayOfWeek)
                                {
                                    sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                    sb +="<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                    sb +="</td>";
                                }
                                else
                                {
                                    sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                    sb +="<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                    sb +="</td>";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!IsDayOfWeek)
                        {
                            sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                            sb +="<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb +="</td>";
                        }
                        else
                        {
                            sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                            sb +="<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\" ><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb +="</td>";
                        }
                    }
                }
                else
                {
                    if (!IsDayOfWeek)
                    {
                        if (RoomListLMABAR > 0)
                        {
                            for (j = 0; j < RoomListLMABAR; j++)
                            {
                                sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                                sb +="<table  width=\"100%\" style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                sb +="</td>";
                            }
                        }
                        else
                        {
                            sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='white'\">";
                            sb +="<table  width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb +="</td>";
                        }
                    }
                    else
                    {
                        if (RoomListLMABAR > 0)
                        {
                            for (j = 0; j < RoomListLMABAR; j++)
                            {
                                sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                                sb +="<table  width=\"100%\"  style=\"border: none;\"  cellpadding=\"0\" cellspacing=\"0\" height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                                sb +="</td>";
                            }
                        }
                        else
                        {
                            sb +="<td style=\"border-right: solid #8D8D8D 1px;border-bottom: solid #8D8D8D 1px;border-left-width:0px;border-top-width:0px;width:" + lmbar2W + ";background-color:#CDEBFF;\" onmousemove=\"javacript:this.style.backgroundColor='#CDEBFF'\" onmouseout=\"javacript:this.style.backgroundColor='#CDEBFF'\">";
                            sb +="<table  width=\"100%\"  style=\"border: none;\" cellpadding=\"0\" cellspacing=\"0\"  height=\"40px;\"><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr><tr align=\"center\"><td style=\"border: solid #8D8D8D 0px;\"> </td></tr></table>";
                            sb +="</td>";
                        }
                    }
                }

                sb +="</tr>";
                sb +="</table></td>";

                sb +="</tr>";
            }  
            
            sb += "</table>";

           document.getElementById("<%=divMain.ClientID%>").innerHTML=sb;
        }

        //获取相差日期数
        function getDays(strDateStart,strDateEnd){
            var strSeparator = "-"; //日期分隔符
            var oDate1;
            var oDate2;
            var iDays;
            oDate1= strDateStart.split(strSeparator);
            oDate2= strDateEnd.split(strSeparator);
            var strDateS = new Date(oDate1[0], oDate1[1]-1, oDate1[2]);
            var strDateE = new Date(oDate2[0], oDate2[1]-1, oDate2[2]);
            iDays = parseInt(Math.abs(strDateS - strDateE ) / 1000 / 60 / 60 /24)//把相差的毫秒数转换为天数 
            return iDays ;
         }

       //计算宽度值  乘法
        function accMul(arg1,arg2) 
        { 
            var m=0,s1=arg1.toString(),s2=arg2.toString(); 
            try{m+=s1.split(".")[1].length}catch(e){} 
            try{m+=s2.split(".")[1].length}catch(e){} 
            return Number(s1.replace(".",""))*Number(s2.replace(".",""))/Math.pow(10,m) 
        } 
        //计算宽度值  加法
        function accAdd(arg1,arg2){ 
            var r1,r2,m; 
            try{r1=arg1.toString().split(".")[1].length}catch(e){r1=0} 
            try{r2=arg2.toString().split(".")[1].length}catch(e){r2=0} 
            m=Math.pow(10,Math.max(r1,r2)) 
            return (arg1*m+arg2*m)/m 
            } 
            //给Number类型增加一个add方法，调用起来更加方便。 
            Number.prototype.add = function (arg){ 
            return accAdd(arg,this); 
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

        function SetSalesRoom(obj) {
            //将当前登录人默认为当前房控人员
            document.getElementById("wcthvpInventoryControl").value = obj;
        }

        function SetControlValue() {
            //将选择的酒店、城市、商圈、 房控人员、排序方式 记录到隐藏域中
            document.getElementById("<%=hidSelectHotel.ClientID%>").value = document.getElementById("wctHotel").value;
            document.getElementById("<%=hidSelectCity.ClientID%>").value = document.getElementById("wctCity").value;
            document.getElementById("<%=hidSelectBussiness.ClientID%>").value = document.getElementById("wcthvpTagInfo").value;
            document.getElementById("<%=hidSelectSalesID.ClientID%>").value = document.getElementById("wcthvpInventoryControl").value;

            var select2 = document.all.<%= DropDownList2.ClientID %>;
            var selectvalue = select2.options[select2.selectedIndex].value;
            document.getElementById("<%=HidDdlSelectValue.ClientID%>").value = selectvalue;
        }

        function MarkFullRoom(obj) {
            //批量操作  关房  开房 
            var s = '';
            $('input[name="chkMarkFullRoom"]:checked').each(function () {
                s += $(this).val() + ',';
            });
            if (s == '') {
                alert("你还没有选择任何内容！");
                return false;
            } else {
//                s = s.substring(0, s.length - 1);
//                var strs= new Array();
//                strs=s.split(",");
//                var sDate = new Date();
//                sDate = sDate.toLocaleDateString();
//                for (i=0;i<strs.length ;i++ )
//                {
//                    var eDate = new Date(strs[i].replace(/\-/g, "\/")).toLocaleDateString();
//                    if(eDate<sDate)
//                    {
//                        alert("开始时间必须大于等于当前时间!");
//                        return false;
//                    }
//                }
debugger;
                s = s.substring(0, s.length - 1);
                var strs= new Array();
                strs=s.split(",");

                var myTime = new Date();
                myTime = myTime.getHours();//当前时分秒
                 if(myTime<4){
                     //超过凌晨4点  正常逻辑
                    var sDate = new Date();
                    var eDate = new Date(document.getElementById("<%=divPlanStartDate.ClientID%>").value.replace(/\-/g, "\/")).toLocaleDateString();
                    sDate.setDate(sDate.getDate()-1);
                    sDate = sDate.toLocaleDateString();//当前年月日 
                    for (i=0;i<strs.length ;i++ )
                    {
                        var eDate = new Date(strs[i].replace(/\-/g, "\/")).toLocaleDateString();
                        if(eDate<sDate)
                        {
                            alert("开始时间必须大于等于当前时间!");
                            return false;
                        }
                    }
                 }else{
                   var sDate = new Date();
                   sDate = sDate.toLocaleDateString();//当前年月日 
                   var eDate = new Date(document.getElementById("<%=divPlanStartDate.ClientID%>").value.replace(/\-/g, "\/")).toLocaleDateString();
                   for (i=0;i<strs.length ;i++ )
                   {
                       var eDate = new Date(strs[i].replace(/\-/g, "\/")).toLocaleDateString();
                       if(eDate<sDate)
                       {
                           alert("开始时间必须大于等于当前时间!");
                           return false;
                       }
                   }
                }

                document.getElementById("<%=HidMarkFullRoom.ClientID%>").value = s;
                document.getElementById("<%=HidCloseOrFullByRoom.ClientID%>").value = obj;
                invokeOpenRemark();
            }
            return true;
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

         function LastOrNextByHotel(obj) {
            //////debugger;
            //上一个 或者 下一个 
            BtnLoadStyle();
            if (obj == '1') {
                document.getElementById("<%=HidScrollValue.ClientID%>").value = '30';
            } else {
                document.getElementById("<%=HidScrollValue.ClientID%>").value = '-30';
            }

            var selectIndex = document.getElementById("<%=HidSelIndex.ClientID%>").value;//当前选中酒店 
            var AllRows = document.getElementById("<%=gridHotelList.ClientID%>").getElementsByTagName("tr");
         
            var Index = accAdd(selectIndex,obj);
            if(Index!="-1" && Index < AllRows.length)
            {
                AllRows[Index].click();
            }else
            {
                if(obj=="-1")
                    alert("无上一个！");
                else    
                    alert("无下一个！");
            }
        }        

        function showDiv(hotelName, priceCode, roomame, roomCode, twoPrice, roomnum, status, isreserve, effectDate) {
            //BtnLoadStyle();
            invokeOpenDiv();
            document.getElementById("<%=lblDivHotelName.ClientID%>").innerHTML = document.getElementById("<%=HidPcode.ClientID%>").value;//酒店名称
            document.getElementById("<%=lblDivRoomType.ClientID%>").innerHTML = roomame;//房型
            document.getElementById("<%=DivlblLinkDetails.ClientID%>").innerHTML =document.getElementById("<%=HidLinkDetails.ClientID%>").value;//酒店联系人 电话 
            document.getElementById("<%=lblDivPrice.ClientID%>").innerHTML = twoPrice;//价格

            if(status=="true")//状态
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
            document.getElementById("<%=txtDivRoomCount.ClientID%>").value = roomnum;//房量 
            
            if(isreserve=="0")//是否是保留房
             {
                document.getElementById("<%=ckDivReserve.ClientID%>").checked = true;
             }
             else
             {
                document.getElementById("<%=ckDivReserve.ClientID%>").checked = false;
             } 
            //批量开始 结束时间
            document.getElementById("<%=divPlanStartDate.ClientID%>").value=effectDate;
            document.getElementById("<%=divPlanEndDate.ClientID%>").value=effectDate;
            
            //备注
            document.getElementById("<%=LmbarRemarkHistory.ClientID%>").innerHTML="";
            document.getElementById("<%=HiddenRoomNum.ClientID%>").value = roomnum;//房量 

            document.getElementById("<%=HiddenEffectDate.ClientID%>").value = effectDate;//生效日期
            document.getElementById("<%=HiddenPrice.ClientID%>").value = twoPrice;//价格
            document.getElementById("<%=HiddenPriceCode.ClientID%>").value = priceCode; //价格代码 
            document.getElementById("<%=HiddenRoomCode.ClientID%>").value = roomCode;//房型代码 
            document.getElementById("<%=HiddenPlanStatus.ClientID%>").value = status;//房型状态
            document.getElementById("<%=HiddenIsReserve.ClientID%>").value = isreserve;//是否是保留房

            $.ajax({
                async : true,
                contentType: "application/json",
                url: "HotelConsultingRoomAllAsyncTable.aspx/GetHistoryRemarkByJson",
                type: "POST",
                dataType: "json",
                data: "{CityID:'" + document.getElementById("<%=HidCityID.ClientID%>").value + "',HotelID:'" + document.getElementById("<%=HidPid.ClientID%>").value + "',PriceCode:'" + document.getElementById("<%=HiddenPriceCode.ClientID%>").value + "',RoomCode:'" + document.getElementById("<%=HiddenRoomCode.ClientID%>").value + "',PlanDTime:'" + document.getElementById("<%=HiddenEffectDate.ClientID%>").value  + "'}",
                success: function (data) {
                //////debugger;
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

        //显示弹出的层
        function invokeOpenDiv() {
            document.getElementById("popupDiv2").style.display = "block";
            //背景
            var bgObj = document.getElementById("bgDiv2");
            bgObj.style.display = "block";
            bgObj.style.width = document.body.offsetWidth + "px";
            bgObj.style.height = screen.height + "px";
            BtnCompleteStyle();
        }

        //隐藏弹出的层
        function invokeCloseDiv() {
            document.getElementById("popupDiv2").style.display = "none";
            document.getElementById("bgDiv2").style.display = "none";
            GetResultFromServer();
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
            document.getElementById("<%=btnEditEXInfo.ClientID%>").click();
        }
        

        function showRoomDiv() {
            $("#MainContent_txtDivRoomCount").removeAttr("disabled", "");
            $("#MainContent_ckDivReserve").removeAttr("disabled", "");
        }

        function closeRoomDiv() {
            document.getElementById("MainContent_txtDivRoomCount").disabled = "true";
            document.getElementById("MainContent_ckDivReserve").disabled = "true";
        }

        function btnAlert()
        {
            alert("1.当天无计划的酒店，全部过滤掉；\n2.当选择房控人员时，过滤房控人员下面所有当天计划全部被关闭，并且关闭人全部是销售人员的酒店；\n3.当天下线的酒店，全部过滤掉；\n4.过滤所有非自签酒店；\n5.过滤HUBS1签约的锦江之星；\n6.过滤永不询房酒店");
        }

        function GetCheckBoxListValue() {
        debugger;
            var myTime = new Date();
            myTime = myTime.getHours();//当前时分秒
            if(myTime<4){
                //超过凌晨4点  正常逻辑
                var sDate = new Date();
                var eDate = new Date(document.getElementById("<%=divPlanStartDate.ClientID%>").value.replace(/\-/g, "\/")).toLocaleDateString();
                sDate.setDate(sDate.getDate()-1);
                sDate = sDate.toLocaleDateString();//当前年月日 
                if(eDate<sDate)
                {
                    alert("开始时间必须大于等于当前时间!");
                    return false;
                }
            }else{
                var sDate = new Date();
                sDate = sDate.toLocaleDateString();//当前年月日 
                var eDate = new Date(document.getElementById("<%=divPlanStartDate.ClientID%>").value.replace(/\-/g, "\/")).toLocaleDateString();
                if(eDate<sDate)
                {
                    alert("开始时间必须大于等于当前时间!");
                    return false;
                }
            }

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
    </script>
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
                                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
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
                            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
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
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("EXLinkMan")+""+Eval("EXLinkTel")+"" %> '
                                                                        ToolTip='<%#Bind("BackPropName") %>' BackColor="#ECECEC" Width="100%"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CITYID" HeaderText="CITYID" Visible="false" />
                                                            <asp:BoundField DataField="isplan" HeaderText="是否有计划" Visible="false" />
                                                            <asp:BoundField DataField="RowOldColor" HeaderText="整行原颜色" />
                                                            <asp:BoundField DataField="ColOldColor" HeaderText="联系人列原颜色" />
                                                            <asp:BoundField DataField="RowNewColor" HeaderText="整行新颜色" />
                                                            <asp:BoundField DataField="ColNewColor" HeaderText="联系人列新颜色" />
                                                            <asp:BoundField DataField="PROP" HeaderText="酒店ID" />
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
                            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
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
                                <%--<asp:Label ID="DivlblContactDetails" runat="server" Text=""></asp:Label>--%>
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
                                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnDivRenewPlan" runat="server" Text="更新计划" CssClass="btn primary"
                                            OnClientClick="return GetCheckBoxListValue()" OnClick="btnDivRenewPlan_Click" />
                                        <input type="button" value="取消" class="btn" onclick="invokeCloseDiv()" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                            <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="Button5" runat="server" Text="确定" CssClass="btn primary" OnClientClick="BtnLoadStyle();"
                                        OnClick="btnCloseOrFullRoom_Click" />
                                    <input type="button" value="取消" class="btn" onclick="invokeCloseRemark()" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
    </div>
    <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidLastHotelIDIndex" runat="server" />
            <%--最后一次进入后台的酒店Index--%>
            <asp:HiddenField ID="HidCityID" runat="server" />
            <%--城市ID--%>
            <asp:HiddenField ID="HidPid" runat="server" />
            <%--酒店ID--%>
            <asp:HiddenField ID="HiddenRoomCode" runat="server" />
            <%--房型代码--%>
            <asp:HiddenField ID="dvscrollX" runat="server" />
            <asp:HiddenField ID="dvscrollY" runat="server" />
            <%--记录滚动条位置--%>
            <asp:HiddenField ID="hidSelectHotel" runat="server" />
            <%--酒店--%>
            <asp:HiddenField ID="hidSelectCity" runat="server" />
            <%--城市--%>
            <asp:HiddenField ID="hidSelectBussiness" runat="server" />
            <%--商圈--%>
            <asp:HiddenField ID="hidSelectSalesID" runat="server" />
            <%--房控人员--%>
            <asp:HiddenField ID="HidDdlSelectValue" runat="server" />
            <%--排序方式--%>
            <asp:HiddenField ID="HidBrowser" runat="server" />
            <%--浏览器格式--%>
            <asp:HiddenField ID="HidPcode" runat="server" />
            <%--酒店名称--%>
            <asp:HiddenField ID="HiddenPrice" runat="server" />
            <%--价格--%>
            <asp:HiddenField ID="HiddenPriceCode" runat="server" />
            <%--价格代码--%>
            <asp:HiddenField ID="HiddenEffectDate" runat="server" />
            <%--生效日期--%>
            <asp:HiddenField ID="HiddenIsReserve" runat="server" />
            <%--是否是保留房--%>
            <asp:HiddenField ID="HiddenPlanStatus" runat="server" />
            <%--房型状态--%>
            <asp:HiddenField ID="HiddenRoomName" runat="server" />
            <%--房型名称--%>
            <asp:HiddenField ID="HiddenRoomNum" runat="server" />
            <%--房量--%>
            <asp:HiddenField ID="HidSelIndexOld" runat="server" />
            <%--上一次选中的酒店的  索引 --%>
            <asp:HiddenField ID="HidSelIndex" runat="server" />
            <%--选择酒店的  索引 --%>
            <asp:HiddenField ID="HidMarkFullRoom" runat="server" />
            <%--起止日期--%>
            <asp:HiddenField ID="HidAlertContactDetails" runat="server" />
            <%--LM联系人--%>
            <asp:HiddenField ID="HidHotelEXLinkMan" runat="server" />
            <%--酒店执行联系人--%>
            <asp:HiddenField ID="HidHotelEXLinkTel" runat="server" />
            <%--酒店执行电话--%>
            <asp:HiddenField ID="HidHotelEXLinkRemark" runat="server" />
            <%--酒店执行备注--%>
            <asp:HiddenField ID="HidCloseOrFullByRoom" runat="server" />
            <%--关房CloseRoom  开房OpenRoom   满房 FullRoom --%>
            <asp:HiddenField ID="HidScrollValue" runat="server" />
            <%--滚动条位置 --%>
            <asp:HiddenField ID="HidLastOrNextByHotel" runat="server" />
            <%--上一个Or下一个 --%>
            <asp:HiddenField ID="HidIsAsync" runat="server" />
            <%--是否是第一次请求 --%>
            <asp:HiddenField ID="HidJudgeLast" runat="server" />
            <%--需要加载上一个酒店  --%>
            <asp:HiddenField ID="HidJudgeNext" runat="server" />
            <%--需要加载下一个酒店  --%>
            <asp:HiddenField ID="HidLastHotelSelectID" runat="server" />
            <%--上一个酒店ID --%>
            <asp:HiddenField ID="HidNextHotelSelectID" runat="server" />
            <%--下一个酒店ID --%>
            <asp:HiddenField ID="HidHotelRoomListLMBARCode" runat="server" />
            <%--当前酒店的Lmbar房型Code --%>
            <asp:HiddenField ID="HidHotelRoomListLMBAR" runat="server" />
            <%--当前酒店的Lmbar房型 --%>
            <asp:HiddenField ID="HidLastHotelRoomListLMBARCode" runat="server" />
            <%--上一个酒店的Lmbar房型Code --%>
            <asp:HiddenField ID="HidLastHotelRoomListLMBAR" runat="server" />
            <%--上一个酒店的Lmbar房型 --%>
            <asp:HiddenField ID="HidNextHotelRoomListLMBARCode" runat="server" />
            <%--下一个酒店的Lmbar房型Code --%>
            <asp:HiddenField ID="HidNextHotelRoomListLMBAR" runat="server" />
            <%--下一个酒店的Lmbar房型 --%>
            <asp:HiddenField ID="HidHotelRoomListLMBAR2Code" runat="server" />
            <%--当前酒店的Lmbar2房型Code --%>
            <asp:HiddenField ID="HidHotelRoomListLMBAR2" runat="server" />
            <%--当前酒店的Lmbar2房型 --%>
            <asp:HiddenField ID="HidLastHotelRoomListLMBAR2Code" runat="server" />
            <%--上一个酒店的Lmbar2房型Code --%>
            <asp:HiddenField ID="HidLastHotelRoomListLMBAR2" runat="server" />
            <%--上一个酒店的Lmbar2房型 --%>
            <asp:HiddenField ID="HidNextHotelRoomListLMBAR2Code" runat="server" />
            <%--下一个酒店的Lmbar2房型Code --%>
            <asp:HiddenField ID="HidNextHotelRoomListLMBAR2" runat="server" />
            <%--下一个酒店的Lmbar2房型 --%>
            <asp:HiddenField ID="HidHotelPlanListLmbar" runat="server" />
            <%--当前酒店的Lmbar计划 --%>
            <asp:HiddenField ID="HidLastHotelPlanListLmbar" runat="server" />
            <%--上一个酒店的Lmbar计划 --%>
            <asp:HiddenField ID="HidNextHotelPlanListLmbar" runat="server" />
            <%--下一个酒店的Lmbar计划 --%>
            <asp:HiddenField ID="HidHotelPlanListLmbar2" runat="server" />
            <%--当前酒店的Lmbar2计划 --%>
            <asp:HiddenField ID="HidLastHotelPlanListLmbar2" runat="server" />
            <%--上一个酒店的Lmbar2计划 --%>
            <asp:HiddenField ID="HidNextHotelPlanListLmbar2" runat="server" />
            <%--下一个酒店的Lmbar2计划 --%>
            <asp:HiddenField ID="HidDtTime" runat="server" />
            <%--计划时间段 年月日  月日  是否周末 --%>
            <asp:HiddenField ID="HidIsBackstage" runat="server" />
            <%-- 判断是否是后台触发  是否是第一次  1(后台)  0（js）--%>
            <asp:HiddenField ID="HiaAllHotelID" runat="server" />
            <%-- 当前酒店列表 所有的酒店ID--%>
            <asp:HiddenField ID="HidLinkDetails" runat="server" />
            <%--酒店LM联系人--%>
            <asp:HiddenField ID="HidIsLoadFinish" runat="server" />
            <%--异步是否加载完成--%>
            <asp:HiddenField ID="HidIsRefresh" runat="server" />
            <%--是否需要刷新--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <input type="button" id="btnSingleHotel" runat="server" onserverclick="btnSingleHotel_Click"
                style="display: none;" />
            <input type="button" id="BtnLastOrNextHotel" runat="server" onserverclick="BtnLastOrNextHotel_Click"
                style="display: none;" />
            <input type="button" id="BtnHotelSite" runat="server" onserverclick="BtnHotelSite_Click"
                style="display: none;" />
            <input type="button" id="btnClickckHotel" runat="server" onserverclick="btnClickckHotel_Click"
                style="display: none;" />
            <input type="button" id="btnEditEXInfo" runat="server" onserverclick="btnEditEXInfo_Click"
                style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function showA() {
            //debugger;
            $.ajax({
                async: false,
                contentType: "application/json",
                url: "HotelConsultingRoomAllAsyncTable.aspx/GetNextOrLastHotelDetails",
                type: "POST",
                dataType: "json",
                data: "{JudgeLast:'" + document.getElementById("<%=HidJudgeLast.ClientID%>").value + "',JudgeNext:'" + document.getElementById("<%=HidJudgeNext.ClientID%>").value + "',startDate:'" + document.getElementById("<%=planStartDate.ClientID%>").value + "',endDate:'" + document.getElementById("<%=planEndDate.ClientID%>").value + "',LastHotelSelectName:'" + document.getElementById("<%=HidLastHotelSelectID.ClientID%>").value + "',NextHotelSelectName:'" + document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value + "'}",
                success: function (data) {
                    //////debugger;
                    document.getElementById("<%=HidIsLoadFinish.ClientID%>").value = "0";
                    document.getElementById("<%=HidLastHotelSelectID.ClientID%>").value = "";
                    document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value = "";
                    document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value = "";
                    document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value = "";
                    document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value = "";

                    var d = jQuery.parseJSON(data.d);
                    $.each(d, function (i) {
                        //上一个酒店 信息 
                        if (i == "siteLast") {
                            document.getElementById("<%=HidLastHotelSelectID.ClientID%>").value = "";
                            document.getElementById("<%=HidLastHotelSelectID.ClientID%>").value = d["siteLast"].LastHotelID; //酒店ID

                            //取房型
                            var lncode = d["siteLast"].LastHotelRoomListLMBARCode; //上一个酒店的Lmbar房型Code   PRICECODE--ROOMCODE--ROOMNM
                            document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value = "";
                            $.each(lncode, function (j) {
                                document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value = document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value + "{\"PRICECODE\":\"" + lncode[j].PRICECODE + "\",\"ROOMCODE\":\"" + lncode[j].ROOMCODE + "\",\"ROOMNM\":\"" + lncode[j].ROOMNM + "\"}&";
                            });
                            if (document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value;
                                document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value = s.substring(0, s.length - 1);
                            }

                            var lncode2 = d["siteLast"].LastHotelRoomListLMBAR2Code; //上一个酒店的Lmbar2房型Code   PRICECODE--ROOMCODE--ROOMNM
                            document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value = "";
                            $.each(lncode2, function (j) {
                                document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value = document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value + "{\"PRICECODE\":\"" + lncode2[j].PRICECODE + "\",\"ROOMCODE\":\"" + lncode2[j].ROOMCODE + "\",\"ROOMNM\":\"" + lncode2[j].ROOMNM + "\"}&";
                            });
                            if (document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value;
                                document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value = s.substring(0, s.length - 1);
                            }

                            //取计划
                            var lnplan = d["siteLast"].LastHotelPlanListLmbar; //上一个酒店的Lmbar计划 
                            document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value = "";
                            $.each(lnplan, function (j) {
                                document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value = document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value + "{\"ROOMTYPECODE\":\"" + lnplan[j].ROOMTYPECODE + "\",\"EFFECTDATESTRING\":\"" + lnplan[j].EFFECTDATESTRING + "\",\"ROOMNUM\":\"" + lnplan[j].ROOMNUM + "\",\"STATUS\":\"" + lnplan[j].STATUS + "\",\"ISRESERVE\":\"" + lnplan[j].ISRESERVE + "\",\"TWOPRICE\":\"" + lnplan[j].TWOPRICE + "\",\"ISROOMFUL\":\"" + lnplan[j].ISROOMFUL + "\"}&";
                            });
                            if (document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value;
                                document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value = s.substring(0, s.length - 1);
                            }

                            var lnplan2 = d["siteLast"].LastHotelPlanListLmbar2; //上一个酒店的Lmbar2计划 
                            document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value = "";
                            $.each(lnplan2, function (j) {
                                document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value = document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value + "{\"ROOMTYPECODE\":\"" + lnplan2[j].ROOMTYPECODE + "\",\"EFFECTDATESTRING\":\"" + lnplan2[j].EFFECTDATESTRING + "\",\"ROOMNUM\":\"" + lnplan2[j].ROOMNUM + "\",\"STATUS\":\"" + lnplan2[j].STATUS + "\",\"ISRESERVE\":\"" + lnplan2[j].ISRESERVE + "\",\"TWOPRICE\":\"" + lnplan2[j].TWOPRICE + "\",\"ISROOMFUL\":\"" + lnplan2[j].ISROOMFUL + "\"}&";
                            });
                            if (document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value;
                                document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value = s.substring(0, s.length - 1);
                            }
                        }
                        //下一个酒店的所有信息
                        if (i == "siteNext") {
                            document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value = "";
                            document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value = d["siteNext"].NextHotelID; //酒店ID

                            //取房型
                            var lncode = d["siteNext"].NextHotelRoomListLMBARCode; //下一个酒店的Lmbar房型Code   PRICECODE--ROOMCODE--ROOMNM
                            document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value = "";
                            $.each(lncode, function (j) {
                                document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value = document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value + "{\"PRICECODE\":\"" + lncode[j].PRICECODE + "\",\"ROOMCODE\":\"" + lncode[j].ROOMCODE + "\",\"ROOMNM\":\"" + lncode[j].ROOMNM + "\"}&";
                            });
                            if (document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value;
                                document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value = s.substring(0, s.length - 1);
                            }

                            var lncode2 = d["siteNext"].NextHotelRoomListLMBAR2Code; //下一个酒店的Lmbar2房型Code   PRICECODE--ROOMCODE--ROOMNM
                            document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value = "";
                            $.each(lncode2, function (j) {
                                document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value = document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value + "{\"PRICECODE\":\"" + lncode2[j].PRICECODE + "\",\"ROOMCODE\":\"" + lncode2[j].ROOMCODE + "\",\"ROOMNM\":\"" + lncode2[j].ROOMNM + "\"}&";
                            });
                            if (document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value;
                                document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value = s.substring(0, s.length - 1);
                            }

                            //取计划
                            var lnplan = d["siteNext"].NextHotelPlanListLmbar; //下一个酒店的Lmbar计划 
                            document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value = "";
                            $.each(lnplan, function (j) {
                                document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value = document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value + "{\"ROOMTYPECODE\":\"" + lnplan[j].ROOMTYPECODE + "\",\"EFFECTDATESTRING\":\"" + lnplan[j].EFFECTDATESTRING + "\",\"ROOMNUM\":\"" + lnplan[j].ROOMNUM + "\",\"STATUS\":\"" + lnplan[j].STATUS + "\",\"ISRESERVE\":\"" + lnplan[j].ISRESERVE + "\",\"TWOPRICE\":\"" + lnplan[j].TWOPRICE + "\",\"ISROOMFUL\":\"" + lnplan[j].ISROOMFUL + "\"}&";
                            });
                            if (document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value;
                                document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value = s.substring(0, s.length - 1);
                            }

                            var lnplan2 = d["siteNext"].NextHotelPlanListLmbar2; //下一个酒店的Lmbar2计划 
                            document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value = "";
                            $.each(lnplan2, function (j) {
                                document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value = document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value + "{\"ROOMTYPECODE\":\"" + lnplan2[j].ROOMTYPECODE + "\",\"EFFECTDATESTRING\":\"" + lnplan2[j].EFFECTDATESTRING + "\",\"ROOMNUM\":\"" + lnplan2[j].ROOMNUM + "\",\"STATUS\":\"" + lnplan2[j].STATUS + "\",\"ISRESERVE\":\"" + lnplan2[j].ISRESERVE + "\",\"TWOPRICE\":\"" + lnplan2[j].TWOPRICE + "\",\"ISROOMFUL\":\"" + lnplan2[j].ISROOMFUL + "\"}&";
                            });
                            if (document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value;
                                document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value = s.substring(0, s.length - 1);
                            }
                        }
                        //计划时间段
                        if (i == "dTTime") {
                            var strTime = d["dTTime"].dTTime; //时间段
                            document.getElementById("<%=HidDtTime.ClientID%>").value = "";
                            $.each(strTime, function (j) {
                                document.getElementById("<%=HidDtTime.ClientID%>").value = document.getElementById("<%=HidDtTime.ClientID%>").value + "{\"time\":\"" + strTime[j].time + "\",\"timeMD\":\"" + strTime[j].timeMD + "\",\"IsWeek\":\"" + strTime[j].IsWeek + "\"}&";
                            });
                            if (document.getElementById("<%=HidDtTime.ClientID%>").value != "") {
                                var s = document.getElementById("<%=HidDtTime.ClientID%>").value;
                                document.getElementById("<%=HidDtTime.ClientID%>").value = s.substring(0, s.length - 1);
                            }
                        }

                        document.getElementById("<%=HidIsLoadFinish.ClientID%>").value = "1";
                    });
                },
                error: function (json) {
                    //////debugger;
                    document.getElementById("<%=HidIsLoadFinish.ClientID%>").value = "0";
                    document.getElementById("<%=HidLastHotelSelectID.ClientID%>").value = "";
                    document.getElementById("<%=HidLastHotelRoomListLMBARCode.ClientID%>").value = "";
                    document.getElementById("<%=HidLastHotelRoomListLMBAR2Code.ClientID%>").value = "";
                    document.getElementById("<%=HidLastHotelPlanListLmbar.ClientID%>").value = "";
                    document.getElementById("<%=HidLastHotelPlanListLmbar2.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelRoomListLMBARCode.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelRoomListLMBAR2Code.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelPlanListLmbar.ClientID%>").value = "";
                    document.getElementById("<%=HidNextHotelPlanListLmbar2.ClientID%>").value = "";
                }
            });
        }


        function judgeLastOrNext(selectID) {
            //debugger;
            if (selectID != "") {
                var selectIndex = selectID;
                document.getElementById("<%=HidJudgeLast.ClientID%>").value = ""; //上一个 
                document.getElementById("<%=HidJudgeNext.ClientID%>").value = ""; //下一个 
                document.getElementById("<%=HidLastHotelSelectID.ClientID%>").value = "";
                document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value = "";
                var AllRows = document.getElementById("<%=gridHotelList.ClientID%>").getElementsByTagName("tr");
                if (selectIndex == 0) {
                    //只需要判断下一个是否需要查询
                    var addIndex = accAdd(selectIndex, 1);
                    if (AllRows[addIndex].backgroundColor != "#80c0a0" && AllRows[addIndex].backgroundColor != "#ff6666") {
                        document.getElementById("<%=HidJudgeNext.ClientID%>").value = "Next";
                        document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value = AllRows[addIndex].cells[6].innerHTML;
                    }
                }
                else {
                    //判断上一个  下一个  是否需要查询
                    if (selectIndex > 0) {
                        if (AllRows[selectIndex - 1].backgroundColor != "#80c0a0" && AllRows[selectIndex - 1].backgroundColor != "#ff6666") {
                            document.getElementById("<%=HidJudgeLast.ClientID%>").value = "Last";
                            document.getElementById("<%=HidLastHotelSelectID.ClientID%>").value = AllRows[selectIndex - 1].cells[6].innerHTML;
                        }
                    }
                    if (selectIndex < AllRows.length - 1) {
                        var addIndex = accAdd(selectIndex, 1);
                        if (AllRows[addIndex].backgroundColor != "#80c0a0" && AllRows[addIndex].backgroundColor != "#ff6666") {
                            document.getElementById("<%=HidJudgeNext.ClientID%>").value = "Next";
                            document.getElementById("<%=HidNextHotelSelectID.ClientID%>").value = AllRows[addIndex].cells[6].innerHTML;
                        }
                    }
                }
            }
        }

    </script>
</asp:Content>
