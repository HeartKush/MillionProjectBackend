using System.Threading.Tasks;

namespace TaskManagement.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetAccessTokenAsync();
    }
}
