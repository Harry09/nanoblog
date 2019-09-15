using Nanoblog.Common.Data.Dto;

namespace Nanoblog.Api.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(string id, string role);
    }
}
