 #r "Microsoft.WindowsAzure.Storage"
        using System.Net;
        using Microsoft.WindowsAzure.Storage.Table;

        public static async Task<HttpResponseMessage>
        Run(HttpRequestMessage req,TraceWriter
        log,CloudTable objUserProfileTable)
        {
            dynamic data = await 
             req.Content.ReadAsAsync<object>();
            string firstname= data.firstname;
            string lastname=data.lastname;
 
            UserProfile objUserProfile = new UserProfile(firstname,    
             lastname);
            TableOperation objTblOperationInsert =  
             TableOperation.Insert(objUserProfile);
            objUserProfileTable.Execute(objTblOperationInsert);
    
            return req.CreateResponse(HttpStatusCode.OK,
             "Thank you for Registering..");
        } 

        public class UserProfile : TableEntity
        {
            public UserProfile(string firstName, string lastName)
            {
                this.PartitionKey = "p1";
                this.RowKey = Guid.NewGuid().ToString();;
                this.FirstName = firstName;
                this.LastName = lastName;
            }
            public UserProfile() { }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
