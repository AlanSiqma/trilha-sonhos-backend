using Ostium.BeforeIDie.API.Model.Contracts.Settings;

namespace Ostium.BeforeIDie.API.Settings
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string CollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
