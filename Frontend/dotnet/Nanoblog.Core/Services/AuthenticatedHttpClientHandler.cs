using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Nanoblog.Core.Services
{
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;

            if (auth != null)
            {
                var jwt = await JwtService.Instance.GetJwtAsync();

                if (jwt != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, jwt.Token);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
