using System.Security;

namespace Nanoblog.AppCore.ViewModels
{
    public interface IHavePassword
    {
        SecureString Password { get; }
    }
}
