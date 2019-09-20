using Nanoblog.Common.Dto;

namespace Nanoblog.Api.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(string id, string role);
    }
}
