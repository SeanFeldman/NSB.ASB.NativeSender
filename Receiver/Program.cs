using System;
using System.IO;
using NServiceBus;
using NServiceBus.Azure.Transports.WindowsAzureServiceBus;

namespace Receiver
{
    class Program
    {
        private static void Main()
        {
            var configuration = new BusConfiguration();

            configuration.EndpointName("Samples.ASB.NativeIntegration");
            configuration.ScaleOut().UseSingleBrokerQueue();
            configuration.EnableInstallers();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();

            BrokeredMessageBodyConversion.ExtractBody = brokeredMessage =>
            {
                using (var stream = new MemoryStream())
                using (var body = brokeredMessage.GetBody<Stream>())
                {
                    body.CopyTo(stream);
                    return stream.ToArray();
                }
            };

            configuration.UseTransport<AzureServiceBusTransport>()
                .ConnectionString(Environment.GetEnvironmentVariable("AzureServiceBus.ConnectionString"));

            using (IBus bus = Bus.Create(configuration).Start())
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}