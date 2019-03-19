package com.packtpub;

import com.microsoft.azure.functions.annotation.*;
import com.sendgrid.Content;
import com.sendgrid.Mail;

import org.apache.commons.codec.binary.Base64;

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
        to = "{emailAddress}",
        subject = "A new user got successfully registered",
        text = "") OutputBinding<Mail> outputEmail,
        final ExecutionContext context
    ) {
        context.getLogger().info("Java Queue trigger function processed a message: " + message.getEmailAddress());
        Mail mail = new Mail();
        Content content = new Content();
        content.setType("text/html");
        String contentString = "Hi <b>" + message.getFirstName() + " " + message.getLastName() + "</b>, you were successfully registered";
        content.setValue(contentString);
        mail.addContent(content);
        outputEmail.setValue(mail);
    }
}
