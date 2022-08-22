namespace PTW_API.Contracts
{
    public interface IJobsSettings
    {
        /// <summary>
        /// Transaction timeout in seconds
        /// </summary>
        int TransactionTimeoutInSeconds { get; }

        /// <summary>
        /// If set to true, it creates database tables
        /// </summary>
        bool PrepareSchemaIfNecessary { get; }

        /// <summary>
        /// Dashboard job list limit
        /// </summary>
        int DashboardJobListLimit { get; }

        /// <summary>
        /// Hangfire prefix
        /// </summary>
        string JobsHangfirePrefix { get; }

        /// <summary>
        /// Database connection string.
        /// </summary>
        string DbConnectionString { get; }

        /// <summary>
        /// Default queue.
        /// </summary>
        string DefaultQueue { get; }

        /// <summary>
        /// Hangfire background job processing queues
        /// </summary>
        string[] ProcessingQueues { get; }

        /// <summary>
        /// Jobs server username
        /// </summary>
        string JobsServerUsername { get; }

        /// <summary>
        /// Jobs server user password
        /// </summary>
        string JobsServerPassword { get; }
    }
}
