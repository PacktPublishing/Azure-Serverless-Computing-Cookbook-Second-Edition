package com.packtpub;

import com.microsoft.azure.functions.annotation.*;

import java.io.BufferedInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.net.URLConnection;

import com.microsoft.azure.functions.*;

/**
 * Azure Functions with Azure Storage Queue trigger.
 */
public class CreateProfilePictures {
    /**
     * This function will be invoked when a new message is received at the specified path. The message contents are provided as input to this function.
     */
    @FunctionName("CreateProfilePictures")
    public void run(
        @QueueTrigger(name = "message", queueName = "UserImages", connection = "AzureWebJobsStorage") Employee message,
        @BlobOutput(name = "outputBlob", connection = "AzureWebJobsStorage", path = "userprofileimagecontainer/{rowKey}") OutputBinding<byte[]> outputBlob,
        final ExecutionContext context
    ) {
        context.getLogger().info("Java Queue trigger function processed a message: " + message);

        try {
            URL url = new URL(message.getProfilePictureUrl());
            URLConnection connection = url.openConnection();
            try (InputStream is = connection.getInputStream()) {
                BufferedInputStream input = new BufferedInputStream(is);
                ByteArrayOutputStream output = new ByteArrayOutputStream();
                int count = 0;
                byte[] bytes = new byte[1048576];
                while ((count = input.read(bytes, 0, bytes.length)) != -1) {
                    output.write(bytes, 0, count);
                }
                outputBlob.setValue(output.toByteArray());
            }
        } catch (IOException e) {
            context.getLogger().info("Java Queue trigger function failed: " + e.getMessage());
        }
    }
}
