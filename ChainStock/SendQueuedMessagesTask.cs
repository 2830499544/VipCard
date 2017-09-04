using Chain.BLL;
using Chain.Model;
using Chain.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Xml;

public class SendQueuedMessagesTask : ITask
{
	private int _maxTries = 5;

	public void Execute(XmlNode node)
	{
		XmlAttribute attribute = node.Attributes["maxTries"];
		if (attribute != null && !string.IsNullOrEmpty(attribute.Value))
		{
			this._maxTries = int.Parse(attribute.Value);
		}
		List<string> bcc = new List<string>();
		List<string> cc = new List<string>();
		DataTable dtEmail = new Chain.BLL.EmailLog().GetList(1, " EmailState=0 and EmailCount <" + this._maxTries, " EmailSendTime asc ").Tables[0];
		if (dtEmail.Rows.Count > 0)
		{
			EmailAccount account = new EmailAccount();
			Chain.Model.SysParameter modelParameter = new Chain.BLL.SysParameter().GetModel(1);
			account.Email = modelParameter.EmailAdress;
			account.Username = modelParameter.EmailUserName;
			account.Password = modelParameter.EmailPwd;
			account.Host = modelParameter.EmailSMTP;
			account.Port = modelParameter.EnterpriseEmailPort;
			account.DisplayName = modelParameter.EnterpriseEmailDisplayName;
			account.EnableSSL = modelParameter.EnterpriseEmailEnableSSL;
			account.UseDefaultCredentials = modelParameter.EnterpriseEmailUseDefaultCredentials;
			try
			{
				if (dtEmail.Rows.Count > 0)
				{
					this.SendEmail(dtEmail.Rows[0]["EmailTitle"].ToString(), dtEmail.Rows[0]["EmailContent"].ToString(), new MailAddress(account.Email, account.DisplayName), new MailAddress(dtEmail.Rows[0]["EmailAdress"].ToString(), ""), bcc, cc, account);
					new Chain.BLL.EmailLog().UpdateEmail(int.Parse(dtEmail.Rows[0]["EmailID"].ToString()), 1);
				}
			}
			catch (Exception)
			{
				new Chain.BLL.EmailLog().UpdateEmailCount(int.Parse(dtEmail.Rows[0]["EmailID"].ToString()));
				if (int.Parse(dtEmail.Rows[0]["EmailCount"].ToString()) + 1 > this._maxTries)
				{
					new Chain.BLL.EmailLog().UpdateEmail(int.Parse(dtEmail.Rows[0]["EmailID"].ToString()), 2);
				}
			}
		}
	}

	private void SendEmail(string subject, string body, MailAddress from, MailAddress to, List<string> bcc, List<string> cc, EmailAccount emailAccount)
	{
		MailMessage message = new MailMessage();
		message.From = from;
		message.To.Add(to);
		if (null != bcc)
		{
			foreach (string address in bcc)
			{
				if (address != null)
				{
					if (!string.IsNullOrEmpty(address.Trim()))
					{
						message.Bcc.Add(address.Trim());
					}
				}
			}
		}
		if (null != cc)
		{
			foreach (string address in cc)
			{
				if (address != null)
				{
					if (!string.IsNullOrEmpty(address.Trim()))
					{
						message.CC.Add(address.Trim());
					}
				}
			}
		}
		message.Subject = subject;
		message.Body = body;
		message.IsBodyHtml = true;
		SmtpClient smtpClient = new SmtpClient();
		smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
		smtpClient.Host = emailAccount.Host;
		smtpClient.Port = emailAccount.Port;
		smtpClient.EnableSsl = emailAccount.EnableSSL;
		smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
		smtpClient.Timeout = 10000;
		if (emailAccount.UseDefaultCredentials)
		{
			smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
		}
		else
		{
			smtpClient.Credentials = new NetworkCredential(emailAccount.Username, emailAccount.Password);
		}
		smtpClient.Send(message);
	}
}
