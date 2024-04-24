using PidginDemo.Library.Expressions;

namespace PidginDemo.Library.Extensions
{
	public static class ArithmeticExpressionExtensions
	{
		public static double CalculateValue(this ArithmeticExpression arithmeticExpression)
		{
			var calculationVisitor = new CalculationVisitor();
			arithmeticExpression.Visit(calculationVisitor);

			return calculationVisitor.Value;
		}
	}
}
