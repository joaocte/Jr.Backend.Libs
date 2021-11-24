using System.Collections.Generic;

namespace Jr.Backend.Libs.Infrastructure.MongoDB.DependencyInjection
{
    public class SecurityConfiguration
    {
        public static Dictionary<string, string> InMemoryDesCollection =
            new Dictionary<string, string>
            {
                {"MongoSettings:Connection", "mongodb://localhost:27017/?authSource=admin"},
                {"MongoSettings:DatabaseName", "JrTenant"}
            };
    }
}