namespace PTW_API.Services
{
    using PTW_API.Contracts;
    using System.Threading.Tasks;
    using PTW.Domain.Storage.Forecast.Domain;
    using PTW.Domain.Storage.Forecast.Repositories.Abstractions;

    public class ForecastUpdaterService : IForecastUpdaterService
    {
        private readonly IForecastService _forecastService;
        private readonly IForecastRepository _forecastRepository;

        public ForecastUpdaterService(IForecastService forecastService,
                                      IForecastRepository forecastRepository = null)
        {
            _forecastService = forecastService;
            _forecastRepository = forecastRepository;
        }

        public async Task UpdateForecastAsync()
        {
            List<Forecast> forecasts = await _forecastService.FetchForecasts();

            _forecastRepository.AddOrUpdateMany(forecasts);
        }
    }
}
