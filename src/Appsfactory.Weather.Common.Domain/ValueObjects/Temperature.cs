using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Domain.ValueObjects
{
    public class Temperature : ValueObject
    {
        public decimal Value { get; private set; }
        public Scale Scale { get; private set; }

        public Temperature()
        {

        }

        public Temperature(decimal value, Scale scale)
        {
            Value = value;
            Scale = scale;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Scale;
        }

        public override string ToString() => $"{Value} {Scale.ScaleUnit}";
    }
}
