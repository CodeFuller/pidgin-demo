using PidginDemo.Library.Expressions;

namespace PidginDemo.Library
{
	public interface IArithmeticExpressionVisitor
	{
		void DoForNumberExpression(NumberExpression numberExpression);

		void DoForAdditionExpression(AdditionExpression additionExpression);

		void DoForSubtractionExpression(SubtractionExpression subtractionExpression);

		void DoForMultiplicationExpression(MultiplicationExpression multiplicationExpression);

		void DoForDivisionExpression(DivisionExpression divisionExpression);
	}
}
