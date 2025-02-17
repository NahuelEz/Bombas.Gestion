using Clover.DbLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Clover.Gestion
{
    public static class Mailing
    {
        /// <summary>
        /// Envia un mensaje de correo electrónico.
        /// </summary>
        /// <param name="mi">Objeto con los datos del correo.</param>
        /// <param name="business">Empresa.</param>
        /// <param name="mailSetting">Ajustes de correo electrónico.</param>
        /// <param name="mailServer">Servidor de correo electrónico.</param>
        /// <returns></returns>
        public static async Task SendMailAsync(MailInformation mi, Business business, MailSetting mailSetting, MailServer mailServer)
        {
            MemoryStream memoryStream1 = null;
            MemoryStream memoryStream2 = null;
            MemoryStream memoryStream3 = null;
            MemoryStream memoryStream4 = null;
            MemoryStream memoryStream5 = null;
            MemoryStream memoryStream6 = null;
            LinkedResource linkedResource1 = null;
            LinkedResource linkedResource2 = null;
            LinkedResource linkedResource3 = null;
            LinkedResource linkedResource4 = null;
            LinkedResource linkedResource5 = null;
            LinkedResource linkedResource6 = null;
            try
            {
                // Preparación de recursos
                if (mi.Message.Contains("{logo}"))
                {
                    if (business.MailLogo != null)
                    {
                        memoryStream1 = new MemoryStream(business.MailLogo);
                        memoryStream1.Position = 0;
                        linkedResource1 = new LinkedResource(memoryStream1, "image/png");
                        linkedResource1.ContentId = Guid.NewGuid().ToString();
                        mi.Message = mi.Message.Replace("{logo}", $"<img src =\"cid:{linkedResource1.ContentId}\" />");
                    }
                    else
                    {
                        mi.Message = mi.Message.Replace("{logo}", string.Empty);
                    }
                }
                if (mi.Message.Contains("{logo_firma}"))
                {
                    if (mailSetting.SignatureLogo != null)
                    {
                        memoryStream2 = new MemoryStream(mailSetting.SignatureLogo);
                        memoryStream2.Position = 0;
                        linkedResource2 = new LinkedResource(memoryStream2, "image/png");
                        linkedResource2.ContentId = Guid.NewGuid().ToString();
                        mi.Message = mi.Message.Replace("{logo_firma}", $"<img src =\"cid:{linkedResource2.ContentId}\" />");
                    }
                    else
                    {
                        mi.Message = mi.Message.Replace("{logo_firma}", string.Empty);
                    }
                }
                // Redes sociales.
                if (mi.Message.Contains("{redes}"))
                {
                    string socialMediaHtml = string.Empty;
                    if (business.SocialMediaLogo1 != null)
                    {
                        memoryStream3 = new MemoryStream(business.SocialMediaLogo1);
                        memoryStream3.Position = 0;
                        linkedResource3 = new LinkedResource(memoryStream3, "image/png");
                        linkedResource3.ContentId = Guid.NewGuid().ToString();
                        socialMediaHtml += $"<a href=\"{business.SocialMediaLink1}\" target=\"_blank\"><img src=\"cid:{linkedResource3.ContentId}\" width=\"51\" height=\"51\" hspace=\"5\" border=\"0\" /></a>";
                    }
                    if (business.SocialMediaLogo2 != null)
                    {
                        memoryStream4 = new MemoryStream(business.SocialMediaLogo2);
                        memoryStream4.Position = 0;
                        linkedResource4 = new LinkedResource(memoryStream4, "image/png");
                        linkedResource4.ContentId = Guid.NewGuid().ToString();
                        socialMediaHtml += $"<a href=\"{business.SocialMediaLink2}\" target=\"_blank\"><img src=\"cid:{linkedResource4.ContentId}\" width=\"51\" height=\"51\" hspace=\"5\" border=\"0\" /></a>";
                    }
                    if (business.SocialMediaLogo3 != null)
                    {
                        memoryStream5 = new MemoryStream(business.SocialMediaLogo3);
                        memoryStream5.Position = 0;
                        linkedResource5 = new LinkedResource(memoryStream5, "image/png");
                        linkedResource5.ContentId = Guid.NewGuid().ToString();
                        socialMediaHtml += $"<a href=\"{business.SocialMediaLink3}\" target=\"_blank\"><img src=\"cid:{linkedResource5.ContentId}\" width=\"51\" height=\"51\" hspace=\"5\" border=\"0\" /></a>";
                    }
                    if (business.SocialMediaLogo4 != null)
                    {
                        memoryStream6 = new MemoryStream(business.SocialMediaLogo4);
                        memoryStream6.Position = 0;
                        linkedResource6 = new LinkedResource(memoryStream6, "image/png");
                        linkedResource6.ContentId = Guid.NewGuid().ToString();
                        socialMediaHtml += $"<a href=\"{business.SocialMediaLink4}\" target=\"_blank\"><img src=\"cid:{linkedResource6.ContentId}\" width=\"51\" height=\"51\" hspace=\"5\" border=\"0\" /></a>";
                    }
                    mi.Message = mi.Message.Replace("{redes}", socialMediaHtml);
                }
                // Envia correo electrónico.
                using (var smtpServer = new SmtpClient()
                {
                    Host = mailServer.SmtpServer,
                    Port = mailServer.SmtpServerPort,
                    Credentials = new System.Net.NetworkCredential(mailServer.EmailAddress, mailServer.EmailPassword),
                    EnableSsl = mailServer.SmtpServerEnableSsl
                })
                using (var mailMessage = new MailMessage())
                using (var view = AlternateView.CreateAlternateViewFromString(mi.Message, null, "text/html"))
                using (var pdfAttachmentStream = new MemoryStream())
                {
                    // Carga adjunto pdf en stream.
                    using (var fs = new FileStream(mi.PdfAttachment, FileMode.Open))
                    {
                        await fs.CopyToAsync(pdfAttachmentStream);
                        pdfAttachmentStream.Position = 0;
                    }
                    // Agrega los recursos a la vista.
                    view.LinkedResources.AddIfNotNull(linkedResource1);
                    view.LinkedResources.AddIfNotNull(linkedResource2);
                    view.LinkedResources.AddIfNotNull(linkedResource3);
                    view.LinkedResources.AddIfNotNull(linkedResource4);
                    view.LinkedResources.AddIfNotNull(linkedResource5);
                    view.LinkedResources.AddIfNotNull(linkedResource6);
                    // Completa campos principales
                    mailMessage.From = new MailAddress(mi.FromAddress);
                    mailMessage.To.AddRange(mi.ToAddress);
                    mailMessage.CC.AddRange(mi.CCAddress);
                    mailMessage.Attachments.AddRange(mi.AuxAttachments);
                    // Agrega adjunto principal
                    mailMessage.Attachments.Add(new Attachment(pdfAttachmentStream, new FileInfo(mi.PdfAttachment).Name, "application/octet-stream"));
                    mailMessage.Subject = mi.Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.AlternateViews.Add(view);
                    await smtpServer.SendMailAsync(mailMessage);
                }
            }
            finally
            {
                // Libera recursos utilizados.
                linkedResource1.SafeDispose();
                linkedResource2.SafeDispose();
                linkedResource3.SafeDispose();
                linkedResource4.SafeDispose();
                linkedResource5.SafeDispose();
                linkedResource6.SafeDispose();
                memoryStream1.SafeDispose();
                memoryStream2.SafeDispose();
                memoryStream3.SafeDispose();
                memoryStream4.SafeDispose();
                memoryStream5.SafeDispose();
                memoryStream6.SafeDispose();
            }
        }
    }

    public struct MailInformation
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public string FromAddress { get; set; }
        public string[] ToAddress { get; set; }
        public string[] CCAddress { get; set; }
        public string PdfAttachment { get; set; }
        public string[] AuxAttachments { get; set; }
    }

    public static class MailExtensions
    {
        public static void AddRange(this MailAddressCollection collection, IEnumerable<string> items)
        {
            foreach (string item in items)
            {
                collection.Add(item);
            }
        }
        public static void AddRange(this AttachmentCollection collection, IEnumerable<string> items)
        {
            foreach (string item in items)
            {
                collection.Add(new Attachment(item));
            }
        }
        public static void SafeDispose(this MemoryStream ms)
        {
            if (ms != null)
            {
                ms.Dispose();
            }
        }
        public static void SafeDispose(this LinkedResource lr)
        {
            if (lr != null)
            {
                lr.Dispose();
            }
        }
        public static void AddIfNotNull(this LinkedResourceCollection collection, LinkedResource resource)
        {
            if (resource != null)
            {
                collection.Add(resource);
            }
        }
    }
}