namespace PTW.Domain.Utils.Deserializers.Forecast
{
    public class WeeklyForecastDeserializer
    {
        public List<DailyForecastDeserializer> Daily { get; set; }

        public DailyForecastDeserializer Current { get; set; }
    }
}
