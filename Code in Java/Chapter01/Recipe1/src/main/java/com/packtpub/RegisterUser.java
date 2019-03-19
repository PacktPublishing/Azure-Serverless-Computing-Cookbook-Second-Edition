package com.packtpub;

import java.io.IOException;
import java.util.*;
import com.microsoft.azure.functions.annotation.*;

import org.apache.commons.lang3.StringUtils;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with HTTP Trigger.
 */
public class RegisterUser {
    /**
     * This function listens at endpoint "/api/RegisterUser". Two ways to invoke it using "curl" command in bash:
     * 1. curl -d "HTTP Body" {your host}/api/RegisterUser
     * 2. curl {your host}/api/RegisterUser?name=HTTP%20Query
     */
    @FunctionName("RegisterUser")
    public HttpResponseMessage run(
            @HttpTrigger(name = "request", methods = {HttpMethod.GET, HttpMethod.POST}, authLevel = AuthorizationLevel.ANONYMOUS) HttpRequestMessage<Optional<String>> request,
            final ExecutionContext context) {
        context.getLogger().info("Java HTTP trigger processed a request.");

        Employee employee = null;

        String json = request.getBody().orElse(null);
        if (json != null) {
            ObjectMapper mapper = new ObjectMapper();
            try {
                employee = mapper.readValue(json, Employee.class);
            } catch (IOException e) {
                employee = new Employee();
            }
        }
        else {
            employee = new Employee();
            employee.setFirstName(request.getQueryParameters().get("firstName"));
            employee.setLastName(request.getQueryParameters().get("lastName"));
        }

        if (StringUtils.isBlank(employee.getFirstName()) 
                || StringUtils.isBlank(employee.getLastName())) {
            return request.createResponseBuilder(HttpStatus.BAD_REQUEST).body("Please pass a name on the query string or in the request body").build();
        } else {
            return request.createResponseBuilder(HttpStatus.OK).body("Hello, " + employee.getFirstName() + " " + employee.getLastName()).build();
        }
    }
}
