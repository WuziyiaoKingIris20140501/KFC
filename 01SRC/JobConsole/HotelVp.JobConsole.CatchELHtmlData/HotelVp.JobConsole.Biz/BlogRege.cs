using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;

namespace HotelVp.JobConsole.Biz
{
    public class BlogRege
    {
        public void Insert(string title, string content, string linkurl)
        {
            //SqlHelper helper = new SqlHelper();
            //helper.Insert(title, content, categoryID,linkurl);
            Console.WriteLine("Title:" + title + "Content:" + content + "Linkurl:" + linkurl);
        }

        /// <summary>
        /// 通过Url地址获取具体网页内容 发起一个请求获得html内容
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public string SendUrl(string strUrl)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(strUrl);
                WebResponse webResponse = webRequest.GetResponse();
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                string result = reader.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分析Html  解析出里面具体的数据
        /// </summary>
        /// <param name="htmlContent"></param>
        public void AnalysisHtml(string htmlContent)
        {//这个就是我在regulator正则表达式工具中拼接获取到的正则表达式  还有一点请注意就是转义字符的问题
            string strPattern = "<div\\s*class=\"post_item\">\\s*.*\\s*.*\\s*.*\\s*.*\\s*.*\\s*.*\\s*.*\\s*<div\\s*class=\"post_item_body\">\\s*<h3><a\\s*class=\"titlelnk\"\\s*href=\"(?<href>.*)\"\\s*target=\"_blank\">(?<title>.*)</a>.*\\s*<p\\s*class=\"post_item_summary\">\\s*(?<content>.*)\\s*</p>";

            Regex regex = new Regex(strPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);
            if (regex.IsMatch(htmlContent))
            {
                MatchCollection matchCollection = regex.Matches(htmlContent);
                foreach (Match match in matchCollection)
                {
                    string title = match.Groups[2].Value;//获取到的是列表数据的标题
                    string content = match.Groups[3].Value;//获取到的是内容
                    string linkurl = match.Groups[1].Value;//获取到的是链接到的地址
                    Insert(title, content, linkurl);//执行插入到数据库的操作
                }
            }
        }

        /// <summary>
        /// 分析Html  解析出里面具体的数据
        /// </summary>
        /// <param name="htmlContent"></param>
        public void AnalysisHtml_City_1(string htmlContent)
        {
            string strPattern = "<a\\s*method=\"cityABC\"\\s*href=\"(?<href>.*)\"\\s*title=\"(?<title>.*)\">\\s*(?<text>.*)\\s*</a>";
            //"<dd\\s*class=\"citytablist\"\\s*method=\"A-B-C-D\">\\s*<div\\s*>\\s*.*\\s*<ul\\s*>\\s*(?<content>.*)\\s*</ul>";
            Hashtable htCity = new Hashtable();
            Regex regex = new Regex(strPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);
            if (regex.IsMatch(htmlContent))
            {
                MatchCollection matchCollection = regex.Matches(htmlContent);
                foreach (Match match in matchCollection)
                {
                    //string linkurl = match.Groups[1].Value;//获取到的是链接到的地址
                    //Console.WriteLine(linkurl);//执行插入到数据库的操作
                    if (!String.IsNullOrEmpty(match.Groups[3].Value.ToString().Trim()) && !htCity.ContainsKey(match.Groups[3].Value.ToString().Trim()))
                    {
                        htCity.Add(match.Groups[3].Value.ToString().Trim(), match.Groups[1].Value.ToString().Trim());
                    }
                }
            }
        }

