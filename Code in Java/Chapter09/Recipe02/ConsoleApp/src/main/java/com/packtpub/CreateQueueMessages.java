package com.packtpub;

import java.net.URISyntaxException;
import java.security.InvalidKeyException;

import com.microsoft.azure.storage.CloudStorageAccount;
import com.microsoft.azure.storage.StorageException;
import com.microsoft.azure.storage.queue.CloudQueue;
import com.microsoft.azure.storage.queue.CloudQueueClient;
import com.microsoft.azure.storage.queue.CloudQueueMessage;

public class CreateQueueMessages {
    public static void main(String[] args) {
        CloudStorageAccount storageAccount;
        try {
            storageAccount = CloudStorageAccount.parse("DefaultEndpointsProtocol=https;AccountName=packtfunctionapp;AccountKey=your_account_key");
            CloudQueueClient queueClient = storageAccount.createCloudQueueClient();
            CloudQueue queue = queueClient.getQueueReference("myqueuemessages");
            queue.createIfNotExists();

            CloudQueueMessage message = null;
            for (int i = 0; i <= 100; i++) {
                message = new CloudQueueMessage(Integer.toString(i));
                queue.addMessage(message);
            }
        } catch (InvalidKeyException e) {
            e.printStackTrace();
        } catch (URISyntaxException e) {
            e.printStackTrace();
        } catch (StorageException e) {
            e.printStackTrace();
        }
    }
}