using Hful.Iam.Dto;

namespace Hful.Iam.Service
{
    public interface ILoginService
    {
        Task<LoginDto> LoginAsync(string username, string password);
    }
}
