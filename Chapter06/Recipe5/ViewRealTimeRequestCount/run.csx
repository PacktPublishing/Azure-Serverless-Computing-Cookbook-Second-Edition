#r "Newtonsoft.Json"

using System.Configuration;
using System.Text;

using Newtonsoft.Json.Linq;

private const string AppInsightsApi = "https://api.applicationinsights.io/beta/apps";
private const string RealTimePushURL = "https://api.powerbi.com/beta/e62467b7-b6a0-4699-868c-6e23ab90ac24/datasets/e56b8039-a7bf-4e94-80ee-4b626376ddcb/rows?key=3ZsrZduawfNjwdcrUB7Z4Ce7KBQ80FTrqxapzd0TpvqopwU93%2Bh7neXZtfLnjOFAF8PWIZZrjVcTvhuzWVArIw%3D%3D";
private static readonly string AiAppId = ConfigurationManager.AppSettings["AI_APP_ID"];
private static readonly string AiAppKey = ConfigurationManager.AppSettings["AI_APP_KEY"];

public static async Task Run(TimerInfo myTimer, TraceWriter log)
{
    if (myTimer.IsPastDue)
    {
        log.Warning($"[Warning]: Timer is running late! Last ran at: {myTimer.ScheduleStatus.Last}");
    }

      await RealTimeFeedRun(
      query: @"
requests 
| where timestamp > ago(5m) 
| summarize passed = countif(success == true), total = count() 
| project passed
",
        log: log
    );

    log.Info($"Executing real-time Power BI run at: {DateTime.Now}");
   
}
private static async Task RealTimeFeedRun( string query, TraceWriter log)
{
    
    log.Info($"Feeding Data to Power BI has started at: {DateTime.Now}");
    string requestId = Guid.NewGuid().ToString();

     using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("x-api-key", AiAppKey);
            httpClient.DefaultRequestHeaders.Add("x-ms-app", "FunctionTemplate");
            httpClient.DefaultRequestHeaders.Add("x-ms-client-request-id", requestId);
            string apiPath = $"{AppInsightsApi}/{AiAppId}/query?clientId={requestId}&timespan=P1D&query={query}";
            using (var httpResponse = await httpClient.GetAsync(apiPath))
            {
                
                httpResponse.EnsureSuccessStatusCode();
                var resultJson = await httpResponse.Content.ReadAsAsync<JToken>();
                double result;
                if (!double.TryParse(resultJson.SelectToken("Tables[0].Rows[0][0]")?.ToString(), out result))
                {                   
                    throw new FormatException("Query must result in a single metric number. Try it on Analytics before scheduling.");
                }
                 string postData = $"[{{ \"requests\": \"{result}\" }}]";
                log.Verbose($"[Verbose]: Sending data: {postData}");
   
                using (var response = await httpClient.PostAsync(RealTimePushURL, new ByteArrayContent(Encoding.UTF8.GetBytes(postData))))
                {
                    log.Verbose($"[Verbose]: Data sent with response: {response.StatusCode}");
                }
            }
        }
}
