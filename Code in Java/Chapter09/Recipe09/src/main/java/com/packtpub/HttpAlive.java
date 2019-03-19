package com.packtpub;

import java.util.*;
import com.microsoft.azure.functions.annotation.*;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with HTTP Trigger.
 */
public class HttpAlive {
    /**
     * This function listens at endpoint "/api/HttpAlive". Two ways to
     * invoke it using "curl" command in bash: 1. curl -d "HTTP Body" {your
     * host}/api/HttpAlive 2. curl {your
     * host}/api/HttpAlive?name=HTTP%20Query
     */
    @FunctionName("HttpAlive")
    public HttpResponseMessage run(@HttpTrigger(name = "request", methods = { HttpMethod.GET,
            HttpMethod.POST }, authLevel = AuthorizationLevel.ANONYMOUS) HttpRequestMessage<Optional<String>> request,
            final ExecutionContext context) {
        context.getLogger().info("Java HTTP trigger processed a request.");

        return request.createResponseBuilder(HttpStatus.BAD_REQUEST).body("Thanks for keeping me alive.").build();
    }
}
