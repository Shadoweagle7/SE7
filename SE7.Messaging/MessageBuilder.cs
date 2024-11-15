using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Messaging
{
    public class MessageBuilder
    {
        internal byte[] Bytes;
        public MessageBuilder(byte[] bytes)
        {
            Bytes = bytes;
        }

        public MessageBuilderEncryptionBuilder EncryptUsing => new(this);

        public Message Build()
        {
            return new Message(Bytes);
        }
    }
}
