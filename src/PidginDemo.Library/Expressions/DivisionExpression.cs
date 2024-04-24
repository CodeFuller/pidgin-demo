using System;

namespace PidginDemo.Library.Expressions
{
	public class DivisionExpression : ArithmeticExpression
	{
		public ArithmeticExpression Dividend { get; }

		public ArithmeticExpression Divisor { get; }

		public DivisionExpression(ArithmeticExpression dividend, ArithmeticExpression divisor)
		{
			Dividend = dividend ?? throw new ArgumentNullException(nameof(dividend));
			Divisor = divisor ?? throw new ArgumentNullException(nameof(divisor));
		}

		public override void Visit(IArithmeticExpressionVisitor visitor)
		{
			visitor.DoForDivisionExpression(this);
		}

		public override string ToString()
		{
			return $"[{Dividend} / {Divisor}]";
		}
	}
}
