namespace PTW_API.Contracts
{
    using PTW_API.Settings;

    public interface IAppSettings
    {
        /// <summary>
        /// Gets API versioning version 1.0
        /// </summary>
        ApiVersionSettings Version1_0 { get; }
    }
}
