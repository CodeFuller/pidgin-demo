using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PidginDemo.Library.Extensions;

namespace PidginDemo.Library.UnitTests.Extensions
{
	[TestClass]
	public class ArithmeticExpressionExtensionsTests
	{
		[DataTestMethod]
		[DataRow("123", 123)]
		[DataRow("5 + 3", 8)]
		[DataRow("5 - 3", 2)]
		[DataRow("5 * 3", 15)]
		[DataRow("12 / 4", 3)]
		[DataRow("5 - 3 + 4", 6)]
		[DataRow("2 * 3 + 4 * 5 - 6 / 3", 24)]
		[DataRow("12 / 3 * 4", 16)]
		[DataRow("2 * (3 + 4) - 6 / (2 * 3)", 13)]
		public void CalculateValue_ForCorrectArithmeticExpression_ReturnsCorrectValue(string expression, double expectedValue)
		{
			// Arrange

			var arithmeticExpression = ArithmeticExpressionParser.Parse(expression);

			// Act

			var value = arithmeticExpression.CalculateValue();

			// Assert

			value.Should().Be(expectedValue);
		}
	}
}
