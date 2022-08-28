namespace PTW_API.Services
{
    using PTW_API.Contracts;
    using System.Threading.Tasks;
    using System.Net.Http;
    using Newtonsoft.Json;
    using PTW.Domain.Utils.Deserializers.Forecast;
    using PTW.Domain.Storage.Forecast.Domain;
    using PTW.Domain.Utils.Mappers;
    using PTW.Domain.Storage.Forecast.Repositories.Abstractions;

    public class ForecastUpdaterService : IForecastUpdaterService
    {
        private static HttpClient _httpClient = new HttpClient();

        private static IForecastRepository _forecastRepository;

        private IServiceProvider ServiceProvider;

        public ForecastUpdaterService(IServiceProvider serviceProvider, 
                                      IForecastRepository forecastRepository = null)
        {
            ServiceProvider = serviceProvider;
            _forecastRepository = forecastRepository;
        }

        public async Task UpdateForecastAsync()
        {
            ISecretsManagerSettings? secretsManager = ServiceProvider.GetService<ISecretsManagerSettings>();
            IForecastSettings? forecastSettings = ServiceProvider.GetService<IForecastSettings>();

            List<Forecast> forecasts = Task.Run(() => FetchForecasts(secretsManager, forecastSettings)).Result;

            forecasts.ForEach(dailyForecast =>
            {
                _forecastRepository.Insert(dailyForecast);
            });
        }

        private async Task<List<Forecast>> FetchForecasts(ISecretsManagerSettings? secretsManager, 
                                                          IForecastSettings? forecastSettings)
        {
            List<Forecast> forecasts = new List<Forecast>();

            forecastSettings?.Cities?.ForEach(async city =>
            {
                HttpResponseMessage response = Task.Run(() => _httpClient.GetAsync(forecastSettings.ApiUrl
                                                                          + $"?lat={city.Latitude}"
                                                                          + $"&lon={city.Longitude}"
                                                                          + $"&appid={secretsManager?.ApiKey}"
                                                                          + $"&units=metric")).Result;

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    string forecastJson = await response.Content.ReadAsStringAsync();

                    List<Forecast> forecastForCity = Task.Run(() => MapResponseToForecast(forecastJson, 
                                                                                          city.Name, 
                                                                                          city.CountryCode)).Result;

                    forecasts.AddRange(forecastForCity);
                }
            });

            return forecasts;
        }

        private async Task<List<Forecast>> MapResponseToForecast(string forecastJson, 
                                                                 string cityName, 
                                                                 string cityCountryCode)
        {
            WeeklyForecastDeserializer weeklyForecastDeserialized = JsonConvert
                .DeserializeObject<WeeklyForecastDeserializer>(forecastJson);

            List<Forecast> forecasts = new List<Forecast>();

            weeklyForecastDeserialized.Daily.ForEach(dailyForecast =>
            {
                forecasts.Add(ForecastDeserializedToEntity.Map(dailyForecastDeserializer: dailyForecast, 
                                                               cityName: cityName, 
                                                               cityCountryCode: cityCountryCode));
            });

            return forecasts;
        }
    }
}
