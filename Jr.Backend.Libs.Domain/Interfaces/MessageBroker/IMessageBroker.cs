using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Domain.Interfaces.MessageBroker
{
    public interface IMessageBroker : IDisposable
    {
        public Task Connect();
    }
}