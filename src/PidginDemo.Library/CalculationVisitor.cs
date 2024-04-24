using System;
using System.Collections.Generic;
using PidginDemo.Library.Expressions;

namespace PidginDemo.Library
{
	internal class CalculationVisitor : IArithmeticExpressionVisitor
	{
		private readonly Stack<double> stack = new();

		public double Value
		{
			get
			{
				if (stack.Count != 1)
				{
					throw new InvalidOperationException($"The number of values in calculator stack is invalid: {stack.Count}");
				}

				return stack.Peek();
			}
		}

		public void DoForNumberExpression(NumberExpression numberExpression)
		{
			stack.Push(numberExpression.Value);
		}

		public void DoForAdditionExpression(AdditionExpression additionExpression)
		{
			ProcessBinaryOperator(additionExpression.Addend1, additionExpression.Addend2, (x, y) => x + y);
		}

		public void DoForSubtractionExpression(SubtractionExpression subtractionExpression)
		{
			ProcessBinaryOperator(subtractionExpression.Minuend, subtractionExpression.Subtrahend, (x, y) => x - y);
		}

		public void DoForMultiplicationExpression(MultiplicationExpression multiplicationExpression)
		{
			ProcessBinaryOperator(multiplicationExpression.Multiplier, multiplicationExpression.Multiplicand, (x, y) => x * y);
		}

		public void DoForDivisionExpression(DivisionExpression divisionExpression)
		{
			ProcessBinaryOperator(divisionExpression.Dividend, divisionExpression.Divisor, (x, y) => x / y);
		}

		private void ProcessBinaryOperator(ArithmeticExpression operand1, ArithmeticExpression operand2, Func<double, double, double> valueProvider)
		{
			operand1.Visit(this);
			var value1 = stack.Pop();

			operand2.Visit(this);
			var value2 = stack.Pop();

			var resultValue = valueProvider(value1, value2);

			stack.Push(resultValue);
		}
	}
}
