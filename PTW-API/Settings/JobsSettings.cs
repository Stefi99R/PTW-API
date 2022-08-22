namespace PTW_API.Settings
{
    using PTW_API.Contracts;

    public class JobsSettings : SettingsBase, IJobsSettings
    {
        public JobsSettings(IConfiguration configuration) : base(configuration) { }

        public bool JobsStartServer => GetValue<bool>("jobsSettings:startServer");

        public int TransactionTimeoutInSeconds => GetValue<int>("jobsSettings:transactionTimeout");

        public bool PrepareSchemaIfNecessary => GetValue<bool>("jobsSettings:prepareSchemaIfNecessary");

        public int DashboardJobListLimit => GetValue<int>("jobsSettings:dashboardJobListLimit");

        public string[] ProcessingQueues => GetSection<string[]>("jobsSettings:processingQueues");

        public string DbConnectionString => GetValue<string>("jobsSettings:dbConnectionString");

        private string _jobsHangfirePrefix;
        public string JobsHangfirePrefix
        {
            get
            {
                if (_jobsHangfirePrefix == null)
                {
                    _jobsHangfirePrefix = GetValue<string>("jobsSettings:hangfirePrefix");
                }

                return _jobsHangfirePrefix;
            }
        }

        private string _defaultQueue;
        public string DefaultQueue
        {
            get
            {
                if (_defaultQueue == null)
                {
                    _defaultQueue = GetValue<string>("jobsSettings:defaultQueue");
                }

                return _defaultQueue;
            }
        }

        private string _jobsServerUsername;
        public string JobsServerUsername
        {
            get
            {
                if (_jobsServerUsername == null)
                {
                    _jobsServerUsername = GetValue<string>("jobsSettings:jobsServerUsername");
                }

                return _jobsServerUsername;
            }
        }

        private string _jobsServerPassword;
        public string JobsServerPassword
        {
            get
            {
                if (_jobsServerPassword == null)
                {
                    _jobsServerPassword = GetValue<string>("jobsSettings:jobsServerPassword");
                }

                return _jobsServerPassword;
            }
        }
    }
}
