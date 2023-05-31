using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.Domain.Settings
{
    [ExcludeFromCodeCoverageAttribute]
    public class AuthMessageSenderOptions
    {
        public string SendGridKey { get; set; }
    }
}