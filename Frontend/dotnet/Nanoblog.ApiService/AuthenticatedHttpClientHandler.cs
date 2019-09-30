using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Nanoblog.ApiService
{
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;

            if (auth is { })
            {
                var jwt = await JwtService.Instance.GetJwtAsync();

                if (jwt is { })
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, jwt.Token);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
