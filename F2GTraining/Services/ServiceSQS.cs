using Amazon.SQS.Model;
using Amazon.SQS;
using Newtonsoft.Json;
using System.Net;
using F2GTraining.Models;

namespace F2GTraining.Services
{
    public class ServiceSQS
    {
        private IAmazonSQS clientSQS;
        private string UrlQueue;

        public ServiceSQS(IAmazonSQS amazonSQS, IConfiguration configuration)
        {
            this.clientSQS = amazonSQS;
            this.UrlQueue =
                configuration.GetValue<string>("AWS:SQS:UrlQueue");
        }

        public async Task SendMessageAsync(Nota nota)
        {
            string json = JsonConvert.SerializeObject(nota);
            SendMessageRequest request =
                new SendMessageRequest(this.UrlQueue, json);
            SendMessageResponse response =
                await this.clientSQS.SendMessageAsync(request);
            HttpStatusCode statusCode = response.HttpStatusCode;
        }
    }
}
