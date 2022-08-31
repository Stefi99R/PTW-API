using Newtonsoft.Json;
using PTW.Domain.Storage.Forecast.Domain;
using PTW.Domain.Utils.Deserializers.Forecast;
using PTW.Domain.Utils.Mappers;
using PTW_API.Contracts;

namespace PTW_API.Services
{
    public class OpenWeatherMapService : IForecastService
    {
        private readonly HttpClient httpClient;
        private readonly ISecretsManagerSettings secretsManager;
        private readonly IForecastSettings forecastSettings;

        public OpenWeatherMapService(HttpClient httpClient, ISecretsManagerSettings secretsManager,
            IForecastSettings forecastSettings)
        {
            this.httpClient = httpClient;
            this.secretsManager = secretsManager;
            this.forecastSettings = forecastSettings;
        }

        public async Task<List<Forecast>> FetchForecasts()
        {
            List<Forecast> forecasts = new List<Forecast>();

            List<ICity> cities = forecastSettings?.Cities;

            HttpResponseMessage[] responses = await Task.WhenAll(cities.Select(city => httpClient.GetAsync(
                                                                               forecastSettings.ApiUrl
                                                                               + $"?lat={city.Latitude}"
                                                                               + $"&lon={city.Longitude}"
                                                                               + $"&appid={secretsManager?.ApiKey}"
                                                                               + $"&units=metric")));

            IEnumerable<(HttpResponseMessage, ICity)> citiesAndResponses = responses.Zip(cities);

            foreach (var (response, city) in citiesAndResponses)
            {
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    string forecastJson = await response.Content.ReadAsStringAsync();

                    List<Forecast> forecastForCity = MapResponseToForecast(forecastJson, city.Name, city.CountryCode);

                    forecasts.AddRange(forecastForCity);
                }
            }

            return forecasts;
        }

        private List<Forecast> MapResponseToForecast(string forecastJson,
                                                                 string cityName,
                                                                 string cityCountryCode)
        {
            WeeklyForecastDeserializer weeklyForecastDeserialized = JsonConvert
                .DeserializeObject<WeeklyForecastDeserializer>(forecastJson);

            List<Forecast> forecasts = new List<Forecast>();

            forecasts.Add(DailyForecastDeserializedToEntity.Map(dailyForecastDeserializer: weeklyForecastDeserialized.Current,
                                                                cityName: cityName,
                                                                cityCountryCode: cityCountryCode));

            weeklyForecastDeserialized.Daily.GetRange(1, 7).ForEach(dailyForecast =>
            {
                forecasts.Add(DailyForecastDeserializedToEntity.Map(dailyForecastDeserializer: dailyForecast,
                                                               cityName: cityName,
                                                               cityCountryCode: cityCountryCode));
            });

            return forecasts;
        }
    }
}
