namespace PTW_API.Contracts
{
    using System;

    public interface IOAuthSettings
    {
        /// <summary>
        /// OAuth authorize URL.
        /// </summary>
        public Uri AuthorizeUrl { get; }
    }
}
