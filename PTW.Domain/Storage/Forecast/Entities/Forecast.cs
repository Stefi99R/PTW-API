namespace PTW.Domain.Storage.Forecast.Domain
{
    using PTW.Domain.Storage.Common.Entities;

    public sealed partial class Forecast : Entity
    {
        private Forecast() { }

        public DateTime ForecastDate { get; private set; }

        public string WeatherMain { get; private set; }

        public string WeatherDescription { get; private set; }

        public string WeatherIcon { get; private set; }

        public int TemperatureInCelsius { get; private set; }

        public string CityName { get; private set; }

        public string CityCoutryCode { get; private set; }
    }
}
