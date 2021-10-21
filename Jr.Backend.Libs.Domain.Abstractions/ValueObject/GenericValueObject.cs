using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Jr.Backend.Libs.Domain.Abstractions.ValueObject
{
    public abstract class GenericValueObject
    {
        protected static bool EqualOperator(GenericValueObject obj, GenericValueObject otherObj)
        {
            if (ReferenceEquals(obj, null) ^ ReferenceEquals(otherObj, null))
            {
                return false;
            }
            return ReferenceEquals(obj, null) || obj.Equals(otherObj);
        }

        public static GenericValueObject Parse(string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(GenericValueObject));
            if (converter != null)
            {
                return (GenericValueObject)converter.ConvertFromString(value);
            }
            return default(GenericValueObject);
        }

        protected static bool NotEqualOperator(GenericValueObject obj, GenericValueObject otherObj)
        {
            return !(EqualOperator(obj, otherObj));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (GenericValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}