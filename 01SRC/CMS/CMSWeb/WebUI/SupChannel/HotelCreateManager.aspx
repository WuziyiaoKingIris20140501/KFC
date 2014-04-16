<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" MasterPageFile="~/Site.master"  CodeFile="HotelCreateManager.aspx.cs"  Title="酒店基础信息新建管理" Inherits="HotelCreateManager" %>
<%@ Register src="../../UserControls/AutoCptControl.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
<link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
<link href="../../Styles/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />

<script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.tabs.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../Scripts/jquery.autocomplete.js"></script>

<script language="javascript" type="text/javascript">
    function PopupMapArea() {
        var obj = new Object();
        var time = new Date();
        var retunValue = window.showModalDialog("GoogleMapIn.aspx?lng=" + document.getElementById("<%=txtLongitude.ClientID%>").value + "&lat=" + document.getElementById("<%=txtLatitude.ClientID%>").value + "&time=" + time, obj, "dialogWidth=900px;dialogHeight=650px");
        if (retunValue) {
            document.getElementById("<%=retLgVal.ClientID%>").value = retunValue;
            var comName = new Array();
            comName = retunValue.split(","); //字符分割
            if (comName.length > 0)
            {
                document.getElementById("<%=txtLongitude.ClientID%>").value = comName[0];
                document.getElementById("<%=txtLatitude.ClientID%>").value = comName[1];
            }
        }
    }

    function SetControlVal() {
        document.getElementById("<%=hidCityID.ClientID%>").value = document.getElementById("wctCity").value; ;
    }

    function SetPYEvent(lp, sp) {
        document.getElementById("<%=txtHotelPN.ClientID%>").value = lp;
        document.getElementById("<%=txtHotelJP.ClientID%>").value = sp;
    } 

    function ConvertPN() {
        var arg = document.getElementById("<%=txtHotelNM.ClientID%>").value;
        var msg = isChinese(arg);
        if (!msg) {
            document.getElementById("<%=txtHotelPN.ClientID%>").value = "";
            return;
        }
        var str = toPinyinOnly(arg);
        document.getElementById("<%=txtHotelPN.ClientID%>").value = str;
    }

    function isChinese(str) {
        if ("" == str) {
            return false;
        }
        return true;
    }
    //进行汉字转换
    function pinyin(char) {
        var spellArray = new Array();
        var tx = char
        execScript("ascCode=hex(asc(\"" + char + "\"))", "vbscript");
        ascCode = eval("0x" + ascCode);
        if (event.keyCode == 13)
            event.keyCode = 9;
        else if (!char.charCodeAt(0) || char.charCodeAt(0) < 1328) {
            return tx;
        }
        else if (!(ascCode > 0xB0A0 && ascCode < 0xD7FC)) {
            return tx;
        }
        else {
            for (var i = ascCode; !spell[i] && i > 0; )
                i--;
            return spell[i]
        }
    }
    function toPinyinOnly(str) {
        var pStr = ""
        for (var i = 0; i < str.length; i++) {
            if (str.charAt(i) == "\r") {//重要！解决回车输入的Bug！！
                pStr += "\r";
                i++;
            }
            else {
                //pStr += pinyin(str.charAt(i)) + " ";
                pStr += pinyin(str.charAt(i));
            } 
        }
        return pStr;
    }

    //拼音转换编码

    var spell = {
        0xB0A1: "a", 0xB0A3: "ai", 0xB0B0: "an", 0xB0B9: "ang", 0xB0BC: "ao",
        0xB0C5: "ba", 0xB0D7: "bai", 0xB0DF: "ban", 0xB0EE: "bang", 0xB0FA: "bao",
        0xB1AD: "bei", 0xB1BC: "ben", 0xB1C0: "beng", 0xB1C6: "bi", 0xB1DE: "bian",
        0xB1EA: "biao", 0xB1EE: "bie", 0xB1F2: "bin", 0xB1F8: "bing", 0xB2A3: "bo",
        0xB2B8: "bu", 0xB2C1: "ca", 0xB2C2: "cai", 0xB2CD: "can", 0xB2D4: "cang",
        0xB2D9: "cao", 0xB2DE: "ce", 0xB2E3: "ceng", 0xB2E5: "cha", 0xB2F0: "chai",
        0xB2F3: "chan", 0xB2FD: "chang", 0xB3AC: "chao", 0xB3B5: "che", 0xB3BB: "chen",
        0xB3C5: "cheng", 0xB3D4: "chi", 0xB3E4: "chong", 0xB3E9: "chou", 0xB3F5: "chu",
        0xB4A7: "chuai", 0xB4A8: "chuan", 0xB4AF: "chuang", 0xB4B5: "chui", 0xB4BA: "chun",
        0xB4C1: "chuo", 0xB4C3: "ci", 0xB4CF: "cong", 0xB4D5: "cou", 0xB4D6: "cu",
        0xB4DA: "cuan", 0xB4DD: "cui", 0xB4E5: "cun", 0xB4E8: "cuo", 0xB4EE: "da",
        0xB4F4: "dai", 0xB5A2: "dan", 0xB5B1: "dang", 0xB5B6: "dao", 0xB5C2: "de",
        0xB5C5: "deng", 0xB5CC: "di", 0xB5DF: "dian", 0xB5EF: "diao", 0xB5F8: "die",
        0xB6A1: "ding", 0xB6AA: "diu", 0xB6AB: "dong", 0xB6B5: "dou", 0xB6BC: "du",
        0xB6CB: "duan", 0xB6D1: "dui", 0xB6D5: "dun", 0xB6DE: "duo", 0xB6EA: "e",
        0xB6F7: "en", 0xB6F8: "er", 0xB7A2: "fa", 0xB7AA: "fan", 0xB7BB: "fang",
        0xB7C6: "fei", 0xB7D2: "fen", 0xB7E1: "feng", 0xB7F0: "fo", 0xB7F1: "fou",
        0xB7F2: "fu", 0xB8C1: "ga", 0xB8C3: "gai", 0xB8C9: "gan", 0xB8D4: "gang",
        0xB8DD: "gao", 0xB8E7: "ge", 0xB8F8: "gei", 0xB8F9: "gen", 0xB8FB: "geng",
        0xB9A4: "gong", 0xB9B3: "gou", 0xB9BC: "gu", 0xB9CE: "gua", 0xB9D4: "guai",
        0xB9D7: "guan", 0xB9E2: "guang", 0xB9E5: "gui", 0xB9F5: "gun", 0xB9F8: "guo",
        0xB9FE: "ha", 0xBAA1: "hai", 0xBAA8: "han", 0xBABB: "hang", 0xBABE: "hao",
        0xBAC7: "he", 0xBAD9: "hei", 0xBADB: "hen", 0xBADF: "heng", 0xBAE4: "hong",
        0xBAED: "hou", 0xBAF4: "hu", 0xBBA8: "hua", 0xBBB1: "huai", 0xBBB6: "huan",
        0xBBC4: "huang", 0xBBD2: "hui", 0xBBE7: "hun", 0xBBED: "huo", 0xBBF7: "ji",
        0xBCCE: "jia", 0xBCDF: "jian", 0xBDA9: "jiang", 0xBDB6: "jiao", 0xBDD2: "jie",
        0xBDED: "jin", 0xBEA3: "jing", 0xBEBC: "jiong", 0xBEBE: "jiu", 0xBECF: "ju",
        0xBEE8: "juan", 0xBEEF: "jue", 0xBEF9: "jun", 0xBFA6: "ka", 0xBFAA: "kai",
        0xBFAF: "kan", 0xBFB5: "kang", 0xBFBC: "kao", 0xBFC0: "ke", 0xBFCF: "ken",
        0xBFD3: "keng", 0xBFD5: "kong", 0xBFD9: "kou", 0xBFDD: "ku", 0xBFE4: "kua",
        0xBFE9: "kuai", 0xBFED: "kuan", 0xBFEF: "kuang", 0xBFF7: "kui", 0xC0A4: "kun",
        0xC0A8: "kuo", 0xC0AC: "la", 0xC0B3: "lai", 0xC0B6: "lan", 0xC0C5: "lang",
        0xC0CC: "lao", 0xC0D5: "le", 0xC0D7: "lei", 0xC0E2: "leng", 0xC0E5: "li",
        0xC1A9: "lia", 0xC1AA: "lian", 0xC1B8: "liang", 0xC1C3: "liao", 0xC1D0: "lie",
        0xC1D5: "lin", 0xC1E1: "ling", 0xC1EF: "liu", 0xC1FA: "long", 0xC2A5: "lou",
        0xC2AB: "lu", 0xC2BF: "lv", 0xC2CD: "luan", 0xC2D3: "lue", 0xC2D5: "lun",
        0xC2DC: "luo", 0xC2E8: "ma", 0xC2F1: "mai", 0xC2F7: "man", 0xC3A2: "mang",
        0xC3A8: "mao", 0xC3B4: "me", 0xC3B5: "mei", 0xC3C5: "men", 0xC3C8: "meng",
        0xC3D0: "mi", 0xC3DE: "mian", 0xC3E7: "miao", 0xC3EF: "mie", 0xC3F1: "min",
        0xC3F7: "ming", 0xC3FD: "miu", 0xC3FE: "mo", 0xC4B1: "mou", 0xC4B4: "mu",
        0xC4C3: "na", 0xC4CA: "nai", 0xC4CF: "nan", 0xC4D2: "nang", 0xC4D3: "nao",
        0xC4D8: "ne", 0xC4D9: "nei", 0xC4DB: "nen", 0xC4DC: "neng", 0xC4DD: "ni",
        0xC4E8: "nian", 0xC4EF: "niang", 0xC4F1: "niao", 0xC4F3: "nie", 0xC4FA: "nin",
        0xC4FB: "ning", 0xC5A3: "niu", 0xC5A7: "nong", 0xC5AB: "nu", 0xC5AE: "nv",
        0xC5AF: "nuan", 0xC5B0: "nue", 0xC5B2: "nuo", 0xC5B6: "o", 0xC5B7: "ou",
        0xC5BE: "pa", 0xC5C4: "pai", 0xC5CA: "pan", 0xC5D2: "pang", 0xC5D7: "pao",
        0xC5DE: "pei", 0xC5E7: "pen", 0xC5E9: "peng", 0xC5F7: "pi", 0xC6AA: "pian",
        0xC6AE: "piao", 0xC6B2: "pie", 0xC6B4: "pin", 0xC6B9: "ping", 0xC6C2: "po",
        0xC6CB: "pu", 0xC6DA: "qi", 0xC6FE: "qia", 0xC7A3: "qian", 0xC7B9: "qiang",
        0xC7C1: "qiao", 0xC7D0: "qie", 0xC7D5: "qin", 0xC7E0: "qing", 0xC7ED: "qiong",
        0xC7EF: "qiu", 0xC7F7: "qu", 0xC8A6: "quan", 0xC8B1: "que", 0xC8B9: "qun",
        0xC8BB: "ran", 0xC8BF: "rang", 0xC8C4: "rao", 0xC8C7: "re", 0xC8C9: "ren",
        0xC8D3: "reng", 0xC8D5: "ri", 0xC8D6: "rong", 0xC8E0: "rou", 0xC8E3: "ru",
        0xC8ED: "ruan", 0xC8EF: "rui", 0xC8F2: "run", 0xC8F4: "ruo", 0xC8F6: "sa",
        0xC8F9: "sai", 0xC8FD: "san", 0xC9A3: "sang", 0xC9A6: "sao", 0xC9AA: "se",
        0xC9AD: "sen", 0xC9AE: "seng", 0xC9AF: "sha", 0xC9B8: "shai", 0xC9BA: "shan",
        0xC9CA: "shang", 0xC9D2: "shao", 0xC9DD: "she", 0xC9E9: "shen", 0xC9F9: "sheng",
        0xCAA6: "shi", 0xCAD5: "shou", 0xCADF: "shu", 0xCBA2: "shua", 0xCBA4: "shuai",
        0xCBA8: "shuan", 0xCBAA: "shuang", 0xCBAD: "shui", 0xCBB1: "shun", 0xCBB5: "shuo",
        0xCBB9: "si", 0xCBC9: "song", 0xCBD1: "sou", 0xCBD4: "su", 0xCBE1: "suan",
        0xCBE4: "sui", 0xCBEF: "sun", 0xCBF2: "suo", 0xCBFA: "ta", 0xCCA5: "tai",
        0xCCAE: "tan", 0xCCC0: "tang", 0xCCCD: "tao", 0xCCD8: "te", 0xCCD9: "teng",
        0xCCDD: "ti", 0xCCEC: "tian", 0xCCF4: "tiao", 0xCCF9: "tie", 0xCCFC: "ting",
        0xCDA8: "tong", 0xCDB5: "tou", 0xCDB9: "tu", 0xCDC4: "tuan", 0xCDC6: "tui",
        0xCDCC: "tun", 0xCDCF: "tuo", 0xCDDA: "wa", 0xCDE1: "wai", 0xCDE3: "wan",
        0xCDF4: "wang", 0xCDFE: "wei", 0xCEC1: "wen", 0xCECB: "weng", 0xCECE: "wo",
        0xCED7: "wu", 0xCEF4: "xi", 0xCFB9: "xia", 0xCFC6: "xian", 0xCFE0: "xiang",
        0xCFF4: "xiao", 0xD0A8: "xie", 0xD0BD: "xin", 0xD0C7: "xing", 0xD0D6: "xiong",
        0xD0DD: "xiu", 0xD0E6: "xu", 0xD0F9: "xuan", 0xD1A5: "xue", 0xD1AB: "xun",
        0xD1B9: "ya", 0xD1C9: "yan", 0xD1EA: "yang", 0xD1FB: "yao", 0xD2AC: "ye",
        0xD2BB: "yi", 0xD2F0: "yin", 0xD3A2: "ying", 0xD3B4: "yo", 0xD3B5: "yong",
        0xD3C4: "you", 0xD3D9: "yu", 0xD4A7: "yuan", 0xD4BB: "yue", 0xD4C5: "yun",
        0xD4D1: "za", 0xD4D4: "zai", 0xD4DB: "zan", 0xD4DF: "zang", 0xD4E2: "zao",
        0xD4F0: "ze", 0xD4F4: "zei", 0xD4F5: "zen", 0xD4F6: "zeng", 0xD4FA: "zha",
        0xD5AA: "zhai", 0xD5B0: "zhan", 0xD5C1: "zhang", 0xD5D0: "zhao", 0xD5DA: "zhe",
        0xD5E4: "zhen", 0xD5F4: "zheng", 0xD6A5: "zhi", 0xD6D0: "zhong", 0xD6DB: "zhou",
        0xD6E9: "zhu", 0xD7A5: "zhua", 0xD7A7: "zhuai", 0xD7A8: "zhuan", 0xD7AE: "zhuang",
        0xD7B5: "zhui", 0xD7BB: "zhun", 0xD7BD: "zhuo", 0xD7C8: "zi", 0xD7D7: "zong",
        0xD7DE: "zou", 0xD7E2: "zu", 0xD7EA: "zuan", 0xD7EC: "zui", 0xD7F0: "zun", 0xD7F2: "zuo"
    };


    function PopupArea() {
        var cityName = document.getElementById("wctCity").value;
        document.getElementById("<%=dvAddBuss.ClientID%>").innerText = "";
        if (cityName.length == 0) {
            document.getElementById("<%=dvAddBuss.ClientID%>").innerText = "无法添加商圈，请先选择城市！";
            return;
        }
        if (cityName.indexOf("]") >= 0)
        {
            cityName = cityName.substring(1, cityName.indexOf("]"));
        }
        var argList = encodeURI(document.getElementById("<%=hidBussList.ClientID%>").value);
        var time = new Date();
        var retunValue = window.showModalDialog("CBusinessCircle.aspx?city=" + cityName + "&argList=" + argList + "&time=" + time, "", "dialogWidth=800px;dialogHeight=400px");
        if (retunValue != "undefinded" && retunValue != null) {
            document.getElementById("<%=hidBussList.ClientID %>").value = retunValue;
            document.getElementById("<%=dvAreaList.ClientID %>").innerHTML = "";
            var strs = new Array();
            strs = retunValue.split(",");
            for (i = 0; i < strs.length; i++) {
                if (strs[i] != "")
                {
                    document.getElementById("<%=dvAreaList.ClientID %>").innerHTML += "<span style='background:#DBEAF9;height:15px'>" + strs[i] + "</span>&nbsp;&nbsp;";
                }
            }
        }
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
    <div class="frame01" style="margin:8px 14px 5px 14px;">
      <ul>
        <li class="title">酒店基础信息管理</li>
        <li>
               <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                <div id="MessageContent" runat="server" style="color:red;width:800px;"></div>
                <asp:HiddenField ID="retLgVal" runat="server"/>
                <asp:HiddenField ID="hidBussList" runat="server"/>
                <asp:HiddenField ID="hidColsNM" runat="server"/>
                <asp:HiddenField ID="hidLMCount" runat="server"/>
                <asp:HiddenField ID="hidLM2Count" runat="server"/>
                <asp:HiddenField ID="hidHotelID" runat="server"/>
                <asp:HiddenField ID="hidHotelNo" runat="server"/>
                <asp:HiddenField ID="hidSelectedID" runat="server"/>
                <asp:HiddenField ID="hidSalesID" runat="server"/>
                <asp:HiddenField ID="hidCityID" runat="server"/>
                </ContentTemplate>
                </asp:UpdatePanel>
        </li>
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

            <div style="margin:0px 14px 5px 14px;">
                <div id="tabs" style="background:#FFFFFF;border: 0px solid #FFFFFF;">
	                <div id="tabs-1">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                             <table cellspacing="2" cellpadding="1" width="95%">
                             <tr>
                                <td align="right" style="width:11%">酒店中文名：</td>
                                <td><div style="margin-left:3px">
                                        <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                        <asp:TextBox ID="txtHotelNM" runat="server" 
                                        Width="385px" MaxLength="1000" AutoPostBack="True" 
                                        ontextchanged="txtHotelNM_TextChanged"/><font color="red">*</font>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                     </div>
                                </td>
                                <td align="right">上下线状态：</td>
                                <td><asp:DropDownList ID="ddpOnline" CssClass="noborder_inactive" runat="server" Width="155px"/><font color="red">*</font></td>
                            </tr>
                            <tr>
                                <td align="right">酒店英文名：</td>
                                <td><div style="margin-left:3px"><asp:TextBox ID="txtHotelNMEN" runat="server" Width="150px" MaxLength="100"/></div></td>
                                <td align="right">酒店拼音：</td>
                                <td><asp:TextBox ID="txtHotelPN" runat="server" Width="150px" MaxLength="100"/><font color="red">*</font></td>
                            </tr>
                            <tr>
                                <td align="right">房间数：</td>
                                <td><div style="margin-left:3px"><asp:TextBox ID="txtTotalRooms" runat="server" Width="150px" MaxLength="5"/><font color="red">*</font></div></td>
                                <td align="right">酒店简拼：</td>
                                <td><asp:TextBox ID="txtHotelJP" runat="server" Width="150px" MaxLength="100"/><font color="red">*</font></td>
                            </tr>
                            <tr>
                                 <td align="right">
                                     城市：
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <uc1:WebAutoComplete ID="wctCity" runat="server" CTLID="wctCity" CTLLEN="150px" AutoType="city" AutoParent="HotelCreateManager.aspx?Type=city" />
                                                <%--<asp:DropDownList ID="ddpCity" CssClass="noborder_inactive" runat="server" Width="153px"/>--%>
                                            </td>
                                            <td align="left"><font color="red">*</font></td>
                                            <td align="right" style="width:65px;display:none">
                                                行政区：
                                            </td>
                                            <td style="display:none">
                                                <asp:DropDownList ID="ddpAdministration" CssClass="noborder_inactive" runat="server" Width="153px" Enabled="false"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">星级：</td>
                                <td><asp:DropDownList ID="ddpStarRating" CssClass="noborder_inactive" runat="server" Width="155px"/><font color="red">*</font></td>
                            </tr>
                            <tr>
                                <td align="right">地址：</td>
                                <td><div style="margin-left:3px"><asp:TextBox ID="txtAddress" runat="server" Width="385px" MaxLength="150"/><font color="red">*</font></div></td>
                                <td align="right">开业时间：</td>
                                <td><input id="dpOpenDate" class="Wdate" type="text" style="width:150px" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/></td>
                            </tr>
                            <tr>
                                <td align="right">邮编：</td>
                                <td><div style="margin-left:3px"><asp:TextBox ID="txtZip" runat="server" Width="150px" MaxLength="10"/></div></td>
                                <td align="right">最后装修日期：</td>
                                <td><input id="dpRepairDate" class="Wdate" type="text" style="width:150px" onfocus ="WdatePicker({dateFmt:'yyyy-MM-dd'})" runat="server"/></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    经  纬  度：
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtLongitude" runat="server" Width="150px" MaxLength="200" Enabled="false"/>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLatitude" runat="server" Width="150px" MaxLength="200" Enabled="false"/><font color="red">*</font>&nbsp;
                                                <input type="button" id="btnChkMap" class="btn primary" value="查询" onclick="PopupMapArea()" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">最低安全价格：</td>
                                <td><asp:TextBox ID="txtPriceLow" runat="server" Width="150px" MaxLength="4"/></td>
                            </tr>
                            <tr>
                                <td align="right">酒店商圈：</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="dvAreaList" runat="server"></div>
                                            </td>
                                            <td>
                                                <%--<asp:TextBox ID="txtArea" runat="server" Width="300px" MaxLength="200"/>--%>
                                                <input type="button" id="btnAdd" class="btn primary" value="添加" onclick="PopupArea();"/><font color="red">*</font>
                                                
                                            </td>
                                            <td><div id="dvAddBuss" runat="server" style="color:red;"></div></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">常规联系人：</td>
                                <td><asp:TextBox ID="txtContactNameZh" runat="server" Width="150px" MaxLength="100"/><font color="red">*</font></td>
                            </tr>
                            <tr>
                                <td align="right">常规单传真：</td>
                                <td><div style="margin-left:3px"><asp:TextBox ID="txtHotelFax" runat="server" Width="150px" MaxLength="100"/><font color="red">*</font></div></td>
                                <td align="right">联系人电话：</td>
                                <td><asp:TextBox ID="txtContactPhone" runat="server" Width="150px" MaxLength="30"/><font color="red">*</font></td>
                            </tr>
                            <tr>
                                <td align="right">常规单电话：</td>
                                <td><div style="margin-left:3px"><asp:TextBox ID="txtHotelTel" runat="server" Width="150px" MaxLength="40"/><font color="red">*</font></div></td>
                                <td align="right">联系人邮箱：</td>
                                <td><asp:TextBox ID="txtContactEmail" runat="server" Width="150px" MaxLength="100"/><font color="red">*</font></td>
                            </tr>
                            <%--<tr style="display:none">
                                <td align="right">酒店品牌：</td>
                                <td><asp:DropDownList ID="ddpHotelGroup" CssClass="noborder_inactive" runat="server" Width="200px"  Enabled="false"/></td>
                                <td colspan="2" align="left">酒店服务：</td>
                                <td align="right" style="width:120px">酒店设施：</td>
                                <td colspan="2"><asp:TextBox ID="txtHServe" runat="server" TextMode="MultiLine" Width="460px" Height="70px"/></td>
                                <td colspan="2"><div style="margin-left:60px"><asp:TextBox ID="txtHFacility" runat="server" TextMode="MultiLine" Width="460px" Height="70px"/></div></td>
                            </tr>--%>
                            <tr>
                                <td align="right" valign="top">酒店一句话描述：</td>
                                <td align="left"><div style="margin-left:3px"><asp:TextBox ID="txtSimpleDescZh" runat="server" TextMode="MultiLine" Width="380px" Height="70px"/><br /><font color="red">*</font><span style="color:#AAAAAA">&nbsp;最多1000个字符</span></div></td>
                                <td align="right" valign="top">酒店详情：</td>
                                <td align="left"><asp:TextBox ID="txtDescZh" runat="server" TextMode="MultiLine" Width="380px" Height="70px"/></td>
                            </tr>
                            </table>
                             </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                        <div style="display:none">
                            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                            <table width="89%">
                                <tr>
                                    <td align="left">
                                        房型信息：
                                    </td>
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
                                                <asp:BoundField DataField="ROOMNM" HeaderText="房型名称" ><ItemStyle HorizontalAlign="Center" Width="21%"/></asp:BoundField>
                                                <asp:BoundField DataField="ROOMID" HeaderText="房型ID" ><ItemStyle HorizontalAlign="Center" Width="21%"/></asp:BoundField>
                                                <asp:BoundField DataField="AREA" HeaderText="面积" ><ItemStyle HorizontalAlign="Center" Width="21%"/></asp:BoundField>
                                                <asp:BoundField DataField="WLAN" HeaderText="宽带" ><ItemStyle HorizontalAlign="Center" Width="21%"/></asp:BoundField>
                                                <asp:BoundField DataField="STATDIS" HeaderText="状态" ><ItemStyle HorizontalAlign="Center" Width="21%"/></asp:BoundField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="编辑">
                                                  <ItemTemplate>
                                                  <a href="#" onclick="PopupArea('<%# DataBinder.Eval(Container.DataItem, "ROOMID") %>')">编辑</a>
                                                  </ItemTemplate>
                                                  <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
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
                             </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                        <table width="95%">
                            <tr>
                                <td>
                                    <div id="save" style="text-align:left;margin-left:40%">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" OnClientClick="SetControlVal()" onclick="btnSave_Click" />&nbsp;<font color="red">*</font><span style="color:#AAAAAA">为必填字段，请注意填写</span>
                                        <div id="divBtnList" runat="server">
                                    <%--<asp:Button ID="btnFog" runat="server" Width="120px" Height="20px" Text="读取FOG信息" onclick="btnReadFog_Click" />&nbsp;&nbsp;--%>
                                    &nbsp;&nbsp;
                                    <%--<asp:Button ID="btnReset" runat="server" Width="80px" Height="20px" Text="取消编辑" onclick="btnReset_Click" />&nbsp;&nbsp;
                                    <input type="button" id="btnOpenIssue" style="width:100px;height:20px;" value="创建Issue单" onclick="OpenIssuePage();" />--%>
                                    </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        </ContentTemplate>
                        </asp:UpdatePanel>
	                </div>
	              <%--  <div id="tabs-2" style="border: 1px solid #D5D5D5;">
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
                    <asp:Button ID="btnSaveSales" runat="server" Width="80px" Height="20px" Text="保存" OnClientClick="BtnSelectSales()" onclick="btnSaveSales_Click" />
                </div>
                        <div class="frame01" style="margin-top:15px;margin-left:5px">
                    <ul>
                        <li class="title">合同变更历史</li>
                   </ul>
                </div>
                        <div class="frame02" style="margin-left:5px">
                    <asp:GridView ID="gridViewCSSalesList" runat="server" AutoGenerateColumns="False" BackColor="White" AllowPaging="True" 
                                        BorderWidth="2px" CellPadding="4" CellSpacing="1" 
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
                        <asp:Button ID="btnBalAdd" runat="server" Width="120px" Height="20px" Text="保存结算信息" onclick="btnBalAdd_Click" />
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
                                            <asp:Button ID="btnBalSearch" runat="server" Width="80px" Height="20px" Text="查询" onclick="btnBalSearch_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnExportBal" runat="server" Width="80px" Height="20px" Text="导出" onclick="btnExportBal_Click" />
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
                                        BorderWidth="2px" CellPadding="4" CellSpacing="1" 
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
	                </div>--%>
                </div>
            </div>
        </li>
        
      </ul>
    </div>


</asp:Content>