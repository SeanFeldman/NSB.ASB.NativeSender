using System;
using NServiceBus.Serializers.Binary;
using NServiceBus.Serializers.Json;

namespace Shared
{
    public class SerializedMessageGenerator
    {
        public static void Generate()
        {
            var serializer = new JsonMessageSerializer(new SimpleMessageMapper());
            var serializedMessage = serializer.SerializeObject(new NativeMessage
            {
                Content = "This is a native message",
                SendOnUtc = DateTime.UtcNow
            });
            Console.WriteLine(serializedMessage);
        }
    }
}