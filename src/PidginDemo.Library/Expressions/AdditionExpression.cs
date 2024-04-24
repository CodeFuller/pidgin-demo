using System;

namespace PidginDemo.Library.Expressions
{
	public class AdditionExpression : ArithmeticExpression
	{
		public ArithmeticExpression Addend1 { get; }

		public ArithmeticExpression Addend2 { get; }

		public AdditionExpression(ArithmeticExpression addend1, ArithmeticExpression addend2)
		{
			Addend1 = addend1 ?? throw new ArgumentNullException(nameof(addend1));
			Addend2 = addend2 ?? throw new ArgumentNullException(nameof(addend2));
		}

		public override void Visit(IArithmeticExpressionVisitor visitor)
		{
			visitor.DoForAdditionExpression(this);
		}

		public override string ToString()
		{
			return $"[{Addend1} + {Addend2}]";
		}
	}
}
