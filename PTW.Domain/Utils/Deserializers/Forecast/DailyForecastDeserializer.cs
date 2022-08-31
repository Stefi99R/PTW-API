namespace PTW.Domain.Utils.Deserializers.Forecast
{
    public class DailyForecastDeserializer
    {
        /// <summary>
        /// Epoch date.
        /// </summary>
        public long Dt { get; set; }

        public List<WeatherDeserializer> Weather { get; set; }

        public object Temp { get; set; }
    }
}
