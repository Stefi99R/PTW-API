namespace PTW_API.Settings
{
    using Newtonsoft.Json.Linq;
    using PTW_API.Contracts;
    using PTW_API.Services;

    public class SecretsManagerSettings : SettingsBase, ISecretsManagerSettings
    {
        private IWebHostEnvironment Environment;

        public SecretsManagerSettings(IConfiguration configuration, IWebHostEnvironment environment)
            : base(configuration) 
        { 
            Environment = environment;
        }

        public string ApiKey => Environment.IsDevelopment() ?
              GetValue<string>("OpenWeather:ApiKey") :
              JObject.Parse(RegisterSecretsManager.GetSecret())["PTWApiKey"]!.ToString();
    }
}
