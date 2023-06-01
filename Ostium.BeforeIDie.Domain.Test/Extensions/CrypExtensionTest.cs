using Ostium.BeforeIDie.Domain.Extensions;
using Xunit;
namespace Ostium.BeforeIDie.Domain.Test.Extensions
{
    public static class CrypExtensionTest
    {
        [Fact]
        public static void Encrypt_ShouldEncryptText()
        {
            // Arrange
            string texto = "aaa";
            string expectedEncryptedText = "1F9E1D76685D765AA3A6FF85DCED2F0A04F612536DF52696684AAA67787E6CDD";

            // Act
            string encryptedText = texto.Encrypt();

            // Assert
            Assert.Equal(expectedEncryptedText, encryptedText);
        }
    }
}