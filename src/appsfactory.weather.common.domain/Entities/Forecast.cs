using Appsfactory.Weather.Domain.ValueObjects;
using System;
using System.Text.Json.Serialization;

namespace Appsfactory.Weather.Domain.Entities
{
    public class Forecast : Entity
    {
        public int Id { get; set; }
        public Temperature Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal WindSpeed { get; set; }
        public Address Address { get; set; }
        public DateTimeOffset ForecastDate { get; set; } 
        public DateTimeOffset CreatedDate { get; set; }

        protected override void When(object @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
