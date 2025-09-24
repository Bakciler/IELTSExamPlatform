using IELTSExamPlatform.BL.DTOs.Auth;
using IELTSExamPlatform.BL.ResponceObject;

namespace IELTSExamPlatform.BL.Services.Abstractions
{
    public interface IAuthService
    {
        Task<Response> LoginAsync(LoginDto user);
        Task<Response> LogoutAsync();
    }
}
