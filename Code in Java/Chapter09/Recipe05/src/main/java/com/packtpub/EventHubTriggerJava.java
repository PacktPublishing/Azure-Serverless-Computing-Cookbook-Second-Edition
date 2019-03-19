package com.packtpub;

import com.microsoft.azure.functions.annotation.*;

import java.util.List;

import com.microsoft.azure.functions.*;

/**
 * Azure Functions with Event Hub trigger.
 */
public class EventHubTriggerJava {
    /**
     * This function will be invoked when an event is received from Event Hub.
     */
    @FunctionName("EventHubTriggerJava")
    public void run(
        @EventHubTrigger(name = "message", eventHubName = "capturemessage", connection = "EVENT_HUB", consumerGroup = "$Default", cardinality = Cardinality.MANY) List<String> message,
        final ExecutionContext context
    ) {
        context.getLogger().info("Java Event Hub trigger function executed.");
        context.getLogger().info("Length:" + message.size());
        message.forEach(msg -> context.getLogger().info(msg));
    }
}
