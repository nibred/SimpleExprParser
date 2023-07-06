﻿namespace ExprParser.Expressions;

internal class SubtractionExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public SubtractionExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate() => _leftExpression.Evaluate() - _rightExpression.Evaluate();
}
