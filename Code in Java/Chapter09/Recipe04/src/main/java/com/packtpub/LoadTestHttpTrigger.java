package com.packtpub;

import java.util.*;
import com.microsoft.azure.functions.annotation.*;

import com.microsoft.azure.functions.*;

/**
 * Azure Functions with HTTP Trigger.
 */
public class LoadTestHttpTrigger {
    /**
     * This function listens at endpoint "/api/LoadTestHttpTrigger". Two ways to
     * invoke it using "curl" command in bash: 1. curl -d "HTTP Body" {your
     * host}/api/LoadTestHttpTrigger 2. curl {your
     * host}/api/LoadTestHttpTrigger?name=HTTP%20Query
     */
    @FunctionName("LoadTestHttpTrigger")
    public HttpResponseMessage run(@HttpTrigger(name = "request", methods = { HttpMethod.GET,
            HttpMethod.POST }, authLevel = AuthorizationLevel.ANONYMOUS) HttpRequestMessage<Optional<String>> request,
            final ExecutionContext context) {
        context.getLogger().info("Java HTTP trigger processed a request.");

        try {
            Thread.sleep(2000);
        } catch (InterruptedException e) {
        }

        return request.createResponseBuilder(HttpStatus.OK).body("Success").build();
    }
}
