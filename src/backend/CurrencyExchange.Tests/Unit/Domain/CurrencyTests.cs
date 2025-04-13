using CurrencyExchange.Domain.Exceptions.CurrencyExceptions;
using CurrencyExchange.Domain.Models;
using FluentAssertions;

namespace CurrencyExchange.Tests.Unit.Domain
{
    public class CurrencyTests
    {
        [Fact]
        public void Constructor_ValidCode_CreatesCurrency()
        {
            var code = "USD";
            var name = "US Dollar";
            var sign = "$";

            var errors = Currency.Validate(code, name, sign);

            errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("US")]
        [InlineData("USDD")]
        [InlineData("UsD")]
        [InlineData("")]
        public void Constructor_InvalidCode_ThrowsExceptions(string code)
        {
            var errors = Currency.Validate(code, "TEST", "T");
            errors.Should().Contain(e => e.Message.Contains("код", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Constructor_InvalidFullName_ThrowExceptions()
        {
            var errors = Currency.Validate("TST", "", "T");
            errors.Should().NotBeEmpty()
                .And.Contain(e => e.Message.Contains("имя", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Constructor_InvalidSign_ThrowException()
        {
            var errors = Currency.Validate("TST", "TEST", ""); ;
            errors.Should().NotBeEmpty()
                .And.Contain(e => e.Message.Contains("знак", StringComparison.OrdinalIgnoreCase));
        }
    }
}
