using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// ToBase64 的摘要说明
/// </summary>
public class ToBase64
{
    public ToBase64()
    { 
    }
    public static string EncodingForFile(string fileName)
    {
        System.IO.FileStream fs = System.IO.File.OpenRead(fileName);
        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);

        /*System.Byte[] b=new System.Byte[fs.Length];
        fs.Read(b,0,Convert.ToInt32(fs.Length));*/


        string base64String = Convert.ToBase64String(br.ReadBytes((int)fs.Length));
        br.Close();
        fs.Close();
        return base64String;
    }
    public static bool SaveDecodingToFile(string base64String, string fileName)
    {
        System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
        System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
        bw.Write(Convert.FromBase64String(base64String));
        bw.Close();
        fs.Close();
        return true;
    }
}
