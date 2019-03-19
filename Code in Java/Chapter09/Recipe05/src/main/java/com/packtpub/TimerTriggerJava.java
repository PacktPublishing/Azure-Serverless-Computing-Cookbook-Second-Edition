package com.packtpub;

import java.io.IOException;
import java.time.*;
import com.microsoft.azure.functions.annotation.*;

import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.HttpClientBuilder;

import com.microsoft.azure.functions.*;

/**
 * Azure Functions with Timer trigger.
 */
public class TimerTriggerJava {
    /**
     * This function will be invoked periodically according to the specified
     * schedule.
     */
    @FunctionName("TimerTriggerJava")
    public void run(@TimerTrigger(name = "timerInfo", schedule = "0 */5 * * * *") String timerInfo,
            final ExecutionContext context) {
        context.getLogger().info("Java Timer trigger function executed at: " + LocalDateTime.now());

        HttpClient client = HttpClientBuilder.create().build();
        HttpGet request = new HttpGet("https://packt-function-app.azurewebsites.net/api/HttpAlive");
        try {
            client.execute(request);
            context.getLogger().info("Keep alive ping!");
        } catch (ClientProtocolException e) {
            context.getLogger().warning(e.getMessage());
        } catch (IOException e) {
            context.getLogger().warning(e.getMessage());
        }
    }
}
