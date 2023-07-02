using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Expressions;

internal class AdditionExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public AdditionExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Interpret() => _leftExpression.Interpret() + _rightExpression.Interpret();
}
