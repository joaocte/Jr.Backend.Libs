using System.Collections.Generic;

namespace Jr.Backend.Libs.Security.Abstractions.Infrastructure.Interfaces
{
    public interface ISecurityConfiguration
    {
        string Connection { get; set; }
        string DataBaseName { get; set; }

        Dictionary<string, string> InMemoryCollection { get; }
    }
}