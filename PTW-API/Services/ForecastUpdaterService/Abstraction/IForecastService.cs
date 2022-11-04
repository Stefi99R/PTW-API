namespace PTW_API.Services.ForecastUpdaterService.Abstraction
{
    using PTW.Domain.Storage.Forecast.Domain;

    public interface IForecastService
    {
        Task<List<Forecast>> FetchForecasts();
    }
}