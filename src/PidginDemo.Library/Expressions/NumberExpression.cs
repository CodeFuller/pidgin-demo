using System.Globalization;

namespace PidginDemo.Library.Expressions
{
	public class NumberExpression : ArithmeticExpression
	{
		public double Value { get; set; }

		public NumberExpression(double value)
		{
			Value = value;
		}

		public override void Visit(IArithmeticExpressionVisitor visitor)
		{
			visitor.DoForNumberExpression(this);
		}

		public override string ToString()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}
	}
}
