using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Expressions;

internal class DivideExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public DivideExpression(Expression leftExpression, Expression rightExpression)
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
        return _leftExpression.Evaluate() / _rightExpression.Evaluate();
    }
}
