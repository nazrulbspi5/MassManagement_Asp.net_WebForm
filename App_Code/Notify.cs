using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for Common
/// </summary>
public class Notify
{
    public static string SendEmail(string receiverEmail, string subject, string body)
    {
        string ret = string.Empty;
        try
        {
            //MailMessage o = new MailMessage("nazrul.bspi@hotmail.com", to, subject, body);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("nazrul.bspi@hotmail.com", "Green House");
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = body;
            mail.To.Add(receiverEmail);
            NetworkCredential netCred = new NetworkCredential("nazrul.bspi@hotmail.com", "Nazrulbspi5");
            SmtpClient smtpobj = new SmtpClient("smtp.office365.com", 587);
            //SmtpClient smtpobj = new SmtpClient("smtp.live.com", 587);
            smtpobj.EnableSsl = true;

            smtpobj.Credentials = netCred;
            smtpobj.Send(mail);

            //ret = "Sending done! " + DateTime.Now.ToString();
            return ret;
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

    public static void MailSendForReg(string receiverEmail, string subject, String body)
    {
        MailMessage o = new MailMessage("nazrul.bspi@hotmail.com", receiverEmail, subject, body);

        NetworkCredential netCred = new NetworkCredential("nazrul.bspi@hotmail.com", "Nazrulbspi5");
        SmtpClient smtpobj = new SmtpClient("smtp.live.com", 587);
        smtpobj.EnableSsl = true;
        o.IsBodyHtml = true;
        smtpobj.Credentials = netCred;
        smtpobj.Send(o);

    }
    public static bool SendSimpleMail(string emailTo, string emailSubject, string emailBody)
    {
        MailMessage objMessage;
        SmtpClient objClient;
        try
        {
            DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ConfigID, DisplayName, DisplayEmail, ReplyToEmail, SMTPServer, Port, SSL, Authentication, UserName, Password, IsEmailSent FROM EmailConfig where ConfigID='1'");
            if (dt.Rows.Count == 0) return false;
            MailAddress From = new MailAddress(dt.Rows[0]["DisplayEmail"].ToString(), dt.Rows[0]["DisplayName"].ToString());
            MailAddress To = new MailAddress(emailTo);
            objMessage = new MailMessage(From, To);
            objMessage.ReplyToList.Add(dt.Rows[0]["ReplyToEmail"].ToString());
            objMessage.IsBodyHtml = true;
            objMessage.Subject = emailSubject;
            objMessage.Body = emailBody;
            objMessage.Priority = MailPriority.Normal;
            objClient = new SmtpClient();
            objClient.Port = Convert.ToInt32(dt.Rows[0]["Port"].ToString());
            objClient.EnableSsl = Convert.ToBoolean(dt.Rows[0]["SSL"]);
            objClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            objClient.UseDefaultCredentials = Convert.ToBoolean(dt.Rows[0]["Authentication"]);
            objClient.Timeout = 30000;
            objClient.Host = dt.Rows[0]["SMTPServer"].ToString();
            if (Convert.ToBoolean(dt.Rows[0]["Authentication"]))
                objClient.Credentials = new NetworkCredential(dt.Rows[0]["UserName"].ToString(), dt.Rows[0]["Password"].ToString());
            objClient.Send(objMessage);
            return true;           
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write("Sending Failure:<br>" + ex.Message);
            return false;
        }
    }
}