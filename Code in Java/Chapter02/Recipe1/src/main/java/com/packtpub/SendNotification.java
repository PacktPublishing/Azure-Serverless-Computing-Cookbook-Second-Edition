package com.packtpub;

import com.microsoft.azure.functions.annotation.*;
import com.sendgrid.Mail;

import com.microsoft.azure.functions.*;

/**
 * Azure Functions with Azure Storage Queue trigger.
 */
public class SendNotification {
    /**
     * This function will be invoked when a new message is received at the specified path. The message contents are provided as input to this function.
     */
    @FunctionName("SendNotification")
    public void run(
        @QueueTrigger(name = "message", queueName = "UserNotifications", connection = "AzureWebJobsStorage") Employee message,
        @SendGridOutput(name = "emailOutput", 
        apiKey = "SendGridAPIKey",  
        from = "donotreply@example.com", 
        to = "administrator@example.com",
        subject = "A new user got successfully registered",
        text = "Hi Admin, a new user got successfully registered.") OutputBinding<Mail> outputEmail,
        @BlobOutput(name = "outputBlob", connection = "AzureWebJobsStorage", path = "userregistrationemails/{rowKey}.log") OutputBinding<String> outputBlob,
        final ExecutionContext context
    ) {
        context.getLogger().info("Java Queue trigger function processed a message: " + message.getEmailAddress());
        mail.addAttachments(attachments);
        outputEmail.setValue(new Mail());
    }
}
