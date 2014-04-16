using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Configuration;

public partial class FileUpLoadControl : System.Web.UI.UserControl
{
    private String folder;
    private String url;
    private int iMaxFileUploadLength = String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxFileUploadLength"]) ? 100 : int.Parse(ConfigurationManager.AppSettings["MaxFileUploadLength"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        folder = Server.MapPath("~/temp");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        lbxFile.Attributes.Add("onchange", "document.getElementById('" + preViewSimple.ClientID + "').style.display='';document.getElementById('" + preViewSimple.ClientID + "').src=this.value.split('\\|')[0]");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (FileUpload.HasFile)
        {
            String guid = Guid.NewGuid().ToString();
            String newFileName = folder + "\\" + guid + Path.GetExtension(FileUpload.FileName);
            url = Page.ResolveUrl("~") + "temp/" + guid + Path.GetExtension(FileUpload.FileName);
            int totalFileSize = Int32.Parse(allFileSize.Value);
            int fileSize = FileUpload.PostedFile.ContentLength;
            iMaxFileUploadLength = 1024 * 1024 * iMaxFileUploadLength;
            //此处也可以限制单个文件的大小
            if (totalFileSize + fileSize > iMaxFileUploadLength)
            {
                messageContent.InnerHtml = "总上传的文件超过了大小设置！";
                return;
            }
            FileUpload.SaveAs(newFileName);
            ListItem item = new ListItem();
            item.Text = FileUpload.FileName;
            item.Value = url + "|" + newFileName;
            for (int i = 0; i < lbxFile.Items.Count; i++)
            {
                if (lbxFile.Items[i].Text.Equals(FileUpload.FileName, StringComparison.InvariantCultureIgnoreCase))
                {
                    messageContent.InnerHtml = "不能添加已经添加过的文件！";
                    return;
                }
            }
            preViewSimple.Style["display"] = "";
            preViewSimple.Src = url;
            totalFileSize += fileSize;
            allFileSize.Value = totalFileSize.ToString();
            lbxFile.Items.Add(item);
            PreViewImage();
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        //对上传的文件进行进一步处理，或者退出弹出窗口等操作
        for (int i = lbxFile.Items.Count - 1; i > -1; i--)
        {
            lbxFile.Items.Remove(lbxFile.Items[i]);
        }
        //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "", @"<script>alert('上传成功！')</script>");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        for (int i = lbxFile.Items.Count - 1; i > -1; i--)
        {
            if (lbxFile.Items[i].Selected)
            {
                String value = lbxFile.Items[i].Value;
                lbxFile.Items.Remove(lbxFile.Items[i]);
                if (File.Exists(value.Split('|')[1]))
                {
                    File.Delete(value.Split('|')[1]);
                }
            }
        }
        PreViewImage();
        preViewSimple.Src = "";
        preViewSimple.Style["display"] = "none";
    }
    private void PreViewImage()
    {
        String p = "";
        for (int i = 0; i < lbxFile.Items.Count; i++)
        {
            p += "<img src='" + lbxFile.Items[i].Value.Split('|')[0] + "' style='width:100px;height:100px'>";
        }
        preViewList.InnerHtml = p;
    }
}