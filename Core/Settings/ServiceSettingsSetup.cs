using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Settings
{
    public class ServiceSettingsSetup : IConfigureOptions<ServiceSettings>
    {
        private readonly string _sectionName = nameof(ServiceSettings);
        private readonly IConfiguration _configuration;

        public ServiceSettingsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(ServiceSettings options)
        {
            _configuration.GetSection(_sectionName).Bind(options);
        }
    }
}
