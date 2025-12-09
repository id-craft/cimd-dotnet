using AutoFixture.Xunit2;
using IdCraft.Cimd.Validation;
using Xunit;

namespace IdCraft.Cimd.Tests.Validation
{
    public class ClientIdentifierValidatorTests
    {
        #region Valid Scenarios

        [Theory]
        [InlineAutoData("https://example.com/client")]
        [InlineAutoData("https://example.com:8443/client")]
        [InlineAutoData("https://example.com/path/to/client")]
        [InlineAutoData("https://example.com/client/")]
        [InlineAutoData("https://example.com/client%20name")]
        [InlineAutoData("https://example.com:9443/client")]
        [InlineAutoData("https://example.com/very/long/path/with/many/segments/that/goes/on/and/on/client")]
        public void IsValid_WithValidClientIdentifier_ReturnsTrue(string clientId, ClientIdentifierValidator sut)
        {
            // Act
            bool result = sut.IsValid(clientId);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region Invalid Scenarios - Null/Empty/Malformed

        [Theory]
        [InlineAutoData(null)]
        [InlineAutoData("")]
        [InlineAutoData("   ")]
        [InlineAutoData("not a valid url")]
        public void IsValid_WithNullEmptyOrMalformedInput_ReturnsFalse(string clientId, ClientIdentifierValidator sut)
        {
            // Act
            bool result = sut.IsValid(clientId);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Invalid Scenarios - Scheme

        [Theory]
        [InlineAutoData("http://example.com/client")]
        [InlineAutoData("ftp://example.com/client")]
        public void IsValid_WithNonHttpsScheme_ReturnsFalse(string clientId, ClientIdentifierValidator sut)
        {
            // Act
            bool result = sut.IsValid(clientId);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Invalid Scenarios - Path

        [Theory]
        [InlineAutoData("https://example.com")]
        [InlineAutoData("https://example.com/")]
        public void IsValid_WithoutValidPath_ReturnsFalse(string clientId, ClientIdentifierValidator sut)
        {
            // Act
            bool result = sut.IsValid(clientId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineAutoData("https://example.com/./path")]
        [InlineAutoData("https://example.com/../path")]
        [InlineAutoData("https://example.com/path/../other")]
        [InlineAutoData("https://example.com/path/.")]
        [InlineAutoData("https://example.com/path/..")]
        public void IsValid_WithDotPathSegments_ReturnsFalse(string clientId, ClientIdentifierValidator sut)
        {
            // Act
            bool result = sut.IsValid(clientId);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Invalid Scenarios - Fragment

        [Theory]
        [InlineAutoData("https://example.com/path#fragment")]
        [InlineAutoData("https://example.com/path#")]
        public void IsValid_WithFragment_ReturnsFalse(string clientId, ClientIdentifierValidator sut)
        {
            // Act
            bool result = sut.IsValid(clientId);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Invalid Scenarios - UserInfo

        [Theory]
        [InlineAutoData("https://user@example.com/path")]
        [InlineAutoData("https://user:password@example.com/path")]
        [InlineAutoData("https://:password@example.com/path")]
        public void IsValid_WithUserInfo_ReturnsFalse(string clientId, ClientIdentifierValidator sut)
        {
            // Act
            bool result = sut.IsValid(clientId);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Invalid Scenarios - Query String

        [Theory]
        [InlineAutoData("https://example.com/path?query=value")]
        [InlineAutoData("https://example.com/path?")]
        [InlineAutoData("https://example.com/path?param1=value1&param2=value2")]
        public void IsValid_WithQueryString_ReturnsFalse(string clientId, ClientIdentifierValidator sut)
        {
            // Act
            bool result = sut.IsValid(clientId);

            // Assert
            Assert.False(result);
        }

        #endregion
    }
}
