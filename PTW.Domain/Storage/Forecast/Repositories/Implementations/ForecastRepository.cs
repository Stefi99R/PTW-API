namespace PTW.Domain.Storage.Forecast.Repositories.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using PTW.Domain.Storage.Common.Context;
    using PTW.Domain.Storage.Common.Repositories;
    using PTW.Domain.Storage.Forecast.Domain;
    using PTW.Domain.Storage.Forecast.Repositories.Abstractions;
    using System;
    using System.Collections.Generic;

    public class ForecastRepository : Repository<Forecast>, IForecastRepository
    {
        public ForecastRepository(IPTWDbContext ptwDbContext) : base(ptwDbContext) { }

        public async Task<IEnumerable<Forecast>> FindManyByDates(List<DateTime> dates)
        {
            IEnumerable<Forecast> forecasts = await (from forecast in AllNoTrackedOf<Forecast>()
                                                    .Where(f => dates.Any(d => f.ForecastDate == d)) select forecast)
                                                    .ToListAsync();

            return forecasts;
        }

        public void AddOrUpdateMany(IEnumerable<Forecast> forecasts)
        {
            List<Forecast> forecastsToUpdate = FindManyByDates(forecasts.Select(f => f.ForecastDate).ToList()).Result.ToList();

            List<DateTime> datesToUpdate = forecastsToUpdate.Select(f => f.ForecastDate).ToList();

            List<Forecast> forecastsToAdd = forecasts.Select(f => f)
                                                     .Where(f => !datesToUpdate.Contains(f.ForecastDate))
                                                     .ToList();

            foreach (Forecast oldForecast in forecastsToUpdate)
            {
                Forecast newForecast = forecasts.Select(f => f)
                                                .Where(f => f.ForecastDate == oldForecast.ForecastDate
                                                            && f.CityName == oldForecast.CityName
                                                            && f.CityCoutryCode == oldForecast.CityCoutryCode)
                                                .FirstOrDefault();

                Update(oldForecast, newForecast);
            }

            InsertMany(forecastsToAdd);

            SaveChanges();
        }
    }
}
