namespace PTW.Domain.Storage.Common.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PTW.Domain.Storage.Forecast.Domain;

    public class ForecastMapping : IEntityTypeConfiguration<Forecast>
    {
        public void Configure(EntityTypeBuilder<Forecast> builder)
        {
            builder.ToTable(nameof(Forecast));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedOn).HasColumnName("created_on").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.DeletedOn).HasColumnName("deleted_on").HasColumnType("datetime").HasDefaultValue(null);

            builder.Property(x => x.ForecastDate).HasColumnName("forecast_date").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.WeatherMain).HasColumnName("weather_main").HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.WeatherDescription).HasColumnName("weather_description").HasColumnType("varchar(50)");
            builder.Property(x => x.WeatherIcon).HasColumnName("weather_icon").HasColumnType("varchar(10)");
            builder.Property(x => x.TemperatureInCelsius).HasColumnName("temperature_in_celsius").HasColumnType("int").IsRequired();
            builder.Property(x => x.CityName).HasColumnName("city_name").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.CityCoutryCode).HasColumnName("city_country_code").HasColumnType("varchar(5)").IsRequired();
        }
    }
}
