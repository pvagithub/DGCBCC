using RazorEngine;
using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebMVC.Framework.Email
{
    public class EmailNotification
    {
        public long ID { get; set; }

        public string From { get; set; }

        public string FromName { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime? SendTime { get; set; }

        public DateTime CreationTime { get; set; }
    }

    public class WebMVCMailer<T> where T : class
    {
        public string From { get; set; }

        public string FromName { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Bcc { get; set; }

        public string Cc { get; set; }

        public string RazorTemplate { get; set; }

        public T Model { get; set; }

        public virtual void Send(MailDeliveryMode mode = MailDeliveryMode.Asynchronous, byte[] attachFile = null, ContentType contentType = null)
        {
            PrepareTemplate();
            SendDirect(mode, attachFile, contentType);
        }

        private string ParseTemplate()
        {
            return Razor.Parse<T>(this.RazorTemplate, this.Model, typeof(T).AssemblyQualifiedName);
        }

        protected virtual void PrepareTemplate()
        {
            // special case for razor highlight syntax
            this.RazorTemplate = Regex.Replace(this.RazorTemplate, "@inherits ([a-zA-Z0-9.<>]*)", string.Empty);
        }

        protected virtual void SendDirect(MailDeliveryMode mode = MailDeliveryMode.Synchronous, byte[] attachFile = null, ContentType contentType = null)
        {
            try
            {
                // message content
                MailMessage message = new MailMessage
                {
                    Subject = Subject,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true,
                    Body = ParseTemplate()
                };

                //http://server68:9090/browse/LL-1779
                if (attachFile != null)
                {
                    var stream = new MemoryStream(attachFile);
                    if (contentType == null)
                    {
                        contentType = new ContentType();
                    }

                    var attachment = new Attachment(stream, contentType);
                    message.Attachments.Add(attachment);
                }

                // message delivery info : from, to, cc, bcc
                message.From = new MailAddress(From, FromName);
                message.To.Add(this.To);
                if (!string.IsNullOrEmpty(this.Cc))
                    message.CC.Add(this.Cc);
                if (!string.IsNullOrEmpty(this.Bcc))
                    message.Bcc.Add(this.Bcc);

                // config in system.net.mail the host, port and more

                if (ConfigurationManager.AppSettings["ForceSendEmail"] != null &&
                    bool.Parse(ConfigurationManager.AppSettings["ForceSendEmail"]))
                {
                    mode = MailDeliveryMode.Synchronous;
                }

                if (mode == MailDeliveryMode.Synchronous) // will block current request
                {
                    using (SmtpClient smtpMailer = new SmtpClient())
                    {
                        smtpMailer.Send(message);
                    }
                }
                else // for Asynchronous mail, don't block current request, don't need callback function so don't need use sendasycn
                {
                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        // try - catch to don't make scatch for main thread
                        try
                        {
                            using (SmtpClient smtpMailer = new SmtpClient())
                            {
                                smtpMailer.Send(message);
                            }
                        }
                        catch (Exception ex)
                        {
                            //Logger.MailLogger.Error(ex);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    public enum MailDeliveryMode
    {
        Synchronous,
        Asynchronous
    }
}