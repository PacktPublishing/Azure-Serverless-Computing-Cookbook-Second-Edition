package com.packtpub;

import java.util.*;
import com.microsoft.azure.functions.annotation.*;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with HTTP Trigger.
 */
public class HttpSecured {
    /**
     * This function listens at endpoint "/api/HttpSecured". Two ways to
     * invoke it using "curl" command in bash: 1. curl -d "HTTP Body" {your
     * host}/api/HttpSecured 2. curl {your
     * host}/api/HttpSecured?name=HTTP%20Query
     */
    @FunctionName("HttpSecured")
    public HttpResponseMessage run(@HttpTrigger(name = "request", methods = { HttpMethod.GET,
            HttpMethod.POST }, authLevel = AuthorizationLevel.FUNCTION) HttpRequestMessage<Optional<String>> request,
            final ExecutionContext context) {
        context.getLogger().info("Java HTTP trigger processed a request.");

        return request.createResponseBuilder(HttpStatus.BAD_REQUEST).body("Secure function accessed.").build();
    }
}
