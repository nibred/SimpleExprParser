namespace ExprParser.Expressions;

internal class MultExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public MultExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate() => _leftExpression.Evaluate() * _rightExpression.Evaluate();
}
