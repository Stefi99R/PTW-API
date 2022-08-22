namespace PTW_API.Services
{
    using PTW_API.Contracts;
    using System.Threading.Tasks;
    using System.Net.Http;
    using PTW.Domain.Entities.Forecast.Domain;
    using Newtonsoft.Json;

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
                ForecastDeserializer forecast = null;

                HttpResponseMessage response = await _httpClient.GetAsync(forecastSettings.ApiUrl
                                                                          + $"?lat={city.Latitude}" 
                                                                          + $"&lon={city.Longitude}" 
                                                                          + $"&appid={secretsManager?.ApiKey}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    string forecastJson = await response.Content.ReadAsStringAsync();

                    forecast = JsonConvert.DeserializeObject<ForecastDeserializer>(forecastJson);
                }
            });
        }
    }
}
