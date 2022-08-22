namespace PTW_API.Contracts
{
    public interface IForecastSettings
    {
        string CronJobInterval { get; }

        List<ICity> Cities { get; }

        string ApiUrl { get; }
    }
}
