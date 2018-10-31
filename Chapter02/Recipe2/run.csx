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
                               out Mail message
                               )
        {
            var inputs = req.Content.ReadAsStringAsync().Result;
            dynamic inputJson = JsonConvert.DeserializeObject<dynamic>
             (inputs);
    
            string firstname= inputJson.firstname;
            string lastname=inputJson.lastname;
            string profilePicUrl = inputJson.ProfilePicUrl;
             string email = inputJson.email;

            objUserProfileQueueItem = profilePicUrl;
            UserProfile objUserProfile = new UserProfile(firstname, 
             lastname, profilePicUrl,email);
            TableOperation objTblOperationInsert = 
             TableOperation.Insert(objUserProfile);
           
            objUserProfileTable.Execute(objTblOperationInsert);
            message = new Mail();
            message.Subject = "New User got registered          successfully.";
        message.From = new Email("donotreply@example.com");
        message.AddContent(new Content("text/html","Thank you so much          for getting registered to our site."));

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