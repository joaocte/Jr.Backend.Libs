using System;
using System.Collections.Generic;

namespace Jr.Backend.Libs.Framework.Abstractions
{
    /// <summary>
    /// Fornece lista de erros para serem tradados pelas exceções.
    /// </summary>
    public class ErrorResult
    {
        private ErrorResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Adiciona a mensagens de Exceção a lista de erros.
        /// </summary>
        /// <param name="ex">Referência da <see cref="Exception"/>.</param>
        public ErrorResult(Exception ex) : this()
        {
            while (ex != null)
            {
                Errors.Add(ex.Message);
                ex = ex.InnerException;
            }
        }

        public ErrorResult(IEnumerable<string> erros) : this()
        {
            Errors.AddRange(erros);
        }

        /// <summary>
        /// Lista de erros.
        /// </summary>
        public List<string> Errors { get; set; }
    }
}