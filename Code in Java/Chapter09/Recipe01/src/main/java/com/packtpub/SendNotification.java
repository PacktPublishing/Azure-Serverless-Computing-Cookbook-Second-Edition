package com.packtpub;

import com.microsoft.azure.functions.annotation.*;
import com.sendgrid.Attachments;
import com.sendgrid.Content;
import com.sendgrid.Mail;
import com.twilio.Twilio;
import com.twilio.rest.api.v2010.account.Message;
import com.twilio.type.PhoneNumber;

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
        @BlobOutput(name = "outputBlob", connection = "AzureWebJobsStorage", path = "userregistrationemails/{rowKey}.log") OutputBinding<String> outputBlob,
        final ExecutionContext context
    ) {
        context.getLogger().info("Java Queue trigger function processed a message: " + message.getEmailAddress());
        Mail mail = new Mail();
        Content content = new Content();
        content.setType("text/html");
        String contentString = "Hi <b>" + message.getFirstName() + " " + message.getLastName() + "</b>, you were successfully registered";
        content.setValue(contentString);
        mail.addContent(content);
        Attachments attachments = new Attachments();
        attachments.setType("text/html");
        attachments.setFilename(message.getFirstName() + "_" + message.getLastName() + ".log");
        Base64 encoder = new Base64();
        attachments.setContent(encoder.encodeAsString(contentString.getBytes()));
        mail.addAttachments(attachments);
        outputEmail.setValue(mail);
        outputBlob.setValue(contentString);
        Twilio.init(System.getenv("TwilioAccountSID"), System.getenv("TwilioAuthToken"));
        Message.creator(new PhoneNumber(message.getMobileNumber()), new PhoneNumber("+441992351853"), "You were successfully registered").create();
    }
}
