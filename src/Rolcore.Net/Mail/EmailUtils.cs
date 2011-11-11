using System;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;


namespace Rolcore.Net.Mail
{
    /// <summary>
    /// Utilities that simplify email related tasks.
    /// </summary>
    public sealed class EmailUtils
    {
        private static Regex CreateLenientEmailAddressMatcher()
        {
            return new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
        }

        private static Regex CreateStrictEmailAddressMatcher()
        {
            return new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+"
                + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                + @"[a-zA-Z]{2,}))$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Validates an email address according to lenient rules.
        /// </summary>
        /// <param name="email">The email address to validate</param>
        /// <returns>True to indicate the email address is valid.</returns>
        public static bool LenientIsEmailAddress(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false; // no need to do all that work

            return CreateLenientEmailAddressMatcher().IsMatch(email);
        }

        /// <summary>
        /// Validates an email address according to strict rules.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>True to indicate the email address is valid.</returns>
        public static bool StrictIsEmailAddress(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                return false; // no need to do all that work

            return CreateStrictEmailAddressMatcher().IsMatch(email);
        }

        /// <summary>
        /// Creates and configures a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="to">The value for <see cref="MailMessage.To"/></param>
        /// <param name="bcc">The value for <see cref="MailMessage.Bcc"/></param>
        /// <param name="from">The value for <see cref="MailMessage.From"/></param>
        /// <param name="subject">The value for <see cref="MailMessage.Subject"/></param>
        /// <param name="body">The value for <see cref="MailMessage.Body"/></param>
        /// <param name="mailFormat">The value for <see cref="MailMessage.BodyFormat"/></param>
        /// <returns>A configured <see cref="MailMessage"/>.</returns>
        public static MailMessage CreateMessage(string to, string cc, string bcc, string from, string subject, string body, bool isBodyHtml)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(to), "to is null or empty.");
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(from), "from is null or empty.");
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(subject), "subject is null or empty.");
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(body), "body is null or empty.");

            MailMessage message = new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = isBodyHtml,
                BodyEncoding = Encoding.ASCII
            };

            if (!string.IsNullOrEmpty(cc))
                message.CC.Add(cc);
            if (!string.IsNullOrEmpty(bcc))
                message.Bcc.Add(bcc);

            return message;
        }

        /// <summary>
        /// Sends the given message via email using the given SMTP server.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="smtpServer">The SMTP server to send the message through.</param>
        public static void SendEmail(MailMessage message, string smtpServer = null)
        {
            Contract.Requires<ArgumentException>(message != null, "message is null.");

            using (SmtpClient smtpClient = new SmtpClient())
            {
                if (smtpClient.Host != "offline")
                {
                    if (!string.IsNullOrEmpty(smtpServer))
                        smtpClient.Host = smtpServer;
                    smtpClient.Send(message);
                }
            }
        }
    }
}
