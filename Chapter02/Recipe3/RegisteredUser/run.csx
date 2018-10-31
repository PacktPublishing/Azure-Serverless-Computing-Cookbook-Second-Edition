#r "Microsoft.WindowsAzure.Storage"
#r "SendGrid"
        using System.Net;
         using SendGrid.Helpers.Mail;
        using Microsoft.WindowsAzure.Storage.Table;
        using Newtonsoft.Json;
        public static void Run(HttpRequestMessage req, 
                               TraceWriter log,
                               CloudTable         
                               objUserProfileTable,
                               out string 
                               objUserProfileQueueItem,
                               out Mail message, TextWriter outputBlob
                               )
        {
            var inputs = req.Content.ReadAsStringAsync().Result;
            dynamic inputJson = JsonConvert.DeserializeObject<dynamic>
             (inputs);
    
            string firstname= inputJson.firstname;
            string lastname=inputJson.lastname;
            string profilePicUrl = inputJson.ProfilePicUrl;
             string email = inputJson.email;
             string emailContent;

            objUserProfileQueueItem = profilePicUrl;
            UserProfile objUserProfile = new UserProfile(firstname, 
             lastname, profilePicUrl,email);
            TableOperation objTblOperationInsert = 
             TableOperation.Insert(objUserProfile);
           
            objUserProfileTable.Execute(objTblOperationInsert);
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
        }

        public class UserProfile : TableEntity
        {
            public UserProfile(string lastname, string firstname,
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
