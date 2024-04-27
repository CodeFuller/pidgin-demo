using System;
using Pidgin;
using Pidgin.Expression;
using PidginDemo.Library.Expressions;
using static Pidgin.Parser;

namespace PidginDemo.Library
{
	public static class ArithmeticExpressionParser
	{
		private static readonly Parser<char, char> OpenParenthesisParser = TokenParser('(');
		private static readonly Parser<char, char> CloseParenthesisParser = TokenParser(')');

		private static readonly Parser<char, ArithmeticExpression> NumberParser = Map<char, double, ArithmeticExpression>(value => new NumberExpression(value), TokenParser(Real));

		// TODO: Can these parsers definitions be simplified?
		private static readonly Parser<char, Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression>> AdditionParser =
			TokenParser('+').Select<Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression>>(_ => (addend1, addend2) => new AdditionExpression(addend1, addend2));

		private static readonly Parser<char, Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression>> SubtractionParser =
			TokenParser('-').Select<Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression>>(_ => (minuend, subtrahend) => new SubtractionExpression(minuend, subtrahend));

		private static readonly Parser<char, Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression>> MultiplicationParser =
			TokenParser('*').Select<Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression>>(_ => (multiplier, multiplicand) => new MultiplicationExpression(multiplier, multiplicand));

		private static readonly Parser<char, Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression>> DivisionParser =
			TokenParser('/').Select<Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression>>(_ => (dividend, divisor) => new DivisionExpression(dividend, divisor));

		private static readonly Parser<char, ArithmeticExpression> ExpressionParser =
			Pidgin.Expression.ExpressionParser.Build<char, ArithmeticExpression>(
				expression => (
					OneOf(NumberParser, ParenthesizedParser(expression)),
					new[]
					{
						Operator.InfixL(MultiplicationParser).And(Operator.InfixL(DivisionParser)),
						Operator.InfixL(AdditionParser).And(Operator.InfixL(SubtractionParser)),
					}));

		public static ArithmeticExpression Parse(string expression)
		{
			var parseResult = ExpressionParser.Parse(expression);
			return parseResult.Success ? parseResult.Value : null;
		}

		private static Parser<char, char> TokenParser(char token)
		{
			return Char(token).Before(SkipWhitespaces);
		}

		private static Parser<char, T> TokenParser<T>(Parser<char, T> token)
		{
			return Try(token).Before(SkipWhitespaces);
		}

		private static Parser<char, T> ParenthesizedParser<T>(Parser<char, T> parser)
		{
			return parser.Between(OpenParenthesisParser, CloseParenthesisParser);
		}
	}
}
