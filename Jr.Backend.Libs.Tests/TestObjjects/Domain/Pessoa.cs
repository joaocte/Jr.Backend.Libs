using System.ComponentModel.DataAnnotations;

namespace Jr.Backend.Libs.Tests.TestObjjects.Domain
{
    public class Pessoa
    {
        [Key]
        public string Cpf { get; set; }

        public string Nome { get; set; }
    }
}