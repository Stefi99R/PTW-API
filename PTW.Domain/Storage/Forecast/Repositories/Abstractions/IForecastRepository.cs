namespace PTW.Domain.Storage.Forecast.Repositories.Abstractions
{
    using Storage.Forecast.Domain;

    public interface IForecastRepository
    {
        void Insert(Forecast forecast);

        void InsertMany(IEnumerable<Forecast> forecasts);

        void AddOrUpdateMany(IEnumerable<Forecast> forecasts);

        Task<IEnumerable<Forecast>> FindManyByDates(List<DateTime> dates);
    }
}