        /// <summary>
        /// 分析Html  解析出里面具体的数据
        /// </summary>
        /// <param name="htmlContent"></param>
        public void AnalysisHtml_City_2()
        {
            DataSet dsResult = new DataSet();
            dsResult.Tables.Add(new DataTable());
            dsResult.Tables[0].Columns.Add("CTYNM");
            dsResult.Tables[0].Columns.Add("HTLNM");
            dsResult.Tables[0].Columns.Add("CONTS");
            dsResult.Tables[0].Columns.Add("TWOPE");
            dsResult.Tables[0].Columns.Add("THRPE");
            dsResult.Tables[0].Columns.Add("PERSM");


            Hashtable htCity = Get360CityList();



            //"<div\\s*class=\"post_item\">\\s*.*\\s*.*\\s*.*\\s*.*\\s*.*\\s*.*\\s*.*\\s*<div\\s*class=\"post_item_body\">\\s*<h3><a\\s*class=\"titlelnk\"\\s*href=\"(?<href>.*)\"\\s*target=\"_blank\">(?<title>.*)</a>.*\\s*<p\\s*class=\"post_item_summary\">\\s*(?<content>.*)\\s*</p>";
            string strPattern = "<div\\s*class=\"tuan_extro_info\">\\s*.*\\s*<li>(?<hotelnm>.*)</li>\\s*<li>(?<addresses>.*)</li>\\s*<li\\s*class=\"time\"\\s*beforetoday=\"(?<beforetoday>.*)\"\\s*countdown=\"(?<countdown>.*)\"></li>\\s*.*\\s*.*\\s*<h3><a\\s*ex=\"(?<extent>.*)\"\\s*sitekey=\"(?<sitekey>.*)\"\\s*classno=\"(?<classno>.*)\"\\s*oauth=\"(?<oauth>.*)\"\\s*target=\"(?<target>.*)\"\\s*title=\"(?<target>.*)\"\\s*href=\"(?<href>.*)\">(?<content>.*)</a></h3>\\s*<div\\s*class=\"tuan_meta\">\\s*.*\\s*<li\\s*class=\"price\">\\s*<a\\s*ex=\"(?<extents>.*)\"\\s*sitekey=\"(?<sitekeys>.*)\"\\s*classno=\"(?<classnos>.*)\"\\s*oauth=\"(?<oauths>.*)\"\\s*href=\"(?<hrefs>.*)\"\\s*target=\"_blank\"><strong>(?<twope>.*)</strong></a></li>\\s*<li\\s*class=\"discount\">\\s*<del>(?<thrpe>.*)</del>\\s*\\|\\s*<em>(?<ems>.*)</em>\\s*</li>\\s*.*\\s*\\s*.*\\s*\\s*.*\\s*\\s*.*\\s*<div\\s*class=\"tuan_info\">\\s*.*\\s*<li\\s*class=\"sales\">\\s*<em>(?<persm>.*)</em>\\s*";

            //string strPattern = "<a\\s*city=\"(?<title>.*)\"\\s*href=\"(?<href>.*)\">\\s*(?<text>.*)\\s*</a>";
            Regex regex = new Regex(strPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);
            string baseUrl = "http://tuan.360.cn/{0}/s_%E8%89%BA%E9%BE%99%E9%85%92%E5%BA%97_{1}.html?qtype=valid&qforce=1";
            string strCityID = string.Empty;
            string strCityNM = string.Empty;
            string sendUrl = string.Empty;
            string htmlContent = string.Empty;
            foreach (DictionaryEntry de in htCity)
            {
                strCityID = de.Value.ToString();
                strCityNM = de.Key.ToString();
                Console.WriteLine("抓取EL团酒店-城市ID：" + strCityID + " 城市名称：" + strCityNM);

                for (int i = 1; i < 20; i++)
                {
                    sendUrl = String.Format(baseUrl, strCityID, i.ToString());
                    htmlContent = SendUrl(sendUrl);
                    if (regex.IsMatch(htmlContent))
                    {
                        MatchCollection matchCollection = regex.Matches(htmlContent);
                        foreach (Match match in matchCollection)
                        {
                            if (dsResult.Tables[0].Select("HTLNM='" + match.Groups[1].Value.ToString().Trim() + "'").Length == 0)
                            {
                                DataRow drNew = dsResult.Tables[0].NewRow();
                                drNew["CTYNM"] = strCityNM = de.Key.ToString();
                                drNew["HTLNM"] = match.Groups[1].Value.ToString().Trim();
                                drNew["CONTS"] = match.Groups[11].Value.ToString().Trim();
                                drNew["TWOPE"] = match.Groups[17].Value.ToString().Trim();
                                drNew["THRPE"] = match.Groups[18].Value.ToString().Trim();
                                drNew["PERSM"] = match.Groups[20].Value.ToString().Trim();
                                dsResult.Tables[0].Rows.Add(drNew);
                            }
                        }
                    }
                }
            }

            //ExportExcelData(dsResult);

            ExcelManager exManager = new ExcelManager(@"D:\TFS\HotelVPBackend\CMS\01SRC\JobConsole\Tools\HotelVp.JobConsole.CatchELHtmlData\EL_tuan.xlsx");
            exManager.ExportToExcel(dsResult, "EL_TUAN");
        }


