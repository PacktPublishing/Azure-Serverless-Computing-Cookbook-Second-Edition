using System.Net;
public static string Run(HttpRequestMessage req, TraceWriter
log)
{
log.Info("C# HTTP trigger function processed a request.");
string Firstname =
req.GetQueryNameValuePairs().FirstOrDefault(q =>
string.Compare(q.Key, "Firstname", true) == 0).Value;
string Lastname =
req.GetQueryNameValuePairs().FirstOrDefault(q =>
string.Compare(q.Key, "Lastname", true) == 0).Value;
return "Hello " + Firstname + " " + Lastname;
}