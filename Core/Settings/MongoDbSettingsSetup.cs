using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Settings
{
    public class MongoDbSettingsSetup : IConfigureOptions<MongoDbSettings>
    {
        private readonly string _sectionName = nameof(MongoDbSettings);
        private readonly IConfiguration _configuration;

        public MongoDbSettingsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(MongoDbSettings options)
        {
            _configuration.GetSection(_sectionName).Bind(options);
        }
    }
}
