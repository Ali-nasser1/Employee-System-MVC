using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using Project.DAL.Models;

namespace Project.PL.Utilities
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("email", "pass");
			Client.Send("email", email.ToWhome, email.Subjuect, email.Body);
		}
	}
}
