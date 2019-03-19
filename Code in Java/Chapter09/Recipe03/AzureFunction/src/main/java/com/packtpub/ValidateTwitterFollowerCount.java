package com.packtpub;

import java.io.IOException;
import java.util.*;
import com.microsoft.azure.functions.annotation.*;
import com.sendgrid.Mail;

import org.apache.commons.lang3.StringUtils;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with HTTP Trigger.
 */
public class ValidateTwitterFollowerCount {
    /**
     * This function listens at endpoint "/api/ValidateTwitterFollowerCount". Two ways to invoke it using "curl" command in bash:
     * 1. curl -d "HTTP Body" {your host}/api/ValidateTwitterFollowerCount
     * 2. curl {your host}/api/ValidateTwitterFollowerCount?name=HTTP%20Query
     */
    @FunctionName("ValidateTwitterFollowerCount")
    public HttpResponseMessage run(
            @HttpTrigger(name = "request", methods = {HttpMethod.GET, HttpMethod.POST}, authLevel = AuthorizationLevel.ANONYMOUS) HttpRequestMessage<Optional<String>> request,
            @SendGridOutput(name = "outputEmail", 
            apiKey = "SendGridAPIKey",  
            from = "donotreply@example.com", 
            to = "your_email_address",
            subject = "{Name} with {followersCount} followers has posted a tweet",
            text = "{tweettext}") OutputBinding<Mail> outputEmail,
                final ExecutionContext context) {
        context.getLogger().info("Java HTTP trigger processed a request.");

        TwitterMessage twitterMessage = null;

        String json = request.getBody().orElse(null);
        if (json != null) {
            ObjectMapper mapper = new ObjectMapper();
            try {
                twitterMessage = mapper.readValue(json, TwitterMessage.class);
            } catch (IOException e) {
				twitterMessage = new TwitterMessage();
			}
        }
        else {
            twitterMessage = new TwitterMessage();
            twitterMessage.setFollowersCount(Integer.parseInt(request.getQueryParameters().get("followersCount")));
            twitterMessage.setTweetText(request.getQueryParameters().get("tweettext"));
            twitterMessage.setName(request.getQueryParameters().get("Name"));
        }

        if (twitterMessage.getFollowersCount() == 0 
                || StringUtils.isBlank(twitterMessage.getTweetText()) 
                || StringUtils.isBlank(twitterMessage.getName())) {
            return request.createResponseBuilder(HttpStatus.BAD_REQUEST).body("Please pass a name on the query string or in the request body").build();
        } else {
            outputEmail.setValue(new Mail());
            return request.createResponseBuilder(HttpStatus.OK).body("Success").build();
        }
    }
}
