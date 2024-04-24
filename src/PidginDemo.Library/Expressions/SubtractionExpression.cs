using System;

namespace PidginDemo.Library.Expressions
{
	public class SubtractionExpression : ArithmeticExpression
	{
		public ArithmeticExpression Minuend { get; }

		public ArithmeticExpression Subtrahend { get; }

		public SubtractionExpression(ArithmeticExpression minuend, ArithmeticExpression subtrahend)
		{
			Minuend = minuend ?? throw new ArgumentNullException(nameof(minuend));
			Subtrahend = subtrahend ?? throw new ArgumentNullException(nameof(subtrahend));
		}

		public override void Visit(IArithmeticExpressionVisitor visitor)
		{
			visitor.DoForSubtractionExpression(this);
		}

		public override string ToString()
		{
			return $"[{Minuend} - {Subtrahend}]";
		}
	}
}
