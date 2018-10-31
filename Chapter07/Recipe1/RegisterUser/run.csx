#r "Microsoft.WindowsAzure.Storage"
        #r "Twilio.Api"
        #r "SendGrid" 

        #load "..\SharedClasses\Helper.csx"

        using System.Net; 
        using SendGrid.Helpers.Mail;  
        using Microsoft.WindowsAzure.Storage.Table;
        using Newtonsoft.Json;
        using Twilio;
        using Microsoft.Azure.WebJobs.Host.Bindings.Runtime;

        public static void Run(HttpRequestMessage req, 
                               TraceWriter log,
                               CloudTable objUserProfileTable,
                               out string objUserProfileQueueItem,
                               out Mail message,
                               IBinder binder,
                               out SMSMessage objsmsmessage
                               )
        {
           var inputs = req.Content.ReadAsStringAsync().Result;
           dynamic inputJson = JsonConvert.DeserializeObject<dynamic>
            (inputs);
           objUserProfileQueueItem = inputJson.ProfilePicUrl;
           string firstname= inputJson.firstname; 
           string lastname=inputJson.lastname; 
           string email = inputJson.email; 
           string profilePicUrl = inputJson.ProfilePicUrl;
           UserProfile objUserProfile = new UserProfile(firstname,             
            lastname,profilePicUrl,email);
           TableOperation objTblOperationInsert = TableOperation.Insert
            (objUserProfile);
           TableResult objTableResult = objUserProfileTable.Execute
            (objTblOperationInsert);
           UserProfile objInsertedUser = (UserProfile)
            objTableResult.Result; 
 
           string strFromEmailAddress = "donotreply@example.com";
           string strSubject = "New User got registered successfully.";
           string emailContent = "Thank you <b>" + firstname + " " +             
            lastname +"</b> for your registration.<br><br>" +
            "Below are the details that you have provided us<br><br>"+
            "<b>First name:</b> " + firstname + "<br>" +
            "<b>Last name:</b> " + lastname + "<br>" +
            "<b>Email Address:</b> " + email + "<br>" +
            "<b>Profile Url:</b> " + profilePicUrl + "<br><br><br>" +
            "Best Regards," + "<br>" + "Website Team";
           string strAttachmentName = firstname + "_" + lastname + 
            ".log";

           message = Helper.SendMail(strSubject,emailContent,
            strFromEmailAddress,email,strAttachmentName);
 
          using (var emailLogBloboutput = binder.Bind<TextWriter>(new            
           BlobAttribute($"userregistrationemaillogs/{objInsertedUser.RowKey}.log")))
          {
             emailLogBloboutput.WriteLine(emailContent);
          } 
          objsmsmessage = new SMSMessage();
          objsmsmessage.Body = "Hello.. Thank you for getting registered.";
        }
       public class UserProfile : TableEntity
        {
           public UserProfile(string firstname, string lastname,string             
            profilePicUrl,string email)
           {
              this.PartitionKey = "p1";
              this.RowKey = Guid.NewGuid().ToString();;
              this.FirstName = firstname; 
              this.LastName = lastname; 
              this.ProfilePicUrl = profilePicUrl; 
              this.Email = email;
           }
           public UserProfile() { }
           public string FirstName { get; set; } 
           public string LastName { get; set; } 
           public string ProfilePicUrl {get; set;} 
           public string Email { get; set; } 
        } 
