namespace ExprParser.Expressions;

internal class AdditionExpression : Expression
{
    private Expression _leftExpression;
    private Expression _rightExpression;

    public AdditionExpression(Expression leftExpression, Expression rightExpression)
    {
        _leftExpression = leftExpression;
        _rightExpression = rightExpression;
    }
    public override int Evaluate() => _leftExpression.Evaluate() + _rightExpression.Evaluate();
}
