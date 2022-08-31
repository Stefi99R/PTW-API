namespace PTW.Domain.Utils.Deserializers.Forecast
{
    public class TemperatureDeserializer
    {
        /// <summary>
        /// Minimum temperature during the day in Celsius.
        /// </summary>
        public float Min { get; set; }

        /// <summary>
        /// Maximum temperature during the day in Celsius.
        /// </summary>
        public float Max { get; set; }
    }
}
