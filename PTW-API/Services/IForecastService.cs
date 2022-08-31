namespace PTW_API.Services
{
    using PTW.Domain.Storage.Forecast.Domain;

    public interface IForecastService
    {
        Task<List<Forecast>> FetchForecasts();
    }
}