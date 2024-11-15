using System.Collections.Concurrent;

namespace SE7.Messaging
{
    public class MessageQueue
    {
        private readonly ConcurrentQueue<Message> Messages = [];

        public MessageQueue()
        {

        }
    }
}
