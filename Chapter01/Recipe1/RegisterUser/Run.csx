using System.Net;
public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
string firstname=null,lastname = null;
dynamic data = await req.Content.ReadAsAsync<object>();
firstname = firstname ?? data?.firstname;
lastname = data?.lastname;
return (lastname + firstname) == null ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body") : req.CreateResponse(HttpStatusCode.OK, "Hello " + firstname + " " + lastname);
        } 