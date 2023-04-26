using Ostium.BeforeIDie.Domain.Contracts.Settings;

namespace Ostium.BeforeIDie.Domain.Settings
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string CollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}