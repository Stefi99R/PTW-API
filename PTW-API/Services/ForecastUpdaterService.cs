namespace PTW_API.Services
{
    using PTW_API.Contracts;
    using System.Threading.Tasks;
    using System.Net.Http;
    using Newtonsoft.Json;
    using PTW.Domain.Utils.Deserializers.Forecast;
    using PTW.Domain.Storage.Forecast.Domain;
    using PTW.Domain.Utils.Mappers;

    public class ForecastUpdaterService : IForecastUpdaterService
    {
        private static HttpClient _httpClient = new HttpClient();

        private IServiceProvider ServiceProvider;

        public ForecastUpdaterService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task UpdateForecastAsync()
        {
            ISecretsManagerSettings? secretsManager = ServiceProvider.GetService<ISecretsManagerSettings>();
            IForecastSettings? forecastSettings = ServiceProvider.GetService<IForecastSettings>();

            forecastSettings?.Cities?.ForEach(async city =>
            {
                HttpResponseMessage response = await _httpClient.GetAsync(forecastSettings.ApiUrl
                                                                          + $"?lat={city.Latitude}" 
                                                                          + $"&lon={city.Longitude}" 
                                                                          + $"&appid={secretsManager?.ApiKey}"
                                                                          + $"&units=metric");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    string forecastJson = await response.Content.ReadAsStringAsync();

                    StoreForecastToDatabase(forecastJson, city.Name, city.CountryCode);
                }
            });
        }

        private void StoreForecastToDatabase(string forecastJson, 
                                             string cityName, 
                                             string cityCountryCode)
        {
            WeeklyForecastDeserializer weeklyForecastDeserialized = JsonConvert.DeserializeObject<WeeklyForecastDeserializer>(forecastJson);

            List<Forecast> forecasts = new List<Forecast>();

            weeklyForecastDeserialized.Daily.ForEach(dailyForecast =>
            {
                forecasts.Add(ForecastDeserializedToEntity.Map(dailyForecastDeserializer: dailyForecast, 
                                                               cityName: cityName, 
                                                               cityCountryCode: cityCountryCode));
            });

            Console.WriteLine(forecasts);
            // save to db
        }
    }
}
