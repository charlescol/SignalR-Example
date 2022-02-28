using System; 
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    using MessageItem = AppModel.Storage.ReferencedItem<System.Guid, AppModel.Message.Message<System.Guid>>;
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder().WithUrl("http://localhost:7075/api", 
                (option) => option.Headers.Add("x-ms-signalr-userid", "cd1b2d6a-46a0-4a5c-a489-2cee68f75b6d")).Build();
            connection.On<string>("MessageSent", ShowMessage);
            connection.StartAsync();
            
            Console.ReadLine();
        }
        public static void ShowMessage(string content)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<MessageItem>(content);
                Console.WriteLine(message.Item.TextContent);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
