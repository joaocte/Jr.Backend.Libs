﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Domain.Core.Validations
{
    public class CNPJValidator<T, TProperty> : DocumentoGenericoValidator<T, TProperty>
    {
        internal CNPJValidator(int validLength, string errorMessage)
            : base(validLength, errorMessage)
        { }

        public CNPJValidator(string errorMessage)
            : this(14, errorMessage)
        { }

        public CNPJValidator()
            : this("O CNPJ é inválido!")
        { }

        protected override int[] FirstMultiplierCollection => new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        protected override int[] SecondMultiplierCollection => new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        public override string Name => "CNPJValidator";
    }
}