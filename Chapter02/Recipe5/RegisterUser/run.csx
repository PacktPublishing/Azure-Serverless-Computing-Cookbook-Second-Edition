#r "Microsoft.WindowsAzure.Storage"
#r "SendGrid"
 #r "Twilio.Api"
        using System.Net;
         using SendGrid.Helpers.Mail;
        using Microsoft.WindowsAzure.Storage.Table;
        using Newtonsoft.Json;
        using Twilio;
        public static void Run(HttpRequestMessage req, 
                               TraceWriter log,
                               CloudTable         
                               objUserProfileTable,
                               out string 
                               objUserProfileQueueItem,
                               out Mail message, TextWriter outputBlob,
                               IBinder binder,
                               out SMSMessage objsmsmessage
                               )
        {
            var inputs = req.Content.ReadAsStringAsync().Result;
            dynamic inputJson = JsonConvert.DeserializeObject<dynamic>
             (inputs);
    
            string firstname= inputJson.firstname;
            string lastname=inputJson.lastname;
            string profilePicUrl = inputJson.ProfilePicUrl;
             string email = inputJson.email;
             string emailContent ;

            objUserProfileQueueItem = profilePicUrl;
            UserProfile objUserProfile = new UserProfile(firstname, 
             lastname, profilePicUrl,email);
            TableOperation objTblOperationInsert = 
             TableOperation.Insert(objUserProfile);
           TableResult objTableResult = objUserProfileTable.Execute(objTblOperationInsert);
           UserProfile objInsertedUser =         (UserProfile)objTableResult.Result;
            message = new Mail();
            message.Subject = "New User got registered          successfully.";
        message.From = new Email("donotreply@example.com");
          emailContent = "Thank you <b>" + firstname + " " + 
                 lastname +"</b> for your registration.<br><br>" +
                 "Below are the details that you have provided us<br>                 <br>"+ "<b>First name:</b> " + firstname + "<br>" +
                 "<b>Last name:</b> " + lastname + "<br>" +
                 "<b>Email Address:</b> " + email + "<br>" +
                 "<b>Profile Url:</b> " + profilePicUrl + "<br><br>  <br>" + "Best Regards," + "<br>" + "Website Team";
        message.AddContent(new Content("text/html",emailContent));
         outputBlob.WriteLine(emailContent); 

        Personalization personalization = new Personalization();
        personalization.AddTo(new Email(email));
        message.AddPersonalization(personalization); 

        using (var emailLogBloboutput = binder.Bind<TextWriter>(new 
         BlobAttribute($"userregistrationemaillogs/{objInsertedUser.RowKey}.log")))    
        {
           emailLogBloboutput.WriteLine(emailContent);
        } 
Attachment objAttachment = new Attachment();
        objAttachment.Content = System.Convert.ToBase64String
         (System.Text.Encoding.UTF8.GetBytes(emailContent));
        objAttachment.Filename = firstname + "_" + lastname + ".log";
        message.AddAttachment(objAttachment); 
         objsmsmessage= new SMSMessage();
          objsmsmessage.Body = "Hello.. Thank you for getting            registered.";
        }

        public class UserProfile : TableEntity
        {
            public UserProfile(string firstname, string lastname,
             string profilePicUrl,string email)
            {
                this.PartitionKey = "p1";
                this.RowKey = Guid.NewGuid().ToString();
                this.FirstName = firstname;
                this.LastName = lastname;
                this.ProfilePicUrl = profilePicUrl;
                this.Email = email;
            }
            public UserProfile() { }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ProfilePicUrl {get; set;}
             public string Email  { get; set; }
        }
