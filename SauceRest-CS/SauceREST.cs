using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SauceRESTClient
{
    public class SauceREST
    {
        protected String username;
        protected String accessKey;

        public static String RESTURL = "http://saucelabs.com/rest";

        public SauceREST(String username, String accessKey)
        {
            this.username = username;
            this.accessKey = accessKey;
        }

        public void jobPassed(String jobId)
        {
            var updates = new Dictionary<String, Object>();
            updates.Add("passed", true);
            updateJobInfo(jobId, updates);
        }

        public void jobFailed(String jobId)
        {
            var updates = new Dictionary<String, Object>();
            updates.Add("passed", false);
            updateJobInfo(jobId, updates);
        }

        public void updateJobInfo(String jobId, Dictionary<String, Object> updates)
        {
            String result = null;
            Uri restEndpoint = new Uri(RESTURL + "/v1/" + username + "/jobs/" + jobId);
            String credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username+":"+accessKey));
            String json = JsonConvert.SerializeObject(updates);            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(restEndpoint);
            request.Method = "PUT";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Basic " + credentials);
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
                writer.Close();
                var response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();  
                }
            }
        }

        public string getJobInfo(String jobId)
        {
            String result = null;
            Uri restEndpoint = new Uri(RESTURL + "/v1/" + username + "/jobs/" + jobId);
            String credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + accessKey));
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(restEndpoint);
            request.Method = "GET";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Basic " + credentials);
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
               result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}
