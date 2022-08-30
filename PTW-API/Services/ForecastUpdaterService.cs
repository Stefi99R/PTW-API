namespace PTW_API.Services
{
    using PTW_API.Contracts;
    using System.Threading.Tasks;
    using PTW.Domain.Storage.Forecast.Domain;
    using PTW.Domain.Storage.Forecast.Repositories.Abstractions;

    public class ForecastUpdaterService : IForecastUpdaterService
    {
        private readonly IForecastService openWeatherMapService;
        private readonly IForecastRepository _forecastRepository;

        public ForecastUpdaterService(IForecastService openWeatherMapService,
                                      IForecastRepository forecastRepository = null)
        {
            this.openWeatherMapService = openWeatherMapService;
            _forecastRepository = forecastRepository;
        }

        public async Task UpdateForecastAsync()
        {
            List<Forecast> forecasts = await openWeatherMapService.FetchForecasts();
            _forecastRepository.InsertMany(forecasts);
        }
    }
}
