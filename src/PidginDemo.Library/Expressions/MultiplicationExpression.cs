using System;

namespace PidginDemo.Library.Expressions
{
	public class MultiplicationExpression : ArithmeticExpression
	{
		public ArithmeticExpression Multiplier { get; }

		public ArithmeticExpression Multiplicand { get; }

		public MultiplicationExpression(ArithmeticExpression multiplier, ArithmeticExpression multiplicand)
		{
			Multiplier = multiplier ?? throw new ArgumentNullException(nameof(multiplier));
			Multiplicand = multiplicand ?? throw new ArgumentNullException(nameof(multiplicand));
		}

		public override void Visit(IArithmeticExpressionVisitor visitor)
		{
			visitor.DoForMultiplicationExpression(this);
		}

		public override string ToString()
		{
			return $"[{Multiplier} * {Multiplicand}]";
		}
	}
}
