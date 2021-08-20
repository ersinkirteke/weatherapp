using System.Text.Json;

namespace Appsfactory.Weather.Identity.Api
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
