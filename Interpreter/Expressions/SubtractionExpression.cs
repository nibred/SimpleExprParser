﻿namespace Interpreter.Expressions;

internal class SubtractionExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public SubtractionExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Interpret() => _leftExpression.Interpret() - _rightExpression.Interpret();
}
