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

            objUserProfileQueueItem = profilePicUrl;
            UserProfile objUserProfile = new UserProfile(firstname, 
             lastname, profilePicUrl);
            TableOperation objTblOperationInsert = 
             TableOperation.Insert(objUserProfile);
           
            objUserProfileTable.Execute(objTblOperationInsert);
            message = new Mail();
        }

        public class UserProfile : TableEntity
        {
            public UserProfile(string lastname, string firstname,
             string profilePicUrl)
            {
                this.PartitionKey = "p1";
                this.RowKey = Guid.NewGuid().ToString();
                this.FirstName = firstname;
                this.LastName = lastname;
                this.ProfilePicUrl = profilePicUrl;
            }
            public UserProfile() { }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ProfilePicUrl {get; set;}
        }