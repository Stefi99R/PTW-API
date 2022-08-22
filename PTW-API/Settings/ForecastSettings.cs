namespace PTW_API.Settings
{
    using PTW_API.Contracts;
    using System.Collections.Generic;

    public class ForecastSettings : SettingsBase, IForecastSettings
    {
        public ForecastSettings(IConfiguration configuration)
            : base(configuration) { }

        public string CronJobInterval => GetValue<string>("forecasting:cronJobInterval");

        public List<ICity> Cities => GetSection<List<ICity>>("forecasting:cities");

        public string ApiUrl => GetValue<string>("forecasting:apiUrl");
    }
}
