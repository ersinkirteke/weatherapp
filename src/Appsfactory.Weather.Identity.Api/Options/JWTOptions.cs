using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Identity.Api.Options
{
    public class JWTOptions
    {
        public const string JWT = "JWT";
        public string Security { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
