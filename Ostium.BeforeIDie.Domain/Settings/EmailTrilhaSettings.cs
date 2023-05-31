using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Settings
{
    [ExcludeFromCodeCoverageAttribute]
    public class EmailTrilhaSettings
    {
        public string Sender { get; set; }

        public string UrlTrilhaSonhos { get; set; }
    }
}