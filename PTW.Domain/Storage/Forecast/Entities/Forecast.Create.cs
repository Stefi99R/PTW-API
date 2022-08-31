namespace PTW.Domain.Storage.Forecast.Domain
{
    public partial class Forecast
    {
        public static Forecast Create(DateTime createdOn,
                                      DateTime date,
                                      string weatherMain,
                                      string weatherDescription,
                                      string weatherIcon,
                                      int temperatureInCelsius,
                                      string cityName,
                                      string cityCountryCode)
        {
            return new Forecast
            {
                CreatedOn = createdOn,
                ForecastDate = date,
                WeatherMain = weatherMain,
                WeatherDescription = weatherDescription,
                WeatherIcon = weatherIcon,
                TemperatureInCelsius = temperatureInCelsius,
                CityName = cityName,
                CityCoutryCode = cityCountryCode
            };
        }
    }
}
