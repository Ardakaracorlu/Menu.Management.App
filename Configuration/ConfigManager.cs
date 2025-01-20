namespace Menu.Management.App.Configuration
{
    public class ConfigManager
    {
        private readonly IConfiguration _configuration;

        public ConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApiSettings ApiSettings => new ApiSettings
        {
            Url = _configuration.GetValue<string>("ApiSettings:Url")
        };


    }

    public class ApiSettings
    {
        public string Url { get; set; }
    }
}
