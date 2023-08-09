using HomeWorkPronia.ViewModels.LoginViewModels;

namespace HomeWorkPronia.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEMailAsync(MailRequest mailRequest);
    }
}
