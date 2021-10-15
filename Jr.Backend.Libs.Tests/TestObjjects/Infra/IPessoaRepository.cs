using Jr.Backend.Libs.Domain.Interfaces.Repository;
using Jr.Backend.Libs.Tests.Domain;
using Jr.Backend.Libs.Tests.TestObjjects.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
    }
}