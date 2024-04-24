using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PidginDemo.Library.UnitTests
{
	// TODO: Add UT for incorrect expressions.
	[TestClass]
	public class ArithmeticExpressionParserTests
	{
		[DataTestMethod]
		[DataRow("123.45", "123.45")]
		[DataRow("-123.45", "-123.45")]
		[DataRow("5 + 3", "[5 + 3]")]
		[DataRow("5 - 3", "[5 - 3]")]
		[DataRow("5 * 3", "[5 * 3]")]
		[DataRow("12 / 4", "[12 / 4]")]
		[DataRow("2 * 3 + 4 * 5 - 6 / 3", "[[[2 * 3] + [4 * 5]] - [6 / 3]]")]
		[DataRow("12 / 3 * 4", "[[12 / 3] * 4]")]
		[DataRow("2 * (3 + 4) - 6 / (2 * 3)", "[[2 * [3 + 4]] - [6 / [2 * 3]]]")]
		public void Parse_ForCorrectExpression_ReturnsCorrectExpression(string inputExpression, string expectedExpressionString)
		{
			// Act

			var expression = ArithmeticExpressionParser.Parse(inputExpression);

			// Assert

			expression.ToString().Should().Be(expectedExpressionString);
		}
	}
}
