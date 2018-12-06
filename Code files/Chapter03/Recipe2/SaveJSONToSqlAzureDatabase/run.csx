#r "Newtonsoft.Json"
#r "System.Data"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;


public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
     
    log.LogInformation("C# HTTP trigger function processed a request.");
   
    string firstname,lastname, email, devicelist;

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody); 
    firstname = data.firstname;
    lastname = data.lastname;
    email = data.email;
    devicelist = data.devicelist; 

    SqlConnection con =null;
           try
           {
              string query = "INSERT INTO EmployeeInfo (firstname,lastname, email, devicelist) " +  "VALUES (@firstname,@lastname, @email, @devicelist) ";
             
              con = new 
               SqlConnection("Server=tcp:azurecookbooks.database.windows.net,1433;Initial Catalog=Cookbookdatabase;Persist Security Info=False;User ID=sqladmin;Password=Admin@123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
              SqlCommand cmd = new SqlCommand(query, con);
              cmd.Parameters.Add("@firstname", SqlDbType.VarChar, 
               50).Value = firstname;
              cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 50)
               .Value = lastname;
              cmd.Parameters.Add("@email", SqlDbType.VarChar, 50)
               .Value = email;
              cmd.Parameters.Add("@devicelist", SqlDbType.VarChar)
               .Value = devicelist;
              con.Open();
              cmd.ExecuteNonQuery();
           }
           catch(Exception ex)
           {
              log.LogInformation(ex.Message);
           }
           finally
           {
              if(con!=null)
              {
                 con.Close();
              }
           }

    return (ActionResult)new OkObjectResult($"Successfully inserted the data.");
        
}
