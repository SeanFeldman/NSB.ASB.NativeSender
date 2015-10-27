using System;
using NServiceBus;
using Shared;

namespace Receiver
{
    public class NativeMessageHandler : IHandleMessages<NativeMessage>
    {
        public void Handle(NativeMessage message)
        {
            Console.WriteLine($"Received native message sent on {message.SendOnUtc} UTC");
            Console.WriteLine($"Message content: {message.Content}");    
        }
    }
}