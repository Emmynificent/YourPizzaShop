using YourPizzaShop.Models;

namespace YourPizzaShop.Services
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);
    }
}
