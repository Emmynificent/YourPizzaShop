using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;

namespace YourPizzaShop.Pages
{
    public class sendMailModel : PageModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public async Task OnPostAsync()
        {
            var smtpClient = new SmtpClient("localhost", 2525);
            var mailMessage = new MailMessage
            {
                From = new MailAddress(From),
                Subject = Subject,
                Body = Body,
                IsBodyHtml = false,
            };
            mailMessage.To.Add(To);

            smtpClient.Send(mailMessage);
        }
    }
}
