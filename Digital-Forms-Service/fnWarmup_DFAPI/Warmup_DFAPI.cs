using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fnWarmup_DFAPI
{
    public class Warmup_DFAPI
    {
        string Checkdata = Environment.GetEnvironmentVariable("Checkdata");
        string Getdata = Environment.GetEnvironmentVariable("Getdata");
        string Adddata = Environment.GetEnvironmentVariable("Adddata");
        string Updatedata = Environment.GetEnvironmentVariable("Updatedata");
        string Deletedata = Environment.GetEnvironmentVariable("Deletedata");
        bool EnableDebuglog = Convert.ToBoolean(Environment.GetEnvironmentVariable("EnableDebuglog"));
        string AddFiledatapath = Environment.GetEnvironmentVariable("AddFiledatapath");
        string subkey = Environment.GetEnvironmentVariable("subkey");
        string result = "";
        HttpResponseMessage data = null;


        //Timer trigger schedule format "Sec Min Hour Day Month DayoftheWeek"

        //A specific value	0 5 * * * *	Once every hour of the day at minute 5 of each hour
        //All values(*)  0 * 5 * * *	At every minute in the hour, beginning at hour 5
        //A range(- operator)    5-7 * * * * *	Three times a minute - at seconds 5 through 7 during every minute of every hour of each day
        //A set of values(, operator)    5,8,10 * * * * *	Three times a minute - at seconds 5, 8, and 10 during every minute of every hour of each day
        //An interval value(/ operator)  0 */5 * * * *	12 times an hour - at second 0 of every 5th minute of every hour of each day

        //0 */5 * * * *	once every five minutes
        //0 0 * * * *	once at the top of every hour
        //0 0 */2 * * *	once every two hours
        //0 0 9-17 * * *	once every hour from 9 AM to 5 PM
        //0 30 9 * * *	at 9:30 AM every day
        //0 30 9 * * 1-5	at 9:30 AM every weekday
        //0 30 9 * Jan Mon    at 9:30 AM every Monday in January

        [FunctionName("Warmup_DFAPI")]
        public async Task Run([TimerTrigger("%ScheduleTriggerTime%")]TimerInfo myTimer, ILogger log,ExecutionContext context)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                var client = getclient();

                var sw = Stopwatch.StartNew();

                //check the form exists            
                data = await client.GetAsync(Checkdata);
                result = data.Content.ReadAsStringAsync().Result;
                client.Dispose();
                sw.Stop();
                log.LogInformation($"form data for {Checkdata} exists -> {result}");
                log.LogInformation($"check data took {sw.ElapsedMilliseconds} millisecs to retrieve data");

                //get form data
                sw = Stopwatch.StartNew();
                client = getclient();
                data = await client.GetAsync(Getdata);
                result = data.Content.ReadAsStringAsync().Result;
                client.Dispose();
                sw.Stop();
                log.LogInformation($"form data for {Getdata} ->");
                if (EnableDebuglog)
                    log.LogInformation($"{result}");
                log.LogInformation($"get data took {sw.ElapsedMilliseconds} millisecs to retrieve data");

                //Add form data
                sw = Stopwatch.StartNew();
                client = getclient();
                StreamReader File = new StreamReader(Path.Combine(context.FunctionAppDirectory, AddFiledatapath));
                string jsonString = File.ReadToEnd();
                var content = new System.Net.Http.StringContent(jsonString, Encoding.UTF8, "application/json");
                data = await client.PostAsync(Adddata, content);
                result = data.Content.ReadAsStringAsync().Result;
                client.Dispose();
                sw.Stop();
                log.LogInformation($"form data for {Adddata} -> Testform_warmupscript");
                if (EnableDebuglog)
                    log.LogInformation($"{result}");
                log.LogInformation($"add data took {sw.ElapsedMilliseconds} millisecs to post data");

                //update form data
                sw = Stopwatch.StartNew();
                client = getclient();
                data = await client.PutAsync(Updatedata, content);
                result = data.Content.ReadAsStringAsync().Result;
                client.Dispose();
                sw.Stop();
                log.LogInformation($"form data for {Updatedata} -> Testform_warmupscript");
                if (EnableDebuglog)
                    log.LogInformation($"{result}");
                log.LogInformation($"update data took {sw.ElapsedMilliseconds} millisecs to put data");

                //delete form data
                sw = Stopwatch.StartNew();
                client = getclient();
                data = await client.DeleteAsync(Deletedata);
                result = data.Content.ReadAsStringAsync().Result;
                client.Dispose();
                sw.Stop();
                log.LogInformation($"form data for {Deletedata} ->");
                log.LogInformation($"{result}");
                log.LogInformation($"delete data took {sw.ElapsedMilliseconds} millisecs to delete data");
            }
            catch(Exception ex)
            {
                log.LogInformation($"data returned:{data}");
                //log.LogInformation($"result returned:{result}");
                log.LogError(ex.Message + ex.InnerException + ex.StackTrace);
            }

        }

        public HttpClient getclient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");            
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subkey);
            client.Timeout = new TimeSpan(0, 5, 0);
            return client;
        }
    }
}
