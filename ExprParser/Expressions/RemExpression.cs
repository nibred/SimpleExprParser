using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExprParser.Expressions;

internal class RemExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public RemExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate()
    {
        if (_rightExpression.Evaluate() == 0)
        {
            throw new DivideByZeroException();
        }
        return _leftExpression.Evaluate() % _rightExpression.Evaluate();
    }
}
