using System;
using System.IO;
using System.Text;
using Microsoft.ServiceBus.Messaging;

namespace NativeSender
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ASB connection string and destination queue 

            var connectionString = Environment.GetEnvironmentVariable("AzureServiceBus.ConnectionString");
            var queueClient = QueueClient.CreateFromConnectionString(connectionString, "Samples.ASB.NativeIntegration");

            #endregion

            var nativeMessage = "{\"Content\":\"This is a native message\",\"SendOnUtc\":\"" + DateTime.UtcNow + "\"}";
            var nativeMessageAsStream = new MemoryStream(Encoding.UTF8.GetBytes(nativeMessage));
            var message = new BrokeredMessage(nativeMessageAsStream)
            {
                MessageId = Guid.NewGuid().ToString()
            };
//            message.Properties["NServiceBus.EnclosedMessageTypes"] = "Shared.NativeMessage, Shared, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
            message.Properties["NServiceBus.EnclosedMessageTypes"] = "Shared.NativeMessage";
            message.Properties["NServiceBus.MessageIntent"] = "Send";
            queueClient.Send(message);

            Console.WriteLine("Native message sent");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
