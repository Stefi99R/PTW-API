namespace PTW_API.Contracts
{
    /// <summary>
    /// Abstraction for secrets manager service.
    /// </summary>
    public interface ISecretsManagerSettings
    {
        /// <summary>
        /// Api key for the secrets manager.
        /// </summary>
        public string ApiKey { get; }
    }
}
