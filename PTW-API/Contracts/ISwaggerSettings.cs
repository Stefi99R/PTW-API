namespace PTW_API.Contracts
{
    public interface ISwaggerSettings
    {
        /// <summary>
        /// Swagger client Id.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Swagger authentication flow.
        /// </summary>
        public string AuthenticationFlow { get; }

        /// <summary>
        /// Swagger authentication type.
        /// </summary>
        public uint AuthenticationType { get; }

        /// <summary>
        /// Swagger authentication type name.
        /// </summary>
        public string AuthenticationTypeName { get; }

        /// <summary>
        /// Swagger application name.
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// Swagger application scope.
        /// </summary>
        public string Scope { get; }

        /// <summary>
        /// Swagger description.
        /// </summary>
        public string Description { get; }
    }
}
