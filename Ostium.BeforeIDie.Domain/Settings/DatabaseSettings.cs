using Ostium.BeforeIDie.Domain.Contracts.Settings;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Settings
{
    [ExcludeFromCodeCoverageAttribute]
    public class DatabaseSettings: IDatabaseSettings
    {
        public string CollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}