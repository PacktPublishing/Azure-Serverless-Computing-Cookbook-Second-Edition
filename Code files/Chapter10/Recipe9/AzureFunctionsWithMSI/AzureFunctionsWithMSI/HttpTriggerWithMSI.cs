using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Data.SqlClient;
using System.Data;
using System;
using Microsoft.Azure.Services.AppAuthentication;

namespace AzureFunctionsWithMSI
{
    public static class HttpTriggerWithMSI
    {
        [FunctionName("HttpTriggerWithMSI")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string firstname = string.Empty, lastname = string.Empty, email = string.Empty, devicelist = string.Empty;

            dynamic data = await req.Content.ReadAsAsync<object>();
            firstname = data?.firstname;
            lastname = data?.lastname;
            email = data?.email;
            devicelist = data?.devicelist;

            SqlConnection con = null;
            try
            {
                string query = "INSERT INTO EmployeeInfo (firstname,lastname, email, devicelist) " + "VALUES (@firstname,@lastname, @email, @devicelist) ";

                con = new
                 SqlConnection("Server=tcp:azuresqlmsidbserver.database.windows.net,1433;Initial Catalog=azuresqlmsidatabase;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                SqlCommand cmd = new SqlCommand(query, con);

                con.AccessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;

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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return req.CreateResponse(HttpStatusCode.OK, "Hello, Successfully inserted the data");
        }
    }
}
