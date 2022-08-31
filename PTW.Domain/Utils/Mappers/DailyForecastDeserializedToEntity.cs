namespace PTW.Domain.Utils.Mappers
{
    using Newtonsoft.Json;
    using PTW.Domain.Storage.Forecast.Domain;
    using PTW.Domain.Utils.Deserializers.Forecast;

    public class DailyForecastDeserializedToEntity
    {
        public static Forecast Map(DailyForecastDeserializer dailyForecastDeserializer, string cityName, string cityCountryCode)
        {
            DateTime date = DateTimeOffset.FromUnixTimeSeconds(dailyForecastDeserializer.Dt).Date;

            WeatherDeserializer weatherDeserialized = dailyForecastDeserializer.Weather.FirstOrDefault();

            int temperatureInCelsius = MapTemperatureInCelsius(dailyForecastDeserializer.Temp);

            return Forecast.Create(createdOn: DateTime.UtcNow,
                                   date: date,
                                   weatherMain: weatherDeserialized.Main,
                                   weatherDescription: weatherDeserialized.Description,
                                   weatherIcon: weatherDeserialized.Icon,
                                   temperatureInCelsius: temperatureInCelsius,
                                   cityName: cityName,
                                   cityCountryCode: cityCountryCode);
        }

        private static int MapTemperatureInCelsius(object temperature)
        {
            int averageTemperatureInCelsius;

            bool isTemperatureDouble = temperature is double;

            if (isTemperatureDouble)
            {
                averageTemperatureInCelsius = Convert.ToInt32(temperature);
            }
            else
            {
                TemperatureDeserializer temperatureInCelsiusDeserializer = JsonConvert
                    .DeserializeObject<TemperatureDeserializer>(Convert.ToString(temperature));

                averageTemperatureInCelsius = (int)((temperatureInCelsiusDeserializer.Max
                                                     + temperatureInCelsiusDeserializer.Min)
                                                     / 2);
            }

            return averageTemperatureInCelsius;
        }
    }
}
