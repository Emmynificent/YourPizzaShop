using Microsoft.AspNetCore.Mvc;
using YourPizzaShop.Models;
using YourPizzaShop.Services;



namespace YourPizzaShop.Controller
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class MailController : ControllerBase
    {
        //[HttpGet]

        private readonly IMailService _mail;

        public MailController(IMailService mail)
        {
            _mail = mail;
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(MailData mailData)
        {
            bool result = await _mail.SendAsync(mailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }
    }
}
