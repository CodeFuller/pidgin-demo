namespace PidginDemo.Library.Expressions
{
	public abstract class ArithmeticExpression
	{
		public abstract void Visit(IArithmeticExpressionVisitor visitor);
	}
}
