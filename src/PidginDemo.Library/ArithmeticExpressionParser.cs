using System;
using Pidgin;
using Pidgin.Expression;
using PidginDemo.Library.Expressions;
using static Pidgin.Parser;

using BinaryOperatorParser = Pidgin.Parser<char, System.Func<PidginDemo.Library.Expressions.ArithmeticExpression, PidginDemo.Library.Expressions.ArithmeticExpression, PidginDemo.Library.Expressions.ArithmeticExpression>>;

namespace PidginDemo.Library
{
	public static class ArithmeticExpressionParser
	{
		private static readonly Parser<char, char> OpenParenthesisParser = TokenParser('(');
		private static readonly Parser<char, char> CloseParenthesisParser = TokenParser(')');

		private static readonly Parser<char, ArithmeticExpression> NumberParser = Map(value => (ArithmeticExpression)new NumberExpression(value), TokenParser(Real));

		private static readonly BinaryOperatorParser AdditionParser = BinaryOperatorParser('+', (addend1, addend2) => new AdditionExpression(addend1, addend2));
		private static readonly BinaryOperatorParser SubtractionParser = BinaryOperatorParser('-', (minuend, subtrahend) => new SubtractionExpression(minuend, subtrahend));
		private static readonly BinaryOperatorParser MultiplicationParser = BinaryOperatorParser('*', (multiplier, multiplicand) => new MultiplicationExpression(multiplier, multiplicand));
		private static readonly BinaryOperatorParser DivisionParser = BinaryOperatorParser('/', (dividend, divisor) => new DivisionExpression(dividend, divisor));

		private static BinaryOperatorParser BinaryOperatorParser(char operatorChar, Func<ArithmeticExpression, ArithmeticExpression, ArithmeticExpression> expressionFactory)
		{
			return TokenParser(operatorChar).Select(_ => expressionFactory);
		}

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
