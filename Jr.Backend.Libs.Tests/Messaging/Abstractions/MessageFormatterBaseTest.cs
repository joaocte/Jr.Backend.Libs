using Jr.Backend.Libs.Tests.TestObjjects.Messaging;
using System;
using Xunit;

namespace Jr.Backend.Libs.Tests.Messaging.Abstractions
{
    public class MessageFormatterBaseTest
    {
        [Fact]
        public void WhenCreateaMessageAndFormatterThisReturnJsonFromMessage()
        {
            var message = new MessageFake
            {
                Code = 1,
                MessageName = "MessageName"
            };
            MessageFakeFormatter formatter = new();

            var formatted = formatter.Format(message);

            Assert.NotNull(formatted);
            Assert.IsType<string>(formatted);
        }

        [Fact]
        public void WhenCreateaMessageNullThenThrowArgumentNullException()
        {
            var message = new MessageFake();
            message = null;

            MessageFakeFormatter formatter = new();

            Assert.Throws<ArgumentNullException>(() => formatter.Format(message));
        }
    }
}