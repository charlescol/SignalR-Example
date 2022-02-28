using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.IO;

namespace SignalRFunctionApp
{
    public static class FunctionsSignalR
    {
        public const string TargetName = "newMessage";
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "chat", UserId = "{headers.x-ms-signalr-userid}")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("broadcast")]
        public async static Task Broadcast(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequest req, ILogger logger,
            [SignalR(HubName = "chat")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();
                await signalRMessages.AddAsync(
                    new SignalRMessage
                    {
                        Target = TargetName,
                        Arguments = new[] { content }
                    });
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
        [FunctionName("sendMessage")]
        public async static Task SendMessage(
                [HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequest req, ILogger logger,
                [SignalR(HubName = "chat")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();
                var message = JsonConvert.DeserializeObject<AppModel.Message.Message<Guid>>(content);
                await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    UserId = message.TargetID.ToString(),
                    Target = TargetName,
                    Arguments = new[] { content }
                });
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}