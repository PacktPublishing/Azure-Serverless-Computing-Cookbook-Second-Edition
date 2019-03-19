package com.packtpub;

import java.io.IOException;
import java.nio.charset.Charset;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;

import com.google.gson.Gson;
import com.microsoft.azure.eventhubs.ConnectionStringBuilder;
import com.microsoft.azure.eventhubs.EventData;
import com.microsoft.azure.eventhubs.EventHubClient;
import com.microsoft.azure.eventhubs.EventHubException;

public class EventHubApp {
    public static void main(String[] args) {
        Gson gson = new Gson();
        try {
            ConnectionStringBuilder connStr = new ConnectionStringBuilder()
                .setNamespaceName("packtpub")
                .setEventHubName("capturemessage")
                .setSasKeyName("RootManageSharedAccessKey")
                .setSasKey("qED8k9M/IwyQaSnR0U64pgG7dX7rfTZ0g0svAYitq9M=");

            ScheduledExecutorService executorService = Executors.newScheduledThreadPool(4);
            EventHubClient client = EventHubClient.createSync(connStr.toString(), executorService);

            try {
                for (int i = 0; i <= 100; i++) {
                    String payload = "Message " + i;
                    byte[] payloadBytes = gson.toJson(payload).getBytes(Charset.defaultCharset());
                    EventData sendEvent = EventData.create(payloadBytes);
                    client.sendSync(sendEvent);
                }
            }
            finally {
                client.closeSync();
                executorService.shutdown();
            }
        } catch (EventHubException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }



    }
}