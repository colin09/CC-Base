namespace C.B.Common.helper {

    public class MailHelper {

        public static void SendMail (string subject, string mailAddress, string content) {
            var mailMessage = SetMailMessage (subject, mailAddress, content);
            Send (mailMessage);
        }

        public static MimeMessage SetMailMessage (string subject, string email, string content, string filepath = null) {
            var message = new MimeMessage ();
            //发信人
            message.From.Add (getFromMailAddress ());
            //收信人
            message.To.Add (new MailboxAddress ("", email));
            //标题
            message.Subject = subject;
            //产生一个支持Html的TextPart
            var body = new TextPart (TextFormat.Html) {
                Text = content
            };
            //先产生一个
            var multipart = new Multipart ("mixed");
            //添加正文内容
            multipart.Add (body);
            if (!string.IsNullOrWhiteSpace (filepath)) {
                //生产一个绝对路径
                //filepath = "Upload//NewsPhoto//readme.txt";
                var absolutePath = Path.Combine (_hostingEnv.WebRootPath, string.Format (filepath));
                //附件
                var attachment = new MimePart () {
                    //读取文件(只能用绝对路径)
                    ContentObject = new ContentObject (File.OpenRead (absolutePath), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition (ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    //文件名字
                    FileName = Path.GetFileName (absolutePath)
                };
                //添加附件
                multipart.Add (attachment);
            }
            //正文内容
            message.Body = multipart;
            return message;
        }

        private static void Send (MimeMessage message) {

            var smtpServerUrl = AppSettingConfig.Get ("EMail.SmtpServerUrl");
            int smtpServerPort = AppSettingConfig.Get ("EMail.SmtpServerPort");
            var smtpServerUserName = AppSettingConfig.Get ("EMail.SmtpServerUserName");
            var smtpServerPassword = AppSettingConfig.Get ("EMail.SmtpServerPassword");

            using (var client = new SmtpClient ()) {
                //连接到Smtp服务器
                client.Connect (smtpServerUrl, smtpServerPort, false);
                //登陆
                client.Authenticate (smtpServerUserName, smtpServerPassword);
                //发送
                client.Send (message);
                //断开
                client.Disconnect (true);
            }
        }

        private static MailboxAddress getFromMailAddress () {

            var fromName = AppSettingConfig.Get ("EMail.ServerMailName");
            var fromAddress = AppSettingConfig.Get ("EMail.ServerMailAddress");

            return new MailboxAddress (fromName, fromAddress);
        }

    }

}