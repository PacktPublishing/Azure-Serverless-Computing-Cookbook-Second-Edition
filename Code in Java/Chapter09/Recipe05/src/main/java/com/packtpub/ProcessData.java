package com.packtpub;

import com.microsoft.azure.functions.annotation.*;

import com.microsoft.azure.functions.*;

/**
 * Azure Functions with Azure Storage Queue trigger.
 */
public class ProcessData {
    /**
     * This function will be invoked when a new message is received at the specified path. The message contents are provided as input to this function.
     */
    @FunctionName("ProcessData")
    public void run(
        @QueueTrigger(name = "message", queueName = "myqueuemessages", connection = "AzureWebJobsStorage") Integer message,
        final ExecutionContext context
    ) {
        if (message > 50) {
            throw new RuntimeException(Integer.toString(message));
        }
        else {
            context.getLogger().info("Java Queue trigger function processed a message: " + message);
        }
    }
}