        /// <summary>
        /// 分析Html  解析出里面具体的数据
        /// </summary>
        /// <param name="htmlContent"></param>
        public void AnalysisHtml_City_3(string htmlContent)
        {
            string strPattern = "";

            Regex regex = new Regex(strPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);
            if (regex.IsMatch(htmlContent))
            {
                MatchCollection matchCollection = regex.Matches(htmlContent);
                foreach (Match match in matchCollection)
                {
                    string title = match.Groups[2].Value;//获取到的是列表数据的标题
                    string content = match.Groups[3].Value;//获取到的是内容
                    string linkurl = match.Groups[1].Value;//获取到的是链接到的地址
                    Insert(title, content, linkurl);//执行插入到数据库的操作
                }
            }
        }

        private Hashtable Get360CityList()
        {
            Hashtable htCity = new Hashtable();

            htCity.Add("鞍山", "an_shan");
            htCity.Add("安阳", "an_yang");
            htCity.Add("安庆", "an_qing");
            htCity.Add("澳门", "ao_men");
            htCity.Add("安康", "an_kang");
            htCity.Add("阿克苏", "a_ke_su");
            htCity.Add("北京", "bei_jing");
            htCity.Add("保定", "bao_ding");
            htCity.Add("包头", "bao_tou");
            htCity.Add("蚌埠", "beng_bu");
            htCity.Add("宝鸡", "bao_ji");
            htCity.Add("北海", "bei_hai");
            htCity.Add("滨州", "bin_zhou");
            htCity.Add("本溪", "ben_xi");
            htCity.Add("巴音郭楞", "ba_yin_guo_le");
            htCity.Add("保山", "bao_shan");
            htCity.Add("亳州", "bo_zhou");
            htCity.Add("白山", "bai_shan");
            htCity.Add("百色", "bai_se");
            htCity.Add("白城", "bai_cheng");
            htCity.Add("成都", "cheng_du");
            htCity.Add("重庆", "chong_qing");
            htCity.Add("长沙", "chang_sha");
            htCity.Add("长春", "chang_chun");
            htCity.Add("常州", "chang_zhou");
            htCity.Add("沧州", "cang_zhou");
            htCity.Add("常德", "chang_de");
            htCity.Add("常熟", "chang_shu");
            htCity.Add("潮州", "chao_zhou");
            htCity.Add("巢湖", "chao_hu");
            htCity.Add("郴州", "chen_zhou");
            htCity.Add("滁州", "chu_zhou");
            htCity.Add("承德", "cheng_de");
            htCity.Add("赤峰", "chi_feng");
            htCity.Add("长治", "chang_zhi");
            htCity.Add("池州", "chi_zhou");
            htCity.Add("楚雄", "chu_xiong");
            htCity.Add("昌吉", "chang_ji");
            htCity.Add("从化", "cong_hua");
            htCity.Add("大连", "da_lian");
            htCity.Add("东莞", "dong_guan");
            htCity.Add("东营", "dong_ying");
            htCity.Add("德州", "de_zhou");
            htCity.Add("大庆", "da_qing");
            htCity.Add("德阳", "de_yang");
            htCity.Add("大同", "da_tong");
            htCity.Add("丹东", "dan_dong");
            htCity.Add("丹阳", "dan_yang");
            htCity.Add("大理", "da_li");
            htCity.Add("达州", "da_zhou");
            htCity.Add("东阳", "dong_yang");
            htCity.Add("鄂州", "e_zhou");
            htCity.Add("鄂尔多斯", "e_er_duo_si");
            htCity.Add("峨眉山", "e_mei_shan");
            htCity.Add("福州", "fu_zhou");
            htCity.Add("佛山", "fo_shan");
            htCity.Add("抚顺", "fu_shun");
            htCity.Add("阜阳", "fu_yang");
            htCity.Add("阜新", "fu_xin");
            htCity.Add("抚州", "fu_zhou_");
            htCity.Add("凤凰", "feng_huang");
            htCity.Add("富阳", "fu_yang_1");
            htCity.Add("广州", "guang_zhou");
            htCity.Add("贵阳", "gui_yang");
            htCity.Add("桂林", "gui_lin");
            htCity.Add("赣州", "gan_zhou");
            htCity.Add("贵港", "gui_gang");
            htCity.Add("广元", "guang_yuan");
            htCity.Add("广安", "guang_an");
            htCity.Add("杭州", "hang_zhou");
            htCity.Add("合肥", "he_fei");
            htCity.Add("哈尔滨", "ha_er_bin");
            htCity.Add("海口", "hai_kou");
            htCity.Add("邯郸", "han_dan");
            htCity.Add("惠州", "hui_zhou");
            htCity.Add("呼和浩特", "hu_he_hao_te");
            htCity.Add("湖州", "hu_zhou");
            htCity.Add("淮安", "huai_an");
            htCity.Add("衡阳", "heng_yang");
            htCity.Add("淮南", "huai_nan");
            htCity.Add("衡水", "heng_shui");
            htCity.Add("淮北", "huai_bei");
            htCity.Add("菏泽", "he_ze");
            htCity.Add("黄石", "huang_shi");
            htCity.Add("河源", "he_yuan");
            htCity.Add("葫芦岛", "hu_lu_dao");
            htCity.Add("黄山", "huang_shan");
            htCity.Add("黄冈", "huang_guang");
            htCity.Add("怀化", "huai_hua");
            htCity.Add("汉中", "han_zhong");
            htCity.Add("鹤壁", "he_bi");
            htCity.Add("红河", "hong_he");
            htCity.Add("河池", "he_chi");
            htCity.Add("呼伦贝尔", "hu_lun_bei_er");
            htCity.Add("黑河", "hei_he");
            htCity.Add("鹤岗", "he_gang");
            htCity.Add("花都", "hua_du");
            htCity.Add("海宁", "hai_ning");
            htCity.Add("济南", "ji_nan");
            htCity.Add("济宁", "ji_ning");
            htCity.Add("嘉兴", "jia_xing");
            htCity.Add("江门", "jiang_men");
            htCity.Add("金华", "jin_hua");
            htCity.Add("吉林", "ji_lin");
            htCity.Add("锦州", "jin_zhou");
            htCity.Add("焦作", "jiao_zuo");
            htCity.Add("江阴", "jiang_yin");
            htCity.Add("九江", "jiu_jiang");
            htCity.Add("荆州", "jing_zhou");
            htCity.Add("晋中", "jin_zhong");
            htCity.Add("晋城", "jin_cheng");
            htCity.Add("晋江", "jin_jiang");
            htCity.Add("揭阳", "jie_yang");
            htCity.Add("景德镇", "jing_de_zhen");
            htCity.Add("吉安", "ji__an");
            htCity.Add("佳木斯", "jia_mu_si");
            htCity.Add("荆门", "jing_men");
            htCity.Add("鸡西", "ji_xi");
            htCity.Add("酒泉", "jiu_quan");
            htCity.Add("九寨沟", "jiu_zhai_gou");
            htCity.Add("昆明", "kun_ming");
            htCity.Add("昆山", "kun_shan");
            htCity.Add("开封", "kai_feng");
            htCity.Add("克拉玛依", "ke_la_ma_yi");
            htCity.Add("洛阳", "luo_yang");
            htCity.Add("兰州", "lan_zhou");
            htCity.Add("临沂", "lin_yi");
            htCity.Add("廊坊", "lang_fang");
            htCity.Add("柳州", "liu_zhou");
            htCity.Add("连云港", "lian_yun_gang");
            htCity.Add("临汾", "lin_fen");
            htCity.Add("聊城", "liao_cheng");
            htCity.Add("丽水", "li_shui");
            htCity.Add("龙岩", "long_yan");
            htCity.Add("乐山", "le_shan");
            htCity.Add("莱芜", "lai_wu");
            htCity.Add("临安", "lin_an");
            htCity.Add("丽江", "li_jiang");
            htCity.Add("漯河", "luo_he");
            htCity.Add("泸州", "lu_zhou");
            htCity.Add("吕梁", "lv_liang");
            htCity.Add("辽阳", "liao_yang");
            htCity.Add("娄底", "lou_di");
            htCity.Add("拉萨", "la_sa");
            htCity.Add("凉山", "liang_shan");
            htCity.Add("辽源", "liao_yuan");
            htCity.Add("六安", "liu_an");
            htCity.Add("绵阳", "mian_yang");
            htCity.Add("牡丹江", "mu_dan_jiang");
            htCity.Add("马鞍山", "ma_an_shan");
            htCity.Add("茂名", "mao_ming");
            htCity.Add("梅州", "mei_zhou");
            htCity.Add("眉山", "mei_shan");
            htCity.Add("南京", "nan_jing");
            htCity.Add("宁波", "ning_bo");
            htCity.Add("南昌", "nan_chang");
            htCity.Add("南宁", "nan_ning");
            htCity.Add("南通", "nan_tong");
            htCity.Add("南阳", "nan_yang");
            htCity.Add("宁德", "ning_de");
            htCity.Add("南充", "nan_chong");
            htCity.Add("南平", "nan_ping");
            htCity.Add("内江", "nei_jiang");
            htCity.Add("莆田", "pu_tian");
            htCity.Add("萍乡", "ping_xiang");
            htCity.Add("平顶山", "ping_ding_shan");
            htCity.Add("盘锦", "pan_jin");
            htCity.Add("濮阳", "pu_yang");
            htCity.Add("攀枝花", "pan_zhi_hua");
            htCity.Add("平凉", "ping_liang");
            htCity.Add("平遥", "ping_yao");
            htCity.Add("青岛", "qing_dao");
            htCity.Add("泉州", "quan_zhou");
            htCity.Add("秦皇岛", "qin_huang_dao");
            htCity.Add("齐齐哈尔", "qi_qi_haer");
            htCity.Add("清远", "qing_yuan");
            htCity.Add("衢州", "qu_zhou");
            htCity.Add("曲靖", "qu_jing");
            htCity.Add("钦州", "qin_zhou");
            htCity.Add("全国", "quan_guo");
            htCity.Add("日照", "ri_zhao");
            htCity.Add("上海", "shang_hai");
            htCity.Add("深圳", "shen_zhen");
            htCity.Add("沈阳", "shen_yang");
            htCity.Add("苏州", "su_zhou");
            htCity.Add("石家庄", "shi_jia_zhuang");
            htCity.Add("绍兴", "shao_xing");
            htCity.Add("汕头", "shan_tou");
            htCity.Add("三亚", "san_ya");
            htCity.Add("韶关", "shao_guan");
            htCity.Add("十堰", "shi_yan");
            htCity.Add("宿迁", "su_qian");
            htCity.Add("顺德", "shun_de");
            htCity.Add("上饶", "shang_rao");
            htCity.Add("三明", "san_ming");
            htCity.Add("三门峡", "san_men_xia");
            htCity.Add("商丘", "shang_qiu");
            htCity.Add("汕尾", "shan_wei");
            htCity.Add("四平", "si_ping");
            htCity.Add("朔州", "shuo_zhou");
            htCity.Add("宿州", "su_zhou_");
            htCity.Add("邵阳", "shao_yang");
            htCity.Add("遂宁", "sui_ning");
            htCity.Add("双鸭山", "shuang_ya_shan");
            htCity.Add("随州", "sui_zhou");
            htCity.Add("松原", "song_yuan");
            htCity.Add("石狮", "shi_shi");
            htCity.Add("绥化", "sui_hua");
            htCity.Add("商洛", "shang_luo");
            htCity.Add("天津", "tian_jin");
            htCity.Add("太原", "tai_yuan");
            htCity.Add("唐山", "tang_shan");
            htCity.Add("泰安", "tai_an");
            htCity.Add("泰州", "tai_zhou_2");
            htCity.Add("台州", "tai_zhou_1");
            htCity.Add("铜陵", "tong_ling");
            htCity.Add("铁岭", "tie_ling");
            htCity.Add("塘沽", "tang_gu");
            htCity.Add("通化", "tong_hua");
            htCity.Add("通辽", "tong_liao");
            htCity.Add("天水", "tian_shui");
            htCity.Add("武汉", "wu_han");
            htCity.Add("无锡", "wu_xi");
            htCity.Add("潍坊", "wei_fang");
            htCity.Add("温州", "wen_zhou");
            htCity.Add("威海", "wei_hai");
            htCity.Add("芜湖", "wu_hu");
            htCity.Add("乌鲁木齐", "wu_lu_mu_qi");
            htCity.Add("吴江", "wu_jiang");
            htCity.Add("渭南", "wei_nan");
            htCity.Add("武夷山", "wu_yi_shan");
            htCity.Add("万州", "wan_zhou");
            htCity.Add("梧州", "wu_zhou");
            htCity.Add("西安", "xi_an");
            htCity.Add("厦门", "xia_men");
            htCity.Add("徐州", "xu_zhou");
            htCity.Add("襄阳", "xiang_yang");
            htCity.Add("新乡", "xin_xiang");
            htCity.Add("咸阳", "xian_yang");
            htCity.Add("湘潭", "xiang_tan");
            htCity.Add("西宁", "xi_ning");
            htCity.Add("邢台", "xing_tai");
            htCity.Add("香港", "xiang_gang");
            htCity.Add("信阳", "xin_yang");
            htCity.Add("许昌", "xu_chang");
            htCity.Add("孝感", "xiao_gan");
            htCity.Add("新余", "xin_yu");
            htCity.Add("忻州", "xin_zhou");
            htCity.Add("宣城", "xuan_cheng");
            htCity.Add("咸宁", "xian_ning");
            htCity.Add("湘西", "xiang_xi");
            htCity.Add("扬州", "yang_zhou");
            htCity.Add("烟台", "yan_tai");
            htCity.Add("盐城", "yan_cheng");
            htCity.Add("运城", "yun_cheng");
            htCity.Add("银川", "yin_chuan");
            htCity.Add("宜昌", "yi_chang");
            htCity.Add("岳阳", "yue_yang");
            htCity.Add("义乌", "yi_wu");
            htCity.Add("营口", "ying_kou");
            htCity.Add("阳江", "yang_jiang");
            htCity.Add("云浮", "yun_fu");
            htCity.Add("延吉", "yan_ji");
            htCity.Add("宜春", "yi_chun_shi");
            htCity.Add("玉溪", "yu_xi");
            htCity.Add("玉林", "yu_lin");
            htCity.Add("宜宾", "yi_bin");
            htCity.Add("益阳", "yi_yang");
            htCity.Add("阳泉", "yang_quan");
            htCity.Add("伊春", "yi_chun");
            htCity.Add("榆林", "yu_lin_1");
            htCity.Add("永州", "yong_zhou");
            htCity.Add("伊犁", "yi_li");
            htCity.Add("延安", "yan_an");
            htCity.Add("雅安", "ya_an");
            htCity.Add("阳朔", "yang_shuo");
            htCity.Add("郑州", "zheng_zhou");
            htCity.Add("淄博", "zi_bo");
            htCity.Add("中山", "zhong_shan");
            htCity.Add("珠海", "zhu_hai");
            htCity.Add("镇江", "zhen_jiang");
            htCity.Add("株洲", "zhu_zhou");
            htCity.Add("漳州", "zhang_zhou");
            htCity.Add("肇庆", "zhao_qing");
            htCity.Add("湛江", "zhan_jiang");
            htCity.Add("张家港", "zhang_jia_gang");
            htCity.Add("张家口", "zhang_jia_kou");
            htCity.Add("舟山", "zhou_shan");
            htCity.Add("枣庄", "zao_zhuang");
            htCity.Add("遵义", "zun_yi");
            htCity.Add("驻马店", "zhu_ma_dian");
            htCity.Add("周口", "zhou_kou");
            htCity.Add("自贡", "zi_gong");
            htCity.Add("朝阳", "zhao_yang");
            htCity.Add("资阳", "zi_yang");
            htCity.Add("张家界", "zhang_jia_jie");
            return htCity;
        }

        private void ExportExcelData(DataSet dsResult)
        {
            
        }
    }
}