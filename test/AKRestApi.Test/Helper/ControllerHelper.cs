using Microsoft.AspNetCore.Http;
using Moq;

namespace AKRestApi.Test
{
    public static class ControllerHelper
    {
        public static HttpContext httpHeaderContext()
        {
            var headerDic = new HeaderDictionary();
            var responseHeader = new Mock<HttpResponse>();
            responseHeader.SetupGet(r => r.Headers).Returns(headerDic);

            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Response).Returns(responseHeader.Object);

            return httpContext.Object;
        }
    }
}
