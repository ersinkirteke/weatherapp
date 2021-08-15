using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Domain.ValueObjects
{
    public class Scale : ValueObject
    {
        public string ScaleUnit { get; set; }
        [NotMapped]
        public bool InUse { get; set; }
        [NotMapped]
        public int DecimalPlaces { get; set; }

        public static Scale None = new Scale { InUse = false };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
