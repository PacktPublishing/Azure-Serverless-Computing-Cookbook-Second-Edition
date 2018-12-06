using FunctionAppInVisualStudio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AzureFunctions.Tests
{
    public class ShouldExecuteAzureFunctions
    {
        [Fact]
        public async Task WithAQueryString()
        {
            var httpRequestMock = new Mock<HttpRequest>();
            var LogMock = new Mock<ILogger>();
            var queryStringParams = new Dictionary<String, StringValues>();
            httpRequestMock.Setup(req => req.Query).Returns(new QueryCollection(queryStringParams));
            queryStringParams.Add("name", "Praveen Sreeram");
            
            var result = await HTTPTriggerCSharpFromVS.Run(httpRequestMock.Object,LogMock.Object);
            var resultObject = (OkObjectResult)result;
            Assert.Equal("Hello, Praveen Sreeram", resultObject.Value);
        }
    }
}
