package com.packtpub;

import java.io.IOException;
import java.net.URISyntaxException;
import java.security.InvalidKeyException;
import java.util.*;
import com.microsoft.azure.functions.annotation.*;
import com.microsoft.azure.storage.CloudStorageAccount;
import com.microsoft.azure.storage.StorageException;
import com.microsoft.azure.storage.queue.CloudQueue;
import com.microsoft.azure.storage.queue.CloudQueueClient;
import com.microsoft.azure.storage.queue.CloudQueueMessage;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with HTTP Trigger.
 */
public class BulkDeviceRegistration {
    /**
     * This function listens at endpoint "/api/BulkDeviceRegistration". Two ways to
     * invoke it using "curl" command in bash: 1. curl -d "HTTP Body" {your
     * host}/api/BulkDeviceRegistration 2. curl {your
     * host}/api/BulkDeviceRegistration?name=HTTP%20Query
     */
    @FunctionName("BulkDeviceRegistration")
    public HttpResponseMessage run(@HttpTrigger(name = "request", methods = { HttpMethod.GET,
            HttpMethod.POST }, authLevel = AuthorizationLevel.ANONYMOUS) HttpRequestMessage<Optional<String>> request,
            final ExecutionContext context) {
        context.getLogger().info("Java HTTP trigger processed a request.");

        try {
            CloudStorageAccount storageAccount = CloudStorageAccount.parse(System.getenv().get("AzureWebJobsStorage"));
            CloudQueueClient queueClient = storageAccount.createCloudQueueClient();
            CloudQueue queue = queueClient.getQueueReference("devicequeue");
            queue.createIfNotExists();
        
            String json = request.getBody().orElse(null);
            if (json != null) {
                ObjectMapper mapper = new ObjectMapper();
                try {
                    Devices devices = mapper.readValue(json, Devices.class);
                    for (Device device : devices.getDevices()) {
                        CloudQueueMessage message = new CloudQueueMessage(mapper.writeValueAsString(device));
                        queue.addMessage(message);
                    }
                    return request.createResponseBuilder(HttpStatus.OK).body("Success").build();
                } catch (IOException e) {
                    context.getLogger().warning(e.getMessage());
                }
            } else {
                context.getLogger().warning("No input json provided");
            }
        } catch (InvalidKeyException e) {
            context.getLogger().warning(e.getMessage());
        } catch (URISyntaxException e) {
            context.getLogger().warning(e.getMessage());
        } catch (StorageException e) {
            context.getLogger().warning(e.getMessage());
        }
        return request.createResponseBuilder(HttpStatus.BAD_REQUEST).body("Failure").build();
    }
}
