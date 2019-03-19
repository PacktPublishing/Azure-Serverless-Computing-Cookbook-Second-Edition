package com.packtpub;

import com.microsoft.azure.functions.annotation.*;
import java.io.IOException;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.Gson;
import com.google.gson.internal.LinkedTreeMap;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with Cosmos DB trigger.
 */
public class CookbookDataCollection {
    /**
     * This function will be invoked when there are inserts or updates in the specified database and collection.
     */
    @FunctionName("CookbookDataCollection")
    public void run(
        @CosmosDBTrigger(
            name = "items",
            databaseName = "cookbookdatabase",
            collectionName = "cookbookdatacollection",
            leaseCollectionName="leases",
            connectionStringSetting = "COSMOS_CONNECTION",
            createLeaseCollectionIfNotExists = true
        )
        LinkedTreeMap<?,?>[] items,
    final ExecutionContext context
    ) {
        context.getLogger().info("Java Cosmos DB trigger function executed.");
        context.getLogger().info("Documents count: " + items.length);

        Gson gson = new Gson();
        ObjectMapper mapper = new ObjectMapper();
  
        for (LinkedTreeMap<?,?> item : items) {
            try {
                String itemJson = gson.toJson(gson.toJsonTree(item).getAsJsonObject());
                CosmosDocument document = mapper.readValue(itemJson, CosmosDocument.class);
                context.getLogger().info("Document id = " + document.getId());
            } catch (IOException e) {
                context.getLogger().warning(e.getMessage());
                context.getLogger().warning("Unexpected document type");
            }
        }
    }
}
