package com.packtpub;

import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.util.*;
import com.microsoft.azure.functions.annotation.*;

import org.apache.commons.lang3.StringUtils;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with HTTP Trigger.
 */
public class SaveJSONToAzureSQLDatabase {
    /**
     * This function listens at endpoint "/api/SaveJSONToAzureSQLDatabase". Two ways to invoke it using "curl" command in bash:
     * 1. curl -d "HTTP Body" {your host}/api/SaveJSONToAzureSQLDatabase
     * 2. curl {your host}/api/SaveJSONToAzureSQLDatabase?name=HTTP%20Query
     */
    @FunctionName("SaveJSONToAzureSQLDatabase")
    public HttpResponseMessage run(
            @HttpTrigger(name = "request", methods = {HttpMethod.GET, HttpMethod.POST}, authLevel = AuthorizationLevel.ANONYMOUS) HttpRequestMessage<Optional<String>> request,
            final ExecutionContext context) {
        context.getLogger().info("Java HTTP trigger processed a request.");

        EmployeeInfo employeeInfo = null;

        String json = request.getBody().orElse(null);
        if (json != null) {
            ObjectMapper mapper = new ObjectMapper();
            try {
                employeeInfo = mapper.readValue(json, EmployeeInfo.class);
            } catch (IOException e) {
				employeeInfo = new EmployeeInfo();
			}
        }
        else {
            employeeInfo = new EmployeeInfo();
            employeeInfo.setFirstName(request.getQueryParameters().get("firstname"));
            employeeInfo.setLastName(request.getQueryParameters().get("lastname"));
            employeeInfo.setEmailAddress(request.getQueryParameters().get("email"));
            employeeInfo.setDeviceList(request.getQueryParameters().get("devicelist"));
        }

        if (StringUtils.isBlank(employeeInfo.getFirstName()) 
                || StringUtils.isBlank(employeeInfo.getLastName()) 
                || StringUtils.isBlank(employeeInfo.getEmailAddress())
                || StringUtils.isBlank(employeeInfo.getDeviceList())) {
            return request.createResponseBuilder(HttpStatus.BAD_REQUEST).body("Please pass a name on the query string or in the request body").build();
        } else {
            String hostName = "cookbook-jm.database.windows.net";
            String databaseName = "Cookbookdatabase";
            String userName = "jamarsto";
            String password = "DemoPassword!";

            String url = String.format("jdbc:sqlserver://%s:1433;database=%s;user=%s;password=%s;encrypt=true;hostNameInCertificate=*.database.windows.net;loginTimeout=30;",
                hostName,
                databaseName,
                userName,
                password);

            try (Connection con = DriverManager.getConnection(url)) {
                String sql = "INSERT INTO EmployeeInfo (firstname, lastname, email, devicelist) VALUES(?, ?, ?, ?)";

                try (PreparedStatement stmt = con.prepareStatement(sql)) {
                    stmt.setString(1, employeeInfo.getFirstName());
                    stmt.setString(2, employeeInfo.getLastName());
                    stmt.setString(3, employeeInfo.getEmailAddress());
                    stmt.setString(4, employeeInfo.getDeviceList());

                    stmt.executeUpdate();
                }
            } catch (Exception e) {
                context.getLogger().severe(e.getMessage());
                return request.createResponseBuilder(HttpStatus.INTERNAL_SERVER_ERROR).body("Error Saving Data.").build();
            }
            return request.createResponseBuilder(HttpStatus.OK).body("Successfully Inserted Data").build();
        }
    }
}
