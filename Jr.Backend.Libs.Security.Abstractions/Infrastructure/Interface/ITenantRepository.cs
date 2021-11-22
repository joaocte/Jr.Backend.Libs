using Jr.Backend.Libs.Domain.Abstractions.Interfaces.Repository;
using Jr.Backend.Libs.Security.Abstractions.Entity;

namespace Jr.Backend.Libs.Security.Abstractions.Infrastructure.Interface
{
    public interface ITenantRepository : IRepository<Tenant>
    {
    }
}