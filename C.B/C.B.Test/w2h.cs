using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace C.B.Test {

    public class W2h {

        public static void ConvertWord () {
            var filePath = @"E:\bak-desktop\LED多媒体信息发布平台方案.docx";
            using (FileStream stream = new FileStream (filePath, FileMode.Open, FileAccess.Read)) {
                var content = NpoiDocTest (stream);

                var html = $"<!DOCTYPE html><html><head><meta charset=\"utf-8\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" /></head><body>{content}</body></html>";
                // System.Console.WriteLine (html);

                var htmlFilePath = $"E:\\bak-desktop\\word-{DateTime.Now.ToOADate()}.html";
                System.IO.StreamWriter sw = new System.IO.StreamWriter (htmlFilePath, false, Encoding.Default);
                sw.Write (html);
                sw.Close ();
            }
        }
        public static string NpoiDocTest (Stream stream) {
            var myDocx = new NPOI.XWPF.UserModel.XWPFDocument (stream); //打开07（.docx）以上的版本的文档

            var picturesList = myDocx.AllPictures;

            var picInfoList = new List<PicInfo> ();
            foreach (var pictures in picturesList) {
                var pData = pictures.Data;
                var picType = pictures.GetPictureType ();

                var picInfo = new PicInfo { Id = pictures.GetPackageRelationship ().Id };
                switch (picType) {
                    case 6:
                        picInfo.PicType = "png";
                        break;
                    default:
                        picInfo.PicType = "jpeg";
                        break;
                }

                picInfo.Base64 = $"data:image/{picInfo.PicType};base64,{Convert.ToBase64String(pData)}";
                //先把pData传阿里云得到url  如果有其他方式传改这里 或者转base64
                picInfo.Url = "";
                picInfoList.Add (picInfo);
            }

            var sb = new StringBuilder ();

            foreach (var para in myDocx.BodyElements) {
                sb.Append ("<p>");
                switch (para.ElementType) {
                    case NPOI.XWPF.UserModel.BodyElementType.PARAGRAPH:
                        {
                            var paragraph = (NPOI.XWPF.UserModel.XWPFParagraph) para;
                            var runs = paragraph.Runs;
                            foreach (var run in runs) {
                                var ctr = run.GetCTR ();
                                var drawingList = ctr.GetDrawingList ();
                                foreach (var drawing in drawingList) {
                                    var a = drawing.GetInlineList ();
                                    foreach (var a1 in a) {
                                        var anyList = a1.graphic.graphicData.Any;

                                        foreach (var any1 in anyList) {
                                            var pictures = picInfoList
                                                .FirstOrDefault (x =>
                                                    any1.IndexOf ("\"" + x.Id + "\"", StringComparison.Ordinal) > -1);
                                            if (pictures != null)
                                                sb.Append (!string.IsNullOrWhiteSpace (pictures.Url) ?
                                                    $@"<img src=""{pictures.Url}"" />" :
                                                    $@"<img src=""{pictures.Base64}"" />");
                                        }
                                    }
                                }

                                var textList = ctr.GetTList ();
                                foreach (var text in textList) {
                                    sb.Append (
                                        $"<span style=\"");
                                    if (!string.IsNullOrWhiteSpace (ctr.rPr?.color?.val)) {
                                        sb.Append (
                                            $"color:#{ctr.rPr.color.val};");
                                    }

                                    if (ctr?.rPr?.sz != null) {
                                        sb.Append (
                                            $"font-size:{ctr.rPr.sz.val}px;");
                                    }

                                    if (!string.IsNullOrWhiteSpace (ctr.rPr?.rFonts?.cs)) {
                                        sb.Append (
                                            $"font-family:{ctr.rPr.rFonts.cs};");
                                    }

                                    sb.Append (
                                        $"\">");

                                    sb.Append (text.Value);
                                    sb.Append ("</span>");
                                }
                            }

                            break;
                        }

                    case NPOI.XWPF.UserModel.BodyElementType.TABLE:
                        //表格下次再写
                        break;
                }

                sb.Append ("</p>");
            }

            return sb.ToString ();
        }

        /*
        private string GetPathByDocToHTML(string strFile)
        {
            if (string.IsNullOrEmpty(strFile))
            {
                return "0";//没有文件
            }

            Microsoft.Office.Interop.Word.ApplicationClass word = new Microsoft.Office.Interop.Word.ApplicationClass();
            Type wordType = word.GetType();
            Microsoft.Office.Interop.Word.Documents docs = word.Documents;

            // 打开文件  
            Type docsType = docs.GetType();

            object fileName = strFile;

            Microsoft.Office.Interop.Word.Document doc = (Microsoft.Office.Interop.Word.Document)docsType.InvokeMember("Open",
            System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { fileName, true, true });

            // 转换格式，另存为html  
            Type docType = doc.GetType();
            //给文件重新起名
            string filename = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString();

            string strFileFolder = "/html/";
            DateTime dt = DateTime.Now;
            //以yyyymmdd形式生成子文件夹名
            string strFileSubFolder = dt.Year.ToString();
            strFileSubFolder += (dt.Month < 10) ? ("0" + dt.Month.ToString()) : dt.Month.ToString();
            strFileSubFolder += (dt.Day < 10) ? ("0" + dt.Day.ToString()) : dt.Day.ToString();
            string strFilePath = strFileFolder + strFileSubFolder + "/";
            // 判断指定目录下是否存在文件夹，如果不存在，则创建 
            if (!Directory.Exists(Server.MapPath(strFilePath)))
            {
                // 创建up文件夹 
                Directory.CreateDirectory(Server.MapPath(strFilePath));
            }

            //被转换的html文档保存的位置 
            // HttpContext.Current.Server.MapPath("html" + strFileSubFolder + filename + ".html")
            string ConfigPath = Server.MapPath(strFilePath + filename + ".html");
            object saveFileName = ConfigPath;

            /*下面是Microsoft Word 9 Object Library的写法，如果是10，可能写成： 
              * docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, 
              * null, doc, new object[]{saveFileName, Word.WdSaveFormat.wdFormatFilteredHTML}); 
              * 其它格式： 
              * wdFormatHTML 
              * wdFormatDocument 
              * wdFormatDOSText 
              * wdFormatDOSTextLineBreaks 
              * wdFormatEncodedText 
              * wdFormatRTF 
              * wdFormatTemplate 
              * wdFormatText 
              * wdFormatTextLineBreaks 
              * wdFormatUnicodeText 
            * /
            docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod,
            null, doc, new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatFilteredHTML });

            //docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod,
            //  null, doc, new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatFilteredHTML }); 

            //关闭文档  
            docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod,
            null, doc, new object[] { null, null, null });

            // 退出 Word  
            wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
            //转到新生成的页面  
            //return ("/" + filename + ".html");

            //转化HTML页面统一编码格式
            TransHTMLEncoding(ConfigPath);

            return (strFilePath + filename + ".html");
        }
         
        private void TransHTMLEncoding(string strFilePath)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(strFilePath, Encoding.GetEncoding(0));
                string html = sr.ReadToEnd();
                sr.Close();
                html = System.Text.RegularExpressions.Regex.Replace(html, @"<meta[^>]*>", "<meta http-equiv=Content-Type content='text/html; charset=gb2312'>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(strFilePath, false, Encoding.Default);

                sw.Write(html);
                sw.Close();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('" + ex.Message + "')</script>");
            }
        }*/

    }

    public class PicInfo {
        public string Id { get; set; }
        public string Url { get; set; }
        public string PicType { get; set; }
        public string Base64 { get; set; }
    }

}