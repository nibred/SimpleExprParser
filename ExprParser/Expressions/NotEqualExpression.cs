using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExprParser.Expressions;

internal class NotEqualExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public NotEqualExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate() => Convert.ToInt32(_leftExpression.Evaluate() != _rightExpression.Evaluate());
}
