<%@ Page Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Data" %>

<%@ Import Namespace="HotelVp.Common.Json" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Response.Clear();
        string pageName = Request.RawUrl.Substring(Request.RawUrl.LastIndexOf("/") + 1);
        //string errorFormat = "{0}\t{1}\t{2}";

      
        string req = Request.QueryString.Get("req");
        string json = string.Empty;
        string callback = string.Empty;
        string nid = string.Empty;
        switch (req)
        {           
            case "U001":///获取计划信息
                Response.ContentType = "application/json";
                string account = Request.QueryString.Get("planid");
                if (string.IsNullOrEmpty(account)) account = UserSession.Current.UserAccount; 
                
                //try
                //{
                //    BPOLib.UserClass user = BPOManagement.GetUserInfo(common.BPOSever, UserSession.Current.LoginAccount, UserSession.Current.LoginPwd,account);
                    
                //    json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                //    json = JsonConvert.SerializeObject();
                        
                //    Response.Write(json);
                    
                    
                    
                //}
                //catch (Exception ex)
                //{
                //    LogHelper.Logger.Write(string.Format(errorFormat, pageName, req, ex.Message.ToString()));
                //    json = Newtonsoft.Json.JsonConvert.SerializeObject(new ServerMessage(false, null, new List<String> { objMessage.GetMessage("70004") }));
                //    Response.Write(json);

                //}
                
                Response.ContentType = "application/json";
                string jsondisplay = "";
                try
                {
                    if (Request.QueryString["planid"] != null)
                    {
                        nid = HttpContext.Current.Server.UrlDecode(Request.QueryString["taskid"].ToString().Trim());
                        ExamManagement exam = new ExamManagement();
                       // BatchNodeHelper bHelper = new BatchNodeHelper();
                        exam.GetExamDetailInfo(nid);
                        jsondisplay = JsonConvert.SerializeObject(exam);                        
                    }
                    Response.Write(jsondisplay);
                }
                catch (Exception ex)
                {
                    //jsondisplay = Newtonsoft.Json.JsonConvert.SerializeObject(new ServerMessage(false, null, new List<String> { objMessage.GetMessage("40417") }));
                    Response.Write(jsondisplay);
                    Response.End();
                }     
                
                
                break;         

            //case "U120":///获取登录用户信息
            //    Response.ContentType = "application/json";                
            //    uid = UserSession.Current.LoginAccount;
            //    json = Newtonsoft.Json.JsonConvert.SerializeObject(UserSession.Current);
            //    Response.Write(json);
            //    break;

        }

        Response.End();

    }

   


   
    
</script>