#r "Microsoft.WindowsAzure.Storage"
        using System.Net;
        using Microsoft.WindowsAzure.Storage.Table;
        using Newtonsoft.Json;
        public static void Run(HttpRequestMessage req, 
                               TraceWriter log,
                               CloudTable         
                               objUserProfileTable,
                               out string 
                               objUserProfileQueueItem
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
        }

        public class UserProfile : TableEntity
        {
            public UserProfile(string firstname, string lastname,
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
