namespace PTW_API.Contracts
{
    /// <summary>
    /// Abstraction for service for updating forecasts.
    /// </summary>
    public interface IForecastUpdaterService
    {
        /// <summary>
        /// Method used for async update of forecast.
        /// </summary>
        /// <returns>Task</returns>
        public Task UpdateForecastAsync();
    }
}
